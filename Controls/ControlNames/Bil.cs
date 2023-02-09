using System.ComponentModel;

namespace Controls.ControlNames
{
    /// БИЛ
    public enum Bil
    {
        [Description("Лампочка 'АЛС Зеленый 4'")]
        IndicatorGreen4 = 0,

        [Description("Лампочка 'АЛС Зеленый 3'")]
        IndicatorGreen3 = 1,

        [Description("Лампочка 'АЛС Зеленый 2'")]
        IndicatorGreen2 = 2,

        [Description("Лампочка 'АЛС Зеленый 1'")]
        IndicatorGreen1 = 3,

        [Description("Лампочка 'АЛС Желтый'")]
        IndicatorYellow = 4,

        [Description("Лампочка 'АЛС Жёлто-красный'")]
        IndicatorYellowRed = 5,

        [Description("Лампочка 'АЛС Красный'")]
        IndicatorRed = 6,

        [Description("Лампочка 'АЛС Бело лунный'")]
        IndicatorWhite = 7,
    }
}
