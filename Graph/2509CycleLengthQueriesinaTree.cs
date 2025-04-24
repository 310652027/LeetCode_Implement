using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class Class2509
    {
        public int[] CycleLengthQueries(int n, int[][] queries)
        {
            int[] Ans = new int[queries.Length];

            for (int index = 0; index < queries.Length; index++)
            {
                int[] query = queries[index];
                int select1 = query[0], select2 = query[1];
                //Find Lowest Common Ancestor
                //If two number is not equal, change one number to its upper level.
                //Every time number change, we pass through a edge , ex: 7 => 3 will pass one edge.
                int path = 0;
                while(select1 != select2)
                {
                    if (select1 > select2) select1 >>= 1;
                    else select2 >>= 1;
                    path++;
                }
                Ans[index] = path + 1;
            }
            return Ans;
        }
    }
}
