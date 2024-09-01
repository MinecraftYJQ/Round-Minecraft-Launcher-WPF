using iNKORE.UI.WPF.Modern.Controls;
using Newtonsoft.Json;
using Round_Minecraft_Launcher.Pages.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Round_Minecraft_Launcher.Pages.Load_Page;

namespace Round_Minecraft_Launcher.Cs.API
{
    class Update
    {
        public static async void Update_Launcher()
        {
            if (GL.Launcher_Update_Config != null)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                // 获取程序集的版本属性
                Version version = assembly.GetName().Version;
                Debug.WriteLine(version.ToString());

                dynamic jsonObject = JsonConvert.DeserializeObject(Cs.GL.Launcher_Update_Config);
                string NewVersion = jsonObject.NewVersion;

                string[] verssssssss = version.ToString().Split('.');

                string ver = $"{verssssssss[0]}.{verssssssss[1]}.{verssssssss[2]}";
                if (NewVersion != ver)
                {
                    string url = jsonObject.url;
                    string updatemessage = jsonObject.updatemessage;
                    string updatetime = jsonObject.updatetime;

                    var obj = JsonConvert.DeserializeObject<UpdateInfo>(Cs.GL.Launcher_Update_Config);
                    foreach (var item in obj.History)
                    {
                        Console.WriteLine(item);
                        //这个地方是更新历史
                    }

                    ContentDialog contentDialog = new ContentDialog();
                    contentDialog.Title = "启动器更新";
                    contentDialog.Content = new Label
                    {
                        Content = "" +
                       $"当前版本：{ver.ToString()}\n" +
                       $"更新版本：{NewVersion}\n" +
                       $"更新时间：{updatetime}\n" +
                       $"更新内容：{updatemessage}\n"
                    };

                    contentDialog.PrimaryButtonText = "更新";
                    contentDialog.DefaultButton = ContentDialogButton.Primary;
                    contentDialog.CloseButtonText = "忽略";

                    Task.Run(() =>
                    {
                        while (true)
                        {
                            if (!GL.openmessage)
                            {
                                GL.Frame.Dispatcher.Invoke(async () =>
                                {
                                    var res = await contentDialog.ShowAsync();
                                    if (res == ContentDialogResult.Primary)
                                    {
                                        GL.Frame.Dispatcher.Invoke(() =>
                                        {
                                            GL.Frame.Navigate(new Pages.API.Update(url, NewVersion));
                                        });

                                    }
                                });
                                break;
                            }
                        }
                    });
                }
            }
        }


    }
}
