using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _1745_PalindromePartitioning_IV
    {
        public bool CheckPartitioning(string s)
        {
            int n = s.Length;
            bool[,] b_palindrome = new bool[n, n];

            // Perduce a 2D dynamic program array , check the subarray int region s[left: right] is palindrome
            // b_palindrome[left: right]  = s[left] == s[right] && b_palindrome[left +1 , right-1] ,which means the middle range is palindrome already
            for (int len = 1; len <= n; len++)
            {
                for (int l = 0; l + len - 1 < n; l++)
                {
                    int r = l + len - 1;
                    if (s[l] == s[r]) b_palindrome[l, r] = (len <= 2) || b_palindrome[l + 1, r - 1];
                }
            }

            // We need to find twoc line two separate the array to be => s[0:i] | s[i+1,j] | s[j+1,n-1]
            // the solution happend when b_palindrome[0,i] && b_palindrome[i+1,j] && b_palindrome[j-1,n-1]
            for (int i = 0; i < n - 2; i++)           
            {
                if (!b_palindrome[0, i]) continue;
                for (int j = i + 1; j < n - 1; j++)   // 第二段：s[i+1..j]
                {
                    if (b_palindrome[i + 1, j] && b_palindrome[j + 1, n - 1]) return true;
                }
            }
            return false;
        }
    }
}
