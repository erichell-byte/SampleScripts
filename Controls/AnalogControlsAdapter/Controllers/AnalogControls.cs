#nullable enable

using System;
using System.Collections;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Controls.AnalogControlsAdapter.Adapters;
using Controls.AnalogControlsAdapter.Enums;
using Controls.AnalogControlsAdapter.Models;
using Controls.AnalogControlsAdapter.Protocol;
using Controls.AnalogControlsAdapter.Utils;
using Controls.ControlNames;
using Controls.ControlsMessageBus;
using Extensions;
using Extensions.RxExtensions;
using UnityEngine;

namespace Controls.AnalogControlsAdapter.Controllers
{
    public class AnalogControls : IDisposable
    {
        private readonly IAnalogInputAdapter _requestAdapter;
        private readonly ControllerConfig[] _configs;
        private readonly int[] _displaysCapacity = { 3, 3, 4 };
        private readonly CompositeDisposable _subscribes = new();
        private readonly CancellationTokenSource? _updateCts;
        private readonly SemaphoreSlim _lock = new(1, 1);

        // OUT only
        private readonly int[][] _arrowValues;
        private readonly int?[] _displaysValues;
        private readonly BitArray _ledValues = new(3);

        // IN OUT
        private AnalogControllerData[]? _analogControllerData;

        public AnalogControls(IAnalogInputAdapter requestAdapter, ControllerConfig[] configs)
        {
            _requestAdapter = requestAdapter;
            _configs = configs;
            _displaysValues = new int?[3];
            _arrowValues = configs
                .Where(c => c.Arrows.Count > 0)
                .Select(config => new int[config.Arrows.Count])
                .ToArray();

            _requestAdapter.AnalogControllerData.Subscribe(OnValuesReceived).AddTo(_subscribes);

            // Подписка на включение / выключение лампочек
            foreach (var (pin, controllerIdx, portIdx, pinIdx) in configs.TraversePins(true))
            {
                if (pin.PinType != PinType.Out)
                    continue;

                OutputPubSub.Receive<bool>(pin.ControlType!)
                    .DistinctUntilChanged()
                    .Throttle(TimeSpan.FromSeconds(1))
                    .Subscribe(value => { _ = SetPinValue(controllerIdx, portIdx, pinIdx, value); })
                    .AddTo(_subscribes);
            }

            // Подписка на включение / выключение лампочек и текста в саут
            if (configs.Any(config => config.UseSaut))
            {
                var sautText = new[]
                {
                    Saut.SpeedReal,
                    Saut.SpeedMax,
                    Saut.NextTrafficLightDistance
                };

                foreach (var (controlName, displayIndex) in sautText.WithIndex())
                {
                    Observable.CombineLatest(
                            InputPubSub.Receive<bool>(LocomotiveControls.ToggleEpc),
                            InputPubSub.Receive<bool>(LocomotiveControls.KeyEpc),
                            OutputPubSub.Receive<int>(controlName).DistinctUntilChanged(),
                            (toggle, key, newValue) => (toggle, key, newValue)
                        )
                        .Subscribe(tuple =>
                        {
                            if (tuple.key && tuple.toggle)
                                _ = SetDigitalDisplayValue(displayIndex, tuple.newValue);
                            else
                                _ = SetDigitalDisplayValue(displayIndex, null);
                        })
                        .AddTo(_subscribes);
                }

                var sautLed = new[]
                {
                    Saut.SautEnabledLed,
                    Saut.SautDisabledLed,
                    Saut.ForbiddenBrakeRelease
                };

                foreach (var (controlName, ledIndex) in sautLed.WithIndex())
                {
                    Observable.CombineLatest(
                            InputPubSub.Receive<bool>(LocomotiveControls.ToggleEpc),
                            InputPubSub.Receive<bool>(LocomotiveControls.KeyEpc),
                            OutputPubSub.Receive<bool>(controlName),
                            (toggle, key, newValue) => toggle && key && newValue
                        )
                        .DistinctUntilChanged()
                        .Subscribe(newValue => SetLedValue(ledIndex, newValue))
                        .AddTo(_subscribes);
                }
            }

            // Подписка на изменение состояния стрелок
            foreach (var (config, controllerIdx) in configs.WithIndex())
            {
                foreach (var (arrow, arrowIdx) in config.Arrows.WithIndex().Where(v => v.Item1.ControlType != null))
                {
                    OutputPubSub.Receive<float>(arrow.ControlType!).Subscribe(newValue =>
                    {
                        _ = SetArrowDeviceValue(controllerIdx, arrowIdx, arrow.ControlType!, newValue);
                    }).AddTo(_subscribes);
                }
            }
        }

        /// Сравнивает новые значения и старые. Если что-то изменилось шлет событие изменения 
        private void OnValuesReceived(byte[] rawValues)
        {
            var controllerData = ResponseParser.ParseAnalogControllerData(rawValues, _configs);
            if (controllerData == null)
                return;

            foreach (var (pin, controllerIdx, portIdx, pinIdx) in _configs.TraversePins(true))
            {
                var oldControllerValues = _analogControllerData?.ElementAtOrDefault(controllerIdx);
                var newControllerValues = controllerData?.ElementAtOrDefault(controllerIdx);

                if (pin.PinType == PinType.Adc)
                {
                    var oldValue = oldControllerValues?.ADC?.ElementAtOrDefault(portIdx * 8 + pinIdx);
                    var newValue = newControllerValues?.ADC?.ElementAtOrDefault(portIdx * 8 + pinIdx);

                    if (newValue != null && oldValue != newValue)
                        PublishNormalizedAdcValue(pin, newValue.Value);
                }

                if (pin.PinType is PinType.Out)
                {
                    var oldValue = oldControllerValues?.PortPins?.ElementAtOrDefault(portIdx)?[pinIdx];
                    var newValue = newControllerValues?.PortPins?.ElementAtOrDefault(portIdx)?[pinIdx];

                    if (newValue != null && oldValue != newValue)
                        InputPubSub.Publish(pin.ControlType!, newValue);
                }

                if (pin.PinType is PinType.In)
                {
                    var oldValue = oldControllerValues?.PortPins?.ElementAtOrDefault(portIdx)?[pinIdx];
                    var newValue = newControllerValues?.PortPins?.ElementAtOrDefault(portIdx)?[pinIdx];

                    if (newValue != null && oldValue != newValue)
                        InputPubSub.Publish(pin.ControlType!, !newValue);
                }
            }

            _analogControllerData = controllerData!;
        }

        private void PublishNormalizedAdcValue(Pin pin, int newValue)
        {
            var controlType = pin.ControlType;

            // Нормализация значений ADC
            switch (controlType)
            {
                case LocomotiveControls.HandleReverse:
                case LocomotiveControls.HandleDriverCrane215State:
                case LocomotiveControls.HandleDriverCrane395State:
                    InputPubSub.Publish(controlType, Mathf.RoundToInt(pin.NormalizeValue(newValue)));
                    break;
                default:
                    InputPubSub.Publish(controlType, pin.NormalizeValue(newValue));
                    break;
            }
        }

        /// <summary> Включает или выключает лампочку </summary>
        public async Task SetPinValue(int controller, int port, int pin, bool value)
        {
            if (_analogControllerData == null)
                return;

            await _lock.WaitAsync();
            try
            {
                var valueCopy = _analogControllerData
                    .Select(data => data.PortPins.Select(port => new BitArray(port)).ToArray()).ToArray();
                valueCopy[controller][port][pin] = value;

                var attempts = 4;

                while (attempts-- > 0)
                {
                    try
                    {
                        await _requestAdapter.SendCommand(Commands.WritePins(valueCopy));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                    }
                }

                _analogControllerData[controller].PortPins[port][pin] = value;
            }
            finally
            {
                _lock.Release();
            }
        }

        /// <summary> Выводит надпись на цифровой дисплей </summary>
        public async Task SetDigitalDisplayValue(int displayIndex, int? newValue)
        {
            if (newValue.HasValue)
            {
                if (displayIndex >= _displaysValues.Length ||
                    NumberUtils.GetNumLength(newValue.Value) > _displaysCapacity[displayIndex])
                    return;
            }


            _displaysValues[displayIndex] = newValue;
            await Update();
        }

        /// <summary> Включает или выключает диоды на устройстве (их всего 3) </summary>
        public async Task SetLedValue(int diodeIndex, bool value)
        {
            if (diodeIndex > 2)
                return;

            _ledValues[diodeIndex] = value;
            await Update();
        }

        /// Устанавливает значение поворота аналоговых стрелок. Максимальное значение 27999 
        public async Task SetArrowDeviceValue(int controllerIndex, int arrowDeviceIndex, Enum arrowControlType,
            float newValue)
        {
            if (controllerIndex >= _configs.Length || arrowDeviceIndex >= _configs[controllerIndex].Arrows.Count ||
                1f < newValue)
                return;

            var intVal = arrowControlType switch
            {
                LocomotiveControls.ArrowAnchor => newValue * (22909f - 1414f) + 1414f,
                LocomotiveControls.ArrowExcitation => newValue * (23181f - 1590f) + 1590f,
                _ => newValue * (23181f - 1590f) + 1590f,
            };

            _arrowValues[controllerIndex][arrowDeviceIndex] = Math.Clamp((int)intVal, 0, 27999);
            await Update();
        }

        private async Task Update()
        {
            try
            {
                var command =
                    Commands.SetUartAnalogControls(_arrowValues, _displaysValues, _displaysCapacity, _ledValues);
                await _requestAdapter.SendCommand(command);
            }
            catch (Exception e)
            {
                Debug.LogError("Error" + e.Message);
            }
        }

        public void Dispose()
        {
            _subscribes.Dispose();
            _updateCts?.Dispose();
        }
    }
}
