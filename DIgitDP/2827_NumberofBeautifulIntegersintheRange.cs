using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Emgu.Util.Platform;

namespace HTR
{
    internal class _2827_NumberofBeautifulIntegersintheRange
    {
        //Main idea from using digit Dp to find the corresponding num in range 0 ~ low - 1 and 0 ~ high
        //We need to initial the arrays : digits(the upperbound of each position) & memo ( to memorize the result we calculate the same conditions in digit Dp)

        public int NumberOfBeautifulIntegers(int low, int high, int k)
        {
            int len = 0;
            int[] digits = new int[0];
            int[] memo = new int[0];

            //DFS will check the conditions : position , diff of oddCount And evenCount , 
            // reminder to k , and bools tight & leadingzero
            int DFS(int pos, int diff, int rem, int tight, int lead)
            {
                //Check the statue when we fill all positions
                if (pos == len) return (rem == 0 && diff == 0 && lead == 0) ? 1 : 0; 

                //Check whether we calculate the same condition already, if true , just return it 
                int key = (pos << 11) | (rem << 6) | (tight << 5) | (lead << 4) | (diff + len);
                if (memo[key] != -1) return memo[key];

                int up = tight == 1 ? digits[pos] : 9;
                int ans = 0;

                for (int d = 0; d <= up; d++)
                {
                    int nLead = (lead == 1 && d == 0) ? 1 : 0;
                    int nDiff = nLead == 1 ? diff : diff + ((d & 1) == 1 ? 1 : -1);
                    int nRem = (rem * 10 + d) % k;
                    int nTight = (tight == 1 && d == up) ? 1 : 0;

                    ans += DFS(pos + 1, nDiff, nRem, nTight, nLead);
                }

                memo[key] = ans;

                return ans;
            }
            int Solve(int value)
            {
                if (value == 0) return 0;         

                len = 0;
                int copy = value;
                while (copy > 0)
                {
                    copy /= 10;
                    len++;
                }
                digits = new int[len];
                for (int i = len - 1; i >= 0; i--)
                {
                    digits[i] = value % 10;
                    value /= 10;
                }
                len = digits.Length;

                int upperBound_Memo = (len << 11) | ((k - 1) << 6) | 32 | 16 | 2;
                memo = new int[upperBound_Memo];
                Array.Fill(memo, -1);
                return DFS(0, 0, 0, 1, 1);
            }

            return (Solve(high) - Solve(low - 1));
        }
    }
}
