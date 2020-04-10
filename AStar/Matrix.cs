using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AStar
{
    class Matrix
    {
        const int Wall = -1;
        const int Error = -2;

        public static List<Location> Dirs = new List<Location>
        {
            new Location(-1, 0),
            new Location(0, -1),
            new Location(1, 0),
            new Location(0, 1),
        };

        private List<List<int>> _matrix = new List<List<int>>();


        public int this[int x, int y]
        {
            get
            {
                if ((x >= 0 && x < _matrix.Count) && (y >= 0 && y < _matrix[x].Count))
                {
                    return _matrix[x][y];
                }
                return Error;
            }
        }

        public void ReadFromFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var splitedLine = line.Split(" ");
                    var row = new List<int>();
                    foreach (var weight in splitedLine)
                    {
                        row.Add(int.Parse(weight));
                    }
                    _matrix.Add(row);
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < _matrix.Count; ++i)
            {
                for (int j = 0; j < _matrix[i].Count; ++j)
                {
                    Console.Write($"{_matrix[i][j]} ");
                }
                Console.WriteLine();
            }
        }

        public IEnumerable<Edge> Neighbours(Location l)
        {
            foreach (var dir in Dirs)
            {
                var x = l.X + dir.X;
                var y = l.Y + dir.Y;
                if (this[x, y] != Error && this[x, y] != Wall)
                {
                    yield return new Edge(l, new Location(x, y), _matrix[x][y]);
                }
            }
        }

        public Graph ConvertToGraph()
        {
            Graph graph = new Graph();
            for (int i = 0; i < _matrix.Count; ++i)
            {
                for (int j = 0; j < _matrix[i].Count; ++j)
                {
                    if (_matrix[i][j] != Wall)
                    {
                        var location = new Location(i, j);
                        graph.AddLocation(location);
                        var edges = Neighbours(location);
                        foreach (var edge in edges)
                        {
                            graph.AddEdge(edge);
                        }
                    }
                }
            }
            return graph;
        }
    }
}
