using System.ComponentModel;

namespace Controls.ControlNames
{
    public enum Saut
    {
        [Description("Текущая скорость(3 цифры)")]
        SpeedReal,

        [Description("Допустимая скорость (3 цифры)")]
        SpeedMax,

        [Description("Дистанция до светофора  (4 цифры)")]
        NextTrafficLightDistance,

        [Description("САУТ включен или нет (лампочка)")]
        SautEnabledLed,

        [Description("САУТ выключен (лампочка)")]
        SautDisabledLed,

        [Description("САУТ запрет отпуска тормозов")]
        ForbiddenBrakeRelease,
    }
}