using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal class DictionaryGraphRepresentation : IGraphRepresentation
    {
        public IAdjacencyStructure<T> GetAdjacencyStructure<T>(int size) where T : new()
            => (IAdjacencyStructure<T>)new DictionaryAdjacencyStructure<T>(size);
    }
}
