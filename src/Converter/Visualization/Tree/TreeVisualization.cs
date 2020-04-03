namespace Converter.Visualization.Tree
{
    public class TreeVisualization : Visualization
    {
        public override string[] Tags => new[] {"tree"};

        public TreeVisualization(TreeNode root)
        {
            Root = root;
        }

        public TreeNode Root { get; }
    }
}
