using System;
using System.Collections.Generic;
using System.Text;

namespace AStar
{
    class Graph
    {
        public List<ILocation> V = new List<ILocation>(); 
        public List<Edge> E = new List<Edge>();

        private IPathFindingAlgorythm _pathFindingAlgorythm;

        public void AddLocation(ILocation l)
        {
            V.Add(l);
        }

        public void RemoveLocation(ILocation l)
        {
            V.Remove(l);
        }

        public void AddEdge(Edge e)
        {
            E.Add(e);
        }

        public void AddEdges(List<Edge> edges)
        {
            E.AddRange(edges);
        }

        public void RemoveEdge(Edge e)
        {
            E.Remove(e);
        }

        public void Connect(ILocation l1, ILocation l2, int weight)
        {
            E.Add(new Edge(l1, l2, weight));
        }

        public void Print()
        {
            foreach (var v in V)
            {
                Console.Write($"{v}: ");
                var neighbours = E.FindAll(e => e.From.Equals(v));
                foreach (var e in neighbours)
                {
                    Console.Write($"({e.Other(v)}, {e.Weight}) ");
                }
                Console.WriteLine();
            } 
        }

        public Stack<ILocation> FindPathAStar(ILocation start, ILocation finish)
        {
            _pathFindingAlgorythm = new AStar(this);
            return _pathFindingAlgorythm.FindPath(start, finish);
        }
    }
}
