using System;
using System.Linq;

namespace percol
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tmp = (Console.ReadLine().Split(null));
            int n = Int32.Parse(tmp[0]);

            int[,] matrix = new int[n, n];
            int target = 0;

            for (int r = 0 ; r < n ; ++r) {
                target += r + 1;
                string[] row = Console.ReadLine().Split(null);
                for (int c = 0 ; c < n; ++c)
                    matrix[r, c] = int.Parse(row[c]);
            }
            
            int total = 0;

            for (int c = 0 ; c < n ; ++c) {
                int cur_sum = 0;
                int[] met = new int[n];
                for (int r = 0 ; r < n; ++r){
                    if (met.Contains(matrix[r, c])) break;

                    if (matrix[r, c] >= 1 && matrix[r, c] <= n){
                        cur_sum += matrix[r, c];
                        met[r] = matrix[r, c];
                    }
                }
                if (cur_sum == target) total += 1;
            }

            Console.WriteLine(total);
        }
    }
}
