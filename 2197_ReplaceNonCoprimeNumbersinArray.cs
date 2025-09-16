using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _2197_ReplaceNonCoprimeNumbersinArray
    {
        public IList<int> ReplaceNonCoprimes(int[] nums)
        {
            //Main Idea : Build a stack to store all number we operate or not before.
            //Once we operate and produce a new number, we still need to check there' s a new non-coprime pair 
            // from new number and the top of stack.
            //We check the pair is coprime by gcd == 1, otherwise we get lcm of two number by value1 * value2 / gcd easily.
            Stack<int> st = new Stack<int>();
            st.Push(nums[0]);
            for (int i = 1; i < nums.Length; i++)
            {
                int num = nums[i];
                long Lnum = num;
                while (st.Count > 0)
                {
                    int peek = st.Peek();
                    long gcd = GCD(peek, Lnum);
                    if (gcd == 1) break;
                    Lnum = LCM(st.Pop(), Lnum, gcd);
                }
                st.Push((int)Lnum);
            }
            List<int> ints = new List<int>();
            while (st.Count > 0) ints.Add(st.Pop());
            ints.Reverse();
            return ints;
        }
        static long GCD(long a, long b)
        {
            while (b != 0) { long t = a % b; a = b; b = t; }
            return a;
        }

        static long LCM(long a, long b, long g) => a / g * b;
    }
}
