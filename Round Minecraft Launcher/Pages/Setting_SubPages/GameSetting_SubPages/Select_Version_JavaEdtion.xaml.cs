using Round_Minecraft_Launcher.Control.Version;
using Round_Minecraft_Launcher.EntityClasses;
using System;
using System.Collections.Generic;
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

namespace Round_Minecraft_Launcher.Pages.Setting_SubPages.GameSetting_SubPages
{
    /// <summary>
    /// Select_Version_JavaEdtion.xaml 的交互逻辑
    /// </summary>
    public partial class Select_Version_JavaEdtion : Page
    {
        public Select_Version_JavaEdtion()
        {
            InitializeComponent();

            if (Directory.Exists(".minecraft\\versions"))
            {
                string[] versions = Directory.GetDirectories(".minecraft\\versions");

                foreach (string version in versions) { 
                    Version_Control_Config version_Control_Config = new Version_Control_Config();
                    version_Control_Config.Text = System.IO.Path.GetDirectoryName(version);

                    Version_Control version_Control = new Version_Control(version_Control_Config);
                    version_Control.HorizontalAlignment = HorizontalAlignment.Stretch;
                    Versions_List.Items.Add(version_Control);
                }
            }
        }
    }
}
