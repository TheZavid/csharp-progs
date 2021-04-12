using System;

class Program
{
    static void CalcFact (int number)
    {
        DynArray array = new DynArray();
        array.append(1);
        for (int i = 2; i <= number; i++) {
            int carry = 0;
            int to_iter = array.getLength();
            // multiply every digit by i and write only 1 digit
            for (int j = 0; j < to_iter; j++) {
                int cur_res = array.get(j)*i + carry;
                carry = cur_res / 10;
                array.set(j, cur_res % 10);
            }
            // when carry is greater then 10 its neceserary to "spread" all of its digits
            int ci = 0;
            while (carry != 0){
                array.set(to_iter + ci, carry % 10);
                carry = carry / 10;
                ci++;
            }
        }

        string output = "";

        // print the number
        for (int i = 0; i < array.getLength(); i++) {
            output = array.get(i).ToString() + output;
        }
        Console.WriteLine(output);
    }

    public static void Main(string[] args)
    {
        CalcFact(Int32.Parse(Console.ReadLine()));
    }
}