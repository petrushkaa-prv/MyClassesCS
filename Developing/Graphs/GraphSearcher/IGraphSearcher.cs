using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    public interface IGraphSearcher
    {
        IEnumerable<Edge> SearchFrom(int v);

        IEnumerable<Edge> SearchAll();
    }

    public interface IGraphSearcher<T>
    {
        IEnumerable<Edge<T>> SearchFrom(int v);

        IEnumerable<Edge<T>> SearchAll();
    }
}
