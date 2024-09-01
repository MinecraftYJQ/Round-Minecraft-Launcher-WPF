using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Common.IconKeys;
using iNKORE.UI.WPF.Modern.Helpers.Styles;
using Newtonsoft.Json.Linq;
using Round_Minecraft_Launcher.Cs;
using Round_Minecraft_Launcher.Pages;
using System;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion.Json;

namespace Round_Minecraft_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory("RMCL");
            Directory.CreateDirectory(".minecraft\\temp");
            Directory.CreateDirectory(".minecraft\\bedrock");
            if (Directory.Exists(".minecraft\\versions"))
            {
                // 获取目录下的所有文件夹（不包括文件和子文件夹）
                string[] directories = Directory.GetDirectories(".minecraft\\versions");
                GL.Java_Install_List.Clear();
                // 遍历文件夹数组
                foreach (string folder in directories)
                {
                    GL.Java_Install_List.Add(System.IO.Path.GetFileName(folder));
                }
            }
            // 获取目录下的所有文件
            string[] directoriess = Directory.GetFiles(".minecraft\\temp");
            GL.Bedrock_Install_List.Clear();
            // 遍历文件夹数组
            foreach (string folder in directoriess)
            {
                GL.Bedrock_Install_List.Add(System.IO.Path.GetFileName(folder));
            }
            GL.MainWindow = this;

            if (File.Exists("RMCL\\Size"))
            {
                string em = File.ReadAllText("RMCL\\Size");
                Width = int.Parse(em.Split('|')[0]);
                Height = int.Parse(em.Split('|')[1]);
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            if (File.Exists("RMCL\\Theme"))
            {
                if (File.ReadAllText("RMCL\\Theme") == "1")
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                }
                else
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                }
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            }
            GL.Frame = Main_Frame;
            
            GL.Frame.Navigate(new Load_Page());

            Directory.CreateDirectory("RMCL\\Skin");
            int backjs = 1;
            try
            {
                backjs = int.Parse(File.ReadAllText("RMCL\\Skin\\BackdropHelper"));
            }
            catch { }
            if (backjs == 0)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Acrylic);
            }
            else if (backjs == 1)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Mica);
            }
            else if (backjs == 2)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Tabbed);
            }
            else
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.None);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            File.WriteAllText("RMCL\\Size", Width.ToString() + "|" + Height.ToString());
            if (ThemeManager.Current.ApplicationTheme == ApplicationTheme.Dark) File.WriteAllText("RMCL\\Theme", "0");
            else File.WriteAllText("RMCL\\Theme", "1");
        }
    }
}