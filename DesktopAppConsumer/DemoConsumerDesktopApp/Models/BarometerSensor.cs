using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsumerDesktopApp.Models
{
   public class BarometerSensor
    {
      public  DateTime DateTime { get; set; }

        public float Temperature { get; set; }

        public float Humidity { get; set; }

        public float Pressure { get; set; }


    }
}
