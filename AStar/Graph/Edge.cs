using System;
using System.Collections.Generic;
using System.Text;

namespace AStar
{
    class Edge
    {
        public readonly ILocation From;
        public readonly ILocation To;
        public readonly int Weight;

        public Edge(ILocation l1, ILocation l2, int weight)
        {
            From = l1;
            To = l2;
            Weight = weight;
        }

        public bool Incident(ILocation l)
        {
            return From.Equals(l) || To.Equals(l);
        }

        public ILocation Other(ILocation l)
        {
            if (From.Equals(l))
            {
                return To;
            }
            else
            {
                return From;
            }
        }
    }
}
