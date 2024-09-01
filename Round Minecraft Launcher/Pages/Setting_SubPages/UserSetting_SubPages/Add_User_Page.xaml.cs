using iNKORE.UI.WPF.Modern.Controls;
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
using Page = System.Windows.Controls.Page;

namespace Round_Minecraft_Launcher.Pages.Setting_SubPages.UserSetting_SubPages
{
    /// <summary>
    /// Add_User_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Add_User_Page : Page
    {
        ContentDialog dialogs;
        public Add_User_Page(ContentDialog dialog)
        {
            InitializeComponent();
            dialogs= dialog;
            userlx.SelectedIndex = 0;
        }
        public string Get_UserMessage()
        {
            return "1|" + username.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dialogs.Hide();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "添加账户";
            dialog.CloseButtonText = "取消";
            iNKORE.UI.WPF.Modern.Controls.Frame frame = new iNKORE.UI.WPF.Modern.Controls.Frame();
            UserSetting_SubPages.Login_Microsoft_Page add_User_Page = new UserSetting_SubPages.Login_Microsoft_Page(dialog);
            frame.Content = add_User_Page;
            dialog.Content = frame;

            dialog.ShowAsync();
        }
    }
}
