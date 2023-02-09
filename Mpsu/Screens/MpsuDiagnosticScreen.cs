using System;
using Controls.ControlsMessageBus;
using Controls.ScreenControlsAbstract;
using Extensions.RxExtensions;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuDiagnosticScreen : MpsuBaseScreen
    {
        [SerializeField] private ButtonsLine<AbstractScreenButton<bool>> computerButtonLine;
        [SerializeField] private ButtonsLine<AbstractScreenButton<bool>> associatedEquipmentButtonLine;
        [SerializeField] private ButtonsLine<AbstractScreenButton<bool>> electroEquipmentButtonLine;
        [SerializeField] private ButtonsLine<AbstractScreenButton<bool>> diezelEquipmentButtonLine;

        private ButtonsLine<AbstractScreenButton<bool>> activeButtonsLine;
        private MpsuScreenButtonControls activePanelName;


        public override void OnEnable()
        {
            base.OnEnable();
            OutputPubSub.Receive<bool>(MpsuScreenButtonControls.ЭВМ).Subscribe(msg => EnableBottomButtonPanel(MpsuScreenButtonControls.ЭВМ, msg)).AddTo(_disposable);
            OutputPubSub.Receive<bool>(MpsuScreenButtonControls.Электрооборудование).Subscribe(msg => EnableBottomButtonPanel(MpsuScreenButtonControls.Электрооборудование, msg)).AddTo(_disposable);
            OutputPubSub.Receive<bool>(MpsuScreenButtonControls.Дизель).Subscribe(msg => EnableBottomButtonPanel(MpsuScreenButtonControls.Дизель, msg)).AddTo(_disposable);
            OutputPubSub.Receive<bool>(MpsuScreenButtonControls.ВспомогательноеОборудование).Subscribe(msg => EnableBottomButtonPanel(MpsuScreenButtonControls.ВспомогательноеОборудование, msg)).AddTo(_disposable);
            
        }

        public override void OnDisable()
        {
            base.OnDisable();
            DeactivatePreviousActiveButton(activePanelName);
        }

        private void EnableBottomButtonPanel(MpsuScreenButtonControls buttonName, bool isActive)
        {
            
            if (isActive)
            {
                if (activePanelName != buttonName)
                {
                    DeactivatePreviousActiveButton(activePanelName);
                    activePanelName = buttonName;
                }
                if (activeButtonsLine != null)
                {
                    HideActiveBottomLine();
                    RemoveActiveBottomLineFromList();
                }
                switch (buttonName)
                {
                    case MpsuScreenButtonControls.ЭВМ:
                        buttonLines.Add(computerButtonLine);
                        ShowBottomPanel(computerButtonLine);
                        activeButtonsLine = computerButtonLine;
                        break;
                    case MpsuScreenButtonControls.Электрооборудование:
                        buttonLines.Add(electroEquipmentButtonLine);
                        ShowBottomPanel(electroEquipmentButtonLine);
                        activeButtonsLine = electroEquipmentButtonLine;
                        break;
                    case MpsuScreenButtonControls.Дизель:
                        buttonLines.Add(diezelEquipmentButtonLine);
                        ShowBottomPanel(diezelEquipmentButtonLine);
                        activeButtonsLine = diezelEquipmentButtonLine;
                        break;
                    case MpsuScreenButtonControls.ВспомогательноеОборудование:
                        buttonLines.Add(associatedEquipmentButtonLine);
                        ShowBottomPanel(associatedEquipmentButtonLine);
                        activeButtonsLine = associatedEquipmentButtonLine;
                        break;
                }
            }
            else
            {
                if (activeButtonsLine != null)
                {
                    HideActiveBottomLine();
                    RemoveActiveBottomLineFromList();
                }
            }
            
        }

        private void DeactivatePreviousActiveButton(MpsuScreenButtonControls buttonName)
        {
            if ((bool?)InputPubSub.GetValue(buttonName) == false)
                return;
            foreach (var centrButton in buttonLines[1].line)
            {
                if ((MpsuScreenButtonControls)centrButton.ControlName == buttonName)
                    centrButton.ActivateButton();
            }
        }

        private void HideActiveBottomLine()
        {
            foreach (var button in activeButtonsLine.line)
            {
                button.gameObject.SetActive(false);
            }
        }

        private void ShowBottomPanel(ButtonsLine<AbstractScreenButton<bool>> buttonsLine)
        {
            foreach (var button in buttonsLine.line)
            {
                button.gameObject.SetActive(true);
            }
        }

        private void RemoveActiveBottomLineFromList()
        {
            buttonLines.Remove(activeButtonsLine);
        }
    }
}




