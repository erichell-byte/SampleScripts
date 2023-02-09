using System;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;

namespace Controls.ControlsAbstracts
{
    public abstract class AbstractInputControlOut<TValueType> : AbstractControl<TValueType>
    {
        public override void OnEnable()
        {
            base.OnEnable();
            InputPubSub.Receive<TValueType>(ControlName)
                .Subscribe(msg =>
                {
                    Value = msg;
                    OnControlInteraction();
                })
                .AddTo(Disposables);
        }
    }
}