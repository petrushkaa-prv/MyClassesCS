using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal interface IGraphRepresentation
    {
        public IAdjacencyStructure<T> GetAdjacencyStructure<T>(int size) where T : new();
    }
}
