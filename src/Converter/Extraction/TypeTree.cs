using System;
using System.Collections.Generic;
using System.Linq;

namespace Converter.Extraction
{
    public class TypeTree
    {
        private TypeTree(Type type)
        {
            Type = type;
        }
        
        public Type Type { get; }

        public List<TypeTree>? DirectlyImplementedInterfaces { get; set; }

        public List<GenericArgument>? GenericArguments { get; set; }

        public static TypeTree From(Type type)
        {
            return GetTypeTree(type);
        }

        private static TypeTree GetTypeTree(Type type)
        {
            return new TypeTree(type)
                .AddImplementedInterfaces()
                .AddGenericArguments();
        }

        private TypeTree AddImplementedInterfaces()
        {
            var allInterfaces = Type.GetInterfaces();
            
            var directInterfaces = allInterfaces
                .Except(allInterfaces.SelectMany(t => t.GetInterfaces()))
                .ToList();

            if (directInterfaces.Any())
            {
                DirectlyImplementedInterfaces = directInterfaces.Select(GetTypeTree).ToList();
            }

            return this;
        }

        private TypeTree AddGenericArguments()
        {
            if (Type.IsGenericType)
            {
                GenericArguments = GetGenericArguments(Type);
            }

            return this;
        }
        
        private static List<GenericArgument> GetGenericArguments(Type type)
        {
            var arguments = new List<GenericArgument>();

            var definitionGenericArguments = type.GetGenericTypeDefinition().GetGenericArguments();
            var genericArgumentsTypes = type.GetGenericArguments();

            foreach (var (argumentType, definitionType) in genericArgumentsTypes.Zip(definitionGenericArguments))
            {
                var genericArgument = new GenericArgument(argumentType, definitionType);
                
                if (argumentType.IsGenericType)
                {
                    genericArgument.GenericArguments = GetGenericArguments(argumentType);
                }
                
                arguments.Add(genericArgument);
            }

            return arguments;
        }
    }
}
