using HeerDev.MLRExtension.Authenticator.Model;
using iNKORE.UI.WPF.Modern.Controls;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Round_Minecraft_Launcher.Cs.Launcher.Authenticator;
using Round_Minecraft_Launcher.Cs.Launcher.JavaEdtion;
using System;
using System.Collections.Generic;
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
using Page = System.Windows.Controls.Page;

namespace Round_Minecraft_Launcher.Pages.Setting_SubPages.UserSetting_SubPages.Login_SubPages
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Page
    {
        public static ContentDialog ContentDialogs;
        public Login(string token,ContentDialog dialog)
        {
            InitializeComponent();
            logss(token);
            ContentDialogs = dialog;


        }
        private static async Task<int> logss(string token)
        {
            //MinecraftAccessToknLibrary.GetMinecraftToken getMinecraftToken = new MinecraftAccessToknLibrary.GetMinecraftToken();
            //GetUserUUIDRoot getUserUUIDRoot = await getMinecraftToken.GetMinceraftUserInfo("b91b99f8-e1e9-4da6-966c-bb50a7bb6c47", "M.C541_SN1.2.U.bd134a8f-840b-200b-aa84-bab53ff708b3");
            //Console.WriteLine(getUserUUIDRoot.skins);
            ContentDialogs.Hide();
            return 0;
        }
        /*private async void en()
        {
            await Task.Run(() =>
            {
                *//*Cs.Launcher.JavaEdtion.Login.GetCode("b91b99f8-e1e9-4da6-966c-bb50a7bb6c47", "https://login.live.com/oauth20_desktop.srf");
                var v = Cs.Launcher.JavaEdtion.Login.GetAllinfo();*//*

//System.Windows.MessageBox.Show(v.Name + "|" + v.Uuid + "|" + v.Token);
            });
        }*/
        /*private void qiyonglogin(string token)
        {
            if (token != null)
            {
                this.Dispatcher.Invoke(() =>
                {
                    JD.Content = "正在登录...10%";
                });

                string clientId = "b91b99f8-e1e9-4da6-966c-bb50a7bb6c47";
                string authCode = token;
                string redirectUri = "https://login.live.com/oauth20_desktop.srf";
                string scope = "XboxLive.signin offline_access";

                // 构建请求的URL
                string tokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";

                // 创建HttpClient实例
                using (var client = new HttpClient())
                {
                    // 创建请求体
                    var postData = new Dictionary<string, string>
                            {
                                { "client_id", clientId },
                                { "code", authCode },
                                { "grant_type", "authorization_code" },
                                { "redirect_uri", redirectUri },
                                { "scope", scope }
                            };
                    var content = new FormUrlEncodedContent(postData);

                    var response = client.PostAsync(tokenEndpoint, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // 获取响应内容
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        JObject tokenResponse = JObject.Parse(jsonResponse);

                        // 输出令牌信息
                        Console.WriteLine("Token Type: " + tokenResponse["token_type"]);
                        Console.WriteLine("Scope: " + tokenResponse["scope"]);
                        Console.WriteLine("Expires In: " + tokenResponse["expires_in"]);
                        Console.WriteLine("Access Token: " + tokenResponse["access_token"]);
                        Console.WriteLine("Refresh Token: " + tokenResponse["refresh_token"]);
                        this.Dispatcher.Invoke(() =>
                        {
                            JD.Content = "正在登录...20%";
                        });

                        //Xbox Live 验证
                        string accessToken = (string)tokenResponse["access_token"];
                        string rpsTicket = $"d={accessToken}";

                        // 创建请求的JSON体
                        string jsonRequestBody = JsonConvert.SerializeObject(new
                        {
                            Properties = new
                            {
                                AuthMethod = "RPS",
                                SiteName = "user.auth.xboxlive.com",
                                RpsTicket = rpsTicket
                            },
                            RelyingParty = "http://auth.xboxlive.com",
                            TokenType = "JWT"
                        });

                        using (var client_jwt = new HttpClient())
                        {
                            // 设置请求头
                            client_jwt.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            // 创建请求内容
                            var content_jwt = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                            // 发送POST请求
                            HttpResponseMessage response_jwt = client_jwt.PostAsync("https://user.auth.xboxlive.com/user/authenticate", content_jwt).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                // 获取响应内容
                                string jsonResponse_jwt = response_jwt.Content.ReadAsStringAsync().Result;
                                dynamic tokenResponse_jwt = JsonConvert.DeserializeObject(jsonResponse_jwt);

                                // 输出Xbox Live令牌和uhs
                                Console.WriteLine("Xbox Live Token: " + tokenResponse_jwt.Token);
                                if (tokenResponse_jwt.DisplayClaims.xui != null && tokenResponse_jwt.DisplayClaims.xui.Count > 0)
                                {
                                    Console.WriteLine("User Hash (uhs): " + tokenResponse_jwt.DisplayClaims.xui[0].uhs);
                                    this.Dispatcher.Invoke(() =>
                                    {
                                        JD.Content = "正在登录...30%";
                                    });
                                }
                                this.Dispatcher.Invoke(() =>
                                {
                                    JD.Content = "正在登录...40%";
                                });



                                //XSTS验证
                                //XSTS((string)tokenResponse_jwt.DisplayClaims.xui[0].uhs, (string)tokenResponse_jwt.Token);
                            }
                            else
                            {
                                Console.WriteLine("Error: " + response_jwt.StatusCode);
                                Console.WriteLine(response_jwt.Content.ReadAsStringAsync().Result);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode);
                    }
                }
            }
            else
            {
                //dialog.Dispatcher.Invoke(() =>
                //{
                    //dialog.Hide();
                //});
            }
        }

        *//*static async Task<string> XSTS(string userHashs, string xstsTokens)
        {

            // 创建HttpClient实例
            using (var client = new HttpClient())
            {
                *//*string authHeader = $"XBL3.0 x={userHashs};{xstsTokens}";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authHeader);

                string requestUrl = "https://xsts.auth.xboxlive.com/xsts/authorize"; // 修改为正确的端点

                HttpResponseMessage response = await client.PostAsync(requestUrl, new StringContent(""));

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse_XSTS = await response.Content.ReadAsStringAsync();
                    JObject tokenResponse_XSTS = JObject.Parse(jsonResponse_XSTS);

                    // 确保"Token"存在
                    if (tokenResponse_XSTS["Token"] != null)
                    {
                        string xstsTokenNew = tokenResponse_XSTS["Token"].ToString();

                        string uhs = tokenResponse_XSTS["DisplayClaims"]["xui"][0]["uhs"].ToString();

                        // 第1步：获取Minecraft访问令牌
                        string minecraftAccessToken = GetMinecraftAccessToken(uhs, xstsTokenNew).Result;

                        if (!string.IsNullOrEmpty(minecraftAccessToken))
                        {
                            // 第2步：检查游戏拥有情况
                            bool hasMinecraft = CheckMinecraftOwnership(minecraftAccessToken).Result;

                            if (hasMinecraft)
                            {
                                // 第3步：获取玩家UUID
                                string playerUUID = GetPlayerUUID(minecraftAccessToken).Result;

                                if (!string.IsNullOrEmpty(playerUUID))
                                {
                                    Console.WriteLine("玩家UUID: " + playerUUID);
                                }
                                else
                                {
                                    Console.WriteLine("无法获取玩家UUID。");
                                }
                            }
                            else
                            {
                                Console.WriteLine("账号没有拥有Minecraft。");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }*/

               /* string minecraftAccessToken = GetMinecraftAccessToken(userHashs, Cs.Launcher.JavaEdtion.Login.GetXSTSToken()).Result;

                if (!string.IsNullOrEmpty(minecraftAccessToken))
                {
                    // 第2步：检查游戏拥有情况
                    bool hasMinecraft = CheckMinecraftOwnership(minecraftAccessToken).Result;

                    if (hasMinecraft)
                    {
                        // 第3步：获取玩家UUID
                        string playerUUID = GetPlayerUUID(minecraftAccessToken).Result;

                        if (!string.IsNullOrEmpty(playerUUID))
                        {
                            Console.WriteLine("玩家UUID: " + playerUUID);
                            return playerUUID;
                        }
                        else
                        {
                            Console.WriteLine("无法获取玩家UUID。");
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine("账号没有拥有Minecraft。");
                        return null;
                    }
                }
                else
                {
                    return null;
                }*//*
            }
        }*//*

        static async System.Threading.Tasks.Task<string> GetMinecraftAccessToken(string uhs, string xstsToken)
        {
            string identityToken = $"XBL3.0 x={uhs};{xstsToken}";
            string jsonBody = JsonConvert.SerializeObject(new { identityToken = identityToken });

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.minecraftservices.com/authentication/login_with_xbox", content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject tokenResponse = JObject.Parse(jsonResponse);
                    return tokenResponse["access_token"].ToString();
                }

                return null;
            }
        }

        static async System.Threading.Tasks.Task<bool> CheckMinecraftOwnership(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync("https://api.minecraftservices.com/entitlements/mcstore");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject entitlementsResponse = JObject.Parse(jsonResponse);
                    return entitlementsResponse["items"].Count() > 0;
                }

                return false;
            }
        }

        static async System.Threading.Tasks.Task<string> GetPlayerUUID(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await client.GetAsync("https://api.minecraftservices.com/minecraft/profile");

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject profileResponse = JObject.Parse(jsonResponse);
                    return profileResponse["id"].ToString();
                }

                return null;
            }
        }*/
    }
}
