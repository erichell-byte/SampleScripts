using System;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;
using UnityEngine.EventSystems;

namespace Controls.ControlsAbstracts
{
    public abstract class AbstractOutputControl<TValueType> : AbstractControl<TValueType>
    {

        public override void OnEnable()
        {
            base.OnEnable();
            OutputPubSub.Receive<TValueType>(ControlName)
                .Subscribe(msg =>
                {
                    Value = msg;
                    OnControlInteraction();
                })
                .AddTo(Disposables);
        }

        public override void OnTouchBegun(PointerEventData eventData)
        {

        }

        public override void OnTouchEnded(PointerEventData eventData)
        {

        }

        public override void OnTouchMoved(PointerEventData eventData)
        {

        }
    }
}
