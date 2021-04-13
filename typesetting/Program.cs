using System;

class Program
{


    static bool is_latin(char c) {
        if (('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z'))
            return true;
        return false;
    }


    static void Main(string[] args)
    {
        string output = "";
        while(true) {
            string line = Console.ReadLine();
            if (line == null)
                break;
            line = line.Trim();
            string words = "";
            for (int c = 0;  c < line.Length; c++) {
                if (is_latin(line[c]))
                    words += line[c];
                else    
                    words += ' ';
            }
            string[] awords = words.Trim().Split(null);
            for ( int w = 0; w < awords.Length; w++) {
                if ((output.Length + awords[w].Length + 1) > 30) {
                    Console.WriteLine(output);
                    output = awords[w];
                } else
                    output += ' ' + awords[w];
                output = output.Trim();
                if (w < awords.Length - 1 && awords[w+1].Length >= 30) {
                    Console.WriteLine(output);
                    Console.WriteLine(awords[w+1]);
                    w++;
                    output = "";
                }
            }
        }
        if (output.Length > 0)
            Console.WriteLine(output);
    }
}
