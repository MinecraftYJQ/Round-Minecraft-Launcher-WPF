using iNKORE.UI.WPF.Modern.Controls;
using MCLauncher;
using Newtonsoft.Json.Linq;
using Round_Minecraft_Launcher.Cs;
using Round_Minecraft_Launcher.Cs.API.DownloadTask;
using Round_Minecraft_Launcher.Cs.Launcher.BedrockEdition;
using Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
using static Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion.Json;

namespace Round_Minecraft_Launcher.Pages.Main_SubPages.Download_SubPages
{
    /// <summary>
    /// Download_Bedrock.xaml 的交互逻辑
    /// </summary>
    public partial class Download_Bedrock : System.Windows.Controls.Page
    {
        public Download_Bedrock()
        {
            InitializeComponent();
            Task.Run(() => {
                List<string> lis = Cs.Launcher.BedrockEdition.Load_Version.Get_Version_List();
                GL.Bedrock_Version_List = lis;

                mess.Dispatcher.Invoke(() =>
                {
                    mess.Visibility = Visibility.Hidden;
                });

                list_ver.Dispatcher.Invoke(() =>
                {
                    list_ver.Visibility = Visibility.Visible;
                    Load_Version_List();
                });
            });
        }
        public void Load_Version_List()
        {
            list_ver.Items.Clear();
            int js = 0;
            foreach (string item in GL.Bedrock_Version_List)
            {
                js++;
                string[] temp = item.Split(',');
                try
                {
                    if (int.Parse(temp[2]) == 0)
                    {
                        Grid grid = new Grid
                        {
                            Height = 50,
                            Width = 584,
                            VerticalAlignment = VerticalAlignment.Top
                        };
                        Label vers = new Label
                        {
                            Content = temp[0],
                            Margin = new Thickness(87, 8, 84, 8),
                            FontSize = 16,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        grid.Children.Add(vers);

                        Button download = new Button
                        {
                            Margin = new Thickness(0, 0, 10, 0),
                            Height = 30,
                            Name = "Dow_ID_NUM" + js.ToString(),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };

                        download.Content = "安装";
                        foreach (string versssss in GL.Bedrock_Install_List)
                        {
                            if (versssss == temp[0]+".appx")
                            {
                                download.Content = "启动";

                                Button del = new Button();
                                del.Content = "删除";
                                del.HorizontalAlignment = HorizontalAlignment.Right;
                                del.Margin = new Thickness(0, 0, 70, 0);
                                del.Click += (s, e) =>
                                {
                                    try
                                    {
                                        File.Delete(".minecraft\\temp\\" + temp[0] + ".appx");
                                        Directory.Delete(".minecraft\\bedrock\\" + temp[0]);
                                    }
                                    catch { }
                                    grid.Children.Remove(del);
                                    download.Content = "安装";
                                };
                                grid.Children.Add(del);
                            }
                        }
                        download.Click += (s, e) =>
                        {
                            //Cs.Launcher.JavaEdtion.Download.Download_Game(id);
                            if (download.Content == "安装")
                            {
                                string itemuuid = NewDownloadTask.GetItemUUID();

                                Download_SubPages.Download_Bedrock_Game_Page download_Games = new Download_SubPages.Download_Bedrock_Game_Page(temp[1], temp[0], itemuuid);
                                //GL.Frame.Navigate(download_Games);
                                NewDownloadTask.AddDownloadTask(download_Games);

                                download.Content = "启动";

                                Button del = new Button();
                                del.Content = "删除";
                                del.HorizontalAlignment = HorizontalAlignment.Right;
                                del.Margin = new Thickness(0, 0, 70, 0);
                                del.Click += (s, e) =>
                                {
                                    File.Delete(".minecraft\\bedrock\\" + temp[0] + ".appx");
                                    grid.Children.Remove(del);
                                    download.Content = "安装";
                                };
                                grid.Children.Add(del);
                            }
                            else
                            {
                                Task.Run(() =>
                                {
                                    string vers = temp[0];
                                    Launcher.Launch(vers);
                                });
                            }
                        };
                        grid.Children.Add(download);

                        Bitmap bitmap = Round_Minecraft_Launcher.Properties.Resources.bedrock;
                        bitmap.Save("RMCL\\bedrock.png");
                        bitmap.Dispose();
                        byte[] imageBytes = File.ReadAllBytes("RMCL\\bedrock.png");
                        using (var memoryStream = new System.IO.MemoryStream(imageBytes))
                        {
                            var bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = memoryStream;
                            bitmapImage.EndInit();
                            bitmapImage.Freeze();
                            var imageBrush = new ImageBrush
                            {
                                ImageSource = bitmapImage,
                                Stretch = Stretch.Uniform
                            };
                            var gridItem = new Grid
                            {
                                Height = 50,
                                Margin = new Thickness(32, 0, 0, 0),
                                Width = 53,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Background = imageBrush
                            };
                            grid.Children.Add(gridItem);
                        }

                        list_ver.Items.Add(grid);
                    }
                }
                catch { }
            }
        }
    }
}
