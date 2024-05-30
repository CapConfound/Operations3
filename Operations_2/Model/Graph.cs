using System;
using System.Collections.Generic;


namespace Operations_2
{
    public class Graph
    {
        public List<Vertex> Vertices { get; set; }
        public List<Edge> Edges { get; set; }

        public Graph()
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }
        
        public bool AddEdge(Vertex v1, Vertex v2, double d)
        {
            if (!Vertices.Contains(v1)) return false;
            if (!Vertices.Contains(v2)) return false;
            foreach (Edge cure in v1.GetEdges())
            {
                if (cure.Destination.GetId() == v2.GetId()) return false;
            }
                

            Edge ev1v2 = new Edge(v1, v2, d);
            v1.GetEdges().Add(ev1v2); Edges.Add(ev1v2);
            return true;
        }
        
        public Dictionary<Vertex, Vertex> BFS(Vertex startVertex, Vertex endVertex = null)
        {
            
            Queue<Vertex> Queue = new Queue<Vertex>();
            List<Vertex> visited = new List<Vertex>();
            
            // Create a dictionary to store the path
            Dictionary<Vertex, Vertex> path = new Dictionary<Vertex, Vertex>();
            
            visited.Add(startVertex);

            
            // Инициализация
            foreach(Vertex cv in Vertices)
            {
                cv.SumDistance = double.MaxValue;
                cv.PrevVertex = null;
                cv.IsVisited = false;
            }
            
            startVertex.Color = Color.Gray;
            
            startVertex.SumDistance = 0;
            startVertex.IsVisited = true;
            Queue.Enqueue (startVertex);
            
            Vertex current, v;
            List<Edge> edges_u;
            
            // Основной цикл
            while (Queue.Count > 0)
            {
                current = Queue.Dequeue();
                if (current.Equals(endVertex) && endVertex != null) break;
                edges_u = current.GetEdges();
                current.IsVisited = true;
                
                foreach (Edge edge in edges_u)
                {
                    v = edge.Destination;
                    if (!v.IsVisited)
                    {
                        path[current] = v;
                        Queue.Enqueue(v);
                        visited.Add(v);
                        v.IsVisited = true;
                    }
                }
            }
            return path;
        }

        // Depth-First Search
        public Dictionary<Vertex, Vertex> DFS(Vertex startVertex, Vertex endVertex = null)
        {
            Stack<Vertex> stack = new Stack<Vertex>();
            
            // Инициализация
            foreach (Vertex vert in Vertices)
            {
                vert.IsVisited = false;
                vert.PrevVertex = null;
            }

            // Путь
            Dictionary<Vertex, Vertex> path = new Dictionary<Vertex, Vertex>();
            
            Vertex v = startVertex;
            stack.Push(v);
            
            while (stack.Count > 0)
            {
                v = stack.Pop();
                if (!v.IsVisited)
                {
                    v.IsVisited = true;
                     
                    foreach (var edge in v.GetEdges())
                    {
                        stack.Push(edge.Destination);
                        path[v] = edge.Destination;
                        if (edge.Destination == endVertex && endVertex != null) return path;
                    }
                }
            }

            return path;
        }
        

        private List<Vertex> GetPath(Dictionary<Vertex, Vertex> parents, Vertex start, Vertex end)
        {
            List<Vertex> path = new List<Vertex>();

            if (!parents.ContainsKey(end))
            {
                return null;
            }

            Vertex current = end;

            while (current != start)
            {
                path.Add(current);
                current = parents[current];
            }

            path.Add(start);

            path.Reverse();

            return path;
        }
        
        
        // Метод определения кол-ва подграфов на основе BFS
        public int CountConnectedComponents()
        {
            // Инициализируем переменную, чтобы отслеживать кол-во компонент
            int count = 0;
            // Выполняем итерацию по всем вершинам графа
            foreach (Vertex v in Vertices)
            {
                // Проверка, не посещена ли вершина(цвет белый)
                if (v.IsVisited)
                {
                    // Выполняем поиск в ширину, начиная с текущей вершины
                    BFS(v);
                    count++;
                }
            }
            // Возвращаем кол-во связанных компонент
            return count;
        }
        
        // Просмотр всех вершины
        public void ViewAllVertexes()
        {
            Console.WriteLine("All vertices:");
            foreach (Vertex v in Vertices)
            {
                Console.WriteLine(v);
            }
        }
        
        // Просмотр всех ребер
        public void ViewAllEdges()
        {
            Console.WriteLine("All edges:");
            foreach (Edge e in Edges)
            {
                Console.WriteLine(e);
            }
        }
        
        // Просмотр графа
        public void ViewGraph()
        {
            Console.WriteLine("Graph:");
            foreach (Vertex v in Vertices)
            {
                Console.Write(v + ": ");
                v.ViewEdges();
            }
        }
    }

}


