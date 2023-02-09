using System;
using Code.LocomotiveSystems;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;

namespace UI.MPSU
{
    public class MpsuCompressorScreen : MpsuBaseScreen
    {
        public override void OnEnable()
        {
            base.OnEnable();
            
            LocomotiveSystemsPubSub.Receive<bool>(LocomotiveSystem.CompressorState).Subscribe(DisplayCompressor).AddTo(_disposable); 
        }

        private void DisplayCompressor(bool state)
        {
            OutputPubSub.Publish(MpsuControls.CompressorState, state);
            
            if (state)
            {
                OutputPubSub.Publish(MpsuControls.CompressorState, "Включен");
            }
            else
                OutputPubSub.Publish(MpsuControls.CompressorState, "Выключен");
        }
    }
}