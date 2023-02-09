using System.ComponentModel;

namespace Controls.ControlNames
{
    public enum LocomotiveControls
    {
        #region Toggle

        [Description("Управление тепловозом")]
        ToggleLocomotiveControl = 0,

        [Description("Тумблер 'Блок управления С1'")]
        ToggleControlUnitC1 = 1,

        [Description("Тумблер 'Блок управления С2'")]
        ToggleControlUnitC2 = 2,

        [Description("Тумблер 'Блок управления С3'")]
        ToggleControlUnitC3 = 3,

        [Description("Тумблер 'Блок управления С4'")]
        ToggleControlUnitC4 = 4,

        [Description("Тумблер 'Отключение секций С1'")]
        ToggleDisablingSectionsC1 = 5,

        [Description("Тумблер 'Отключение секций С2'")]
        ToggleDisablingSectionsC2 = 6,

        [Description("Тумблер 'Отключение секций С3'")]
        ToggleDisablingSectionsC3 = 7,

        [Description("Тумблер 'Отключение секций С4'")]
        ToggleDisablingSectionsC4 = 8,

        [Description("Тумблер 'Буферный фонарь левый включен'")]
        ToggleBufferLampLeft = 9,

        [Description("Тумблер 'Буфер фонарь правый включен'")]
        ToggleBufferLampRight = 10,

        [Description("Тумблер 'Буфер фонарь левый цвет: Белый (true) красный (false)'")]
        ToggleBufferLampLeftWhiteRed = 11,

        [Description("Тумблер 'Буфер фонарь правый цвет: Белый (true) красный (false)'")]
        ToggleBufferLampRightWhiteRed = 12,

        [Description("Тумблер 'Задний правый буферный фонарь включен'")]
        ToggleBufferLightBackRight = 13,

        [Description("Тумблер 'Задний левый буферный фонарь включен'")]
        ToggleBufferLightBackLeft = 14,

        [Description("Тумблер 'Освещeние кабины ярко'")]
        ToggleLightingBright = 15,

        [Description("Тумблер 'Освещeние кабины тускло'")]
        ToggleLightingDim = 16,

        [Description("Тумблер 'Освещeние кабины документ'")]
        ToggleLightingDocument = 17,

        [Description("Тумблер 'Освещeние кабины зеленый'")]
        ToggleLightingGreen = 18,

        [Description("Тумблер 'регулирование температуры Ручное (true) – автоматическое (false)'")]
        ToggleAutoTempRegulation = 19,

        [Description("Тумблер 'Панели группа 1'")]
        TogglePanelsGroup1 = 20,

        [Description("Тумблер 'Панели группа 2'")]
        TogglePanelsGroup2 = 21,

        [Description("Тумблер 'Панели группа 3'")]
        TogglePanelsGroup3 = 22,

        [Description("Тумблер 'Обогреватель 1 ступень 1'")]
        ToggleHeater1Stage1 = 23,

        [Description("Тумблер 'Обогреватель 1 ступень 2'")]
        ToggleHeater1Stage2 = 24,

        [Description("Тумблер 'Обогреватель 2 ступень 1'")]
        ToggleHeater2Stage1 = 25,

        [Description("Тумблер 'Обогреватель 2 ступень 2'")]
        ToggleHeater2Stage2 = 26,

        [Description("Тумблер 'Обогрев стекол'")]
        ToggleGlassHeating = 27,

        [Description("Тумблер 'Обогрев зеркал'")]
        ToggleHeatedMirrors = 28,

        [Description("Тумблер 'Радио-станция'")]
        ToggleRadioStation = 29,

        [Description("Тумблер 'Песок экстренный'")]
        ToggleEmergencySand = 30,

        [Description("Тумблер 'Песок автоматический'")]
        ToggleAutomaticSand = 31,

        [Description("Тумблер 'Стеклоочиститель'")]
        ToggleWiper = 32,

        [Description("Тумблер 'Стеклоочеститель Непрерывно (true) Прерывно (false)'")]
        ToggleInfiniteWiper = 33,

        [Description("Тумблер 'Штора вверх'")]
        ToggleShadeUp = 34,

        [Description("Тумблер 'Штора вниз'")]
        ToggleShadeDown = 35,

        [Description("Тумблер 'Нагрев масла'")]
        ToggleOilHeating = 36,

        [Description("Тумблер 'Кондиционер'")]
        ToggleConditioner = 37,

        [Description("Тумблер 'Управление холодильником автоматическое")]
        RefrigeratorControl = 38,

        [Description("Тумблер 'Освещение кабины'")]
        CockpitLighting = 39,

        [Description("Тумблер 'ЭПК переключатель'")]
        ToggleEpc = 40,

        [Description("Тумблер 'Освещение документов выключатель'")]
        ToggleDocumentCoverage = 41,

        [Description("Тумблер 'Освещение приборов'")]
        ToggleLightingOfDevices = 42,

        [Description("Тумблер 'Освещение тележек'")]
        ToggleTrolleyLighting = 43,

        [Description("Тумблер 'Автоматическое (true) - Ручное (false) регулирование тяги'")]
        ToggleAutomaticTractionControl = 44,

        [Description("Тумблер 'Вспомогательные машины'")]
        ToggleAuxiliaryMachines = 45,

        [Description("Тумблер 'Сигнализация'")]
        ToggleAlarmSystem = 46,

        [Description("Тумблер 'Сигнализация секций С1'")]
        ToggleSignalisationSectionsC1 = 47,

        [Description("Тумблер 'Сигнализация секций С2'")]
        ToggleSignalisationSectionsC2 = 48,

        [Description("Тумблер 'Сигнализация секций С3'")]
        ToggleSignalisationSectionsC3 = 49,

        [Description("Тумблер 'Сигнализация секций С4'")]
        ToggleSignalisationSectionsC4 = 50,

        [Description("Тумблер 'МПК С1'")]
        ToggleMpcS1 = 51,

        [Description("Тумблер 'МПК С2'")]
        ToggleMpcS2 = 52,

        [Description("Тумблер 'МПК С3'")]
        ToggleMpcS3 = 53,

        [Description("Тумблер 'МПК С4'")]
        ToggleMpcS4 = 54,

        [Description("Тумблер 'Цепи управления'")]
        ToggleControlCircuits = 55,

        [Description("Тумблер 'Токоприемник задний'")]
        ToggleRearPantograph = 56,

        [Description("Тумблер 'Токоприемник передний'")]
        ToggleFrontPantograph = 57,

        [Description("Тумблер 'Главный выключатель'")]
        ToggleMain = 58,

        [Description("Тумблер 'Прожектор тускло'")]
        ToggleTheProjectorIsDim = 59,

        [Description("Тумблер 'Прожектор ярко'")]
        ToggleTheProjectorIsBright = 60,

        [Description("Тумблер 'МСУД'")]
        ToggleMsud = 61,

        [Description("Тумблер 'Компрессор'")]
        ToggleCompressor = 62,

        [Description("Тумблер 'Вентилятор 1'")]
        ToggleFan1 = 63,

        [Description("Тумблер 'Вентилятор 2'")]
        ToggleFan2 = 64,

        [Description("Тумблер 'Резерв'")]
        ToggleReserve = 65,

        [Description("Тумблер 'Т1'")]
        ToggleT1 = 66,

        [Description("Тумблер 'Т2'")]
        ToggleT2 = 67,

        [Description("Тумблер 'Т3'")]
        ToggleT3 = 68,

        [Description("Тумблер 'Т4'")]
        ToggleT4 = 69,

        [Description("Тумблер 'Питание ТСКБМ'")]
        ToggleTskbmPower = 70,

        #endregion

        #region Buttons

        [Description("Кнопка 'РБ помощника'")]
        ButtonRbAssistant = 71,

        [Description("Кнопка 'РБC помощника'")]
        ButtonRbsAssistant = 72,

        [Description("Кнопка 'Экстренное торможение'")]
        ButtonExtraBrake = 73,

        [Description("Кнопка 'Аварийный тормоз'")]
        ButtonEmergencyStop = 74,

        [Description("Кнопка 'РБ машиниста'")]
        ButtonRb = 75,

        [Description("Кнопка 'РБС машиниста'")]
        ButtonRbs = 76,

        [Description("Ключ 'ЭПК'")]
        KeyEpc = 77,

        [Description("Кнопка 'Резервуар 1'")]
        ButtonReservoir1 = 78,

        [Description("Кнопка 'Резервуар 2'")]
        ButtonReservoir2 = 79,

        [Description("Кнопка 'Резервуар 3'")]
        ButtonReservoir3 = 80,

        [Description("Кнопка 'Маслоотделитель'")]
        ButtonOilSeparator = 81,

        [Description("Кнопка 'Омыватель'")]
        ButtonWasher = 82,

        [Description("Кнопка 'Тифон'")]
        ButtonTyphon = 83,

        [Description("Кнопка 'Свисток'")]
        ButtonWhistle = 84,

        [Description("Кнопка 'Песок'")]
        ButtonSand = 85,

        [Description("Кнопка 'Компрессор'")]
        ButtonCompressor = 86,

        [Description("Кнопка 'Возврат защиты'")] // Выглядит как тумблер, но по сути кнопка
        ToggleReturnOfProtection = 87,

        [Description("Кнопка 'Возврат реле'")] // Выглядит как тумблер, но по сути кнопка
        ToggleRelayReturn = 88,

        [Description("Кнопка 'Старт дизельного двигателя 1'")]
        ButtonDieselStart1 = 89,

        [Description("Кнопка 'Остановка дизельного двигателя 1'")]
        ButtonDieselStop1 = 90,

        [Description("Кнопка 'Старт дизельного двигателя 2'")]
        ButtonDieselStart2 = 91,

        [Description("Кнопка 'Остановка дизельного двигателя 2'")]
        ButtonDieselStop2 = 92,

        [Description("Кнопка 'Старт дизельного двигателя 3'")]
        ButtonDieselStart3 = 93,

        [Description("Кнопка 'Остановка дизельного двигателя 3'")]
        ButtonDieselStop3 = 94,

        [Description("Кнопка 'Отпуск тормозов'")]
        ButtonBrakeRelease = 95,

        [Description("Кнопка 'Тифон резерв'")]
        ButtonTyphonReserve = 96,

        [Description("Кнопка 'Вызов ассистента'")]
        ButtonCallAssistant = 97,

        [Description("Кнопка 'Вперед'")]
        ButtonForward = 98,

        [Description("Кнопка 'Назад'")]
        ButtonBackward = 99,

        #endregion

        #region Joystick

        [Description("Управл зеркал левое верх [bool]")]
        MirrorControlLeftTop = 100,

        [Description("Управл зеркал левое низ [bool]")]
        MirrorControlLeftBottom = 101,

        [Description("Управл зеркал левое лево [bool]")]
        MirrorControlLeftLeft = 102,

        [Description("Управл зеркал левое право [bool]")]
        MirrorControlLeftRight = 103,

        [Description("Управл зеркал правое верх [bool]")]
        MirrorControlRightTop = 104,

        [Description("Управл зеркал правое низ [bool]")]
        MirrorControlRightBottom = 105,

        [Description("Управл зеркал правое лево [bool]")]
        MirrorControlRightLeft = 106,

        [Description("Управл зеркал правое право [bool]")]
        MirrorControlRightRight = 107,

        #endregion

        #region Arrows

        [Description("Стрелка 'якорь'")]
        ArrowAnchor = 108,

        [Description("Стрелка 'возбуждение'")]
        ArrowExcitation = 109,

        [Description("Стрелка 'сеть'")]
        ArrowElectricNetwork = 110,

        [Description("Стрелка 'тормозные цилиндры (черная)'")]
        ArrowBrakeCylindersBlack = 111,

        [Description("Стрелка 'тормозные цилиндры (красная)'")]
        ArrowBrakeCylindersRed = 112,

        [Description("Стрелка 'Питательная магистраль'")]
        ArrowFeedLine = 113,

        [Description("Стрелка 'Тормозная магистраль'")]
        ArrowBreakLine = 114,

        [Description("Стрелка 'Уравнительный резервуар'")]
        ArrowSurgeTank = 115,

        #endregion

        #region Handles

        [Description("Рукоятка Реверс [int -1, 0, 1]")]// электровоз
        HandleReverse = 116,

        [Description("Тяга [float -1 ~ 1]")] // электровоз
        HandleTraction = 117,

        [Description("Редуктор КМ 395 [float 0-1]")]
        HandleReducer395 = 118,

        [Description("Акселератор [float -1 ~ 1]")] // тепловоз
        HandleAccelerator = 119,

        [Description("Кран машиниста № 215 [int 0-5]")]
        HandleDriverCrane215State = 120,

        [Description("Кран машиниста № 395 [int 0-6]")]
        HandleDriverCrane395State = 121,

        #endregion

        // [float 0-1]

        #region Resistors

        [Description("Крутилка 'Задатчик скорости'")]
        ResistorSpeed = 122,

        [Description("Крутилка 'Освещение приборов крутилка'")]
        ResistorLightingOfDevices = 123,

        [Description("Крутилка 'Освещение манометров'")]
        ResistorManometrLightening = 124,

        [Description("Крутилка 'Освещение консоли'")]
        ResistorConsoleLightening = 125,

        [Description("Крутилка 'Светильник тускло ярко'")]
        ResistorLamp = 126,

        #endregion

        // ЭУ с состоянием [IN - int]

        #region States

        [Description("Переключатель 'Стеклоочеститель правый' [int 0-2]")]
        WiperRightState = 127,

        [Description("Переключатель 'Стеклоочеститель левый' [int 0-2]")]
        WiperLeftState = 128,

        #endregion

        // Рычаги [IN - bool]

        #region Levers

        [Description("Рычаг 'Блокировка тормозов (367) верхнее(true) нижнее(false)'")]
        LeverLock367UpperLower = 129,

        #endregion

        // Лампочки [OUT - bool]

        #region Indicators

        [Description("Индикатор 'С1'")]
        IndicatorC1 = 130,

        [Description("Индикатор 'С2'")]
        IndicatorC2 = 131,

        [Description("Индикатор 'С3'")]
        IndicatorC3 = 132,

        [Description("Индикатор 'С4'")]
        IndicatorC4 = 133,

        [Description("Индикатор 'ГВ'")]
        IndicatorGw = 134,

        [Description("Индикатор 'ТД1'")]
        IndicatorTd1 = 135,

        [Description("Индикатор 'ТД2'")]
        IndicatorTd2 = 136,

        [Description("Индикатор 'ТД3'")]
        IndicatorTd3 = 137,

        [Description("Индикатор 'ТД4'")]
        IndicatorTd4 = 138,

        [Description("Индикатор 'ВИП'")]
        IndicatorVip = 139,

        [Description("Индикатор 'ВУВ'")]
        IndicatorVuv = 140,

        [Description("Индикатор 'В1'")]
        IndicatorB1 = 141,

        [Description("Индикатор 'В2'")]
        IndicatorB2 = 142,

        [Description("Индикатор 'В3'")]
        IndicatorV3 = 143,

        [Description("Индикатор 'МК'")]
        IndicatorMk = 144,

        [Description("Индикатор 'Тр-р'")]
        IndicatorTrr = 145,

        [Description("Индикатор 'ДП'")]
        IndicatorDp = 146,

        [Description("Индикатор 'РЗ'")]
        IndicatorRz = 147,

        [Description("Индикатор 'РКЗ'")]
        IndicatorRkz = 148,

        [Description("Индикатор 'ЗБ'")]
        IndicatorZb = 149,

        [Description("Индикатор 'ТЦ'")]
        IndicatorTc = 150,

        [Description("Индикатор 'ТМ'")]
        IndicatorTm = 151,

        [Description("Индикатор 'ПС'")]
        IndicatorPs = 152,

        [Description("Индикатор 'ДМ'")]
        IndicatorDm = 153,

        [Description("Индикатор 'Обрыв тормозной магистрали'")]
        IndicatorBreakLineBroke = 154,

        #endregion

        // Переключатели автомотов

        #region Switch

        [Description("Автомат 'Токоприемники' [bool]")]
        SwitchPantographs = 155,

        [Description("Автомат 'Главный выключатель' [bool]")]
        SwitchMainPower = 156,

        [Description("Автомат 'Тяга' [bool]")]
        SwitchTraction = 157,

        [Description("Автомат 'Торможение' [bool]")]
        SwitchBraking = 158,

        [Description("Автомат вспомогательные 'машины' [bool]")]
        SwitchAuxiliaryMachines = 159,

        [Description("Автомат 'Вентиляторы 1,2' [bool]")]
        SwitchFan12 = 160,

        [Description("Автомат 'Компрессор' [bool]")]
        SwitchCompressor = 161,

        [Description("Автомат 'Обогрев стекол' [bool]")]
        SwitchGlassHeating = 162,

        [Description("Автомат 'Сигнализация' [bool]")]
        SwitchSignalisation = 163,

        [Description("Автомат 'Песок, сигналы, резервуары' [bool]")]
        SwitchSandSignalsTanks = 164,

        [Description("Автомат 'Прожектор' [bool]")]
        SwitchProjector = 165,

        [Description("Автомат 'Резерв' [bool]")]
        SwitchReserve = 166,

        [Description("Автомат 'Обогрев кабины' [bool]")]
        SwitchCabin = 167,

        [Description("Автомат 'Питание 50В' [bool]")]
        SwitchPower50V = 168,

        [Description("Автомат 'Радиосвязь нормально' [bool]")]
        SwitchRadioNormal = 169,

        [Description("Автомат 'МПСУ'")]
        SwitchMpsuPower = 170,

        [Description("Автомат 'Бытовые приборы'")]
        SwitchDevicesPower = 171,

        [Description("Возбуждение 'МПСУ'")]
        SwitchExcitation = 172,

        #endregion

        [Description("Кассетоприемник (вставлена кассета - true) [bool]")]
        CassetteReceiver = 173,

        [Description("Двойная тяга [bool]")]
        DoubleTraction = 174,

        [Description("Блокировка тяги (ключ) [bool]")]
        TractionLock = 175,
    }
}
