using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Interfaces
{
    public interface IOrderedCollection<T> : IEnumerable<T>
    {
        void Push(T val);

        T Peek { get; }

        T Pop();

        int Size { get; }
    }
}
