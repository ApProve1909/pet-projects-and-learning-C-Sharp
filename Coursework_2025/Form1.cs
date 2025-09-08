using Graph;
using System.IO;
using System.Runtime.InteropServices;

namespace Graphs
{
    public partial class Form1 : Form
    {
        private int numberOfTheVertex = 0; //���������� ������
        private int numberOfTheEdge = 0; //���������� ����
        private int startingVertexIndex = -1; //��������� �������. ���������� ���������� ��� �������
        private int gridRowsCountVertexes = 0; //���������� ����� ��� ������������ ������.
        private int gridRowsCountEdges = 0; //���������� ����� ��� ������������ ������.
        private int startingX = -1; // ���������� ��� "������" �����
        private int startingY = -1; //���������� ��� "������" �����
        int a = 30, b = 40; //������ ��� �������
        private bool isActive = false;  //������ ���������� ��� "�������� ��� ��������� ������";
        private bool isDrawingEdge = false; //���������� ��� ��������� ������� ��������� ����� �� ����� ������������
        int[,] vertexes = new int[100, 3];//������ ��� ������
        int[,] edges = new int[100, 3]; // ������ ��� ����

        [DllImport("kernel32.dll")]
        internal static extern bool AllocConsole(); // ������� ��� ��������� �������

        [DllImport("kernel32.dll")]
        internal static extern bool FreeConsole(); // ������� ��� ������������ �������
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Columns.Add("Index", "�"); // ����� �������
            dataGridView1.Columns.Add("X", "X");    // ���������� X
            dataGridView1.Columns.Add("Y", "Y");    // ���������� Y

            // ��������� dataGridView2 (�����)
            dataGridView2.Columns.Add("Index", "�");  // ����� �����
            dataGridView2.Columns.Add("Start", "������");  // ��������� �������
            dataGridView2.Columns.Add("End", "�����");    // �������� �������
        }

        private void toolStripButton1_Click(object sender, EventArgs e) // ������������� ������� ���������� ��� *
        {
            isActive = !isActive;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) //��������� ��������� ������ ����� ��� ������� ���
        {
            if (isActive && pictureBox1.Enabled) //���� �������� ������� � ���� ��������� ��������
            {
                int nearestVertexIndex = FindNearestVetrex(e.X, e.Y);//���� ������ ������� � ������� ������� ������������� ���

                if (nearestVertexIndex == -1) //���� �� ���������� ������� � ����� ����� ����, �� �������� ����� �������
                {
                    if (numberOfTheVertex < vertexes.GetLength(0))
                    {
                        vertexes[numberOfTheVertex, 0] = numberOfTheVertex; //����������� ������� �������
                        vertexes[numberOfTheVertex, 1] = e.X; //����������� ���������� X �������
                        vertexes[numberOfTheVertex, 2] = e.Y; //����������� ���������� Y �������

                        startingVertexIndex = numberOfTheVertex;
                        numberOfTheVertex++; //����������� ���-�� ������ (�������)
                        startingX = e.X;
                        startingY = e.Y;
                        isDrawingEdge = true; // ������������� ����, ��� ������ �������� �����
                        pictureBox1.Invalidate(); // ��������������
                        int numberOfTheRows = gridRowsCountVertexes; //���������� ��� ��������� ������
                        int coordX = vertexes[gridRowsCountVertexes, 1]; //���������� ��� ��������� ������
                        int coordY = vertexes[gridRowsCountVertexes, 2]; //���������� ��� ��������� ������
                        //���������� ������ � ������� � ������������ �������
                        dataGridView1.Rows.Add(numberOfTheRows.ToString(), coordX.ToString(), coordY.ToString());
                        gridRowsCountVertexes++;//���������� ���-�� ����� � �������
                    }
                    else
                    {
                        MessageBox.Show("���������� ������������ ���������� ������!");
                    }
                }
                else
                {
                    startingVertexIndex = nearestVertexIndex; //�������������� ��������� ������ ��������� �������
                    startingX = vertexes[nearestVertexIndex, 1]; //
                    startingY = vertexes[nearestVertexIndex, 2];
                    isDrawingEdge = true; // ������������� ����, ��� ������ �������� �����
                    pictureBox1.Invalidate(); // ��������������, ����� ������ ��������� ���������
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // ��������� ��������� ������ ����� ����� ������� ���
        {
            //���� ������������� ������� � ������������ ����� �� �������
            if (isActive && pictureBox1.Enabled && startingVertexIndex != -1)
            {
                //���������� ����� �������, � ������� ����� ������������
                int nearestVertexIndex = FindNearestVetrex(e.X, e.Y);
                //���� ��������� ������� �� ����� -1 � �� ����� ������� ��������� �������
                if (nearestVertexIndex != -1)
                {
                    if (numberOfTheEdge < edges.GetLength(0))
                    {
                        //���� ��������� ������ ����� ������� ������� � �������, � ������� ������������ �������
                        if (nearestVertexIndex == startingVertexIndex)
                        {

                        }
                        else //�����, ����������� ������� ���� � ��������� ������ � �����
                        {
                            int row = gridRowsCountEdges; //���������� ��� ��������� ������ ���������� ������ � �������
                            int edge1 = edges[gridRowsCountEdges, 1];//���������� ��� ��������� ������ ���������� ������ � �������
                            int edge2 = edges[gridRowsCountEdges, 2];//���������� ��� ��������� ������ ���������� ������ � �������
                            dataGridView2.Rows.Add(row, edge1, edge2); //���������� ������ ������ � ����� � ���������� �������
                            gridRowsCountEdges++; //����������� ������� ���� 
                            edges[numberOfTheEdge, 0] = numberOfTheEdge;//��������� ����� �����
                            edges[numberOfTheEdge, 1] = startingVertexIndex;//��������� ����� ������ �����
                            edges[numberOfTheEdge, 2] = nearestVertexIndex;//��������� ����� ����� �����
                            numberOfTheEdge++;//����������� ���������� �����
                        }

                    }
                    else
                    {
                        MessageBox.Show("���������� ������������ ���������� �����!");
                    }
                }
                else //���� ������ ��������� ������� ����� -1 (�� ����, ������� � ������� ����� ���), �� ������ ������ ������� � ������� ���������� ���
                {
                    if (numberOfTheVertex < vertexes.GetLength(0))
                    {
                        vertexes[numberOfTheVertex, 0] = numberOfTheVertex;//���������� ������ �������
                        vertexes[numberOfTheVertex, 1] = e.X;//����������
                        vertexes[numberOfTheVertex, 2] = e.Y;//����������
                        int numberOfTheRows = gridRowsCountVertexes;//���������� ��� ��������� ������ ���������� ������ � �������
                        int coordX = vertexes[gridRowsCountVertexes, 1];//���������� ��� ��������� ������ ���������� ������ � �������
                        int coordY = vertexes[gridRowsCountVertexes, 2];//���������� ��� ��������� ������ ���������� ������ � �������
                        //��������� ������ � ������� � ���������� �������
                        dataGridView1.Rows.Add(numberOfTheRows.ToString(), coordX.ToString(), coordY.ToString());
                        if (numberOfTheEdge < edges.GetLength(0))
                        {
                            edges[numberOfTheEdge, 0] = numberOfTheEdge; //��������� ����� ������ � ����� (����� �����)
                            edges[numberOfTheEdge, 1] = startingVertexIndex; //��������� ����� ������ � ����� (������ �����)
                            edges[numberOfTheEdge, 2] = numberOfTheVertex; //��������� ����� ������ � ����� (����� �����)
                            numberOfTheEdge++; //����������� ���-�� ����
                            int row = gridRowsCountEdges; //���������� ��� ��������� ������ ���������� ������ � �������
                            int edge1 = edges[gridRowsCountEdges, 1]; //���������� ��� ��������� ������ ���������� ������ � �������
                            int edge2 = edges[gridRowsCountEdges, 2]; //���������� ��� ��������� ������ ���������� ������ � �������
                            dataGridView2.Rows.Add(row, edge1, edge2);//���������� ������ � ����� � ���������� �������
                            gridRowsCountEdges++; //����������� ������� ����� ������� ������ � �����
                        }
                        else
                        {
                            MessageBox.Show("���������� ������������ ���������� �����!");
                        }
                        numberOfTheVertex++;//����������� ����� ������
                        gridRowsCountVertexes++; // ����������� ����� ����� � ������� ������ � ��������
                    }
                    else
                    {
                        MessageBox.Show("���������� ������������ ���������� ������!");
                    }
                }

                startingVertexIndex = -1;
                startingX = -1;
                startingY = -1;
                isDrawingEdge = false; // ���������� ����
                pictureBox1.Invalidate();

            }
            pictureBox1.Invalidate();
        }
        private void pictureBox1_Move(object sender, EventArgs e)
        {
            if (isActive && isDrawingEdge && pictureBox1.Enabled) // ��������� ���� ��������� �����
            {
                pictureBox1.Invalidate(); // ��������������, ����� �������� "������" �����
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (isActive && pictureBox1.Enabled)
            {
                //������������� ������� �������� ��� ��������������� ������������ ��������, ��� ������������� ������ Dispoce
                using (Pen myPen = new Pen(Color.Black, 2))
                using (SolidBrush myBrus = new SolidBrush(Color.White))
                using (Font vertexFont = new Font("Arial", 12))
                using (Font edgeFont = new Font("Arial", 15))
                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = StringAlignment.Center; //�������� ������, ���������� �� ��������� ������ 
                    stringFormat.LineAlignment = StringAlignment.Center;
                    // ������ �����
                    for (int i = 0; i < numberOfTheEdge; i++)
                    {
                        int vertex1Index = edges[i, 1];
                        int vertex2Index = edges[i, 2];

                        // ���������, ��� ������� ������ ��������� � �������� ������� vertexes
                        if (vertex1Index >= 0 && vertex1Index < numberOfTheVertex &&
                            vertex2Index >= 0 && vertex2Index < numberOfTheVertex)
                        {
                            int x1 = vertexes[vertex1Index, 1];
                            int y1 = vertexes[vertex1Index, 2];
                            int x2 = vertexes[vertex2Index, 1];
                            int y2 = vertexes[vertex2Index, 2];
                            e.Graphics.DrawLine(myPen, x1, y1, x2, y2);

                            // ������ ����� �����
                            float edgeX = (x1 + x2) / 2f;
                            float edgeY = (y1 + y2) / 2f;
                            e.Graphics.DrawString((i + 1).ToString(), edgeFont, Brushes.Blue, edgeX, edgeY, stringFormat);
                        }
                    }
                    // ������ �������
                    for (int i = 0; i < numberOfTheVertex; i++)
                    {
                        int rectX = vertexes[i, 1] - a / 2;
                        int rectY = vertexes[i, 2] - b / 2;
                        Rectangle rect = new Rectangle(rectX, rectY, a, b);
                        e.Graphics.DrawEllipse(myPen, rect);
                        e.Graphics.FillEllipse(myBrus, rect);
                        // ������ ����� �������
                        e.Graphics.DrawString((i + 1).ToString(), vertexFont, Brushes.Black, rect, stringFormat);
                    }
                    // ������ "������" ����� (���� �������� �����)
                    if (isDrawingEdge && startingX != -1 && startingY != -1) // ��������� ���� � ����������
                    {
                        e.Graphics.DrawLine(myPen, startingX, startingY, pictureBox1.PointToClient(Control.MousePosition).X, pictureBox1.PointToClient(Control.MousePosition).Y);
                    }
                }
                pictureBox1.Invalidate();//�������������� pictureBox
            }
        }
        private void button1_Click(object sender, EventArgs e) // ���������� ���������� �����
        {
            StreamWriter strGraphs = new StreamWriter("Graphs.txt"); //�������� ������� ������ ������ � ����
            strGraphs.WriteLine("vertexes = " + numberOfTheVertex);
            strGraphs.WriteLine("edges = " + numberOfTheEdge);
            strGraphs.WriteLine("������ ������");
            strGraphs.WriteLine("N\tX1\tY1");
            for (int i = 0; i < vertexes.GetLength(0); i++)//���������� ������ ������ �����
            {
                strGraphs.Write("#" + vertexes[i, 0] + "\t");//���������� ����� �������
                strGraphs.Write("?" + vertexes[i, 1] + "\t");//���������� ���������� X
                strGraphs.Write("??" + vertexes[i, 2] + "\t");//���������� ���������� Y
                strGraphs.WriteLine();
            }


            strGraphs.WriteLine("������ Ш���");
            strGraphs.WriteLine("�\t������ �����\t����� �����");
            for (int i = 0; i < edges.GetLength(0); i++) //���������� ������ ����
            {
                strGraphs.Write("N" + edges[i, 0] + "\t  "); //���������� ����� �����
                strGraphs.Write("!" + edges[i, 1] + "\t\t  ");//���������� ������ �����
                strGraphs.Write("!!" + edges[i, 2] + "\t  ");//���������� ����� �����
                strGraphs.WriteLine();
            }

            strGraphs.Close(); //��������� ����� ������
        }
        private void button2_Click(object sender, EventArgs e) //������ �������� ����� �� ���������� �����
        {
            pictureBox1.Enabled = true;
            checkBox1.Checked = true;
            AllocConsole(); // ��������� �������
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // ������ ������
            openFileDialog.Title = "�������� ���� � ������� �����";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // ��������� ����������

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadGraphDataFromFile(filePath); // �������� ����� ��� �������� ������
            }
            for (int i = 0; i < numberOfTheVertex; i++) //������� ������ � �������� � ������������ �������
            {
                int vert1 = vertexes[i, 1];
                int vert2 = vertexes[i, 2];
                dataGridView1.Rows.Add(i.ToString(), vert1.ToString(), vert2.ToString());
                gridRowsCountVertexes++;
            }
            for (int i = 0; i < numberOfTheEdge; i++) //������� ������ � ����� � ���������� �������
            {
                int edge1 = edges[i, 1];
                int edge2 = edges[i, 2];
                dataGridView2.Rows.Add(i.ToString(), edge1.ToString(), edge2.ToString());
                gridRowsCountVertexes++;
            }
        }

        //������ ������� ��������, ����������, ������ � ���� ���������
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
            gridRowsCountVertexes = 0; //���������� ����� ��� ������������ ������.
            gridRowsCountEdges = 0; //���������� ����� ��� ������������ ������.
            startingX = -1; // ���������� ��� "������" �����
            startingY = -1; //���������� ��� "������" �����
            isActive = true;  //������ ���������� ��� "�������� ��� ��������� ������";
            isDrawingEdge = false; //���������� ��� ��������� ������� ��������� ����� �� ����� ������������
            pictureBox1.Invalidate();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView1.Columns.Add("Index", "�"); // ����� �������
            dataGridView1.Columns.Add("X", "X");    // ���������� X
            dataGridView1.Columns.Add("Y", "Y");    // ���������� Y

            // ��������� dataGridView2 (�����)
            dataGridView2.Columns.Add("Index", "�");  // ����� �����
            dataGridView2.Columns.Add("Start", "������");  // ��������� �������
            dataGridView2.Columns.Add("End", "�����");    // �������� �������
        }
        private void LoadGraphDataFromFile(string filePath) //����� �������� ������ �� ���������� ����� ������
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath); //��������� ��� �������� �����

                int vertexCount = 0; //���������� ������
                int edgeCount = 0; //���������� �����

                // ��������� ���������� ������ � �����
                foreach (string line in lines)
                {
                    if (line.StartsWith("vertexes ="))
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out vertexCount))
                        {
                            Console.WriteLine($"���������� ������: {vertexCount}");
                        }
                        else
                        {
                            MessageBox.Show("������: �������� ������ ���������� ������.");
                        }
                    }
                    else if (line.StartsWith("edges ="))
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out edgeCount))
                        {
                            Console.WriteLine($"���������� �����: {edgeCount}");
                        }
                        else
                        {
                            MessageBox.Show("������: �������� ������ ���������� �����.");
                        }
                    }
                }

                // ������� ������������ ������
                numberOfTheVertex = 0;
                numberOfTheEdge = 0;

                // ���������� ��������� ������� ��� ������ ������
                int vertexDataStartIndex = -1;
                int edgeDataStartIndex = -1;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Trim() == "������ ������")
                    {
                        vertexDataStartIndex = i + 2; // �������� ������ � 3-� ������ ����� "������ ������"
                    }
                    else if (lines[i].Trim() == "������ Ш���")
                    {
                        edgeDataStartIndex = i + 2; // �������� ������ � 3-� ������ ����� "������ Ш���"
                    }
                }

                // ���������, ������� �� �������
                if (vertexDataStartIndex == -1 || edgeDataStartIndex == -1)
                {
                    MessageBox.Show("������: �� ������� ������� '������ ������' ��� '������ Ш���'.");
                    return;
                }

                // ��������� �������
                for (int i = vertexDataStartIndex; i < vertexDataStartIndex + vertexCount; i++)
                {
                    string cleanLine = lines[i].Trim(); // ������� �� �������� � ������ � �����
                    string[] vertexData = cleanLine.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries); // ��������� �� ���������
                                                                                                                       // ������ ������� 3 ��������: N, X1, Y1

                    if (vertexData.Length == 3)
                    {
                        // ����������� X � Y, ������ �������
                        if (int.TryParse(vertexData[1].Replace("?", ""), out int x) && int.TryParse(vertexData[2].Replace("??", ""), out int y))
                        {
                            vertexes[numberOfTheVertex, 0] = numberOfTheVertex; // ������ �������
                            vertexes[numberOfTheVertex, 1] = x; // X ����������
                            vertexes[numberOfTheVertex, 2] = y; // Y ����������
                            numberOfTheVertex++;
                            Console.WriteLine($"��������� �������: X = {x}, Y = {y}");
                        }
                        else
                        {
                            MessageBox.Show($"������: �������� ������ ��������� ������� � ������ {i + 1}.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"������: �������� ���������� ������ ������� � ������ {i + 1}. ��������� 3, �������� {vertexData.Length}.");
                    }
                }

                // ��������� �����
                for (int i = edgeDataStartIndex; i < edgeDataStartIndex + edgeCount; i++)
                {
                    string cleanLine = lines[i].Trim(); // ������� �� �������� � ������ � �����
                    string[] edgeData = cleanLine.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries); // ��������� �� ���������

                    if (edgeData.Length == 3) // ������� 3 ��������: N, ������, �����
                    {
                        if (int.TryParse(edgeData[1].Replace("!", ""), out int start) && int.TryParse(edgeData[2].Replace("!!", ""), out int end))
                        {
                            edges[numberOfTheEdge, 0] = numberOfTheEdge; // ������ �����
                            edges[numberOfTheEdge, 1] = start; // ������ ������ �������
                            edges[numberOfTheEdge, 2] = end; // ������ ������ �������
                            numberOfTheEdge++;
                            Console.WriteLine($"��������� �����: ������ = {start}, ����� = {end}");
                        }
                        else
                        {
                            MessageBox.Show($"������: �������� ������ �������� ������ � ������ {i + 1}.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"������: �������� ���������� ������ ����� � ������ {i + 1}. ��������� 3, �������� {edgeData.Length}.");

                    }
                }
                pictureBox1.Invalidate(); // �������������� PictureBox
            }

            catch (Exception ex)
            {
                MessageBox.Show("������ ��� �������� ������: " + ex.Message);
            }

        }
        private int FindNearestVetrex(int mouseClickX, int mouseClickY) //����� ��� ������ ������� ������� � ����� ����� ������������
        {
            for (int i = 0; i < numberOfTheVertex; i++)
            {
                //������������� ��������� ������� ��� ��������, ������ �� ����� ����� ����������� � ������� ����������� ������� (�������)
                double ellipseValue = (Math.Pow(mouseClickX - vertexes[i, 1], 2) / Math.Pow(a, 2)) +
                              (Math.Pow(mouseClickY - vertexes[i, 2], 2) / Math.Pow(b, 2));
                if (ellipseValue <= 1) //���� ������, �� ���������� ������ �������
                {
                    return i;
                }

            }
            return -1; //���� �� ������, �� ���������� -1 - �����, ����������� �� ��, ��� ��������� ������� � ����� ���
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//��������� �������� ��� ��������� ������
        {
            if (checkBox1.Checked == true)
            {
                isActive = true;
                checkBox2.Checked = false;
                pictureBox1.Enabled = true;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)//���������� �������� ��� ��������� ������
        {
            if (checkBox2.Checked == true)
            {
                isActive = false;
                checkBox1.Checked = false;
                pictureBox1.Enabled = false;
            }
        }


    }
}