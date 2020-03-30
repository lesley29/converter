using Newtonsoft.Json;

namespace Converter.Visualization.Graph
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class EdgeData
    {
        public EdgeData(string from, string to)
        {
            From = from;
            To = to;
        }
            
        public string From { get; set; }
            
        public string To { get; set; }
            
        public string? Label { get; set; }
            
        public string? Id { get; set; }
            
        public string? Color { get; set; }
            
        public bool? Dashes { get; set; }
    }
}
