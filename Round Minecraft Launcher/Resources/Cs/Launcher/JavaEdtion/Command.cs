using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion
{
    internal class Command
    {
        public static void Start_Command(string Command, Process process)
        {
            bool trss = true;
            if (File.Exists("RMCL\\Command"))
            {
                if (File.ReadAllText("RMCL\\Command") == "False")
                {
                    trss = false;
                }
            }
            if (trss)
            {
                string[] temp = Command.Split(' ');

                switch (temp[0])
                {
                    case "stop":
                        foreach (Process process1 in Process.GetProcessesByName("java"))
                        {
                            process1.Kill();
                        }
                        GL.Frame.Dispatcher.Invoke(() => {
                            ContentDialog dialog = new ContentDialog();
                            dialog.Title = "游戏关闭";
                            dialog.Content = $"你触发了RMCL的启动器指令 stop\n如不想启用此功能，可前往 设置>游戏设置 内关闭";
                            dialog.PrimaryButtonText = "确定";

                            dialog.ShowAsync();
                        });
                        process.WaitForExit();
                        break;
                    case "start":
                        if (temp.Length > 1)
                        {
                            Process processss = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            if (Command.Contains("Args"))
                            {
                                int argsindex = Command.IndexOf("Args");
                                startInfo.FileName = "cmd.exe";
                                try
                                {
                                    startInfo.Arguments = "/c " + Command.Replace(" Args ", " ");
                                }
                                catch (Exception ex) { }
                            }
                            else
                            {
                                startInfo.FileName = "cmd.exe";
                                if (Command.Contains("\""))
                                {
                                    startInfo.Arguments = "/c " + Command.Replace("start ", "");
                                }
                                else
                                {
                                    startInfo.Arguments = "/c " + Command;
                                }
                            }

                            startInfo.CreateNoWindow = true;
                            startInfo.RedirectStandardOutput = true;
                            startInfo.UseShellExecute = false;
                            processss.StartInfo = startInfo;
                            processss.Start();
                        }
                        break;
                }
            }
        }
    }
}
