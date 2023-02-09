using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Controls.AnalogControlsAdapter.Adapters;
using Controls.AnalogControlsAdapter.Controllers;
using Controls.AnalogControlsAdapter.Enums;
using Controls.AnalogControlsAdapter.Models;
using Controls.AnalogControlsAdapter.Protocol;
using Controls.ControlNames;
using Controls.ControlsMessageBus;
using CPR.Startup;
using Extensions;
using Extensions.RxExtensions;
using UnityEngine;

namespace Controls.AnalogControlsAdapter
{
    public class AnalogControlsService : MonoBehaviour
    {
        [SerializeField] private bool enableDebugView;

        private static bool _isInitialized;
        private ControllerConfig[] _controllerConfigs;
        private Vector2 _scrollPosition = Vector2.zero;
        private Dictionary<Pin, object> _pinValueMap = new();
        private float[] _arrowsValues = { 0, 0, 0, 0, 0, 0 };
        private BitArray _ledValues = new(3);
        private int[] _displayValues = new int[3];
        private string _errorMessage = "";

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (_isInitialized) return;
            _isInitialized = true;

            var configPath = Path.Join(Application.streamingAssetsPath, Config.Instance.AnalogControlsConfigPath);
            var configText = File.ReadAllText(configPath);

            // Get all available name enums 
            var namespaces = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.IsEnum && t.Namespace == typeof(Klub).Namespace);

            try
            {
                _controllerConfigs = ConfigParser.ParseConfig(configText, namespaces);
                if (_controllerConfigs.Length == 0)
                    throw new Exception("Parse Error");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;
            }

            if (ApplicationArguments.Arguments.ContainsKey("port") == false)
            {
                Debug.LogError("Port not selected, use -port= cmd argument");
                return;
            }

            var analogInputAdapter = AnalogInputComAdapter
                .GetInstance(ApplicationArguments.Arguments["port"], _controllerConfigs).AddTo(this);
            analogInputAdapter.Start()
                .ContinueWith((started) =>
                {
                    try
                    {
                        if (!started.Result)
                            throw new Exception("Can't start com server");
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        _errorMessage = e.Message;
                        return;
                    }


                    new AnalogControls(analogInputAdapter, _controllerConfigs).AddTo(this);
                });

            if (enableDebugView)
                InitDebugView();
        }

        private void InitDebugView()
        {
            var pins = _controllerConfigs
                .SelectMany(config => config.Ports.SelectMany(p => p))
                .ToArray();
            foreach (var pin in pins)
            {
                if (_pinValueMap.ContainsKey(pin) || pin.PinType == PinType.Disabled || pin.ControlType == null)
                    continue;

                switch (pin.PinType)
                {
                    case PinType.Adc:
                        _pinValueMap.Add(pin, "");
                        if (pin.ControlType is LocomotiveControls.HandleReducer395
                            or LocomotiveControls.HandleTraction)
                        {
                            InputPubSub.Receive<float>(pin.ControlType)
                                .Subscribe(value => _pinValueMap[pin] = value.ToString())
                                .AddTo(this);
                        }
                        else
                        {
                            InputPubSub.Receive<int>(pin.ControlType)
                                .Subscribe(value => _pinValueMap[pin] = value.ToString())
                                .AddTo(this);
                        }

                        break;
                    case PinType.Out or PinType.In:
                        _pinValueMap.Add(pin, false);
                        InputPubSub.Receive<bool>(pin.ControlType)
                            .Subscribe(value => { _pinValueMap[pin] = value; }).AddTo(this);
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }


        private Saut GetSautDisplayByIndex(int index)
        {
            return index switch
            {
                0 => Saut.SpeedMax,
                1 => Saut.SpeedReal,
                2 => Saut.NextTrafficLightDistance,
            };
        }

        private Saut GetSautLedByIndex(int index)
        {
            return index switch
            {
                0 => Saut.ForbiddenBrakeRelease,
                1 => Saut.SautDisabledLed,
                2 => Saut.SautEnabledLed,
            };
        }

        [GUITarget(0)]
        private void OnGUI()
        {
            if (!enableDebugView)
                return;

            // Error message
            if (_errorMessage.Length > 0)
            {
                GUILayout.Label(_errorMessage);
                return;
            }

            if (_controllerConfigs == null)
                return;

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.MinWidth(600));
            GUILayout.BeginVertical();

            // Arrows
            foreach (var (arrow, arrowIndex) in _controllerConfigs.SelectMany(control => control.Arrows).Where(a => a.ControlType != null).WithIndex())
            {
                GUILayout.BeginHorizontal();

                var currentValue = _arrowsValues[arrowIndex];
                GUILayout.Label($"{arrow.Name}: {currentValue}");
                var newValue = GUILayout.HorizontalSlider(currentValue, 0, 1);
                if (Math.Abs(newValue - currentValue) > 0.02f)
                {
                    _arrowsValues[arrowIndex] = newValue;
                    OutputPubSub.Publish(arrow.ControlType!, newValue);
                }

                GUILayout.EndHorizontal();
            }

            // Digital displays
            for (int i = 0; i < 3; i++)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Number " + GetSautDisplayByIndex(i));
                var newValue = GUILayout.TextField(_displayValues[i].ToString());
                if (int.TryParse(newValue, out var intVal) && intVal != _displayValues[i])
                {
                    OutputPubSub.Publish(GetSautDisplayByIndex(i), intVal);
                    _displayValues[i] = intVal;
                }

                GUILayout.EndHorizontal();
            }

            // Led indicators (SAUD)
            for (int i = 0; i < 3; i++)
            {
                GUILayout.BeginHorizontal();

                var newValue = GUILayout.Toggle(_ledValues[i], "Led " + (GetSautLedByIndex(i)));
                if (newValue != _ledValues[i])
                {
                    OutputPubSub.Publish(GetSautLedByIndex(i), newValue);
                    _ledValues[i] = newValue;
                }

                GUILayout.EndHorizontal();
            }

            foreach (var pin in _pinValueMap.Keys.ToArray())
            {
                if (pin.PinType is PinType.Adc)
                    GUILayout.Label($"{pin.RealName} ({pin.Name}): {_pinValueMap[pin]}");
                else if (pin.PinType is PinType.In or PinType.Out)
                {
                    var newValue = GUILayout.Toggle((bool)_pinValueMap[pin], $"{pin.RealName} ({pin.Name})");
                    if ((bool)_pinValueMap[pin] != newValue && pin.ControlType != null)
                        OutputPubSub.Publish(pin.ControlType, newValue);
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
    }
}
