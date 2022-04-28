using System.Collections;
using System.Collections.Generic;
using System.Text;
using Developing.Nodes;

namespace Developing.Interfaces
{
    /// <summary>
    /// Interface of general methods to be present in all MyClasses Classes
    /// </summary>
    public interface IMyCollections<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        public int Size { get; }

        public bool Contains(T element);

        public void Clear();
    }

    public interface IMyStack<T> : IOrderedCollection<T>
    {
        new void Push(T elem);
        new T Pop();
        new T Peek { get; }
    }
}
