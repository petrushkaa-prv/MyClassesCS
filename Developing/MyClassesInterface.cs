using System;
using System.Collections.Generic;
using System.Text;

namespace Developing.MyClasses
{
    /// <summary>
    /// Interface of general methods to be present in all MyClasses Classes
    /// </summary>
    interface IMyClasses<T>
    {
        bool IsEmpty { get; }
        int Size { get; }

        void FillWith(T element, int howMany = 0);
        bool Contains(T element);
    }
}
