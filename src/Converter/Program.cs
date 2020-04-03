using System;
using System.Collections.Generic;
using Converter.Extensions;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            var type = typeof(Dictionary<Tuple<int, string>, List<HashSet<double>>>);
            Console.Out.WriteLine(type.Visualize());
            type.Visualize();
        }
    }
}
