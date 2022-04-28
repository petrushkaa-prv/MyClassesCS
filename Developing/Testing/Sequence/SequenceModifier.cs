using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    public class SequenceModifier<T> : ISequenceModifier<T>
    {
        private protected readonly Random Random;
        private readonly int _count;

        protected SequenceModifier(int count, int seed = 0)
        {
            _count = count;

            Random = new Random(seed);
        }

        /// <inheritdoc/>
        public virtual T Next => (dynamic)Random.Next();

        /// <inheritdoc/>
        public virtual T[] Array
        {
            get
            {
                var array = new T[_count];

                for (int i = 0; i < _count; i++)
                {
                    array[i] = Next;
                }

                return array;
            }
        }
    }
}
