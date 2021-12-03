using ConfOffre.Models;
using ConfOffre.Pages;
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

namespace ConfOffre.Views.Produits
{
    /// <summary>
    /// Logique d'interaction pour ViewProduit.xaml
    /// </summary>
    public partial class ViewProduit : UserControl
    {
        private readonly ProduitPage parent;
        private readonly DataCommunication dc;
        private readonly MProduit model;

        internal ViewProduit(ProduitPage parent, DataCommunication dc, MProduit model)
        {
            InitializeComponent();
            this.parent = parent;
            this.dc = dc;
            this.model = model;

            TB_Produit_Nom.Text = model.Nom_produit;
            TB_Produit_Desc.Text = model.Desc_produit;
            TB_Produit_Prix.Text = model.Prix_produit.ToString() + " €";
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parent.ShowProduit(model);
        }
    }
}
