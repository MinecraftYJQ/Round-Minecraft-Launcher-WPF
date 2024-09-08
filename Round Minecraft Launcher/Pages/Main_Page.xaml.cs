using iNKORE.UI.WPF.Helpers;
using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Common.IconKeys;
using iNKORE.UI.WPF.Modern.Controls;
using iNKORE.UI.WPF.Modern.Media.Animation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Round.Online.Luncher.Pages;
using Round_Minecraft_Launcher.Cs;
using Round_Minecraft_Launcher.Cs.API;
using Round_Minecraft_Launcher.Cs.API.DownloadTask;
using Round_Minecraft_Launcher.Cs.API.MessageSystem;
using Round_Minecraft_Launcher.Cs.API.SDK;
using Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion;
using Round_Minecraft_Launcher.Pages.Main_SubPages;
using Round_Minecraft_Launcher.Pages.Main_SubPages.Download_SubPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
using Windows.ApplicationModel.Store.Preview;
using static Round_Minecraft_Launcher.Pages.Load_Page;

namespace Round_Minecraft_Launcher.Pages
{
    /// <summary>
    /// Main_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Main_Page : System.Windows.Controls.Page
    {
        public Main_Page()
        {
            InitializeComponent();
            Init.Init_Luncher();
            if (!File.Exists("RMCL\\Name"))
            {
                NameShow.Content = $"RMCLPlayer{new Random().Next(1000,9999)}";
                File.WriteAllText("RMCL\\Name", (string?)NameShow.Content);
            }
            if (!File.Exists("RMCL\\User"))
            {
                File.WriteAllText("RMCL\\User", "1|"+ NameShow.Content);
            }
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            if (GL.Game_Version_Str == null)
                            {
                                Lunch_Title.Content = "选择游戏";
                                Game_Version.Content = "点击此处选择游戏版本~";
                                lunch_ico.Icon = FluentSystemIcons.ArrowParagraph_24_Regular;
                            }
                            else
                            {
                                Lunch_Title.Content = "启动游戏";
                                lunch_ico.Icon = FluentSystemIcons.AirplaneTakeOff_20_Regular;
                                Game_Version.Content = "Minecraft " + GL.Game_Version_Str;
                            }
                        });
                    }
                    catch { }

                    if (File.Exists("RMCL\\Name"))
                    {
                        try
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                NameShow.Content = File.ReadAllText("RMCL\\Name");
                            });
                        }
                        catch
                        {
                            NewMessage.Show("发生错误", "发送错误", 3);
                        }
                    }
                    Thread.Sleep(500);
                }
            });

            if (!GL.back)
            {
                Update.Update_Launcher();
            }

            GL.back = true;
            GL.temppage = this;

            GL.MessageBoxGrid = MessageBoxGrid;
            GL.MainNav = Nav;
            GL.MainNavShow = Frame_Main;

            Home Home = new Home();
            Main_SubPages.Setting setting = new Main_SubPages.Setting();
            About about = new About();
            Online.Home_Link_Page Link_Main_Page = new Online.Home_Link_Page();
            Main_SubPages.Download downloads = new Main_SubPages.Download();
            Main_SubPages.DownloadTask downloadstask = new Main_SubPages.DownloadTask();

            //上方
            Function.RegisterPage(new Function.FuncConifg
            {
                ItemPage = Home,
                ItemTitle = "主页",
                ItemType=Function.FuncConfigValue.Head,
                IconFontStr = SegoeFluentIcons.Home,
                IsThis = true
            });
            Function.RegisterPage(new Function.FuncConifg
            {
                ItemPage = downloads,
                ItemTitle = "版本管理",
                ItemType = Function.FuncConfigValue.Head,
                IconFontStr = SegoeFluentIcons.Manage
            });
            Function.RegisterPage(new Function.FuncConifg
            {
                ItemPage = Link_Main_Page,
                ItemTitle = "联机",
                ItemType = Function.FuncConfigValue.Head,
                IconFontStr = SegoeFluentIcons.Link
            });

            //下方
            Function.RegisterPage(new Function.FuncConifg
            {
                ItemPage = downloadstask,
                ItemTitle = "下载任务",
                ItemType = Function.FuncConfigValue.Foot,
                IconFontStr = SegoeFluentIcons.CloudDownload
            });
            Function.RegisterPage(new Function.FuncConifg
            {
                ItemPage = setting,
                ItemTitle = "设置",
                ItemType = Function.FuncConfigValue.Foot,
                IconFontStr = SegoeFluentIcons.Settings
            });
            Function.RegisterPage(new Function.FuncConifg
            {
                ItemPage = about,
                ItemTitle = "关于",
                ItemType = Function.FuncConfigValue.Foot,
                IconFontStr = SegoeFluentIcons.Info
            });

            //Nav.MenuItemsSource = 1;
        }

        public static System.Windows.Controls.Frame Frames_Main;
        private void NavigationView_SelectionChanged(iNKORE.UI.WPF.Modern.Controls.NavigationView sender, iNKORE.UI.WPF.Modern.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var item = Nav.SelectedItem;
            System.Windows.Controls.Page? page = null;

            page = GetPage.GetSubPage(item.ToString());

            if (page != null)
            {
                Frame_Main.Navigate(page, null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromBottom });
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (GL.Game_Version_Str == null)
            {
                Frame_Main.Navigate(GetPage.GetSubPage("设置"));
            }
            else
            {
                Task.Run(() =>
                {
                    string vers = File.ReadAllText("RMCL\\version").Split('|')[0];

                    this.Dispatcher.Invoke(() => {
                        NewDownloadTask.AddDownloadTask(new Download_Games(vers, NewDownloadTask.GetItemUUID(), true));
                    });
                });
            }
        }
    }
}
