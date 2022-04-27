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

    public interface IMyStack<T>
    {
        void Push(T elem);
        void Pop();
        public T Peek { get; }
    }

    public interface IMyList<T>
    {
        public T Front { get; }
        public T Back { get; }

        public void AddFront(T val);
        public void AddEnd(T val);

        public void RemoveFront();
        public void RemoveEnd();
    }

    public abstract class SinglyLinked<T> : IMyCollections<T>
    {
        public abstract SlNode<T> Head { get; }

        /// <inheritdoc />
        public abstract bool IsEmpty { get; }

        /// <inheritdoc />
        public abstract int Size { get; }

        /// <inheritdoc />
        public bool Contains(T element)
        {
            return Contains(element, out _, out _);
        }

        /// <inheritdoc />
        public abstract void Clear();

        protected bool Contains(T element, out SlNode<T> foundPtr, out SlNode<T> prevPtr)
        {
            prevPtr = null;

            if (this.IsEmpty)
            {
                foundPtr = null;
                return false;
            }

            var ptr = Head;

            while (ptr != null && ptr.Value != (dynamic)element)
            {
                prevPtr = ptr;
                ptr = ptr!.Next;
            }

            foundPtr = ptr;
            return foundPtr is not null && foundPtr.Value == (dynamic)element;
        }

        /// <inheritdoc />
        public virtual IEnumerator<T> GetEnumerator()
        {
            if(IsEmpty) yield break;

            var ptr = this.Head;

            while (ptr is not null)
            {
                yield return ptr.Value;
                ptr = ptr.Next;
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendJoin(", ", this);

            return sb.ToString();
        }
    }
}
