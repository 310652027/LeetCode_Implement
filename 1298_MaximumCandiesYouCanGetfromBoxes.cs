using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    class _1298_MaximumCandiesYouCanGetfromBoxes
    {
        public int MaxCandies(int[] status, int[] candies, int[][] keys, int[][] containedBoxes, int[] initialBoxes)
        {
            // Main Idea: We can receive the candy int one box if and only if we receive that box and we keep the key or it's opened;  
            // We change the value in status like
            // 1 => if the box is opened.
            // 0 => the box is closed.
            // -1 => the box is close but we got the key.
            // -2 => the box is close but we already found it.
            // Notice that if (value == -1 & we get find the box) OR (value == -2 & we revceive the key) can make the box open. 
            int ReceiveCount = 0;
            Queue<int> Opening = new Queue<int>();
            foreach (int IBox in initialBoxes)
            {
                if (status[IBox] == 1) Opening.Enqueue(IBox);
                else status[IBox] = -2;
            }
            while (Opening.Count > 0)
            {
                int openIndex = Opening.Dequeue();
                ReceiveCount += candies[openIndex];
                int[] FoundKey = keys[openIndex];
                int[] ReceivedBox = containedBoxes[openIndex];
                foreach (int key in FoundKey)
                {
                    if (status[key] == 0) status[key]--;
                    else if (status[key] == -2)
                    {
                        status[key] = 0;
                        Opening.Enqueue(key);
                    }
                }

                foreach (int box in ReceivedBox)
                {
                    if (status[box] == 0) status[box] = -2;
                    else if (status[box] == -1 || status[box] == 1)
                    {
                        status[box] = 0;
                        Opening.Enqueue(box);
                    }
                }
            }
            return ReceiveCount;
        }

    }
}
