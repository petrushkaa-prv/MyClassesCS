using Developing.Nodes;

namespace Developing.Interfaces
{
    /// <summary>
    /// Interface of general methods to be present in all MyClasses Classes
    /// </summary>
    public interface IMyClasses<in T>
    {
        bool IsEmpty { get; }
        public int Size { get; }

        public bool Contains(T element);
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

    public abstract class SinglyLinked<T> : IMyClasses<T>
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
            return foundPtr != null && foundPtr.Value == (dynamic)element;
        }
    }
}
