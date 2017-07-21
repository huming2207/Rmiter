using Newtonsoft.Json;

namespace RmiterCore.MyTimetable.Timetable
{
    [JsonObject]
    public class AllocateResult
    {
        [JsonProperty("msg")]
        public string Message { get; set; }
        
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}