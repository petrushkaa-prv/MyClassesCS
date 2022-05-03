using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Developing.GeneralInterfaces;

namespace Developing.Interfaces
{
    /// <summary>
    /// Interface to represent a basic FIFO/LIFO and similar containers
    /// </summary>
    /// <typeparam name="T">Value to be contained</typeparam>
    public interface IOrderedContainer<T> : IEnumerable<T>
    {
        /// <summary>
        /// Adds the value to the collection
        /// </summary>
        /// <param name="val">Value to be inserted</param>
        void Push(T val);

        /// <summary>
        /// Returns the value of the current top node
        /// </summary>
        T Peek { get; }

        /// <summary>
        /// Deletes the value currently on top
        /// </summary>
        /// <returns></returns>
        T Pop();

        /// <summary>
        /// The number of elements contained
        /// </summary>
        int Size { get; }
    }
}
