namespace Operations_2
{
    public class TestCommivoyager
    {
        static Matrix adjacencyTable = new Matrix(new double[,] { { 2, 3, 4, 2, 4 }, { 3, 4, 1, 4, 1 }, { 9, 7, 3, 7, 2 } });
        
        

        public static void TestCommy()
        {
            Commivoyager com = new Commivoyager();
            
            com.FindShortestPath(adjacencyTable, 2);
            
        }

        
    }
}