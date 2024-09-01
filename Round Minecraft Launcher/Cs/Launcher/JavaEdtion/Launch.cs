using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iNKORE.UI.WPF.Modern.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion
{
    class Launch
    {
        public static string Launcher_Game(string version_str,string java_path,string name)
        {
            string minecraft_games_path = $"{Directory.GetCurrentDirectory()}\\.minecraft";
            string version_path = $"{minecraft_games_path}\\versions";
            //string java_path = "C:\\Users\\ahadd\\AppData\\Roaming\\.minecraft\\runtime\\java\\zulu-openjdk-17\\windows-x64\\zulu-openjdk-17\\bin\\java.exe";

            string version_json = File.ReadAllText($"{version_path}\\{version_str}\\{version_str}.json");

            Json.RootObject rootObject = JsonConvert.DeserializeObject<Json.RootObject>(version_json);
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Launching Ready/Get ID]: {rootObject.AssetIndex.Id}");
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Launching Ready/Get URL]: {rootObject.AssetIndex.Url}");

            int num = rootObject.Libraries.Count;
            int test = 0;
            string cp = "";
            // 遍历libraries数组
            foreach (var library in rootObject.Libraries)
            {
                try
                {
                    if (library.Downloads.Artifact != null)
                    {
                        if (!library.Downloads.Artifact.Path.Contains("macos")
                    && !library.Downloads.Artifact.Path.Contains("linux")
                    && !library.Downloads.Artifact.Path.Contains("arm64")
                    && !library.Downloads.Artifact.Path.Contains("x86")
                    && !library.Downloads.Artifact.Path.Contains("3.2.1"))
                        {
                            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Launching Ready/Get Jar]: {library.Downloads.Artifact.Path}");
                            test++;
                            if (test == num)
                            {
                                cp += minecraft_games_path + "\\libraries\\" + library.Downloads.Artifact.Path;
                            }
                            else
                            {
                                cp += minecraft_games_path + "\\libraries\\" + library.Downloads.Artifact.Path + ";";
                            }
                        }
                        else
                        {
                            num--;
                        }
                    }

                }
                catch
                {
                    test++;
                }

                if (library.Downloads.Classifiers != null && library.Downloads.Classifiers.AdditionalProperties != null)
                {
                    // 尝试获取 "natives-windows" 分类器
                    if (library.Downloads.Classifiers.AdditionalProperties.TryGetValue("natives-windows", out JToken windowsToken))
                    {
                        // 将 JToken 转换为 Artifact 类型
                        var nativesWindowsArtifact = windowsToken.ToObject<Json.Windows_N>();
                        if (nativesWindowsArtifact != null)
                        {
                            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}][RMCL Launcher][Launching Ready/Get Natives Jar]: {nativesWindowsArtifact.Path}");
                            try
                            {
                                ZipFile.ExtractToDirectory(".minecraft\\libraries\\" + nativesWindowsArtifact.Path, $"D:\\User File\\Desktop\\MinecraftLauncher_Test\\MinecraftLauncher_Test\\bin\\Debug\\.minecraft\\versions\\{version_str}\\{version_str}-natives");
                            }
                            catch { }
                            if (Directory.Exists($"D:\\User File\\Desktop\\MinecraftLauncher_Test\\MinecraftLauncher_Test\\bin\\Debug\\.minecraft\\versions\\{version_str}\\{version_str}-natives\\META-INF"))
                            {
                                Directory.Delete($"D:\\User File\\Desktop\\MinecraftLauncher_Test\\MinecraftLauncher_Test\\bin\\Debug\\.minecraft\\versions\\{version_str}\\{version_str}-natives\\META-INF", true);
                            }
                        }
                    }
                }
            }

            //下面一段是删除无用文件
            try
            {
                foreach (string na in Directory.GetFiles($"D:\\User File\\Desktop\\MinecraftLauncher_Test\\MinecraftLauncher_Test\\bin\\Debug\\.minecraft\\versions\\{version_str}\\{version_str}-natives"))
                {
                    if (na.Contains(".git") || na.Contains(".sha1") || na.Contains(".x"))
                    {
                        File.Delete(na);
                    }
                }
            }
            catch { }
            //end
            try
            {
                Directory.CreateDirectory($"D:\\User File\\Desktop\\MinecraftLauncher_Test\\MinecraftLauncher_Test\\bin\\Debug\\.minecraft\\versions\\{version_str}\\{version_str}-natives");
            }
            catch { }
            string bat = $"@echo off\n\"{java_path}\" -Xmn491m -Xmx3276m -XX:+UseG1GC -XX:-UseAdaptiveSizePolicy -XX:-OmitStackTraceInFastThrow -Dfml.ignoreInvalidMinecraftCertificates=True -Dfml.ignorePatchDiscrepancies=True -Dlog4j2.formatMsgNoLookups=true -XX:HeapDumpPath=MojangTricksIntelDriversForPerformance_javaw.exe_minecraft.exe.heapdump -Djava.library.path=\"D:\\User File\\Desktop\\MinecraftLauncher_Test\\MinecraftLauncher_Test\\bin\\Debug\\.minecraft\\versions\\{version_str}\\{version_str}-natives\" -Djna.tmpdir=\"{minecraft_games_path}\\versions\\{version_str}\\{version_str}-natives\" -Dorg.lwjgl.system.SharedLibraryExtractPath=\"{minecraft_games_path}\\versions\\{version_str}\\{version_str}-natives\" -Dio.netty.native.workdir=\"{minecraft_games_path}\\versions\\{version_str}\\{version_str}-natives\" -Dminecraft.launcher.version=0 -cp \"{cp};{version_path}\\{version_str}\\{version_str}.jar\" net.minecraft.client.main.Main --username {name} --version {version_str} --gameDir \"{minecraft_games_path}\" --assetsDir \"{minecraft_games_path}\\assets\" --assetIndex {rootObject.AssetIndex.Id} --uuid 00000000000000000000000000000000 --accessToken 1145141919810 --clientId {version_str} --xuid WelcomeToTheMinecraft.ItisLaunchForRMCL --userType msa --versionType \"{Cs.GL.LauncherMessage}\" --width 870 --height 500".Replace('/', '\\');
            //Encoding.Default.GetString(Encoding.Default.GetBytes(bat))
            File.WriteAllText("RMCL\\Launcher.bat", Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(bat)));



            ProcessStartInfo psi = new ProcessStartInfo("RMCL\\Launcher.bat");
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            string logs = "";
            using (Process process = Process.Start(psi))
            {
                if (process != null)
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            Debug.WriteLine(e.Data.Replace("] [", "][RMCL Launcher]["));
                            logs += e.Data.Replace("] [", "][RMCL Launcher][");
                            if (e.Data.Contains("#RMCL")&& e.Data.Contains($"[CHAT]"))
                            {
                                Command.Start_Command(e.Data.Substring(e.Data.ToString().IndexOf("#RMCL") + 6, e.Data.Length - e.Data.ToString().IndexOf("#RMCL") - 6), process);
                            }
                        }
                    };
                    process.BeginOutputReadLine();

                    process.WaitForExit();
                }
            }
            return logs;
        }
    }
}
