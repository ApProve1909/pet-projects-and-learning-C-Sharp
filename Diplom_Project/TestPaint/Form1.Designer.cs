namespace TestPaint
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операцииСГрафамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалениеВершиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рисованиеГрафаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьДанныеГрафаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рисованиеОриентированногоГрафаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.рисованиеНеориентированногоГрафаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очисткаДанныхИГрафаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.алгоритмыОбработкиГрафовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обходВШиринуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обходВГлубинуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Aquamarine;
            this.pictureBox1.Location = new System.Drawing.Point(12, 58);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(979, 594);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(417, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Область рисования графа";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(998, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(997, 580);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(997, 509);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Статус: Рисование";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файоToolStripMenuItem,
            this.операцииСГрафамиToolStripMenuItem,
            this.алгоритмыОбработкиГрафовToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1160, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файоToolStripMenuItem
            // 
            this.файоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьГрафToolStripMenuItem,
            this.загрузитьГрафToolStripMenuItem});
            this.файоToolStripMenuItem.Name = "файоToolStripMenuItem";
            this.файоToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файоToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьГрафToolStripMenuItem
            // 
            this.сохранитьГрафToolStripMenuItem.Name = "сохранитьГрафToolStripMenuItem";
            this.сохранитьГрафToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.сохранитьГрафToolStripMenuItem.Text = "Сохранить граф";
            this.сохранитьГрафToolStripMenuItem.Click += new System.EventHandler(this.сохранитьГрафToolStripMenuItem_Click);
            // 
            // загрузитьГрафToolStripMenuItem
            // 
            this.загрузитьГрафToolStripMenuItem.Name = "загрузитьГрафToolStripMenuItem";
            this.загрузитьГрафToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.загрузитьГрафToolStripMenuItem.Text = "Загрузить граф";
            this.загрузитьГрафToolStripMenuItem.Click += new System.EventHandler(this.загрузитьГрафToolStripMenuItem_Click);
            // 
            // операцииСГрафамиToolStripMenuItem
            // 
            this.операцииСГрафамиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалениеВершиныToolStripMenuItem,
            this.рисованиеГрафаToolStripMenuItem,
            this.показатьДанныеГрафаToolStripMenuItem,
            this.рисованиеОриентированногоГрафаToolStripMenuItem,
            this.рисованиеНеориентированногоГрафаToolStripMenuItem,
            this.очисткаДанныхИГрафаToolStripMenuItem});
            this.операцииСГрафамиToolStripMenuItem.Name = "операцииСГрафамиToolStripMenuItem";
            this.операцииСГрафамиToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.операцииСГрафамиToolStripMenuItem.Text = "Операции с графами";
            // 
            // удалениеВершиныToolStripMenuItem
            // 
            this.удалениеВершиныToolStripMenuItem.Name = "удалениеВершиныToolStripMenuItem";
            this.удалениеВершиныToolStripMenuItem.Size = new System.Drawing.Size(368, 26);
            this.удалениеВершиныToolStripMenuItem.Text = "Удаление вершины";
            this.удалениеВершиныToolStripMenuItem.Click += new System.EventHandler(this.удалениеВершиныToolStripMenuItem_Click);
            // 
            // рисованиеГрафаToolStripMenuItem
            // 
            this.рисованиеГрафаToolStripMenuItem.Name = "рисованиеГрафаToolStripMenuItem";
            this.рисованиеГрафаToolStripMenuItem.Size = new System.Drawing.Size(368, 26);
            this.рисованиеГрафаToolStripMenuItem.Text = "Рисование графа";
            this.рисованиеГрафаToolStripMenuItem.Click += new System.EventHandler(this.рисованиеГрафаToolStripMenuItem_Click);
            // 
            // показатьДанныеГрафаToolStripMenuItem
            // 
            this.показатьДанныеГрафаToolStripMenuItem.Name = "показатьДанныеГрафаToolStripMenuItem";
            this.показатьДанныеГрафаToolStripMenuItem.Size = new System.Drawing.Size(368, 26);
            this.показатьДанныеГрафаToolStripMenuItem.Text = "Показать данные графа";
            this.показатьДанныеГрафаToolStripMenuItem.Click += new System.EventHandler(this.показатьДанныеГрафаToolStripMenuItem_Click);
            // 
            // рисованиеОриентированногоГрафаToolStripMenuItem
            // 
            this.рисованиеОриентированногоГрафаToolStripMenuItem.Name = "рисованиеОриентированногоГрафаToolStripMenuItem";
            this.рисованиеОриентированногоГрафаToolStripMenuItem.Size = new System.Drawing.Size(368, 26);
            this.рисованиеОриентированногоГрафаToolStripMenuItem.Text = "Рисование ориентированного графа";
            this.рисованиеОриентированногоГрафаToolStripMenuItem.Click += new System.EventHandler(this.рисованиеОриентированногоГрафаToolStripMenuItem_Click);
            // 
            // рисованиеНеориентированногоГрафаToolStripMenuItem
            // 
            this.рисованиеНеориентированногоГрафаToolStripMenuItem.Name = "рисованиеНеориентированногоГрафаToolStripMenuItem";
            this.рисованиеНеориентированногоГрафаToolStripMenuItem.Size = new System.Drawing.Size(368, 26);
            this.рисованиеНеориентированногоГрафаToolStripMenuItem.Text = "Рисование неориентированного графа";
            this.рисованиеНеориентированногоГрафаToolStripMenuItem.Click += new System.EventHandler(this.рисованиеНеориентированногоГрафаToolStripMenuItem_Click);
            // 
            // очисткаДанныхИГрафаToolStripMenuItem
            // 
            this.очисткаДанныхИГрафаToolStripMenuItem.Name = "очисткаДанныхИГрафаToolStripMenuItem";
            this.очисткаДанныхИГрафаToolStripMenuItem.Size = new System.Drawing.Size(368, 26);
            this.очисткаДанныхИГрафаToolStripMenuItem.Text = "Очистка данных и графа";
            this.очисткаДанныхИГрафаToolStripMenuItem.Click += new System.EventHandler(this.очисткаДанныхИГрафаToolStripMenuItem_Click);
            // 
            // алгоритмыОбработкиГрафовToolStripMenuItem
            // 
            this.алгоритмыОбработкиГрафовToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обходВШиринуToolStripMenuItem,
            this.обходВГлубинуToolStripMenuItem});
            this.алгоритмыОбработкиГрафовToolStripMenuItem.Name = "алгоритмыОбработкиГрафовToolStripMenuItem";
            this.алгоритмыОбработкиГрафовToolStripMenuItem.Size = new System.Drawing.Size(235, 24);
            this.алгоритмыОбработкиГрафовToolStripMenuItem.Text = "Алгоритмы обработки графов";
            // 
            // обходВШиринуToolStripMenuItem
            // 
            this.обходВШиринуToolStripMenuItem.Name = "обходВШиринуToolStripMenuItem";
            this.обходВШиринуToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.обходВШиринуToolStripMenuItem.Text = "Обход в ширину";
            this.обходВШиринуToolStripMenuItem.Click += new System.EventHandler(this.обходВШиринуToolStripMenuItem_Click);
            // 
            // обходВГлубинуToolStripMenuItem
            // 
            this.обходВГлубинуToolStripMenuItem.Name = "обходВГлубинуToolStripMenuItem";
            this.обходВГлубинуToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.обходВГлубинуToolStripMenuItem.Text = "Обход в глубину";
            this.обходВГлубинуToolStripMenuItem.Click += new System.EventHandler(this.обходВГлубинуToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 681);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Редактор графов";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem операцииСГрафамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem алгоритмыОбработкиГрафовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обходВШиринуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обходВГлубинуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалениеВершиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рисованиеГрафаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьДанныеГрафаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рисованиеОриентированногоГрафаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem рисованиеНеориентированногоГрафаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьГрафToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьГрафToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очисткаДанныхИГрафаToolStripMenuItem;
    }
}

