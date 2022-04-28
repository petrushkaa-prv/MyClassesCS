using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    /// <summary>
    /// Represents a generic interface for all the sequence modifications
    /// </summary>
    /// <typeparam name="T">Type to be modified</typeparam>
    public interface ISequenceModifier<out T>
    {
        /// <summary>
        /// Returns the next random value of the generic type
        /// </summary>
        T Next { get; }

        /// <summary>
        /// Returns an array of random values
        /// </summary>
        T[] Array { get; }
    }
}
