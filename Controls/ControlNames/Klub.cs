using System.ComponentModel;

namespace Controls.ControlNames
{
    public enum Klub
    {
        [Description("[float] Координата локомотива (железнодорожная),м")]
        LocomotiveCoordinate = 0,

        [Description("[float] Текущая скорость")]
        CurrentSpeed = 1,

        [Description("[float] Максимальная скорость")]
        MaxSpeed = 2,

        [Description("[float] Целевая скорость")]
        TargetSpeed = 3,

        [Description("[bool] Направление движения вперед")]
        MovementDirectionIsForward = 4,

        [Description("[bool] Направление движения назад")]
        MovementDirectionIsBackward = 5,

        [Description("[float] расстояние до следующей точки")]
        DistanceToNextTarget = 6,

        [Description("[string] Имя следующей станции (8 символов)")]
        NextStationName = 7,

        [Description("[bool] Маневровый режим включен")]
        IndicatorManeuveringMode = 8,

        [Description("[bool] Поездной режим включен")]
        IndicatorLongWayMode = 9,

        [Description("[bool] Кассета авторизации вставлена")]
        IndicatorСassettePluggedIn = 10,

        [Description("[bool] Индикатор 'Внимание'")]
        IndicatorAlert = 11,

        [Description("[float] Давление в ТМ ( в кгс/ см2 или в Мпа)")]
        TmPressure = 12,

        [Description("[float] Давление в УР (в кгс/ см2 или в Мпа)")]
        UrPressure = 13,

        [Description("[string] Номер пути, на котором находится локомотив и признак его правильности (Пр или НП)")]
        WayNumberText = 14,

        [Description("[float] Ускорение, м/с2 (4 цифры)")]
        Velocity = 15,

        [Description("[float] Коэффициент торможения (4 цифры)")]
        BrakingCoefficient = 16,

        [Description("[string] Информационная строка")]
        InformationText = 17,

        [Description("[int] Нажата кнопка на цифровом блоке")]
        ButtonKeyboardNumber = 18,

        [Description("[bool] Кнопка Плюс")]
        ButtonPlus = 19,

        [Description("[bool] Кнопка Минут")]
        ButtonMinus = 20,

        [Description("[bool] Кнопка ВК")]
        ButtonVk = 21,

        [Description("[bool] Кнопка РМП")]
        ButtonRmp = 22,

        [Description("[bool] Кнопка F")]
        ButtonF = 23,

        [Description("[bool] Кнопка П")]
        ButtonP = 24,

        [Description("[bool] Кнопка И")]
        ButtonI = 25,

        [Description("[bool] Кнопка Л")]
        ButtonL = 26,

        [Description("[bool] Кнопка K")]
        ButtonK = 27,

        [Description("[bool] Кнопка Вниз")]
        ButtonDown = 28,

        [Description("[bool] Кнопка Вверх")]
        ButtonUp = 29,

        [Description("[bool] Кнопка >0<")]
        ButtonZeroReset = 30,

        [Description("[bool] Кнопка Подтяг")]
        ButtonPulling = 31,

        [Description("[bool] Кнопка Отправить")]
        ButtonSend = 32,

        [Description("[bool] Кнопка ОС")]
        ButtonOs = 33,

        [Description("[bool] Кнопка K20")]
        ButtonK20 = 34,
    }
}
