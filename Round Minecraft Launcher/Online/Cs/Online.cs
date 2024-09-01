using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Round.Online.Luncher.Cs
{
    class Online
    {
        public static void Open_Online(string Node)
        {
            Directory.CreateDirectory("bin");
            try
            {
                File.WriteAllBytes("bin\\openp2p.exe", global::Round_Minecraft_Launcher.Properties.Resources.openp2p);
            }
            catch
            {
                End_Online();
            }
            string json = "" +
                        "{\r\n" +
                        "  \"network\": {\r\n" +
                        "    \"Token\": 17190022896174664900,\r\n" +
                       $"    \"Node\": \"{Node}\",\r\n" +
                        "    \"User\": \"MinecraftYJQ_\",\r\n" +
                        "    \"ShareBandwidth\": 10,\r\n" +
                        "    \"ServerHost\": \"api.openp2p.cn\",\r\n" +
                        "    \"ServerPort\": 27183,\r\n" +
                        "    \"UDPPort1\": 27182,\r\n" +
                        "    \"UDPPort2\": 27183,\r\n" +
                        "    \"TCPPort\": 50448\r\n" +
                        "  },\r\n" +
                        "  \"apps\": null,\r\n" +
                        "  \"LogLevel\": 2\r\n" +
                        "}";

            File.WriteAllText("bin\\config.json", json);

            // 创建进程对象
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "bin\\openp2p.exe"; // 控制台应用路径
            startInfo.RedirectStandardOutput = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            startInfo.StandardErrorEncoding = Encoding.UTF8;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true; // 不显示新的命令行窗口

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void End_Online() {
            Process[] process = Process.GetProcessesByName("openp2p");
            foreach (Process processItem in process)
            {
                processItem.Kill();
            }
        }

        public static void Open_Online_C(string Node)
        {
            Directory.CreateDirectory("bin");
            try
            {
                File.WriteAllBytes("bin\\openp2p.exe", global::Round_Minecraft_Launcher.Properties.Resources.openp2p);
            }
            catch
            {
                End_Online();
            }

            byte[] base64DecodedBytes = Convert.FromBase64String(Node.Replace("ROL-", ""));
            string originalString = System.Text.Encoding.UTF8.GetString(base64DecodedBytes);

            //iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(originalString, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            string[] okys = originalString.Split('|');
            string json = "" +
                        "{\r\n" +
                        "  \"network\": {\r\n" +
                        "    \"Token\": 17190022896174664900,\r\n" +
                       $"    \"Node\": \"{Get_Uid.Get_Uid_Func(int.Parse(okys[2]), okys[0])}\",\r\n" +
                        "    \"User\": \"MinecraftYJQ_\",\r\n" +
                        "    \"ShareBandwidth\": 10,\r\n" +
                        "    \"ServerHost\": \"api.openp2p.cn\",\r\n" +
                        "    \"ServerPort\": 27183,\r\n" +
                        "    \"UDPPort1\": 27182,\r\n" +
                        "    \"UDPPort2\": 27183,\r\n" +
                        "    \"TCPPort\": 50448\r\n" +
                        "   },\r\n" +
                        "  \"apps\": [\r\n" +
                        "    {\r\n" +
                        "      \"AppName\": \"Minecraft Server\",\r\n" +
                        "      \"Protocol\": \"tcp\",\r\n" +
                        "      \"UnderlayProtocol\": \"\",\r\n" +
                        "      \"Whitelist\": \"\",\r\n " +
                       $"     \"SrcPort\": {okys[2]},\r\n" +
                       $"      \"PeerNode\": \"{Node}\",\r\n" +
                       $"      \"DstPort\": {okys[2]},\r\n" +
                        "      \"DstHost\": \"localhost\",\r\n" +
                        "      \"PeerUser\": \"\",\r\n" +
                        "      \"RelayNode\": \"\",\r\n" +
                        "      \"ForceRelay\": 0,\r\n" +
                        "      \"Enabled\": 1\r\n" +
                        "    }\r\n" +
                        "  ],\r\n" +
                        "  \"LogLevel\": 2"+
                        "}";

            File.WriteAllText("bin\\config.json", json);

            // 创建进程对象
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "bin\\openp2p.exe"; // 控制台应用路径
            startInfo.RedirectStandardOutput = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            startInfo.StandardErrorEncoding = Encoding.UTF8;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true; // 不显示新的命令行窗口

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
