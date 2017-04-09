using System;
using Newtonsoft.Json;

namespace RmiterCoreClassic.MyRmit.Details
{
    [JsonObject]
    public class HomeAddress
    {
        [JsonProperty("addressLine")]
        public string AddressLine { get; set; }

        [JsonProperty("suburb")]
        public string Suburb { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("addressFull")]
        public string AddressFull { get; set; }
    }
}
