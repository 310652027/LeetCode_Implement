using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _3405_CounttheNumberofArrayswithKMatchingAdjacentElements
    {
        const int MOD = 1_000_000_007;
        const int Upper = 100000;
        static long[] fac = new long[Upper];
        static long[] inv = new long[Upper];
        public int CountGoodArrays(int n, int m, int k)
        {
            //Main Idea: First we cam set any color on first node => m
            // For each node for[1, n -1] we can spilt them into 2 part
            // Keep : keep the color as last node, which can make the a matching adjacent pair
            // Change : modify the color, making it different from last 
            // Notice that Keep won't has chance to change, and Change can pick a color from m -1 type of colors
            // Thus, the answer = m [first node] * C^(n-1)_k [choose k positions to keep] * (m-1)^(n-k-1) [node which belong to Change]  

            // For Combinatorics but need to Mod, prepare fac & inv by Fermat
            long ModPow(long x, long e)
            {
                long res = 1;
                x %= MOD;
                while (e > 0)
                {
                    if ((e & 1) == 1) res = (res * x) % MOD;
                    x = (x * x) % MOD;
                    e >>= 1;
                }
                return res;
            }

            if (fac[0] == 0)
            {
                fac[0] = 1;
                for (int i = 1; i < Upper; i++) fac[i] = fac[i - 1] * i % MOD;

                inv[Upper - 1] = ModPow(fac[Upper - 1], MOD - 2);
                for (int i = Upper - 2; i >= 0; i--) inv[i] = inv[i + 1] * (i + 1) % MOD;
            }

            long PositionsPick = fac[n - 1] * inv[k] % MOD * inv[n - 1 - k] % MOD;
            long Mminus1_times = ModPow(m - 1, n - k - 1);
            return (int)(m * PositionsPick % MOD * Mminus1_times % MOD);
        }
    }
}
