using System;
using static System.Console;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string[] tmp = ReadLine().Split();
        int size = int.Parse(tmp[0]);
        Matrix[] matrices = new Matrix[int.Parse(tmp[1])+1];
        for (int i = 1; i < matrices.Length; i++)
        {
            matrices[i] = new Matrix(size);
            matrices[i].read();
            ReadLine();
        }
        string expr = ReadLine().Trim();
        Stack<Matrix> expr_stack = new Stack<Matrix>();
        string op = "+-*";
        foreach (char c in expr)
        {
            if (op.Contains(c)) {
                Matrix right = expr_stack.Pop();
                Matrix left = expr_stack.Pop();
                switch (c)
                {
                    case '+':
                        expr_stack.Push(left + right);
                        break;
                    case '-':
                        expr_stack.Push(left - right);
                        break;
                    case '*':
                        expr_stack.Push(left * right);
                        break;
                    default:
                        WriteLine("bad input");
                        return;
                }
            } else if (c != ' ') {
                expr_stack.Push(matrices[int.Parse(c.ToString())]);
            }
        }
        expr_stack.Pop().PrintOut();
    }
}