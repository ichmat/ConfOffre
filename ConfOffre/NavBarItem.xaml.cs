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
    /// Logique d'interaction pour NavBarItem.xaml
    /// </summary>
    public partial class NavBarItem : UserControl
    {
        private readonly NavBar parent;
        internal readonly PAGE_NAV nav;
        private bool is_selected = false;

        private readonly BitmapImage[] source = new BitmapImage[] {
            new BitmapImage(new Uri(@"/Images/menu.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/cart.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/category.png", UriKind.Relative)),
            new BitmapImage(new Uri(@"/Images/sale.png", UriKind.Relative))
        };

        public NavBarItem(NavBar parent, PAGE_NAV nav, string title, string desc = "")
        {
            InitializeComponent();
            this.parent = parent;
            this.DataContext = this;
            this.nav = nav;
            this.TB_Title.Text = title;
            icon.Source = source[(int)nav];
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Selected();
        }

        internal void Selected()
        {
            if (!is_selected)
            {
                this.is_selected = true;
                MainBorder.Background = Brushes.LightGray;
                parent.SelectedNav(this);
            }
        }

        internal void Unselected()
        {
            var converter = new BrushConverter();
            Brush brush = converter.ConvertFromString("#EEE") as Brush;
            MainBorder.Background = brush;
            this.is_selected = false;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!is_selected)
            {
                MainBorder.Background = Brushes.LightGray;
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!is_selected)
            {
                var converter = new BrushConverter();
                Brush brush = converter.ConvertFromString("#EEE") as Brush;
                MainBorder.Background = brush;
            }
        }
    }
}
