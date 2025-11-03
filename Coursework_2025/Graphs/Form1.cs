using Graph;
using System.IO;
using System.Runtime.InteropServices;

namespace Graphs
{
    public partial class Form1 : Form
    {
        private int numberOfTheVertex = 0; //Количество вершин
        private int numberOfTheEdge = 0; //Количество рёбер
        private int startingVertexIndex = -1; //Стартовая вершина. Переменная необходима для методов
        private int gridRowsCountVertexes = 0; //Количество строк для динамических таблиц.
        private int gridRowsCountEdges = 0; //Количество строк для динамических таблиц.
        private int startingX = -1; // Координата для "гибкой" линии
        private int startingY = -1; //Координата для "гибкой" линии
        int a = 30, b = 40; //Данные для Эллипса
        private bool isActive = false;  //Статус активности для "Кисточки для рисования графов";
        private bool isDrawingEdge = false; //Пеерменная ждя изменения статуса рисования линии во время движениямыши
        int[,] vertexes = new int[100, 3];//Массив для вершин
        int[,] edges = new int[100, 3]; // Массив для рёбер
        [DllImport("kernel32.dll")]
        internal static extern bool AllocConsole(); // Функция для выделения консоли

        [DllImport("kernel32.dll")]
        internal static extern bool FreeConsole(); // Функция для освобождения консоли
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e) // Переключатель статуса активности для *
        {
            isActive = !isActive;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) //Получение координат первой точки при нажатии ЛКМ
        {
            if (isActive && pictureBox1.Enabled)
            {
                int nearestVertexIndex = FindNearestVetrex(e.X, e.Y);

                if (nearestVertexIndex == -1) //Если не существует вершины в точке клика мыши, то строится новая вершина
                {
                    if (numberOfTheVertex < vertexes.GetLength(0))
                    {
                        vertexes[numberOfTheVertex, 0] = numberOfTheVertex; //Запоминание индекса вершины
                        vertexes[numberOfTheVertex, 1] = e.X; //Запоминание координаты X вершины
                        vertexes[numberOfTheVertex, 2] = e.Y; //Запоминание координаты Y вершины

                        startingVertexIndex = numberOfTheVertex;
                        numberOfTheVertex++; //Увеличиваем кол-во вершин (счётчик)
                        startingX = e.X;
                        startingY = e.Y;
                        isDrawingEdge = true; // Устанавливаем флаг, что начали рисовать ребро
                        pictureBox1.Invalidate(); // Перерисовываем
                        int numberOfTheRows = gridRowsCountVertexes; //Переменная для упрощения работы
                        int coordX = vertexes[gridRowsCountVertexes, 1]; //Переменная для упрощения работы
                        int coordY = vertexes[gridRowsCountVertexes, 2]; //Переменная для упрощения работы
                        //Добавление данных о ВЕРШИНЕ в динамическую таблицу
                        dataGridView1.Rows.Add(numberOfTheRows.ToString(), coordX.ToString(), coordY.ToString());
                        gridRowsCountVertexes++;//Увеличение кол-во строк в таблице
                    }
                    else
                    {
                        MessageBox.Show("Достигнуто максимальное количество вершин!");
                    }
                }
                else
                {
                    startingVertexIndex = nearestVertexIndex;
                    startingX = vertexes[nearestVertexIndex, 1];
                    startingY = vertexes[nearestVertexIndex, 2];
                    isDrawingEdge = true; // Устанавливаем флаг, что начали рисовать ребро
                    pictureBox1.Invalidate(); // Перерисовываем, чтобы убрать возможные артефакты
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // Получение координат второй точки после отжатия ЛКМ
        {
            //Если переключатель активен и пользователь нажал на вершину
            if (isActive && pictureBox1.Enabled && startingVertexIndex != -1)
            {
                //Определяем номер вершины, в которую нажал пользователь
                int nearestVertexIndex = FindNearestVetrex(e.X, e.Y);
                //Если найденная вершина не равна -1 и не равна индексу стартовой вершины
                if (nearestVertexIndex != -1)
                {
                    if (numberOfTheEdge < edges.GetLength(0))
                    {
                        if (nearestVertexIndex == startingVertexIndex)
                        {
                            int row = gridRowsCountEdges;
                            int edge1 = edges[gridRowsCountEdges, 1];
                            int edge2 = edges[gridRowsCountEdges, 2];
                            dataGridView2.Rows.Add(row, edge1, edge2);
                            gridRowsCountEdges++;
                        }
                        else
                        {
                            edges[numberOfTheEdge, 0] = numberOfTheEdge;//Сохраняем номер ребра
                            edges[numberOfTheEdge, 1] = startingVertexIndex;//Сохраняем номер начала ребра
                            edges[numberOfTheEdge, 2] = nearestVertexIndex;//Сохраняем номер конца ребра
                            numberOfTheEdge++;//Увеличиваем количество ребер
                        }

                    }
                    else
                    {
                        MessageBox.Show("Достигнуто максимальное количество ребер!");
                    }
                }
                else
                {
                    if (numberOfTheVertex < vertexes.GetLength(0))
                    {
                        vertexes[numberOfTheVertex, 0] = numberOfTheVertex;
                        vertexes[numberOfTheVertex, 1] = e.X;
                        vertexes[numberOfTheVertex, 2] = e.Y;
                        int numberOfTheRows = gridRowsCountVertexes;
                        int coordX = vertexes[gridRowsCountVertexes, 1];
                        int coordY = vertexes[gridRowsCountVertexes, 2];
                        dataGridView1.Rows.Add(numberOfTheRows.ToString(), coordX.ToString(), coordY.ToString());
                        if (numberOfTheEdge < edges.GetLength(0))
                        {
                            edges[numberOfTheEdge, 0] = numberOfTheEdge;
                            edges[numberOfTheEdge, 1] = startingVertexIndex;
                            edges[numberOfTheEdge, 2] = numberOfTheVertex;
                            numberOfTheEdge++;
                            int row = gridRowsCountEdges;
                            int edge1 = edges[gridRowsCountEdges, 1];
                            int edge2 = edges[gridRowsCountEdges, 2];
                            dataGridView2.Rows.Add(row, edge1, edge2);
                            gridRowsCountEdges++;
                        }
                        else
                        {
                            MessageBox.Show("Достигнуто максимальное количество ребер!");
                        }
                        numberOfTheVertex++;
                        gridRowsCountVertexes++;
                    }
                    else
                    {
                        MessageBox.Show("Достигнуто максимальное количество вершин!");
                    }
                }

                startingVertexIndex = -1;
                startingX = -1;
                startingY = -1;
                isDrawingEdge = false; // Сбрасываем флаг
                pictureBox1.Invalidate();

            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isActive && pictureBox1.Enabled)
            {
                using (Pen myPen = new Pen(Color.Black, 2))
                using (SolidBrush myBrus = new SolidBrush(Color.White))
                using (Font vertexFont = new Font("Arial", 12))
                using (Font edgeFont = new Font("Arial", 15))
                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    // Рисуем ребра
                    for (int i = 0; i < numberOfTheEdge; i++)
                    {
                        int vertex1Index = edges[i, 1];
                        int vertex2Index = edges[i, 2];

                        // Проверяем, что индексы вершин находятся в пределах массива vertexes
                        if (vertex1Index >= 0 && vertex1Index < numberOfTheVertex &&
                            vertex2Index >= 0 && vertex2Index < numberOfTheVertex)
                        {
                            int x1 = vertexes[vertex1Index, 1];
                            int y1 = vertexes[vertex1Index, 2];
                            int x2 = vertexes[vertex2Index, 1];
                            int y2 = vertexes[vertex2Index, 2];
                            e.Graphics.DrawLine(myPen, x1, y1, x2, y2);

                            // Рисуем номер ребра
                            float edgeX = (x1 + x2) / 2f;
                            float edgeY = (y1 + y2) / 2f;
                            e.Graphics.DrawString((i + 1).ToString(), edgeFont, Brushes.Blue, edgeX, edgeY, stringFormat);
                        }
                    }
                    // Рисуем вершины
                    for (int i = 0; i < numberOfTheVertex; i++)
                    {
                        int rectX = vertexes[i, 1] - a / 2;
                        int rectY = vertexes[i, 2] - b / 2;
                        Rectangle rect = new Rectangle(rectX, rectY, a, b);
                        e.Graphics.DrawEllipse(myPen, rect);
                        e.Graphics.FillEllipse(myBrus, rect);
                        // Рисуем номер вершины
                        e.Graphics.DrawString((i + 1).ToString(), vertexFont, Brushes.Black, rect, stringFormat);
                    }
                    // Рисуем "гибкую" линию (если рисуется ребро)
                    if (isDrawingEdge && startingX != -1 && startingY != -1) // Проверяем флаг и координаты
                    {
                        e.Graphics.DrawLine(myPen, startingX, startingY, pictureBox1.PointToClient(Control.MousePosition).X, pictureBox1.PointToClient(Control.MousePosition).Y);
                    }
                }
                pictureBox1.Invalidate();
            }
        }
        private void button1_Click(object sender, EventArgs e) // Сохранение созданного графа
        {
            StreamWriter strGraphs = new StreamWriter("Graphs.txt");
            strGraphs.WriteLine("vertexes = " + numberOfTheVertex);
            strGraphs.WriteLine("edges = " + numberOfTheEdge);
            strGraphs.WriteLine("МАССИВ ВЕРШИН");
            strGraphs.WriteLine("N\tX1\tY1\tX2\tY2");
            for (int i = 0; i < vertexes.GetLength(0); i++)
            {
                strGraphs.Write("#" + vertexes[i, 0] + "\t");
                strGraphs.Write("?" + vertexes[i, 1] + "\t");
                strGraphs.Write("??" + vertexes[i, 2] + "\t");
                strGraphs.WriteLine();
            }


            strGraphs.WriteLine("МАССИВ РЁБЕР");
            strGraphs.WriteLine("№\tНачало ребра\tКонец ребра");
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                strGraphs.Write("N" + edges[i, 0] + "\t  ");
                strGraphs.Write("!" + edges[i, 1] + "\t\t  ");
                strGraphs.Write("!!" + edges[i, 2] + "\t  ");
                strGraphs.WriteLine();
            }

            strGraphs.Close();
        }
        public static void SaveGraphToFile(string fileName, List<(int x1, int y1, int x2, int y2)> lines)//Метод создания файла, в который сохраняются координаты вершин и номера вершин графов
        {
            using (StreamWriter writeGraph = new StreamWriter(fileName))
            {
                writeGraph.WriteLine("X1\tX2\tY1\tY2");
                foreach (var line in lines)
                {
                    writeGraph.WriteLine($"{line.x1}\t{line.y1}\t{line.x2}\t{line.y2}");
                }
            }
        }

        private void pictureBox1_Move(object sender, EventArgs e)
        {
            if (isActive && isDrawingEdge && pictureBox1.Enabled) // Проверяем флаг рисования ребра
            {
                pictureBox1.Invalidate(); // Перерисовываем, чтобы обновить "гибкую" линию
            }
        }
        private void LoadGraphDataFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                int vertexCount = 0;
                int edgeCount = 0;

                // Считываем количество вершин и ребер
                foreach (string line in lines)
                {
                    if (line.StartsWith("vertexes ="))
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out vertexCount))
                        {
                            Console.WriteLine($"Количество вершин: {vertexCount}");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: Неверный формат количества вершин.");
                        }
                    }
                    else if (line.StartsWith("edges ="))
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out edgeCount))
                        {
                            Console.WriteLine($"Количество ребер: {edgeCount}");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка: Неверный формат количества ребер.");
                        }
                    }
                }

                // Очищаем существующие данные
                numberOfTheVertex = 0;
                numberOfTheEdge = 0;

                // Определяем начальные индексы для чтения данных
                int vertexDataStartIndex = -1;
                int edgeDataStartIndex = -1;
                int adjacencyMatrixStartIndex = -1; // Индекс начала матрицы смежности
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Trim() == "МАССИВ ВЕРШИН")
                    {
                        vertexDataStartIndex = i + 2; // Начинаем читать с 3-й строки после "МАССИВ ВЕРШИН"
                    }
                    else if (lines[i].Trim() == "МАССИВ РЁБЕР")
                    {
                        edgeDataStartIndex = i + 2; // Начинаем читать с 3-й строки после "МАССИВ РЁБЕР"
                    }
                }

                // Проверяем, найдены ли индексы
                if (vertexDataStartIndex == -1 || edgeDataStartIndex == -1)
                {
                    MessageBox.Show("Ошибка: Не найдены маркеры 'МАССИВ ВЕРШИН' или 'МАССИВ РЁБЕР'.");
                    return;
                }

                // Загружаем вершины
                for (int i = vertexDataStartIndex; i < vertexDataStartIndex + vertexCount; i++)
                {
                    string cleanLine = lines[i].Trim(); // Очищаем от пробелов в начале и конце
                    string[] vertexData = cleanLine.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries); // Разделяем по табуляции
                                                                                                                       // Теперь ожидаем 3 элемента: N, X1, Y1

                    if (vertexData.Length == 3)
                    {
                        // Преобразуем X и Y, удаляя маркеры
                        if (int.TryParse(vertexData[1].Replace("?", ""), out int x) && int.TryParse(vertexData[2].Replace("??", ""), out int y))
                        {
                            vertexes[numberOfTheVertex, 0] = numberOfTheVertex; // Индекс вершины
                            vertexes[numberOfTheVertex, 1] = x; // X координата
                            vertexes[numberOfTheVertex, 2] = y; // Y координата
                            numberOfTheVertex++;
                            Console.WriteLine($"Добавлена вершина: X = {x}, Y = {y}");
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка: Неверный формат координат вершины в строке {i + 1}.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка: Неверное количество данных вершины в строке {i + 1}. Ожидалось 3, получено {vertexData.Length}.");
                    }
                }

                // Загружаем ребра
                for (int i = edgeDataStartIndex; i < edgeDataStartIndex + edgeCount; i++)
                {
                    string cleanLine = lines[i].Trim(); // Очищаем от пробелов в начале и конце
                    string[] edgeData = cleanLine.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries); // Разделяем по табуляции

                    if (edgeData.Length == 3) // Ожидаем 3 элемента: N, начало, конец
                    {
                        if (int.TryParse(edgeData[1].Replace("!", ""), out int start) && int.TryParse(edgeData[2].Replace("!!", ""), out int end))
                        {
                            edges[numberOfTheEdge, 0] = numberOfTheEdge; // Индекс ребра
                            edges[numberOfTheEdge, 1] = start; // Индекс первой вершины
                            edges[numberOfTheEdge, 2] = end; // Индекс второй вершины
                            numberOfTheEdge++;
                            Console.WriteLine($"Добавлено ребро: начало = {start}, конец = {end}");
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка: Неверный формат индексов вершин в строке {i + 1}.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка: Неверное количество данных ребра в строке {i + 1}. Ожидалось 3, получено {edgeData.Length}.");

                    }
                }


                pictureBox1.Invalidate(); // Перерисовываем PictureBox
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }

        }
        private int FindNearestVetrex(int mouseClickX, int mouseClickY)
        {
            int x = mouseClickX, y = mouseClickY;

            for (int i = 0; i < numberOfTheVertex; i++)
            {
                double ellipseValue = (Math.Pow(mouseClickX - vertexes[i, 1], 2) / Math.Pow(a, 2)) +
                              (Math.Pow(mouseClickY - vertexes[i, 2], 2) / Math.Pow(b, 2));
                if (ellipseValue <= 1)
                {
                    return i;
                }

            }
            return -1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                isActive = true;
                checkBox2.Checked = false;
                pictureBox1.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                isActive = false;
                checkBox1.Checked = false;
                pictureBox1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = true;
            checkBox1.Checked = true;
            AllocConsole(); // Открываем консоль
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр файлов
            openFileDialog.Title = "Выберите файл с данными графа";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Начальная директория

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadGraphDataFromFile(filePath); // Вызываем метод для загрузки данных
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = false;
            pictureBox1.Enabled = true;
            for (int i = 0; i < numberOfTheEdge; i++)
            {
                vertexes[i, 0] = 0;
                vertexes[i, 1] = 0;
                vertexes[i, 2] = 0;
                edges[i, 0] = 0;
                edges[i, 1] = 0;
                edges[i, 2] = 0;
            }
            numberOfTheEdge = 0;
            numberOfTheVertex = 0;
            startingVertexIndex = -1;
            gridRowsCountVertexes = 0; //Количество строк для динамических таблиц.
            gridRowsCountEdges = 0; //Количество строк для динамических таблиц.
            startingX = -1; // Координата для "гибкой" линии
            startingY = -1; //Координата для "гибкой" линии
            isActive = true;  //Статус активности для "Кисточки для рисования графов";
            isDrawingEdge = false; //Пеерменная ждя изменения статуса рисования линии во время движениямыши
            pictureBox1.Invalidate();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
        }
    }
}