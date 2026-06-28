using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPaint
{
    public class Graph
    {
        public List<List<int>> adjacencyList; //Список смежности графа
        public List<List<int>> adjacencyListOrdered; //Список смежности графа
        public List<(int X, int Y)> coordinates;
        public bool isDirected = true; //Ориентированность грфа
        public Graph()
        {
            adjacencyList = new List<List<int>>();//Инициализация списка смежности
            adjacencyListOrdered = new List<List<int>>();
            coordinates = new List<(int X, int Y)>();//Инициализация списка координат
        }
        public void AddVertex(int X, int Y)//Метод добавления и создания вершин графа
        {
            int VertexId = adjacencyList.Count;
            adjacencyList.Add(new List<int>());
            adjacencyListOrdered.Add(new List<int>());
            coordinates.Add((X, Y));
        }
        public void AddEdge(int current, int added)//Метод добавления и создания рёбер графа
        {
            if (isDirected)//Формирование списка смежности для ориентированного графа
            {
                adjacencyList[current].Add(added);
                adjacencyListOrdered[current].Add(added);
            }
            else //Формирование списка смежности для неориентированного графа
            {
                adjacencyList[current].Add(added);
                adjacencyList[added].Add(current);
                adjacencyListOrdered[current].Add(added);
            }
        }
        public void AddCoordinates(int vertex,int x, int y)//Добавление координат вершины графа
        {
            coordinates[vertex] = (x, y); 
        }
        public void DisplayAdjacencyList(ListBox listBox)//Отобразить список смежности графа
        {
            listBox.Items.Clear();
            for (int i = 0; i < adjacencyList.Count; i++)
            {
                string neighbours = string.Join(", ", adjacencyList[i]);
                if (string.IsNullOrEmpty(neighbours))
                {
                    neighbours = "Нет соседей";

                }
                listBox.Items.Add($"Вершина {i} -> {neighbours}");
            }
        }
    }
}
