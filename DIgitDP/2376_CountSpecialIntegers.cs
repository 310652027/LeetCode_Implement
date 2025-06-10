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

            // AllMask will record how many digit we using by adding 1 << i into AllMask where 0 <= i <= 9
            int AllMask = 0;
            // Using dicitionary to record the conditions which may show up again, helping us to reduce the calculation.
            Dictionary<long, int> FindingResult = new Dictionary<long, int>();

            int DigitDp(int zeroTrail, int UpperBound, int index, int num)
            {
                //Since AllMask < 10 ^ 10, we build the key of Dictionary to a long number by BitMask to squeeze the space 
                long key = (index << 13) | (UpperBound << 12) | (zeroTrail << 11) | AllMask;

                // If calculating the same situation before, just return the answer we record
                // else if the index reach at the end, return the count itself unless it is zero;
                if (FindingResult.ContainsKey(key)) return FindingResult[key];
                if (index == totalIndex) return zeroTrail == 1 ? 0 : 1;

                int count = 0;
                int upperbound = UpperBound == 1 ? nSpilt[index] - '0' : 9;
                for (int i = 0; i <= upperbound; i++)
                {
                    if (((AllMask >> i) & 1) == 0) // Using Mask to skip the repeated element i
                    {
                        int AllMaskBefore = AllMask;
                        int N_zeroTrail = 0;

                        if (zeroTrail != 1 || i != 0) AllMask += (1 << i);
                        else N_zeroTrail++;
                        
                        int N_UpperBound = (UpperBound == 1 && i == upperbound) ? 1 : 0;
                        // Adding the count from Next Level 
                        count += DigitDp(N_zeroTrail, N_UpperBound, index + 1, num * 10 + i);
                        AllMask = AllMaskBefore;
                    }
                }
                //Fill the result into Dictionary 
                FindingResult[key] = count;
                return count;
            }
            return DigitDp(1, 1, 0, 0);
        }
    }
}
