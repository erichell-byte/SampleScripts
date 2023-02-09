using System;
using Code.LocomotiveSystems;
using Controls.ControlNames;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;

namespace UI.MPSU
{
    public class MpsuSelectingManagingSectionScreenSecond : MpsuBaseScreen
    {
        public override void OnEnable()
        {
            base.OnEnable();
            
            LocomotiveSystemsPubSub.Receive<float>(LocomotiveSystem.Speed).Subscribe(DisplayLocomotiveSpeed).AddTo(_disposable);
            
            InputPubSub.Receive<int>(LocomotiveControls.HandleReverse).Subscribe(v =>
            {
                OutputPubSub.Publish(MpsuControls.ReversorState, v);
            }).AddTo(_disposable);
            
            LocomotiveSystemsPubSub.Receive<EngineStateData>(LocomotiveSystem.EngineStateData).Subscribe(DisplayEngineInfo)
                .AddTo(_disposable);
            
            
        }


        private void DisplayLocomotiveSpeed(float value)
        {
            OutputPubSub.Publish(MpsuControls.Speed, value.ToString("F1"));
        }

        private void DisplayEngineInfo(EngineStateData engineData)
        {
            HandleEnginesMode(engineData.enginesMode);
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