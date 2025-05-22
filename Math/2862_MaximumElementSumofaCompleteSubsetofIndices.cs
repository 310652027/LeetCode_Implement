namespace HTR
{
    internal class _2862_MaximumElementSumofaCompleteSubsetofIndices
    {
        public long MaximumSum(IList<int> nums)
        {
            // If a * b is a perfect square, a and b has same radicand
            // like 2 * 8 == 16 , 2* 18 == 36 & 8 * 18 == 96 all will be perfect square becuase 2 is radicand of 2 ,8 and 18
            // Thus, we can group the index by there radicand and find the maximum sum.
            bool[] Add = new bool[nums.Count];
            long ans = 0;
            for (int i = 0; i < nums.Count; i++)
            {
                // We track the unfind index, which mean wasn't group, be the leader of new group and find the element has same radicand at behind
                if (Add[i] == false)
                {
                    int index = i + 1;
                    long sum = 0;
                    int findIndex = 0;
                    for (int coefficient = 1; (findIndex = coefficient * coefficient * index) <= nums.Count; coefficient++)
                    {
                        int zero_index = findIndex - 1;
                        Add[zero_index] = true;
                        sum += nums[zero_index];
                    }
                    if (sum > ans) ans = sum;
                }
            }
            return ans;
        }
    }
}
