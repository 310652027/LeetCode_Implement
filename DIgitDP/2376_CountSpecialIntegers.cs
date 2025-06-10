using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Emgu.Util.Platform;

namespace HTR
{
    internal class _2376_CountSpecialIntegers
    {
        public int CountSpecialNumbers(int n)
        {
            char[] nSpilt = n.ToString().ToCharArray();
            int totalIndex = nSpilt.Length;
            int AllMask = 0;
            Dictionary<long, int> FindingResult = new Dictionary<long, int>();

            int DigitDp(int zeroTrail, int UpperBound, int index, int num)
            {
                long key = (index << 12) | (UpperBound << 11) | (zeroTrail << 10) | AllMask;

                if (FindingResult.ContainsKey(key)) return FindingResult[key];
                if (index == totalIndex) return zeroTrail == 1 ? 0 : 1;

                int count = 0;
                int upperbound = UpperBound == 1 ? nSpilt[index] - '0' : 9;
                for (int i = 0; i <= upperbound; i++)
                {
                    if (zeroTrail == 1 && i == 0)
                    {
                        int N_zeroTrail = 1;
                        int N_UpperBound = (UpperBound == 1 && i == upperbound) ? 1 : 0;
                        count += DigitDp(N_zeroTrail, N_UpperBound, index + 1, num * 10);
                        continue;
                    }

                    if (((AllMask >> i) & 1) == 0)
                    {
                        AllMask += (1 << i);
                        int N_zeroTrail = 0;
                        int N_UpperBound = (UpperBound == 1 && i == upperbound) ? 1 : 0;
                        count += DigitDp(N_zeroTrail, N_UpperBound, index + 1, num * 10 + i);
                        AllMask -= (1 << i);
                    }
                }
                FindingResult[key] = count;
                return count;
            }
            return DigitDp(1, 1, 0, 0);
        }
    }
}
