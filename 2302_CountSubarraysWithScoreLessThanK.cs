using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _2302_CountSubarraysWithScoreLessThanK
    {
        public long CountSubarrays(int[] nums, long k)
        {
            // sliding window to count the number of the array contain num at index i 
            long left = 0, right = 0;
            long sum = 0;
            long times = 0;
            while (right != nums.Length)
            {
                sum += nums[right++];
                //score can used a better calculation which only used +/- , but also sum * len is more trivial.
                while (sum * (right - left) >= k) sum -= nums[left++];
                // form index left to index right are all subarray which score is smaller than k. Just add it in result.
                times += right - left;
            }
            return times;
        }
    }
}
