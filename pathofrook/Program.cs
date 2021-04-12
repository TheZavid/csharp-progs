using System;
using System.Collections.Generic; // for HashSet

class Program
{
    // to be change according to needs
    const int NROWS = 8, NCOL = 8, MAXSTEPS = 8;
    const char FSQ = '.', OSQ = 'x', SSQ = 'v', ESQ = 'c';

    static HashSet<(int,int)> rook_moves = new HashSet<(int, int)> {(1,0), (0,1), (-1,0), (0,-1)};
    //no more changing
    static (int r, int c) start_pos;
    static (int r, int c) end_pos;

    static int[,] field = new int[NROWS, NCOL];
    
    // init field with -2 at obstacles and -1 everywhere else,
    // also remember start and end position
    static void InitField(){
        for (int r = 0; r < NROWS; r++) {
            string row = Console.ReadLine().Trim();
            for (int c = 0; c < NCOL; c++) {
                switch (row[c])
                {
                    case '.':
                        field[r,c] = -1;
                        break;
                    case 'x':
                        field[r,c] = -2;
                        break;
                    case 'v':
                        field[r,c] = -1;
                        start_pos.r = r;
                        start_pos.c = c;
                        break;
                    case 'c':
                        field[r,c] = -1;
                        end_pos.r = r;
                        end_pos.c = c;
                        break;
                    default:
                        Console.WriteLine("Bad Input!");
                        Environment.Exit(1);
                        break;
                }
            }
        }
    }

    // trying all posible squares where we can move to reach a given vertex
    static HashSet<(int r, int c)> FindSQ((int r, int c) pos) {
        HashSet<(int,int)> output = new HashSet<(int, int)>();
        foreach ((int dr, int dc) move in rook_moves) {
            for (int i = 1; i < MAXSTEPS; i++) {
                int newr = pos.r + move.dr * i;
                int newc = pos.c + move.dc * i;
                // if we got out of bounds
                if ((newr >= NROWS || newr < 0) || (newc >= NCOL || newc < 0))
                    break;
                // if obstacle all already visited
                else if (field[newr,newc] != -1)
                    break;
                output.Add((newr, newc));
            }
        }
        return output;
    }

    static int FindShortestPath() {
        HashSet<(int r, int c)> cur_squares = new HashSet<(int r, int c)>();
        cur_squares.Add(start_pos);
        HashSet<(int r, int c)> to_visit = new HashSet<(int r, int c)>();
        int distance = 0;
        while (cur_squares.Count != 0) {
            // specify shortest path to visited squares
            foreach((int r, int c) pos in cur_squares)
                field[pos.r,pos.c] = distance;
            
            foreach((int r, int c) pos in cur_squares)
                to_visit.UnionWith(FindSQ(pos)); // find where else we can go.

            // made instead of =
            cur_squares.Clear();
            cur_squares.UnionWith(to_visit);
            // 
            to_visit.Clear();
            distance++;

        }
        return field[end_pos.r, end_pos.c]; // returns shortest path to end or -1
    }
    static void Main(string[] args)
    {
        InitField();
        Console.WriteLine(FindShortestPath().ToString());
    }
}
