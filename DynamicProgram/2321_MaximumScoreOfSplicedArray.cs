using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _2321_MaximumScoreOfSplicedArray
    {
        public int MaximumsSplicedArray(int[] nums1, int[] nums2)
        {
            //Calculate the diff between elements in array at index i
            //also preparing the sum
            int[] Diff_1To2 = new int[ nums1.Length];
            int sum1 =0, sum2 =0;   
            for(int i = 0; i < nums1.Length; i++)
            {
                Diff_1To2[i] = nums1[i] - nums2[i];
                sum1 += nums1[i];
                sum2 += nums2[i];
            }

            //Find Max Gain Score after switch Array
            //Using Kadane algorithm 
            int MaxGain1 = 0 , MaxGain2 = 0 ; 
            int gain1 = 0, gain2 = 0;
            for (int i = 0; i < nums1.Length; i++)
            {
                int diff = Diff_1To2[i];
                gain1 -= diff;
                gain2 += diff;
                if (gain1 < 0) gain1 = 0;
                else MaxGain1 = int.Max(MaxGain1, gain1);
                if (gain2 < 0) gain2 = 0;
                else MaxGain2 = int.Max(MaxGain2, gain2);
            }
            // the maximum will be the sum of one array add sum of switch the subarray where has the MaxGain from another array.
            return int.Max(sum1 + MaxGain1, sum2  + MaxGain2);
        }
    }
}
