#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Controls.AnalogControlsAdapter.Enums;
using Controls.AnalogControlsAdapter.Models;
using Controls.ControlNames;

namespace Controls.AnalogControlsAdapter.Protocol
{
    public static class ConfigParser
    {
        public static ControllerConfig[] ParseConfig(string text, IEnumerable<Type> namespaces)
        {
            var reader = new StringReader(text);
            reader.ReadLine(); // Количество контроллеров

            var numberOfControllers = int.Parse(reader.ReadLine());
            var controllers = new ControllerConfig[numberOfControllers];

            for (var controllerIdx = 0; controllerIdx < numberOfControllers; controllerIdx++)
            {
                var controllerConfig = new ControllerConfig();

                // 3 порта по 8 пинов
                for (var portIdx = 0; portIdx < 3; portIdx++)
                {
                    for (var pinIdx = 0; pinIdx < 8; pinIdx++)
                    {
                        reader.ReadLine(); // пропуск строки с номером пина
                        var pinConfigStrings = reader.ReadLine().Split("///").Select(s => s.Trim()).ToArray();

                        var name = pinConfigStrings[0];
                        var controlName = ParsePinName(pinConfigStrings.ElementAtOrDefault(1) ?? "", namespaces);
                        var triggers = ParseTriggers(pinConfigStrings.ElementAtOrDefault(2) ?? "");
                        var pinType = ParsePinType(reader.ReadLine());

                        controllerConfig.Ports[portIdx].Insert(pinIdx, new Pin(name, pinType, controlName, triggers));
                    }

                    reader.ReadLine().StartsWith("Use 7sd");
                }

                // Стрелки
                var numberOfArrows = int.Parse(reader.ReadLine());
                for (var arrowIdx = 0; arrowIdx < numberOfArrows; arrowIdx++)
                {
                    var nameId = reader.ReadLine().Split("///");
                    var name = nameId[0].Trim();

                    var isKnownControl = Enum.TryParse<LocomotiveControls>(nameId.ElementAtOrDefault(1)?.Trim() ?? "",
                        out var controlId);

                    controllerConfig.Arrows.Add(new Arrow(name, isKnownControl ? controlId : null));
                }

                for (int i = 0; i < 6 - numberOfArrows; i++)
                    reader.ReadLine();

                controllerConfig.UseSaut = reader.ReadLine().StartsWith("Use SAUT");
                for (var indicatorIdx = 0; indicatorIdx < controllerConfig.UseIndicators.Length; indicatorIdx++)
                {
                    controllerConfig.UseIndicators[indicatorIdx] = reader.ReadLine().StartsWith("Use Indicator");
                    controllerConfig.Something[indicatorIdx, 0] = int.Parse(reader.ReadLine());
                    controllerConfig.Something[indicatorIdx, 1] = int.Parse(reader.ReadLine());
                }

                controllerConfig.UseDisplay = reader.ReadLine().StartsWith("Use Text Display");
                controllerConfig.DisplayCapacity = int.Parse(reader.ReadLine());

                controllers[controllerIdx] = controllerConfig;
            }

            return controllers;
        }

        private static TriggerValue[]? ParseTriggers(string stringValue)
        {
            if (stringValue == "")
                return null;


            return stringValue.Split(' ')
                .Where(v => v != "")
                .Select(stringTriggerValue =>
                {
                    var triggerValues = stringTriggerValue.Split('=').Select(float.Parse).ToArray();
                    return new TriggerValue(triggerValues[0], triggerValues[1]);
                })
                .OrderBy(tv => tv.Trigger)
                .ToArray();
        }

        private static Enum? ParsePinName(string stringValue, IEnumerable<Type> namespaces)
        {
            if (stringValue == "")
                return null;

            foreach (var namespaceType in namespaces)
            {
                if (Enum.TryParse(namespaceType, stringValue, out var controlName))
                    return (Enum)controlName;
            }

            return null;
        }

        private static PinType ParsePinType(string stringValue)
        {
            return stringValue switch
            {
                "ADC" => PinType.Adc,
                "In" => PinType.In,
                "Out" => PinType.Out,
                _ => PinType.Disabled,
            };
        }
    }
}
