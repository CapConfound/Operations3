using System;
using System.Collections.Generic;

namespace Operations_2
{

    public enum Color
    {
        Red,
        Black,
        White,
        Gray
    }

    /// <summary>
    /// Вершина графа
    /// </summary>
    public class Vertex
    {
        private static int IdV = 0;

        private int Id;

        // Метка (имя вершины)
        public string Label;

        // Список ребер, связанных с вершиной
        private List<Edge> _edges;

        // Сумма растояний
        public double SumDistance;

        // Цвет вершины
        public Color Color;

        // Ссылка на предшественника
        public Vertex PrevVertex;

        // Посещена ли вершина
        public bool IsVisited;


        public Vertex(string Label) // Конструктор
        {
            this.Label = Label;
            IdV++;
            _edges = new List<Edge>();
            SumDistance = Double.MaxValue;
            Color = Color.White;
            PrevVertex = null;
            Id = IdV;
            IsVisited = false;
        }


        public int GetId() { return Id; }
    
        // Получение списка ребер
        public List<Edge> GetEdges() { return _edges; }

        public override string ToString() => Label + "  Id=" + Id;
    
    
        // Просмотр ребер, связанных с вершиной
        public void ViewEdges()
        {
            Console.Write("Edges for {0}", this);
        
            foreach(Edge edge in GetEdges())
                Console.Write("  {0}", edge);
        
            Console.WriteLine();
        }
    
        // Добавление ребра
        public bool AddEdge(Edge edge)
        {
            if (edge.Source != this) return false;
        
            for (int i = 0; i < GetEdges().Count; i++)
            {
                Edge CurEdge = GetEdges()[i];
                if (edge.Destination.Equals(CurEdge.Destination)) return false;
            }
        
            GetEdges().Add(edge);
        
            return true;
        }        
    }

}