using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Testing
{
    public class Sequence<T> : ISequenceModifier<T>, IEnumerable<T>
    {
        private ISequenceModifier<T> _sequence;

        private readonly int _count;
        private readonly int _min;
        private readonly int _max;
        private readonly int _strLength;
        private int _seed;

        public Sequence(int count = 10, int min = 0, int max = 10, int strLength = 0, int seed = 0)
        {
            _count = count;
            _min = min;
            _max = max;
            _seed = seed;
            
            Initialize(_count, _min, _max, _seed, _strLength = Type.GetTypeCode(typeof(T)) == TypeCode.String ? strLength : 0);
        }

        private void Initialize(int count = 10, int min = 0, int max = 10, int seed = 0, int strLength = 0)
        {
            _sequence = Type.GetTypeCode(typeof(T)) switch
            {
                TypeCode.Int32 => new IntegerSequence(count, min, max, seed),
                TypeCode.Single => (dynamic)new FloatSequence(count, seed),
                TypeCode.Char => new CharSequence(count, seed),
                TypeCode.String => new StringSequence(count, strLength, seed),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void Reset()
        {
            _seed = DateTime.Now.Millisecond;

            Initialize(_count, _min, _max, _seed, _strLength);
        }
        public void Reset(int newSeed)
        {
            _seed = newSeed;

            Initialize(_count, _min, _max, _seed);
        }

        /// <inheritdoc/>
        public T Next => _sequence.Next;

        /// <inheritdoc/>
        public T[] Array => _sequence.Array;

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            var arr = this.Array;

            if(Array.Length <= 0) yield break;

            foreach (var el in arr)
                yield return el;
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc/>
        public override string ToString()
        {
            return _sequence.ToString();
        }
    }
}



//internal class Sequence : IEnumerable<int>
//{
//    private Random _rand;
//    private int _howMuch;
//    private int _min;
//    private int _max;
//    internal Sequence(int howMuch, int min = 0, int max = 10,int seed = 0)
//    {
//        _rand = new Random(seed);
//        //_array = new int[_howMuch = howMuch];
//        _max = max;
//        _min = min;
//        //for (int i = 0; i < _array.Length; i++)
//        //{
//        //    _array[i] = _rand.Next(_min, _max);
//        //}
//    }
//    /// <inheritdoc />
//    public IEnumerator<int> GetEnumerator()
//    {
//        for (int i = 0; i < _howMuch; i++)
//        {
//            yield return _array[i];
//        }
//    }
//    public static implicit operator int[](Sequence seq) => seq._array;
//    /// <inheritdoc />
//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }
//    /// <inheritdoc />
//    public override string ToString() => string.Join(" ", _array);
//}