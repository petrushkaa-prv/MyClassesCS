using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Graphs
{
    internal static class DFSGraphExtender
    {
        public static IGraphSearcher DFS(this IGraph g) => (IGraphSearcher)new GeneralGraphSearcher(g, new SlStack<int>(), !g.Directed);

        public static IGraphSearcher<T> DFS<T>(this IGraph<T> g) => (IGraphSearcher<T>)new GeneralGraphSearcher<T>(g, new SlStack<int>(), !g.Directed);

        public static IGraphSearcher<T> DFS<T>(this Graph<T> g) where T : new() => (IGraphSearcher<T>)new GeneralGraphSearcher<T>(g, new SlStack<int>(), true);

        public static IGraphSearcher<T> DFS<T>(this DiGraph<T> g) where T : new() => (IGraphSearcher<T>)new GeneralGraphSearcher<T>(g, new SlStack<int>(), false);
    }
}
