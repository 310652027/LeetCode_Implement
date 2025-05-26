using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _1857_LargestColorValueinaDirectedGraph
    {
        public int LargestPathValue(string colors, int[][] edges)
        {
            int nodeCount = colors.Length;

            //Build the infos for Topological Sort
            //Find the node not the end of any edge be the start of alg 
            int[] InDegree = new int[nodeCount];
            List<int>[] Next = new List<int>[nodeCount];
            for (int i = 0; i < nodeCount; i++) Next[i] = new List<int>();
            foreach (int[] edge in edges)
            {
                int st = edge[0], ed = edge[1];
                InDegree[ed]++;
                Next[st].Add(ed);
            }
            var Start = new Queue<int>();
            for (int i = 0; i < nodeCount; i++) if (InDegree[i] == 0) Start.Enqueue(i);

            //Find the whole biggest color value of each node 
            int[,] dp = new int[nodeCount, 26];
            for (int i = 0; i < nodeCount; i++) dp[i, colors[i] - 'a'] = 1;
            int Longest = 1;
            int Pass = 0;
            while (Start.Count > 0)
            {
                int node = Start.Dequeue();
                Pass++;
                foreach (int next in Next[node])
                {
                    for (int color = 0; color < 26; color++)
                    {
                        // Change value by this node to next point 
                        dp[next, color] = int.Max(dp[next, color], dp[node, color] + (colors[next] - 'a' == color ? 1 : 0));
                        Longest = int.Max(Longest, dp[next, color]);
                    }
                    // If next point is no other adjecent point is uncheck, adding it to the queue. 
                    if (--InDegree[next] == 0) Start.Enqueue(next);
                }
            }
            return Pass == nodeCount ? Longest : -1;
        }
    }
}
