using System.Collections.Generic;

namespace Converter.Visualization.Graph
{
    public class GraphVisualization : Visualization
    {
        public override string[] Tags => new[] { "graph" };
        
        public IList<NodeData> Nodes { get; set; } = new List<NodeData>();
        
        public IList<EdgeData> Edges { get; set; } = new List<EdgeData>();
    }
}
