using System;
using System.Collections.Generic;
using System.Text;
using Developing.Arrays;

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

        /// <inheritdoc />
        public override void Push((TKey key, TValue value) el)
        {
            Extend();

            for (int i = 0; i < _count; i++)
            {
                if ((dynamic)base._vector[i].key == (dynamic)el.key)
                {
                    _vector[i].value = el.value;
                    if (i + 1 <= _count)
                    {
                        (_vector[i], _vector[i + 1]) = (_vector[i + 1], _vector[i]);
                    }
                    return;
                }
            }

            _vector[_count++] = el;
        }

        public void Push(TKey key, TValue value)
        {
            Push((key, value));
        }
    }
}
