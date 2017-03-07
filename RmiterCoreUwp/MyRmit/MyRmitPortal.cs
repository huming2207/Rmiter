using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCoreUwp.MyRmit
{
    public class MyRmitPortal
    {
        private CookieContainer cookieContainer;

        public MyRmitPortal(CookieContainer cookieContainer)
        {
            this.cookieContainer = cookieContainer;
        }

        private HttpClient _MyRmitHttpClient(string baseUrl = "https://my.rmit.edu.au")
        {
            var clientHandler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer,
                AllowAutoRedirect = false
            };

            // HttpClient declaration
            var client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(baseUrl)
            };

            return client;
        }

        private async Task<T> _GetDataAsync<T>(string queryPath)
        {
            var httpResponse = await GetWithManualRedirectionAsync(queryPath);
            
            string jsonStr = await httpResponse.Content.ReadAsStringAsync();
            var jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonObject = JsonConvert.DeserializeObject<T>(jsonStr, jsonSettings);
            return jsonObject;
        }

        private async Task<HttpResponseMessage> GetWithManualRedirectionAsync(string queryPath, string baseUrl = "https://my.rmit.edu.au")
        {
            var clientHandler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer,

                // Disable the original shitty auto redirect function who won't treat cookies well.
                AllowAutoRedirect = false
            };

            var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(baseUrl),
            };

            var httpResponse = await httpClient.GetAsync(queryPath);

            // If it's not HTTP 302 response, return the HttpResponseMessage
            // If it is, then do a manual redirection (follow the content in HTTP headers)
            if (httpResponse.StatusCode != HttpStatusCode.Redirect)
            {
                return httpResponse;
            }
            else
            {
                // Get the redirect URL from redirection headers, get the first one (and the only one)
                //  then remove the base URL, prepare to run the GET again.
                string redirectQueryPath = httpResponse.Headers.GetValues("Location")
                    .First()
                    .Replace(baseUrl, "");

                return await GetWithManualRedirectionAsync(redirectQueryPath);
            }

        }


        public async Task<Home> GetHomeMessages()
        {
            var result = await _GetDataAsync<Home>("/service/announcements");
            return result;
        }

        public async Task<ClassTimetable> GetCurrentClassTimetable()
        {
            // Get an Unix timestamp in milliseconds
            Int64 unixTimestamp = (Int64)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string path = string.Format("/service/myclasstimetable?time={0}", unixTimestamp.ToString());
            var result = await _GetDataAsync<ClassTimetable>(path);
            return result;
        }

        public async Task<ClassTimetable> GetSpecificClassTimetable(DateTime specificDate)
        {
            // Grab a current time string in format like "250217" (indicating 25 Feb, 2017)
            string dateStrInDigits = specificDate.ToString("ddmmyy");
            Int64 unixTimestamp = (Int64)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string path = string.Format("/service/myclasstimetable/{0}?time={1}", dateStrInDigits, unixTimestamp.ToString());
            var result = await _GetDataAsync<ClassTimetable>(path);
            return result;
        }

        public async Task<MyResult> GetMyResult()
        {
            var result = await _GetDataAsync<MyResult>("/service/myexamresults/new");
            return result;
        }

        public async Task<MyDetails> GetMyDetails()
        {
            Int64 unixTimestamp = (Int64)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string path = string.Format("/service/mydetails?time={0}", unixTimestamp.ToString());
            var result = await _GetDataAsync<MyDetails>(path);
            return result;
        }

    }
}
