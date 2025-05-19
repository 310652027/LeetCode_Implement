using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _1931_PaintingaGridWithThreeDifferentColors
    {
        const int MOD = 1_000_000_007;

        bool connect(int a, int b, int m)
        {
            for (int r = 0; r < m; r++)
            {
                if ((a % 3) == (b % 3)) return false;
                a /= 3;
                b /= 3;
            }
            return true;
        }

        List<int> VaildArrange(int m)
        {
            var list = new List<int>();
            void BackTrack(int depth, int lastColor, int mask)
            {
                if (depth == m) { list.Add(mask); return; }
                for (int c = 0; c < 3; ++c)
                    if (c != lastColor)
                        BackTrack(depth + 1, c, mask * 3 + c);
            }
            BackTrack(0, -1, 0);      // 先呼叫一次
            return list;
        }

        // Using BitMask Dp to Find the count of the color statue by Last Column
        public int ColorTheGrid(int m, int n)
        {
            // First, Find the Corresponding color arrangment in one column by BackTrack
            List<int> Vaild_arrange = VaildArrange(m);
            // Next, Build the relations between two color columns if they can be adjecent, this is why we used bitmask.
            List<int>[] connectColumn = new List<int>[Vaild_arrange.Count];
            for (int i = 0; i < Vaild_arrange.Count; i++) connectColumn[i] = new List<int>();
            for (int i = 0; i < Vaild_arrange.Count; i++)
                for (int j = i + 1; j < Vaild_arrange.Count; j++)
                    if (connect(Vaild_arrange[i], Vaild_arrange[j], m))
                    {
                        connectColumn[i].Add(j);
                        connectColumn[j].Add(i);
                    }
            // Finally, when this column will be paint to color c_i, we need to find there's any color 
            // which is connnected with it , the statue count wil be summary of them (int dp[nei])  
            int[] dp = new int[connectColumn.Length];
            int[] next = new int[connectColumn.Length];
            Array.Fill(dp, 1);
            for (int col = 1; col < n; ++col)   
            {
                Array.Fill(next, 0);
                for (int i = 0; i < Vaild_arrange.Count; ++i)
                    foreach (int j in connectColumn[i])
                        next[i] = (next[i] + dp[j]) % MOD;
                (dp, next) = (next, dp);        
            }
            int total = 0;
            foreach (int v in dp) total = (total + v) % MOD;
            return total;
        }
    }
}
