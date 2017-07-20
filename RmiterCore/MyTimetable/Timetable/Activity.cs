using Newtonsoft.Json;

namespace RmiterCore.MyTimetable.Timetable
{
    [JsonObject]
    public class Activity
    {
        [JsonProperty("subject_code")]
        public string SubjectCode { get; set; }

        [JsonProperty("activity_group_code")]
        public string ActivityGroupCode { get; set; }

        [JsonProperty("activity_code")]
        public string ActivityCode { get; set; }

        [JsonProperty("campus")]
        public string Campus { get; set; }

        [JsonProperty("campus_description")]
        public string CampusDescription { get; set; }

        [JsonProperty("day_of_week")]
        public string DayOfWeek { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("selectable")]
        public string Selectable { get; set; }

        [JsonProperty("availability")]
        public int Availability { get; set; }

        [JsonProperty("week_pattern")]
        public string WeekPattern { get; set; }

        [JsonProperty("zone")]
        public string Zone { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("semester")]
        public string Semester { get; set; }

        [JsonProperty("semester_description")]
        public string SemesterDescription { get; set; }

        [JsonProperty("activityType")]
        public string ActivityType { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }

        [JsonProperty("qualify")]
        public string Qualify { get; set; }
    }
}
