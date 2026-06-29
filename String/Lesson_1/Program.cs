namespace String_Less_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Создание строк. Различные способы.
            char[] c = { 'a', 'b', 'c', 'd', 's' };
            string s1 = "Hello";
            string s2 = new String('a', 6);
            string s3 = new String(new char[] {'w','o','l','f'});
            string s4 = new String(c,1,2);

            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
            Console.WriteLine(s4);

            //Строка как набор символов.
            string message = "hello";
            char firstChar = message[0];
            Console.WriteLine(firstChar);

            Console.WriteLine(message.Length);

            //Перебор строк
            string example = "qwert qqqwwweeerrr";
            foreach(var a in example)
            {
                Console.Write(a + " ");
            }

            //Сравнение строк. Сравниваются значения, а не ссылки
            string ex1 = "hello";
            string ex2 = "hello";
            Console.WriteLine(ex1 == ex2);
            Console.WriteLine(String.Compare(ex1,ex2));

            //Многострочные строки
            string text = """
              <element attr="content">
                <body>
                </body>
              </element>
              """;
            Console.WriteLine(text);

            /*Основные методы класса String
            Compare - сравнивает две строки с учетом текущей культуры (локали) пользователя
            CompareOrdinal - сравнивает две строки без учета локали
            Contains - определяет, содержится ли подстрока в строке
            Concat - соединяет строки
            CopyTo - копирует часть строки, начиная с определенного индекса в массив
            Format - форматирует строку 
            IndexOf - находит индекс первого вхождения символа или подстроки в строке 
            Split - разделяет одну строку на массив строк
            Substring - извлекает из строки подстроку, начиная с указанной позиции
            ToLower - переводит все символы строки в нижний регистр
            ToUpper - переводит все символы строки в верхний регистр
            Trim - удаляет начальные и конечные пробелы из строки
            */
        }
    }
}
