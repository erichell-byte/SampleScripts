#nullable enable

using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Extensions.RxExtensions;


namespace Controls.ControlsMessageBus
{
    /// Класс для работы с событиями ввода (подписка, вызов события)
    public class InputPubSub
    {
        private static volatile Dictionary<Enum, BehaviorSubject<object?>> _observables = new();

        private InputPubSub()
        {
        }

        /// Отправка сообщений об обновлении элемента управления с определенным значением
        public static void Publish(Enum controlName, object value)
        {
            EnsureCreated(controlName);
            _observables[controlName].OnNext(value);
        }

        /// Подписка на определенный элемент управления с определенным типом значения
        public static IObservable<TValue> Receive<TValue>(Enum controlName)
        {
            EnsureCreated(controlName);

            return _observables[controlName]
                .Where(value => value is TValue)!
                .Cast<TValue>();
        }

        /// <summary>Получение текущего значениея элемента управления</summary>
        /// <returns>Значение если оно имеется, иначе null</returns>
        public static object? GetValue(Enum controlName)
        {
            return _observables.ContainsKey(controlName)
                ? _observables[controlName].Value
                : null;
        }

        private static void EnsureCreated(Enum controlName)
        {
            lock (_observables)
            {
                if (_observables.ContainsKey(controlName) == false)
                    _observables[controlName] = new BehaviorSubject<object?>(null);
            }
        }
    }
}
