using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.Cs.API.DownloadTask
{
    internal class DelDownloadTask
    {
        public static void DelItemByUUID(string uuid)
        {
            for (int i = 0; i <= GL.DownloadTaskItemUUID.Count-1; i++) {
                if (GL.DownloadTaskItemUUID[i] == uuid) { 
                    GL.DownloadTaskItemUUID.RemoveAt(i);
                    GL.MainWindow.Dispatcher.Invoke(() =>
                    {
                        GL.DownloadTaskBox.Items.RemoveAt(i);
                    });
                    break;
                }
            }
        }
    }
}
