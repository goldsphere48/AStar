using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStar
{
    class AStar : IPathFindingAlgorythm
    {
        private Graph _graph;

        public AStar(Graph graph)
        {
            _graph = graph;
        }

        private List<AStarLocation> GetAdjacentNodes(ILocation location)
        {
            var queue = from e in _graph.E
                        where location.Equals(e.From)
                        select new AStarLocation(e.To);
            return queue.ToList();
        }

        private Stack<ILocation> ReconstructPath(AStarLocation start, AStarLocation goal)
        {
            var result = new Stack<ILocation>();
            var current = goal;
            while (current != null)
            {
                result.Push(current);
                current = current.Parent;
            }
            return result;
        }

        private double DistanceBetween(ILocation a, ILocation b)
        {
            return _graph.E.Find(x => x.From.Equals(a) && x.To.Equals(b)).Weight;
        }

        public Stack<ILocation> FindPath(ILocation startLocation, ILocation finishLocation)
        {
            Graph graph = new Graph();
            foreach (var v in _graph.V)
            {
                graph.AddLocation(new AStarLocation(v));
            }
            graph.AddEdges(_graph.E);
            var start = graph.V.Find(v => v.Equals(startLocation)) as AStarLocation;
            var finish = graph.V.Find(v => v.Equals(finishLocation)) as AStarLocation;
            if (start == null || finish == null)
            {
                return new Stack<ILocation>();
            }
            return FindPathAStar(start, finish);
        }

        public Stack<ILocation> FindPathAStar(AStarLocation start, AStarLocation finish)
        {

            start.G = 0;
            start.H = HeuristicFunction(start, finish);
            start.F = start.G + start.H;

            var closed = new List<AStarLocation>();
            var open = new List<AStarLocation>();
            open.Add(start);

            while (open.Count > 0)
            {
                var current = open.Find(x => x.F == open.Min(y => y.F));

                if (current.Equals(finish))
                {
                    return ReconstructPath(start, current);
                }

                open.Remove(current);
                closed.Add(current);

                var neighbours = GetAdjacentNodes(current);
                foreach (var n in neighbours)
                {
                    if (closed.Contains(n))
                    {
                        continue;
                    }

                    var tentativeGScore = current.G + DistanceBetween(current, n);
                    var tentativeIsBetter = false;

                    if (!open.Contains(n))
                    {
                        open.Add(n);
                        tentativeIsBetter = true;
                    } else
                    {
                        tentativeIsBetter = tentativeGScore < n.G;
                    }

                    if (tentativeIsBetter)
                    {
                        n.Parent = current;
                        n.G = tentativeGScore;
                        n.H = HeuristicFunction(n, finish);
                        n.F = n.G + n.H;
                    }
                }
            }
            return new Stack<ILocation>();
        }

        private double HeuristicFunction(ILocation start, ILocation finish)
        {
            return Math.Sqrt(Math.Pow(finish.X - start.X, 2) + Math.Pow(finish.Y - start.Y, 2));
        }
    }
}
