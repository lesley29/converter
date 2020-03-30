using System.Collections.Generic;
using Newtonsoft.Json;

namespace Converter.Visualization.Grid
{
    public class RowData
    {
        public RowData(int rowNumber)
        {
            Label = $"row_{rowNumber}";
            Cells = new List<CellData>();
        }
            
        public string? Label { get; set; }
            
        [JsonProperty("columns")]
        public List<CellData> Cells { get; set; }
    }
}
