using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chamber
{
    public class Sequence
    {
        public readonly int Count;

        public Sequence(int count)
        {
            Count = count;
        }

        public int[] Integers(int min = 0, int max = 100) => new IntegerSequence(Count, min, max).Array;
        public float[] Floats() => new FloatSequence(Count).Array;
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
                T[] array = new T[_count];

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

        public override float Next => base.Random.NextSingle();

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