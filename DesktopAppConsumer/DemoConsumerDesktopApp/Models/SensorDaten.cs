using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsumerDesktopApp.Models
{
   public class SensorDaten
    {

        public ObservableCollection<BarometerSensor> SensorData
        {
            get;
            set;
        } = new ObservableCollection<BarometerSensor>();
    }
}
