using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCoreClassic.MyRmit
{
    [JsonObject]
    public class MyResult
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "responseCode")]
        public string ResponseCode { get; set; }

        [JsonProperty(PropertyName = "responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty(PropertyName = "timeStamp")]
        [JsonConverter(typeof(Converter.UnixTimestampConverter))]
        public DateTime TimeStamp { get; set; }

        [JsonProperty(PropertyName = "lastUpdated")]
        public string LastUpdated { get; set; }

        [JsonProperty(PropertyName = "termsAndResults")]
        public List<Result.TermsAndResult> TermsAndResults { get; set; }
    }
}
