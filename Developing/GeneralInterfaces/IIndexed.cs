using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.GeneralInterfaces
{
    public interface IIndexed<TData>
    {
        public TData this[int key] { get; set; }
    }

    public static class IndexWrapper
    {
        private class IndexHelper<T> : IIndexed<T>
        {
            private T[] _data;

            public IndexHelper(T[] data)
            {
                _data = data;
            }



            /// <inheritdoc />
            public T this[int key]
            {
                get => _data[key];
                set => _data[key] = value;
            }
        }

        public static IIndexed<T> Array2Indexed<T>(T[] arr)
        {
            return new IndexHelper<T>(arr);
        }
    }
}

//public static class IndexWrapper
//{
//    private class IndexHelper<TValue, TCollection> : IIndexed<TValue>
//    {
//        private TCollection _data;

//        public IndexHelper(TCollection data)
//        {
//            _data = data;
//        }

//        private TValue? _tempValue;
//        private TValue? Getter(int key)
//        {
//            if (_data is IIndexed<TValue> indexed)
//            {
//                return indexed[key];
//            }

//            if (_data is IMyCollection<TValue> myList)
//            {
//                if (myList.IsEmpty)
//                {
//                    return default;
//                }

//                int i = 0;
//                foreach (var val in myList)
//                {
//                    if (i++ == key)
//                        return val;
//                }
//            }

//            return default;
//        }

//        /// <inheritdoc />
//        public TValue this[int key]
//        {
//            get => Getter(key);
//            set => Getter(key) = value;
//        }
//    }
