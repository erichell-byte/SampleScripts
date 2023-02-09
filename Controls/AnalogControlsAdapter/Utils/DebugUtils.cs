using System.Linq;

namespace Controls.AnalogControlsAdapter.Utils
{
    public static class DebugUtils
    {
        public static string GetHexString(byte[] bytes)
        {
            return string.Join(',', bytes.Select(n => $"0x{n:X}"));
        }
    }
}
