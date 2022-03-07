using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Developing.Other
{
    internal class SequenceMatcher
    {
        protected readonly string Sequence1;
        protected readonly string Sequence2;

        protected int MatchValue;
        protected int MisMatchValue;
        protected int GapValue;

        protected string Result1;
        protected string Result2;
        protected int CheckSumValue;

        public SequenceMatcher(string seq1, string seq2, int matchValue = 1, int misMatchValue = -3, int gapValue = -2)
        {
            Sequence1 = seq1;
            Sequence2 = seq2;
            MatchValue = matchValue;
            MisMatchValue = misMatchValue;
            GapValue = gapValue;

            (Result1, Result2, CheckSumValue) = FindMatching(Sequence1, Sequence2, GapValue, MatchValue, MisMatchValue);
        }

        public static (string matchingSeq1, string matchingSeq2, int bestMatchingValue) FindMatching(string seq1, string seq2, int gapValue, int matchValue, int mismatchValue)
        {
            int m = seq1.Length, n = seq2.Length;

            var table = new (int val, int dir)[m + 1, n + 1];
            const short left = 0, up = 1, left_up = 2;      // directions

            for (int i = 1; i <= m; i++)
            {
                table[i, 0] = (i * gapValue, left);
            }

            for (int i = 1; i <= n; i++)
            {
                table[0, i] = (i * gapValue, up);
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
                        table[i, j] = tlu >= tl ? (tlu, left_up) : (tl, left);
                    }
                    else
                        table[i, j] = tu >= tl ? (tu, up) : (tl, left);
                }
            }

            int k = m, t = n;
            var backtrace = new LinkedList<(int i, int j)>();

            while (k != 0 || t != 0)
            {
                backtrace.AddLast((t, k));

                switch (table[k, t].dir)
                {
                    case left_up:
                        {
                            k--;
                            t--;
                        }
                        break;
                    case left:
                        {
                            k--;
                        }
                        break;
                    case up:
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
                    case left_up:
                        {
                            res1[k] = seq1[p.j - 1];
                            res2[k] = seq2[p.i - 1];

                            break;
                        }
                    case left:
                        {
                            res1[k] = seq1[p.j - 1];
                            res2[k] = '-';

                            break;
                        }
                    case up:
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

        public void Deconstruct(out string Match1, out string Match2, out int BestMatchingValue)
        {
            Match1 = Result1;
            Match2 = Result2;
            BestMatchingValue = CheckSumValue;
        }

        /// <inheritdoc />
        public override string ToString() => $"First sequence: {Sequence1}\tAfter match: {Result1}\nSecond sequence: {Sequence2}\tAfter match: {Result2}\nBest matching value: {CheckSumValue}";
    }
}
