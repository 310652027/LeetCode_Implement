using Emgu.CV.Flann;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _440_KthSmallestinLexicographicalOrder
    {
        public int FindKthNumber(int n, int k)
        {
            // the Subfunction will find how many element contain in range [prefix, next) 
            long Counting_Range(long prefix, long next)
            {
                long count = 0;
                while(prefix <= n)
                {
                    count += Math.Min(n + 1L, next) - prefix;
                    prefix *= 10;
                    next *= 10;
                }
                return count;
            }

            long prefix = 1;
            k--;
            while(k > 0)
            {
                //Every time we loop, we find the count from [i , i+1) ,
                // that's , how many element using same prefix .
                long cnt = Counting_Range(prefix, prefix + 1);
                if(k >= cnt)
                {
                    // the count of element is too small, this region doesn' have the result we want.
                    k -= (int)cnt;
                    prefix++;
                }
                else
                {
                    // the element contain the result we desire, need to go deeper to fine it
                    prefix *= 10;
                    k--;
                }
            }
            return (int)prefix;
        }
    }
}
