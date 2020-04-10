using System;
using System.Collections.Generic;
using System.Text;

namespace AStar
{
    interface IPathFindingAlgorythm
    {
        Stack<ILocation> FindPath(ILocation start, ILocation finish);
    }
}
