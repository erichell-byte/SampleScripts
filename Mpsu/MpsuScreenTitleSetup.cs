using System;
using System.Reactive.Disposables;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;
using TMPro;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuScreenTitleSetup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI screenNameText;

        private CompositeDisposable _disposable;
        
        private void OnEnable()
        {
            _disposable = new CompositeDisposable();
            
            OutputPubSub.Receive<string>(MpsuScreenTopPanelControls.НазваниеЭкрана)
                .Subscribe(SwitchScreenName).AddTo(_disposable);
        }
        
        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void SwitchScreenName(string screenName)
        {
            screenNameText.text = screenName;
        }
    }
}