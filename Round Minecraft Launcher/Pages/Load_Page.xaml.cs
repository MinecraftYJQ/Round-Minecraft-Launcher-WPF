using iNKORE.UI.WPF.Modern.Controls;
using Newtonsoft.Json;
using Round.Online.Luncher.Cs;
using Round_Minecraft_Launcher.Cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Round_Minecraft_Launcher.Pages
{
    /// <summary>
    /// Load_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Load_Page : System.Windows.Controls.Page
    {
        public Load_Page()
        {
            InitializeComponent();
            Task.Run(() =>
            {
                Task.Run(() =>
                {
                    using (var client = new HttpClient())
                    {
                        string json = null;
                        try
                        {
                            json = client.GetStringAsync("https://piston-meta.mojang.com/mc/game/version_manifest.json").Result;
                            Cs.GL.Update_Config = client.GetStringAsync("https://gitee.com/minecraftyjq/round-minecraft-launcher-update/raw/master/Config/Config.json").Result;
                            Cs.GL.Launcher_Update_Config = client.GetStringAsync("https://gitee.com/minecraftyjq/round-minecraft-launcher-update/raw/master/Update/Update.json").Result;
                        }
                        catch (Exception ex) { }
                        if (json != null) Cs.GL.Download_Game_List_Json = json;
                        else Cs.GL.Download_Game_List_Json = "notjson";

                        if (Cs.GL.Update_Config != null)
                        {
                            dynamic jsonObject = JsonConvert.DeserializeObject(Cs.GL.Update_Config);
                            bool online = jsonObject.Online;
                            string launcherMessage = jsonObject.LauncherMessage;
                            string newmessage = jsonObject.NewMessage;

                            Console.WriteLine("Online: " + online);
                            Console.WriteLine("Launcher Message: " + launcherMessage);
                            Cs.GL.Frame.Dispatcher.Invoke(() =>
                            {
                                ContentDialog contentDialog = new ContentDialog();
                                contentDialog.Content = newmessage;
                                contentDialog.DefaultButton=ContentDialogButton.Primary;
                                contentDialog.PrimaryButtonText = "确定";
                                contentDialog.Title = "通知";
                                contentDialog.PrimaryButtonClick += (s,e) =>
                                {
                                    Cs.GL.openmessage = false;
                                };
                                Cs.GL.openmessage = true;
                                contentDialog.ShowAsync();
                            });

                            Cs.GL.LauncherMessage = launcherMessage;
                        }
                        else {
                            Cs.GL.openmessage = false;
                            Cs.GL.LauncherMessage = "RMCL By YJQ_";
                        }
                    }
                    Console.WriteLine("Java 列表加载完毕");
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        Cs.GL.temppage = new Main_Page();
                        Cs.GL.Frame.Navigate(Cs.GL.temppage);
                    }));
                });
            });
        }
        public class UpdateInfo
        {
            public string NewVersion { get; set; }
            public string Url { get; set; }
            public string UpdateMessage { get; set; }
            public string UpdateTime { get; set; }
            public List<string> History { get; set; } // 假设history是一个string数组，根据实际情况调整
        }
    }
}
