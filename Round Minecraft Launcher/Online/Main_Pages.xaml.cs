using Round.Online.Luncher.Cs;
using System;
using System.Collections.Generic;
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

namespace Round.Online.Luncher.Pages
{
    /// <summary>
    /// Main_Pages.xaml 的交互逻辑
    /// </summary>
    public partial class Main_Pages : Page
    {
        public Main_Pages()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Round.Online.Luncher.Cs.GL.Main_Frame.Dispatcher.Invoke(() =>
            {
                Round.Online.Luncher.Cs.GL.Main_Frame.Navigate(new Link_Tunnels());
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Round.Online.Luncher.Cs.GL.Main_Frame.Dispatcher.Invoke(() =>
            {
                Round.Online.Luncher.Cs.GL.Main_Frame.Navigate(new Create_Tunnel());
            });
        }
    }
}
