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

namespace ConfOffre.Views.Produits
{
    /// <summary>
    /// Logique d'interaction pour VP_OptionAchat.xaml
    /// </summary>
    public partial class VP_OptionAchat : UserControl
    {
        private readonly ViewProduitInfo parent;
        private readonly MOptAchat model;
        private readonly DataCommunication dc;
        private readonly Dictionary<MConfiguration, Vp_Configuration> models_view = new Dictionary<MConfiguration, Vp_Configuration>();

        internal VP_OptionAchat(ViewProduitInfo parent, DataCommunication dc, MOptAchat model)
        {
            InitializeComponent();
            this.parent = parent;
            this.dc = dc;
            this.model = model;
            ResetView();
        }

        private void ResetView()
        {
            TB_OptionAchat_Nom.Text = model.Nom_optAch;
            TB_OptionAchat_Desc.Text = model.Desc_optAch;

        }
    }
}
