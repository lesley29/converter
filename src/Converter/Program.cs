using System;
using System.Collections.Generic;
using Converter.Extensions;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            TestTypes();
            
            var arr = new[]
            {
                1, 2, 3
            };

            Console.WriteLine(arr.Visualize());
            arr.Visualize();
            
            // var arr = new[,]
            // {
            //     {1, 2, 3, 4, 5},
            //     {6, 7, 8, 9, 10},
            //     {11, 12, 13, 14, 15}
            // };
            //
            // Console.WriteLine(arr.GetLength(0)); // 3
            // Console.WriteLine(arr.GetLength(1)); // 5

            


            // var list = new LinkedList<int>();
            // list.Visualize();
            // list.AddLast(1);
            // list.AddLast(5);
            // list.AddLast(10);
            // list.AddLast(1);
            //
            // list.Visualize();
            // Console.WriteLine(list.Visualize());
        }

        private static void TestTypes()
        {
            // var type = typeof(Dictionary<string, int>);
            var type = typeof(Dictionary<Tuple<int, string>, List<HashSet<double>>>);
            Console.Out.WriteLine(type.Visualize());
            type.Visualize();
        }
    }
}
