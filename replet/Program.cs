using System;
using System.Collections.Generic;

public class LineComparer: IComparer<(string line, (char c, int num) stat )>
{
    public int Compare((string line, (char c, int num) stat ) x, (string line, (char c, int num) stat ) y) {
        if (x.stat.num == y.stat.num)
            return - x.line.CompareTo(y.line);
        else
            return x.stat.num.CompareTo(y.stat.num);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var global_st = new List<(string line, (char c, int num) stat )>();
        while (true) {
            string line = Console.ReadLine();
            if (line == null)
                break;
            var local_st = new Dictionary<char, int>();
            (char c, int num) max = ('\0', 0);
            foreach ( char c in line.ToLower()) {
                if (Char.IsLetter(c)) {
                    if(local_st.ContainsKey(c))
                        local_st[c]++;
                    else
                        local_st[c] = 1;
                    
                    if (local_st[c] > max.num)
                        max = (c, local_st[c]);
                    else if (local_st[c] == max.num && c < max.c)
                        max = (c, local_st[c]);
                }
            }
            global_st.Add((line, max));
        }
        LineComparer lc = new LineComparer();
        global_st.Sort(lc);
        if (global_st.Count > 10)
            global_st.RemoveRange(0, global_st.Count - 10);
        global_st.Reverse();
        foreach ( (string line, (char c, int num) stat) rec in global_st)
            Console.WriteLine(rec.stat.num.ToString() + ' ' + '\'' + Char.ToUpper(rec.stat.c) + "\': " + rec.line);
    }
}