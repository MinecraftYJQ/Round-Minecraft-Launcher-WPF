using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Pages.Setting_SubPages.UserSetting_SubPages.Login_SubPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Round_Minecraft_Launcher.Pages.Setting_SubPages.UserSetting_SubPages
{
    /// <summary>
    /// Login_Microsoft_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Login_Microsoft_Page : System.Windows.Controls.Page
    {
        ContentDialog dialogss;
        public Login_Microsoft_Page(ContentDialog dialog)
        {
            dialogss=dialog;
            InitializeComponent();
            Task.Run(() =>
            {
                while (true)
                {
                    string windowTitle = "脚本错误";

                    // 调用FindWindow查找窗口
                    IntPtr windowHandle = FindWindow(null, windowTitle);

                    if (windowHandle != IntPtr.Zero)
                    {
                        // 如果找到了窗口，发送关闭消息
                        SendMessage(windowHandle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        //Console.WriteLine("Window with title '" + windowTitle + "' has been closed.");
                    }
                    else
                    {
                        //Console.WriteLine("No window with title '" + windowTitle + "' found.");
                    }
                    Thread.Sleep(50);
                }
            });
            /*Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "powershell";
            //info.Arguments = "InetCpl.cpl ClearMyTracksByProcess 4351";
            info.Arguments = "/c start \"https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize?client_id=b91b99f8-e1e9-4da6-966c-bb50a7bb6c47&response_type=code&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_mode=query&scope=XboxLive.signin offline_access\"";

            process.StartInfo = info;
            process.Start();*/

            System.Diagnostics.Process.Start("https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize?client_id=b91b99f8-e1e9-4da6-966c-bb50a7bb6c47&response_type=code&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_mode=query&scope=XboxLive.signin offline_access");
            
            //Web.Source = new Uri("https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize?client_id=b91b99f8-e1e9-4da6-966c-bb50a7bb6c47&response_type=code&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_mode=query&scope=XboxLive.signin offline_access");
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        const uint WM_CLOSE = 0x0010; // 用于关闭窗口的消息
        static string GetCodeFromUrl(string url)
        {
            // 找到 '?' 符号，获取参数部分
            int queryIndex = url.IndexOf('?');
            if (queryIndex != -1)
            {
                string query = url.Substring(queryIndex + 1);

                // 解析参数对
                string[] queryParams = query.Split('&');
                foreach (string param in queryParams)
                {
                    string[] keyValue = param.Split('=');
                    if (keyValue.Length == 2 && keyValue[0] == "code")
                    {
                        return keyValue[1];
                    }
                }
            }
            return null; // 如果没有找到 'code' 参数，返回null
        }

        private void Web_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //MessageBox.Show("Navigating to: " + e.Uri.ToString());
            if (e.Uri.ToString().Contains("https://login.live.com/oauth20_desktop.srf"))
            {
                string mess = e.Uri.ToString();

                //MessageBox.Show("Navigating to: " + GetCodeFromUrl(mess));
                //Web_Grid.Children.Remove(Web);
                Process process = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "RunDll32.exe";
                info.Arguments = "InetCpl.cpl ClearMyTracksByProcess 4351";

                process.StartInfo = info;
                process.Start();

                dialogss.Hide();
                ContentDialog dialog = new ContentDialog();
                iNKORE.UI.WPF.Modern.Controls.Frame frame = new iNKORE.UI.WPF.Modern.Controls.Frame();
                Console.WriteLine(mess);
                UserSetting_SubPages.Login_SubPages.Login add_User_Page = new UserSetting_SubPages.Login_SubPages.Login(GetCodeFromUrl(mess), dialog);
                frame.Content = add_User_Page;
                dialog.Content = frame;

                dialog.ShowAsync();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ReturnText.Text.Contains("https://login.live.com/oauth20_desktop.srf"))
            {
                string mess = ReturnText.Text;

                //MessageBox.Show("Navigating to: " + GetCodeFromUrl(mess));
                //Web_Grid.Children.Remove(Web);
                /*Process process = new Process();
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = "RunDll32.exe";
                info.Arguments = "InetCpl.cpl ClearMyTracksByProcess 4351";

                process.StartInfo = info;
                process.Start();*/

                dialogss.Hide();
                ContentDialog dialog = new ContentDialog();
                iNKORE.UI.WPF.Modern.Controls.Frame frame = new iNKORE.UI.WPF.Modern.Controls.Frame();
                Console.WriteLine(mess);
                UserSetting_SubPages.Login_SubPages.Login add_User_Page = new UserSetting_SubPages.Login_SubPages.Login(GetCodeFromUrl(mess), dialog);
                frame.Content = add_User_Page;
                dialog.Content = frame;

                dialog.ShowAsync();
            }
        }
    }
}
