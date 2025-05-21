using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _2561_RearrangingFruits
    {
        public long MinCost(int[] basket1, int[] basket2)
        {
            int globalMin = int.MaxValue;

            //Find the frequency of each element in both basket, but add 1 when element in first basket , in the other will minus 1
            Dictionary<int, int> feq = new Dictionary<int, int>();
            foreach (int i in basket1)
            {
                globalMin = int.Min(globalMin, i);
                if (!feq.ContainsKey(i)) feq[i] = 1;
                else feq[i]++;
            }
            foreach (int i in basket2)
            {
                globalMin = int.Min(globalMin, i);
                if (!feq.ContainsKey(i)) feq[i] = -1;
                else feq[i]--;
            }
            //If the element is odd , we can't reach the answer => return -1 
            foreach (var pair in feq) if (pair.Value % 2 == 1) return -1;

            bool FindBiggerThanAnother(int target, bool findBigger) => (findBigger && (feq[target] > 0)) || (!findBigger && (feq[target] < 0));

            //Make the cost of change wrothy
            Array.Sort(basket1);
            Array.Sort(basket2, (a, b) => b.CompareTo(a));
            int Index1 = 0, Index2 = 0;
            long Ans = 0;

            //Find the ShortCut when Min(a,b) is bigger than  min(a,g) + min(b,g) = 2 * g ,where g is smallest element in basket;
            //That happened when change (a,g) & (b,g) to avoid the cost of changing (a,b)
            int ShortCut = globalMin + globalMin;
            while (Index1 < basket1.Length && Index2 < basket2.Length)
            {
                // Change the element in basket 1 if its value in feq is positive
                // we choosing element in basket 2 by feq value is negative
                while (Index1 < basket1.Length && !FindBiggerThanAnother(basket1[Index1], true)) Index1++;
                while (Index2 < basket2.Length && !FindBiggerThanAnother(basket2[Index2], false)) Index2++;
                if (Index1 == basket1.Length || Index2 == basket2.Length) break;
                Ans += long.Min(int.Min(basket1[Index1], basket2[Index2]), ShortCut);
                feq[basket1[Index1]]--;
                feq[basket2[Index2]]++;
            }
            return Ans >> 1;
        }
    }
}
