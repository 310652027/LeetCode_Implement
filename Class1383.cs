using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class Class1383
    {

        public int MaxPerformance(int n, int[] speed, int[] efficiency, int k)
        {
            const int mod = (int)1e9 + 7;
            var ChooseEngineers = new PriorityQueue<int, int>();
            int[][] engineers = new int[n][];
            for (int i = 0; i < n; i++) engineers[i] = new int[] { speed[i], efficiency[i] };
            Array.Sort(engineers, (a, b) => b[1].CompareTo(a[1])); //order by DESC
            long PreSum = engineers[0][0];
            long MaxPerfom = engineers[0][0] * engineers[0][1];
            ChooseEngineers.Enqueue(engineers[0][0], engineers[0][0]);
            for (int index = 1; index < n; index++)
            {
                int spe = engineers[index][0], eff = engineers[index][1];
                // Add Faster Speed into Queue And PreSum
                // Make Later engineer will calculate as Fastest Group 
                if (ChooseEngineers.Count == k && ChooseEngineers.Peek() < spe)
                {
                    PreSum -= ChooseEngineers.Dequeue();
                    PreSum += spe;
                    ChooseEngineers.Enqueue(spe, spe);
                }
                else if (ChooseEngineers.Count < k)
                {
                    PreSum += spe;
                    ChooseEngineers.Enqueue(spe, spe);
                }
                MaxPerfom = long.Max(MaxPerfom, PreSum * eff);
            }
            return (int)(MaxPerfom % mod);
        }
    }
}
