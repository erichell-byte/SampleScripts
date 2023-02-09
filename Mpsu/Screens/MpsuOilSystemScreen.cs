using System;
using Code.LocomotiveSystems;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;

namespace UI.MPSU
{
    public class MpsuOilSystemScreen : MpsuBaseScreen
    {
        public override void OnEnable()
        {
            base.OnEnable();
            
            LocomotiveSystemsPubSub.Receive<EngineStateData>(LocomotiveSystem.EngineStateData).Subscribe(DisplayOilInfo)
                .AddTo(_disposable);
        }

        private void DisplayOilInfo(EngineStateData stateData)
        {
            // if (LocomotiveSystemsPubSub.GetValue(LocomotiveSystem.EngineDiesel01State) != "stopped") // нужно будет сделать давление масла в зависимости от секции
                OutputPubSub.Publish(MpsuControls.OilPressureFirstSection, stateData.oilPressureFirstSection.ToString("F"));
            // else
            //     OutputPubSub.Publish(MpsuControls.OilPressureFirstSection, stateData.oilPressureFirstSection.ToString("F"));
        }
    }
}