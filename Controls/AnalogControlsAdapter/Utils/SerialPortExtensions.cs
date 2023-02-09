using System.IO.Ports;
using System.Threading.Tasks;

namespace Controls.AnalogControlsAdapter.Utils
{
    public static class SerialPortExtension
    {
        public static async Task<int> ReadAsync(this SerialPort port, byte[] buffer, int offset, int count)
        {
            return await Task.Run(() =>
            {
                var readed = port.Read(buffer, offset, count);
                return readed;
            });
        }

        public static Task WriteAsync(this SerialPort port, byte[] buffer, int offset, int count)
        {
            return Task.Run(() => port.Write(buffer, offset, count));
        }
    }
}
