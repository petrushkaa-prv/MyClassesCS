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
    internal class DlList<T> : IMyList<T>, IMyCollection<T>
    {
        private DlNode<T> _head;
        public DlNode<T> Head => _head;

        private DlNode<T> _rear;
        public DlNode<T> Rear => _rear;

        private int _size;


        /// <inheritdoc />
        public bool IsEmpty => _head is null || _rear is null || _size == 0;

        /// <inheritdoc />
        public int Size => _size;



        /// <inheritdoc />
        public T Front => _head.Value;

        /// <inheritdoc />
        public T Back => _rear.Value;

        public DlList()
        {
            _size = 0;
            _head = null;
            _rear = null;
        }
        public DlList(params T[] arr)
        {
            foreach (var val in arr)
                AddEnd(val);            
        }



        /// <inheritdoc />
        public bool Contains(T element)
        {
            return Contains(element, out _);
        }

        protected bool Contains(T element, out DlNode<T> foundPtr)
        {
            if (IsEmpty)
            {
                foundPtr = null;
                return false;
            }

            var ptr = _head;

            while (ptr is not null && ptr.Value != (dynamic)element) 
                ptr = ptr!.Next;

            foundPtr = ptr;

            return foundPtr is not null && foundPtr.Value == (dynamic)element;
        }

        /// <inheritdoc />
        public void Clear()
        {
            _head = null;
            _rear = null;
            _size = 0;
        }



        /// <inheritdoc />
        public void AddFront(T val)
        {
            _size++;

            var newNode = new DlNode<T>(val);

            if (IsEmpty)
            {
                _head = newNode;
                _rear = newNode;

                return;
            }

            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }

        /// <inheritdoc />
        public void AddEnd(T val)
        {
            _size++;

            var newNode = new DlNode<T>(val);

            if (IsEmpty)
            {
                _head = newNode;
                _rear = newNode;

                return;
            }

            _rear.Next = newNode;
            newNode.Prev = _rear;
            _rear = newNode;
        }

        /// <inheritdoc />
        public void RemoveFront()
        {
            if(IsEmpty) return;

            _size--;

            if (_rear.Next is null && _rear.Prev is null)
            {
                _head = _rear = null;
                return;
            }

            _head = _head.Next;
            _head.Prev = null;
        }

        /// <inheritdoc />
        public void RemoveEnd()
        {
            if(IsEmpty) return;

            _size--;

            if (_rear.Next is null && _rear.Prev is null)
            {
                _head = _rear = null;
                return;
            }

            _rear = _rear.Prev;
            _rear.Next = null;
        }

        public void Remove(T val)
        {
            if(_head is null) return;
            if (_head.Value == (dynamic)val)
            {
                RemoveFront();
                return;
            }
            if (_rear.Value == (dynamic)val)
            {
                RemoveEnd();
                return;
            }

            if (!Contains(val, out var ptr)) return;

            ptr.Prev.Next = ptr.Next;
            ptr.Next.Prev = ptr.Prev;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty) yield break;

            var ptr = _head;

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

            sb.AppendJoin(" <-> ", this);

            return sb.ToString();
        }
    }
}
