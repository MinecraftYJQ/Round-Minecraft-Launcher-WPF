using iNKORE.UI.WPF.Modern.Common.IconKeys;
using iNKORE.UI.WPF.Modern.Controls;
using iNKORE.UI.WPF.Modern.Media.Animation;
using Round_Minecraft_Launcher.Cs.API.MessageSystem;
using Round_Minecraft_Launcher.Pages.Main_SubPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Round_Minecraft_Launcher.Cs.API.SDK
{
    public class Function
    {
        public class FuncConifg
        {
            public string ItemTitle { get; set; }
            public FontIconData IconFontStr { get; set; }
            public int ItemType { get; set; }
            public System.Windows.Controls.Page ItemPage { get; set; }
            public bool IsThis { get; set; } = false;
        }

        public static class FuncConfigValue
        {
            public static int Head = 1;
            public static int Foot = 2;
        }

        public static void RegisterPage(FuncConifg Config)
        {
            if (Config != null)
            {
                //构建项入口
                NavigationViewItem navigationViewItem = new NavigationViewItem();
                navigationViewItem.Content = Config.ItemTitle;

                //构建图标
                FontIcon fontIcon = new FontIcon();
                fontIcon.Icon = Config.IconFontStr;
                navigationViewItem.Icon = fontIcon;

                GetPage.ItemNamesList.Add(Config.ItemTitle);
                GetPage.ItemPagesList.Add(Config.ItemPage);

                if (Config.ItemType == FuncConfigValue.Head)
                {
                    GL.MainNav.MenuItems.Add(navigationViewItem);

                    if (Config.IsThis)
                    {
                        GL.MainNav.SelectedItem = navigationViewItem;
                        GL.MainNavShow.Navigate(GetPage.GetSubPage(Config.ItemTitle));
                    }
                }
                else if(Config.ItemType == FuncConfigValue.Foot)
                {
                    GL.MainNav.FooterMenuItems.Add(navigationViewItem);

                    if (Config.IsThis)
                    {
                        GL.MainNav.SelectedItem = navigationViewItem;
                        GL.MainNavShow.Navigate(GetPage.GetSubPage(Config.ItemTitle));
                    }
                }
                else
                {
                    NewMessage.Show("无法加载此项", "拓展加载", 3);
                }
            }
            else
            {
                NewMessage.Show("无法加载此项", "拓展加载", 3);
            }
        }
    }
}
