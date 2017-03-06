using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCoreUwp.MyRmit.Details
{
    [JsonObject]
    public class PostalAddress
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
