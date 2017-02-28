using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Ivony.Html;
using Ivony.Html.Parser;
using Ivony.Parser;

namespace RmiterCore
{
    public class CasLogin
    {
        // CookieContainer declaration
        private CookieContainer cookieContainer = new CookieContainer();

        public async Task<CookieContainer> RunCasLogin(string username, string password, string pathStr = "/rmitcas/login")
        {
            var client = _CasHttpClient();

            // Get the Lt token and JSESSIONID token
            var initialResult = await _GetInitialToken(client);

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
                return cookieContainer;
            }
            else
            {
                return null;
            }
        }

        private async Task<InitialToken> _GetInitialToken(HttpClient client, string pathStr = "/rmitcas/login")
        {
            // Grab the page from RMIT CAS server
            var httpClient = client;
            string rawHtml = await httpClient.GetStringAsync(pathStr);

            // Prepare Jumony Parser
            var jumonyParser = new JumonyParser();
            var htmlDoc = jumonyParser.Parse(rawHtml);
            
            // Use Jumony Parser engine to parse those two tokens
            string ltTokenStr = htmlDoc.FindFirst("input[name=\"lt\"]").Attribute("value").Value();
            string redirectUrlStr = htmlDoc.FindFirst("form[id=\"fm1\"]").Attribute("action").Value();

            // Split JSESSIONID Token from   
            string jsessionId = redirectUrlStr.Split('=')[1];


            // Prepare the result and return
            return new InitialToken { JsessionToken = jsessionId, LtToken = ltTokenStr, RedirectUrl = redirectUrlStr };

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
