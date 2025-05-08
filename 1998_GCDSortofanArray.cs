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

            int[] Parent = new int[numMax + 1];
            for (int i = 2; i <= numMax; i++) Parent[i] = i;
            int find(int x)          // ★ 路徑壓縮
            {
                return Parent[x] == x ? x : Parent[x] = find(Parent[x]);
            }
            void Union(int num)
            {
                int x = num;
                List<int> primes = new List<int>();

                if (x % 2 == 0)                  // 取因子 2
                {
                    primes.Add(2);
                    while (x % 2 == 0) x >>= 1;
                }

                for (int p = 3; p * p <= x; p += 2) // 只到 sqrt(x)
                {
                    if (x % p == 0)
                    {
                        primes.Add(p);
                        while (x % p == 0) x /= p;
                    }
                }
                if (x > 1) primes.Add(x);        // 剩餘質因子
                primes.Add(num);
                /*  依序 union 所有質因子  */
                int root = find(primes[0]);
                for (int i = 1; i < primes.Count; i++)
                    Parent[find(primes[i])] = root;
            }
            foreach (int num in nums) Union(num);
            int[] order = new int[nums.Length];
            for (int i = 0; i < order.Length; i++) order[i] = i;
            int[] sorted = nums.ToArray();
            Array.Sort(sorted);

            for (int i = 0; i < nums.Length; i++)
                if (find(nums[i]) != find(sorted[i])) return false;
            return true;
        }
    }
}
