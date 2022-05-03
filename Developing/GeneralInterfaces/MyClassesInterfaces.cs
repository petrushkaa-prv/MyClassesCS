using System.Collections;
using System.Collections.Generic;
using System.Text;
using Developing.Graphs;
using Developing.Nodes;

namespace Developing.Interfaces
{
    public interface IMyStack<T> : IOrderedContainer<T>
    {
        new void Push(T elem);
        new T Pop();
        new T Peek { get; }
    }
}
