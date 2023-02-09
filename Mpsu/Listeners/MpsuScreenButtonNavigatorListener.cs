using System;
using System.Reactive.Disposables;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuScreenButtonNavigatorListener : MonoBehaviour
    {
        private CompositeDisposable _disposable;
        
        
        private void OnEnable()
        {
            _disposable = new CompositeDisposable();
                
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Вверх)
            .Subscribe(msg => OnButtonClick(MpsuScreenTopPanelControls.Вверх, msg)).AddTo(_disposable); // temporarily
            
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.СостояниеРегулятора)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.РегуляторМощности, msg)).AddTo(_disposable); // temporarily
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Начало)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Главный, msg)).AddTo(_disposable); // temporarily
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.КСтарым)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Главный, msg)).AddTo(_disposable); // temporarily
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.КНовым)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Главный, msg)).AddTo(_disposable); // temporarily
            
            
            
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Автопрогрев)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Автопрогрев, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Справка)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Справка, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Параметры)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.ВводПараметров, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Управление)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Управление, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Диагностика)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Диагностика, msg)).AddTo(_disposable);
            
            // from diagnostic screen
            
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.УСО)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.УСО, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ДатаВремя)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.НастройкаДатыВремени, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.АрхивСообщений)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.АрхивСообщений, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КопированиеНаФлешкуЭкран)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.КопированиеАрхива, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.Компрессор)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Компрессор, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.САРТДизеля)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.САРТДизеля, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ОхлаждениеТЭДиВУ)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.СистемаОхлажденияТЭДиВУ, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.СистемаВозбуждение)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.ВозбуждениеГенератора, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ТяговаяСхема)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.ТяговаяСхема, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ЭдТормоз)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.ЭлектродинамическийТормоз, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.БортоваяСеть)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.БортоваяСеть, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.РегуляторМощности)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.РегуляторМощности, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.Цилиндры)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.Цилиндры, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МаслянаяСистема)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.МаслянаяСистема, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ТопливнаяСистема)
                .Subscribe(msg => OnButtonClick(MpsuScreenNames.ТопливнаяСистема, msg)).AddTo(_disposable);
            
            

        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }


        private void OnButtonClick(Enum controlName, bool value)
        {
            OutputPubSub.Publish(controlName, value);
        }
    }
}