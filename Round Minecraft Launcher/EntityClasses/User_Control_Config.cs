using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.EntityClasses
{
    public class User_Control_Config
    {
        public class Genuine
        {
            public string UserName { get; set; }
        }
        public class Offline
        { 
            public string UserName { get; set; }
        }
        public static class UserType
        {
            public static int Genuine { get; } = 1;
            public static int Offline { get; } = 0;
        }
        public class UserConfig
        {
            public int UserType { get; set; }
            public Genuine GenuineConfig = new Genuine();
            public Offline OfflineConfig = new Offline();
        }
    }
}
