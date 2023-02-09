using UnityEngine;
using UnityEngine.UI;

namespace Controls.ScreenControlsAbstract
{
    public class ScreenButton : AbstractScreenButton<bool>
    {
        [SerializeField] private Image buttonDefaultImage;
        [SerializeField] private Image buttonOnPointImage;
        
        internal override void OnControlInteraction()
        {
            buttonDefaultImage.enabled = !isSelected;
            buttonOnPointImage.enabled = isSelected;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Value = false;

        }
        
        public override void SelectButton()
        {
            isSelected = true;
            OnControlInteraction();
        }

        public override void DeselectButton()
        {
            isSelected = false;
            OnControlInteraction();
        }

        public override void ActivateButton()
        {
            Value = true;
        }

        public override void DeactivateButton()
        {
            Value = false;
        }
    }
}