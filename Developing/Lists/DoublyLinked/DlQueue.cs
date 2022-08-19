using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.GeneralInterfaces;
using Developing.Interfaces;

namespace Developing.Lists
{
    internal class DlQueue<T> : IEnumerable<T>, IMyCollection<T>, IOrderedContainer<T>
    {
        private readonly DlList<T> _list;

        public DlQueue()
        {
            _list = new DlList<T>();
        }

        public DlQueue(params T[] arr)
        {
            _list = new DlList<T>(arr);
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        /// <inheritdoc />
        public bool IsEmpty => _list.IsEmpty;

        /// <inheritdoc />
        public void Push(T val) => _list.AddFront(val);

        public void Push(params T[] values)
        {
            foreach(var val in values)
                this.Push(val);
        }

        /// <inheritdoc />
    public T Peek => _list.Back;

        /// <inheritdoc />
        public T Pop()
        {
            var returnValue = Peek;

            _list.RemoveEnd();

            return returnValue;
        }

        /// <inheritdoc cref="IMyCollection{T}.Size" />
        public int Size => _list.Size;

        /// <inheritdoc />
        public bool Contains(T element) => _list.Contains(element);

        /// <inheritdoc />
        public void Clear() => _list.Clear();
    }
}
