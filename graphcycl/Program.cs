using static System.Console;

class Graph {
    int num;         // number of vertices
    int[][] edge;    // jagged array: edge[i] is a list of neighbors of i
    
    public Graph() {
        num = int.Parse(ReadLine());
        edge = new int[num + 1][];
        
        for (int i = 1 ; i <= num ; ++i) {
            string[] words = ReadLine().Trim().Split(null);
            int m = int.Parse(words[0]);
            if (words.Length != m + 1) {
                WriteLine("bad input");
                return;
            }
            edge[i] = new int[m];
            for (int j = 0 ; j < m ; ++j)
                edge[i][j] = int.Parse(words[j + 1]);
        }
    }

    private static void printCycle(int[] stack, int stack_i, int target) {
        string output = "";
        int num = 0;
        while (true) {
            num++;
            if (stack[stack_i] == target) {
                output += target.ToString();
                break;
            }

            output += stack[stack_i--].ToString() + " ";
        }
        WriteLine(num.ToString());
        WriteLine(output);
    }

    private static bool hasCycleUtil(Graph G, int[] visited, int[] stack, int stack_i, int cur_v) {
        visited[cur_v] = 1;
        stack[stack_i] = cur_v;
        for(int i = 0; i < G.edge[cur_v].Length; i++) {
            int u = G.edge[cur_v][i];
            if (visited[u] == 1){
                printCycle(stack, stack_i, u);
                return true;
            } else if (visited[u] == 0) {
                if (hasCycleUtil(G, visited, stack, stack_i + 1, u))
                    return true;
            }
        }
        visited[cur_v] = -1;
        return false;
    }

    public static bool hasCycle(Graph G) {
        int[] visited = new int[G.num + 1];
        int[] stack = new int[G.num ];

        for (int i = 1; i <= G.num; i++) {
            if (visited[i] == 0) {
                if (hasCycleUtil(G, visited, stack, 0,  i))
                    return true;
            }
        }
        WriteLine("neni");
        return false;
    }
}

    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph();
            Graph.hasCycle(g);
        }
    }