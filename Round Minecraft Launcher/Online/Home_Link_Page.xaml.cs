using Round.Online.Luncher.Pages;
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

namespace Round_Minecraft_Launcher.Online
{
    /// <summary>
    /// Home_Link_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Home_Link_Page : Page
    {
        public Home_Link_Page()
        {
            InitializeComponent();
            Round.Online.Luncher.Cs.GL.Main_Frame=Main_Page_Link_Frame;
            Round.Online.Luncher.Cs.GL.Main_Frame.Navigate(new Main_Pages());
        }
    }
}
