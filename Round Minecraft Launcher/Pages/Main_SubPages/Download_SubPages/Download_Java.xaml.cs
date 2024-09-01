using iNKORE.UI.WPF.Modern.Controls;
using Newtonsoft.Json.Linq;
using Round_Minecraft_Launcher.Cs;
using Round_Minecraft_Launcher.Pages.Main_SubPages.Download_SubPages;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Round_Minecraft_Launcher.Pages.Main_SubPages
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download_Java : System.Windows.Controls.Page
    {
        public Download_Java()
        {
            InitializeComponent();
            Load_Version_List();
        }

        private void Load_Version_List()
        {
            Main_SubPages.Setting setting = new Main_SubPages.Setting();
            list_ver.Items.Clear();
            if (GL.Download_Game_List_Json != "notjson")
            {
                JObject manifest = JObject.Parse(GL.Download_Game_List_Json);
                JArray versions = (JArray)manifest["versions"];
                for (int i = 0; i < versions.Count; i++)
                {
                    JObject version = (JObject)versions[i];
                    if ((string)version["type"] == "release")
                    {
                        string id = (string)version["id"];
                        string time = (string)version["time"];
                        string releaseTime = (string)version["releaseTime"];

                        //Console.WriteLine($"ID: {id}, Time: {time}, ReleaseTime: {releaseTime}");

                        Grid grid = new Grid
                        {
                            Height = 50,
                            Width = 584,
                            VerticalAlignment = VerticalAlignment.Top
                        };
                        Label vers = new Label
                        {
                            Content = id,
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
                            Name = "Dow_ID_NUM" + i.ToString(),
                            HorizontalAlignment = HorizontalAlignment.Right
                        };
                        download.Content = "安装";
                        foreach (string versssss in GL.Java_Install_List)
                        {
                            if (versssss == id)
                            {
                                download.Content = "启动";

                                Button del = new Button();
                                del.Content = "删除";
                                del.HorizontalAlignment = HorizontalAlignment.Right;
                                del.Margin = new Thickness(0, 0, 70, 0);
                                del.Click += (s, e) =>
                                {
                                    Directory.Delete(".minecraft\\versions\\"+id,true);
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
                                this.Dispatcher.Invoke(() =>
                                {
                                    ContentDialog contentDialog = new ContentDialog();
                                    contentDialog.Content = new System.Windows.Controls.Frame
                                    {
                                        Content = new Download_Games(id, contentDialog, true)
                                    };

                                    contentDialog.Title = "下载游戏";
                                    contentDialog.ShowAsync();
                                });

                                download.Content = "启动";

                                Button del = new Button();
                                del.Content = "删除";
                                del.HorizontalAlignment = HorizontalAlignment.Right;
                                del.Margin = new Thickness(0, 0, 70, 0);
                                del.Click += (s, e) =>
                                {
                                    Directory.Delete(".minecraft\\versions\\" + id);
                                    grid.Children.Remove(del);
                                    download.Content = "安装";
                                };
                                grid.Children.Add(del);
                            }
                            else
                            {
                                Task.Run(() =>
                                {
                                    string vers = id;
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        ContentDialog contentDialog = new ContentDialog();
                                        contentDialog.Content = new System.Windows.Controls.Frame
                                        {
                                            Content= new Download_Games(vers, contentDialog,true)
                                        };

                                        contentDialog.Title = "下载游戏";
                                        contentDialog.ShowAsync();
                                    });
                                });
                            }
                        };
                        grid.Children.Add(download);

                        byte[] imageBytes = Round_Minecraft_Launcher.Properties.Resources.Minecraft_Version;
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
            }
        }
    }
}
