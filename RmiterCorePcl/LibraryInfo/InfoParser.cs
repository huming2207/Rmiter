using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Ivony.Html;
using Ivony.Html.Parser;

namespace RmiterCorePcl.LibraryInfo
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

            // Declare and prepare for the Jumony engine
            var jumonyParser = new JumonyParser();
            var htmlDoc = jumonyParser.Parse(infoRawHtml);

            // Grab all contents with "<td>" tags
            var tableContents = htmlDoc.Find("td");
            var pickedList = new List<string>();
            
            for(int index = 1; index <= 10; index++)
            {
                string tableContent = tableContents.ElementAt(index - 1).InnerText();

                // Have a look at the structure of the table and you'll know why.
                // Only the even-number-index content is the REAL time string.
                if(index % 2 == 0)
                {
                    pickedList.Add(tableContent);
                }
            }

            var infoResult = new OpeningHours()
            {
                BrunswickLibrary = pickedList[0],
                BundooraLibrary = pickedList[1],
                BundooraEastLibrary = pickedList[2],
                CarltonLibrary = pickedList[3],
                SwanstonLibrary = pickedList[4]
            };

            return infoResult;
        }
    }
}
