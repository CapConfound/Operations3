using System;
using System.Linq;
using Operations_2;

namespace Operations_2
{
    public class TransportTheory
    {
        private Vector A;
        private Vector B;
        
        private Matrix C;
        private int n;
        private int m;
        
        // Производные матрицы
        private Matrix X;
        private Matrix Delta;
        
        // Для вычисления дельты
        private Vector V;
        private Vector U;

        // Структура для дельты
        private MinDelta deltamin;
        
        private Vector col_sum;
        private Vector row_sum;
        
        public TransportTheory(
            Vector A,
            Vector B,
            Matrix C
           // int n,
           // int m
        )
        {
            this.A = A.Copy();
            this.B = B.Copy();
            this.C = C.Copy();
            n = A.Size;
            m = B.Size;
            
            if (C.Rows != n || C.Columns != m) throw new ArgumentException("Размерность матрицы C неверна.");
            
            X = new Matrix(n, m);
            X.PutZeros();
            col_sum = new Vector(m);
            col_sum.PutZeros();
            row_sum = new Vector(n);
            row_sum.PutZeros();
            
            U = new Vector(m);
            U.PutVals(Double.MaxValue);
            V = new Vector(n);
            V.PutVals(Double.MaxValue);
            V[0] = 0;
        }

        public Matrix NorthWestCornerRule()
        {
            double[] temp_row = new double[m];

            for (int row = 0; row < n; row++)
            {
                temp_row = new double[m];
                for (int i = 0; i < m; i++)
                {
                    temp_row[i] = 0;
                }
                
                for (int col = 0; col < m; col++)
                {
                    double value;
                 
                    if (Math.Abs(col_sum[col] - B[col]) < 1)
                    {
                        value = 0;
                    }
                    else
                    {
                        double col_rem = B[col] - col_sum[col]; 
                        double row_rem = A[row] - row_sum[row];

                        value = col_rem < row_rem ? col_rem : row_rem;
                    }
                    
                    col_sum[col] += value;
                    row_sum[row] += value;
                    
                    temp_row[col] = value;
                }
                X.SetRow(row, new Vector(temp_row));
            }

            return X;
        }

        public Matrix MinCost()
        {
            Matrix CCopy = C.Copy();
            
            while (true)
            {
                (int i, int j) = FindMinCost(ref CCopy);
                if (i < 0 || j < 0) break;
                double value;
                 
                if (Math.Abs(col_sum[j] - B[j]) < 1)
                {
                    value = 0;
                }
                else
                {
                    double col_rem = B[j] - col_sum[j]; 
                    double row_rem = A[i] - row_sum[i];

                    value = col_rem < row_rem ? col_rem : row_rem;
                }
                    
                col_sum[j] += value;
                row_sum[i] += value;

                X[i, j] = value;

            }
            
            return X;
        }
        
        public void GetPotentials()
        {
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.WriteLine("i: " + i + " j: " + j);
                    if (X[i, j] != null && X[i, j] <= 0) continue;

                    if (Math.Abs(Double.MaxValue - U[j]) < 0.001 && Math.Abs(Double.MaxValue - V[i]) > 0.001) 
                    {
                        U[j] = C[i, j] - V[i];
                    }
                    else if (Math.Abs(Double.MaxValue - U[j]) > 0.001 && Math.Abs(Double.MaxValue - V[i]) < 0.001)
                    {
                        V[i] = C[i, j] - U[j];
                    }
                }
                
            }
        }

        public Matrix GetDelta(Matrix X, Vector V, Vector U)
        {
            Matrix slau = new Matrix(n, m);
            
            for (int row = 0; row < n; row++)
            {
                double[] temp_row = {};
                for (int i = 0; i < m; i++)
                {
                    temp_row[i] = 0;
                }
                for (int col = 0; col < m; col++)
                {
                    if (X[row,col] == 0) continue;
                    
                }
                slau.SetRow(row, new Vector(temp_row));
            }

            return slau;
        }
        
        private (int, int) FindMinCost(ref Matrix CMatrix)
        {
            double min = Double.MaxValue;
            int i = 0;
            int j = 0;
            bool found = false;
        
            for (int row = 0; row < CMatrix.Rows; row++)
            {
                for (int col = 0; col < CMatrix.Columns; col++)
                {
                    if (CMatrix[row, col] < min)
                    {
                        min = CMatrix[row, col];
                        i = row;
                        j = col;
                        found = true;
                    }
                }
            }
            
            if (found)
            {
                CMatrix[i, j] = Double.MaxValue;

                return (i, j);
            }
            else
            {
                return (-1, -1);
            }
        }
    }
}