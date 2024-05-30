using System;
using System.Collections.Generic;
using System.IO;
using System;
using System.IO;

namespace Operations_2
{
    public class TravellingSalesman
    {
        private int[,] distances;
        private int numCities;

        public TravelingSalesman(int[,] distanceMatrix)
        {
            distances = distanceMatrix;
            numCities = distances.GetLength(0);
        }
        
        static List<int> ClosestNeighbor(int start)
        {
            int numCities = distances.GetLength(0);
            bool[] visited = new bool[numCities];
            List<int> tour = new List<int>();

            tour.Add(start);
            visited[start] = true;

            for (int i = 0; i < numCities - 1; i++)
            {
                int closestCity = -1;
                int shortestDistance = int.MaxValue;

                for (int j = 0; j < numCities; j++)
                {
                    if (!visited[j] && distances[tour[i], j] < shortestDistance)
                    {
                        closestCity = j;
                        shortestDistance = distances[tour[i], j];
                    }
                }

                tour.Add(closestCity);
                visited[closestCity] = true;
            }

            tour.Add(start); // Complete the tour by returning to the start city
            return tour;
        }
        public void FindShortestPath(Matrix adjacencyTable, int startVertex = -1)
        {
            int vertices_count = adjacencyTable.Columns;
            Graph resultingPath = new Graph();

            startVertex -= 1;

            double leastLenght = double.MaxValue;
            int leastVertex = 0;
            for (int row = 0; row < adjacencyTable.Rows; row++)
            {
                // Пропускаем дуги. Они равны нулю.
                if (row == startVertex) continue;
                
                if (leastLenght > adjacencyTable[row, startVertex])
                {
                    leastLenght = adjacencyTable[row, startVertex];
                    leastVertex = row;
                }
                
                
            }
            
            leastVertex += 1;
            
            Console.WriteLine("Least vertex: V" + leastVertex);





        }
    }
}