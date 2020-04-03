using System.Collections.Generic;

namespace Converter.Visualization.Tree
{
    public class TreeNode
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string EmphasizedValue { get; set; }
            
        public List<TreeNode> Children { get; } = new List<TreeNode>();

        public bool IsMarked { get; set; }
    }
}
