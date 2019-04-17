using DemoConsumerDesktopApp.Models;
using DemoConsumerDesktopApp.ViewModels;
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


        private MainWindowViewModel viewModel;



        public MainWindow()
        {
            InitializeComponent();

            viewModel = ((App)Application.Current).ViewModel;
            this.DataContext = viewModel;


            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;

        }

        private async void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            await viewModel.UnRegisterEventProcessorHost();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           await viewModel.RegisterEventProcessorHost();

            

            dateTimeContinuousAxis.LabelFormat = "dd-MM-yyyy\nHH:mm:ss";


            Console.WriteLine("Waiting for incoming events...");
            Console.WriteLine("Press any key to shutdown");
            Console.ReadLine();

       

        }
    }
}
