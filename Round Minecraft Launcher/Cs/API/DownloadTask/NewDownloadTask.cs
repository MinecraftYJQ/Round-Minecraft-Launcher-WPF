using Round_Minecraft_Launcher.Cs.API.MessageSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Round_Minecraft_Launcher.Cs.API.DownloadTask
{
    internal class NewDownloadTask
    {
        public static void AddDownloadTask(Page page)
        {
            GL.MainWindow.Dispatcher.Invoke(new Action(() => {
                Grid grid = new Grid();
                grid.Width = 590;

                Frame frame = new Frame();
                frame.Width = 590;
                grid.Children.Add(frame);

                frame.Content = page;
                GL.DownloadTaskBox.Items.Add(grid);

                NewMessage.Show("任务已添加至下载任务内", "下载任务", 0);
            }));
        }

        public static string GetItemUUID()
        {
            Guid guid = Guid.NewGuid();
            string uuid = guid.ToString("N");
            GL.DownloadTaskItemUUID.Add(uuid);
            return uuid;
        }
    }
}
