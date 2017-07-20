using System.Collections.Generic;
using Newtonsoft.Json;

namespace RmiterCore.MyTimetable.Timetable
{
    [JsonObject]
    public class TimetableIndexItem
    {
        [JsonProperty("agv_subject_description")]
        public string SubjectDescription { get; set; }

        [JsonProperty("agv_activityType")]
        public string ActivityType { get; set; }

        [JsonProperty("agv_location")]
        public string Location { get; set; }

        /// <summary>
        /// Week pattern is a 52 bits binary string, 1 -> has this course in a certain week, 0 -> Vice-versa
        /// </summary>
        [JsonProperty("agv_week_pattern")]
        public string WeekPattern { get; set; }
    }
}
