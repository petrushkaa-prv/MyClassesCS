using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.GeneralInterfaces
{
    /// <summary>
    /// Interface of general methods to be present in all container classes of [MyClassesCS]
    /// </summary>
    public interface IMyCollection<T> : IEnumerable<T>
    {
        /// <summary>
        /// Tells if the collection doesn't contain any elements
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Returns the size(count) of elements currently contained by the collection
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Searches for a specific value in the collection
        /// </summary>
        /// <param name="element">Searched value</param>
        /// <returns>If value found returns true, and vise versa</returns>
        public bool Contains(T element);

        /// <summary>
        /// Detaches all elements currently contained in the collection
        /// </summary>
        public void Clear();
    }
}
