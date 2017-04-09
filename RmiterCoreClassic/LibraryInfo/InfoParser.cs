using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;
using RmiterCoreClassic;

namespace RmiterCoreClassic.LibraryInfo
{
    public class InfoParser
    {
        private HttpClient _LibraryHttpClient(string baseUrl = "http://www.lib.rmit.edu.au")
        {
            // HttpClient declaration
            var client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl),
            };

            client.DefaultRequestHeaders.Add("User-Agent", UserAgentGenerator.GenerateUserAgent());
            return client;
        }

        public async Task<OpeningHours> GetOpeningHours()
        {
            // Declare HttpClient
            var httpClient = _LibraryHttpClient();
            string infoRawHtml = await httpClient.GetStringAsync("/hours/today.php");

            // Workaround: when the library is closed, the </td> tag is missing,
            //   which causes the HAP engine cannot parse the HTML properly.
            // (i.e. they wrote "<td>CLOSED" instead of the "correct" one which is "<td>CLOSED</td>")
            if (infoRawHtml.Contains("<td>CLOSED") && !infoRawHtml.Contains("<td>CLOSED</td>"))
            {
                infoRawHtml.Replace("<td>CLOSED", "<td>CLOSED</td>");
            }

            // Prepare to parse via HAP engine
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(infoRawHtml);

            var infoResult = new OpeningHours()
            {
                CarltonLibrary = htmlDoc.DocumentNode.SelectNodes("//*[@id='iframecontent']")[0].SelectNodes("//tr[4]/td[2]")[0].InnerText,
                SwanstonLibrary = htmlDoc.DocumentNode.SelectNodes("//*[@id='iframecontent']")[0].SelectNodes("//tr[5]/td[2]")[0].InnerText,
                BrunswickLibrary = htmlDoc.DocumentNode.SelectNodes("//*[@id='iframecontent']")[0].SelectNodes("//tr[1]/td[2]")[0].InnerText,
                BundooraLibrary = htmlDoc.DocumentNode.SelectNodes("//*[@id='iframecontent']")[0].SelectNodes("//tr[2]/td[2]")[0].InnerText,
                BundooraEastLibrary = htmlDoc.DocumentNode.SelectNodes("//*[@id='iframecontent']")[0].SelectNodes("//tr[3]/td[2]")[0].InnerText
            };

            return infoResult;
        }
    }
}
