using System;

namespace CS_Test_Chamber.Developing.Arrays
{
    public class Dictionary<TKey, TValue> : Vector<(TKey key, TValue value)>
        where TKey : struct
        where TValue : IComparable<TValue>
    {
        public Dictionary():
            base()
        {

        }
        public Dictionary(params (TKey, TValue)[] arr)
        {
            Count = arr.Length;
            Size = 2 * Count;
            _vector = new (TKey, TValue)[Size];

            for (int i = 0; i < arr.Length; i++)
            {
                _vector[i] = arr[i];
            }
        }
        public Dictionary(TKey[] keys, TValue[] values)
        {
            if (keys.Length < values.Length) 
                throw new Exception("Can't combine values to not existing values ");
            Count = keys.Length;
            Size = 2 * Count;
            _vector = new (TKey, TValue)[Size];

            for (int i = 0; i < keys.Length; i++)
            {
                _vector[i] = i < values.Length ? (keys[i], values[i]) : (keys[i], default);
            }
        }

        /// <inheritdoc />
        public override void Push((TKey key, TValue value) el)
        {
            Extend();

            for (int i = 0; i < Count; i++)
            {
                if ((dynamic)base._vector[i].key == (dynamic)el.key)
                {
                    _vector[i].value = el.value;
                    if (i + 1 <= Count)
                    {
                        (_vector[i], _vector[i + 1]) = (_vector[i + 1], _vector[i]);
                    }
                    return;
                }
            }

            _vector[Count++] = el;
        }
        public void Push(TKey key, TValue value)
        {
            Push((key, value));
        }

        public void Deconstruct(out TKey[] keys, out TValue[] values)
        {
            keys = new TKey[this.Count];
            values = new TValue[this.Count];

            for (int i = 0; i < Count; i++)
            {
                keys[i] = _vector[i].key;
                values[i] = _vector[i].value;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var res = string.Empty;

            foreach (var (key, value) in this)
            {
                res += "{" + key + "," + value + "} ";
            }

            return res;
        }
    }
}