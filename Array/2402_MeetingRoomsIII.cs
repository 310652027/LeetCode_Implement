using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _2402_MeetingRoomsIII
    {
        public int MostBooked(int n, int[][] meetings)
        {
            //Main Idea: We reocrd the endTime of each conference room in an long Array.
            // Compare to heap, we find and operate on array for O(n) to find the corresponding conference room, whose time in array is less than start
            // Also, if not, we operate the closetRoom like we already reach the time that meeting in that room is end.


            Span<long> ConferenceEndTime = stackalloc long[n];
            Span<int> UsedTimes = stackalloc int [n];
            Array.Sort(meetings, (a, b) => a[0] - b[0]);

            foreach (var meeting in meetings)
            {
                int start = meeting[0], end = meeting[1], len = end - start;
                int cloestLaterRoom = 0;
                long cloestTime = long.MaxValue;
                bool FindBlank = false;
                for(int i = 0; i < n; i++)
                {
                    if (ConferenceEndTime[i] <= start)
                    {
                        FindBlank = true;
                        UsedTimes[i]++;
                        ConferenceEndTime[i] = end;
                        break;
                    }
                    if (cloestTime > ConferenceEndTime[i])
                    {
                        cloestTime = ConferenceEndTime[i];
                        cloestLaterRoom = i;
                    }
                }
                if (!FindBlank)
                {
                    ConferenceEndTime[cloestLaterRoom] += len;
                    UsedTimes[cloestLaterRoom]++;
                }
            }

            int maxCount = 0, maxRoom = 0;
            for(int i = 0; i < n; i++)
            {
                if (UsedTimes[i] > maxCount)
                {
                    maxCount = UsedTimes[i];
                    maxRoom = i;
                }
            }
            return maxRoom;
        }
    }
}
