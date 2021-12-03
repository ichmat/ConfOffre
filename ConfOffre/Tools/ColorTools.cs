using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ConfOffre.Tools
{
    internal static class ColorTools
    {
        internal static SolidColorBrush Dark_Blue { get { return ((SolidColorBrush)App.Current.Resources["dark_blue"]).Clone(); } }
        internal static SolidColorBrush Alt_Blue { get { return ((SolidColorBrush)App.Current.Resources["alt_blue"]).Clone(); } }
        internal static SolidColorBrush Main_Blue { get { return ((SolidColorBrush)App.Current.Resources["main_blue"]).Clone(); } }
        internal static SolidColorBrush Brown { get { return ((SolidColorBrush)App.Current.Resources["brown"]).Clone(); } }
        internal static SolidColorBrush Yellow { get { return ((SolidColorBrush)App.Current.Resources["yellow"]).Clone(); } }
    }
}
