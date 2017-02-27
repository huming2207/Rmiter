using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCorePcl.MyRmit.Result
{
    [JsonObject]
    public class TermsAndResult
    {
        [JsonProperty(PropertyName = "semesterName")]
        public string SemesterName { get; set; }

        [JsonProperty(PropertyName = "subjects")]
        public List<Subject> Subjects { get; set; }
    }
}
