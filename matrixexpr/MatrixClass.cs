using static System.Console;
using System;
class Matrix {
    int size;
    int[,] a;
    
    public Matrix(int n) {
        size = n;
        a = new int[n, n];
    }
    
    public void read() {
        for (int i = 0 ; i < size ; ++i) {
            string[] words = ReadLine().Trim().Split(null);
            if (words.Length != size) {
                WriteLine("bad input");
                return;
            }
            for (int j = 0 ; j < size ; ++j)
                a[i, j] = int.Parse(words[j]);
        }
    }


    public static Matrix operator +(Matrix x, Matrix y) {
            if (x.size != y.size) {
                    throw new InvalidOperationException("incompatible matrices");
            }
            Matrix z = new Matrix(x.size);
            for (int i = 0; i < x.size; i++) {
                    for (int j = 0; j < x.size; j++)
                        z.a[i,j] = x.a[i,j] + y.a[i,j];
            } 
            return z;
    }


    public static Matrix operator -(Matrix x, Matrix y) {
            if (x.size != y.size) {
                    throw new InvalidOperationException("incompatible matrices");
            }
            Matrix z = new Matrix(x.size);
            for (int i = 0; i < x.size; i++) {
                    for (int j = 0; j < x.size; j++)
                        z.a[i,j] = x.a[i,j] - y.a[i,j];
            } 
            return z;
    }


    public static Matrix operator *(Matrix x, Matrix y) {
            if (x.size != y.size) {
                    throw new InvalidOperationException("incompatible matrices");
            }
            Matrix z = new Matrix(x.size);
            for (int i = 0; i < x.size; i++) {
                    for (int j = 0; j < x.size; j++) {
                        for (int k = 0; k < x.size; k++)
                                z.a[i,j] += x.a[i,k]*y.a[k,j];
                    }
            }   
            return z;
    }

    public void PrintOut() {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                        Write(a[i,j].ToString() + " ");
                WriteLine();
            }
    }
}