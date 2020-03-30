using Newtonsoft.Json;

namespace Converter.Visualization.Grid
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ColumnLabelData
    {
        public ColumnLabelData(int columnNumber)
        {
            Label = $"col_{columnNumber}";
        }
            
        public string? Label { get; set; }
    }
}
