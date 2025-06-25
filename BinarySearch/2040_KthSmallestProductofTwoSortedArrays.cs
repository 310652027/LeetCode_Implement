using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _2040_KthSmallestProductofTwoSortedArrays
    {
        //Main Idea: There are three Binary Seacrh in the program
        //1. first bs will be used to find the first nonegative value in array
        //2. second bs will find the corresponding value
        //3. the third bs will stay in the second bs , helping it to check the value is corresponding or not, that's, the count is enough for k
        int Len1 = 0, Len2 = 0;
        public long KthSmallestProduct(int[] nums1, int[] nums2, long k)
        {
            long left = -10000000000L, right = 10000000000L;
            Len1 = nums1.Length;
            Len2 = nums2.Length;
            int pos1 = BinarySearch_Find0(nums1), pos2 = BinarySearch_Find0(nums2);

            // Second BinarySearch to find the value
            while (left < right)
            {
                long mid = left + (right - left) / 2;
                if (CountLessEqual(nums1, nums2, mid, pos1, pos2) >= k) right = mid;
                else left = mid + 1;
            }
            return left;
        }

        //Find the first nonegative value in array by Binarysearch
        int BinarySearch_Find0(int[] arr)
        {
            int left = 0, right = arr.Length;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (arr[mid] < 0) left = mid + 1;
                else right = mid;
            }
            return left; 
        }
        // Using Binary Search to find the coressponding pair's count in 4 situations 
        long CountLessEqual(int[] A, int[] B, long mid, int pos1 , int pos2)
        {
            long count = 0;
            int index1 = 0, index2 = pos2 - 1;
            //Negative * Negtive 
            while (index1 < pos1 && index2 >= 0)
            {
                if ((long)A[index1] * B[index2] > mid) index1++;
                else
                {
                    count += pos1 - index1;
                    index2--;
                }
            }
            //Postive * Postive 
            index1 = pos1;
            index2 = Len2 - 1;
            while (index1 < Len1 && index2 >= pos2)
            {
                if ((long)A[index1] * B[index2] > mid) index2--;
                else
                {
                    count += index2 - pos2 + 1;
                    index1++;
                }
            }
            //Negative * Postive 
            index1 = 0;
            index2 = pos2;
            while (index1 < pos1 && index2 < Len2)
            {
                if ((long)A[index1] * B[index2] > mid) index2++;
                else
                {
                    count += Len2 - index2;
                    index1++;
                }
            }
            //Positive * Negative 
            index1 = pos1;
            index2 = 0;
            while (index1 < Len1 && index2 < pos2)
            {
                if ((long)A[index1] * B[index2] > mid) index1++;
                else
                {
                    count += Len1 - index1;
                    index2++;
                }
            }
            return count;
        }
    }
}
