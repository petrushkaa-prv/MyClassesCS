using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.GeneralInterfaces
{
    /// <summary>
    /// Represents a generic interface for lists in [MyClassesCS]
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    public interface IMyList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Returns the value contained at the Head(Front) of the list
        /// </summary>
        public T Front { get; }

        /// <summary>
        /// Returns the value contained at the Tail(Back) of the list
        /// </summary>
        public T Back { get; }

        /// <summary>
        /// Inserts a value at the beginning of the list 
        /// </summary>
        /// <param name="val">Value to be added</param>
        public void AddFront(T val);

        /// <summary>
        /// Inserts a value at the end of the list 
        /// </summary>
        /// <param name="val">Value to be added</param>
        public void AddEnd(T val);

        /// <summary>
        /// Deletes the front node and the value it contains
        /// </summary>
        public void RemoveFront();

        /// <summary>
        /// Deletes the end(tail) node and the value it contains
        /// </summary>
        public void RemoveEnd();
    }
}
