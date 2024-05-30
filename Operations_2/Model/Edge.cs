namespace Operations_2
{
    public class Edge
    {
        // Начальная вершина
        public Vertex Source;

        // Конечная вершина
        public Vertex Destination;

        // Длина ребра
        public double Distance;

        // Конструктор
        public Edge(Vertex begin, Vertex end, double distance)
        {
            Source = begin;
            Destination = end;
            Distance = distance;
        }

        public override string ToString() => "{"+Source.Label + "  " + Destination.Label + " D=" + Distance+"}";
    }
}