// remake of my C program at https://github.com/TheZavid/c-progs/blob/main/conway.c

using System;

class Program
{

    static int[] world; // I keep world a global variable to avoid passing it to print out function
    static int[][] patterns =
    {
        new int[] {1, 1, 1},
        new int[] {1, 1, 0},
        new int[] {1, 0, 1},
        new int[] {1, 0, 0},
        new int[] {0, 1, 1},
        new int[] {0, 1, 0},
        new int[] {0, 0, 1},
        new int[] {0, 0, 0}
    };
    static int[] wolframn = new int[8];

    static int get(int index) {
        if (index == -1)
            return (world[world.Length - 1]);
        else if (index == world.Length)
            return world[0];
        else
            return world[index];
    }



    static int get_val(int[] cur_pattern) {
        for ( int i = 0; i < patterns.Length; ++i) {
            bool found = true;
            for (int j = 0; j < 3; ++j){
                if (cur_pattern[j] != patterns[i][j]) {
                    found = false;
                    break;
                }
            }
            if (found)
                return wolframn[i];
        }
        System.Environment.Exit(1);
        return 999;
    }

    static void print_world() {
        for (int i = 0; i < world.Length; ++i){
            if (world[i] == 1)
                Console.Write('x');
            else
                Console.Write('-');
        }
        Console.WriteLine();
    }


    static void Main(string[] args)
    {
        // get wolfram number from input and convert to binary
        int input_wolf = int.Parse(Console.ReadLine().Trim());
        for (int c = 0; c < 8; ++c) {
            int bnum = input_wolf << c;
            if ( (bnum & 128 ) == 128 ) // 128 is 1000_0000 in binary
                wolframn[c] = 1;
            else 
                wolframn[c] = 0;
        }

        int iterations = int.Parse(Console.ReadLine().Trim());
        
        // get initial state string and convert to an array of 1 and 0
        string initial_state = Console.ReadLine().Trim();
        world = new int[initial_state.Length];
        for (int i = 0; i < initial_state.Length; ++i) {
            if ( initial_state[i] == '-')
                world[i] = 0;
            else
                world[i] = 1;
        }
        // for (int i = 0; i < initial_state.Length; ++i)
        //     Console.WriteLine(world[i]);
        bool do_print = true;
        for (int i = 0; i < iterations; ++i) {
            int[] next = new int[world.Length];
            for ( int j = 0; j < world.Length; ++j) {
                int[] cur_pattern = new int[] {
                    get(j-1),
                    get(j),
                    get(j+1)
                };
                next[j] = get_val(cur_pattern);
            }
            Array.Copy(next, world, next.Length);
            if (iterations > 40) {
                if (i == 20) {
                    Console.WriteLine("...");
                    do_print = false;
                    continue;
                }
                if (i == iterations - 20)
                    do_print = true;
            }
            if (do_print)
                print_world();
        }
    }
}
