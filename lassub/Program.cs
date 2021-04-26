using System;
using static System.Console;

namespace lassub
{
    class Program
    {

        static void lassub(int[] arr) {
            int[] lengths = new int[arr.Length];
            int[] prevs = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++) {
                lengths[i] = 1;
                prevs[i] = -1;
            }

            int max = 0;

            for (int i = 0; i < arr.Length; i++) {
                for (int j = 0; j < i; j++) {
                    if (arr[i] > arr[j] && lengths[i] < lengths[j] + 1) {
                        lengths[i] = lengths[j] + 1;
                        prevs[i] = j;
                    }
                }
                if (lengths[i] > lengths[max])
                    max = i;
            }

            int cur_i = max;
            string output = "";
            while (cur_i != -1) {
                output = arr[cur_i].ToString() + " " + output;
                cur_i = prevs[cur_i];
            }
            WriteLine(output);
        }


        static void Main(string[] args)
        {
            int[] arr = Array.ConvertAll(ReadLine().Split(), v => int.Parse(v));
            lassub(arr);
        }
    }
}
