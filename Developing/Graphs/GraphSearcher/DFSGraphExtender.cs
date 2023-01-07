using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Lists;

namespace Developing.Graphs
{
    public static class DFSGraphExtender
    {
        public static IGraphSearcher DFS(this IGraph graph) 
            => new GeneralGraphSearcher(graph, new SlStack<int>(), !graph.Directed);

        public static IGraphSearcher<T> DFS<T>(this IGraph<T> graph) 
            => new GeneralGraphSearcher<T>(graph, new SlStack<int>(), !graph.Directed);

        public static IGraphSearcher<T> DFS<T>(this Graph<T> graph) where T : new() 
            => new GeneralGraphSearcher<T>(graph, new SlStack<int>(), true);

        public static IGraphSearcher<T> DFS<T>(this DirectedGraph<T> graph) where T : new()
            => new GeneralGraphSearcher<T>(graph, new SlStack<int>(), false);
    }
}
