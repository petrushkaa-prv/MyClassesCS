using System;
using System.Collections;
using System.Collections.Generic;

namespace Developing.MyClasses
{
    public class Stack<T> : IEnumerable<T>, IMyClasses<T>
    {
        private int _counter = 0;
        public int Size => _counter;

        private Node.SinglyLinked<T> _topSinglyLinked = null;
        public Node.SinglyLinked<T> TopSinglyLinked => _topSinglyLinked;
        public T Peek => _topSinglyLinked.Value;

        public bool IsEmpty => Size == 0;

        public Stack()
        {

        }
        public Stack(T[] arr)
        {
            foreach (var val in arr)
            {
                Push(val);
            }

            _counter = arr.Length;
        }

        public void Push(T value)
        {
            this._counter++;

            Node.SinglyLinked<T> newSinglyLinked = new Node.SinglyLinked<T>(value);

            if (_topSinglyLinked == null)
            {
                _topSinglyLinked = newSinglyLinked;
                return;
            }

            newSinglyLinked.Next = _topSinglyLinked;
            _topSinglyLinked = newSinglyLinked;
        }
        public void Pop()
        {
            if (IsEmpty) return;

            _counter--;
            _topSinglyLinked = _topSinglyLinked.Next;
        }
        public Stack<T> Copy()
        {
            T[] tmp = (T[])this;
            Array.Reverse(tmp);

            return tmp;
        }

        public static explicit operator T[](Stack<T> stack)
        {
            T[] res = new T[stack.Size];

            int i = 0;
            //SinglyLinked<T> ptr = stack.TopSinglyLinked;

            foreach (var v in stack)
            {
                res[i] = v;
                i++;
            }

            Array.Reverse(res);

            return res;
        }
        public static implicit operator Stack<T>(T[] arr) => new Stack<T>(arr);
        
        public static Stack<T> operator +(Stack<T> lhs, Stack<T> rhs)
        {
            if (lhs.IsEmpty) return rhs.Copy();
            if (rhs.IsEmpty) return lhs.Copy();

            T[] lhsT = (T[])lhs, rhsT = (T[])rhs;
            T[] resT = new T[lhsT.Length + rhsT.Length];

            for (int i = 0; i < resT.Length; i++)
            {
                resT[i] = i < lhsT.Length ? lhsT[i] : rhsT[i - lhsT.Length];
            }

            return resT;
        }
        public static Stack<T> operator -(Stack<T> lhs, Stack<T> rhs)
        {
            if (lhs.IsEmpty) return rhs.Copy();
            if (rhs.IsEmpty) return lhs.Copy();

            Stack <T> res = new Stack<T>();

            foreach (var lhsV in lhs)
            {
                bool met = false;

                foreach (var rhsV in rhs)
                {
                    if ((dynamic)lhsV == (dynamic)rhsV) met = true;
                }

                if(!met) res.Push(lhsV);
            }

            return res;
        }
        public static bool operator ==(Stack<T> lhs, Stack<T> rhs)
        {
            //if (IsNull(lhs) || IsNull(rhs)) return false;
            if (lhs.Size != rhs.Size) return false;

            Node.SinglyLinked<T> rptr = rhs.TopSinglyLinked;

            foreach (var v in lhs)
            {
                if ((dynamic)v != (dynamic)rptr.Value) return false;

                rptr = rptr.Next;
            }

            return true;
        }
        public static bool operator !=(Stack<T> lhs, Stack<T> rhs)
        {
            return !(lhs == rhs);
        }

        private T this[int idx]
        {
            get
            {
                if(idx > this.Size || idx < 0) throw new IndexOutOfRangeException();

                return ((T[])this)[idx];
            }
            set
            {
                if (idx > this.Size || idx < 0) throw new IndexOutOfRangeException();
                //if (IsEmpty || IsNull(this)) throw new Exception();

                Node.SinglyLinked<T> tmp = TopSinglyLinked;

                int i = 0;
                while (tmp != null && i != idx)
                {
                    tmp = tmp.Next;
                    i++;
                }

                tmp.Value = value;
            }
        }

        public Stack<T> Sort()
        {
            var res = (T[])this;

            Array.Sort(res);

            return res;


            //var res = this.Copy();

            //for (int i = 0; i < Size - 1; i++)
            //{
            //    int idxMin = i;

            //    for (int j = i + 1; j < Size; j++)
            //    {
            //        if ((dynamic)res[j] > (dynamic)res[idxMin]) idxMin = j;
            //    }

            //    if (idxMin != i)
            //    {
            //        (res[idxMin], res[i]) = (res[i], res[idxMin]);
            //    }
            //}

            //return res;
        }
        
        /// <inheritdoc />
        void IMyClasses<T>.FillWith(T element, int howMany)
        {
            if(IsEmpty) return;

            for (int i = 0; i < howMany; i++)
            {
                Push(element);
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return this == (Stack<T>)obj;
        }

        /// <inheritdoc />
        public override int GetHashCode() => HashCode.Combine(this._topSinglyLinked, this._counter);

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Join(" -> ", (T[])this);
        }

        // version 3 (see SinglyLinked class)
        public IEnumerator<T> GetEnumerator()
        {
            return _topSinglyLinked.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}





// version 2 (main functionality in Stack class)
//class StackEnumerator : IEnumerator
//{
//    private T[] stack;
//    private int position = -1;
//    public StackEnumerator(SinglyLinked<T> SinglyLinked, int size)
//    {
//        stack = new T[size];
//        SinglyLinked<T> temp = SinglyLinked; 
//        for (int i = 0; i < size && temp != null; i++)
//        {
//            stack[i] = temp.value;
//            temp = temp.next;
//        }
//    }
//    object IEnumerator.Current
//    {
//        get
//        {
//            if (position == -1 || position >= stack.Length)
//                throw new InvalidOperationException();
//            return stack[position];
//        }
//    }
//    public bool MoveNext()
//    {
//        if (position < stack.Length - 1)
//        {
//            position++;
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }
//    public void Reset()
//    {
//        position = -1;
//    }
//}
//public IEnumerator GetEnumerator()
//{
//    return new StackEnumerator(topSinglyLinked, counter);
//}
// version 1 (using yield)
//public IEnumerable<T> GetAll()
//{
//    SinglyLinked<T> temp = topSinglyLinked;
//    for (int i = 0; i < counter; i++)
//    {
//        yield return temp.value;
//        temp = temp.next;
//    }
//}