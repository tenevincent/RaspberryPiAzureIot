using System;
using System.Collections.Generic;
using System.Text;

namespace DemoRaspberryPiConsumerMobileApp.Models
{

    public class BarometerSensor
    {
        public DateTime DateTime { get; set; }

        public float Temperature { get; set; }

        public float Humidity { get; set; }

        public float Pressure { get; set; }


    }
}
