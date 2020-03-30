using Newtonsoft.Json;

namespace Converter.Visualization.Graph
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class NodeData
    {
        public NodeData(string id)
        {
            Id = id;
        }
            
        public string Id { get; set; }
            
        public string? Label { get; set; }
            
        public string? Color { get; set; }
            
        public string? Shape { get; set; }
    }
}
