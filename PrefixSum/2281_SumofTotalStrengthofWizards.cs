using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _2281_SumofTotalStrengthofWizards
    {
        public int TotalStrength(int[] strength)
        {
            int mod = 1000000007;
            int len = strength.Length;
            int[] PrefixSum = new int[len + 2];
            int[] PrefixSum2 = new int[len + 2];
            for (int i = 0; i < len; i++)
            {
                PrefixSum[i + 1] = (PrefixSum[i] + strength[i]) % mod;
                PrefixSum2[i + 1] = (PrefixSum2[i] + PrefixSum[i + 1]) % mod;
            }
            PrefixSum[len + 1] = (PrefixSum[len]) % mod;
            PrefixSum2[len + 1] = (PrefixSum2[len] + PrefixSum[len + 1]) % mod;


            // EachLeft && EahcRight Record the EndPoints of Longest subarray whose all elements is greater than strength[i]
            // Finding Method using monotonic stack
            int[] EachLeft = new int[len];
            int[] EachRight = new int[len];
            var stack = new Stack<int>();
            for (int i = 0; i < len; i++)
            {
                while (stack.Count > 0 && strength[stack.Peek()] > strength[i]) stack.Pop();
                EachLeft[i] = stack.Count == 0 ? 0 : stack.Peek() + 1;
                stack.Push(i);
            }
            stack.Clear();

            for (int i = len - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && strength[stack.Peek()] >= strength[i]) stack.Pop();
                EachRight[i] = stack.Count == 0 ? len - 1 : stack.Peek() - 1;
                stack.Push(i);
            }


            long Ans = 0;
            for (int i = 0; i < len; i++)
            {
                int left = EachLeft[i], right = EachRight[i];
                // find Counting from i to EndPoints
                // the Counting of subarray whose all elements is greater than strength[i] = leftCnt * rightCnt
                long leftCnt = i - left + 1;
                long rightCnt = right - i + 1;

                long sumRight = (PrefixSum2[right + 1] - PrefixSum2[i] + mod) % mod;
                long sumLeft = (PrefixSum2[i] - (left > 0 ? PrefixSum2[left - 1] : 0) + mod) % mod;
                //foreach i <= r <= right , the sum of subarrays using r to be right endpoint will be
                // = (i- left + 1) * PrefixSum[r +1] - sum of PrefixSum[l] where left <= l  <= i 
                //Hence, the sum of all subarrys we choose to protect the min number = strength[i] can be writen as
                // (i- left + 1)  * (sum of PrefixSum[r +1] where i <= r <= right) - (right - i + 1)* (sum of PrefixSum[l] where left <= l  <= i )
                // => leftCnt * sumRight - rightCnt * sumLeft

                long total = (sumRight * leftCnt % mod - sumLeft * rightCnt % mod + mod) % mod;
                Ans = (Ans + total * strength[i]) % mod;
            }
            return (int)Ans;
        }
    }
}
