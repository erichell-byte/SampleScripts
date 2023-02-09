using System;
using System.Collections;
using System.Linq;
using Controls.AnalogControlsAdapter.Enums;
using Controls.AnalogControlsAdapter.Models;
using Controls.AnalogControlsAdapter.Utils;
using Extensions;

namespace Controls.AnalogControlsAdapter.Protocol
{
    public static class ResponseParser
    {
        public static bool IsErrorMessage(byte[] rawBytes)
        {
            return rawBytes.Length < 2 || rawBytes[1] == 0x66;
        }

        public static AnalogControllerData[] ParseAnalogControllerData(byte[] rawBytes,
            ControllerConfig[] controllerConfigs)
        {
            var payloadLength = rawBytes[2];
            var payload = rawBytes[3..(3 + payloadLength)];
            var currentIndex = 0;

            var dataArray = new AnalogControllerData[controllerConfigs.Length];
            foreach (var (controllerConfig, controllerIdx) in controllerConfigs.WithIndex())
            {
                var controllerData = new AnalogControllerData
                {
                    ADC = new ushort[8],
                    PortPins = new BitArray[3]
                };

                var adcIndex = 0;
                foreach (var _ in controllerConfig.Ports[0].Where(pin => pin.PinType == PinType.Adc))
                {
                    controllerData.ADC[adcIndex] = ParseAdc(payload, currentIndex);
                    currentIndex += 2;
                    adcIndex++;
                }

                for (int portIndex = 0; portIndex < 3; portIndex++)
                {
                    var index = currentIndex++;
                    controllerData.PortPins[portIndex] = new BitArray(payload[index..(index + 1)]).Reverse();
                }

                dataArray[controllerIdx] = controllerData;
            }

            return dataArray;
        }

        private static ushort ParseAdc(byte[] buffer, int index)
        {
            return BitConverter.ToUInt16(buffer[index..(index + 2)].Reverse().ToArray());
        }
    }
}
