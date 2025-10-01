namespace Delegates
{
    delegate void Message(); //Простейший делегат для вывод информации объекта
    delegate int Operation(int x, int y);// Делегат для работы с методами, которые требуют параметр вызова других методов

    internal class Program
    {
        static void DoOperation(int x, int y, Operation op) //Метод для вызова метода с помощью делегата
        {
            Console.WriteLine(op(x,y)); //Результат отработанного метода
        }
        public static void Main(string[] args)
        {
            
        Info info = new Info();

            Message? mes = new Info().InfoAboutName; //Использование делегатов 
            mes += info.InfoAboutAge;
            mes();

            mes = null;
            // Не работает mes()?;
            mes?.Invoke(); // Работает. Метод Invoke() может принимать параметры вместо (и для) делегата

            Operations ops = new Operations();

            DoOperation(1, 4, ops.Sum);
            DoOperation(1, 4, ops.Multiply);
        }
    } 
    public class Operations
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
        public int Multiply(int a, int b)
        {
            return a * b;
        }
    }
    public class Info
    {
        public void InfoAboutName()
        {
            Console.WriteLine("Name is ...");
        }
        public void InfoAboutAge()
        {
            Console.WriteLine("Age is ... ");
        }
    }
}

