
using System.Text;

namespace String_less_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder("Hello world!!",32);
            Console.WriteLine(sb.ToString());
            Console.WriteLine(sb);
            Console.WriteLine($"Длина: {sb.Length} \nЁмкость: {sb.Capacity}");

            sb.Append("How are your day today?");
            Console.WriteLine($"Длина: {sb.Length} \nЁмкость: {sb.Capacity}");

            sb.Append("Is there anything that is bothering you in any way?");
            Console.WriteLine($"Длина: {sb.Length} \nЁмкость: {sb.Capacity}");

            sb.Insert(1, "NO");
            Console.WriteLine(sb);

            sb.Replace("world!!", "МИР!!");
            Console.WriteLine(sb);

            string text = sb.ToString();
            Console.WriteLine(text);
        }
    }
}
