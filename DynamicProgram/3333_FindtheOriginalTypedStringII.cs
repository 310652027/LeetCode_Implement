using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _3333_FindtheOriginalTypedStringII
    {
        public int PossibleStringCount(string word, int k)
        {
            //Main Idea : Check all possible typed string first, then deleting the count of typed string whose length is smaller than k
            //The back idea need to implement by dynamic program, which help us to
            //  tracking the repeating section can have how many uncorresponding strings.

            int MOD = 1_000_000_007;

            // Find total possible string not limited size
            // delete k by 1 for checking how many repeat section already have 
            long total = 0;
            List<int> Repeat = new();
            int r = 1;
            k--;

            for(int i =1; i < word.Length; i++)
            {
                if (word[i] == word[i - 1]) r++;
                else
                {
                    if(r  > 1)
                    {
                        Repeat.Add(r);
                        total = total * r % MOD;
                    }
                    r = 1;
                    k--;
                }
            }
            if(r > 1)
            {
                Repeat.Add(r);
                total = total * r % MOD;
            }
            //Once the count of section is smaller than k, there's may some uncorresponding strings whose size < k.
            //Then we use Dynamic Program to find the size
            //Each section can take the result of last section, where is LessThanK[index, a] 
            //Also, we can add the result from previous => LessThanK[index, c] by adding a char 
            //However, we need to remove the size which is bigger than Repeat[i]  => LessThanK[self, a]
            if (k > 0)
            {
                long[,] LessThanK = new long[k, 2];
                LessThanK[0, 0] = LessThanK[0, 1] = 1;
                for(int i = 0; i < Repeat.Count; i++)
                {
                    int c = i % 2, a = c == 0 ? 1 : 0;
                    for(int index = 1, self = 1 - Repeat[i]; index < k; index++, self++)
                    {
                        LessThanK[index, c] = LessThanK[index - 1, c] + LessThanK[index, a] - (self > -1 ? LessThanK[self, a] : 0);
                        LessThanK[index, c] %= MOD;
                        if (LessThanK[index, c] == 0) break;
                    }
                }
                int last = (Repeat.Count - 1) & 1;
                for(int i = 0;i < k; i++) total -= LessThanK[i, last];
            }
            while (total < 0) total += MOD;

            return (int)(total % MOD);
        }

    }
}
