using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _1998_GCDSortofanArray
    {
        public bool GcdSort(int[] nums)
        {
            int numMax = nums[0];
            foreach (int num in nums) numMax = int.Max(num, numMax);

            // Build the Parent Tabel , Constructing the Method "find" and "Union"
            // Union Will find the primes which can divide the num, then the group of primes will be Union 
            int[] Parent = new int[numMax + 1];
            for (int i = 2; i <= numMax; i++) Parent[i] = i;
            int find(int x) => Parent[x] == x ? x : Parent[x] = find(Parent[x]);
            void Union(int num)
            {
                int x = num;
                List<int> primes = new List<int>();

                if (x % 2 == 0)
                {
                    primes.Add(2);
                    while (x % 2 == 0) x >>= 1;
                }

                for (int p = 3; p * p <= x; p += 2)
                {
                    if (x % p == 0)
                    {
                        primes.Add(p);
                        while (x % p == 0) x /= p;
                    }
                }
                primes.Add(num);
                if (x > 1) primes.Add(x);

                int root = find(primes[0]);
                for (int i = 1; i < primes.Count; i++)
                    Parent[find(primes[i])] = root;
            }
            foreach (int num in nums) Union(num);
            int[] order = new int[nums.Length];
            for (int i = 0; i < order.Length; i++) order[i] = i;
            int[] sorted = nums.ToArray();
            Array.Sort(sorted);


            //After Unions the primes to same group, check each element can change with the element ont index which standing on the orignal position.
            for (int i = 0; i < nums.Length; i++)
                if (find(nums[i]) != find(sorted[i]))
                    return false;
            return true;
        }
    }
}
