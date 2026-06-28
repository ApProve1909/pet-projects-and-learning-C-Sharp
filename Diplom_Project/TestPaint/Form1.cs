using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPaint
{


    public partial class Form1 : Form
    {
        public Bitmap pictOfGrapf; // Переменная, хранящая нарисованный граф
        Graphics g; //Графика для рисования
        int a = 20, b = 20; // Данные для эллипса
        int fromVertex = 0, toVertex = 0; //Переменные, испоьзуемые при построении рёбер
        Pen pen = new Pen(Color.Black, 2); // Ручка для рисования графа
        private List<int> DFSOrder = new List<int>();//Список порядка поиска в глубину
        private List<int> BFSOrder = new List<int>();//Список порядка поиска в ширину
        private bool isDrawing = true; //Статус инструмента рисования графа
        private bool isDeleting = false; //Статус инструмента удаления графа
        private bool isDrawingEdge = false; //Статус рисования ребра
        private int edges = 1;
        public Form1()
        {
            InitializeComponent();
            pictOfGrapf = new Bitmap(pictureBox1.Width, pictureBox1.Height); //Инициализиуем Bitmap размерами pictureBox1
            g = Graphics.FromImage(pictOfGrapf); //Задаем холст, на котором будет происходит рисование
            label4.Text = "Статус: Рисование";
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                //Поиск номера вершины в точки клика курсора мыши
                int nearestIndex = FindNearestVetrex(e.X, e.Y, a, b);
                g = Graphics.FromImage(pictOfGrapf);//Макет для рисования - Bitmap

                if (nearestIndex == -1)
                {
                    Program.graph.AddVertex(e.X, e.Y);//Добавляем координаты вершины в список
                    DrawGraph();//Рисуем граф
                    pictureBox1.Image = pictOfGrapf;//Применяем результат к области рисования
                    pictureBox1.Invalidate();//Обновляем область для ототражения результата
                    fromVertex = Program.graph.coordinates.Count - 1; // Последняя добавленная вершина
                }
                else
                {
                    fromVertex = nearestIndex;
                }
                isDrawingEdge = true;
                if (isDeleting)
                {
                    RedrawGraphWithHighlight(fromVertex);
                    pictureBox1.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                //Поиск номера вершины в точки клика курсора мыши
                int nearestIndex = FindNearestVetrex(e.X, e.Y, a, b);
                g = Graphics.FromImage(pictOfGrapf);//Макет для рисования - Bitmap
                if (nearestIndex == -1)
                {
                    Program.graph.AddVertex(e.X, e.Y);//Добавляем координаты вершины в список
                    DrawGraph();//Рисуем граф
                    toVertex = Program.graph.coordinates.Count - 1;

                    // Используем универсальный метод для рисования рёбер
                    DrawEdge(g, pen,
                        Program.graph.coordinates[fromVertex].X,
                        Program.graph.coordinates[fromVertex].Y,
                        Program.graph.coordinates[toVertex].X,
                        Program.graph.coordinates[toVertex].Y
                        );

                    Program.graph.AddEdge(fromVertex, toVertex);//Добавляем данные ребра в список
                    pictureBox1.Image = pictOfGrapf;//Применяем результат к области рисования
                    pictureBox1.Invalidate();//Обновляем область для ототражения результата


                }
                else
                {
                    toVertex = nearestIndex;
                    Program.graph.AddEdge(fromVertex, toVertex);
                    DrawEdge(g, pen,
                        Program.graph.coordinates[fromVertex].X,
                        Program.graph.coordinates[fromVertex].Y,
                        Program.graph.coordinates[toVertex].X,
                        Program.graph.coordinates[toVertex].Y);
                   
                    
                    pictureBox1.Image = pictOfGrapf;
                    pictureBox1.Invalidate();
                }
                isDrawingEdge = false;
                fromVertex = 0;
                toVertex = 0;
                edges++;
            }
            if (isDeleting)
            {
                DeletingVertex(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            var temp = new Bitmap(pictOfGrapf);
            g = Graphics.FromImage(temp);
            label2.Text = ("X: " + e.X.ToString());
            label3.Text = ("Y: " + e.Y.ToString());

            if (isDrawingEdge)
            {
                // Рисуем линию к курсору (используем DrawEdge)
                if (Program.graph.isDirected)
                {
                    // Для ориентированного показываем стрелку
                    DrawDirectedEdge(g, pen,
                        Program.graph.coordinates[fromVertex].X,
                        Program.graph.coordinates[fromVertex].Y,
                        e.X, e.Y);
                }
                else
                {
                    // Для неориентированного просто линию
                    g.DrawLine(pen,
                        Program.graph.coordinates[fromVertex].X,
                        Program.graph.coordinates[fromVertex].Y,
                        e.X, e.Y);
                }
                pictureBox1.Image = temp;
                pictureBox1.Invalidate();
            }
        }
        //Меню
        private void обходВШиринуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BFS(0);
        }
        private void обходВГлубинуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DFS(0);
        }
        private void удалениеВершиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDeleting = true;
            isDrawing = false;
            label4.Text = "Статус: Удаление";
            MessageBox.Show("Выберите вершину для удаления");
        }

        private void рисованиеГрафаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDeleting = false;
            isDrawing = true;
            label4.Text = "Статус: Рисование";
        }

        private void показатьДанныеГрафаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.ShowDialog();
        }

        private void добавлениеВесаГрафуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 thirdForm = new Form3();
            thirdForm.ShowDialog();
        }

        private void рисованиеОриентированногоГрафаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.graph.isDirected = true;
            isDrawing = true;
            isDeleting = false;
            label4.Text = "Статус: Рисование (ориентированный)";

            RedrawGraph();
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 aboutProg = new Form4();
            aboutProg.ShowDialog();
        }
        private void рисованиеНеориентированногоГрафаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.graph.isDirected = false;
            isDrawing = true;
            isDeleting = false;
            label4.Text = "Статус: Рисование (неориентированный)";

            RedrawGraph();
        }
        //Метод для поиска существующей вершины в области клика мыши
        private int FindNearestVetrex(int x, int y, int width, int height)
        {
            int radius = Math.Max(width, height) / 2;
            int nearestIndex = -1;
            double minDistance = double.MaxValue;

            for (int i = 0; i < Program.graph.coordinates.Count; i++)
            {
                var coord = Program.graph.coordinates[i];
                double distance = Math.Sqrt(Math.Pow(x - coord.X, 2) + Math.Pow(y - coord.Y, 2));

                if (distance <= radius && distance < minDistance)
                {
                    minDistance = distance;
                    nearestIndex = i;
                }
            }
            return nearestIndex;
        }
        private void RedrawGraphWithHighlight(int highlightVertex)
        {
            using (Graphics g = Graphics.FromImage(pictOfGrapf))
            {
                g.Clear(Color.Aquamarine);

                //Рисуем ВСЕ рёбра
                using (Pen edgePen = new Pen(Color.Black, 2))
                {
                    for (int i = 0; i < Program.graph.adjacencyList.Count; i++)
                    {
                        foreach (var neighbor in Program.graph.adjacencyList[i])
                        {
                            if (i != neighbor)
                            {
                                DrawEdge(g, edgePen,
                                    Program.graph.coordinates[i].X,
                                    Program.graph.coordinates[i].Y,
                                    Program.graph.coordinates[neighbor].X,
                                    Program.graph.coordinates[neighbor].Y,
                                    15, 12);
                            }
                        }
                    }
                }

                //Рисуем вершины с подсветкой
                using (Pen myPen = new Pen(Color.Black, 2))
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                using (SolidBrush darkGrayBrush = new SolidBrush(Color.Aquamarine))
                using (Font vertexFont = new Font("Arial", 10))
                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    for (int i = 0; i < Program.graph.coordinates.Count; i++)
                    {
                        int rectX = Program.graph.coordinates[i].X - a / 2;
                        int rectY = Program.graph.coordinates[i].Y - a / 2;
                        Rectangle rect = new Rectangle(rectX, rectY, a, b);

                        if (i == highlightVertex)
                        {
                            g.FillEllipse(darkGrayBrush, rect);
                            g.DrawEllipse(myPen, rect);
                        }
                        else
                        {
                            g.FillEllipse(whiteBrush, rect);
                            g.DrawEllipse(myPen, rect);
                        }

                        g.DrawString((i + 1).ToString(), vertexFont, Brushes.Black, rect, stringFormat);
                    }
                }
            }

            pictureBox1.Image = pictOfGrapf;
            pictureBox1.Invalidate();
        }
        private void DeletingVertex(int x, int y)
        {
            //Поиск номера вершины в области клика
            int deletingVertex = FindNearestVetrex(x, y, a, b);

            if (deletingVertex != -1)
            {
                // Показываем удаляемую вершину тёмно-серой
                using (Pen darkGrayPen = new Pen(Color.DarkGray, 2))
                using (SolidBrush darkGrayBrush = new SolidBrush(Color.DarkGray))
                using (Graphics g = Graphics.FromImage(pictOfGrapf))
                {
                    RedrawGraphWithHighlight(deletingVertex);
                }

                // Удаляем все рёбра, связанные с удаляемой вершиной
                for (int i = 0; i < Program.graph.adjacencyList.Count; i++)
                {
                    Program.graph.adjacencyList[i].RemoveAll(v => v == deletingVertex);
                }

                // Удаляем саму вершину
                Program.graph.adjacencyList.RemoveAt(deletingVertex);
                Program.graph.coordinates.RemoveAt(deletingVertex);

                // Обновляем индексы ВО ВСЕХ СПИСКАХ
                for (int i = 0; i < Program.graph.adjacencyList.Count; i++)
                {
                    for (int j = 0; j < Program.graph.adjacencyList[i].Count; j++)
                    {
                        if (Program.graph.adjacencyList[i][j] > deletingVertex)
                        {
                            Program.graph.adjacencyList[i][j]--;
                        }
                    }
                }

                // Перерисовываем граф окончательно
                RedrawGraph();
            }
        }
        private void RedrawGraph()
        {
            using (Graphics g = Graphics.FromImage(pictOfGrapf))
            {
                g.Clear(Color.Aquamarine);

                // Рисуем все рёбра
                using (Pen edgePen = new Pen(Color.Black, 2))
                {
                    for (int i = 0; i < Program.graph.adjacencyList.Count; i++)
                    {
                        foreach (var neighbor in Program.graph.adjacencyList[i])
                        {
                            if (i != neighbor) // Не рисуем петли
                            {
                                // Используем универсальный метод
                                DrawEdge(g, edgePen,
                                    Program.graph.coordinates[i].X,
                                    Program.graph.coordinates[i].Y,
                                    Program.graph.coordinates[neighbor].X,
                                    Program.graph.coordinates[neighbor].Y,
                                    15, 12);
                            }
                        }
                    }
                }

                // Рисуем вершины
                DrawGraph(g);
            }

            pictureBox1.Image = pictOfGrapf;
            pictureBox1.Invalidate();
        }
        private async Task BFS(int start)
        {
            //Коллекция типа Hash для отслеживания посещенных вершин
            var visited = new HashSet<int>();
            //Очередь, в которой хранятся номера вершин, ожидающих своей обработки
            var queue = new Queue<int>();
            visited.Add(start);//Добавляем стартовую вершину в список посещенных
            queue.Enqueue(start);//Добавляем в очередь номер стартовой вершины
            BFSOrder.Add(start);//Список обхода графа

            while (queue.Count > 0)
            {
                //Извлекаем номер вершины из очереди и инициализируем её значение
                //как текущую рассматриваемую вершину
                var current = queue.Dequeue();

                // СОЗДАЁМ НОВЫЙ Bitmap на каждом шаге
                Bitmap currentFrame = new Bitmap(pictOfGrapf);

                using (SolidBrush visitedBrush = new SolidBrush(Color.Green))
                using (SolidBrush currentBrush = new SolidBrush(Color.Red))
                using (SolidBrush queueBrush = new SolidBrush(Color.Yellow))
                using (Graphics g = Graphics.FromImage(currentFrame))
                {
                    // Рисуем ВСЕ посещённые вершины
                    foreach (var v in visited)
                    {
                        int vX = Program.graph.coordinates[v].X - a / 2;
                        int vY = Program.graph.coordinates[v].Y - a / 2;
                        g.FillEllipse(visitedBrush, new Rectangle(vX, vY, a, b));
                    }

                    // Рисуем ВСЕ вершины в очереди (жёлтые)
                    foreach (var v in queue)
                    {
                        int vX = Program.graph.coordinates[v].X - a / 2;
                        int vY = Program.graph.coordinates[v].Y - a / 2;
                        g.FillEllipse(queueBrush, new Rectangle(vX, vY, a, b));
                    }

                    // Подсвечиваем текущую вершину (красная)
                    int curX = Program.graph.coordinates[current].X - a / 2;
                    int curY = Program.graph.coordinates[current].Y - a / 2;
                    g.FillEllipse(currentBrush, new Rectangle(curX, curY, a, b));

                    pictureBox1.Image = currentFrame;
                    pictureBox1.Invalidate();
                    await Task.Delay(400);

                    // Обрабатываем соседей
                    foreach (var neighbour in Program.graph.adjacencyList[current])
                    {
                        if (!visited.Contains(neighbour))
                        {
                            visited.Add(neighbour);
                            queue.Enqueue(neighbour);

                            // Показываем добавление в очередь
                            int nX = Program.graph.coordinates[neighbour].X - a / 2;
                            int nY = Program.graph.coordinates[neighbour].Y - a / 2;
                            g.FillEllipse(queueBrush, new Rectangle(nX, nY, a, b));

                            pictureBox1.Image = currentFrame;
                            pictureBox1.Invalidate();
                            await Task.Delay(300);
                        }
                    }

                    // После обработки всех соседей - текущая становится зелёной
                    g.FillEllipse(visitedBrush, new Rectangle(curX, curY, a, b));
                    pictureBox1.Image = currentFrame;
                    pictureBox1.Invalidate();
                    await Task.Delay(200);
                }
            }
        }
        private async Task DFSRecursive(int current, HashSet<int> visited)
        {
            visited.Add(current);
            DFSOrder.Add(current);

            Bitmap currentFrame = new Bitmap(pictOfGrapf);

            using (SolidBrush visitedBrush = new SolidBrush(Color.Green))
            using (SolidBrush currentBrush = new SolidBrush(Color.Red))
            using (SolidBrush neighbourBrush = new SolidBrush(Color.Yellow))
            using (Graphics g = Graphics.FromImage(currentFrame))
            {
                // Рисуем все посещённые вершины
                foreach (var v in visited)
                {
                    int vX = Program.graph.coordinates[v].X - a / 2;
                    int vY = Program.graph.coordinates[v].Y - a / 2;
                    g.FillEllipse(visitedBrush, new Rectangle(vX, vY, a, b));
                }

                // Подсвечиваем текущую вершину (поверх зелёных)
                int curX = Program.graph.coordinates[current].X - a / 2;
                int curY = Program.graph.coordinates[current].Y - a / 2;
                g.FillEllipse(currentBrush, new Rectangle(curX, curY, a, b));

                pictureBox1.Image = currentFrame;
                pictureBox1.Invalidate();
                await Task.Delay(400);

                foreach (var neighbour in Program.graph.adjacencyList[current])
                {
                    if (!visited.Contains(neighbour))
                    {
                        // Подсвечиваем соседа жёлтым (поверх всех)
                        int nX = Program.graph.coordinates[neighbour].X - a / 2;
                        int nY = Program.graph.coordinates[neighbour].Y - a / 2;
                        g.FillEllipse(neighbourBrush, new Rectangle(nX, nY, a, b));
                        pictureBox1.Image = currentFrame;
                        pictureBox1.Invalidate();
                        await Task.Delay(300);

                        // Рекурсивный вызов
                        await DFSRecursive(neighbour, visited);

                        // После возврата - обновляем отображение
                        // Перерисовываем все посещённые и текущую вершину
                        foreach (var v in visited)
                        {
                            int vX = Program.graph.coordinates[v].X - a / 2;
                            int vY = Program.graph.coordinates[v].Y - a / 2;
                            g.FillEllipse(visitedBrush, new Rectangle(vX, vY, a, b));
                        }

                        // Текущая вершина снова красная
                        g.FillEllipse(currentBrush, new Rectangle(curX, curY, a, b));

                        pictureBox1.Image = currentFrame;
                        pictureBox1.Invalidate();
                        await Task.Delay(200);
                    }
                }

                // После обработки всех соседей - текущая вершина становится просто зелёной
                foreach (var v in visited)
                {
                    int vX = Program.graph.coordinates[v].X - a / 2;
                    int vY = Program.graph.coordinates[v].Y - a / 2;
                    g.FillEllipse(visitedBrush, new Rectangle(vX, vY, a, b));
                }
                pictureBox1.Image = currentFrame;
                pictureBox1.Invalidate();
                await Task.Delay(200);
            }
        }
        private async Task DFS(int start)
        {
            var visited = new HashSet<int>();
            DFSOrder.Clear();
            await DFSRecursive(start, visited);
        }
        private void DrawEdge(Graphics g, Pen pen, int x1, int y1, int x2, int y2,
            int vertexRadius = 15, int arrowLength = 12)
        {
            if (Program.graph.isDirected)
            {
                // Рисуем ориентированное ребро со стрелкой
                DrawDirectedEdge(g, pen, x1, y1, x2, y2, vertexRadius, arrowLength);
            }
            else
            {
                // Рисуем неориентированное ребро (простая линия)
                g.DrawLine(pen, x1, y1, x2, y2);
            }
        }

        private void DrawDirectedEdge(Graphics g, Pen pen, int x1, int y1, int x2, int y2,
            int vertexRadius = 15, int arrowLength = 12)
        {
            double angle = Math.Atan2(y2 - y1, x2 - x1); //Вычисляем угол между вершинами
            int offsetX = (int)(vertexRadius * Math.Cos(angle));//Опрелеляяем отступы от вершин
            int offsetY = (int)(vertexRadius * Math.Sin(angle));

            int startX = x1 + offsetX;
            int startY = y1 + offsetY;
            int endX = x2 - offsetX;
            int endY = y2 - offsetY;

            g.DrawLine(pen, startX, startY, endX, endY);

            float arrowAngle = (float)Math.PI / 6;

            PointF arrowPoint1 = new PointF(
                endX - arrowLength * (float)Math.Cos(angle - arrowAngle),
                endY - arrowLength * (float)Math.Sin(angle - arrowAngle)
            );

            PointF arrowPoint2 = new PointF(
                endX - arrowLength * (float)Math.Cos(angle + arrowAngle),
                endY - arrowLength * (float)Math.Sin(angle + arrowAngle)
            );

            g.DrawLine(pen, endX, endY, arrowPoint1.X, arrowPoint1.Y);//Рисуем "перья" для ребра
            g.DrawLine(pen, endX, endY, arrowPoint2.X, arrowPoint2.Y);
        }

        private void сохранитьГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.DefaultExt = "graph";
                saveFileDialog.FileName = "my_graph.graph";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GraphFileManager.SaveGraph(saveFileDialog.FileName, Program.graph, pictOfGrapf);
                }
            }
        }

        private void загрузитьГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Graph files (*.graph)|*.graph|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (GraphFileManager.LoadGraph(openFileDialog.FileName, Program.graph, out Bitmap loadedImage))
                    {
                        // Обновляем изображение
                        pictOfGrapf = loadedImage;

                        // Перерисовываем граф
                        RedrawGraph();

                        // Обновляем интерфейс
                        pictureBox1.Image = pictOfGrapf;
                        pictureBox1.Invalidate();

                        // Обновляем список смежности (если открыта Form2)
                        // и другие элементы интерфейса
                        label4.Text = $"Загружен граф: {openFileDialog.FileName}";

                        MessageBox.Show("Граф успешно загружен!",
                            "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void очисткаДанныхИГрафаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Создать новый граф? Все несохранённые данные будут потеряны.",
       "Новый граф", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Очищаем граф
                Program.graph.adjacencyList.Clear();
                Program.graph.adjacencyListOrdered.Clear();
                Program.graph.coordinates.Clear();
                Program.graph.isDirected = true;
                edges = 1;
                // Очищаем изображение
                pictOfGrapf = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(pictOfGrapf))
                {
                    g.Clear(Color.Aquamarine);
                }

                // Обновляем интерфейс
                pictureBox1.Image = pictOfGrapf;
                pictureBox1.Invalidate();
                label4.Text = "Новый граф создан";

                // Очищаем списки обхода
                DFSOrder.Clear();
                BFSOrder.Clear();
            }
        }

       

        private void DrawGraph(Graphics g = null)
        {
            bool disposeGraphics = false;
            if (g == null)
            {
                g = Graphics.FromImage(pictOfGrapf);//Копируем оригинальный Bitmap графа
                disposeGraphics = true;
            }

            using (Pen myPen = new Pen(Color.Black, 2))
            using (SolidBrush myBrus = new SolidBrush(Color.White))
            using (Font vertexFont = new Font("Arial", 10))
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                for (int i = 0; i < Program.graph.coordinates.Count; i++)
                {
                    int rectX = Program.graph.coordinates[i].X - a / 2;
                    int rectY = Program.graph.coordinates[i].Y - a / 2;
                    Rectangle rect = new Rectangle(rectX, rectY, a, b);
                    g.DrawEllipse(myPen, rect);
                    g.FillEllipse(myBrus, rect);
                    g.DrawString((i + 1).ToString(), vertexFont, Brushes.Black, rect, stringFormat);
                    
                }
            }

            if (disposeGraphics)
                g.Dispose();
        }
        public static class GraphFileManager
        {
            // Сохранение графа в файл
            public static void SaveGraph(string filePath, Graph graph, Bitmap graphImage)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        //Сохраняем тип графа
                        writer.WriteLine($"#Тип графа: {(graph.isDirected ? "Ориентированный" : "Неориентированный")}");
                        writer.WriteLine($"#Количество вершин: {graph.coordinates.Count}");
                        writer.WriteLine();

                        // Сохраняем вершины в формате ID, X, Y
                        writer.WriteLine("# Вершины (ID, X, Y):");
                        for (int i = 0; i < graph.coordinates.Count; i++)
                        {
                            writer.WriteLine($"{i},{graph.coordinates[i].X},{graph.coordinates[i].Y}");
                        }
                        writer.WriteLine();

                        //Сохраняем рёбра (список смежности)
                        writer.WriteLine("# Рёбра (Список смежности):");
                        for (int i = 0; i < graph.adjacencyList.Count; i++)
                        {
                            if (graph.adjacencyList[i].Count > 0)
                            {
                                string edges = string.Join(",", graph.adjacencyList[i]);
                                writer.WriteLine($"{i}:{edges}");
                            }
                            else
                            {
                                writer.WriteLine($"{i}:");
                            }
                        }
                        writer.WriteLine();

                        //Сохраняем изображение
                        writer.WriteLine("# Изображение:");
                        string imagePath = Path.ChangeExtension(filePath, ".png");

                        // СОЗДАЁМ НОВЫЙ BITMAP С ФОНОМ AQUAMARINE
                        using (Bitmap newBitmap = new Bitmap(graphImage.Width, graphImage.Height))
                        {
                            using (Graphics g = Graphics.FromImage(newBitmap))
                            {
                                // Заливаем фон Aquamarine
                                g.Clear(Color.Aquamarine);
                                // Рисуем исходное изображение поверх
                                g.DrawImage(graphImage, 0, 0);
                            }
                            // Сохраняем новый bitmap
                            newBitmap.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                        }

                        writer.WriteLine($"Изображение сохранено в : {imagePath}");
                    }

                    MessageBox.Show($"Граф успешно сохранён в файл:\n{filePath}\n\nИзображение сохранено в:\n" +
                        $"{Path.ChangeExtension(filePath, ".png")}",
                        "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении графа:\n{ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Новый метод для сохранения изображения с белым фоном
            private static void SaveImageWithWhiteBackground(Bitmap sourceImage, string imagePath)
            {
                // Создаём новое изображение с белым фоном
                using (Bitmap newImage = new Bitmap(sourceImage.Width, sourceImage.Height))
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    // Заливаем белым фоном
                    g.Clear(Color.White);

                    // Рисуем исходное изображение поверх белого фона
                    g.DrawImage(sourceImage, 0, 0);

                    // Сохраняем новое изображение
                    newImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            // Загрузка графа из файла
            public static bool LoadGraph(string filePath, Graph graph, out Bitmap loadedImage)
            {
                loadedImage = null;

                try
                {
                    // Очищаем текущий граф
                    graph.adjacencyList.Clear();
                    graph.adjacencyListOrdered.Clear();
                    graph.coordinates.Clear();

                    string[] lines = File.ReadAllLines(filePath);
                    bool readingVertices = false;
                    bool readingEdges = false;

                    // Временное хранение
                    List<(int id, int x, int y)> vertices = new List<(int, int, int)>();
                    Dictionary<int, List<int>> edges = new Dictionary<int, List<int>>();
                    Dictionary<int, int> weights = new Dictionary<int, int>();

                    foreach (string line in lines)
                    {
                        string trimmedLine = line.Trim();

                        // Пропускаем пустые строки и комментарии
                        if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
                        {
                            // Проверяем секции
                            if (trimmedLine.StartsWith("# Вершины"))
                            {
                                readingVertices = true;
                                readingEdges = false;
                            }
                            else if (trimmedLine.StartsWith("# Рёбра"))
                            {
                                readingVertices = false;
                                readingEdges = true;
                            }
                            else if (trimmedLine.StartsWith("#Тип графа:"))
                            {
                                // Читаем тип графа
                                string type = trimmedLine.Split(':')[1].Trim();
                                graph.isDirected = type == "Ориентированный";
                            }
                            continue;
                        }

                        // Читаем вершины
                        if (readingVertices)
                        {
                            string[] parts = trimmedLine.Split(',');
                            if (parts.Length == 3 &&
                                int.TryParse(parts[0], out int id) &&
                                int.TryParse(parts[1], out int x) &&
                                int.TryParse(parts[2], out int y))
                            {
                                vertices.Add((id, x, y));
                            }
                        }

                        // Читаем рёбра
                        if (readingEdges)
                        {
                            string[] parts = trimmedLine.Split(':');
                            if (parts.Length == 2 && int.TryParse(parts[0], out int vertexId))
                            {
                                List<int> neighbors = new List<int>();
                                if (!string.IsNullOrEmpty(parts[1]))
                                {
                                    foreach (string neighbor in parts[1].Split(','))
                                    {
                                        if (int.TryParse(neighbor, out int n))
                                        {
                                            neighbors.Add(n);
                                        }
                                    }
                                }
                                edges[vertexId] = neighbors;
                            }
                        }


                    }

                    // Восстанавливаем граф
                    //Добавляем вершины
                    foreach (var v in vertices.OrderBy(v => v.id))
                    {
                        graph.AddVertex(v.x, v.y);
                    }

                    //Добавляем рёбра
                    foreach (var kvp in edges.OrderBy(k => k.Key))
                    {
                        foreach (int neighbor in kvp.Value)
                        {
                            graph.adjacencyList[kvp.Key].Add(neighbor);
                            graph.adjacencyListOrdered[kvp.Key].Add(neighbor);
                        }
                    }


                    //Загружаем изображение
                    string imagePath = Path.ChangeExtension(filePath, ".png");
                    if (File.Exists(imagePath))
                    {
                        loadedImage = new Bitmap(imagePath);
                    }
                    else
                    {
                        MessageBox.Show("Изображение графа не найдено. Будет создан новый холст.",
                            "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        loadedImage = new Bitmap(800, 600);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке графа:\n{ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
