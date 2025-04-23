using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class Class1269
    {

        public int NumWays(int steps, int arrLen)
        {
            int mod = (int)1e9 + 7;
            int MaxPos = Math.Min(steps >> 1, arrLen - 1);

            // Record the times of step:x move to pos:y in dp[x,y]
            int[,] dp = new int[steps + 1, MaxPos + 1];
            dp[0, 0] = 1;

            for (int step = 1; step <= steps; step++)
            {
                for (int pos = 0; pos <= MaxPos; pos++)
                {
                    dp[step, pos] = dp[step - 1, pos];
                    if (pos > 0) dp[step, pos] = (dp[step, pos] + dp[step - 1, pos - 1]) % mod;
                    if (pos < MaxPos) dp[step, pos] = (dp[step, pos] + dp[step - 1, pos + 1]) % mod;
                }
            }

            return dp[steps, 0];
        }

    }
}
