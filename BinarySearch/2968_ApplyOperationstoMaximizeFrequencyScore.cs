using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _2968_ApplyOperationstoMaximizeFrequencyScore
    {
        public int MaxFrequencyScore(int[] nums, long k)
        {
            // Main Idea: Fix the RightEnd Side and find the corresponding LeftEnd Side which cost <= k
            // To speed up the process, using Binary Search at finding the left side spend time in log N;
            // Time complexity = Sort O(NlogN) +  Fix RightEnd O(N) * BinarySearch O(log N) = O(NlogN)
            int Len = nums.Length;

            //Sort Array will help to find the subArray region easily
            Array.Sort(nums);
            long[] PrefixSum = new long[Len + 1];

            //Using Prefix Sum to find to sum of subarray in Time O(1), which can accelerate when counting the cost
            for (int i = 0; i < Len; i++) PrefixSum[i + 1] = nums[i] + PrefixSum[i];

            int length = 1;
            // Setting the right end of subarray region
            for (long rightEnd = 0; rightEnd < Len; rightEnd++)
            {
                // Use Binary Search to find left side
                // The minimum cost of subaray will happened when the elements in subarray all move to the median
                // Thus, we can find the cost of subarray by finding the median , multiply by count and minus the sum in subarray;  
                long leftEnd_l = 0, leftEnd_r = rightEnd; 
                long leftEnd = leftEnd_l;
                while (leftEnd_l <= leftEnd_r)
                {
                    long leftEnd_mid = leftEnd_l + (leftEnd_r - leftEnd_l >> 1); // alternate LeftEnd 

                    // Find the Median of subarray
                    long Subarray_MedianIndex = leftEnd_mid + (rightEnd - leftEnd_mid >> 1);
                    long Subarray_Median = nums[Subarray_MedianIndex]; 

                    //long LeftCost = Subarray_Median * (Subarray_MedianIndex - leftEnd_mid + 1) - PrefixSum[Subarray_MedianIndex + 1] + PrefixSum[leftEnd_mid];
                    //long RightCost = PrefixSum[rightEnd + 1] - PrefixSum[Subarray_MedianIndex + 1] - Subarray_Median * (rightEnd - Subarray_MedianIndex);
                    //long cost = LeftCost + RightCost;

                    long cost = PrefixSum[rightEnd + 1] + PrefixSum[leftEnd_mid] - (PrefixSum[Subarray_MedianIndex + 1] << 1)
                        + Subarray_Median * ((Subarray_MedianIndex << 1) + 1 - leftEnd_mid - rightEnd);

                    if (cost <= k)
                    {
                        leftEnd = leftEnd_mid;
                        leftEnd_r = leftEnd_mid - 1;
                    }
                    else leftEnd_l = leftEnd_mid + 1;
                }
                length = int.Max(length, (int)(rightEnd - leftEnd + 1));
            }
            return length;
        }
    }
}
