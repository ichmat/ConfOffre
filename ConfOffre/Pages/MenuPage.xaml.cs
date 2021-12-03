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

namespace ConfOffre.Pages
{
    /// <summary>
    /// Logique d'interaction pour MenuPage.xaml
    /// </summary>
    public partial class MenuPage : UserControl
    {
        private readonly MainWindow parent;

        public MenuPage(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
    }
}
