using System;
using System.Windows;
using Inventory.Application;

namespace Inventory.WPF
{
    public partial class App : System.Windows.Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Error inesperado:\n{e.Exception.Message}",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);

            e.Handled = true;
        }
    }
}