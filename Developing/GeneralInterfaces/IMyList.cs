using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.GeneralInterfaces
{
    public interface IMyList<T>
    {
        public T Front { get; }
        public T Back { get; }

        public void AddFront(T val);
        public void AddEnd(T val);

        public void RemoveFront();
        public void RemoveEnd();
    }
}
