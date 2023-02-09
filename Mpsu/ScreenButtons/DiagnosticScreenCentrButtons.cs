using UnityEngine;
using UnityEngine.UI;

namespace Controls.ScreenControlsAbstract
{
    public class DiagnosticScreenCentrButtons : AbstractScreenButton<bool>
    {
        [SerializeField] private Image buttonDefaultImage;
        [SerializeField] private Image buttonOnPointImage;
        [SerializeField] private Image buttonActiveImage;
        [SerializeField] private Image buttonActiveOnPointImage;
        
        internal override void OnControlInteraction()
        {
            HideAllImage();
            if (isSelected && Value)
            {
                buttonActiveOnPointImage.enabled = true;
            }
            else if (!isSelected && Value)
            {
                buttonActiveImage.enabled = true;
            }
            else if (!isSelected && !Value)
            {
                buttonDefaultImage.enabled = true;
            }
            else if (isSelected && !Value)
            {
                buttonOnPointImage.enabled = true;
            }
        }

        private void HideAllImage()
        {
            buttonDefaultImage.enabled = false;
            buttonActiveImage.enabled = false;
            buttonOnPointImage.enabled = false;
            buttonActiveOnPointImage.enabled = false;
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
            Value = !Value;
            OnControlInteraction();
        }

        public override void DeactivateButton()
        {
            
        }
    }
}