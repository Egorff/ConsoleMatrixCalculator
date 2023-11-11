using System;
using LinAlgebraCore.DependencyInjection.Interfaces;

namespace LinAlgebraCore.Models
{
	public class Matrix<T>
	{
        #region Main

        public void Main(string msg)
        {
            Console.WriteLine(msg);

            do
            {
                bool flag = true;

                int n = 0;

                string str = string.Empty;

                int rowA = 0, rowB = 0, colA = 0, colB = 0;

                while (flag)
                {
                    Console.WriteLine("Choose operation:\n + -- 0\n - -- 1\n * -- 2\n " +
                    "Determeinant -- 3.");
                    str = Console.ReadLine();

                    if (!int.TryParse(str, out n))
                    {
                        Console.WriteLine("Wrong Value!");

                        flag = true;
                    }
                    else
                    {
                        n = Convert.ToInt32(str);

                        flag = false;
                    }
                }

                flag = true;

                while (flag)
                {
                    rowA = Input<int>("Enter rows of matrix A: \n", IsMatrixDimCorrect, ConvertStringToInt);
                    rowB = Input<int>("Enter rows of matrix B: \n", IsMatrixDimCorrect, ConvertStringToInt);

                    colA = Input<int>("Enter columns of matrix A: \n", IsMatrixDimCorrect, ConvertStringToInt);
                    colB = Input<int>("Enter columns of matrix B: \n", IsMatrixDimCorrect, ConvertStringToInt);

                    if (rowA != rowB || colA != colB)
                    {
                        Console.WriteLine("Matrixes' sizes aren't equal!");
                        flag = true;
                    }
                    else
                        flag = false;
                }

                var m1 = GetMatrix("Matrix1", rowA, colA);
                var m2 = GetMatrix("Matrix2", rowB, colB);
                //var m3 = new Matrix<int>((uint)rowA, (uint)colA);

                if (n == 0)
                {
                    //var m3 = (double)(m1 + m2);
                }

                Console.WriteLine("Press Q to finish program, or press any other key to continue.");

            } while (!(Console.ReadKey().Key == ConsoleKey.Q));
        }

        //Main("Console matrix calculator");
        
        #endregion

        #region Fields

        T[,] m_matrix;

		uint m_rows;

		uint m_columns;

        #endregion

        #region Prop

        public uint Rows { get => m_rows; }

        public uint Columns { get => m_columns; }

        #endregion

        #region ctor

        public Matrix(uint rows, uint columns)
        {
            m_matrix = new T[rows, columns];

            m_rows = rows;

            m_columns = columns;
        }

        //конструктор копирования
        public Matrix(Matrix<T> other, bool deep = false, IDeepCloner<T> dc = null)
        {
            if (other == null)
                throw new ArgumentNullException("Other obj equals to null reference.");

            m_matrix = new T[other.Rows, other.Columns];

            m_rows = other.Rows;

            m_columns = other.Rows;

            if (!deep )
            {
                for (int i = 0; i < m_rows; i++)
                {
                    for (int j = 0; j < m_columns; j++)
                    {
                        m_matrix[i, j] = other.m_matrix[i, j];
                    }
                }
            }
            else if(deep && dc != null)
            {
                for (int i = 0; i < m_rows; i++)
                {
                    for (int j = 0; j < m_columns; j++)
                    {
                        m_matrix[i, j] = dc.DeepClone(other[i, j]);
                    }
                }
            }
        }

        #endregion

        #region Methods

        public static void Determeinant(Matrix<T> m)
        {
            if (m.Rows != 2 || m.Columns != 2)
                throw new Exception("Determeinant ca be found only if matrix is square!");

            //var det = m[0, 0] * m[1,1]
        }

        static bool IsMatrixDimCorrect(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                Console.WriteLine("String is empty");

                return false;
            }

            int n = 0;

            if(! int.TryParse(str, out n))
            {
                Console.WriteLine("Wrong Input!");

                return false;
            }

            if (n < 0)
                return false;

            return true;
        }

        static int ConvertStringToInt(string str)
        {
            return Convert.ToInt32(str);
        }

        static double[,] GetMatrix(string name, int rows, int columns)
        {
            double[,] matrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{name}[{i}, {j}] = ");
                    matrix[i, j] = double.Parse(Console.ReadLine());
                }
            }

            return matrix;
        }

        static Tout Input<Tout>(string msg, Func<string, bool> checker, Func<string, Tout> converter)
        {
            if (checker == null || converter == null)
                throw new ArgumentNullException("Delegates checker and conoverter are not set!");

            string str = string.Empty;

            Tout temp = default;

            do
            {
                Console.WriteLine(msg);
                str = Console.ReadLine();



            } while (!checker.Invoke(str));

            temp = converter.Invoke(str);

            return temp;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region Indexer

        public T this[int i, int j]
        {
            get => m_matrix[i, j];

            set => m_matrix[i, j] = value;
        }

        #endregion

        #region Operators == != + - *

        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1 == null || m2 == null)
                throw new ArgumentNullException("m1 and m2 equals to null!");

            if (m1.Rows == m2.Rows && m1.Columns == m2.Columns)
                throw new Exception("Operation + is impossible!");

            Matrix<T> result = new Matrix<T>(m1.Rows, m1.Columns);

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    result[i, j] = (dynamic)m1[i, j] + m2[i, j];
                }
            }

            return result;
        }

        public static Matrix<T> operator -(Matrix<T> m1, Matrix<T> m2)
        {
            if (m1 == null || m2 == null)
                throw new ArgumentNullException("m1 and m2 equals to null!");

            if (m1.Rows == m2.Rows && m1.Columns == m2.Columns)
                throw new Exception("Operation + is impossible!");

            Matrix<T> result = new Matrix<T>(m1.Rows, m1.Columns);

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    result[i, j] = (dynamic)m1[i, j] - m2[i, j];
                }
            }

            return result;
        }

        public static Matrix<T> operator *(Matrix<T> m, double number)
        {
            if (m == null || number == null)
                throw new ArgumentNullException("m1 and m2 equals to null!");

            Matrix<T> result = new Matrix<T>(m.Rows, m.Columns);

            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    result[i, j] = (dynamic)m[i, j] * number;
                }
            }

            return result;
        }

        #endregion
    }
}

