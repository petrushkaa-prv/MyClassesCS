using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chamber
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

        public T Next => _sequence.Next;
        public T[] Array => _sequence.Array;

        public IEnumerator<T> GetEnumerator()
        {
            var arr = this.Array;

            if(Array.Length <= 0) yield break;

            foreach (var el in arr)
                yield return el;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            return _sequence.ToString();
        }
    }

    public interface ISequenceModifier<out T>
    {
        T Next { get; }

        T[] Array { get; }
    }

    public class SequenceModifier<T> : ISequenceModifier<T>
    {
        private protected readonly Random Random;
        private readonly int _count;

        protected SequenceModifier(int count, int seed = 0)
        {
            _count = count;

            Random = new Random(seed);
        }

        public virtual T Next => (dynamic)Random.Next();
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

    public class FloatSequence : SequenceModifier<float>
    {
        public FloatSequence(int count, int seed = 0) : 
            base(count, seed)
        {
        }

        public override float Next => Random.NextSingle();
    }

    public class DoubleSequence : SequenceModifier<double>
    {
        public DoubleSequence(int count, int seed = 0) :
            base(count, seed)
        {
        }

        public override double Next => Random.NextDouble();
    }

    public class IntegerSequence : SequenceModifier<int>
    {
        private readonly int _min;
        private readonly int _max;

        public IntegerSequence(int count, int min = 0, int max = 100, int seed = 0) : base(count, seed)
        {
            _min = min;
            _max = max;
        }

        public override int Next => base.Random.Next(_min, _max);
    }

    public class CharSequence : SequenceModifier<char>
    {
        private const int MinAsciiChar = 33;
        private const int MaxAsciiChar = 126;

        public CharSequence(int count, int seed = 0) : base(count, seed)
        {
        }

        public override char Next => (char)base.Random.Next(MinAsciiChar, MaxAsciiChar);
        
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

    public class StringSequence : SequenceModifier<string>
    {
        private readonly CharSequence _charSequence;

        public StringSequence(int count, int stringLength, int seed = 0) : base(count, seed)
        {
            _charSequence = new CharSequence(stringLength, seed);
        }

        public override string Next => _charSequence.ToString();
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