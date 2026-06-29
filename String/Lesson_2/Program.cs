namespace String_less_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s1 = "hello";
            string s2 = "world!";
            string s3 = string.Concat(s1, s2);// Объединение строк
            Console.WriteLine(s3);

            string st1 = "hey";
            string st2 = "woooh";
            string st3 = "isn't it?";
            string[] values = new string[] { st1, st2, st3 }; //Создание массива строк на основе трёх строк
            string final = String.Join("T", values);
            Console.WriteLine(final);
            foreach(var v in values)
            {
                Console.Write(v);
            }

            int result = string.Compare(s1, s2); //Сравнение двух строк
                if (result < 0)
                {
                    Console.WriteLine("Строка s1 перед строкой s2");
                }
                else if (result > 0)
                {
                    Console.WriteLine("Строка s1 стоит после строки s2");
                }
                else
                {
                    Console.WriteLine("Строки s1 и s2 идентичны");
                }
            Console.WriteLine("Индекс буквы i = " + final.IndexOf('i'));
            string[] _varriables = new string[] {
                "people.exe",
                "forest.png",
                "woman.exe",
                "child.dll",
                "duck.exe"
            };
            foreach (var s in _varriables) 
            {
                if (s.EndsWith(".exe")) //Поиск строк, которые оканчиваются на заданный фильтр
                {
                    Console.WriteLine(s);
                }
            }

            string someString = "what a beautifull day!";
            string[] words = someString.Split(' ',StringSplitOptions.RemoveEmptyEntries); //Разбиение строки на отдельные строки и создания на их основе массива
            foreach(var v in words)
            {
                Console.WriteLine(v);
            }
            someString = someString.Remove(0, 4);
            Console.WriteLine(someString);
        }
    }
}
