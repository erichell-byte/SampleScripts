using System;
using System.Reactive.Disposables;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

namespace UI.MPSU
{
    public class MpsuMainPanelButtonsListener : MonoBehaviour
    {
        private CompositeDisposable _disposable;

        private void OnEnable()
        {
            _disposable = new CompositeDisposable();
            
            SubscribeButtons();
        }
        
        private void OnDisable()
        {
            _disposable?.Dispose();
        }
        
        private void SubscribeButtons()
        {
            // number panel
            InputPubSub.Receive<bool>(MpsuPanelControls.OneNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.Главный, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.TwoNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.Управление, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.ThreeNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.ТяговаяСхема, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.FourNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.САРТДизеля, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.FiveNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.РегуляторМощности, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.SixNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.БортоваяСеть, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.SevenNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.ВозбуждениеГенератора, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.EightNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.СистемаОхлажденияТЭДиВУ, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.NineNum).Subscribe(msg => OnButtonClick(MpsuScreenNames.ДействияБригадыПриТревожномСообщении, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.CSection).Subscribe(msg => OnButtonClick(MpsuScreenNames.ВыборУправлениеСекциями, msg)).AddTo(_disposable);
            // arrows panel
            InputPubSub.Receive<bool>(MpsuPanelControls.LeftArrow).Subscribe(msg => OnButtonClick(MpsuScreenActionControls.СтрелкаВлево, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.RightArrow).Subscribe(msg => OnButtonClick(MpsuScreenActionControls.СтрелкаВправо, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.DownArrow).Subscribe(msg => OnButtonClick(MpsuScreenActionControls.СтрелкаВниз, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.UpArrow).Subscribe(msg => OnButtonClick(MpsuScreenActionControls.СтрелкаВверх, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuPanelControls.Enter).Subscribe(msg => OnButtonClick(MpsuScreenActionControls.Активировать, msg)).AddTo(_disposable);
        }
        
        private void OnButtonClick<T>(T controlName, bool isActive) where T: Enum
        {
            OutputPubSub.Publish(controlName, isActive);
        }


    }
}