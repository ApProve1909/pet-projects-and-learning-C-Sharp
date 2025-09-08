using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Kurs_project_4_sem
{

    public partial class Form1 : Form
    {
        int N; // Количество элементов в массиве, введенные в соответствующем поле программы
        int a, b, points = 10; // Левая и правая границы генерации случайных чисел, а также количество 
                          //промежуточных значений на графике
        double[] array; //Исходный массив
        double[] firstArray; //Массив для сортировки пузырьком
        double[] secondArray;//Массив для сортировки выбором
        double[] thirdArray;//Массив для сортировки вставками
        double[] fourthArray;//Массив для сортировки слиянием
        double[] fifthArray;//Массив для быстрой сортировки
        private DateTime startTime;
        public static int percentSort;

        public Form1()
        {
            InitializeComponent();
            startTime = DateTime.Now;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Вычисляем прошедшее время
            TimeSpan elapsedTime = DateTime.Now - startTime;
            // Обновляем текст метки
            label5.Text = $"{elapsedTime.Minutes}:{elapsedTime.Seconds}";
        }
        private void button1_Click(object sender, EventArgs e) // Кнопка "Сгенерировать массив "
        {
            try
            {
                N = Convert.ToInt32(textBox1.Text); // Считывание введенных данных и присовение значения переменной
                a = Convert.ToInt32(textBox2.Text);// Считывание введенных данных и присовение значения переменной
                b = Convert.ToInt32(textBox4.Text);// Считывание введенных данных и присовение значения переменной
                array = new double[N]; // Исходный массив. Определение размера массива
                firstArray = new double[N];//Определение размера массива
                secondArray = new double[N];//Определение размера массива
                thirdArray = new double[N];//Определение размера массива
                fourthArray = new double[N];//Определение размера массива
                fifthArray = new double[N];//Определение размера массива
                Random rnd = new Random(); // Создание экземпляра(объекта) класс Random 
                for (int i = 0; i < array.Length - 1; i++)
                {
                    array[i] = rnd.Next(a, b); // С помощью объекта генерируются случайные числа в заданном диапазоне
                    firstArray[i] = array[i]; // Заполнение массивов, используемых для сортировки, числами из исходного массива (*)
                    secondArray[i] = array[i]; //(*)
                    thirdArray[i] = array[i];//(*)
                    fourthArray[i] = array[i];//(*)
                    fifthArray[i] = array[i];//(*)
                }
                for (int j = 0; j < (int)(array.Length * (50.0 / 100)); j++)
                {
                    firstArray[j] = 1; // Заполнение массивов, используемых для сортировки, числами из исходного массива (*)
                    secondArray[j] = 1; //(*)
                    thirdArray[j] = 1;//(*)
                    fourthArray[j] = 1;//(*)
                    fifthArray[j] = 1;//(*)
                }
                button1.Enabled = false;
                button2.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Заполните поля ввода");
            }
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e) // Флажок "Выбор всех методов"
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            if (checkBox6.Checked == false)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;

            }
        }   
        private void button2_Click(object sender, EventArgs e) // Кнопка "Вполнить сортировку"
        {
            textBox3.Text = null; //Обнуление области вывода результата сортировки
            textBox6.Text = null; //Обнуление области вывода части отсортированных элементов массивов
            chart1.Series[0].Points.Clear(); //Очистка графиков
            chart1.Series[1].Points.Clear(); //
            chart1.Series[2].Points.Clear(); //
            chart1.Series[3].Points.Clear(); //
            chart1.Series[4].Points.Clear(); //

            if (checkBox1.Checked == false & checkBox2.Checked == false & checkBox3.Checked == false &
                checkBox4.Checked == false & checkBox5.Checked == false & checkBox6.Checked == false
                )
            {
                MessageBox.Show("Выберите метод сортировки");
            }
            else
            {
                if (checkBox1.Checked == true) //Если первый флажок выбран, то используется сортировка пузырьком
                {
                    Stopwatch spw1 = new Stopwatch(); // Класс для измерения времени выполнения сортировки
                    spw1.Start(); // Начало отсчета времени
                    firstArray = Bubble_Sort(firstArray); //Вызов метода сортировки пузырьком
                    spw1.Stop(); //Конец отсчета времени
                    textBox3.Text += "Сортировка пузырьком: " + spw1.ElapsedMilliseconds + "\r\n"; //Вывод результата
                    textBox6.Text += "Сортировка пузырьком \r\n";
                    textBox6.Text += "Номер элемента | элемент\r\n";
                    for (int i = 0; i <= 100; i += 2) // Просмотр части отсортированного массива
                    {
                        textBox6.Text += "№" + i + "                        |   " + firstArray[i] + "\r\n";
                    }
                    textBox6.Text += "\r\n";
                }
                if (checkBox2.Checked == true) // Если выбран второй флажок, то используется сортировка выбором
                {
                    Stopwatch spw2 = new Stopwatch(); // Класс для измерения времени выполнения сортировки
                    spw2.Start();// Начало отсчета времени
                    secondArray = Choise_Sort(secondArray);//Вызов метода сортировки выбором
                    spw2.Stop();//Конец отсчета времени
                    textBox3.Text += "Сортировка выбором: " + spw2.ElapsedMilliseconds + "\r\n"; //Вывод результатов
                    textBox6.Text += "Сортировка выбором \r\n";
                    textBox6.Text += "Номер элемента | элемент\r\n";
                    for (int i = 0; i <= 100; i += 2) // Просмотр части отсортированного массива
                    {
                        textBox6.Text += "№" + i + "                       |   " + secondArray[i] + "\r\n";
                    }
                    textBox6.Text += "\r\n";
                }
                if (checkBox3.Checked == true) // Если выбран третий флажок, то используется сортировка вставками
                {
                    Stopwatch spw3 = new Stopwatch(); // Класс для измерения времени выполнения сортировки
                    spw3.Start();// Начало отсчета времени
                    thirdArray = Insert_Sort(thirdArray);//Вызов метода сортировки вставками
                    spw3.Stop();// Конец отсчета времени
                    textBox3.Text += "Сортировка вставками: " + spw3.ElapsedMilliseconds + "\r\n"; //Вывод результатов
                    textBox6.Text += "Сортировка вставками \r\n";
                    textBox6.Text += "Номер элемента | элемент\r\n";
                    for (int i = 0; i <= 100; i += 2) // Просмотр части отсортированного массива
                    {
                        textBox6.Text += "№" + i + "                       |   " + thirdArray[i] + "\r\n";
                    }
                    textBox6.Text += "\r\n";
                }
                if (checkBox4.Checked == true) // Если выбран четвертый флажок, то используется сортировка слиянием
                {
                    Stopwatch spw4 = new Stopwatch(); // Класс для измерения времени выполнения сортировки
                    spw4.Start(); // Начало отсчета времени
                    fourthArray = Merge_Sort(fourthArray); //Вызов метода сортировки слиянием
                    spw4.Stop(); // Конец отсчета времени
                    textBox3.Text += "Сортировка слиянием: " + spw4.ElapsedMilliseconds + "\r\n"; // Вывод результатов
                    textBox6.Text += "Сортировка слиянием \r\n";
                    textBox6.Text += "Номер элемента | элемент\r\n";
                    for (int i = 0; i <= 100; i += 2) // Просмотр части отсортированного массива
                    {

                        textBox6.Text += "№" + i + "                       |      " + fourthArray[i] + "\r\n";
                    }
                    textBox6.Text += "\r\n";
                }
                if (checkBox5.Checked == true) // Если выбран пятый флажок, то используется быстрая сортировка
                {
                    Stopwatch spw5 = new Stopwatch(); // Класс для измерения времени выполнения сортировки
                    spw5.Start();// Начало отсчета времени
                    fifthArray = Quick_Sort(fifthArray, 0, fifthArray.Length - 1); //Вызов метода сортировки слиянием
                    spw5.Stop(); // Конец отсчета времени
                    textBox3.Text += "Быстрая сортивка: " + spw5.ElapsedMilliseconds + "\r\n"; // Вывод результатов
                    textBox6.Text += "Быстрая сортировка \r\n";
                    textBox6.Text += "Номер элемента | элемент\r\n";
                    for (int i = 0; i <= 100; i += 2)// Просмотр части отсортированного массива
                    {
                        textBox6.Text += "№" + i + "                       |     " + fifthArray[i];
                    }
                    textBox6.Text += "\r\n";
                }
                button2.Enabled = false; // Кнопка "Выполнить сортировку" становится недоступна
                button3.Enabled = true; // Кнопка "Построить график" становится доступна
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                textBox6.Text += "\r\n";
            }
            
           
        }
        private void button3_Click(object sender, EventArgs e) // Кнопка "Построить график"
        {
            int h = N / points; // Определение расстояния между промежуточными точками
            for (int i = 1; i <= points; i++)
            {
                textBox3.Text += "\r\n";
                textBox3.Text += "Количество элементов = " + h * i;
                if (checkBox1.Checked == true)
                {
                    Stopwatch spw1 = new Stopwatch(); // Класс для измерения времени
                    firstArray = new double[i * h]; // Формирование массива новой длины
                    for (int i_first = 0; i_first < i * h; i_first++)
                    {
                        firstArray[i_first] = array[i_first]; // Заполнение массива новой длины числами исходного массива
                    }
                    spw1.Start();
                    firstArray = Bubble_Sort(firstArray);
                    spw1.Stop();
                    chart1.Series[0].Points.AddXY(i * h, spw1.ElapsedMilliseconds); // Построение графика
                    textBox3.Text += "\r\n Сортивка пузырьком:" + spw1.ElapsedMilliseconds; // Вывод результатов сортировки
                }
                if (checkBox2.Checked == true)
                {
                    Stopwatch spw2 = new Stopwatch(); // Класс для измерения времени
                    secondArray = new double[i * h]; // Формирование массива новой длины
                    for (int i_second = 0; i_second < i * h; i_second++)
                    {
                        secondArray[i_second] = array[i_second]; // Заполнение массива новой длины числами исходного массива
                    }
                    spw2.Start();
                    secondArray = Choise_Sort(secondArray);
                    spw2.Stop();
                    chart1.Series[1].Points.AddXY(i * h, spw2.ElapsedMilliseconds); // Построение графика
                    textBox3.Text += "\r\nСортировка выбором:" + spw2.ElapsedMilliseconds; // Вывод результатов сортировки
                }
                if (checkBox3.Checked == true)
                {
                    Stopwatch spw3 = new Stopwatch(); // Класс для измерения времени
                    thirdArray = new double[i * h]; // Формирование массива новой длины
                    for (int i_third = 0; i_third < i * h; i_third++)
                    {
                        thirdArray[i_third] = array[i_third]; // Заполнение массива новой длины числами исходного массива
                    }
                    spw3.Start();
                    thirdArray = Insert_Sort(thirdArray);
                    spw3.Stop();
                    chart1.Series[2].Points.AddXY(i * h, spw3.ElapsedMilliseconds); // Построение графика
                    textBox3.Text += "\r\n Сортировка вставками:" + spw3.ElapsedMilliseconds; // Вывод результатов сортировки
                }
                if (checkBox4.Checked == true)
                {
                    Stopwatch spw4 = new Stopwatch(); // Класс для измерения времени
                    fourthArray = new double[i * h]; // Формирование массива новой длины
                    for (int i_fourth = 0; i_fourth < i * h; i_fourth++)
                    {
                        fourthArray[i_fourth] = array[i_fourth]; // Заполнение массива новой длины числами исходного массива
                    }
                    spw4.Start();
                    fourthArray = Merge_Sort(fourthArray);
                    spw4.Stop();
                    chart1.Series[3].Points.AddXY(i * h, spw4.ElapsedMilliseconds); // Построение графика
                    textBox3.Text += "\r\nСортировка слиянием:" + spw4.ElapsedMilliseconds; // Вывод результатов сортировки
                }
                if (checkBox5.Checked == true)
                {
                    Stopwatch spw5 = new Stopwatch(); // Класс для измерения времени
                    fifthArray = new double[i * h]; // Формирование массива новой длины
                    for (int i_fifth = 0; i_fifth < i * h; i_fifth++)
                    {

                        fifthArray[i_fifth] = array[i_fifth]; // Заполнение массива новой длины числами исходного массива
                    }

                    spw5.Start();
                    fifthArray = Quick_Sort(fifthArray, 0, fifthArray.Length - 1);
                    spw5.Stop();
                    chart1.Series[4].Points.AddXY(i * h, spw5.ElapsedMilliseconds); // Построение графика
                    textBox3.Text += "\r\n Быстрая сортировка:" + spw5.ElapsedMilliseconds +"\r\n";  // Вывод результатов сортировки
                }
                button3.Enabled = false; // Кнопка "Построить график" становится недоступной
                button1.Enabled = true; // Кнопка "Сгенерировать массив" становится доступной
                
                textBox3.Text += "\r\n";
            }
        }
   
        static void Merge(double[] array, int lowIndex, int middleIndex, int highIndex)
        {
            int left = lowIndex;
            int right = middleIndex + 1;
            double[] tempArray = new double[highIndex - lowIndex + 1];
            int index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }
        static double[] Merge_Sort(double[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2;
                Merge_Sort(array, lowIndex, middleIndex);
                Merge_Sort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }

            return array;
        }
        static double[] Merge_Sort(double[] array)
        {
            return Merge_Sort(array, 0, array.Length - 1);
        }
        public static double[] Bubble_Sort(double[] arr) // Метод сортировки пузырьком
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    double temp; //Переменная, предназначенная для временного хранения обмениваемого числа
                    if (arr[i] > arr[j])
                    {
                        temp = arr[i];// Обмен значений в массиве
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
            return arr;
        }
        public static double[] Choise_Sort(double[] arr) // Метод сортировки выбором
        {
            int min_index = 0; //Предполагаем, что индекс минимального числа является первым
            for (int i = 0; i < arr.Length; i++)
            {

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[min_index])
                    {
                        min_index = j; // Замена значения индекса минимального элемента, если таковой был найден

                    }
                }
                double temp = arr[i]; //Обмен значений в массиве
                arr[i] = arr[min_index];
                arr[min_index] = temp;
            }
            return arr;
        }
        public static double[] Insert_Sort(double[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                double key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
            return arr;
        } // Метод вставок
        public static int FindPivot(double[] arr, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;
            double temp = 0;
            for (int i = minIndex; i < maxIndex - 1; i++)
            {
                if (arr[i] < arr[maxIndex])
                {
                    pivot++;
                    temp = arr[pivot];
                    arr[pivot] = arr[i];
                    arr[i] = temp;
                }
            }

            pivot++;
            temp = arr[pivot];
            arr[pivot] = arr[maxIndex];
            arr[maxIndex] = temp;

            return pivot;
        } // Поиск опорного элемента для метода быстрой сортировки
        public static double[] Quick_Sort(double[] arr, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return arr;
            }

            int pivot = FindPivot(arr, minIndex, maxIndex);
            Quick_Sort(arr, minIndex, pivot - 1);
            Quick_Sort(arr, pivot + 1, maxIndex);

            return arr;
        } // Быстрая сортировка
        
    }
}

       
