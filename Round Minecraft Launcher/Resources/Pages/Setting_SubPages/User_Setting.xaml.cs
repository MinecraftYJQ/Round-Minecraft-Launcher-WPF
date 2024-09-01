using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Cs;
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

namespace Round_Minecraft_Launcher.Pages.Setting_SubPages
{
    /// <summary>
    /// User_Setting.xaml 的交互逻辑
    /// </summary>
    public partial class User_Setting : System.Windows.Controls.Page
    {
        public User_Setting()
        {
            InitializeComponent();
            if (File.Exists("RMCL\\User"))
            {
                foreach(string name in File.ReadAllLines("RMCL\\User"))
                {
                    string[] message = name.Split('|');
                    string item = "";
                    if (message[0] == "1")
                    {
                        item += $"离线账户 - {message[1]}";
                    }

                    if (File.Exists("RMCL\\Name"))
                    {
                        if (File.ReadAllText("RMCL\\Name") == message[1])
                        {
                            NameList.SelectedItem = item;
                        }
                    }

                    NameList.Items.Add(item);
                }
            }
            else
            {
                if (File.Exists("RMCL\\Name"))
                {
                    NameList.Items.Add("离线账户 - " + File.ReadAllText("RMCL\\Name"));
                    NameList.SelectedItem = "离线账户 - " + File.ReadAllText("RMCL\\Name");
                }
            }
        }
        bool oks = true;
        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            oks = false;
            //Add_User add = new Add_User();
            //add.ShowDialog();
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "添加账户";
            dialog.PrimaryButtonText = "添加";
            dialog.CloseButtonText = "取消";
            dialog.DefaultButton = ContentDialogButton.Primary;
            iNKORE.UI.WPF.Modern.Controls.Frame frame = new iNKORE.UI.WPF.Modern.Controls.Frame();
            UserSetting_SubPages.Add_User_Page add_User_Page = new UserSetting_SubPages.Add_User_Page(dialog);
            frame.Content = add_User_Page;
            dialog.Content = frame;

            var result = await dialog.ShowAsync();

            //string user = add_User_Page.Get_UserMessage();
            if (result == ContentDialogResult.Primary)
            {
                string user = add_User_Page.Get_UserMessage();

                List<string> userlist = new List<string>();
                userlist.Add(user);
                //userlist.Add("1|"+File.ReadAllText("RMCL\\Name"));
                File.WriteAllText("RMCL\\Name", user.Split('|')[1]);
                if (File.Exists("RMCL\\User"))
                {
                    NameList.Items.Clear();
                    foreach (string name in File.ReadAllLines("RMCL\\User"))
                    {
                        userlist.Add(name);
                    }
                    File.WriteAllLines("RMCL\\User", userlist);
                    foreach (string name in File.ReadAllLines("RMCL\\User"))
                    {
                        string[] message = name.Split('|');
                        string item = "";
                        if (message[0] == "1")
                        {
                            item += $"离线账户 - {message[1]}";
                        }

                        if (File.Exists("RMCL\\Name"))
                        {
                            if (File.ReadAllText("RMCL\\Name") == message[1])
                            {
                                NameList.Dispatcher.Invoke(new Action(() =>
                                {
                                    NameList.SelectedItem = item;
                                }));
                            }
                        }

                        NameList.Items.Add(item);
                    }
                }
            }
            oks = true;
        }

        private void NameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (oks)
            {
                File.WriteAllText("RMCL\\Name", NameList.SelectedItem.ToString().Split(" - ")[1]);
            }
        }
    }
}
