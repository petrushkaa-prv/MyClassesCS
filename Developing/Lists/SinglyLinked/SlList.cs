using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.Interfaces;
using Developing.Nodes;

namespace Developing.Lists
{
    public class SlList<T> : IMyList<T>, IMyClasses<T>, IEnumerable<T>
    {
        public int Size { get; private set; }

        public SlNode<T> Head { get; private set; }
        public SlNode<T> Rear { get; private set; }

        public T Front => IsEmpty ? throw new InvalidOperationException() : Head.Value;
        public T Back => IsEmpty ? throw new InvalidOperationException() : Rear.Value;

        public bool IsEmpty => Head is null || Rear is null || Size == 0;

        public SlList()
        {
            Size = 0;
            Head = null;
            Rear = null;
        }
        public SlList(params T[] arr)
        {
            foreach (var val in arr)
            {
                this.AddEnd(val);
            }
        }

        public void AddFront(T val)
        {
            Size++;

            var newNode = new SlNode<T>(val);

            if (IsEmpty)
            {
                Head = newNode;
                Rear = newNode;

                return;
            }

            newNode.Next = Head;
            Head = newNode;
        }
        public void AddEnd(T val)
        {
            Size++;

            var newNode = new SlNode<T>(val);

            if (IsEmpty)
            {
                Head = newNode;
                Rear = newNode;

                return;
            }

            Rear.Next = newNode;
            Rear = newNode;
        }

        public void RemoveFront()
        {
            if (IsEmpty)
            {
                return;
            }

            Size--;

            Head = Head.Next;
        }
        public void RemoveEnd()
        {
            if (IsEmpty)
            {
                return;
            }

            Size--;

            var ptr = Head;
            while (ptr.Next is not null && ptr.Next != Rear)
            {
                //if (ptr.Next == Rear)
                //{
                //    ptr.Next = null;
                //    Rear = ptr;
                //    return;
                //}

                ptr = ptr.Next;
            }

            ptr.Next = null;
            Rear = ptr;
        }
        public void Remove(T val)
        {
            if(Head is null) return;
            if (Head.Value == (dynamic)val)
            {
                this.RemoveFront();
                return;
            }
            if (Rear.Value == (dynamic)val)
            {
                this.RemoveEnd();
                return;
            }
            
            var ptr = Head;
            
            while (ptr!.Next is not null && ptr.Next.Value != (dynamic)val)
            {
                ptr = ptr.Next;
            }

            ptr.Next = ptr.Next?.Next;
        }

        /// <inheritdoc />
        public bool Contains(T element)
        {
            foreach (var el in this)
            {
                if (element == (dynamic)el) return true;
            }

            return false;
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

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
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
    }
}
