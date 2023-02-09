namespace Controls.AnalogControlsAdapter.Utils
{
    public static class NumberUtils
    {
        public static int GetNumLength(int number)
        {
            int counter = 1;
            while (number > 9)
            {
                counter++;
                number /= 10;
            }

            return counter;
        }

        /// Конвертирует цифры числа в последовательность байт с дополнением до указанной длины
        public static byte[] NumberToByteSequence(int? number, int targetLength = -1, byte paddingValue = 0)
        {
            var numLen = number.HasValue ? GetNumLength(number.Value) : 0;
            if (targetLength < 0)
                targetLength = numLen;

            var byteArray = new byte[targetLength];

            for (var digitIndex = targetLength - 1; digitIndex >= 0; digitIndex--)
            {
                if (digitIndex < targetLength - numLen)
                {
                    byteArray[digitIndex] = paddingValue;
                    continue;
                }

                byteArray[digitIndex] = (byte)(number!.Value % 10);
                number /= 10;
            }

            return byteArray;
        }
    }
}
