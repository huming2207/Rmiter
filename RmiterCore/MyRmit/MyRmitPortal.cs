using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCore.MyRmit
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
                CookieContainer = cookieContainer
            };

            // HttpClient declaration
            var client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(baseUrl),
            };

            return client;
        }

        private async Task<T> _GetDataAsync<T>(string queryPath)
        {
            var client = _MyRmitHttpClient();
            string jsonStr = await client.GetStringAsync(queryPath);

            var jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonObject = JsonConvert.DeserializeObject<T>(jsonStr, jsonSettings);
            return jsonObject;
        }

        public async Task<MyRmit.Home> GetHomeMessages()
        {
            var result = await _GetDataAsync<MyRmit.Home>("/service/announcements");
            return result;
        }

        public async Task<MyRmit.ClassTimetable> GetCurrentClassTimetable()
        {
            // Get an Unix timestamp in milliseconds
            Int64 unixTimestamp = (Int64)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string path = string.Format("/service/myclasstimetable?time={0}", unixTimestamp.ToString());
            var result = await _GetDataAsync<MyRmit.ClassTimetable>(path);
            return result;
        }

        public async Task<MyRmit.ClassTimetable> GetSpecificClassTimetable(DateTime specificDate)
        {
            // Grab a current time string in format like "250217" (indicating 25 Feb, 2017)
            string dateStrInDigits = specificDate.ToString("ddmmyy");
            Int64 unixTimestamp = (Int64)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string path = string.Format("/service/myclasstimetable/{0}?time={1}", dateStrInDigits, unixTimestamp.ToString());
            var result = await _GetDataAsync<MyRmit.ClassTimetable>(path);
            return result;
        }

        public async Task<MyRmit.MyResult> GetMyResult()
        {
            var result = await _GetDataAsync<MyRmit.MyResult>("/service/myexamresults/new");
            return result;
        }

        public async Task<MyRmit.MyDetails> GetMyDetails()
        {
            Int64 unixTimestamp = (Int64)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            string path = string.Format("/service/mydetails?time={0}", unixTimestamp.ToString());
            var result = await _GetDataAsync<MyRmit.MyDetails>(path);
            return result;
        }

    }
}
