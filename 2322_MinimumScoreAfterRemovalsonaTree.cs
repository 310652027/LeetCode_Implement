using Emgu.CV.Features2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _2322_MinimumScoreAfterRemovalsonaTree
    {
        public int MinimumScore(int[] nums, int[][] edges)
        {
            //Main Idea: if A ^ B = C , then C ^ B = A is always true.
            //To simply the process , we set 0 as the root of the tree, after that , except 0, every node in the tree must have a parent.
            //We build the parent's information and the the sum of XOR in a subtree by DFS.
            //For XOR, we find the XOR result of each child the XOR to parent itself.
            //To know the relation of two node is Ancestor or not, we buile the timestamp.
            //We make sure the Ancestor node  will have the smaller value in "inTime" than child's, but will bigger on "outTime".
            //Therefore, if the node1 & node2 is the root of two subtree when two edges are cut,
            //  we can find the XOR solution which define by the relation of node1 & node2.
            //Also, the XOR solution of remain subtree can be estimate as we stated first.


            //1. Build the environment
            int nodeNum = nums.Length;
            int[] XorOfSubtree = new int[nodeNum];
            List<int>[] Neighbor = new List<int>[nodeNum];
            for(int i = 0; i < nodeNum; i++) Neighbor[i] = new List<int>();
            foreach (int[] edge in edges)
            {
                int node1 = edge[0] , node2 = edge[1];
                Neighbor[node1].Add(node2);
                Neighbor[node2].Add(node1);
            }
            int[] inTime = new int[nodeNum];
            int[] outTime = new int[nodeNum];
            int time = 0;

            //2.Used DFS to find the XOR solution and parent solution
            int DFS(int node, int parent)
            {
                inTime[node] = time++;
                int xor = nums[node];
                foreach (int nei in Neighbor[node]) if (nei != parent) xor ^= DFS(nei, node);
                XorOfSubtree[node] = xor;
                outTime[node] = time++;
                return xor;
            }
            DFS(0, -1);
            int TotalXOR = XorOfSubtree[0];
            int MinDiff = int.MaxValue;

            bool IsAncestor(int node1, int node2)
                =>  inTime[node1] <= inTime[node2] && outTime[node2] <= outTime[node1];


            //3.Enumerate two nodes as the root of chosen subtrees
            for (int node1 = 1; node1 < nodeNum -1; node1++)
            {
                int Xor = XorOfSubtree[node1];
                for(int node2 = node1+ 1;  node2 < nodeNum; node2++)
                {
                    int Xor1 = Xor;
                    int Xor2 = XorOfSubtree[node2];
                    int Xor3 = 0;

                    if (IsAncestor(node1, node2))
                    {
                        Xor3 = TotalXOR ^ Xor1;
                        Xor1 ^= Xor2;
                    }
                    else if (IsAncestor(node2, node1))
                    {
                        Xor3 = TotalXOR ^ Xor2;
                        Xor2 ^= Xor1;
                    }
                    else Xor3 = TotalXOR ^ Xor1 ^ Xor2;

                    int max = Math.Max(Xor3, Math.Max(Xor2, Xor1));
                    int min = Math.Min(Xor3, Math.Min(Xor2, Xor1));
                    MinDiff = Math.Min(MinDiff, max - min);
                }
            }

            return MinDiff;
        }
    }
}
