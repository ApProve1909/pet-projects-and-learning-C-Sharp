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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int thisEdge = int.Parse(textBox1.Text);
                int thisWeight = int.Parse(textBox2.Text);

                
                listBox1.Items.Add($"Добавлен вес {thisWeight} ребру №{thisEdge}");
            }
            catch
            {
                MessageBox.Show("Указанное ребро не существует. \n Введите целые числа в поля");
            }

        }
    }
}
