using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using Controls.ControlsMessageBus;
using Controls.ScreenControlsAbstract;
using Extensions.RxExtensions;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuBaseScreen : MonoBehaviour
    {
        [SerializeField] protected List<ButtonsLine<AbstractScreenButton<bool>>> buttonLines = new List<ButtonsLine<AbstractScreenButton<bool>>>();
        [SerializeField] protected string screenName;
        
        protected int currentRaw, currentColumn;
        protected CompositeDisposable _disposable;
        
        
        public MpsuScreenNames currentScreenName;

        

        
        public virtual void OnEnable()
        {
            this._disposable = new CompositeDisposable();
            ShowTopPanelButton();
            
            OutputPubSub.Receive<bool>(MpsuScreenActionControls.СтрелкаВниз).Subscribe(msg => SwitchingActiveButton(MpsuScreenActionControls.СтрелкаВниз, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenActionControls.СтрелкаВлево).Subscribe(msg => SwitchingActiveButton(MpsuScreenActionControls.СтрелкаВлево, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenActionControls.СтрелкаВправо).Subscribe(msg => SwitchingActiveButton(MpsuScreenActionControls.СтрелкаВправо, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenActionControls.СтрелкаВверх).Subscribe(msg => SwitchingActiveButton(MpsuScreenActionControls.СтрелкаВверх, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenActionControls.Активировать).Subscribe(ActivateSelectedButton).AddTo(_disposable);;
            
            OutputPubSub.Publish(MpsuScreenTopPanelControls.НазваниеЭкрана, screenName);
            
            currentRaw = 0;
            currentColumn = 0;
            buttonLines[currentRaw].line[currentColumn].SelectButton();
        }

        public virtual void OnDisable()
        {
            DeselectButton();
            HideTopPanelButton();
            _disposable?.Dispose();
        }

        public void SwitchingActiveButton(MpsuScreenActionControls switchingArrowsTypes, bool isActive)
        {
            if (isActive)
            {
                switch (switchingArrowsTypes)
                {
                    case MpsuScreenActionControls.СтрелкаВверх:
                        if (currentRaw - 1 >= 0)
                        {
                            DeselectButton();
                            currentRaw--;
                            if (buttonLines[currentRaw].line.Count <= currentColumn) currentColumn = 0;
                        }

                        break;
                    case MpsuScreenActionControls.СтрелкаВниз:
                        if (currentRaw + 1 < buttonLines.Count)
                        {
                            DeselectButton();
                            currentRaw++;
                            if (buttonLines[currentRaw].line.Count <= currentColumn) currentColumn = 0;
                        }

                        break;
                    case MpsuScreenActionControls.СтрелкаВлево:
                        if (currentColumn - 1 >= 0)
                        {
                            DeselectButton();
                            currentColumn--;
                        }

                        break;
                    case MpsuScreenActionControls.СтрелкаВправо:
                        if (currentColumn + 1 < buttonLines[currentRaw].line.Count)
                        {
                            DeselectButton();
                            currentColumn++;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                buttonLines[currentRaw].line[currentColumn].SelectButton();
            }
        }

        protected void ActivateSelectedButton(bool isActive)
        {
            if (isActive && buttonLines[currentRaw].line[currentColumn].isSelected)
            {
                buttonLines[currentRaw].line[currentColumn].ActivateButton();
            }
                
        }

        protected void DeselectButton()
        {
            buttonLines[currentRaw].line[currentColumn].DeselectButton();
        }

        public void HideScreen()
        {
            gameObject.SetActive(false);
        }

        public void ShowScreen()
        {
            gameObject.SetActive(true);
        }

        protected void ShowTopPanelButton()
        {
            for (int i = 0, count = buttonLines[0].line.Count; i < count; i++)
            {
                buttonLines[0].line[i].gameObject.SetActive(true);
            }
        }
        
        private void HideTopPanelButton()
        {
            for (int i = 0, count = buttonLines[0].line.Count; i < count; i++)
            {
                buttonLines[0].line[i].gameObject.SetActive(false);
            }
        }
        
    }
}