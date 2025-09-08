namespace Graph
{
    public class Vertex  //Класс описания вершин графов
    {
        private int x, y;
        List<int> numberOfTheVertexes = new List<int>();
        public Vertex(int x, int y, int numberOfTheVertexes)
        {
            this.x = x;
            this.y = y;
            this.numberOfTheVertexes.Add(numberOfTheVertexes);
        }
    }
    public class Edge //Класс описания ребер графов
    {
        private int x, y;
        List<int> numberOfTheEdges = new List<int>();
        public Edge(int x, int y, int numberOfTheEdge)
        {
            this.x = x;
            this.y = y;
            this.numberOfTheEdges.Add(numberOfTheEdge);
        }
    }
    internal class Graph
    {
        
    }
}
