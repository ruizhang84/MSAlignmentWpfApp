using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSAlignmentClassLibrary.Aligner
{
    public class DPAligner : IAligner
    {
        public int Gap { get; set; }

        public DPAligner(int penalty = 1)
        {
            Gap = penalty;
        }

        public Dictionary<int, int> Align(Dictionary<int, int> seq1, Dictionary<int, int> seq2)
        {
            Dictionary<int, int> mapping = new Dictionary<int, int>();
            int[] arr1 = seq1.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
            int[] arr2 = seq2.OrderBy(x => x.Key).Select(x => x.Value).ToArray();

            int m = arr1.Length;
            int n = arr2.Length;

            int[,] dp = new int[m + n + 1, m + n + 1];
            for (int i = 0; i < m + n + 1; i++)
                for (int j = 0; j < m + n + 1; j++)
                    dp[i, j] = 0;

            for (int i = 0; i < m + n + 1; i++)
            {
                dp[i, 0] = Gap * i;
                dp[0, i] = Gap * i;
            }

            // dynamic programming
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (arr1[i-1] == arr2[j-1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        dp[i, j] = Math.Min(
                            dp[i - 1, j - 1] + Math.Abs(arr1[i-1] - arr2[j-1]),
                            Math.Min(dp[i - 1, j] + Gap, dp[i, j - 1] + Gap)
                            );
                    }
                }
            }

            int[] scan1 = seq1.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
            int[] scan2 = seq2.OrderBy(x => x.Key).Select(x => x.Key).ToArray();

            //back tracking
            int l = m + n + 1;
            while ( m > 0 && n > 0)
            {
                if (dp[m, n] == dp[m-1, n-1])
                {
                    mapping[scan2[--n]] = scan1[--m];
                } 
                else if (dp[m, n] == dp[m-1, n-1] + Math.Abs(arr1[m-1] - arr2[n-1]))
                {
                    mapping[scan2[--n]] = scan1[--m];
                }
                else if (dp[m, n] == dp[m-1, n] + Gap)
                {
                    m--;
                }
                else
                {
                    n--;
                }
            }

            return mapping;
        }
    }
}
