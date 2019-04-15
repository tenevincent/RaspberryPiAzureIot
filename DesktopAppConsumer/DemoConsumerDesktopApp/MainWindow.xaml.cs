using DemoConsumerDesktopApp.Models;
using Microsoft.Azure.EventHubs.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoConsumerDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // TODO: Initialize the five constants below that are required 
        //       for the EventProcessorHost constructor
        const string eventHubPath = "";
        const string consumerGroupName = "";
        const string eventHubConnectionString = "";
        const string storageConnectionString = "";
        const string leaseContainerName = "";
        private EventProcessorHost _EventProcessorHost;


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;

        }

        private async void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            await _EventProcessorHost.UnregisterEventProcessorAsync();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        { 
             _EventProcessorHost = new EventProcessorHost(
                eventHubPath,
                consumerGroupName,
                eventHubConnectionString,
                storageConnectionString,
                leaseContainerName);

            await _EventProcessorHost.RegisterEventProcessorAsync<TemperatureHumidiyEventProcessor>();

            Console.WriteLine("Waiting for incoming events...");
            Console.WriteLine("Press any key to shutdown");
            Console.ReadLine();

       

        }
    }
}
