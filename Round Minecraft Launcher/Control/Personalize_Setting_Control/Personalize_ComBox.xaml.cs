using iNKORE.UI.WPF.Modern;
using iNKORE.UI.WPF.Modern.Helpers.Styles;
using Round.Online.Luncher.Cs;
using Round_Minecraft_Launcher.Cs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Round_Minecraft_Launcher.Control.Personalize_Setting_Control
{
    /// <summary>
    /// Personalize_ComBox.xaml 的交互逻辑
    /// </summary>
    public partial class Personalize_ComBox : UserControl
    {
        private void SetBackgroundImage(string imagePath)
        {
            if (imagePath != "")
            {
                // 创建一个新的BitmapImage对象
                BitmapImage bitmap = new BitmapImage();

                // 将图片路径设置为BitmapImage对象的UriSource
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();

                // 将BitmapImage设置为Grid的背景
                ImageBrush imageBrush = new ImageBrush(bitmap);
                imageBrush.Stretch = Stretch.UniformToFill;
                Cs.GL.BackGrid.Background = imageBrush;
            }
        }   
        public Personalize_ComBox()
        {
            InitializeComponent();
            if (File.Exists("RMCL\\Skin\\BackImage") && File.Exists("RMCL\\Skin\\BackMs"))
            {
                if (File.ReadAllText("RMCL\\Skin\\BackMs") != "0")
                {
                    images.IsChecked = true;
                    File.WriteAllText("RMCL\\Skin\\BackMs", "1");

                    SetBackgroundImage(File.ReadAllText("RMCL\\Skin\\BackImage").Replace("\"",""));
                    Paths.Text = File.ReadAllText("RMCL\\Skin\\BackImage").Replace("\"", "");
                    Paths.Visibility = Visibility.Visible;
                    Com.Visibility = Visibility.Hidden;
                }
                else
                {
                    Paths.Visibility = Visibility.Hidden;
                    Com.Visibility = Visibility.Visible;
                    File.WriteAllText("RMCL\\Skin\\BackMs", "0");
                    rmclimage.IsChecked = true;
                    try
                    {
                        Com.SelectedIndex = int.Parse(File.ReadAllText("RMCL\\Skin\\BackdropHelper"));
                    }
                    catch
                    {
                        Com.SelectedIndex = 1;
                    }
                }
            }
            else
            {
                rmclimage.IsChecked = true;
                Paths.Visibility = Visibility.Hidden;
                Com.Visibility = Visibility.Visible;
                try
                {
                    Com.SelectedIndex = int.Parse(File.ReadAllText("RMCL\\Skin\\BackdropHelper"));
                }
                catch
                {
                    Com.SelectedIndex = 1;
                }
            }

            try
            {
                if (File.ReadAllText("RMCL\\Skin\\Theme") == "Dark")
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                }
                else
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                }
            }catch { }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Com.SelectedIndex == 0)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Acrylic);
            }
            else if(Com.SelectedIndex == 1) {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Mica);
            }
            else if (Com.SelectedIndex == 2)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Tabbed);
            }
            else
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.None);
            }

            File.WriteAllText("RMCL\\Skin\\BackdropHelper", Com.SelectedIndex.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ApplicationTheme == ApplicationTheme.Dark)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                File.WriteAllText("RMCL\\Skin\\Theme", "Light");
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                File.WriteAllText("RMCL\\Skin\\Theme", "Dark");
            }

            if (Com.SelectedIndex == 0)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Acrylic);
            }
            else if (Com.SelectedIndex == 1)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Mica);
            }
            else if (Com.SelectedIndex == 2)
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.Tabbed);
            }
            else
            {
                BackdropHelper.Apply(Cs.GL.MainWindow, BackdropType.None);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MsSave();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            MsSave();
        }

        private void MsSave()
        {
            if (Com != null)
            {
                if ((bool)rmclimage.IsChecked)
                {
                    File.WriteAllText("RMCL\\Skin\\BackMs", "0");
                    Paths.Visibility = Visibility.Hidden;
                    Com.Visibility = Visibility.Visible;
                }
                else
                {
                    File.WriteAllText("RMCL\\Skin\\BackMs", "1");
                    Paths.Visibility = Visibility.Visible;
                    Com.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Paths_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Paths.Text != "TextBox")
            {
                File.WriteAllText("RMCL\\Skin\\BackImage", Paths.Text.Replace("\"", ""));
            }
        }
    }
}
