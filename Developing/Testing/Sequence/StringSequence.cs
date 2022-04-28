using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    public class StringSequence : SequenceModifier<string>
    {
        private readonly CharSequence _charSequence;

        public StringSequence(int count, int stringLength, int seed = 0) : base(count, seed)
        {
            _charSequence = new CharSequence(stringLength, seed);
        }

        /// <inheritdoc/>
        public override string Next => _charSequence.ToString();
    }
}
