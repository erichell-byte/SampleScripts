using Controls.ControlsMessageBus;
using UnityEngine;

namespace Controls.ControlsAbstracts
{
    public abstract class AbstractInputControl<TValueType> : AbstractControl<TValueType>
    {
        private TValueType _value;

        internal override TValueType Value
        {
            get => _value;
            set
            {
                _value = value;
                InputPubSub.Publish(ControlName, _value);
            }
        }
    }
}
