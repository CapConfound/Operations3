using System;
using System.ComponentModel;

namespace Operations_2
{
    class TestTransportTheory
    {

        public static TransportTheory GetDemoObject()
        {
            // public  double[] a_arr = { 140, 180, 160 };
            Vector A = new Vector(new double[] { 140, 180, 160 });

            // public double[] b_arr = { 60, 70, 120, 130, 100 };
            Vector B = new Vector(new double[] { 60, 70, 120, 130, 100 });

            // public double[,] c_arr = { { 2, 3, 4, 2, 4 }, { 3, 4, 1, 4, 1 }, { 9, 7, 3, 7, 2 } };
            Matrix C = new Matrix(new double[,] { { 2, 3, 4, 2, 4 }, { 3, 4, 1, 4, 1 }, { 9, 7, 3, 7, 2 } });

            TransportTheory DemoObj = new TransportTheory(A, B, C);

            return DemoObj;
        }
        
        public static void TestNorthWestCornerRule()
        {

            TransportTheory test1 = GetDemoObject();
           
            Matrix baseX = test1.NorthWestCornerRule();

            baseX.PrintMatrix();
            
            test1.GetPotentials();
            
            TransportTheory test2 = GetDemoObject();

        }
        
        public static void TestMinCost()
        {

            TransportTheory test1 = GetDemoObject();
           
            Matrix baseX = test1.MinCost();

            baseX.PrintMatrix();
            

        }
        

        public static void TestFindJ()
        {
            TransportTheory test1 = GetDemoObject();
           
            Matrix testM = test1.NorthWestCornerRule();
        }

    }
}
