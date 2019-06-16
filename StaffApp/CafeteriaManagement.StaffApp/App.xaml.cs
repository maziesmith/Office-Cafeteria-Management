using System;
using System.IO;
using System.Threading;
using System.Windows;
using Cafeteria.CoreLibs.Services;
using CafeteriaManagement.StaffApp.ViewModel;

namespace CafeteriaManagement.StaffApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var view = new MainWindow();
            var viewModel = new MainPageViewModel();
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
