using System;
using System.IO;
using System.Net;
using LanTutor.Database;
using Newtonsoft.Json.Linq;

namespace LanTutor.Services
{
    public class TonConnectListener
    {
        private readonly HttpListener listener = new HttpListener();

        public void Start()
        {
            listener.Prefixes.Add("http://localhost:5050/");
            listener.Start();
            listener.BeginGetContext(OnRequest, null);
        }

        private void OnRequest(IAsyncResult result)
        {
            var context = listener.EndGetContext(result);
            listener.BeginGetContext(OnRequest, null);
            if (context.Request.HttpMethod == "GET" && context.Request.Url.AbsolutePath == "/connect.html")
            {
                string htmlPath = Path.Combine(Environment.CurrentDirectory, "Resources", "connect.html");
                if (File.Exists(htmlPath))
                {
                    string html = File.ReadAllText(htmlPath);
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);
                    context.Response.ContentType = "text/html";
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    context.Response.StatusCode = 404;
                    using (var writer = new StreamWriter(context.Response.OutputStream))
                    {
                        writer.Write("connect.html not found.");
                    }
                }

                context.Response.Close();
                return;
            }

            if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/ton-auth")
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    string body = reader.ReadToEnd();
                    var json = JObject.Parse(body);
                    string walletAddress = json["walletAddress"]?.ToString();
                    Console.WriteLine("Received wallet address: " + walletAddress);

                    if (!string.IsNullOrEmpty(walletAddress))
                    {
                        var userService = new UserService(new LanTutorContext());
                        var user = userService.GetOrCreateUserByWallet(walletAddress);

                        ConfigurationService.SetCurrentUserId(user.UserId);
                    }
                }

                context.Response.StatusCode = 200;
                using (var writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.Write("OK");
                }
            }

            context.Response.Close();
        }
        /*
    private void OnRequest(IAsyncResult result)
        {
            var context = listener.EndGetContext(result);
            listener.BeginGetContext(OnRequest, null); // Keep listening

            var request = context.Request;
            if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/ton-auth")
            {
                using (var reader = new StreamReader(request.InputStream))
                {
                    string body = reader.ReadToEnd();
                    // Parse JSON: { walletAddress, signedMessage }
                    // Save walletAddress to UserSettings
                    // Replace hardcoded userId with wallet-based identity
                }

                context.Response.StatusCode = 200;
                using (var writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.Write("OK");
                }
            }

            context.Response.Close();
        }
    */}
}


