using iNKORE.UI.WPF.Modern.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.Cs.Launcher.BedrockEdition
{
    internal class Launcher
    {
        static ContentDialog dialog;
        public static void Launch(string version) {
            GL.Frame.Dispatcher.Invoke(() => {
                dialog = new ContentDialog();
                dialog.Title = "启动基岩版...";
                dialog.Content = "请稍等...\n正在初始化基岩版\n如果出现打不开的情况，请确保系统的开发者模式为开启状态\n如果开启的版本不是当前所选版本，请尝试在设置内卸载Minecraft Bedrock\n\n!!此操作前请备份重要存档!!";
                dialog.ShowAsync();
            });

            Install(version);
            Process process = new Process();
            process.StartInfo.FileName = "powershell";
            process.StartInfo.Arguments = "/c start minecraft://";
            process.StartInfo.CreateNoWindow = true;

            process.Start();

            GL.Frame.Dispatcher.Invoke(() => {
                dialog.Hide();
            });
        }

        public static void Install(string version_path) {
            try
            {
                if (File.Exists("RMCL\\Bedrock"))
                {
                    string uninstall_version = File.ReadAllText(".minecraft\\bedrock\\" + File.ReadAllText("RMCL\\Bedrock") + "\\versionname");//.Replace(".appx", "").Replace(".Appx", "");//加上不影响结果

                    Process del = new Process();
                    ProcessStartInfo delinfo = new ProcessStartInfo();
                    delinfo.FileName = "powershell.exe";
                    delinfo.CreateNoWindow = true;
                    delinfo.Arguments = $"/c Remove-AppxPackage -Package {uninstall_version}\r\n";
                    del.StartInfo = delinfo;
                    del.Start();

                    del.WaitForExit();
                }
            }
            catch { }

            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "powershell.exe";
            info.Arguments = $"/c Add-AppxPackage -Register \".minecraft\\bedrock\\{version_path}\\AppxManifest.xml\" -ForceApplicationShutdown";
            process.StartInfo = info;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            process.WaitForExit();
            File.WriteAllText("RMCL\\Bedrock", version_path);
        }
    }
}
