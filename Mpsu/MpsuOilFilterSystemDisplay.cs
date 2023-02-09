using Controls.ControlsAbstracts;
using TMPro;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuOilFilterSystemDisplay : AbstractOutputControl<float>
    {
        [SerializeField] private TextMeshProUGUI beforeFilterText;
        [SerializeField] private TextMeshProUGUI afterFilterText;
        [SerializeField] private TextMeshProUGUI dropOilText;
        [SerializeField] private float AmountOfOilLostByFilter;
        internal override void OnControlInteraction()
        {
            beforeFilterText.text = Value.ToString("0.00");
            float dropOilAmount = Value - AmountOfOilLostByFilter;
            afterFilterText.text = dropOilAmount.ToString("0.00");
            dropOilText.text = dropOilAmount.ToString("0.00");
        }
    }
}