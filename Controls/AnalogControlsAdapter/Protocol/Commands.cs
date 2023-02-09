using System;
using System.Collections;
using System.Linq;
using Controls.AnalogControlsAdapter.Models;
using Controls.AnalogControlsAdapter.Utils;
using Extensions;

namespace Controls.AnalogControlsAdapter.Protocol
{
    /// <summary>
    /// Протокол содержит в себе 4 основные команды
    /// </summary>
    public class Commands
    {
        private const byte StartByte = 0x73;
        private const byte EndByte = 0x65;

        public static byte[] Configure(ControllerConfig[] configs)
        {
            var message = new byte[5 + configs.Length * 7];

            message[0] = StartByte;
            message[1] = 0x02;
            message[2] = 0xE4;
            message[3] = (byte)configs.Length;

            var currentIndex = 4;
            foreach (var controllerConfig in configs)
            {
                foreach (var pins in controllerConfig.Ports)
                {
                    var pinTypes = pins.Select(pin => (int)pin.PinType).ToArray();
                    message[currentIndex++] =
                        (byte)(pinTypes[0] << 6 | pinTypes[1] << 4 | pinTypes[2] << 2 | pinTypes[3]);
                    message[currentIndex++] =
                        (byte)(pinTypes[4] << 6 | pinTypes[5] << 4 | pinTypes[6] << 2 | pinTypes[7]);
                }

                var arrowNamesUartLenght = controllerConfig.Arrows.Count > 0
                    ? 1 + controllerConfig.Arrows.Count * 3
                    : 0;

                var indicatorsUartLength = controllerConfig.UseSaut ? 24 : 0; // 3 displays (22) + 3 diods (2)

                message[currentIndex++] = (byte)(arrowNamesUartLenght + indicatorsUartLength);
            }

            message[^1] = EndByte;
            return message;
        }

        public static byte[] ReadValues => new byte[] { 0x73, 0x13, 0x01, 0x65 };

        /// <summary>
        /// Создает команду для записи значений пинов (лампочек)
        /// </summary>
        /// <param name="controllersPortsPins">Значения пинов на каждом порту каждого контроллера</param>
        public static byte[] WritePins(BitArray[][] controllersPortsPins)
        {
            var headerLen = 3;
            var portsLen = 3;
            var dataLen = (byte)(controllersPortsPins.Length * portsLen);
            var message = new byte[headerLen + dataLen + 1];

            message[0] = StartByte;
            message[1] = (byte)CommandCode.WritePins;
            message[2] = dataLen;

            for (int controllerIdx = 0; controllerIdx < controllersPortsPins.Length; controllerIdx++)
            {
                var ports = controllersPortsPins[controllerIdx];
                for (int portIdx = 0; portIdx < ports.Length; portIdx++)
                {
                    var pins = ports[portIdx].Reverse();
                    pins.CopyTo(message, headerLen + portsLen * controllerIdx + portIdx);
                }
            }

            message[^1] = EndByte;
            return message;
        }


        /// <summary> Создает команду для записи значений стрелок, цифровых дисплеев и лампочек </summary>
        public static byte[] SetUartAnalogControls(int[][] controllerArrowValues, int?[] displayValues,
            int[] displaysCapacity,
            BitArray ledValues)
        {
            var headerLength = 3;
            var arrowValuesLength = controllerArrowValues.Sum(vs => vs.Length > 0 ? 1 + vs.Length * 3 : 0);
            var digitalDisplayValuesLength = displaysCapacity.Sum(n => n + 4);
            var diodeValuesLength = displayValues.Length > 0 ? 2 : 0;
            var payloadLength = arrowValuesLength + digitalDisplayValuesLength + diodeValuesLength;

            var message = new byte[headerLength + payloadLength + 1];

            message[0] = StartByte;
            message[1] = (byte)CommandCode.WriteOther;
            message[2] = (byte)payloadLength;

            var currentIndex = 3;

            if (controllerArrowValues.Length > 0)
            {

                foreach (var arrowValues in controllerArrowValues)
                {
                    message[currentIndex++] = (byte)BlockCode.Arrows;
                    for (int deviceIdx = 0; deviceIdx < arrowValues.Length; deviceIdx++)
                    {
                        message[currentIndex] = (byte)(deviceIdx + 1);
                        currentIndex += 1;
                        BitConverter.GetBytes(arrowValues[deviceIdx]).Reverse().ToArray()[2..]
                            .CopyTo(message, currentIndex);
                        currentIndex += 2;
                    }
                }
            }

            foreach (var (displayValue, index) in displayValues.WithIndex())
            {
                message[currentIndex++] = (byte)BlockCode.Indicators;
                message[currentIndex++] = (byte)(index + 1);
                message[currentIndex++] = (byte)displaysCapacity[index];

                var digitSequence = NumberUtils.NumberToByteSequence(displayValue, displaysCapacity[index], 0xa);
                foreach (var digit in digitSequence)
                {
                    message[currentIndex++] = digit;
                }

                message[currentIndex++] = 0xff;
            }

            if (displayValues.Length > 0)
            {
                message[currentIndex++] = (byte)BlockCode.Led;
                ledValues.CopyTo(message, currentIndex++);
            }

            message[^1] = EndByte;
            return message;
        }

        private enum CommandCode
        {
            WritePins = 0x04,
            WriteOther = 0x05
        }

        private enum BlockCode
        {
            Arrows = 0x41,
            Indicators = 0x49,
            Led = 0x4c,
            Text = 0x54
        }
    }
}
