using Round.Online.Luncher.Cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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

namespace Round.Online.Luncher.Pages.Link
{
    /// <summary>
    /// Link_End_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Link_End_Page : Page
    {
        string UID;
        Thread Thread;
        public Link_End_Page(string uid)
        {
            InitializeComponent();
            UID = uid;
            Cs.Online.Open_Online_C(UID);
            UID_Code.Text = uid;

            byte[] base64DecodedBytes = Convert.FromBase64String(UID.Replace("ROL-", ""));
            string originalString = System.Text.Encoding.UTF8.GetString(base64DecodedBytes);

            //iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(originalString, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            string[] okys = originalString.Split('|');

            nams.Content = "房间名称：" + okys[0];
            ports.Content = "游戏端口：" + okys[2];

            Thread = new Thread(() =>
            {
                string multicastGroup = "224.0.2.60";
                int multicastPort = 4445;

                using (UdpClient client = new UdpClient(int.Parse(okys[2])))
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(multicastGroup), multicastPort);

                    byte[] ttl = new byte[] { 2 }; // 多播数据包的存活时间
                    client.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);

                    while (true)
                    {
                        string message = $"[MOTD]§b§l[RMCL.Online] §2{okys[0]}[/MOTD][AD]{int.Parse(okys[2])}[/AD]";
                        byte[] data = Encoding.UTF8.GetBytes(message);

                        client.Send(data, data.Length, remoteEP);

                        Thread.Sleep(100);
                    }
                }
            });
            Thread.Start();
        }

        private void Back_Main_Page(object sender, RoutedEventArgs e)
        {
            if (iNKORE.UI.WPF.Modern.Controls.MessageBox.Show("返回主页，将会关闭房间且此页面内容完全消失！\n请问你是否继续？", "是否继续？", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Cs.Online.End_Online();
                GL.Main_Frame.Navigate(new Main_Pages());
            }
        }

        private void Close_Room(object sender, RoutedEventArgs e)
        {
            if (Close.Content.ToString() == "关闭房间")
            {
                Cs.Online.End_Online();
                Close.Content = "开启房间";
                Thread.Abort();
            }
            else
            {
                Close.Content = "关闭房间";
                Thread.Start();
                Cs.Online.Open_Online_C(UID);
            }
        }

        private void UID_Code_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UID_Code.Text != UID)
            {
                UID_Code.Text = UID;
            }
        }
    }
}
