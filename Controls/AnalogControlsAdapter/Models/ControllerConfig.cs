using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Controls.AnalogControlsAdapter.Models
{
    public class ControllerConfig
    {
        /// Использовуется ли SAUT в контроллере (3 цифровых дисплея + 3 led) 
        public bool UseSaut = false;

        /// Использовуется текстовый дисплей в контроллере 
        public bool UseDisplay = false;

        /// Количество символов на текстовом дисплее 
        public int DisplayCapacity = 20;

        /// Пока не используется по умолчанию активны индикаторы в контроллере с САУТ
        public readonly BitArray UseIndicators = new(3);

        /// Не понятные значения, скорее всего максимальная длина экранов САУТа
        public readonly int[,] Something = new int[3, 2];

        /// Имена стрелок и их количество: сколько имен - столько стрелок 
        public readonly List<Arrow> Arrows = new();

        /// Пины их значения и вид, влияет на ADC и количество передаваемых данных
        public readonly List<List<Pin>> Ports = new() { new List<Pin>(), new List<Pin>(), new List<Pin>() };
    }

    public static class ControllerConfigExtensions
    {
        public static IEnumerable<(Pin port, int controllerIdx, int portIdx, int pinIdx)> TraversePins(
            this IEnumerable<ControllerConfig> configs, bool filterUnknownDevices = false)
        {
            return configs.SelectMany((controller, controllerIdx) =>
            {
                return controller.Ports.SelectMany((port, portIdx) =>
                {
                    return port
                        .Select((pin, pinIdx) => (pin, controllerIdx, portIdx, pinIdx))
                        .Where(x => !filterUnknownDevices || x.pin.ControlType != null);
                });
            });
        }
    }
}
