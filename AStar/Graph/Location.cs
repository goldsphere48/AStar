using System;
using System.Collections.Generic;
using System.Text;

namespace AStar
{
    class Location : ILocation
    {
        public int X { get; }
        public int Y { get; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}:{Y})";
        }

        public override bool Equals(object obj)
        {
            if (obj is ILocation)
            {
                var l = obj as ILocation;
                return l.X == X && l.Y == Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
