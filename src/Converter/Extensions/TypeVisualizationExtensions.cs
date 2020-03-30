using System;
using Converter.Extraction;
using Converter.Visualization.Tree;

namespace Converter.Extensions
{
    public static class TypeVisualizationExtensions
    {
        public static string Visualize(this Type type)
        {
            var tree = TypeTree.From(type);
            
            var root = new TreeVisualization.TreeNode();
            Traverse(root, tree);
            
            return new TreeVisualization
            {
                Root = root
            }.ToString();
        }

        private static void Traverse(TreeVisualization.TreeNode node, TypeTree typeTree)
        {
            node.Value = typeTree.Type.FullName;

            // base types
            if (typeTree.DirectImplementedInterfaces != null)
            {
                var baseTypesChild = new TreeVisualization.TreeNode
                {
                    Name = "Base types"
                };

                foreach (var implementedInterface in typeTree.DirectImplementedInterfaces)
                {
                    var interfaceNode = new TreeVisualization.TreeNode();
                    Traverse(interfaceNode, implementedInterface);
                    baseTypesChild.Children.Add(interfaceNode);
                }
            
                node.Children.Add(baseTypesChild);
            }

            // generic arguments
            if (typeTree.GenericArguments != null)
            {
                var genericArgumentsChild = new TreeVisualization.TreeNode
                {
                    Name = "Generic arguments"
                };

                foreach (var genericArgument in typeTree.GenericArguments)
                {
                    genericArgumentsChild.Children.Add(Traverse(genericArgument));
                }
                
                node.Children.Add(genericArgumentsChild);
            }
        }

        private static TreeVisualization.TreeNode Traverse(TypeTree.GenericArgument genericArgument)
        {
            var treeNode = new TreeVisualization.TreeNode
            {
                Id = genericArgument.DefinitionType.Name,
                Name = "->",
                Value = genericArgument.Type.FullName
            };

            if (genericArgument.GenericArguments != null)
            {
                foreach (var argument in genericArgument.GenericArguments)
                {
                    treeNode.Children.Add(Traverse(argument));
                }
            }

            return treeNode;
        }
    }
}
