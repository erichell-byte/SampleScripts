using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuScreenSwitcher : MonoBehaviour
    {
        public List<MpsuBaseScreen> Screens => screens;
        public int debugIndexScreen;
        
        private CompositeDisposable _disposable;
        private List<MpsuBaseScreen> screens = new List<MpsuBaseScreen>();
        private MpsuBaseScreen activeScreen;
        private MpsuBaseScreen previousActiveScreen;
        private void Awake()
        {
            for (int i = 0, count = transform.childCount; i < count; i++)
            {
                MpsuBaseScreen screen;
                if (transform.GetChild(i).TryGetComponent(out screen))
                    Screens.Add(screen);
            }
        }

        private void OnEnable()
        {
            _disposable = new CompositeDisposable();
            foreach (var screen in Screens )
            {
                screen.gameObject.SetActive(false);
            }
            SubscribeSwitchScreens();
            Screens[debugIndexScreen].ShowScreen();
        }
        
        private void OnDisable()
        {
            _disposable?.Dispose();
        }
        
        private void SubscribeSwitchScreens()
        {
            OutputPubSub.Receive<bool>(MpsuScreenTopPanelControls.Вверх).Subscribe(ShowPreviousScreen).AddTo(_disposable);;

            OutputPubSub.Receive<bool>(MpsuScreenNames.Главный).Subscribe(msg => SwitchScreen(MpsuScreenNames.Главный, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.Управление).Subscribe(msg => SwitchScreen(MpsuScreenNames.Управление, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ТяговаяСхема).Subscribe(msg => SwitchScreen(MpsuScreenNames.ТяговаяСхема, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.САРТДизеля).Subscribe(msg => SwitchScreen(MpsuScreenNames.САРТДизеля, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.РегуляторМощности).Subscribe(msg => SwitchScreen(MpsuScreenNames.РегуляторМощности, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.БортоваяСеть).Subscribe(msg => SwitchScreen(MpsuScreenNames.БортоваяСеть, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ВозбуждениеГенератора).Subscribe(msg => SwitchScreen(MpsuScreenNames.ВозбуждениеГенератора, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.СистемаОхлажденияТЭДиВУ).Subscribe(msg => SwitchScreen(MpsuScreenNames.СистемаОхлажденияТЭДиВУ, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ДействияБригадыПриТревожномСообщении).Subscribe(msg => SwitchScreen(MpsuScreenNames.ДействияБригадыПриТревожномСообщении, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.Автопрогрев).Subscribe(msg => SwitchScreen(MpsuScreenNames.Автопрогрев, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.Диагностика).Subscribe(msg => SwitchScreen(MpsuScreenNames.Диагностика, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.Компрессор).Subscribe(msg => SwitchScreen(MpsuScreenNames.Компрессор, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.Справка).Subscribe(msg => SwitchScreen(MpsuScreenNames.Справка, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.Цилиндры).Subscribe(msg => SwitchScreen(MpsuScreenNames.Цилиндры, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.АрхивСообщений).Subscribe(msg => SwitchScreen(MpsuScreenNames.АрхивСообщений, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ВводПараметров).Subscribe(msg => SwitchScreen(MpsuScreenNames.ВводПараметров, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ДискретныеВходы).Subscribe(msg => SwitchScreen(MpsuScreenNames.ДискретныеВходы, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.КопированиеАрхива).Subscribe(msg => SwitchScreen(MpsuScreenNames.КопированиеАрхива, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.МаслянаяСистема).Subscribe(msg => SwitchScreen(MpsuScreenNames.МаслянаяСистема, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ТопливнаяСистема).Subscribe(msg => SwitchScreen(MpsuScreenNames.ТопливнаяСистема, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ЭлектродинамическийТормоз).Subscribe(msg => SwitchScreen(MpsuScreenNames.ЭлектродинамическийТормоз, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ВыборУправлениеСекциями).Subscribe(msg => SwitchScreen(MpsuScreenNames.ВыборУправлениеСекциями, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.НастройкаДатыВремени).Subscribe(msg => SwitchScreen(MpsuScreenNames.НастройкаДатыВремени, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.УСО).Subscribe(msg => SwitchScreen(MpsuScreenNames.УСО, msg)).AddTo(_disposable);;
            OutputPubSub.Receive<bool>(MpsuScreenNames.ВыборУправлениеСекциямиВторой).Subscribe(msg => SwitchScreen(MpsuScreenNames.ВыборУправлениеСекциямиВторой, msg)).AddTo(_disposable);;
            
        }
        
        private void SwitchScreen(MpsuScreenNames screenName, bool isActive)
        {
            if (isActive)
            {
                previousActiveScreen = FindActiveScreen();
                HideAllScreens();
                activeScreen = FindScreen(screenName);
                activeScreen.ShowScreen();
            }

        }

        private MpsuBaseScreen FindScreen(MpsuScreenNames screenName)
        {
            foreach (var screen in Screens)
            {
                if (screen.currentScreenName == screenName)
                {
                    return screen;
                }
            }
            return default;
        }

        private void HideAllScreens()
        {
            foreach (var screen in Screens)
            {
                screen.HideScreen();
            }
        }
        
        private MpsuBaseScreen FindActiveScreen()
        {
            foreach (var screen in Screens)
            {
                if (screen.gameObject.activeSelf)
                {
                    return screen;
                }
            }
            return default;
        }

        private void ShowPreviousScreen(bool isActive)
        {
            if (isActive && previousActiveScreen != activeScreen)
            {
                HideAllScreens();
                previousActiveScreen.ShowScreen();
            }
            
        }
    }
}