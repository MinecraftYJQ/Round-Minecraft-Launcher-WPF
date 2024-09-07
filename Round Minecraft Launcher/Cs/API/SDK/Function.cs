using iNKORE.UI.WPF.Modern.Common.IconKeys;
using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Cs.API.MessageSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Round_Minecraft_Launcher.Cs.API.SDK
{
    internal class Function
    {
        public class FuncConifg
        {
            public string Title { get; set; }
            public FontIconData IconFontStr { get; set; }
            public int ItemType { get; set; }
            public System.Windows.Controls.Page ItemPage { get; set; }
        }

        public class FuncConfigValue
        {
            public int Head = 1;
            public int Foot = 2;
        }

        public static void RegisterPage(FuncConifg Config)
        {
            if (Config != null) {
                //构建项入口
                NavigationViewItem navigationViewItem = new NavigationViewItem();
                navigationViewItem.Content = Config.Title;

                //构建图标
                FontIcon fontIcon = new FontIcon();
                fontIcon.Icon = Config.IconFontStr;
                navigationViewItem.Icon = fontIcon;

                GL.MainNav.MenuItems.Add(navigationViewItem);
            }
            else
            {
                NewMessage.Show("无法加载此项", "拓展加载", 3);
            }
        }
    }
}
