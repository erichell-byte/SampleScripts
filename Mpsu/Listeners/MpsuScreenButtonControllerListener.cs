using System;
using System.Reactive.Disposables;
using Controls.ControlsMessageBus;
using Extensions.RxExtensions;
using UnityEngine;

namespace UI.MPSU
{
    public class MpsuScreenButtonControllerListener : MonoBehaviour
    {
        private CompositeDisposable _disposable;

        private void OnEnable()
        {
            _disposable = new CompositeDisposable();
            
            // Топливная Система
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ПовыситьПлотность)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ПовыситьПлотность, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ПонизитьПлотность)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ПонизитьПлотность, msg)).AddTo(_disposable);
            // Копирование Архива
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.НачальнаяДатаУвеличитьДень)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.НачальнаяДатаУвеличитьДень, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.НачальнаяДатаУменьшитьДень)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.НачальнаяДатаУменьшитьДень, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.НачальнаяДатаУвеличитьМесяц)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.НачальнаяДатаУвеличитьМесяц, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.НачальнаяДатаУменьшитьМесяц)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.НачальнаяДатаУменьшитьМесяц, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.НачальнаяДатаУвеличитьГод)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.НачальнаяДатаУвеличитьГод, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.НачальнаяДатаУменьшитьГод)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.НачальнаяДатаУменьшитьГод, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КонечнаяДатаУвеличитьГод)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.КонечнаяДатаУвеличитьГод, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КонечнаяДатаУвеличитьДень)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.КонечнаяДатаУвеличитьДень, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КонечнаяДатаУвеличитьМесяц)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.КонечнаяДатаУвеличитьМесяц, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КонечнаяДатаУменьшитьГод)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.КонечнаяДатаУменьшитьГод, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КонечнаяДатаУменьшитьДень)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.КонечнаяДатаУменьшитьДень, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КонечнаяДатаУменьшитьМесяц)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.КонечнаяДатаУменьшитьМесяц, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.КопироватьАрхивНаФлешку)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.КопироватьАрхивНаФлешку, msg)).AddTo(_disposable);
            //Настройка Даты и Времени
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ЧасыУвеличить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ЧасыУвеличить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ЧасыУменьшить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ЧасыУменьшить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МинутыУвеличить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МинутыУвеличить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МинутыУменьшить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МинутыУменьшить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ДеньУвеличить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ДеньУвеличить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ДеньУменьшить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ДеньУменьшить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МесяцУвеличить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МесяцУвеличить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МесяцУменьшить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МесяцУменьшить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ГодУвеличить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ГодУвеличить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ГодУменьшить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ГодУменьшить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ПрименитьНастройкиВремени)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ПрименитьНастройкиВремени, msg)).AddTo(_disposable);
                //Автопрогрев
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МинТемператураПускаУвеличить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МинТемператураПускаУвеличить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МинТемператураПускаУменьшить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МинТемператураПускаУменьшить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МаксТемператураОстановкиУвеличить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МаксТемператураОстановкиУвеличить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.МаксТемператураОстановкиУменьшить)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.МаксТемператураОстановкиУменьшить, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ЗапускПрогрева)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ЗапускПрогрева, msg)).AddTo(_disposable);
                //Ввод параметров
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ДолжностнойПризнак)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ДолжностнойПризнак, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.РежимРаботы)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.РежимРаботы, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ВремяНачала)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ВремяНачала, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ВремяОкончания)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ВремяОкончания, msg)).AddTo(_disposable);
                //Управление секциями
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ПерваяСекцияХолостойХод)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ПерваяСекцияХолостойХод, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ВтораяСекцияХолостойХод)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ВтораяСекцияХолостойХод, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ТретьяСекцияХолостойХод)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ТретьяСекцияХолостойХод, msg)).AddTo(_disposable);
                // Экран Управление
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ПрокачкаМасла)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ПрокачкаМасла, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ПрокачкаТоплива)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ПрокачкаТоплива, msg)).AddTo(_disposable);
                // Регулятор Мощности
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.УровеньМощности)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.УровеньМощности, msg)).AddTo(_disposable);
                // Экран диагностики
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ЭВМ)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ЭВМ, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.Электрооборудование)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.Электрооборудование, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.Дизель)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.Дизель, msg)).AddTo(_disposable);
            InputPubSub.Receive<bool>(MpsuScreenButtonControls.ВспомогательноеОборудование)
                .Subscribe(msg => OnScreenButtonClick(MpsuScreenButtonControls.ВспомогательноеОборудование, msg)).AddTo(_disposable);
            
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }


        private void OnScreenButtonClick(Enum controlName, bool value)
        {
            OutputPubSub.Publish(controlName, value);
        }
    }
}