using Round.Online.Luncher.Cs;
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

namespace Round.Online.Luncher.Pages.Create
{
    /// <summary>
    /// Edit_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Edit_Page : Page
    {
        string UID;
        public Edit_Page(string uid_code)
        {
            InitializeComponent();
            UID=uid_code;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GL.Main_Frame.Navigate(new Create_End_Page(UID));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
    }
}
