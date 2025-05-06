using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _2071_MaximumNumberofTasksYouCanAssign
    {
        public int MaxTaskAssign(int[] tasks, int[] workers, int pills, int strength)
        {
            // Implement by BinarySearch => O(n log n) 
            int workLen = workers.Length;
            bool Checker(int tasknum)
            {
                int pillCount = pills;
                // Fill the correspongind worker in the LinkedList 
                // Every time facing the task, sending the strongest worker or weakest worker but can used pill
                // If there is out of pill or corresponding worker return false
                LinkedList<int> UsedWorker = new LinkedList<int>();
                int worker_index = workLen - 1;
                for(int i = tasknum -1 ; i >= 0; i--)
                {
                    while(worker_index >= workLen- tasknum && workers[worker_index] + strength >= tasks[i])
                    {
                        UsedWorker.AddFirst(workers[worker_index]);
                        worker_index--;
                    }
                    if (UsedWorker.Count == 0) return false;

                    if (UsedWorker.Last.Value >= tasks[i]) UsedWorker.RemoveLast();
                    else
                    {
                        if(pillCount == 0) return false;
                        pillCount--;
                        UsedWorker.RemoveFirst();
                    }
                }
                return true;
            }
            Array.Sort(tasks);
            Array.Sort(workers);
            int left = 0, right = int.Min(workers.Length, tasks.Length);
            int last = 0;
            while (left <= right)
            {
                int mid = left + (right - left >> 1);
                if (Checker(mid))
                {
                    last = mid;
                    left = mid + 1;
                }
                else right = mid - 1;
            }
            return last;
        }
    }
}
