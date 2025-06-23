using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _2081_SumofkMirrorNumbers
    {
        public long KMirror(int k, int n)
        {
            // Main Idea: we produce the Palindrome number by a prefix number A 
            // From A, we used two method to provide Palindromes. The following examples set A = 123;
            // 1(Odd). Take the number of A without the last one and reverse it => "21"
            // 2(Even). Take all number and reverse it => "321"
            // We put the concequence to the back of original value, getting "12321" and " 123321"
            // Making sure that we add the value which still be Mirror at k-based then adding it as their order

            int take = 0;
            long sum = 0;
            int[] fill = new int[32];
            bool kMirrorChecker(long value)
            {
                Array.Clear(fill);
                int l = 0;
                while (value > 0)
                {
                    fill[l++] = (int)(value % k);
                    value /= k;
                }
                int left = 0, right = l - 1;
                while (left < right)
                {
                    if (fill[left] != fill[right]) return false;
                    left++;
                    right--;
                }
                return true;
            }
            long OddProduction(long value, int length)
            {
                long ret = 0;
                int copylen = length - 1;
                long copyvalue = value;
                copyvalue /= 10;
                while (copylen-- > 0)
                {
                    value *= 10;
                    ret = ret * 10 + copyvalue % 10;
                    copyvalue /= 10;
                }
                return value + ret;
            }
            long EvenProduction(long value, int length)
            {
                long ret = 0;
                int copylen = length;
                long copyvalue = value;
                while (copylen-- > 0)
                {
                    value *= 10;
                    ret = ret * 10 + copyvalue % 10;
                    copyvalue /= 10;
                }
                return value + ret;
            }
            int length = 1;
            long last = 1;
            long next = 10;
            while (take < n)
            {
                // Care about the order, adding all odds first , then add evens
                for (long i = last; i < next && take < n; i++)
                {
                    long Odd_value = OddProduction(i, length);
                    if (kMirrorChecker(Odd_value))
                    {
                        take++;
                        sum += Odd_value;
                    }
                }
                if (take == n) break;

                for (long i = last; i < next && take < n; i++)
                {
                    long Even_value = EvenProduction(i, length);
                    if (kMirrorChecker(Even_value))
                    {
                        take++;
                        sum += Even_value;
                    }
                }
                last = next;
                next *= 10;
                length++;
            }
            return sum;
        }

    }
}
