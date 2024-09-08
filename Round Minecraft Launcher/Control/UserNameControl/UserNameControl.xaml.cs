using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.EntityClasses;
using Round_Minecraft_Launcher.Pages.Setting_SubPages.UserSetting_SubPages.UserNameControl_SubPages;
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
using Frame = System.Windows.Controls.Frame;

namespace Round_Minecraft_Launcher.Control.UserNameControl
{
    /// <summary>
    /// UserControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserNameControl : UserControl
    {
        public UserNameControl(User_Control_Config.UserConfig userConfig)
        {
            InitializeComponent();

            Name.Content = userConfig.OfflineConfig.UserName;
            if(userConfig.UserType == User_Control_Config.UserType.Offline)
            {
                UserMs.Content = "离线账户";
            }
            else
            {
                UserMs.Content = "正版账户";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog contentDialog = new ContentDialog();
            contentDialog.DefaultButton = ContentDialogButton.Close;
            contentDialog.PrimaryButtonText = "取消";
            contentDialog.SecondaryButtonText = "删除此账户";
            contentDialog.CloseButtonText = "选择此账户";
            contentDialog.Title = "账户信息";
            Frame frame = new Frame();
            frame.Content = new Setting_UserNameControl();
            contentDialog.Content = frame;

            contentDialog.ShowAsync();
        }
    }
}
