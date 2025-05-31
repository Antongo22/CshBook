using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Ответы.Глава_0
{
    internal class IfSolutions
    {
        public static void Main_()
        {
            // Пользователь вводит возраст, если больше или равно 18, то пропускаем, иначе нет. (+тернарный)
            Console.WriteLine("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());
            if (age >= 18)
            {
                Console.WriteLine("Вам можно пройти!");
            }
            else
            {
                Console.WriteLine("Вы ещё малы!(");
            }

            Console.WriteLine(age >= 18 ? "Вам можно пройти!" : "Вы ещё малы!(\n\n");



            // Пользователь вводит число, если оно чётное, то пропускаем, иначе нет. (+тернарный)
            
            int num1 = int.Parse(Console.ReadLine());
            if (num1 % 2 == 0)
            {
                Console.WriteLine("чётное");
            }
            else
            {
                Console.WriteLine("нечётное");
            }

            Console.WriteLine(num1 % 2 == 0 ? "чётное" : "нечётное");
        }

    }
}
