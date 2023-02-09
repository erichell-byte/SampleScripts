using System.Collections;

namespace Controls.AnalogControlsAdapter.Utils
{
    public static class BitArrayExtensions
    {
        public static BitArray Reverse(this BitArray source)
        {
            var reversed = new BitArray(source.Length);

            for (var i = 0; i < source.Length; i++)
            {
                reversed[reversed.Length - i - 1] = source[i];
            }

            return reversed;
        }
    }
}
