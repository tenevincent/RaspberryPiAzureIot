using DemoConsumerDesktopApp.ViewModels;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace DemoConsumerDesktopApp.Models
{
    public class TemperatureHumidiyEventProcessor : IEventProcessor
    {
        private MainWindowViewModel viewModel;

        public TemperatureHumidiyEventProcessor()
        {
            this.viewModel = AppDataContext.ViewModel;
        }


        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Shutting down processor for partition {context.PartitionId}. " +
   $"Reason: {reason}");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"Initialized processor for partition {context.PartitionId}");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error for partition {context.PartitionId}: {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> eventDatas)
        {
            if (eventDatas != null)
            {
                foreach (var eventData in eventDatas)
                {
                    var dataAsJson = Encoding.UTF8.GetString(eventData.Body.Array);
                    var sensorDaten = Deserialize<BarometerSensor>(dataAsJson);
                    Console.WriteLine($"{sensorDaten} | PartitionId: {context.PartitionId}" +
                      $" | Offset: {eventData.SystemProperties.Offset}");


                    System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(
                        () =>
                        { 
                            AppDataContext.ViewModel.SensorDaten.SensorData.Add(sensorDaten);
                        }));

                }
            }
            // This stores the current offset in the Azure Blob Storage
            return context.CheckpointAsync();
        }


        public static T Deserialize<T>(string dataJson)
        {
            try
            {
                var settings = new DataContractJsonSerializerSettings
                {
                    DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-ddTHH:mm:ss")
                };
                var _Bytes = Encoding.Unicode.GetBytes(dataJson);
                using (MemoryStream _Stream = new MemoryStream(_Bytes))
                {

                    var _Serializer = new DataContractJsonSerializer(typeof(T), settings);

                    return (T)_Serializer.ReadObject(_Stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
