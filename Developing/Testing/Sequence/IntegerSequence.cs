using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    public class IntegerSequence : SequenceModifier<int>
    {
        private readonly int _min;
        private readonly int _max;

        public IntegerSequence(int count, int min = 0, int max = 100, int seed = 0) : base(count, seed)
        {
            _min = min;
            _max = max;
        }

        /// <inheritdoc/>
        public override int Next => base.Random.Next(_min, _max);
    }
}
