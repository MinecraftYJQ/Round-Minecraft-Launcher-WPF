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
using System.Windows.Shapes;

namespace Round_Minecraft_Launcher.Windows.Setting
{
    /// <summary>
    /// Add_User.xaml 的交互逻辑
    /// </summary>
    public partial class Add_User : Window
    {
        public Add_User()
        {
            InitializeComponent();
            userlx.SelectedIndex = 0;
        }
        string message = null;
        public string Get_UserMessage()
        {
            return message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text != "")
            {
                message = "1|" + username.Text;
            }
            Close();
        }
    }
}
