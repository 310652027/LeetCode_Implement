using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _3337_TotalCharactersinStringAfterTransformationsII
    {
        const int MOD = 1_000_000_007;
        static long[,] MatMul(long[,] A, long[,] B)
        {
            long[,] C = new long[26, 26];

            for (int i = 0; i < 26; i++)
                for (int k = 0; k < 26; k++)
                    if (A[i, k] != 0)
                        for (int j = 0; j < 26; j++)
                            C[i, j] = (C[i, j] + A[i, k] * B[k, j]) % MOD;

            return C;
        }

        static long[,] MatrixPower(long[,] baseMat, long exp)
        {
            long[,] res = new long[26, 26];
            for (int i = 0; i < 26; i++) res[i, i] = 1;

            long[,] pow = baseMat;
            while (exp > 0)
            {
                if ((exp & 1) == 1) res = MatMul(res, pow);
                pow = MatMul(pow, pow);
                exp >>= 1;
            }
            return res;
        }

        public int LengthAfterTransformations(string s, long t, IList<int> nums)
        {
            long[,] ConvertTable = new long[26, 26];
            long[] count = new long[26];

            //Prepare the Table from nums
            for (int i = 0; i < 26 ; i++)
            {
                int copy = (i + 1) % 26;
                for (int m = 0; m < nums[i]; m++)
                {
                    ConvertTable[copy, i] = 1;
                    copy = (copy + 1) % 26;
                }
            }

            //Record the chars' times
            foreach (char ch in s) count[ch - 'a']++;

            //For final result, Answer will be summary of  T * T *...*T(t times) * count  where T = ConvertTable
            //Counting M =  T * T *...*T(t times) by fast exponentiation, reducing the multiply time to log2t
            long[,] M = MatrixPower(ConvertTable, t);

            //Estimate summary of M * count 
            long total = 0;
            for (int r = 0; r < 26; r++)
            {
                long sum = 0;
                for (int c = 0; c < 26; c++)
                    sum = (sum + M[r, c] * count[c]) % MOD;
                total = (total + sum) % MOD;
            }
            return (int)total;
        }
    }
}
