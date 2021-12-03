using ConfOffre.Models;
using ConfOffre.Tools;
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

namespace ConfOffre
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal readonly DataCommunication dataManager;
        private readonly NavBar navBar;

        public MainWindow()
        {
            InitializeComponent();
            dataManager = new DataCommunication();
            navBar = new NavBar(this);
            MainGrid.Children.Add(navBar);
            navBar.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await dataManager.GetData();
            
        }
        
    }
}
