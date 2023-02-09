using Controls.ControlsAbstracts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MPSU
{
    public class MpsuMainPanelButton : AbstractInputControl<bool>, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image buttonDefaultImage;
        [SerializeField] private Image buttonPressedImage;
        
        
        internal override void OnControlInteraction()
        {
            buttonDefaultImage.enabled = !Value;
            buttonPressedImage.enabled = Value;
        }

        public override void OnTouchBegun(PointerEventData eventData)
        {
            OnPointerDown(eventData);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Value = true;
            OnControlInteraction();
            
        }

        public override void OnTouchEnded(PointerEventData eventData)
        {
            OnPointerUp(eventData);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Value = false;
            OnControlInteraction();
        }
        
        public override void OnTouchMoved(PointerEventData eventData)
        {
        }
    }
}