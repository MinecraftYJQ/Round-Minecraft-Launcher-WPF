using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Round_Minecraft_Launcher.Cs
{
    internal class GL
    {
        public static string Game_Version_Str = null;
        public static string Download_Game_List_Json = "notjson";
        public static List<string> Bedrock_Version_List;
        public static iNKORE.UI.WPF.Modern.Controls.Frame Frame;
        public static bool back = false;

        public static List<string> Java_Install_List=new List<string>();
        public static List<string> Bedrock_Install_List = new List<string>();
        public static Main_Page temppage;

        public static string Update_Config = null;
        public static string LauncherMessage;

        public static string Launcher_Update_Config = null;
        public static bool openmessage = false;
        public static MainWindow MainWindow;

        public static List<string> DownloadTaskItemUUID = new List<string>();
        public static ListBox DownloadTaskBox;

        public static Grid MessageBoxGrid;

        public static NavigationView MainNav;
    }
}
