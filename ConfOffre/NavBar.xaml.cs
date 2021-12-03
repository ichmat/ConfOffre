using ConfOffre.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConfOffre
{
    /// <summary>
    /// Logique d'interaction pour NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        internal const double INIT_WIDTH = 50;
        internal const double OPEN_WIDTH = 200;
        internal NavBarItem selectednav;
        private readonly MainWindow parent;

        public NavBar(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            LoadNavItems();
        }

        private void LoadNavItems()
        {
            NavBarItem nav1 = new NavBarItem(this, PAGE_NAV.MENU, "Menu");
            NavBarItem nav2 = new NavBarItem(this, PAGE_NAV.PRODUCT, "Produits");
            NavBarItem nav3 = new NavBarItem(this, PAGE_NAV.CATEGORY, "Catégorie");
            NavBarItem nav4 = new NavBarItem(this, PAGE_NAV.REDUCTION, "Réductions");
            SP_Items.Children.Add(nav1);
            SP_Items.Children.Add(nav2);
            SP_Items.Children.Add(nav3);
            SP_Items.Children.Add(nav4);
            nav1.Selected();
        }

        #region EVENT

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Storyboard)this.Resources["MouseEnterEffect"]).Begin();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Storyboard)this.Resources["MouseLeaveEffect"]).Begin();
        }

        internal void SelectedNav(NavBarItem navBarItem)
        {
            if (selectednav != null) selectednav.Unselected();

            selectednav = navBarItem;
            OpenPage(selectednav.nav);
        }

        private void OpenPage(PAGE_NAV nav)
        {
            switch (nav)
            {
                case PAGE_NAV.MENU:
                    parent.BorderPage.Child = new MenuPage(parent);
                    return;
                case PAGE_NAV.PRODUCT:
                    parent.BorderPage.Child = new ProduitPage(parent);
                    return;
                case PAGE_NAV.CATEGORY:
                    parent.BorderPage.Child = new CategoriePage(parent);
                    return;
                case PAGE_NAV.REDUCTION:
                    parent.BorderPage.Child = new ReductionPage(parent);
                    return;
            }
        }

        #endregion
    }

    public enum PAGE_NAV
    {
        MENU = 0,
        PRODUCT = 1,
        CATEGORY = 2,
        REDUCTION = 3,
    }
}
