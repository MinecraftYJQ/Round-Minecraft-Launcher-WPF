using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion.Json;
using System.Net.Http;
using System.Windows.Controls;
using Round_Minecraft_Launcher.Pages;
using iNKORE.UI.WPF.Modern.Controls;
using Round_Minecraft_Launcher.Cs.API.DownloadTask;
using System.Diagnostics;
using Round_Minecraft_Launcher.Cs.API.MessageSystem;

namespace Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion
{
    class Download
    {
        
        private static string geturl(string url)
        {
            using (var getjson = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpRequestMessage = getjson.GetAsync(url).Result;
                    httpRequestMessage.EnsureSuccessStatusCode();
                    string get_jsonsss = httpRequestMessage.Content.ReadAsStringAsync().Result;
                    return get_jsonsss;
                }
                catch { return "dont"; }
            }
        }
        private static byte[] getfile(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();

                    // 读取响应内容
                    byte[] fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                    return fileBytes;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static void Download_Game(string version, System.Windows.Controls.ProgressBar progressBar, System.Windows.Controls.ProgressBar progressBar1, string uuid, bool launch = false)
        {
            uuids = uuid;
            if (launch)
            {
                if (!File.Exists($".minecraft\\versions\\{version}\\{version}.json"))
                {
                    Download_Game(version, progressBar, progressBar1, uuid, false);
                }
                try
                {
                    down(File.ReadAllText($".minecraft\\versions\\{version}\\{version}.json"), version, progressBar, progressBar1, launch);
                }
                catch
                {
                    NewMessage.Show("无法连接至服务器", "下载错误",3);
                }
            }
            else
            {
                Task.Run(() =>
                {
                    Directory.CreateDirectory(".minecraft\\assets\\indexes");
                    Directory.CreateDirectory($".minecraft\\versions\\{version}");
                    string url = "http://launchermeta.mojang.com/mc/game/version_manifest_v2.json";

                    string responseBody = GL.Download_Game_List_Json;
                    JObject manifest = JObject.Parse(responseBody);
                    JArray versions = (JArray)manifest["versions"];

                    foreach (JObject version11 in versions)
                    {
                        if ((string)version11["type"] == "release")
                        {
                            string releaseId = (string)version11["id"];
                            if (releaseId == version)
                            {
                                string version_json_url = (string)version11["url"];
                                Console.WriteLine(version_json_url);

                                try
                                {
                                    down(geturl(version_json_url), version, progressBar, progressBar1, launch);
                                }
                                catch
                                {
                                    NewMessage.Show("无法连接至服务器", "下载错误", 3);
                                }
                            }
                        }
                    }
                });
            }
        }
        static string uuids;

        private static void down(string get_jsonsss,string version, System.Windows.Controls.ProgressBar progressBar, System.Windows.Controls.ProgressBar progressBar1,bool launch)
        {
            Debug.WriteLine(version);
            //string get_jsonsss = geturl(version_json_url);
            Directory.CreateDirectory($".minecraft\\versions\\{version}");
            File.WriteAllText($".minecraft\\versions\\{version}\\{version}.json", get_jsonsss);
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(get_jsonsss);

            JObject centeeeee = JObject.Parse(get_jsonsss);
            JToken download_cenereeeee = centeeeee["downloads"]["client"];
            if (!File.Exists($".minecraft\\versions\\{version}\\{version}.jar"))
            {
                File.WriteAllBytes($".minecraft\\versions\\{version}\\{version}.jar", getfile(download_cenereeeee["url"].ToString()));
                Console.WriteLine($"客户端文件下载完毕");
            }
            else
            {
                Console.WriteLine($"客户端文件存在");
            }
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Launching Ready/Get ID]: {rootObject.AssetIndex.Id}");
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Launching Ready/Get URL]: {rootObject.AssetIndex.Url}");

            string assets_json = "";
            if (launch)
            {
                if (!File.Exists($".minecraft\\assets\\indexes\\{rootObject.AssetIndex.Id}.json"))
                {
                    assets_json=geturl(rootObject.AssetIndex.Url);
                }
                else
                {
                    assets_json = File.ReadAllText($".minecraft\\assets\\indexes\\{rootObject.AssetIndex.Id}.json");
                }
            }
            else
            {
                assets_json=geturl(rootObject.AssetIndex.Url);
            }
            File.WriteAllText($".minecraft\\assets\\indexes\\{rootObject.AssetIndex.Id}.json", assets_json);

            JObject jsonObject = JObject.Parse(assets_json);
            JObject objects = (JObject)jsonObject["objects"];
            int js_close = 0;
            int num = objects.Properties().Count() - 1;
            int js_num = 0;
            Task.Run(() =>
            {
                progressBar.Dispatcher.Invoke(() =>
                {
                    progressBar.Maximum = num + 1;
                    progressBar.Value = 1;
                });
                foreach (JProperty item in objects.Properties())
                {
                    if (!File.Exists($".minecraft\\assets\\objects\\{(string)item.Value["hash"].ToString().Substring(0, 2)}\\{(string)item.Value["hash"]}"))
                    {
                        Task.Run(() =>
                        {
                            string download_url = $"https://resources.download.minecraft.net/{(string)item.Value["hash"].ToString().Substring(0, 2)}/{(string)item.Value["hash"]}";
                            if (!Directory.Exists($".minecraft\\assets\\objects\\{(string)item.Value["hash"].ToString().Substring(0, 2)}"))
                            {
                                Directory.CreateDirectory($".minecraft\\assets\\objects\\{(string)item.Value["hash"].ToString().Substring(0, 2)}");
                            }
                            byte[] bytes = getfile(download_url);
                            if (bytes != null)
                            {
                                File.WriteAllBytes($".minecraft\\assets\\objects\\{(string)item.Value["hash"].ToString().Substring(0, 2)}\\{(string)item.Value["hash"]}", bytes);
                                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][Download Game Files][{js_num}/{num}]{(string)item.Value["hash"]} 下载完毕");
                                js_num++;
                            }
                            else
                            {
                                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][Download Game Files][{js_num}/{num}]{(string)item.Value["hash"]} 下载失败");
                                js_num++;
                            }
                        });
                        progressBar.Dispatcher.Invoke(new Action(() =>
                        {
                            progressBar.Value = js_num;
                        }));
                        Thread.Sleep(100);
                    }
                    else
                    {
                        Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][Download Game Files][{js_num}/{num}]{(string)item.Value["hash"]} 文件已存在");
                        js_num++;
                        progressBar.Dispatcher.Invoke(new Action(() =>
                        {
                            progressBar.Value = js_num;
                        }));
                    }
                }
                js_close++;
                if (js_close == 2)
                {
                    GL.Frame.Dispatcher.Invoke(() =>
                    {
                        GL.Frame.Navigate(GL.temppage);
                        if (launch)
                        {
                            Laun(version);
                        }
                    });
                }
            });
            Task.Run(() =>
            {
                int libnum = rootObject.Libraries.Count();
                int jss = 0;
                foreach (var lib in rootObject.Libraries)
                {
                    if (lib.Downloads.Artifact != null)
                    {
                        if (lib.Downloads.Artifact.Path.Contains("macos")
                            && lib.Downloads.Artifact.Path.Contains("linux")
                            && lib.Downloads.Artifact.Path.Contains("arm64")
                            && lib.Downloads.Artifact.Path.Contains("x86"))
                        {
                            libnum--;
                        }
                    }
                    if (lib.Downloads.Classifiers != null && lib.Downloads.Classifiers.AdditionalProperties != null)
                    {
                        if (lib.Downloads.Classifiers.AdditionalProperties.TryGetValue("natives-windows", out JToken windowsToken))
                        {
                            libnum++;
                        }
                    }
                }
                progressBar1.Dispatcher.Invoke(() =>
                {
                    progressBar1.Maximum = libnum - 1;
                    progressBar1.Value = 0;
                });
                foreach (var library in rootObject.Libraries)
                {
                    if (library.Downloads.Artifact != null)
                    {
                        if (!File.Exists($".minecraft\\libraries\\{library.Downloads.Artifact.Path}"))
                        {
                            string[] temp = library.Downloads.Artifact.Path.Split('/');
                            string file_save_path = "";
                            for (int i = 0; i <= temp.Length - 2; i++)
                            {
                                file_save_path += temp[i] + "\\";
                            }
                            Directory.CreateDirectory(".minecraft\\libraries\\" + file_save_path);
                            Task.Run(() =>
                            {
                                try
                                {
                                    File.WriteAllBytes($".minecraft\\libraries\\{library.Downloads.Artifact.Path}", getfile(library.Downloads.Artifact.Url));
                                }
                                catch { }
                                jss++;
                                Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Download Library Jar][{jss}/{libnum}]: .minecraft\\libraries\\{library.Downloads.Artifact.Path} 下载成功");
                            });
                            Thread.Sleep(100);
                        }
                        else
                        {
                            jss++;
                            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Download Library Jar][{jss}/{libnum}]: .minecraft\\libraries\\{library.Downloads.Artifact.Path} 文件已存在");
                        }
                        progressBar1.Dispatcher.Invoke(() =>
                        {
                            progressBar1.Value = jss;
                        });
                    }


                    if (library.Downloads.Classifiers != null && library.Downloads.Classifiers.AdditionalProperties != null)
                    {
                        // 尝试获取 "natives-windows" 分类器
                        if (library.Downloads.Classifiers.AdditionalProperties.TryGetValue("natives-windows", out JToken windowsToken))
                        {
                            // 将 JToken 转换为 Artifact 类型
                            var nativesWindowsArtifact = windowsToken.ToObject<Windows_N>();
                            if (nativesWindowsArtifact != null)
                            {
                                if (!File.Exists($".minecraft\\libraries\\{nativesWindowsArtifact.Path}"))
                                {
                                    string[] temp = nativesWindowsArtifact.Path.Split('/');
                                    string file_save_path = "";
                                    for (int i = 0; i <= temp.Length - 2; i++)
                                    {
                                        file_save_path += temp[i] + "\\";
                                    }
                                    Directory.CreateDirectory(".minecraft\\libraries\\" + file_save_path);
                                    File.WriteAllBytes($".minecraft\\libraries\\{nativesWindowsArtifact.Path}", getfile(nativesWindowsArtifact.Url));
                                    jss++;
                                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Download Library Jar][{jss}/{libnum}]: .minecraft\\libraries\\{nativesWindowsArtifact.Path} 下载成功");
                                    Thread.Sleep(100);
                                }
                                else
                                {
                                    jss++;
                                    Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Download Library Jar][{jss}/{libnum}]: .minecraft\\libraries\\{nativesWindowsArtifact.Path} 文件已存在");
                                }

                                progressBar1.Dispatcher.Invoke(() =>
                                {
                                    progressBar1.Value = jss;
                                });
                            }
                        }
                    }
                }
                js_close++;
                if (js_close == 2)
                {
                    GL.Frame.Dispatcher.Invoke(() =>
                    {
                        GL.Frame.Navigate(GL.temppage);
                        if (launch)
                        {
                            Laun(version);
                        }
                    });
                }
            });
        }
        private static void Laun(string vers)
        {
            Task.Run(() =>
            {
                DelDownloadTask.DelItemByUUID(uuids);
                Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion.Launch.Launcher_Game(vers, File.ReadAllText("RMCL\\Java"), File.ReadAllText("RMCL\\Name"));
            });
        }
    }
}
