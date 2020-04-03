using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Converter.Extraction;
using Converter.Visualization.Tree;

namespace Converter.Extensions
{
    public static class TypeVisualizationExtensions
    {
        public static string Visualize(this Type type)
        {
            var typeTree = TypeTree.From(type);
            
            var visualizationRoot = new TreeNode();
            Traverse(visualizationRoot, typeTree);
            
            return new TreeVisualization
            {
                Root = visualizationRoot
            }.ToString();
        }

        private static void Traverse(TreeNode node, TypeTree typeTree)
        {
            var nodeValueBuilder = new StringBuilder(typeTree.Type.Name);
            
            if (typeTree.DirectlyImplementedInterfaces != null)
            {
                AddBaseTypes(node, typeTree.DirectlyImplementedInterfaces);
            }
            
            if (typeTree.GenericArguments != null)
            {
                AddGenericArguments(node, typeTree.GenericArguments);
                nodeValueBuilder.Append($"<{string.Join(", ", node.Children.First().Children.Select(ch => ch.Value))}>");
            }
            
            node.Value = nodeValueBuilder.ToString().RemoveNumberOfGenericArguments();
        }
        
        private static void AddBaseTypes(TreeNode node, IEnumerable<TypeTree> baseTypes)
        {
            var baseTypesChild = new TreeNode
            {
                Name = "Base types"
            };

            foreach (var baseType in baseTypes)
            {
                var interfaceNode = new TreeNode();
                Traverse(interfaceNode, baseType);
                baseTypesChild.Children.Add(interfaceNode);
            }
            
            node.Children.Add(baseTypesChild);
        }
        
        private static void AddGenericArguments(TreeNode node, IEnumerable<GenericArgument> genericArguments)
        {
            var genericArgumentsChild = new TreeNode
            {
                Name = "Generic arguments"
            };
                
            foreach (var genericArgument in genericArguments)
            {
                genericArgumentsChild.Children.Add(BuildGenericArgumentNodeFor(genericArgument));
            }
                
            node.Children.Add(genericArgumentsChild);
        }

        private static TreeNode BuildGenericArgumentNodeFor(GenericArgument genericArgument)
        {
            var valueBuilder = new StringBuilder(genericArgument.Type.Name);
            
            var treeNode = new TreeNode
            {
                Id = genericArgument.DefinitionType.Name,
                Name = "->"
            };

            if (genericArgument.GenericArguments != null)
            {
                foreach (var childTreeNode in genericArgument.GenericArguments.Select(BuildGenericArgumentNodeFor))
                {
                    treeNode.Children.Add(childTreeNode);
                }

                valueBuilder.Append($"<{string.Join(", ", treeNode.Children.Select(ch => ch.Value))}>");
            }
            
            treeNode.Value = valueBuilder.ToString().RemoveNumberOfGenericArguments();
            
            return treeNode;
        }

        private static string RemoveNumberOfGenericArguments(this string s)
        {
            const string pattern = @"`\d+<";
            
            return Regex.Replace(s, pattern, "<", RegexOptions.Compiled);
        }
    }
}
