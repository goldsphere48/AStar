using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new Matrix();
            matrix.ReadFromFile("../../../test6.txt");
            Console.WriteLine("Лабиринт:");
            matrix.Print();
            var graph = matrix.ConvertToGraph();
            var path = graph.FindPathAStar(new Location(0, 1), new Location(4, 4));
            Console.WriteLine("Граф:");
            graph.Print();
            Console.WriteLine("Путь:");
            var pathString = string.Join(" -> ", path);
            Console.WriteLine(pathString);
            Console.ReadKey();
        }
    }
}
