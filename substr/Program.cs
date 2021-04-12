using System;

namespace progII
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            double l = input.Length;
            int n_subsets = Convert.ToInt32(Math.Pow(2.0d, l));
            

            for (int i = n_subsets - 1; i > 0; i--) {
                string cur_gen = "";
                int copy = i;

                while(copy > 0) {
                    int d = copy % 2;
                    copy = copy / 2;
                    cur_gen = d.ToString() + cur_gen;
                }

                if(cur_gen.Length < input.Length){
                    string tmp = new String('0', input.Length - cur_gen.Length);
                    cur_gen = tmp + cur_gen;
                }
                for(int j = 0; j < cur_gen.Length; j++){
                    if(cur_gen[j] == '1'){
                        Console.Write(input[j]);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
