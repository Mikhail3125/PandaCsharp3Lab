using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandaCsharp3Lab
{

    class Matrix 
    {
        int n;
        double[,] data;


        public Matrix(double[,] Mat, int n)
        {
            this.n = n;
            data = new double[n, n];
            data = Mat;

        }
        public Matrix(int n)
        {
            this.n = n;
            data = new double[n, n];

        }

        public void Create()
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    data[i, j] = rnd.Next(1, 10) + Math.Round(rnd.NextDouble(), 2);
                }
            }
        }
        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(data[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static Matrix operator +(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    C.data[i, j] = A.data[i, j] + B.data[i, j];
                }
            }
            return C;
        }

        public static Matrix operator -(Matrix A, Matrix B)
        {
            Matrix D = new Matrix(A.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    D.data[i, j] = A.data[i, j] - B.data[i, j];
                }
            }
            return D;
        }

        public static Matrix operator *(Matrix A, Matrix B)
        {
            Matrix E = new Matrix(A.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    E.data[i, j] = 0;
                    for (int k = 0; k < A.n; k++)
                    {
                        E.data[i, j] = E.data[i, j] + A.data[k, j] * B.data[i, k];
                    }
                }
            }
            return E;
        }
        public static Matrix operator *(int digit, Matrix A )
        {
            Matrix E = new Matrix(A.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    E.data[i, j] = 0;
                    for (int k = 0; k < A.n; k++)
                    {
                        E.data[i, j] = E.data[i, j] + A.data[k, j] * digit;
                    }
                }
            }
            return E;
        }
       public  double DetMat(Matrix mat)
        {
            string s;
            string[] str;
            double det = 1;
            //определяем переменную EPS
            const double EPS = 1E-9;
            //размерность матрицы
            int n;
            //вводим n
            n = mat.n;
            //определяем массив размером nxn
            double[][] a = new double[n][];
            double[][] b = new double[1][];
            b[0] = new double[n];
            //заполняем его
            for (int i = 0; i < n; i++)
            {
               
                a[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    a[i][j] = Convert.ToDouble(mat.data[i,j]);
                }
            }
            //проходим по строкам
            for (int i = 0; i < n; ++i)
            {
                //присваиваем k номер строки
                int k = i;
                //идем по строке от i+1 до конца
                for (int j = i + 1; j < n; ++j)
                    //проверяем
                    if (Math.Abs(a[j][i]) > Math.Abs(a[k][i]))
                        //если равенство выполняется то k присваиваем j
                        k = j;
                //если равенство выполняется то определитель приравниваем 0 и выходим из программы
                if (Math.Abs(a[k][i]) < EPS)
                {
                    det = 0;
                    break;
                }
                //меняем местами a[i] и a[k]
                b[0] = a[i];
                a[i] = a[k];
                a[k] = b[0];
                //если i не равно k
                if (i != k)
                    //то меняем знак определителя
                    det = -det;
                //умножаем det на элемент a[i][i]
                det *= a[i][i];
                //идем по строке от i+1 до конца
                for (int j = i + 1; j < n; ++j)
                    //каждый элемент делим на a[i][i]
                    a[i][j] /= a[i][i];
                //идем по столбцам
                for (int j = 0; j < n; ++j)
                    //проверяем
                    if ((j != i) && (Math.Abs(a[j][i]) > EPS))
                        //если да, то идем по k от i+1
                        for (k = i + 1; k < n; ++k)
                            a[j][k] -= a[i][k] * a[j][i];
            }
            return det;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            // Результат С = 2(A-B)*(A^2+B); Найти |C|
            Console.Write("Введите N: ");
            double[,] MatrixA = new double[3, 3] { { 5, 1, 7 },
                                             {10, -2, 1},
                                             {0, 1, 2  } };
            double[,] MatrixB = new double[3, 3] { { 2, 4, 1 },
                                             { 2, 1, 0 },
                                             { 7, 2, 1 } };
            int n = int.Parse(Console.ReadLine());
            Matrix A = new Matrix(MatrixA, n);
        //    A.Create();
            A.Print();
            Console.WriteLine();
            Matrix B = new Matrix(MatrixB, n);
          //  B.Create();
            B.Print();

            Console.WriteLine();
            Matrix C = (2*(A - B)) * (A*A + B);
            C.Print();

            double det = C.DetMat(C);
            Console.WriteLine("\nОпределитель матрицы C-> " + det);
            Console.WriteLine();
        //    C.Print();
            ///  и так далее :)

            Console.ReadLine();

        }
    }
}
