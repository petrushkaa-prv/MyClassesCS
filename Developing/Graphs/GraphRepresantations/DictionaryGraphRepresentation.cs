using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    public class DictionaryGraphRepresentation : IGraphRepresentation
    {
        public IAdjacencyStructure<T> GetAdjacencyStructure<T>(int size) where T : new()
            => new DictionaryAdjacencyStructure<T>(size);
    }
}
