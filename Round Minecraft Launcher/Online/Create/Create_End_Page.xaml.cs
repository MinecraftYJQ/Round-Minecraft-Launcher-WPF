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
    /// Create_End_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Create_End_Page : Page
    {
        string UID;
        public Create_End_Page(string uid)
        {
            InitializeComponent();
            UID = uid;
            Cs.Online.Open_Online(UID);
            UID_Code.Text = uid;

            byte[] base64DecodedBytes = Convert.FromBase64String(UID.Replace("ROL-",""));
            string originalString = System.Text.Encoding.UTF8.GetString(base64DecodedBytes);

            //iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(originalString, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            string[] okys = originalString.Split('|');

            nams.Content= "房间名称："+okys[0];
            ports.Content = "游戏端口：" + okys[2];
        }

        private void UID_Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(UID_Code.Text!= UID)
            {
                UID_Code.Text=UID;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(UID);
        }

        private void Close_Room(object sender, RoutedEventArgs e)
        {
            if(Close.Content.ToString() == "关闭房间")
            {
                if (iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("如果你关闭了房间，则所有玩家将会失去连接！\n请问你是否继续？", "是否关闭房间？", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                {
                    Cs.Online.End_Online();
                    Close.Content = "开启房间";
                }
            }
            else
            {
                Close.Content = "关闭房间";
                Cs.Online.Open_Online(UID);
            }
        }

        private void bianji_Go(object sender, RoutedEventArgs e)
        {
            if (Close.Content.ToString() == "关闭房间")
            {
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("在编辑信息之前，请先关闭房间！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                GL.Main_Frame.Navigate(new Edit_Page(UID));
            }
        }

        private void Back_Main_Page(object sender, RoutedEventArgs e)
        {
            if (iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("返回主页，将会关闭房间且此页面内容完全消失！\n请问你是否继续？", "是否继续？", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Cs.Online.End_Online();
                GL.Main_Frame.Navigate(new Main_Pages());
            }
        }
    }
}
