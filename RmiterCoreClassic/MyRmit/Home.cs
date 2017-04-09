using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RmiterCoreClassic.MyRmit
{
    [JsonObject]
    public class Home
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

        [JsonProperty(PropertyName = "announcements")]
        public List<Announcements> Announcements { get; set; }
    }
}
