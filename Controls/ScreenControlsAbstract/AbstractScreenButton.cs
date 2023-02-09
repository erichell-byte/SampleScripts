using Controls.ControlsMessageBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controls.ScreenControlsAbstract
{
    public abstract class AbstractScreenButton<TValueType> : ScreenControlAbstract<TValueType>
    {
        private TValueType _value;

        internal bool isSelected;
        internal override TValueType Value
        {
            get => _value;
            set
            {
                _value = value;
                InputPubSub.Publish(ControlName, _value);
            }
        }

        public abstract void SelectButton();

        public abstract void DeselectButton();

        public abstract void ActivateButton();
        public abstract void DeactivateButton();
    }
    
    
}