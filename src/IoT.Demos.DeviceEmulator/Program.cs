using Microsoft.Azure.Devices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.Demos.DeviceEmulator
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "<insert iotHubURI>";
        static string deviceKey = "<insert device key>";
        static int _tempHigh = 25;
        static int _tempLow = 20;
        static int _humHigh = 55;
        static int _humLow = 50;

        static void Main(string[] args)
        {
            Console.WriteLine("Simulated Device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("DemoDevice", deviceKey), TransportType.Mqtt);

            while (true)
            {
                var resultString = SimulateDevice("DemoDevice");
                SendDeviceToCloudMessagesAsync(resultString);

                // I should probably make this configurable
                Thread.Sleep(new TimeSpan(0, 0, 2));
            }
        }

        private static string SimulateDevice(string deviceName)
        {
            var temp = GetRandomMetric(_tempLow, _tempHigh).ToString().Replace(",", ".");
            var humi = GetRandomMetric(_humLow, _humHigh).ToString().Replace(",", ".");
            var unit = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ");
            var hour = DateTime.Now.Hour.ToString();
            var min = DateTime.Now.Minute.ToString();
            var secont = DateTime.Now.Second.ToString();
            var day = DateTime.Now.Day.ToString();
            var month = DateTime.Now.Month.ToString();

            // When connected to the Internet remove the Resources File and use JSON Convert
            return string.Format(Resources.telemetrysample, deviceName, unit, temp, humi, month, day, hour, min, secont, "{", "}");
        }


        private static double GetRandomMetric(int low, int high)
        {
            Random rnd = new Random();
            double n = (double)rnd.Next(low, high);
            n = n + rnd.NextDouble();
            return n;
        }

        private static async void SendDeviceToCloudMessagesAsync(string telemetryMessage)
        {
            var messageString = telemetryMessage;
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            await deviceClient.SendEventAsync(message);
            Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

            await Task.Delay(1000);
        }
    }
}
