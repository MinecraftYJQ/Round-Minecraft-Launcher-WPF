using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Cs;
using System.Diagnostics;
using System.Reflection;

namespace Round_Minecraft_Launcher.Pages.API
{
    /// <summary>
    /// Update.xaml 的交互逻辑
    /// </summary>
    public partial class Update : System.Windows.Controls.Page
    {
        public Update(string url,string version)
        {
            InitializeComponent();

            Task.Run(() =>
            {
                DownloadFile(url, version, p1);
            });
        }
        public void DownloadFile(string URL, string version, System.Windows.Controls.ProgressBar prog)
        {
            Debug.WriteLine(URL);
            Directory.CreateDirectory("RMCL\\temp");
            float percent = 0;
            System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
            System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();

            long totalBytes = myrp.ContentLength;
            if (prog != null)
            {
                prog.Dispatcher.Invoke(() =>
                {
                    prog.Maximum = (int)totalBytes;
                });
            }
            System.IO.Stream st = myrp.GetResponseStream();
            System.IO.Stream so = new System.IO.FileStream("RMCL\\temp\\" + version + ".exe", System.IO.FileMode.Create);
            long totalDownloadedByte = 0;
            byte[] by = new byte[1024];
            int osize = st.Read(by, 0, (int)by.Length);
            while (osize > 0)
            {
                totalDownloadedByte = osize + totalDownloadedByte;
                so.Write(by, 0, osize);
                prog.Dispatcher.Invoke(() =>
                {
                    prog.Value = (int)totalDownloadedByte;
                });
                osize = st.Read(by, 0, (int)by.Length);
            }
            so.Close();
            st.Close();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string programName = assembly.GetName().CultureName;

            Debug.WriteLine(programName);
            Process process = new Process();
            process.StartInfo.FileName = "powershell";//Copy-Item -Path $sourceFilePath -Destination $destinationFilePath -Force
            process.StartInfo.Arguments = $"/c echo 即将开始进行更新... ; echo 请勿关闭此窗口 ; Start-Sleep -Seconds 5 ; Remove-Item -Path \"{programName}.exe\" -Force ; Copy-Item -Path \"{"RMCL\\temp\\" + version + ".exe"}\" -Destination \"{programName.Replace(' ','_')}.exe\" -Force ; start \"{programName.Replace(' ', '_')}.exe\"";
            process.Start();

            Environment.Exit(0);
        }
    }
}
