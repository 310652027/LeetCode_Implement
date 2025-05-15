using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTR
{
    internal class _3519_CountNumberswithNon_DecreasingDigits
    {
        const int MOD = 1_000_000_007;

        int[,,,] dp;      // [idx , last , tight , lead]
        int len, B;
        string choose = "";
        public int CountNumbers(string l, string r, int b)
        {
            B = b;
            int right = Solve(DecToBase(r));            // f(r)
            int left = Solve(DecToBase(MinusOne(l)));  // f(l-1)

            return (right - left + MOD) % MOD;
        }

        string MinusOne(string dec)
        {
            if (dec == "0") return "0";
            char[] ch = dec.ToCharArray();
            int i = ch.Length - 1;
            while (ch[i] == '0') { ch[i] = '9'; --i; }
            ch[i]--;
            return ch[0] == '0' ? new string(ch, 1, ch.Length - 1) : new string(ch);
        }

        string DecToBase(string dec)
        {
            if (dec == "0") return "0";
            var rem = new List<int>();
            while (!(dec.Length == 1 && dec[0] == '0'))
            {
                var q = new System.Text.StringBuilder();
                int carry = 0;
                foreach (char ch in dec)
                {
                    int cur = carry * 10 + (ch - '0');
                    int div = cur / B;
                    carry = cur % B;
                    if (q.Length > 0 || div != 0) q.Append((char)('0' + div));
                }
                rem.Add(carry);
                dec = q.Length == 0 ? "0" : q.ToString();
            }
            var sb = new System.Text.StringBuilder();
            for (int i = rem.Count - 1; i >= 0; --i) sb.Append((char)('0' + rem[i]));
            return sb.ToString();
        }


        int Solve(string bound)
        {
            len = bound.Length;
            choose = bound;
            //Initial dp
            dp = new int[len + 1, B, 2, 2];
            for (int a = 0; a <= len; a++)
                for (int b = 0; b < B; b++)
                    for (int c = 0; c < 2; c++)
                        for (int d = 0; d < 2; d++)
                            dp[a, b, c, d] = -1;

            return Dfs(0, 0, 1, 1);
        }

        //Main Idea on Solving many element is non-decreasing in base B
        //Using DigitDP but fill the result in dp and find it when we need to calcualte the same conditions next times
        int Dfs(int idx, int last, int tight, int lead)
        {
            int memo = dp[idx, last, tight, lead];
            if (memo != -1) return memo;
            if (idx == len) return 1;

            long res = 0;
            int upper = (tight == 1) ? choose[idx] - '0': B - 1;
            // make num in index will be equal or higher than last 
            for (int d = last; d <= upper; d++)
            {
                int nLead = (lead == 1 && d == 0) ? 1 : 0;
                int nLast = (nLead == 1) ? 0 : d;              
                int nTight = (tight == 1 && d == upper) ? 1 : 0;

                res += Dfs(idx + 1, nLast, nTight, nLead);
                if (res >= MOD) res -= MOD;
            }
            return dp[idx, last, tight, lead] = (int)res;
        }
    }
}
