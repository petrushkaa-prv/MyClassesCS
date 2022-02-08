using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Test_Chamber.Chamber
{
    internal class Sequence : IEnumerable<int>
    {
        private Random _rand;
        private int _howMuch;
        private int _min;
        private int _max;
        private int[] _array;

        internal Sequence(int howMuch,int min = 0, int max = 10,int Seed = 0)
        {
            _rand = new Random(Seed);
            _howMuch = howMuch;
            _array = new int[_howMuch];
            _max = max;
            _min = min;

            for (int i = 0; i < _array.Length; i++)
            {
                _array[i] = _rand.Next(_min, _max);
            }
        }

        /// <inheritdoc />
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < _howMuch; i++)
            {
                yield return _array[i];
            }
        }

        public static explicit operator int[](Sequence seq) => seq._array;

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public override string ToString() => string.Join(" ", _array);
    }
}
