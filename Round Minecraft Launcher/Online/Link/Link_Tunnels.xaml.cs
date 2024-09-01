using Round.Online.Luncher.Cs;
using Round.Online.Luncher.Pages.Create;
using Round.Online.Luncher.Pages.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Round.Online.Luncher.Pages
{
    /// <summary>
    /// Link_Tunnels.xaml 的交互逻辑
    /// </summary>
    public partial class Link_Tunnels : Page
    {
        public Link_Tunnels()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                byte[] base64DecodedBytes = Convert.FromBase64String(UID_Code.Text.Replace("ROL-", ""));
                string originalString = System.Text.Encoding.UTF8.GetString(base64DecodedBytes);

                //iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(originalString, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                if (originalString.Split('|').Length==3)
                {
                    GL.Main_Frame.Navigate(new Link_End_Page(UID_Code.Text));
                }
                else
                {
                    iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("非有效联机码！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                string[] okys = originalString.Split('|');
            }
            catch
            {
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("非有效联机码！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Back_Main(object sender, RoutedEventArgs e)
        {
            if (iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("你确定要返回主页面吗？\n如果返回，此页面上的所有进度将清除！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                GL.Main_Frame.Navigate(new Main_Pages());
            }
        }
    }
}
