using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCorePcl.MyRmit
{
    [JsonObject]
    public class ClassTimetable
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

        [JsonProperty(PropertyName = "weekStartDate")]
        [JsonConverter(typeof(Converter.UnixTimestampConverter))]
        public DateTime WeekStartDate { get; set; }

        [JsonProperty(PropertyName = "weeklyTimetable")]
        public List<Timetable.WeeklyTimetable> WeeklyTimetable { get; set; }
    }
}
