using Round_Minecraft_Launcher.EntityClasses;
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

namespace Round_Minecraft_Launcher.Control.Version
{
    /// <summary>
    /// Version_Control.xaml 的交互逻辑
    /// </summary>
    public partial class Version_Control : UserControl
    {
        public Version_Control(Version_Control_Config version_Control_Config)
        {
            InitializeComponent();

            Name.Content=version_Control_Config.Text;
        }
    }
}
