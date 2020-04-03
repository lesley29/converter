using System;
using System.Collections.Generic;

namespace Converter.Extraction
{
    public class GenericArgument
    {
        public GenericArgument(Type type, Type definitionType)
        {
            Type = type;
            DefinitionType = definitionType;
        }

        public Type DefinitionType { get; }
        
        public Type Type { get; }

        public List<GenericArgument>? GenericArguments { get; set; }
    }
}