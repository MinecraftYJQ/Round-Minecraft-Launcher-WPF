using Round_Minecraft_Launcher.Cs;
using Round_Minecraft_Launcher.Pages.Setting_SubPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using static Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion.Json;

namespace Round_Minecraft_Launcher.Pages.Main_SubPages
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Page
    {
        bool oks = false;
        public Setting()
        {
            InitializeComponent();
            Setting_Frame.Navigate(Game_Setting);
            Setting_Tab.SelectedItem = GameSetting;
        }

        public Game_Setting Game_Setting = new Game_Setting();
        public User_Setting User_Setting = new User_Setting();
        public Personalize_Setting personalize_Setting = new Personalize_Setting();
        private void NavigationView_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (Setting_Tab.SelectedItem == GameSetting)
            {
                Setting_Frame.Navigate(Game_Setting);
            }
            else if (Setting_Tab.SelectedItem == UserSetting)
            {
                Setting_Frame.Navigate(User_Setting);
            }
            else if (Setting_Tab.SelectedItem == PersonalizeSetting) {
                Setting_Frame.Navigate(personalize_Setting);
            }
        }
    }
}
