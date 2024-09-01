using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.Cs.API.MessageSystem
{
    internal class NewMessage
    {
        static Thread Thread = null;

        //0是信息 1是疑问 2是警告 3是错误
        public static void Show(string message,string title,int Ms)
        {
            if(Thread != null) try { Thread.Abort(); }catch { }

            GL.MessageBoxGrid.Dispatcher.Invoke(() => {
                GL.MessageBoxGrid.Children.Clear();

                InfoBar infoBar = new InfoBar();
                infoBar.Title = title;
                infoBar.Message = message;
                infoBar.IsClosable = true;
                infoBar.Severity = (InfoBarSeverity)Ms;
                infoBar.IsOpen = true;
                infoBar.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

                GL.MessageBoxGrid.Children.Add(infoBar);
            });
            Thread = new Thread(() =>
            {
                Thread.Sleep(5000);
                GL.MessageBoxGrid.Dispatcher.Invoke(() => {
                    GL.MessageBoxGrid.Children.Clear();
                });
            });
            Thread.Start();
        }
    }
}
