using System;
using Newtonsoft.Json;

namespace RmiterCoreClassic.MyRmit
{
    [JsonObject]
    public class Announcements
    {
        [JsonProperty(PropertyName = "announcementId")]
        public int AnnouncementId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "important")]
        public bool Important { get; set; }

        [JsonProperty(PropertyName = "read")]
        public bool Read { get; set; }

        [JsonProperty(PropertyName = "expiryDate")]
        public string ExpiryDate { get; set; }
    }
}
