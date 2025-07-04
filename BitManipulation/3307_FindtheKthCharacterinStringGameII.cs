using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _3307_FindtheKthCharacterinStringGameII
    {
        public char KthCharacter(long k, int[] operations)
        {
            //Main Idea: Notice that, for index between 2^(i-1) and 2^i, the char at index p is product by char whose index is p - 2^(i-1)
            //Thus, we can produce the char by bit multiplation.
            //If k > 1, we need find a number i (2^i < k and i is maximum) which can help us to find then original number in k - 2^i;
            //Also, compared to Q# 3303, the operation will make the char move to next or stay, it is another variable in this question.

            int time = 0;
            int index = (int)Math.Ceiling(Math.Log2(k)) - 1;
            long len = 1L << index;
            while(k > 1)
            {
                // Find the i , we move the index from k to k - 2^i
                if(k > len)
                {
                    //If opertion is move(1), we add the shift time. Else, we don't shift(0);
                    time += operations[index];
                    k -= len;
                }
                len >>= 1;
                index--;
            }
            time %= 26;
            return (char)('a' + time);
        }
    }
}
