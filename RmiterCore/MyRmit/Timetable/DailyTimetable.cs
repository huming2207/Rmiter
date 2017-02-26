using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCore.MyRmit.Timetable
{
    [JsonObject]
    public class DailyTimetable
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "catalogNumber")]
        public string CatalogNumber { get; set; }

        [JsonProperty(PropertyName = "activityType")]
        public string ActivityType { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "startTime")]
        [JsonConverter(typeof(Converter.UnixTimestampConverter))]
        public DateTime StartTime { get; set; }

        [JsonProperty(PropertyName = "endTime")]
        [JsonConverter(typeof(Converter.UnixTimestampConverter))]
        public DateTime EndTime { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "startDisplayable")]
        public string StartDisplayable { get; set; }

        [JsonProperty(PropertyName = "endDisplayable")]
        public string EndDisplayable { get; set; }
    }
}
