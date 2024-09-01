using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Page
    {
        public About()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            // 获取程序集的版本属性
            Version version = assembly.GetName().Version;
            string[] verssssssss = version.ToString().Split('.');

            string ver = $"{verssssssss[0]}.{verssssssss[1]}.{verssssssss[2]}";
            verss.Content = "V " + ver;
        }
    }
}
