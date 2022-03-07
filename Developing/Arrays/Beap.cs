using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace Developing.Arrays
{
    internal class Beap<T>
    {
        private T[] _beap; // _beap[0] is a sentinel
        private int _index;

        public Beap()
        {
            _beap = new T[3];
            _beap[0] = default;
            _index = 1;
        }
        public Beap(params T[] arr)
        {

        }

        private void Extend()
        {
            var temp = _beap;
            _beap = new T[2 * temp.Length];

            for (int i = 0; i < temp.Length; i++)
            {
                _beap[i] = temp[i];
            }
        }

        public static void UpBeap(T[] arr, int k)
        {
            T tempVal = arr[k];
            int i = (int)(Math.Ceiling(0.5 * (-1 + Math.Sqrt(1.0 + 8 * k))));
            int j = (int)(k - 0.5 * i * (i - 1));

            int k1 = k;

            while (i > 1)
            {
                if (j == 1)
                {
                    k = k - i + 1;
                }
                else if (j == i)
                {
                    k = k - i;
                    j--;
                }
                else
                {
                    k = k - i;
                    j--;
                    if (arr[k + 1] < (dynamic)arr[k])
                    {
                        k++;
                        j++;
                    }
                }

                if (arr[k] < (dynamic)tempVal)
                {
                    arr[k1] = arr[k];
                    k1 = k;
                    i--;
                }
                else
                {
                    break;
                }
            }

            arr[k1] = tempVal;
        }
        public static void DownBeap(T[] arr, int k, int kMax)
        {
            T tempVal = arr[k];
            int i = (int)(Math.Ceiling(0.5 * (-1 + Math.Sqrt(1.0 + 8 * k))));

            int k1 = k;
            k = k + i;
            i++;
            while (k <= kMax)
            {
                if (k < kMax)
                {
                    if (arr[k + 1] > (dynamic)arr[k])
                    {
                        k++;
                    }
                }

                if (arr[k] > (dynamic)tempVal)
                {
                    //arr[k1] = arr[k];
                    //k1 = k;
                    //k += i;
                    //i++:
                }
                else
                {
                    break;
                }
            }

            arr[k1] = tempVal;
        }

        //public int Search(T value)
        //{
        //    int h = 0;
        //    while(h * (h + 1)/2 < h)
        //}

        public void Insert(T value)
        {
            if(++_index >= _beap.Length)
                Extend();

            _beap[_index] = value;
            UpBeap(_beap, _index);
        }
    }
}
