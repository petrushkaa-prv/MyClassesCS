using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Developing.MyClasses
{
    /// <summary>
    /// Interface of general methods to be present in all MyClasses Classes
    /// </summary>
    public interface IMyClasses<T>
    {
        bool IsEmpty { get; }
        int Size { get; }

        void FillWith(T element, int howMany = 0);
        bool Contains(T element);
    }

    public interface IStackLike<T>
    {
        void Push(T elem);
        void Pop();
        public T Peek {get;}
    }
}
