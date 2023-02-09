using System;
using Code.LocomotiveSystems;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;

namespace UI.MPSU
{
    public class MpsuManagementScreen : MpsuBaseScreen
    {
        public override void OnEnable()
        {
            base.OnEnable();
            
            LocomotiveSystemsPubSub.Receive<EngineStateData>(LocomotiveSystem.EngineStateData).Subscribe(HandleEngineInfo)
                .AddTo(_disposable);
            
        }
        

        private void HandleEngineInfo(EngineStateData engineData)
        {
            OutputPubSub.Publish(MpsuControls.OilPressureFirstSection, engineData.oilPressureFirstSection);
            OutputPubSub.Publish(MpsuControls.OilPressureSecondSection, engineData.oilPressureSecondSection);
            OutputPubSub.Publish(MpsuControls.FuelPressureFirstSection, engineData.fuelPressureFirstSection);
            OutputPubSub.Publish(MpsuControls.FuelPressureSecondSection, engineData.fuelPressureSecondSection);
            OutputPubSub.Publish(MpsuControls.OilTemperatureFirstSection, engineData.oilTemperatureFirstSection);
            OutputPubSub.Publish(MpsuControls.OilTemperatureSecondSection, engineData.oilTemperatureSecondSection);
            OutputPubSub.Publish(MpsuControls.WaterTemperatureFirstSection, engineData.waterTemperatureFirstSection);
            OutputPubSub.Publish(MpsuControls.WaterTemperatureSecondSection, engineData.waterTemperatureSecondSection);
        }
    }
}