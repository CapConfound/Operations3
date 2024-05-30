using System;

namespace Operations_2
{
    public class Vector
    {
        protected int size;
        protected double[] data;
        private static Random rnd = new Random();
        public int Size => size;

        public Vector(int size)
        {
            this.size = size;
            data = new double[size];
        }

        public double[] GetElements()
        {
            return data;
        }

        public Vector(double[] v)
        {
            size = v.Length;
            data = new double[size];

            for (var i = 0; i < size; i++) data[i] = v[i];
        }

        public double this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public int GetSize() => size;

        public bool SetElement(double el, int index)
        {
            if (index < 0 || index >= size) return false;
            data[index] = el;
            return true;
        }

        public double GetElement(int index)
        {
            if (index < 0 || index >= size) return default;
            return data[index];
        }

        public Vector Copy()
        {
            Vector rez = new Vector(data);
            return rez;
        }

        public override string ToString() => $"{{{string.Join(";", data)}}}";

        // первая Евклидова норма (расстояние)
        public double Norma1()
        {
            double s = 0;
            for (var i = 0; i < size; i++)
                s += Math.Pow(data[i], 2);
            return Math.Sqrt(s);
        }

        // Вторая форма максимум
        public double Norma2()
        {
            double s = 0;
            for (var i = 0; i < size; i++)
                if (Math.Abs(data[i]) > s)
                    s = Math.Abs(data[i]);
            return s;
        }

        // Третья форма
        public double Norma3()
        {
            double s = 0;
            for (var i = 0; i < size; i++)
                s += Math.Abs(data[i]);
            return s;
        }

        public double ScalarMultiply(Vector b)
        {
            if (size != b.size) return 0;
            double s = 0;
            for (var i = 0; i < size; i++)
                s += data[i] * b.data[i];
            return s;
        }

        public Vector MultiplyScalar(double c)
        {
            var rez = new Vector(size);
            for (var i = 0; i < size; i++) rez.data[i] = data[i] * c;
            return rez;
        }

        public Vector Normalize()
        {
            var rez = new Vector(size);
            var d = Norma1();
            for (var i = 0; i < size; i++)
                if (d != 0) rez.data[i] = data[i] / d;
                else rez.data[i] = data[i];
            return rez;
        }

        public static Vector NormalizeRandom(int size)
        {
            var rez = new Vector(size);
            for (var i = 0; i < size; i++)
                rez.data[i] = (rnd.NextDouble() - 0.5) * 2.0;
            return rez.Normalize();
        }

        public Vector UMinus()
        {
            var rez = new Vector(size);
            for (var i = 0; i < size; i++) rez.data[i] = -data[i];
            return rez;
        }

        public Vector Add(Vector c)
        {
            var rez = new Vector(size);
            for (var i = 0; i < size; i++) rez.data[i] = data[i] + c.data[i];
            return rez;
        }

        public Vector Minus(Vector c)
        {
            var rez = new Vector(size);
            for (var i = 0; i < size; i++) rez.data[i] = data[i] - c.data[i];
            return rez;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            if (a.size == b.size)
            {
                var c = new Vector(a.size);
                for (var i = 0; i < a.size; i++)
                    c[i] += a[i] + b[i];
                return c;
            }

            return null;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            if (a.size != b.size) return null;

            var c = new Vector(a.size);
            for (var i = 0; i < a.size; i++)
                c[i] += a[i] - b[i];
            return c;
        }

        public static Vector operator *(Vector a, double c)
        {
            var r = new Vector(a.size);
            for (var i = 0; i < a.size; i++)
                r[i] = a[i] * c;
            return r;
        }

        public static Vector operator *(double c, Vector a)
        {
            var r = new Vector(a.size);
            for (var i = 0; i < a.size; i++)
                r[i] = a[i] * c;
            return r;
        }

        public static double operator *(Vector a, Vector b)
        {
            if (a.size == b.size)
            {
                var s = 0.0;
                for (var i = 0; i < a.size; i++)
                    s += a[i] * b[i];
                return s;
            }

            return double.NaN;
        }

        public static Vector operator /(Vector b, double c)
        {
            var n = b.Size;
            var result = new Vector(n);

            for (var i = 0; i < n; i++) result[i] = b[i] / c;
            return result;
        }
        
        public  Vector Inverse()
        {
            Vector result = new Vector(size);
            for (int i = 0; i < size; i++)
            {
                if (data[i] != 0)
                {
                    result[i] = 1.0 / data[i];
                }
                else
                {
                    result[i] = 0;
                }
            }
            return result;
        }

        public void PutZeros()
        {
            for (int i = 0; i < size; i++)
            {
                data[i] = 0.0;
            }
        }
        
        public void PutVals(double val = Double.NaN)
        {
            for (int i = 0; i < size; i++)
            {
                data[i] = val;
            }
        }
        
    }
}