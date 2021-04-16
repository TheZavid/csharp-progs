using static System.Console;
using System.Collections.Generic;

class Graph {
    int num;
    List<int>[] edge;     // edge[v] is a list of vertices that v points to
    List<int>[] reverse;  // reverse[v] is a list of vertices that point to v

    Stack<int> util_stack = new Stack<int>();
    bool[] visited;

    public Graph() {
        num = int.Parse(ReadLine());
        edge = new List<int>[num + 1];
        reverse = new List<int>[num + 1];
        visited = new bool[num + 1];
        for (int i = 1 ; i <= num ; ++i) {
            edge[i] = new List<int>();
            reverse[i] = new List<int>();
            visited[i] = false;
        }

        while (true) {
            string s = ReadLine();
            if (s == null)
                break;
            string[] words = s.Split();
            int from = int.Parse(words[0]), to = int.Parse(words[1]);
            edge[from].Add(to);
            reverse[to].Add(from);
        }
    }

    public void DFS1( int source ) {
        visited[source] = true;
        foreach ( int vertex in edge[source])
            if (!visited[vertex]) DFS1(vertex);
        util_stack.Push(source);
    }
    public void DFS2( int source ) {
        visited[source] = true;
        Write(source.ToString() + ' ');
        foreach ( int vertex in reverse[source])
            if (!visited[vertex]) DFS2(vertex);
    }
    public void FindSCC() {
        // run DFS1 to populate stack sorted accroding to discovery time
        for (int i = 1; i <=num; i++) 
            if (!visited[i]) DFS1(i);
        // reset visited array to false
        for (int i = 1; i <= num; i++)
            visited[i] = false;
        // for every connected component DFS2 will run once and print it
        while (util_stack.Count != 0) {
            int vertex = util_stack.Pop();
            if (!visited[vertex]) {
                DFS2(vertex);
                Write('\n');
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Graph aGraph = new Graph();
        aGraph.FindSCC();
    }
}
