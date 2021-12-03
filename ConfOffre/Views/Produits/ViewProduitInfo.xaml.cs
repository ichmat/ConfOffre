using ConfOffre.Models;
using ConfOffre.Pages;
using ConfOffre.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour ViewProduitInfo.xaml
    /// </summary>
    public partial class ViewProduitInfo : UserControl
    {
        private readonly ProduitPage parent;
        private readonly DataCommunication dc;
        private readonly MProduit model;
        private readonly Dictionary<MOptAchat, VP_OptionAchat> models_view = new Dictionary<MOptAchat, VP_OptionAchat>();

        internal ViewProduitInfo(ProduitPage parent, MProduit model, DataCommunication dc)
        {
            InitializeComponent();
            this.parent = parent;
            this.model = model;
            this.dc = dc;
            ResetView();
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        
        private void ResetView()
        {
            TB_Produit_Nom.Text = model.Nom_produit;
            TB_Produit_Desc.Text = model.Desc_produit;
            TB_Produit_Prix.Text = model.Prix_produit.ToString(System.Globalization.CultureInfo.InvariantCulture);
            
            for(int i = 0; i < SP_Infos.Children.Count; ++i)
            {
                if (SP_Infos.Children[i] is VP_OptionAchat)
                {
                    SP_Infos.Children.RemoveAt(i);
                    --i;
                }
            }
            models_view.Clear();

            MOptAchat[] mOptionsAchat = dc.GetOptAchatsByIdProduit(model.Id_produit);
            foreach(MOptAchat mOptAchat in mOptionsAchat)
            {
                VP_OptionAchat view = new VP_OptionAchat(this, dc, mOptAchat);
                models_view.Add(mOptAchat, view);
                SP_Infos.Children.Add(view);
            }
        }

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TB_Produit_Prix_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private async void ButtonModifier_Click(object sender, RoutedEventArgs e)
        {
            await dc.UpdateObj(model, TB_Produit_Nom.Text, TB_Produit_Desc.Text, TB_Produit_Prix.Text);
            parent.HideProduit();
            parent.ResetView();
        }

        private async void ButtonSuppr_Click(object sender, RoutedEventArgs e)
        {
            await dc.DeleteObj(model);
            parent.HideProduit();
            parent.ResetView();
        }

        private void ButtonRetour_Click(object sender, RoutedEventArgs e)
        {
            parent.HideProduit();
        }
    }
}
