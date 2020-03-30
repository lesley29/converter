using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Converter.Visualization
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class Visualization
    {
        static Visualization()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        
        public Dictionary<string, bool> Kind
        {
            get
            {
                return Tags.ToDictionary(tag => tag, tag => true);
            }
        }

        [JsonIgnore]
        public abstract string[] Tags { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
