using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _3277_MaximumXORScoreSubarrayQueries
    {
        public int[] MaximumSubarrayXor(int[] nums, int[][] queries)
        {
            int TotalLen = nums.Length;
            int[,] DynamicProgram = new int[TotalLen, TotalLen];
            for(int i = 0; i < TotalLen; i++) DynamicProgram[i,i] = nums[i];


            //Using DynamicProgram Method to find XOR score of every subarray, => [2,8,4] = [2,8] ^ [8,4] 
            for(int len = 2; len <= TotalLen; len++)
            {
                for (int st = 0; st + len - 1 < TotalLen; st++)
                {
                    int ed = st + len - 1;
                    DynamicProgram[st, ed] = DynamicProgram[st, ed - 1] ^ DynamicProgram[st + 1, ed];
                }
            }
            // Change the Score to Maximum XOR Score from the Subarray included in [st .. ed] 
            for(int len = 2 ; len <= TotalLen; len++)
            {
                for (int st = 0; st + len - 1 < TotalLen; st++)
                {
                    int ed = st + len - 1;
                    DynamicProgram[st, ed] = int.Max(DynamicProgram[st, ed], int.Max(DynamicProgram[st, ed-1], DynamicProgram[st + 1, ed]));
                }
            }

            int[] Ans = new int[queries.Length];
            for (int i = 0; i < queries.Length; i++) Ans[i] = DynamicProgram[queries[i][0], queries[i][1]];
            return Ans;
        }
    }
}
