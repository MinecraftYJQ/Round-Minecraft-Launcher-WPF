using MCLauncher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.Cs
{
    internal class Init
    {
        public static void Init_Luncher()
        {
            //System.Diagnostics.Process.Start("explorer.exe", "\"https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize?client_id=b91b99f8-e1e9-4da6-966c-bb50a7bb6c47&response_type=code&redirect_uri=https://login.live.com/oauth20_desktop.srf&response_mode=query&scope=XboxLive.signin offline_access\"");
            if (File.Exists("RMCL\\version"))
            {
                GL.Game_Version_Str = File.ReadAllText("RMCL\\version").Split('|')[0];
            }
            //Console.WriteLine(WUTokenHelper.GetWUToken());
        }
    }
}
