using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _3343_CountNumberofBalancedPermutations
    {
        long[] fact, invFact;
        const int MOD = 1_000_000_007;
        long Pow(long a, long e)
        {
            long r = 1;
            while (e > 0)
            {
                if ((e & 1) == 1) r = r * a % MOD;
                a = a * a % MOD;
                e >>= 1;
            }
            return r;
        }
        long C(int n, int k) => k < 0 || k > n ? 0
                 : fact[n] * invFact[k] % MOD * invFact[n - k] % MOD;

        public int CountBalancedPermutations(string num)
        {
            // Build the table of Combinatorics 
            fact = new long[num.Length + 1];
            invFact = new long[num.Length + 1];
            fact[0] = 1;
            for (int i = 1; i <= num.Length; i++) fact[i] = fact[i - 1] * i % MOD;
            invFact[num.Length] = Pow(fact[num.Length], MOD - 2);
            for (int i = num.Length; i >= 1; i--) invFact[i - 1] = invFact[i] * i % MOD;


            //Countting the values of frequency, even Index Count and sum of digits. 
            int[] NumsRecord = new int[10];
            int sum = 0;
            foreach (char c in num)
            {
                NumsRecord[c - '0']++;
                sum += (c - '0');
            }
            if (sum % 2 == 1) return 0;
            int target = sum >> 1;
            int evencount = (num.Length + 1 >> 1);
            int oddcount = (num.Length >> 1);

            //Preparing the dynamic table 
            long[,] dp_DigitSum = new long[target + 1, evencount + 1];
            dp_DigitSum[0, 0] = 1;
            for (int d = 0; d <= 9; ++d)
            {
                int c = NumsRecord[d];
                if (c == 0) continue;
                long[,] next = (long[,])dp_DigitSum.Clone();
                for (int take = 1; take <= c; ++take)
                {
                    int addValue = take * d;
                    long comb = C(c, take);
                    //Find the combinatorics of each value which will can be added by addValue 
                    //the result will record in next to avoid the consquence will interfere the
                    //calculation of next take 
                    for (int tempSum = target; tempSum >= addValue; --tempSum)
                    {
                        for (int k = evencount; k >= take; --k)
                        {
                            long add = dp_DigitSum[tempSum - addValue, k - take] * comb % MOD;
                            next[tempSum, k] += add;
                            if (next[tempSum, k] >= MOD) next[tempSum, k] -= MOD;
                        }
                    }
                }
                dp_DigitSum = next;                              
            }

            long ways = dp_DigitSum[target, evencount];
            long baseInv = 1;                        
            for (int d = 0; d <= 9; d++)
                baseInv = baseInv * invFact[NumsRecord[d]] % MOD;

            long ans = ways * fact[evencount] % MOD * fact[oddcount] % MOD;
            ans = ans * baseInv % MOD;              
            return (int)ans;
        }
    }
}
