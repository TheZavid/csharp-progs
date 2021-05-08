using System;
using System.Linq;
using static System.Console;
using System.Collections.Generic;

namespace alphabet
{
    class Program
    {
        static int width, height;
        static int[,] working_grid;
        static Dictionary<char, HashSet<(int r, int c)>> map = new Dictionary<char, HashSet<(int r, int c)>>(); 
        static (int dr, int dc)[] dirs = new (int dr, int dc)[] {(-1, 0), (0, 1), (1, 0), (0, -1)};

        static HashSet<(int, int)> find_next((int r, int c) pos, int dist) {
            var output = new HashSet<(int, int)>();
            foreach ( (int dr, int dc) dir in dirs) {
                int new_r = pos.r + dir.dr;
                int new_c = pos.c + dir.dc;
                if ( ((new_r >= height) || (new_r < 0)) || ((new_c >= width) || (new_c < 0)) )
                    continue;
                    
                if ((working_grid[new_r, new_c] < dist ) && (working_grid[new_r, new_c] > 0))
                    continue;

                output.Add((new_r, new_c));
            }
            return output;
        }
        static void BFS(int sr, int sc, int val) {
            var cur = new HashSet<(int r, int c)>();
            var to_visit = new HashSet<(int r, int c)>();
            cur.Add((sr, sc));
            int dist = val + 1;

            while (cur.Count > 0) {
                foreach ((int r, int c) item in cur)
                    working_grid[item.r, item.c] = dist;

                foreach((int r, int c) pos in cur)
                    to_visit.UnionWith(find_next(pos, dist + 1)); // find where else we can go.

                // made instead of =
                cur.Clear();
                cur.UnionWith(to_visit);
                // 
                to_visit.Clear();
                dist++;
            }
        }
        static void Main(string[] args)
        {
            width = int.Parse(ReadLine().Trim());
            height = int.Parse(ReadLine().Trim());
            char[,] grid = new char[height, width];
            working_grid = new int[height, width];
            char[] to_fill = ReadLine().Trim().ToCharArray();
            int tmp = 0;
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    grid[i, j] = to_fill[tmp];
                    tmp++;
                    if (map.ContainsKey(grid[i,j]))
                        map[grid[i,j]].Add((i, j));
                    else {
                        map.Add(grid[i, j], new HashSet<(int r, int c)>() );
                        map[grid[i ,j]].Add((i, j));
                    }
                }
            }

            string tmp1 = " ";
            foreach ( char c in ReadLine().Trim()) {
                if (to_fill.Contains(c)) 
                    tmp1 += c.ToString();
            }
            char[] seq = tmp1.ToCharArray();

            var pos = new HashSet<(int r, int c, int min)>[seq.Length + 1];
            pos[0] = new HashSet<(int r, int c, int min)>();
            pos[0].Add((0,0,0));

            for (int i = 1; i < seq.Length; i++) {
                pos[i] = new HashSet<(int r, int c, int min)>();
                foreach ((int r, int c, int min) start in pos[i-1])
                    BFS(start.r, start.c, start.min);

                foreach ((int r, int c) end in map[seq[i]])
                        pos[i].Add((end.r, end.c, working_grid[end.r, end.c]));

                working_grid = new int[height, width];
            }

            int min = 0;
            foreach ( (int, int, int min) item in pos[seq.Length - 1]) {
                if (min == 0) {
                    min = item.min;
                    continue;
                }

                if ( item.min < min)
                    min = item.min;
            }
            WriteLine(min.ToString());
        }
    }
}
