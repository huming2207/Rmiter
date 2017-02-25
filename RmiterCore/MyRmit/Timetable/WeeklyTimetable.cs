using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCore.MyRmit.Timetable
{
    [JsonObject]
    public class WeeklyTimetable
    {
        [JsonProperty(PropertyName = "dailyTimetable")]
        public List<DailyTimetable> DailyTimetable { get; set; }

        [JsonProperty(PropertyName = "dayDisplayable")]
        public string DisplayableDate { get; set; }
    }
}
