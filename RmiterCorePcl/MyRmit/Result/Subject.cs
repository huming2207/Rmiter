using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCorePcl.MyRmit.Result
{
    [JsonObject]
    public class Subject
    {
        [JsonProperty(PropertyName = "name")]
        public string StudentName { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string SubjectName { get; set; }

        [JsonProperty(PropertyName = "catalogueNumber")]
        public string CatalogueNumber { get; set; }

        [JsonProperty(PropertyName = "classNumber")]
        public string ClassNumber { get; set; }

        [JsonProperty(PropertyName = "courseDescription")]
        public string CourseDescription { get; set; }

        [JsonProperty(PropertyName = "grade")]
        public string Grade { get; set; }

        [JsonProperty(PropertyName = "mark")]
        public string Mark { get; set; }

        [JsonProperty(PropertyName = "term")]
        public string Term { get; set; }

        [JsonProperty(PropertyName = "unitsOfCredit")]
        public double UnitsOfCredit { get; set; }

        [JsonProperty(PropertyName = "availableFrom")]
        public string AvailableFrom { get; set; }
    }
}
