using System.ComponentModel;

namespace Controls.ControlNames
{
    public enum MsudControls
    {
        [Description("Электровоз - Направление движения [string - вперед/назад/пусто]")]
        Direction = 0,
        [Description("Электровоз - Силовая схема [string - тяга/рекуперация/пусто]")]
        PowerScheme = 1,
        [Description("Электровоз - Режим [string - авторег_ние/ручное]")]
        Mode = 2,
        [Description("Электровоз - Текущая скорость [string]")]
        Speed = 3,
        [Description("Электровоз - Заданная скорость [string]")]
        TargetSpeed = 4,
        [Description("Электровоз - Ток якоря J [string]")]
        ArmatureCurrentJ = 5,
        [Description("Электровоз - Ток якоря F [string]")]
        ArmatureCurrentF = 6,
        [Description("Электровоз - Тяговое напряжение [string]")]
        TractionVoltage = 7
    }
}
