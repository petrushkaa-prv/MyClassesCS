using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    public class FloatSequence : SequenceModifier<float>
    {
        public FloatSequence(int count, int seed = 0) :
            base(count, seed)
        {
        }

        /// <inheritdoc/>
        public override float Next => Random.NextSingle();
    }
}
