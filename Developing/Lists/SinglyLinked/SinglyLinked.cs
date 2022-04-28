using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.GeneralInterfaces;
using Developing.Nodes;

namespace Developing.Lists
{
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
            if (IsEmpty) yield break;

            var ptr = this.Head;

            do
            {
                yield return ptr.Value;
            }
            while ((ptr = ptr.Next) is not null);
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
