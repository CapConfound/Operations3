using System;

namespace Operations_2
{
    public class Matrix
    {
        protected int rows, columns;
        protected double[,] data;
        
        public Matrix(int r, int c)
        {
            this.rows = r; this.columns = c;
            data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++) data[i, j] = 0;
        }
        
        public Matrix(double[,] mm)
        {
            this.rows = mm.GetLength(0); this.columns = mm.GetLength(1);
            data = new double[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    data[i, j] = mm[i, j];
        }
        
        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 && j < 0 && i >= rows && j >= columns)
                {
                    // Console.WriteLine(" Индексы вышли за пределы матрицы ");
                    return Double.NaN;
                }
                else
                    return data[i, j];
            }
            set
            {
                if (i < 0 && j < 0 && i >= rows && j >= columns)
                {
                    //Console.WriteLine(" Индексы вышли за пределы матрицы ");
                }
                else
                    data[i, j] = value;
            }
        }
        
        public Vector GetRow(int r)
        {
            if (r >= 0 && r < rows)
            {
                Vector row = new Vector(columns);
                for (int j = 0; j < columns; j++) row[j] = data[r, j];
                return row;
            }
            return null;
        }

        public void PrintMatrix(Matrix m)
        {
            if (m == null) return;
            int n = m.rows;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(m.GetRow(i));
            }
        }
        
        public Vector GetColumn(int c)
        {
            if (c >= 0 && c < columns)
            {
                Vector column = new Vector(rows);
                for (int i = 0; i < rows; i++) column[i] = data[i, c];
                return column;
            }
            return null;
        }
        public bool SetRow(int index, Vector r)
        {
            if (index < 0 || index > rows) return false;
            if (r.Size != columns) return false;
            for (int k = 0; k < columns; k++) data[index, k] = r[k];
            return true;
        }
        public bool SetColumn(int index, Vector c)
        {
            if (index < 0 || index > columns) return false;
            if (c.Size != rows) return false;
            for (int k = 0; k < rows; k++) data[k, index] = c[k];
            return true;
        }

        public void PutZeros()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    data[i, j] = 0;
                }
            }
        }
        
        public void SwapRows(int r1, int r2)
        {
            if (r1 < 0 || r2 < 0 || r1 >= rows || r2 >= rows || (r1 == r2)) return;
            Vector v1 = GetRow(r1);
            Vector v2 = GetRow(r2);
            SetRow(r2, v1);
            SetRow(r1, v2);
        }
        public Matrix Copy()
            {
                Matrix r = new Matrix(rows, columns);
                for (int i = 0; i < rows; i++)
                    for (int j = 0; j < columns; j++) r[i, j] = data[i, j];
                return r;
            }
        public Matrix Transpose()
        {
            Matrix transposeMatrix = new Matrix(columns, rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    transposeMatrix.data[j, i] = data[i, j];
                }
            }
            return transposeMatrix;
        }
        //Сложение матриц
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
            {
                throw new Exception("Матрицы не совпадают по размерности");
            }
            Matrix result = new Matrix(m1.rows, m1.columns);

            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m2.columns; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return result;
        }
        
        public static Matrix operator -(Matrix m1, Matrix m2)     //Вычитание матриц
        {
            if (m1.rows != m2.rows || m1.columns != m2.columns)
            {
                throw new Exception("Матрицы не совпадают по размерности");
            }
            Matrix result = new Matrix(m1.rows, m1.columns);

            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m2.columns; j++)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix m, int digit)
        {
            if (m.rows != 0 && m.columns != 0)
            {
                throw new Exception("Матрица должна быть ненулевой");
            }
            
            Matrix result = new Matrix(m.rows, m.columns);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++) {
                    result[i, j] = m[i, j] * digit;
                }
            }

            return result;
        }

        public static Vector operator *(Matrix m, Vector v)
        {
            if (m.rows != v.Size)
            {
                throw new Exception("Ряды матрицы должны быть равны размеру вертикального вектора");
            }

            Vector result = new Vector(m.rows);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++) {
                    result[i] += m[i, j] * v[j];
                }
            }

            return result;
        }

        public static Vector operator *(Vector v, Matrix m)
        {
            if (m.rows != v.Size)
            {
                throw new Exception("Ряды матрицы должны быть равны размеру вертикального вектора");
            }

            Vector result = new Vector(m.rows);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++) {
                    result[i] += m[i, j] * v[j];
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.columns != m2.rows)
            {
                throw new Exception("Количество столбцов первой матрицы не равно количеству строк второй матрицы");
            }
            int iters = m1.columns;
            Matrix result = new Matrix(m1.rows, m2.columns);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m2.columns; j++) {
                    result[i, j] = 0;
                    for (int k = 0; k < iters; k++)
                    {
                        result[i, j] += m1[i, k] * m2 [k, j];
                    }
                }
            }
            return result;
        }

        public Matrix Pow(Matrix m, double power)
        {
            Matrix result = new Matrix(m.rows, m.columns);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.columns; j++)
                {
                    result[i,j] = Math.Pow(m[i,j], power);
                }
            }

            return result;
        }

        public double Det(Matrix m)
        {
            if (m.rows != m.columns)
                throw new Exception("Матрица должна быть квадратной");

            double s1 = 0;
            double s2 = 0;

            for (int i = 0; i < m.rows; i++)
                s1 *= m[i,i];

            for (int i = m.columns; i > 0; i--)
                s2 *= m[(m.columns-i), i];
            
            return s1 - s2;
        }

        public Vector TopTriangle(Matrix mat, Vector b)
        {
            double eps = 000000.1;
            if (mat.Rows != b.Size && mat.Rows != mat.Columns) return null;

            for (int i = 0; i < mat.Rows; i++)
            {
                if (mat[i, i] == 0.0) return null;
                
                for (int j = 0; j < i; j++)
                    if (Math.Abs(mat[i, j]) < eps) return null;
            }

            // корни
            Vector x = new Vector(mat.rows);
            
            // записываю очевидное
            x[mat.rows - 1] = b[mat.rows - 1] / mat[mat.rows - 1, mat.rows - 1];
            
            // основной цикл
            for (int i = mat.rows - 2; i >= 0; i--)
            {
                double sum = 0;
                
                for (int j = i + 1; j < mat.rows; j++)
                    sum += mat[i, j] * x[j];
                
                x[i] = (b[i] - sum) / mat[i, i];
            }
            
            return x;
        }
        
        public Vector BottomTriangle(Matrix mat, Vector b)
        {
            double eps = 000000.1;
            
            if (mat.Rows != b.Size && mat.Rows != mat.Columns) return null;

            for (int i = 1; i < mat.Rows; i++)
            {
                if (mat[i, i] == 0.0) return null;
                
                for (int j = i + 1; j < i; j++)
                    if (Math.Abs(mat[i, j]) < eps) return null;
            }

            // корни
            Vector x = new Vector(mat.rows);
            
            // записываю очевидное
            x[0] = b[0] / mat[0, 0];
            
            // основной цикл
            for (int i = 0; i < mat.rows; i++)
            {
                double sum = 0;
                
                for (int j = 0; j < i; j++)
                    sum += mat[i, j] * x[j];
                
                x[i] = (b[i] - sum) / mat[i, i];
            }
            return x;
        }
        
        public Vector Gauss(Matrix mat, Vector b)
        {
            Matrix A = mat.Copy();
            Vector B = b.Copy();
            int rows = A.Rows;
            
            if (rows != B.Size || rows != A.columns) throw new Exception("Не совпадает размерность!");
            
            for (int k = 0; k < rows; k++)
            {
                double maxElement = Math.Abs(A[k, k]);
                int maxIndexRow = k;
                
                for (int i = k + 1; i < rows; i++)
                {
                    if (Math.Abs(A[i, k]) > maxElement)
                    {
                        maxElement = Math.Abs(A[i, k]);
                        maxIndexRow = i;
                    }
                }
                
                /// Проверка на ненулевое элемент
                if (maxElement < 0.00000001)
                {
                    throw new Exception("Матрицы невозможно перемножить");
                }
                //Меняем местами строки
                for (int j = k; j < rows; j++)
                {
                    double tmp = A[maxIndexRow, j];
                    A[maxIndexRow, j] = A[k, j];
                    A[k, j] = tmp;
                    
                }
                
                //Меняем местами значения вектора B
                double temp = B[maxIndexRow];
                B[maxIndexRow] = B[k];
                B[k] = temp;
                

                //Проходим по всем строкам после k-ой
                for (int i = 0; i < rows; i++)
                {
                    if (i == k) continue;
                    double value = A[i, k] / A[k, k];
                    for (int j = k; j < rows; j++)
                        A[i, j] -= A[k, j] * value;
                    B[i] -= B[k] * value;
                }
            }
            return TopTriangle(A, B);
        }

        // TODO: Вычисление обратной матрицы Метод Гаусса
        public Matrix InvertedG(Matrix mat)
        {
            int rows = mat.rows;
            int columns = mat.columns;
            if (rows != columns) return null;
            Matrix aCopy = mat.Copy();
            Matrix result = new Matrix(rows, columns);
            Matrix E = new Matrix(rows, columns);
            
            for (int i = 0; i < rows; i++)
                E[i, i] = 1;

            double eps = 0.000001;
            int max;
            
            for (int j = 0; j < columns; j++)
            {
                max = j;
                for (int i = j + 1; i < rows; i++)
                    if (Math.Abs(aCopy[i, j]) > Math.Abs(aCopy[max, j])) { max = i; };

                if (max != j)
                {
                    Vector temp = aCopy.GetRow(max); aCopy.SetRow(max, aCopy.GetRow(j)); aCopy.SetRow(j, temp);
                    Vector tmp = E.GetRow(max); E.SetRow(max, E.GetRow(j)); E.SetRow(j, tmp);
                }

                if (Math.Abs(aCopy[j, j]) < eps) return null;

                for (int i = j + 1; i < rows; i++)
                {
                    double multiplier = -aCopy[j, j] / aCopy[i, j];
                    for (int k = 0; k < columns; k++)
                    {
                        aCopy[i, k] *= multiplier;
                        aCopy[i, k] += aCopy[j, k];
                        E[i, k] *= multiplier;
                        E[i, k] += E[j, k];
                    }
                }
            }

            Vector column = new Vector(columns);
            for (int i = 0; i < columns; i++)
            {
                column = TopTriangle(aCopy, E.GetColumn(i));
                if (column == null) return null;
                result.SetColumn(i, column);
            }

            return result;
        }

        // Метод прогонки (всего 3 диагонали. остальное нули)
        public Vector Sweep(Vector overMainDia, Vector mainDia, Vector underMainDia, Vector f)
        {
            int n = mainDia.Size;
            Vector x = new Vector(n);
            Vector alpha = mainDia.Copy();
            Vector beta = f.Copy();

            if (f.Size == n) return null;
            
            double m;
            double eps = 0.00000001;

            for (int i = 0; i < overMainDia.Size; i++)
            {
                if (Math.Abs(overMainDia[i]) < eps) return null;
            }
            
            for (int i = 0; i < mainDia.Size; i++)
            {
                if (Math.Abs(mainDia[i]) < eps) return null;
            }
            
            for (int i = 0; i < underMainDia.Size; i++)
            {
                if (Math.Abs(underMainDia[i]) < eps) return null;
            }
            
            for (int i = 1; i < n; i++)
            {
                m = underMainDia[1] / alpha[i - 1];
                alpha[i] -= m * overMainDia[i - 1];
                beta[i] -= m * f[i - 1];
            }

            x[n - 1] = f[n - 1] / mainDia[n - 1];

            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = (f[i] - overMainDia[i] * x[i + 1]) / alpha[i];
            }

            return x;
        }
        
        public Vector GrammSchmidt(Matrix A, Vector B)
        {
            if (A.Rows != A.Columns || A.Rows != B.Size) return null;

            int n = A.Rows;
            Matrix R = new Matrix(n, n);
            Matrix T = new Matrix(n, n);
            for (int i = 0; i < n; i++)
            {
                T[i, i] = 1;
            }

            R.SetColumn(0, A.GetColumn(0));
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Vector a = A.GetColumn(i);
                    Vector r = R.GetColumn(j);
                    T[j, i] = a * r / (r * r);

                    Vector rNew = a;
                    for (int k = 0; k < i; k++)
                        rNew -= T[k, i] * R.GetColumn(k);

                    R.SetColumn(i, rNew);
                }
            }
            Matrix D = R.Transpose() * R;
            for (int i = 0; i < n; i++)
            {
                if (D[i, i] == 0) return null;
                D[i, i] = 1 / D[i, i];
            }
            Vector y = R.Transpose() * B * D;

            return TopTriangle(T, y);
        }

        // TODO: Метод Гивенса (вращения)
        public Vector Givens(Matrix mat, Vector b)
        {
            if (mat.Rows != mat.Columns || mat.Rows != b.Size) return null;
            
            Matrix A = mat.Copy();
            Vector B = b.Copy();
            int n = A.Rows;
            
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {   
                    // находим косинус и синус для матрицы вращения
                    double cos = A[i, i] / Math.Sqrt(A[i, i] * A[i,i] + A[j, i] * A[j,i]);
                    double sin = A[j, i] / Math.Sqrt(A[i, i] * A[i, i] + A[j, i] * A[j, i]);
                    
                    
                    // матрица вращения
                    Matrix Q = new Matrix(n, n);
                    
                    for (int k = 0; k < n; k++) 
                        Q[k, k] = 1;
                    
                    Q[i, i] = cos; Q[j, j] = cos;
                    Q[j, i] = -sin; Q[i, j] = sin;
                    
                    A = Q * A;
                    B = Q * B;
                }
            }
            // решаем уравнение обратным ходом
            return TopTriangle(mat, b);
        }
        
        // метод последовательных приблежений
        public Vector IterativeApprox(Matrix mat, Vector b)
        {
            if (mat.Rows != mat.Columns || mat.Rows != b.Size)
                return null;

            int n = mat.Rows;
            double eps = 0.0000001;
            int max;

            for (int i = 0; i < n; i++)
            {
                max = i;
                for (int j = i + 1; j < n; j++)
                    // Поиск максимума
                    if (Math.Abs(mat[j, i]) > Math.Abs(mat[max, i])) max = j;

                if (max != i)
                {
                    mat.SetRow(max, mat.GetRow(i));
                    mat.SetRow(i, mat.GetRow(max));
                    
                    double tmp;
                    tmp = b[max];
                    b[max] = b[i];
                    b[i] = tmp;
                }
                
                if (Math.Abs(mat[i, i]) < eps) return null;
            }

            Matrix alpha = new Matrix(n, n); 
            Vector beta = new Vector(n);
            
            for (int i = 0; i < n; i++)
            {
                if (mat[i, i] == 0) return null;
                
                beta[i] = b[i] / mat[i, i];
                
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        alpha[i, j] = mat[i, j] / mat[i, i];
                    else
                        alpha[i, j] = 0;
                }
                
                beta[i] = b[i] / mat[i, i];
            }

            Vector prev_x = beta;
            Vector x;
            Vector delta;

            do
            {
                x = beta - alpha * prev_x;
                delta = x - prev_x;
                prev_x = x;
            } while (delta.Norma1() > eps);

            return prev_x;
        }
    }
}

