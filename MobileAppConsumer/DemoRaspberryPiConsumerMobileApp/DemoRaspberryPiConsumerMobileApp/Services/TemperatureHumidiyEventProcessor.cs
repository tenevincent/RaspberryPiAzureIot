﻿using DemoRaspberryPiConsumerMobileApp.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DemoRaspberryPiConsumerMobileApp.Services
{

    public class TemperatureHumidiyEventProcessor : IEventProcessor
    {
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
                    var sensorDaten =  JsonConvert.DeserializeObject<SensorDaten>(dataAsJson);
                    Console.WriteLine($"{sensorDaten} | PartitionId: {context.PartitionId}" +
                      $" | Offset: {eventData.SystemProperties.Offset}");
                }
            }
            // This stores the current offset in the Azure Blob Storage
            return context.CheckpointAsync();
        }
    }
}
