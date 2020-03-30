using System.Collections.Generic;
using Converter.Visualization.Graph;

namespace Converter.Extensions
{
    public static class ListVisualizationExtensions
    {
        public static string Visualize<T>(this LinkedList<T> linkedList) 
            where T : struct
        {
            if (linkedList.Count == 0)
                return "";
            
            var graphVisualization = new GraphVisualization();
            
            var current = linkedList.First;
            var nextId = 0;
            
            var currentNodeId = nextId++.ToString();
                
            graphVisualization.Nodes.Add(new NodeData(currentNodeId)
            {
                Label = current?.Value.ToString()
            });
            
            if (linkedList.Count == 1)
                return graphVisualization.ToString();
            
            var next = current?.Next;

            while (next != null)
            {
                var nextNodeId = nextId++.ToString();

                graphVisualization.Nodes.Add(new NodeData(nextNodeId)
                {
                    Label = next.Value.ToString()
                });
                
                graphVisualization.Edges.Add(new EdgeData(currentNodeId, nextNodeId));

                currentNodeId = nextNodeId;
                next = next.Next;
            }

            return graphVisualization.ToString();
        }
        
    }
}
