#nullable enable
using System;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using Controls.AnalogControlsAdapter.Models;
using Controls.AnalogControlsAdapter.Protocol;
using Controls.AnalogControlsAdapter.Utils;

namespace Controls.AnalogControlsAdapter.Adapters
{
    public class AnalogInputComAdapter : IAnalogInputAdapter, IDisposable
    {
        private static AnalogInputComAdapter? _instance;

        public static AnalogInputComAdapter GetInstance(string port, ControllerConfig[] configs) =>
            _instance ??= new AnalogInputComAdapter(port, configs);

        public IObservable<byte[]> AnalogControllerData => _analogControllerRawData.AsObservable();

        private readonly ControllerConfig[] _configs;
        private readonly SerialPort _port;
        private readonly byte[] _buffer = new byte[1024];
        private readonly Subject<byte[]> _analogControllerRawData = new();
        private CancellationTokenSource? _cts;

        private AnalogInputComAdapter(string port, ControllerConfig[] configs)
        {
            _configs = configs;
            _port = new SerialPort(port, 115200);
            _port.ReadTimeout = 100;
            _port.WriteTimeout = 100;
            if (_port.IsOpen)
                throw new Exception("Port is already opened");
            _port.Open();
        }

        public async Task SendCommand(byte[] command)
        {
            await _port.BaseStream.WriteAsync(command, 0, command.Length);
        }

        private void Update(byte[] data)
        {
            if (ResponseParser.IsErrorMessage(data) || data[1] != 0x03)
                return;

            _analogControllerRawData.OnNext(data);
        }

        /// <summary>
        /// Запускает обновление данных
        /// </summary>
        /// <returns>успешно ли запустился обмен</returns>
        public async Task<bool> Start()
        {
            _cts = new CancellationTokenSource();

            await Configure();

            _ = Task.Factory.StartNew(() =>
            {
                while (_cts.Token.IsCancellationRequested == false)
                {
                    Task.Delay(25).Wait();
                    var response = SendCommandWithResponse(Commands.ReadValues).Result;
                    if (response != null)
                        Update(response);
                }
            }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            return true;
        }

        private async Task Configure()
        {
            var command = Commands.Configure(_configs);
            await _port.WriteAsync(command, 0, command.Length);
        }

        private async Task<byte[]?> SendCommandWithResponse(byte[] command)
        {
            await SendCommand(command);
            var totalLen = 0;
            try
            {
                int needToRead = 256;
                while (totalLen < needToRead)
                {
                    var len = await _port.BaseStream.ReadAsync(_buffer, totalLen, 256);
                    totalLen += len;
                    if (totalLen > 2 && _buffer[1] == 0x3)
                        needToRead = _buffer[2] + 3;
                }
            }
            catch (Exception e)
            {
                if (e is not TimeoutException)
                {
                    throw;
                }
            }

            return totalLen > 0 ? _buffer[..totalLen] : null;
        }

        public void Stop()
        {
            _cts?.Cancel();
        }

        public void Dispose()
        {
            _port.Dispose();
            _analogControllerRawData?.Dispose();
            _cts?.Dispose();
        }
    }
}
