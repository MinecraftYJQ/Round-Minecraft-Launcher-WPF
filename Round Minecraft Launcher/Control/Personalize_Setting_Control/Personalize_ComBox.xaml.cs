using iNKORE.UI.WPF.Modern.Helpers.Styles;
using Round.Online.Luncher.Cs;
using Round_Minecraft_Launcher.Cs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace Round_Minecraft_Launcher.Control.Personalize_Setting_Control
{
    /// <summary>
    /// Personalize_ComBox.xaml 的交互逻辑
    /// </summary>
    public partial class Personalize_ComBox : UserControl
    {
        public Personalize_ComBox()
        {
            InitializeComponent();
            try
            {
                Com.SelectedIndex = int.Parse(File.ReadAllText("RMCL\\Skin\\BackdropHelper"));
            }
            catch
            {
                Com.SelectedIndex = 1;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Com.SelectedIndex == 0)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Acrylic);
            }
            else if(Com.SelectedIndex == 1) {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Mica);
            }
            else if (Com.SelectedIndex == 2)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Tabbed);
            }
            else
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.None);
            }

            File.WriteAllText("RMCL\\Skin\\BackdropHelper", Com.SelectedIndex.ToString());
        }
    }
}
