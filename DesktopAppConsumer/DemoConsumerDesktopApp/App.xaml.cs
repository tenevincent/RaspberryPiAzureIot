using DemoConsumerDesktopApp.Models;
using DemoConsumerDesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DemoConsumerDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel m_ViewModel;

        public MainWindowViewModel ViewModel
        {
            get
            {
                if(null == m_ViewModel) {
                    m_ViewModel = new MainWindowViewModel();

                    AppDataContext.ViewModel = m_ViewModel;
                }
                return m_ViewModel;
            }
        }
    }
}
