using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.Demos.RaspberryPI.SenseHAT.Model
{
    public sealed class DeviceTelemetry
    {
        public string DeviceId { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public double Temperature { get; set; }

        public double Pressure { get; set; }

        public double Humidity { get; set; }
    }
}
