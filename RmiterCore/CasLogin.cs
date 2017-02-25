using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Diagnostics;

namespace RmiterCore
{
    public class CasLogin
    {
        // CookieContainer declaration
        private CookieContainer cookieContainer = new CookieContainer();

        public async Task<bool> DoLogin(string username, string password, string pathStr = "/rmitcas/login")
        {
            var client = _CasHttpClient();

            var initialResult = await _GetInitialToken(client);
            // Add a fake User-Agent header temporarily in order to make it easier to debug
            // will remove or change to another (own) User-Agent later
            client.DefaultRequestHeaders.Add("User-Agent", _GenerateUserAgent());

            // Post content, including username, password, LT token and something else
            string postContent =
               string.Format("username={0}&password={1}&lt={2}&execution=e1s1&_eventId=submit&submit=Login",
                Uri.EscapeDataString(username), Uri.EscapeDataString(password), initialResult.LtToken);

            HttpContent httpContent = new StringContent(postContent, Encoding.UTF8, "application/x-www-form-urlencoded");
            var httpResponse = await client.PostAsync(initialResult.RedirectUrl, httpContent);
            string responseStr = await httpResponse.Content.ReadAsStringAsync();

            Debug.WriteLine("[DEBUG] Login command finished, response: " + responseStr);

            if (responseStr.Contains("Log In Successful") || responseStr.Contains("You have successfully logged into the Central Authentication Service"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<InitialToken> _GetInitialToken(HttpClient client, string pathStr = "/rmitcas/login")
        {
            // Add a fake User-Agent header temporarily in order to make it easier to debug
            // will remove or change to another (own) User-Agent later
            // client.DefaultRequestHeaders.Add("User-Agent", _GenerateUserAgent());

            // Get the HTML string
            var response = await client.GetAsync(pathStr); 
            string htmlStr = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("[DEBUG] Got initial cookie:: {0}", cookieContainer.GetCookieHeader(new Uri("https://sso-cas.rmit.edu.au")));


            // Load HTML string to HAP
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlStr);

            // Parse the Lt token via HAP
            var mainNode = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"fm1\"]");
            var inputNode = mainNode[0].SelectNodes("//input");
            string ltTokenStr = inputNode[2].Attributes["value"].Value;

            // Parse the redirection URL and JSESSION token via HAP
            string redirUrlStr = mainNode[0].Attributes["action"].Value;
            string jsessionId = redirUrlStr.Split('=')[1];

            // Tweak: add JSESSIONID to cookie to ensure it works later
           // cookieContainer.Add(new Uri("https://sso-cas.rmit.edu.au"), new Cookie("JSESSIONID", jsessionId));

            // Prepare the result and return
            return new InitialToken { JsessionToken = jsessionId, LtToken = ltTokenStr, RedirectUrl = redirUrlStr };
        }


        private string _GenerateUserAgent(string version = "1.0")
        {
            string userAgent = string.Format("RmiterCore/{0} (OS: {1}; CLR: {2})", version, Environment.OSVersion.ToString(), Environment.Version.ToString());
            Debug.WriteLine(userAgent);
            return userAgent;
        }

        private HttpClient _CasHttpClient(string baseUrl = "https://sso-cas.rmit.edu.au")
        {
            var clientHandler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
            };

            // HttpClient declaration
            var client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(baseUrl),
            };

            return client;
        }
    }
}
