using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Windows.Foundation;

namespace IoT.Demos.RaspberryPI.SenseHAT
{
    public sealed class IoTHubConnection : IDisposable
    {
        private DeviceClient _deviceClient { get; set; }

        public IoTHubConnection()
        {
            _deviceClient = DeviceClient.Create("iot-gc-demo-hub.azure-devices.net", new DeviceAuthenticationWithRegistrySymmetricKey("pi", "7zthtJqeFAJJh+n8GzjQegKX9L2AA+XbYer4RLXzE/g="), TransportType.Mqtt);
            _deviceClient.SetMethodHandlerAsync("InvokeMethod", InvokeMethod, null).Wait();
        }
        
        private async Task<MethodResponse> InvokeMethod(MethodRequest methodRequest, object userContext)
        {
            string result = "'Input was written to log.'";
            return new MethodResponse(Encoding.UTF8.GetBytes(result), 200);
        }

        private async Task<bool> SendEventAsync(string payload)
        {
            try
            {
                await _deviceClient.SendEventAsync(new Message(Encoding.ASCII.GetBytes(payload)));
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return false;
            }

        }

        public IAsyncOperation<bool> SendEvent(string payload)
        {
            return this.SendEventAsync(payload).AsAsyncOperation();
        }


        public IAsyncOperation<string> ReceiveEvent()
        {
            return this.ReceiveEventAsync().AsAsyncOperation();
        }


        private async Task<string> ReceiveEventAsync()
        {
            try
            {
                var receivedMessage = await _deviceClient.ReceiveAsync(TimeSpan.FromSeconds(1));

                if (receivedMessage != null)
                {
                    var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    await _deviceClient.CompleteAsync(receivedMessage);
                    return messageData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }



        public void Dispose()
        {
            _deviceClient.Dispose();
        }
    }
}
