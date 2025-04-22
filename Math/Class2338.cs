using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class Class2338
    {
        const int mod = (int)1e9 + 7;
        public int IdealArrays(int n, int maxValue)
        {
            int N = n + maxValue + 5;

            long[] factorial = new long[N];
            long[] inverse = new long[N];
            long[] inv = new long[N];

            factorial[0] = inverse[0] = inv[1] = 1;
            // Calculate n! in factorial[n]
            for (int i = 1; i < N; i++) factorial[i] = (factorial[i - 1] * i) % mod;
            //Perduce modular inverse of i by Fermat's little theorem
            for (int i = 2; i < N; i++) inv[i] = mod - mod / i * inv[mod % i] % mod;
            // modular inverse of factorial[n] => (i!)^(-1) % mod 
            for (int i = 1; i < N; i++) inverse[i] = (inverse[i - 1] * inv[i]) % mod;

            long Comb(int a, int b)
            {
                if (b > a) return 0;
                // C(n, k) = factorial[n] * inverse[k] * inverse[n-k] % mod
                return factorial[a] * inverse[b] % mod * inverse[a - b] % mod;
            }

            long total = 0;

            for (int lastValue = 1; lastValue <= maxValue; lastValue++)
            {
                // lastValue = p1^e1 * p2^e2 * p3^e3 ... * pk^ ek
                // for 1 <= i <= k ,fill enough p_i (number of ei) in array where is length n
                // that will be C( n - 1 + ei, ei) for each 1 <= i<= k;
                int copy = lastValue;
                Dictionary<int, int> PrimeCount = new();

                for (int p = 2; p * p <= copy; p++)
                {
                    while (copy % p == 0)
                    {
                        if (!PrimeCount.ContainsKey(p)) PrimeCount[p] = 0;
                        PrimeCount[p]++;
                        copy /= p;
                    }
                }

                if (copy > 1)
                {
                    if (!PrimeCount.ContainsKey(copy)) PrimeCount[copy] = 0;
                    PrimeCount[copy]++;
                }

                long way = 1;
                foreach (var pair in PrimeCount)
                {
                    int e = pair.Value;
                    way = way * Comb(n - 1 + e, e) % mod;
                }

                total = (total + way) % mod;
            }

            return (int)total;
        }

        public int IdealArrays_DPCal(int n, int maxValue)
        {
            int[] MultipleChainLen = new int[Math.Min(n + 1, 15)]; 
            int maxLen = MultipleChainLen.Length - 1;

            int[,] dp = new int[n + 1, maxLen + 1];
            for(int i = 0; i <= n; i++)
            {
                dp[i, 0] = 1;
                for (int j = 1; j <= int.Min(i, maxLen); j++) dp[i, j] = (dp[i - 1, j - 1] + dp[i - 1, j]) % mod;
            }
            //BackTrack to Construct 
            //Record "How many multiple chain in lenth ? " in MultipleChainLen[?]
            void BackTrack(int LastNum, int len)
            {
                if (len > n) return;
                for(int time = 2;time * LastNum <= maxValue;time++) BackTrack(time * LastNum, len + 1);
                MultipleChainLen[len]++;
            }
            for(int start = 1;start <= maxValue; start ++) BackTrack(start, 1);

            int TotalNum = 0;
            for (int takenum = 1; takenum <= maxLen; takenum++)
                for (int time = 0; time < MultipleChainLen[takenum]; time++)
                    TotalNum = (TotalNum + dp[n - 1, takenum - 1]) % mod;

            return TotalNum;
        }
    }
}
