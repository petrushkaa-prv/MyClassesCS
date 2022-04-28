using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    public class DoubleSequence : SequenceModifier<double>
    {
        public DoubleSequence(int count, int seed = 0) :
            base(count, seed)
        {
        }

        /// <inheritdoc/>
        public override double Next => Random.NextDouble();
    }
}
