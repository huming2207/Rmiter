using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RmiterCore.MyTimetable.Timetable
{
    [JsonObject]
    public class Activity
    {
        [JsonProperty("subject_description")]
        public string SubjectDescription { get; set; }

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

        [JsonProperty("staff")]
        public string Staff { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("activity_size")]
        public int ActivitySize { get; set; }

        [JsonProperty("student_count")]
        public int StudentCount { get; set; }

        [JsonProperty("buffer")]
        public int Buffer { get; set; }

        [JsonProperty("availability")]
        public int Availability { get; set; }

        /// <summary>
        /// AllocatePlus REST API returns a 52 bit mask string as "week pattern".
        /// If 1 then this week has activities, if 0 then you've got a week off. :)
        /// </summary>
        [JsonProperty("week_pattern")]
        private string WeekPatternString { get; set; }
        public bool[] WeekPattern
        {
            get { return WeekPatternString.Select(bitMask => bitMask == '1').ToArray(); }
        }

        [JsonProperty("description")]
        public string Description { get; set; }

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

        [JsonProperty("capacity")]
        public string Capacity { get; set; }

        [JsonProperty("section_code")]
        public string SectionCode { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
        
        [JsonProperty("selectable")]
        private string SelectableString { get; set; }
        public bool Selectable => SelectableString.Equals("available");
    }

    public class AllocatedTimetable
    {
        public Dictionary<string, Activity> Timetables { get; set; }
        
        public bool IsSuccess { get; set; }
    }
}
