using System;

class Program
{
    static void Main(string[] args)
    {
        string first_in = Console.ReadLine();
        char[] first_w = new char[first_in.Length];
        string sec_in = Console.ReadLine();
        char[] sec_w = new char[sec_in.Length];
        int[] final = new int[first_in.Length];
        

        for (int i = 0; i <  first_in.Length; i++) {
            first_w[i] = first_in[i];
        }

        for (int i = 0; i <  sec_in.Length; i++) {
            sec_w[i] = sec_in[i];
        } 

        int j = 0;
        foreach (char c in first_w){
            int index = Array.IndexOf(sec_w, c);
            if (index != -1){
                final[j] = (c - 'a');
                sec_w[index] = '0';
                j++;
            }   
        }

        Array.Sort(final);

        for (int i = first_in.Length - j; i < first_in.Length; i++){
            int c = final[i];
            if (c >= 0 && c <= 26){
                char tmp = (char) (c + 'a');
                Console.Write(tmp);
            }   
        }

        Console.WriteLine();
    }
}