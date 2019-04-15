using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DemoRaspberryPiConsumerMobileApp.Models
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
    
