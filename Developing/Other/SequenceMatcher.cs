using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Other
{
    internal class SequenceMatcher
    {
        private readonly string _sequence1;
        private readonly string _sequence2;

        private readonly int _matchValue;
        private readonly int _misMatchValue;
        private readonly int _gapValue;

        protected readonly string Result1;
        protected readonly string Result2;
        protected readonly int CheckSumValue;

        enum Direction
        {
            Left,
            Up,
            LeftUp
        }

        public SequenceMatcher(string seq1, string seq2, int matchValue = 1, int misMatchValue = -3, int gapValue = -2)
        {
            _sequence1 = seq1;
            _sequence2 = seq2;
            _matchValue = matchValue;
            _misMatchValue = misMatchValue;
            _gapValue = gapValue;

            (Result1, Result2, CheckSumValue) = FindMatching(_sequence1, _sequence2, _gapValue, _matchValue, _misMatchValue);
        }

        public static (string matchingSeq1, string matchingSeq2, int bestMatchingValue) FindMatching(string seq1, string seq2, int gapValue, int matchValue, int mismatchValue)
        {
            int m = seq1.Length, n = seq2.Length;

            var table = new (int val, Direction dir)[m + 1, n + 1];
            //const short left = 0, up = 1, leftUp = 2;      // directions

            for (int i = 1; i <= m; i++)
            {
                table[i, 0] = (i * gapValue, Direction.Left);
            }

            for (int i = 1; i <= n; i++)
            {
                table[0, i] = (i * gapValue, Direction.Up);
            }

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {

                    int tlu = table[i - 1, j - 1].val + (seq1[i - 1] == seq2[j - 1] ? matchValue : mismatchValue);

                    int tu = table[i, j - 1].val + gapValue;

                    int tl = table[i - 1, j].val + gapValue;

                    if (tlu >= tu)
                    {
                        table[i, j] = tlu >= tl ? (tlu, left_up: Direction.LeftUp) : (tl, Direction.Left);
                    }
                    else
                        table[i, j] = tu >= tl ? (tu, Direction.Up) : (tl, Direction.Left);
                }
            }

            int k = m, t = n;
            var backtrace = new LinkedList<(int i, int j)>();

            while (k != 0 || t != 0)
            {
                backtrace.AddLast((t, k));

                switch (table[k, t].dir)
                {
                    case Direction.LeftUp:
                        {
                            k--;
                            t--;
                        }
                        break;
                    case Direction.Left:
                        {
                            k--;
                        }
                        break;
                    case Direction.Up:
                        {
                            t--;
                        }
                        break;

                }
            }

            var res1 = new char[backtrace.Count];
            var res2 = new char[backtrace.Count];

            k = 0;
            foreach (var p in backtrace)
            {
                switch (table[p.j, p.i].dir)
                {
                    case Direction.LeftUp:
                        {
                            res1[k] = seq1[p.j - 1];
                            res2[k] = seq2[p.i - 1];

                            break;
                        }
                    case Direction.Left:
                        {
                            res1[k] = seq1[p.j - 1];
                            res2[k] = '-';

                            break;
                        }
                    case Direction.Up:
                        {
                            res1[k] = '-';
                            res2[k] = seq2[p.i - 1];

                            break;
                        }
                }

                k++;
            }

            Array.Reverse(res1);
            Array.Reverse(res2);

            return (new string(res1), new string(res2), table[m, n].val);
        }

        public void Deconstruct(out string match1, out string match2, out int bestMatchingValue)
        {
            match1 = Result1;
            match2 = Result2;
            bestMatchingValue = CheckSumValue;
        }

        /// <inheritdoc />
        public override string ToString() => $"First sequence: \t{_sequence1}\nSecond sequence: \t{_sequence2}\nFirst after match: \t{Result1}\nSecond after match: \t{Result2}\nBest matching value: \t{CheckSumValue}";


    }
}
