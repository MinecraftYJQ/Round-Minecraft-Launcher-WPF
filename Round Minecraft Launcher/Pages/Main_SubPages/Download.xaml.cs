using Round_Minecraft_Launcher.Pages.Main_SubPages.Download_SubPages;
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

namespace Round_Minecraft_Launcher.Pages.Main_SubPages
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : Page
    {
        public Download()
        {
            InitializeComponent();
            Setting_Tab.SelectedItem = Java;
        }

        Download_Java Download_Java = new Download_Java();
        Download_Bedrock Download_Bedrock = null;
        private void NavigationView_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (Setting_Tab.SelectedItem == Java) {
                Setting_Frame.Navigate(Download_Java);
            }
            else
            {
                if (Download_Bedrock == null) {
                    Download_Bedrock = new Download_Bedrock();
                }
                Setting_Frame.Navigate(Download_Bedrock);
            }
        }
    }
}
