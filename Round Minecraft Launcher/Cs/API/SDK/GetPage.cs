using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Round_Minecraft_Launcher.Cs.API.SDK
{
    class GetPage
    {
        public static List<string> ItemNamesList = new List<string>();
        public static List<Page> ItemPagesList = new List<Page>();
        public static Page GetSubPage(string name)
        {
            string itemname = name.Replace("iNKORE.UI.WPF.Modern.Controls.NavigationViewItem: ", "");
            Debug.WriteLine($"[Debug]:目标页面:{itemname}");
            for (int i = 0; i <= ItemNamesList.Count - 1; i++)
            {
                if (ItemNamesList[i] == itemname)
                {
                    return ItemPagesList[i];
                }
            }

            return null;
        }
    }
}
