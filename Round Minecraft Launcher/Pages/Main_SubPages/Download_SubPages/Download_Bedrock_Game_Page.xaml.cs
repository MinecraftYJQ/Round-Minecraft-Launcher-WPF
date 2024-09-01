using MCLauncher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
using System.Xml.Linq;
using System.Xml;
using Round_Minecraft_Launcher.Cs;
using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Cs.Launcher.BedrockEdition;
using System.Text.RegularExpressions;
using System.IO.Compression;

namespace Round_Minecraft_Launcher.Pages.Main_SubPages.Download_SubPages
{
    /// <summary>
    /// Download_Bedrock_Game_Page.xaml 的交互逻辑
    /// </summary>
    public partial class Download_Bedrock_Game_Page : System.Windows.Controls.Page
    {
        private WUProtocol protocol = new WUProtocol();
        private HttpClient client = new HttpClient();
        private XDocument PostXmlAsync(string url, XDocument data)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = false, OmitXmlDeclaration = true }))
                {
                    data.Save(xmlWriter);
                }
                request.Content = new StringContent(stringWriter.ToString(), Encoding.UTF8, "application/soap+xml");
            }
            using (var resp = client.SendAsync(request).Result)
            {
                string str = resp.Content.ReadAsStringAsync().Result;
                return XDocument.Parse(str);
            }
        }
        private async Task<string> GetDownloadUrl(string updateIdentity, string revisionNumber)
        {
            Console.WriteLine("uuid=" + updateIdentity);
            Console.WriteLine("revisionNumber=" + revisionNumber);
            XDocument result = PostXmlAsync(protocol.GetDownloadUrl(),
                protocol.BuildDownloadRequest(updateIdentity, revisionNumber));
            Debug.WriteLine($"GetDownloadUrl() response for updateIdentity {updateIdentity}, revision {revisionNumber}:\n{result.ToString()}");
            foreach (string s in protocol.ExtractDownloadResponseUrls(result))
            {
                if (s.StartsWith("http://tlu.dl.delivery.mp.microsoft.com/"))
                    return s;
            }
            return null;
        }

        string path;
        public void DownloadFile(string URL, string version, System.Windows.Controls.ProgressBar prog)
        {
            Directory.CreateDirectory(".minecraft\\temp");
            float percent = 0;
            try
            {

                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();

                // 尝试从响应头中获取文件名
                string contentDisposition = myrp.Headers["Content-Disposition"];
                string filename = null;
                if (!string.IsNullOrEmpty(contentDisposition))
                {
                    var match = Regex.Match(contentDisposition, @"filename=([^;]+)");
                    if (match.Success)
                    {
                        filename = match.Groups[1].Value.Trim('\"');
                    }
                }

                // 如果没有从响应头中获取到文件名，可以设置一个默认文件名
                if (string.IsNullOrEmpty(filename))
                {
                    filename = "whatthis"; // 替换为默认文件名和扩展名
                }
                filename = filename.Replace(".appx", "").Replace(".Appx", "").Replace(", attachment", "");

                File.WriteAllText("RMCL\\bedrock", System.IO.Path.GetDirectoryName(filename));

                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Dispatcher.Invoke(() =>
                    {
                        prog.Maximum = (int)totalBytes;
                    });
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(".minecraft\\temp\\"+ version+".appx", System.IO.FileMode.Create);
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

                string zipPath = ".minecraft\\temp\\" + version + ".appx";
                // 指定解压到的目录
                string extractPath = ".minecraft\\bedrock\\"+version;

                // 使用ZipFile.ExtractToDirectory方法解压ZIP文件
                ZipFile.ExtractToDirectory(zipPath, extractPath);

                File.Delete(extractPath + "\\AppxSignature.p7x");
                File.WriteAllText(extractPath + "\\versionname", filename);
            }
            catch (System.Exception ex)
            {
                // 处理异常
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        public Download_Bedrock_Game_Page(string uuid,string version)
        {
            InitializeComponent();
            Names.Content = "正在下载 Minecraft Bedrock " + version;
            Task.Run(() => {
                try
                {
                    string url = GetDownloadUrl(uuid, "1").Result;
                    if (url != null)
                    {
                        p1.Dispatcher.Invoke(() =>
                        {
                            p1.Value = p1.Maximum;
                        });

                        DownloadFile(url, version, p2);

                        p3.Dispatcher.Invoke(() =>
                        {
                            p3.IsIndeterminate = true;
                        });
                        
                        Launcher.Install(version);
                        File.WriteAllText("RMCL\\bedrock", System.IO.Path.GetDirectoryName(path));

                        GL.Frame.Dispatcher.Invoke(() =>
                        {
                            GL.Frame.Navigate(GL.temppage);
                        });
                    }
                    else
                    {
                        GL.Frame.Dispatcher.Invoke(() =>
                        {
                            GL.Frame.Navigate(GL.temppage);
                        });
                    }
                }
                catch(Exception ex)
                {
                    GL.Frame.Dispatcher.Invoke(() => {
                        GL.Frame.Navigate(GL.temppage);
                        ContentDialog contentDialog = new ContentDialog();
                        contentDialog.Title = "下载错误";
                        contentDialog.Content = new Label
                        {
                            Content = "无法连接至服务器..."
                        };

                        contentDialog.PrimaryButtonText = "确定";
                        contentDialog.ShowAsync();
                    });
                }
            });
        }
    }
}
