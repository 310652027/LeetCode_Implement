using Emgu.CV.CvEnum;
using Emgu.CV.Flann;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _3445_MaximumDifferenceBetweenEvenandOddFrequencyII
    {
        public int MaxDifference(string s, int k)
        {
            int len = s.Length;
            int Ans = int.MinValue;
            // Using the record to find how many statue has minimum distance
            int[] MinStatue = new int[4];
            //Using function to record a A and B's type is Odd or even;
            int GetStatus(int cntA, int cntB) => (cntA & 1) << 1 | (cntB & 1);

            // Pick two elements to count the element show up times
            // Main idea is spilt the array to three parts, the elements which index smaller than left as region "Pervious"
            // the region previous will already know how the minimum pair in MinStatue
            // All elements whose index smaller & equal than right will record into count 
            // every time we have the count can find the rival make count of A is odd and count of B is even
            // ex: when countA is odd countB is even , we can find the previous region  has the statue that prevA is even and prevB is even
            // that can make sure countA - prevA is odd , countB - PrevB is even 
            foreach (char a in new char[] { '0', '1', '2', '3', '4' })
            {
                foreach (char b in new char[] { '0', '1', '2', '3', '4' })
                {
                    if (a == b) continue;
                    Array.Fill(MinStatue, int.MaxValue);
                    int left = -1;
                    int Counta = 0, Countb = 0;
                    int Preva = 0, Prevb = 0;
                    for (int right = 0; right < len; right++)
                    {
                        //Record the count we meet
                        char pick = s[right];
                        if (a == pick) Counta++;
                        else if (b == pick) Countb++;

                        // fill the MinDistance of each statue into array
                        while (right - left >= k && Countb - Prevb >= 2)
                        {
                            int PrevStatue = GetStatus(Preva, Prevb);
                            MinStatue[PrevStatue] = int.Min(MinStatue[PrevStatue], Preva - Prevb);
                            char delete = s[++left];
                            if (a == delete) Preva++;
                            else if (b == delete) Prevb++;
                        }

                        //Find Corresponding rival statue to count Ans
                        int RightStatue = GetStatus(Counta, Countb);
                        int Rival = MinStatue[RightStatue ^ 0b10];
                        if (Rival != int.MaxValue) Ans = int.Max(Ans, Counta - Countb - Rival);
                    }
                }
            }
            return Ans;
        }

    }
}
