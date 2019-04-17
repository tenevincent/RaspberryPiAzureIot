using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsumerDesktopApp.Models
{
   public class BarometerSensor
    {
        public string deviceId { get; set; }
      public  DateTime datetime { get; set; }

        public float temperature { get; set; }

        public float humidity { get; set; }

        public float pressure { get; set; }


    }
}
