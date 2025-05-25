using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Дополнительно
{

    /* Методы расширения
     Нужно чтобы добавить функционал к существующему классу.  
     */


    /* Правила и особенности

    Методы расширения должны находиться в статическом классе.
    Сам метод должен быть статическим.
    Первый параметр метода должен быть с модификатором this, указывающим, к какому типу применяется метод. 

     */




    public class Person
    {
        public string Name { get; set; }
    }


    public static class PersonExtensions
    {
        // Обрати внимание на ключевое слово "this" перед параметром
        public static void SayHello(this Person person)
        {
            Console.WriteLine($"Привет, меня зовут {person.Name}!");
        }
    }


    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string ToCapitalize(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }


    internal class ExtensionMethod
    {

        public static void Main_()
        {
            Person p = new Person { Name = "Антон" };
            p.SayHello(); // Вывод: Привет, меня зовут Антон!


            string name = "anton";
            Console.WriteLine(name.ToCapitalize()); // Anton
            Console.WriteLine(name.IsNullOrEmpty()); // False
        }

    }
}
