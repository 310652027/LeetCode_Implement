using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _3463_CheckIfDigitsAreEqualinStringAfterOperationsII
    {
        //Main Idea : If simulate the action from orignal string s directly to two digits 
        //The result of two digits can be seem as the multiplication by Pascal 
        //However, the sequence will be too large when s.Length is too high.
        //Thus, we reduce the process by C(n,k) mod 2 and mod5 then Combine them by Chinese reminder thm.
        public bool HasSameDigits(string s)
        {
            int n = s.Length - 2;                
            if (n < 0) return true;               

            int[] d = new int[s.Length];
            for (int i = 0; i < d.Length; i++) d[i] = s[i] - '0';
            int diff = 0;                        

            for (int k = 0; k <= n; k++)          
            {
                int c2 = BinomMod2(n, k);
                int c5 = BinomMod5(n, k);
                int c10 = CombineMod10(c2, c5);   

                int term = d[k] - d[k + 1];
                term %= 10; if (term < 0) term += 10;

                diff = (diff + term * c10) % 10;
            }
            return diff == 0;
        }
        //Method
        #region Method

        //C(n,k) %2
        private static int BinomMod2(int n, int k) => ((k & n) == k) ? 1 : 0;

        private static readonly int[,] C5 =
        {
            {1,0,0,0,0},
            {1,1,0,0,0},
            {1,2,1,0,0},
            {1,3,3,1,0},
            {1,4,1,4,1}
        };
        //C(n,k) % 5 by Lucas
        private static int BinomMod5(int n, int k)
        {
            int res = 1;
            while (n > 0 || k > 0)
            {
                int ni = n % 5, ki = k % 5;
                if (ki > ni) return 0;
                res = (res * C5[ni, ki]) % 5;
                n /= 5; k /= 5;
            }
            return res;
        }

        private static int CombineMod10(int a2, int b5)
        {
            int t = (b5 - a2) % 5;
            if (t < 0) t += 5;
            t = (t * 3) % 5;              
            return (a2 + 2 * t) % 10;
        }
        #endregion Method
    }
}
