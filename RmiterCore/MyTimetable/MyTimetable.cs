using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RmiterCore.Errors;
using RmiterCore.MyTimetable.Timetable;

namespace RmiterCore.MyTimetable
{
    public class MyTimetable
    {
        private CasLoginResult casLoginResult;
        private string siteToken;
        
        public MyTimetable(CasLoginResult casLoginResult)
        {
            this.casLoginResult = casLoginResult;
            siteToken = RefreshSiteToken().Result;
        }

        public async Task<AllocatedTimetable> GetAllocatedTimetable()
        {
            // Stop if CAS login failed
            if (casLoginResult.CasError != CasLoginError.NoError || siteToken == null)
            {
                return new AllocatedTimetable() { IsSuccess = false };
            }
            
            var clientHandler = new HttpClientHandler()
            {
                CookieContainer = casLoginResult.CasCookieContainer
            };
            
            var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://mytimetable.rmit.edu.au")
            };

            var response = await httpClient.GetAsync(string.Format("/odd/rest/student/{0}/allocated/?ss={1}", 
                casLoginResult.UserName, siteToken));

            if (response.IsSuccessStatusCode)
            {
                var allocatedTimetableDict = JsonConvert.DeserializeObject<Dictionary<string, Activity>>
                    (await response.Content.ReadAsStringAsync());

                return new AllocatedTimetable()
                {
                    IsSuccess = true,
                    Timetables = allocatedTimetableDict
                };
            }
            
            // If failed, return false
            return new AllocatedTimetable() { IsSuccess = false };
        }

        /// <summary>
        /// Detect if the activity can be allocated
        /// </summary>
        /// <param name="subjectCode">e.g. "COSC1114_1750_1674_AUSCY" for OS Principles. Btw this course is AWESOME lol.</param>
        /// <param name="activityGroupCode">"TUT01" for tutorials</param>
        /// <param name="activityCode">"08" the sequence ID in the tute list</param>
        /// <returns></returns>
        public async Task<bool?> DetectAllocationPossibility(string subjectCode, string activityGroupCode,
            string activityCode)
        {
            if (casLoginResult.CasError != CasLoginError.NoError)
            {
                return null;
            }

            var clientHandler = new HttpClientHandler()
            {
                CookieContainer = casLoginResult.CasCookieContainer
            };

            var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://mytimetable.rmit.edu.au")
            };
            
            // Test if it available first...
            var activityListResponse = await httpClient.GetAsync(
                string.Format("/odd/rest/student/{0}/subject/{1}/group/{2}/activities/?ss={3}", 
                casLoginResult.UserName, subjectCode, activityGroupCode, siteToken));

            if (!activityListResponse.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                httpClient.Dispose();
                clientHandler.Dispose();
            }

            // Grab the activity list later if everything OK.
            var activityList =
                JsonConvert.DeserializeObject<Dictionary<string, Activity>>(await activityListResponse.Content
                    .ReadAsStringAsync());
            
            
            // Detect if the activity can be chosen
            var activityListKey = string.Format("{0}|{1}|{2}", subjectCode, activityGroupCode, activityCode);
            
            return activityList.ContainsKey(activityListKey) && activityList[activityListKey].Selectable;

        }

        /// <summary>
        /// Do allocation, assume it can be allocated.
        /// </summary>
        /// <param name="subjectCode"></param>
        /// <param name="activityGroupCode"></param>
        /// <param name="activityCode"></param>
        /// <returns></returns>
        public async Task<AllocateResult> DoAllocation(string subjectCode, string activityGroupCode,
            string activityCode)
        {
            if (casLoginResult.CasError != CasLoginError.NoError)
            {
                return new AllocateResult() {Success = false, Message = "Login token invalid, please retry login."};
            }

            var clientHandler = new HttpClientHandler()
            {
                CookieContainer = casLoginResult.CasCookieContainer
            };

            var httpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://mytimetable.rmit.edu.au")
            };
            
            // Prepare POST content
            var postContent = new StringContent(
                Uri.EscapeDataString(
                    $"token=a&student_code={casLoginResult.UserName}&subject_code={subjectCode}&" +
                    $"activity_group_code={activityGroupCode}&activity_code={activityCode}"),
                Encoding.UTF8, "application/x-www-form-urlencoded");
            
            // Fire the hole! Fingercross and see if it works...
            var postResult = await httpClient.PostAsync(
                string.Format("/odd/rest/student/changeActivity/?ss={0}", siteToken),
                postContent);
            
            // Convert result
            if (postResult.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AllocateResult>(await postResult.Content.ReadAsStringAsync());
            }
            else
            {
                return new AllocateResult()
                {
                    Success = false,
                    Message = "Your query to myTimetable server is dead."
                };
            }

        }

        /// <summary>
        /// Grab a site token
        /// </summary>
        /// <returns></returns>
        private async Task<string> RefreshSiteToken()
        {
            // Stop if CAS login failed
            if (casLoginResult.CasError != CasLoginError.NoError)
            {
                return null;
            }

            // AutoRedirect must be disabled due to a damn issue in .NET Core HttpClient library...
            var clientHandler = new HttpClientHandler()
            {
                CookieContainer = casLoginResult.CasCookieContainer,
                AllowAutoRedirect = false
            };

            var casHttpClient = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri("https://sso-cas.rmit.edu.au")
            };

            // Fire the hole!
            var casResponse =
                await casHttpClient.GetAsync("/rmitcas/login?service=https://mytimetable.rmit.edu.au/odd/student");

            // If not 300, 301 or 302, then the query must has been somehow fucked.
            if (casResponse.StatusCode != HttpStatusCode.Found
                || casResponse.StatusCode != HttpStatusCode.MovedPermanently
                || casResponse.StatusCode != HttpStatusCode.Moved)
            {
                return null;
            }
            
            // Assume we've succeeded, grab the CAS Ticket
            // the format should be an URL with a ticket token of course, for example:
            //      https://mytimetable.rmit.edu.au/odd/student?ticket=ST-123456-xxxxxxxxxxxxxxxxxx-rmitcas
            var ticketUrl = casResponse.Headers.GetValues("Location").FirstOrDefault();
            
            // Be nice and do some GC lol...
            casHttpClient.Dispose();
            
            // Declare a new HttpClient, once again, disable the auto redirection for 30x
            var myTimetableHttpClient = new HttpClient(clientHandler);
            
            // Now fire the hole!
            var myTimetableResponse = await myTimetableHttpClient.GetAsync(ticketUrl);
            
            // Return token if successful
            if (myTimetableResponse.StatusCode != HttpStatusCode.Found
                || myTimetableResponse.StatusCode != HttpStatusCode.Moved
                || myTimetableResponse.StatusCode != HttpStatusCode.MovedPermanently)
            {
                return null;
            }
            else
            {
                clientHandler.Dispose();
                myTimetableHttpClient.Dispose();
                
                // Keep in mind that the location is in LOWER CASE!!!
                return myTimetableResponse.Headers.GetValues("location").FirstOrDefault();
            }
        }
    }
}
