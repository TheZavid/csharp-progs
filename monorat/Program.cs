using static System.Console;
using System;

class Program
{
    enum Monotonicity
    {
        increasing,
        decreasing,
        nonincreasing,
        nondecreasing,
        constant,
        nonmonotonic
    }

    static Monotonicity cur_type;

    static void determineMon(double last_n, double cur_n) {
        switch (cur_type)
        {
            case Monotonicity.increasing when (cur_n < last_n):
                cur_type = Monotonicity.nonmonotonic;
                break;
            case Monotonicity.decreasing when (cur_n > last_n):
                cur_type = Monotonicity.nonmonotonic;
                break;
            case Monotonicity.increasing when (cur_n == last_n):
                cur_type = Monotonicity.nondecreasing;
                break;
            case Monotonicity.decreasing when (cur_n == last_n):
                cur_type = Monotonicity.nonincreasing;
                break;
            case Monotonicity.nonincreasing when (cur_n > last_n):
                cur_type = Monotonicity.nonmonotonic;
                break;
            case Monotonicity.nondecreasing when (cur_n < last_n):
                cur_type = Monotonicity.nonmonotonic;
                break;
            case Monotonicity.constant when (cur_n != last_n):
                if (cur_n > last_n)
                    cur_type = Monotonicity.nondecreasing;
                else
                    cur_type = Monotonicity.nonincreasing;
                break;
            default:
                break;
        }
    }

    static void printOut(){
        switch(cur_type)
        {
            case Monotonicity.nondecreasing:
                WriteLine("non-decreasing");
                break;
            case Monotonicity.nonincreasing:
                WriteLine("non-increasing");
                break;
            case Monotonicity.nonmonotonic:
                WriteLine("non-monotonic");
                break;
            default:
                WriteLine(cur_type.ToString());
                break;
        }
    }

    static void isMonotonicUtil(double last_n) {
        string input = ReadLine();
        if (input == null || cur_type == Monotonicity.nonmonotonic){
            printOut();
            return;
        }
        string[] tmp = input.Trim().Split(null);
        double cur_n = double.Parse(tmp[0])/double.Parse(tmp[1]);
        determineMon(last_n, cur_n);
        isMonotonicUtil(cur_n);
    }

    static void isMonotonic()
    {
        string[] tmp1 = ReadLine().Trim().Split(null);
        string[] tmp2 = ReadLine().Trim().Split(null);
        double n1 = double.Parse(tmp1[0])/double.Parse(tmp1[1]);
        double n2 = double.Parse(tmp2[0])/double.Parse(tmp2[1]);
        if (n1 > n2)
            cur_type = Monotonicity.decreasing;
        else if (n1 < n2)
            cur_type = Monotonicity.increasing;
        else
            cur_type = Monotonicity.constant;
        isMonotonicUtil(n2);
    }

    static void Main(string[] args)
    {
        isMonotonic();
    }
}