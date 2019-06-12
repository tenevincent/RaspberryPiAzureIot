using DemoConsumerDesktopApp.Models;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsumerDesktopApp.ViewModels
{
   public class MainWindowViewModel  : INotifyPropertyChanged
    {
        // TODO: Initialize the five constants below that are required 
        //       for the EventProcessorHost constructor
        const string eventHubPath = "raspberrypiazureiothub";
        const string consumerGroupName = "$Default";

        // HostName=RaspberryPiAzureIotHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=1bfHi31Y0DGwmBdxpaYYrrCOJSBqVp/QsUMk6OTDOC0=
        // Endpoint=sb://iothub-ns-raspberryp-1511117-b047ee0577.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=1bfHi31Y0DGwmBdxpaYYrrCOJSBqVp/QsUMk6OTDOC0=;EntityPath=raspberrypiazureiothub
        const string eventHubConnectionString = "Endpoint=sb://iothub-ns-raspberryp-1511117-b047ee0577.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=1bfHi31Y0DGwmBdxpaYYrrCOJSBqVp/QsUMk6OTDOC0=;EntityPath=raspberrypiazureiothub";
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=vteneraspberrypistorage;AccountKey=14Z64Gkce9Emfh2srIbDmhOdCoZv0yGRhJdjO4zHzrn9mw7onmJH0yAgqup71BJLzDqy24cNDYBHAOZhVL8i1w==;EndpointSuffix=core.windows.net";
        const string leaseContainerName = "processcheckpoint";
        private EventProcessorHost _EventProcessorHost;


        public async Task RegisterEventProcessorHost()
        {
            EventProcessorHost = new EventProcessorHost(
                eventHubPath,consumerGroupName,
                eventHubConnectionString,
                storageConnectionString,
                leaseContainerName);

            await _EventProcessorHost.RegisterEventProcessorAsync<TemperatureHumidiyEventProcessor>();
        }

        public async Task UnRegisterEventProcessorHost()
        {
            await EventProcessorHost.UnregisterEventProcessorAsync();
        }


        public SensorDaten SensorDaten { get; set; } = new SensorDaten();
        public EventProcessorHost EventProcessorHost { get => _EventProcessorHost; private set => _EventProcessorHost = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
