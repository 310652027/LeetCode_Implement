using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _3068_FindtheMaximumSumofNodeValues
    {
        public long MaximumValueSum(int[] nums, int k, int[][] edges)
        {
            // Notice that the element numbers will cause the different situation.
            // We can't change all number elements needed to change if the number is odd.
            // So we need to delete or add a value, the MinUnChangeDiff && MinChangeDiff is set to record which kind of situation will get a minimum loss.
            long ans = 0;
            int MinDeleteChange = int.MaxValue;
            int MinAddChange = int.MaxValue;
            int ChangeCount = 0;
            foreach (int num in nums)
            {
                int XOR = num ^ k;
                if (num < XOR)
                {
                    ChangeCount++;
                    MinAddChange = int.Min(MinAddChange, XOR - num);
                    ans += XOR;
                }
                else
                {
                    MinDeleteChange = int.Min(MinDeleteChange, num - XOR);
                    ans += num;
                }
            }
            if (ChangeCount % 2 == 1) ans -= int.Min(MinAddChange, MinDeleteChange);
            return ans;
        }
    }
}
