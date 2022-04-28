using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal interface IGraphSearcher
    {
        IEnumerable<Edge> SearchFrom(int v);

        IEnumerable<Edge> SearchAll();
    }

    internal interface IGraphSearcher<T>
    {
        IEnumerable<Edge<T>> SearchFrom(int v);

        IEnumerable<Edge<T>> SearchAll();
    }
}
