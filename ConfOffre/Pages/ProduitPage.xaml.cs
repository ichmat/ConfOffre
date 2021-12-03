using ConfOffre.Models;
using ConfOffre.Tools;
using ConfOffre.Views.Produits;
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
    /// Logique d'interaction pour ProduitPage.xaml
    /// </summary>
    public partial class ProduitPage : UserControl
    {
        private readonly MainWindow parent;
        private readonly Dictionary<MProduit, ViewProduit> models_view = new Dictionary<MProduit, ViewProduit>();
        private readonly Border bAdd;

        public ProduitPage(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            bAdd = new Border();
            bAdd.Width = 250;
            bAdd.Height = 150;
            bAdd.Background = Brushes.LightGray;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "ajouter";
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center; 
            bAdd.Child = textBlock;
            bAdd.Margin = new Thickness(15);
            bAdd.MouseLeftButtonDown += BAdd_MouseLeftButtonDown;
            ResetView();
        }

        private async void BAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MProduit mProduit = new MProduit(DataCommunication.Index);
            string[] parms = new string[] { "nom", "desc", "0.00" };
            await parent.dataManager.CreateObj(mProduit, parms);
            ResetView();
        }

        internal void ResetView()
        {
            DataCommunication dc = parent.dataManager;
            SP_Produits.Children.Clear();
            models_view.Clear();
            MProduit[] models = dc.GetData<MProduit>();
            foreach (MProduit model in models)
            {
                ViewProduit view = new ViewProduit(this, dc, model);
                models_view.Add(model, view);
                SP_Produits.Children.Add(view);
            }

            SP_Produits.Children.Add(bAdd);
        }

        internal void ShowProduit(MProduit model)
        {
            BorderInfo.Visibility = Visibility.Visible;
            BorderInfo.Child = new ViewProduitInfo(this, model, parent.dataManager);
        }

        internal void HideProduit()
        {
            BorderInfo.Visibility = Visibility.Collapsed;
        }
    }
}
