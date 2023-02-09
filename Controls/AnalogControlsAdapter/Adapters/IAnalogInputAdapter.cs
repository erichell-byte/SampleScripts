using System;
using System.Threading.Tasks;

namespace Controls.AnalogControlsAdapter.Adapters
{
    public interface IAnalogInputAdapter
    {
        public IObservable<byte[]> AnalogControllerData { get; }

        public Task SendCommand(byte[] command);

        public Task<bool> Start();

        public void Stop();
    }
}
