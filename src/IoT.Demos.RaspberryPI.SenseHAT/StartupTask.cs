using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;
using Windows.UI;
using System.Threading;
using System.Runtime.InteropServices;
using IoT.Demos.RaspberryPI.SenseHAT.Model;
using Windows.Storage;
using System.IO;
using System.Net;
using System.IO.IsolatedStorage;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Azure.Devices.Client;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace IoT.Demos.RaspberryPI.SenseHAT
{
    public sealed class StartupTask : IBackgroundTask
    {
        private static readonly Random rnd = new Random();
        private static readonly String deviceId = "pi";

        BackgroundTaskDeferral _deferral;
        private ThreadPoolTimer _timer;
        ISenseHat _senseHat;
        ISenseHatDisplay display;

        IoTHubConnection _conn = new IoTHubConnection();
        Color _color = Colors.Red;
        TinyFont _tinyFont = new TinyFont();


        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            try
            {
                _senseHat = SenseHatFactory.GetSenseHat().Result;

                

                _senseHat.Display.Clear();
                _senseHat.Display.Fill(_color);
                _senseHat.Display.Update();
                //_senseHat = SenseHatFactory.GetSenseHat().Result;


                //_senseHat.Display.Clear();
                //_senseHat.Display.Fill(Colors.GreenYellow);
                //_senseHat.Display.Update();
                _deferral = taskInstance.GetDeferral();
                this._timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_TickAsync, TimeSpan.FromSeconds(3));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw;
            }
        }


        private async void Timer_TickAsync(ThreadPoolTimer timer)
        {
            // Update and Initialize all Sensors
            UpdateAllSensors();

            if (_senseHat.Sensors.Temperature.HasValue)
            {
                // Read and Send Telemetry
                var telemetry = GetDeviceTelemetry();
                await _conn.SendEvent(JsonConvert.SerializeObject(telemetry));
                Debug.WriteLine($"Message send to IoTHub:  {JsonConvert.SerializeObject(telemetry)}");

                var result = await _conn.ReceiveEvent();

                if (!String.IsNullOrEmpty(result))
                {
                    Debug.WriteLine("Message received from IoTHub: " + result);
                    Random r = new Random();
                    _color = Color.FromArgb((byte)r.Next(), (byte)r.Next(), (byte)r.Next(), (byte)r.Next());
                }

                _senseHat.Display.Clear();
                _tinyFont.Write(_senseHat.Display, telemetry.Temperature.ToString().Substring(0, 2), _color);
                _senseHat.Display.Update();

            }
            else
            {
                Debug.WriteLine("Unable to Initialize SenseHat!");
            }
        }


           private void UpdateAllSensors()
        {
            _senseHat.Sensors.HumiditySensor.Update();
            _senseHat.Sensors.PressureSensor.Update();
            _senseHat.Sensors.ImuSensor.Update();
        }

        private DeviceTelemetry GetDeviceTelemetry()
        {
            var temp = _senseHat.Sensors.Temperature.Value;
            var hum = _senseHat.Sensors.Humidity.Value;
            var press = _senseHat.Sensors.Pressure.Value;

            var telemetry = new DeviceTelemetry()
            {
                DeviceId = deviceId,
                TimeStamp = DateTime.UtcNow,
                Temperature = temp,
                Humidity = hum,
                Pressure = press
            };

            return telemetry;
        }




    }
}
