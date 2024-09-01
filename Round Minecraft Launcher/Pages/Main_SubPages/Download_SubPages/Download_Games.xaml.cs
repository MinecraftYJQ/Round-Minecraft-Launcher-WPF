using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Cs;
using Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion;
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

namespace Round_Minecraft_Launcher.Pages.Main_SubPages.Download_SubPages
{
    /// <summary>
    /// Download_Games.xaml 的交互逻辑
    /// </summary>
    public partial class Download_Games : System.Windows.Controls.Page
    {
        public Download_Games(string version, ContentDialog contente, bool launch=false)
        {
            InitializeComponent();
            Name.Content="正在下载 Minecraft "+version;

            Cs.Launcher.JavaEdtion.Download.Download_Game(version,p1,p2,contente, launch);
            GL.Frame.Navigate(GL.temppage);
        }
    }
}
