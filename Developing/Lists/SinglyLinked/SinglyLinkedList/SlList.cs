using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.GeneralInterfaces;
using Developing.Interfaces;
using Developing.Nodes;

namespace Developing.Lists
{
    public class SlList<T> : SinglyLinked<T>, IMyList<T>, IMyCollections<T>, IEnumerable<T>
    {
        private int _size;

        /// <inheritdoc/>
        public override int Size => _size;

        private SlNode<T> _head;

        /// <inheritdoc/>
        public override SlNode<T> Head => _head;

        private SlNode<T> _rear;
        public SlNode<T> Rear => _rear;

        public T Front => IsEmpty ? throw new InvalidOperationException() : Head.Value;
        public T Back => IsEmpty ? throw new InvalidOperationException() : Rear.Value;

        public override bool IsEmpty => Head is null || Rear is null || Size == 0;

        public SlList()
        {
            _size = 0;
            _head = null;
            _rear = null;
        }
        public SlList(params T[] arr)
        {
            foreach (var val in arr) 
                AddEnd(val);
        }

        /// <inheritdoc/>
        public void AddFront(T val)
        {
            _size++;

            var newNode = new SlNode<T>(val);

            if (IsEmpty)
            {
                _head = newNode;
                _rear = newNode;

                return;
            }

            newNode.Next = Head;
            _head = newNode;
        }

        /// <inheritdoc/>
        public void AddEnd(T val)
        {
            _size++;

            var newNode = new SlNode<T>(val);

            if (IsEmpty)
            {
                _head = newNode;
                _rear = newNode;

                return;
            }

            _rear.Next = newNode;
            _rear = newNode;
        }

        /// <inheritdoc/>
        public void RemoveFront()
        {
            if (IsEmpty) return;

            _size--;

            _head = Head.Next;
        }

        /// <inheritdoc/>
        public void RemoveEnd()
        {
            if (IsEmpty) return;

            _size--;

            var ptr = _head;
            while (ptr.Next is not null && ptr.Next != _rear) 
                ptr = ptr.Next;

            ptr.Next = null;
            _rear = ptr;
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
            
            var ptr = _head;
            
            while (ptr!.Next is not null && ptr.Next.Value != (dynamic)val) 
                ptr = ptr.Next;

            ptr.Next = ptr.Next?.Next;
        }

        /// <inheritdoc />
        public override void Clear()
        {
            _head = null;
            _rear = null;
            _size = 0;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this == (SlList<T>)obj;
        }
        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(this.Head, this.Rear, this.Size);
        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendJoin(" -> ", this);

            return sb.ToString();
        }
    }
}
