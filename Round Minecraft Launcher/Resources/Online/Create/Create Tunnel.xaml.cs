using Round.Online.Luncher.Cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using iNKORE.UI.WPF.Modern.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Round.Online.Luncher.Pages.Create;

namespace Round.Online.Luncher.Pages
{
    /// <summary>
    /// Create_Tunnel.xaml 的交互逻辑
    /// </summary>
    public partial class Create_Tunnel : System.Windows.Controls.Page
    {
        public Create_Tunnel()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Port_Box.Text != "")
            {
                try
                {
                    int try_num = int.Parse(Port_Box.Text);
                    if (try_num < 65535 && try_num > 0)
                    {
                        string name;
                        if (Name_Box.Text != "")
                        {
                            name = Name_Box.Text;
                        }
                        else
                        {
                            name = "Minecraft Online Room";
                        }
                        //iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(Get_Uid.Get_Uid_Func(try_num, name), "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        GL.Main_Frame.Navigate(new Create_End_Page(Get_Uid.Get_Uid_Func(try_num, name)));
                    }
                    else
                    {
                        iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("端口范围：0~65535", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch
                {
                    iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("房间端口请使用整数，禁止出现字母，中文，符号", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("请输入端口号", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back_Main(object sender, RoutedEventArgs e)
        {
            
            if(iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("你确定要返回主页面吗？\n如果返回，此页面上的所有进度将清除！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Information)== MessageBoxResult.OK)
            {
                GL.Main_Frame.Navigate(new Main_Pages());
            }
        }
    }
}
