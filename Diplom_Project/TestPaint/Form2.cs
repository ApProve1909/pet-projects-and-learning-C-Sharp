using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPaint
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Program.graph.adjacencyList.Count; i++)
            {
                {
                    dataGridView1.Rows.Add((i + 1).ToString(), Program.graph.coordinates[i].X.ToString(), Program.graph.coordinates[i].Y.ToString());
                }

                Program.graph.DisplayAdjacencyList(listBox1);
            }
        }
    }
}
