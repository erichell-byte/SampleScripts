using System;
using Code.LocomotiveSystems;
using Controls.ControlNames;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuMainScreen : MpsuBaseScreen
    {
        public override void OnEnable()
        {
            base.OnEnable();

            InputPubSub.Receive<int>(LocomotiveControls.HandleReverse).Subscribe(v =>
            {
                OutputPubSub.Publish(MpsuControls.ReversorState, v);
            }).AddTo(_disposable);
            
            LocomotiveSystemsPubSub.Receive<EngineStateData>(LocomotiveSystem.EngineStateData).Subscribe(DisplayEngineInfo)
                .AddTo(_disposable);

            LocomotiveSystemsPubSub.Receive<ElectronicsData>(LocomotiveSystem.ElectronicsData)
                .Subscribe(DisplayElectronicInfo)
                .AddTo(_disposable);
        }



        private void DisplayEngineInfo( EngineStateData stateData)
        {
            // OutputPubSub.Publish(MpsuControls.EngineStateFirstSection, stateData.engineStateFirstSection); // временно
            // OutputPubSub.Publish(MpsuControls.EngineStateSecondSection, stateData.engineStateSecondSection); // временно
            OutputPubSub.Publish(MpsuControls.OilPressureFirstSection, stateData.oilPressureFirstSection);
            OutputPubSub.Publish(MpsuControls.OilPressureSecondSection, stateData.oilPressureSecondSection);
            OutputPubSub.Publish(MpsuControls.FuelPressureFirstSection, stateData.fuelPressureFirstSection);
            OutputPubSub.Publish(MpsuControls.FuelPressureSecondSection, stateData.fuelPressureSecondSection);
            OutputPubSub.Publish(MpsuControls.OilTemperatureFirstSection, stateData.oilTemperatureFirstSection);
            OutputPubSub.Publish(MpsuControls.OilTemperatureSecondSection, stateData.oilTemperatureSecondSection);
            OutputPubSub.Publish(MpsuControls.WaterTemperatureFirstSection, stateData.waterTemperatureFirstSection);
            OutputPubSub.Publish(MpsuControls.WaterTemperatureSecondSection, stateData.waterTemperatureSecondSection);
            
            HandleEnginesMode(stateData.enginesMode);
        }

        private void DisplayElectronicInfo(ElectronicsData electronicsData)
        {
            OutputPubSub.Publish(MpsuControls.VoltageFirstSection, electronicsData.voltageFirstSection);
            OutputPubSub.Publish(MpsuControls.VoltageSecondSection, electronicsData.voltageSecondSection);
            OutputPubSub.Publish(MpsuControls.BatteryChargeFirstSection, electronicsData.batteryChargeFirstSection);
            OutputPubSub.Publish(MpsuControls.BatteryChargeSecondSection, electronicsData.batteryChargeSecondSection);
        }

        void HandleEnginesMode(EngineDieselState engineDieselState)
        {
            switch (engineDieselState)
            {
                case EngineDieselState.Excitation:
                    OutputPubSub.Publish(MpsuControls.EngineMode, "Возбуждение");
                    break;
                case EngineDieselState.Start:
                    OutputPubSub.Publish(MpsuControls.EngineMode, "Пуск");
                    break;
                case EngineDieselState.Stopped:
                    OutputPubSub.Publish(MpsuControls.EngineMode, "Стоп");
                    break;
            }
        }
        
        

        
    }
}