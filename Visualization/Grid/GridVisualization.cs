using System.Collections.Generic;

namespace Converter.Visualization.Grid
{
    public class GridVisualization : Visualization
    {
        public override string[] Tags => new[] {"array"};

        public List<ColumnLabelData>? ColumnLabels { get; set; }
        
        public List<RowData> Rows { get; set; } = new List<RowData>();
        
        public void AddDefaultColumnLabels(int numberOfColumns)
        {
            ColumnLabels = new List<ColumnLabelData>();
            
            for (var i = 0; i < numberOfColumns; i++)
            {
                ColumnLabels.Add(new ColumnLabelData(i));
            }
        }
    }
}
