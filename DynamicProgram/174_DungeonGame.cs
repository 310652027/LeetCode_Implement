using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _174_DungeonGame
    {
        public int CalculateMinimumHP(int[][] dungeon)
        {
            // Main Idea : Using Dynamic Program to find the value we need from down or rigth cells
            // Find the value we need from destinaion 
            // By the move of Upward or Leftward, until we reach at the Begining
            int m = dungeon.Length, n = dungeon[0].Length;
            int[,] MinCost = new int[m + 1, n + 1];
            const int side = int.MaxValue >> 1;
            for (int i = 0; i < m; i++) MinCost[i, n] = side;
            for (int i = 0; i < n; i++) MinCost[m, i] = side;
            MinCost[m, n - 1] = MinCost[m - 1, n] = 1;

            for (int i_m = m - 1; i_m >= 0; i_m--)
            {
                for (int i_n = n - 1; i_n >= 0; i_n--)
                {
                    // Using the Minimum Cost from the cell we can touch and minus the current value , valueBefore will be Minimum health we need 
                    int valueBefore = int.Min(MinCost[i_m + 1, i_n], MinCost[i_m, i_n + 1]) - dungeon[i_m][i_n];
                    // if valueBefore > 1 , the value will influence the up and left cell which calculation at behide.
                    MinCost[i_m, i_n] = int.Max(1, valueBefore);
                }
            }
            return MinCost[0, 0];
        }
    }
}
