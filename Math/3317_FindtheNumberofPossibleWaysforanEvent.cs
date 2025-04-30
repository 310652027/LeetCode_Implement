using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _3317_FindtheNumberofPossibleWaysforanEvent
    {

        public int NumberOfWays(int n, int x, int y)
        {
            const int MOD = 1_000_000_007;
            int m = Math.Max(n, x);

            //Since we need to cal the factorial in permutation and the we also need used modulo 
            //We prepare the fact and invFact to calculate the Permutation
            long[] fact = new long[m + 1];
            long[] invFact = new long[m + 1];
            fact[0] = 1;
            for (int i = 1; i <= m; i++) fact[i] = fact[i - 1] * i % MOD;

            invFact[m] = Pow(fact[m], MOD - 2, MOD);      //By Fermat
            for (int i = m; i >= 1; i--) invFact[i - 1] = invFact[i] * i % MOD;

            long Perm(int n1, int k1) => fact[n1] * invFact[n1 - k1] % MOD;

            // the problem can be seen as to permute n people into most x box
            // if there are k stages contains people ,the permutation will be Stirling number S(n,k) 
            // Where S(n,k) = S(n - 1,k - 1) +  k * S(n - 1, k);
            int K = Math.Min(n, x);
            long[,] S = new long[n + 1, K + 1];          // S[i][j]
            S[0, 0] = 1;
            for (int i = 1; i <= n; i++)
            {
                int up = Math.Min(i, K);
                for (int k = 1; k <= up; k++)
                {
                    S[i, k] = (S[i - 1, k - 1] + k * S[i - 1, k]) % MOD;
                }
            }

            //Finallt the answer will be equal to
            //For every number k , which is < x and mean numbers of stage contain people, how many permutations?
            //=> P(x,k) * S[n,k] *  y^k , whick means (k group sign in k stage) * (Method to permute k group) * score for every group
            long ans = 0;
            for (int k = 1; k <= K; k++)
            {
                long term = Perm(x, k);          // P(x,k)
                term = term * S[n, k] % MOD;     // * S(n,k)
                term = term * Pow(y, k, MOD) % MOD;   // * y^k
                ans = (ans + term) % MOD;
            }
            return (int)ans;
        }

        private static long Pow(long a, long e, int mod)
        {
            long res = 1;
            for (; e > 0; e >>= 1, a = a * a % mod)
                if ((e & 1) == 1) res = res * a % mod;
            return res;
        }
    }
}
