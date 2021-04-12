using System;

namespace num_wheel
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = Int32.Parse(Console.ReadLine());
            int denum = (max+1)*(max+1)*(max+1);
            int num = 0;

            for(int i = 0; i <= max; i++){
                for(int j = 0; j <= max; j++){
                    for(int k = 0; k <= max; k++) {
                        if((i + j + k)% 10 == 0){
                            num += 1;
                        }
                    }
                }
            }

            int copy_denum = denum;
            int copy_num = num;

            while(copy_num != 0 && copy_denum != 0){
                if(copy_num > copy_denum) copy_num %= copy_denum;
                else copy_denum %=copy_num;
            }

            Console.WriteLine($"{num/(copy_num | copy_denum)}/{denum /(copy_num | copy_denum)}");
            
        }
    }
}
