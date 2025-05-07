using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _1916_CountWaystoBuildRoomsinanAntColony
    {
        //Idea : To Estimate a Node, we need to know how many subtree it has
        //We get the number of subnode, children ,and ways in array
        //Using Dynamic Program to find the ways from children by permutation
        const int MOD = 1_000_000_007;
        long[] fact, invFact;
        List<int>[] Child;
        long[] Ways;
        int[] SubnodeCount;

        public int WaysToBuildRooms(int[] prevRoom)
        {
            int n = prevRoom.Length;
            //Since we need to find the number of permutation in MOD
            //Using Fermat Method to build the table, which help us to calculate the permutation count in O(1)
            fact = new long[n + 1];
            invFact = new long[n + 1];
            fact[0] = 1;
            for (int i = 1; i <= n; i++) fact[i] = fact[i - 1] * i % MOD;
            invFact[n] = Pow(fact[n], MOD - 2);
            for (int i = n; i >= 1; i--) invFact[i - 1] = invFact[i] * i % MOD;


            Child = new List<int>[n];
            for (int i = 0; i < n; i++) Child[i] = new List<int>();
            for (int v = 1; v < n; v++) Child[prevRoom[v]].Add(v);

            Ways = new long[n];
            SubnodeCount = new int[n];
            
            Dfs(0);
            return (int)Ways[0];
        }

        void Dfs(int u)
        {
            //DFS Structure make sure the children's way and subnode count will estimated first
            //Which will help us to calculate this node's way
            long way = 1;
            int total = 0;

            foreach (int v in Child[u])
            {
                Dfs(v);                         
                // Adding ways is similar to put n same colored balls into m + 1 basket
                // Where n = v's child count , m = the present nodes count 
                // Because there are N permutations in node v, we need to mulitply it after done the baskets' permutation
                way = way * Ways[v] % MOD;             
                way = way * C(total + SubnodeCount[v], SubnodeCount[v]) % MOD;
                total += SubnodeCount[v];
            }
            Ways[u] = way;
            SubnodeCount[u] = total + 1;                   
        }

        long C(int n, int k) => fact[n] * invFact[k] % MOD * invFact[n - k] % MOD;

        long Pow(long a, long e)
        {
            long r = 1;
            while (e > 0)
            {
                if ((e & 1) == 1) r = r * a % MOD;
                a = a * a % MOD;
                e >>= 1;
            }
            return r;
        }
    }
}
