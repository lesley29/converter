namespace Converter.Visualization.Tree
{
    public class TreeVisualization : Visualization
    {
        public override string[] Tags => new[] {"tree"};

        public TreeNode Root { get; set; }
    }
}
