using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.Cs.Launcher.BedrockEdition
{
    internal class Load_Version
    {
        public static string Get_URL(string url)
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

        public static List<string> Get_Version_List() {
            string fileContent = Get_URL("https://gitee.com/minecraftyjq/round-minecraft-launcher-update/raw/master/Bedrock/versions_ray.json").Replace("[[", "").Replace("]]", "").Replace("\"", "");
            //https://www.raythnetwork.co.uk/versions.php?type=json
            //https://gitee.com/minecraftyjq/round-minecraft-launcher-bedrock-versions/raw/master/versions.json.min
            //https://gitee.com/minecraftyjq/round-minecraft-launcher-bedrock-versions/raw/master/versions_ray.json
            //https://gitee.com/minecraftyjq/round-minecraft-launcher-update/raw/master/Bedrock/versions_ray.json

            // 假设数据是由大括号包围，并且每个条目由逗号分隔
            string[] entries = fileContent.Split("],[");

            List<string> result = new List<string>();
            // 遍历每个条目
            foreach (var entry in entries)
            {
                
                result.Add(entry);

                // 输出结果
                Console.WriteLine($"Version: {entry}");
            }

            return result;
        }
    }
}
