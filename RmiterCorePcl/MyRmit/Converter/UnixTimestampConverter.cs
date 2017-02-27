using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RmiterCorePcl.MyRmit.Converter
{
    // This is a workaround for converting the Unix timestamp included in JSON from school server,
    //   to C# DateTime class, which is easier to read later.
    // Original code from here: 
    // http://stackoverflow.com/questions/19971494/how-to-deserialize-a-unix-timestamp-%CE%BCs-to-a-datetime
    //
    public class UnixTimestampConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var timeStamp = long.Parse(reader.Value.ToString());
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timeStamp);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
