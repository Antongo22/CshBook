using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons
{
    /* Перегрузка
        Перегрузка методов в C# — это возможность создавать несколько методов с одинаковым именем, но с разными параметрами. 
        Это позволяет использовать одно и то же имя метода для выполнения различных операций в зависимости от типа или количества передаваемых аргументов.
        
        Важно при этом, что при перегрузке учитываются только параметры, а не тип возвращаемого значения.
     */

    /* Params
        Params - это ключевое слово, которое может использоваться с любым типом данных.
        
        Ключевое слово params в C# позволяет передавать в метод переменное количество аргументов одного типа. 
        Это особенно полезно, когда вы не знаете заранее, сколько аргументов может понадобиться передать в метод. 
        Параметр с модификатором params должен быть одномерным массивом, и он всегда должен быть последним параметром в списке параметров метода.

        Params может принимать как данные через запятую так и массив

     */


    internal static class NinthLesson
    {
        #region Перегрузка
        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static int Add(params int[] numbers)
        {
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return sum;
        }
        #endregion

        #region Params
        // Метод, принимающий переменное количество целых чисел
        public static int Sum(params int[] numbers)
        {
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return sum;
        }


        // Метод с обычными параметрами и параметром params
        public static void Display(string message, params int[] numbers)
        {
            Console.WriteLine(message);
            foreach (int num in numbers)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
        #endregion


        public static void Main()
        {
            #region Перегрузка
            Console.WriteLine(Add(2, 3)); // Вызов метода Add(int, int)
            Console.WriteLine(Add(2, 3, 5)); // Вызов метода Add(int, int, int)
            Console.WriteLine(Add(2.5, 3.7)); // Вызов метода Add(double, double)
            Console.WriteLine(Add(1, 2, 3, 4, 5)); // Вызов метода Add(params int[])
            #endregion

            #region Params
            Console.WriteLine(Sum(1, 2, 3)); // 6
            Console.WriteLine(Sum(4, 5, 6, 7, 8)); // 30
            Console.WriteLine(Sum()); // 0 (пустой массив)
            int[] array = { 10, 20, 30 };
            Console.WriteLine(Sum(array)); // 60

            Display("Numbers:", 1, 2, 3); // Numbers: 1 2 3
            Display("Values:", 4, 5); // Values: 4 5
            Display("No numbers:"); // No numbers:
            #endregion
        }
    }
}
