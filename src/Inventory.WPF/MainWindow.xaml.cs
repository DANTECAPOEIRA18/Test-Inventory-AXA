using System.Windows;
using Inventory.WPF.ViewModels;

namespace Inventory.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}