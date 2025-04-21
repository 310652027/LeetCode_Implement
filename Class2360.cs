using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class Class2360
    {

        public int LongestCycle(int[] edges)
        {
            // using idx to record the egdes changes in place
            int idx = -2, ans = -1;
            for (int index = 0; index < edges.Length; index++)
            {
                int current = index, start = idx;
                while (current > -1)
                {
                    int nextnode = edges[current];
                    edges[current] = idx--;
                    current = nextnode;
                    // current will be move to next node 
                    // if current be negative that would be visited before or has no path out (-1)
                }
                if (current <= start) ans = int.Max(ans, current - idx - 1);
                // the "If" make sure that the node and its parents who end at no circle
            }
            return ans;
        }
    }
}
