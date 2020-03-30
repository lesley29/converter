using Converter.Visualization.Grid;

namespace Converter.Extensions
{
    public static class ArrayVisualizationExtensions
    {
        public static string Visualize<T>(this T[] array) 
            where T : struct
        {
            var gridVisualization = new GridVisualization();
            
            gridVisualization.AddDefaultColumnLabels(array.Length);

            var row = new RowData(0);
            
            foreach (var item in array)
            {
                var cell = ConstructCell(item, new CellData());
                row.Cells.Add(cell);
            }
            
            gridVisualization.Rows.Add(row);

            return gridVisualization.ToString();
        }
        
        public static string Visualize<T>(this T[,] array) 
            where T : struct
        {
            var gridVisualization = new GridVisualization();

            var numberOfRows = array.GetLength(0);
            var numberOfColumns = array.GetLength(1);

            gridVisualization.AddDefaultColumnLabels(numberOfColumns);

            for (var i = 0; i < numberOfRows; i++)
            {
                var row = new RowData(i);
                
                for (var j = 0; j < numberOfColumns; j++)
                {
                    var cell = ConstructCell(array[i, j], new CellData());
                    row.Cells.Add(cell);
                }
                
                gridVisualization.Rows.Add(row);
            }

            return gridVisualization.ToString();
        }

        private static CellData ConstructCell<T>(T item, CellData cellData)
        {
            cellData.Content = item.ToString();

            return cellData;
        }
    }
}
