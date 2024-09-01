using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Cs;
using Round_Minecraft_Launcher.Pages.Setting_SubPages.GameSetting_SubPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion.Json;

namespace Round_Minecraft_Launcher.Pages.Setting_SubPages
{
    /// <summary>
    /// Game_Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Game_Setting : System.Windows.Controls.Page
    {
        public Game_Setting()
        {
            InitializeComponent();
            string programPath = @"where";

            // 设置要传递给程序的参数
            string arguments = "java";

            // 创建ProcessStartInfo对象，设置需要的属性
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = programPath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true, // 如果你需要捕获输出
                CreateNoWindow = true // 不显示程序窗口
            };

            if (File.Exists("RMCL\\Command"))
            {
                if (File.ReadAllText("RMCL\\Command")=="False")
                {
                    commandplue.IsChecked = false;
                }
            }
            // 创建并启动进程
            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                foreach (string item in process.StandardOutput.ReadToEnd().Split("\r\n"))
                {
                    javasetting.Items.Add(item);
                }
                javasetting.Items.Remove(javasetting.Items[javasetting.Items.Count - 1]);
            }
            if (File.Exists("RMCL\\Java"))
            {
                for (int i = 0; i <= javasetting.Items.Count - 1; i++)
                {
                    if (javasetting.Items[i].ToString() == File.ReadAllText("RMCL\\Java"))
                    {
                        javasetting.SelectedIndex = i;
                        break;
                    }
                }
            }
            Task.Run(() =>
            {
                while (true)
                {
                    if (Directory.Exists(".minecraft\\versions"))
                    {
                        // 获取目录下的所有文件夹（不包括文件和子文件夹）
                        string[] directories = Directory.GetDirectories(".minecraft\\versions");
                        oks = false;
                        version.Dispatcher.Invoke(() => {
                            version.Items.Clear();
                        });
                        GL.Java_Install_List.Clear();
                        // 遍历文件夹数组
                        foreach (string folder in directories)
                        {
                            //Console.WriteLine(folder);
                            version.Dispatcher.Invoke(() => {
                                version.Items.Add(System.IO.Path.GetFileName(folder));
                            });
                            GL.Java_Install_List.Add(System.IO.Path.GetFileName(folder));
                        }
                        if (File.Exists("RMCL\\Version"))
                        {
                            version.Dispatcher.Invoke(() => {
                                for (int i = 0; i <= version.Items.Count - 1; i++)
                                {
                                    if (version.Items[i].ToString() == File.ReadAllText("RMCL\\Version"))
                                    {
                                        version.SelectedIndex = i;
                                        break;
                                    }
                                }
                            });
                        }
                        oks = true;
                    }
                    Thread.Sleep(30000);
                }
            });

            Info_Command.Message = "拓展指令指的是在游戏内有更多第三方指令";
            
        }
        bool oks = false;
        private void version_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (oks)
            {
                Directory.CreateDirectory("RMCL");

                GL.Game_Version_Str = version.Items[version.SelectedIndex].ToString();
                File.WriteAllText("RMCL\\Version", version.Items[version.SelectedIndex].ToString());
            }
        }

        private void javasetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            File.WriteAllText("RMCL\\Java", javasetting.Items[javasetting.SelectedIndex].ToString());
        }
        private void commandplue_Click(object sender, RoutedEventArgs e)
        {
            if (File.ReadAllText("RMCL\\Command") == "True")
            {
                File.WriteAllText("RMCL\\Command", "False");
            }
            else
            {
                File.WriteAllText("RMCL\\Command", "True");
            }
        }

        private void InfoBarButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog contentDialog = new ContentDialog();
            contentDialog.Title = "拓展指令";
            contentDialog.PrimaryButtonText = "确定";
            contentDialog.DefaultButton = ContentDialogButton.Primary;
            contentDialog.Content = new Label
            {
                Content = "游戏内使用启动器指令请在聊天栏输入: \n#RMCL\n\n功能用法(\"< >\"内为内容 \"[ ]\"内为可选内容):\n\nstart <程序路径> [Args <参数>] 启动外部程序\nstop 关闭游戏"
            };
            contentDialog.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = this.Root;

            System.Windows.Controls.Frame frame = new System.Windows.Controls.Frame();
            frame.Content = new Select_Version_JavaEdtion();
            this.Content = frame;
        }
    }
}
