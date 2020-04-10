using System;
using System.Collections.Generic;
using System.Text;

namespace AStar
{
    class AStarLocation : ILocation
    {
        private ILocation _location;

        public int X => _location.X;
        public int Y => _location.Y;

        public double G;
        public double H;
        public double F;

        public AStarLocation Parent { get; set; }

        public AStarLocation(ILocation location)
        {
            _location = location;
        }

        public override bool Equals(object obj)
        {
            return _location.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _location.GetHashCode();
        }

        public override string ToString()
        {
            return $"({X}:{Y})";
        }
    }
}
