using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _1751_MaximumNumberofEventsThatCanBeAttendedII
    {
        public int MaxValue(int[][] events, int k)
        {
            //Main Idea: Two alg. be used in the code
            //1. Dynamic Program help to find the situations that how many score we can pick when we choose j events , where j <= k.
            //2. Binary Search can fin the next event we can attend whe we already join i-th event.
            int EventLen = events.Length;
            Array.Sort(events, (a, b) => a[0].CompareTo(b[0]));

            //First, we find the next event. If there's no event we can join, filling EventLen + 2  whose valur in Dp will be zero. 
            int[] NextEvents = new int[EventLen + 3];
            Array.Fill(NextEvents, EventLen + 2);
            for (int i = 0; i < EventLen; i++)
            {
                int end = events[i][1];
                int left = i + 1, right = EventLen - 1;
                int n = right + 1;
                while (left <= right)
                {
                    int mid = left + (right - left >> 1);
                    int eventS = events[mid][0];
                    if (eventS <= end) left = mid + 1;
                    else
                    {
                        n = mid;
                        right = mid - 1;
                    }
                }
                NextEvents[i] = n;
            }

            //Next, we check every score made by join j <= k events.
            //The score in Dp[eventI, joinNum] means the possible score when we reach events[i] and already join joinNum - 1 events at least.
            //We can attend it ( get the score "events[eventI][2]" and add the front score "  Dp[NextEvents[eventI], joinNum - 1]" 
            //          or skip it ( take the last score "Dp[eventI + 1, joinNum]")
            int Max = 0;
            int[,] Dp = new int[EventLen + 2, k + 1];
            for (int joinNum = 1; joinNum <= k; joinNum++)
            {
                for (int eventI = EventLen - 1; eventI >= 0; eventI--)
                {
                    Dp[eventI, joinNum] =
                        int.Max(Dp[eventI + 1, joinNum], events[eventI][2] + Dp[NextEvents[eventI], joinNum - 1]);
                    Max = int.Max(Max, Dp[eventI, joinNum]);
                }
            }
            return Max;
        }
    }
}
