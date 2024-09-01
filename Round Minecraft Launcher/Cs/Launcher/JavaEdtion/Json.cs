using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion
{
    class Json
    {
        public class Library
        {
            //public string Name { get; set; }
            public Downloads Downloads { get; set; }
            //public List<Rule> Rules { get; set; }
        }

        public class Downloads
        {
            public Artifact Artifact { get; set; }
            public Classifiers Classifiers { get; set; }
        }
        public class CDownloads
        {
            public Client Client { get; set; }
        }
        public class Client
        {
            public string sha1 { get; set; }
            public string size { get; set; }
            public string url { get; set; }
        }
        public class Artifact
        {
            public string Path { get; set; }
            public string Sha1 { get; set; }
            //public long Size { get; set; }
            public string Url { get; set; }
        }

        public class Classifiers
        {
            // 假设 JSON 中的每个分类器都是一个具有相同结构的对象
            [JsonExtensionData] // 允许反序列化未知的属性
            public Dictionary<string, JToken> AdditionalProperties { get; set; }
        }

        public class Windows_N
        {
            public string Path { get; set; }
            public string Sha1 { get; set; }
            public long Size { get; set; }
            public string Url { get; set; }
        }

        public class Rule
        {
            public string Action { get; set; }
            public Os Os { get; set; }
        }

        public class Os
        {
            public string Name { get; set; }
            public string Arch { get; set; }
        }

        public class AssetsIndex
        {
            public string Id { get; set; }
            public string Sha1 { get; set; }
            public long Size { get; set; }
            public long TotalSize { get; set; }
            public string Url { get; set; }
        }

        public class RootObject
        {
            public AssetsIndex AssetIndex { get; set; }
            public CDownloads CDownloads { get; set; }
            public List<Library> Libraries { get; set; }
        }
    }
}
