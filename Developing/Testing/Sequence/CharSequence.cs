using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    public class CharSequence : SequenceModifier<char>
    {
        private const int MinAsciiChar = 33;
        private const int MaxAsciiChar = 126;

        public CharSequence(int count, int seed = 0) : base(count, seed)
        {
        }

        /// <inheritdoc/>
        public override char Next => (char)base.Random.Next(MinAsciiChar, MaxAsciiChar);

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var c in Array)
            {
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
