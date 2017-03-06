using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCoreUwp.MyRmit
{
    [JsonObject]
    public class MyDetails
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("timeStamp")]
        [JsonConverter(typeof(Converter.UnixTimestampConverter))]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("citizenship")]
        public string Citizenship { get; set; }

        [JsonProperty("postalAddress")]
        public Details.PostalAddress PostalAddress { get; set; }

        [JsonProperty("homeAddress")]
        public Details.HomeAddress HomeAddress { get; set; }

        [JsonProperty("phoneHome")]
        public string HomePhone { get; set; }

        [JsonProperty("phoneMobile")]
        public string MobilePhone { get; set; }

        [JsonProperty("phonePlacement")]
        public string PlacementPhone { get; set; }

        [JsonProperty("programName")]
        public string ProgramName { get; set; }

        [JsonProperty("programLoad")]
        public string ProgramLoad { get; set; }

        [JsonProperty("programDuration")]
        public string ProgramDuration { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("photograph")]
        public string Photograph { get; set; }

        [JsonProperty("hashCode")]
        public string HashCode { get; set; }

        [JsonProperty("transportConcessionEligibilityData")]
        public string TransportConcessionEligibilityData { get; set; }

        [JsonProperty("eligibleCaptureMe")]
        public bool EligibleCaptureMe { get; set; }

        [JsonProperty("dob")]
        public string DateOfBirth { get; set; }
    }

}
