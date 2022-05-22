using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Interfaces;

namespace Developing.Graphs
{
    internal class GeneralGraphSearcher<T> : IGraphSearcher<T>
    {
        private IOrderedContainer<int> _container;
        private IGraph<T> _graph;
        private IEnumerator<Edge<T>>[] _enums;
        private bool _skipEdgesToFinished;

        public GeneralGraphSearcher(IGraph<T> graph, IOrderedContainer<int> container, bool skipEdgesToFinished)
        {
            _container = container;
            _graph = graph;
            _skipEdgesToFinished = skipEdgesToFinished;
            _enums = new IEnumerator<Edge<T>>[graph.VertexCount];
        }

        public IEnumerable<Edge<T>> SearchAll()
        {
            for (int i = 0; i < _graph.VertexCount; ++i)
            {
                if (_enums[i] != null) continue;

                foreach (var edge in SearchFrom(i))
                    yield return edge;
            }
        }

        public IEnumerable<Edge<T>> SearchFrom(int v) 
        {
            try 
            {
                var curEn = _graph.OutEdges(v).GetEnumerator();
                _enums[v] = curEn;
                _container.Push(v);

                while (_container.Size > 0) 
                {
                    curEn = _enums[_container.Peek];
                    if (!curEn.MoveNext()) 
                    {
                        curEn.Dispose();
                        _container.Pop();
                    } 
                    else 
                    {
                        yield return curEn.Current;

                        if (_enums[curEn.Current.To] != null) continue;

                        _enums[curEn.Current.To] = _graph.OutEdges(curEn.Current.To).GetEnumerator();
                        _container.Push(curEn.Current.To);
                    }
                }

                curEn = null;
            }
            finally
            {
                foreach (var index in _container) 
                    _enums[index].Dispose();
            }
        }
    }



    internal class GeneralGraphSearcher : IGraphSearcher
    {
        private GeneralGraphSearcher<bool> _internalGraphSearcher;

        public GeneralGraphSearcher(IGraph graph, IOrderedContainer<int> container, bool skipEdgesToFinished) 
            => _internalGraphSearcher = new GeneralGraphSearcher<bool>(new GraphAdapter(graph), container, skipEdgesToFinished);

        public IEnumerable<Edge> SearchAll()
        {
            foreach (var edge in _internalGraphSearcher.SearchAll())
                yield return new Edge(edge.From, edge.To);
        }

        public IEnumerable<Edge> SearchFrom(int v)
        {
            foreach (var edge in _internalGraphSearcher.SearchFrom(v))
                yield return new Edge(edge.From, edge.To);
        }
    }
}
