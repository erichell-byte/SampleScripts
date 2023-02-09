using UnityEngine;
using UnityEngine.UI;

namespace Controls.ScreenControlsAbstract
{
    public class ScreenToggle : AbstractScreenButton<bool>
    {
        [SerializeField] private Image toggleOffImage;
        [SerializeField] private Image toggleOnImage;
        [SerializeField] private Image toggleOnPointImage;
        
        
        internal override void OnControlInteraction()
        {
            toggleOffImage.enabled = !Value;
            toggleOnImage.enabled = Value;
        }
        public override void SelectButton()
        {
            isSelected = true;
            toggleOnPointImage.enabled = true;
        }

        public override void DeselectButton()
        {
            isSelected = false;
            toggleOnPointImage.enabled = false;
        }

        public override void ActivateButton()
        {
            Value = !Value;
            OnControlInteraction();
        }

        public override void DeactivateButton()
        {
            
        }
    }
}