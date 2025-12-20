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


            // Пользователь вводит число, если оно кратно 5, проверяем на кратность 3, если нет, то пропускаем. (+тернарный)
            int num2 = int.Parse(Console.ReadLine());
            if (num2 % 5 == 0)
            {
                if (num2 % 3 == 0)
                {
                    Console.WriteLine("кратно 5 и 3");
                }
                else
                {
                    Console.WriteLine("кратно 5");
                }
            }
            else
            {
                Console.WriteLine("не кратно 5");
            }

            Console.WriteLine(num2 % 5 == 0 ? (num2 % 3 == 0 ? "кратно 5 и 3" : "кратно 5") : "не кратно 5");



            // Пользователь вводит число, если чётно, то пропускаем, иначе проверяем на кратность 3. (+тернарный)
            int num3 = int.Parse(Console.ReadLine());
            if (num3 % 2 == 0)
            {
                Console.WriteLine("чётное");
            }
            else if (num3 % 3 == 0)
            {
                Console.WriteLine("нечётное, но кратно 3");
            }
            else
            {
                Console.WriteLine("нечётное и не кратно 3");
            }


            Console.WriteLine(num3 % 2 == 0 ? "чётное" : (num3 % 3 == 0 ? "нечётное, но кратно 3" : "нечётное и не кратно 3"));



            // Дано число. Если оно от -10 до 10 не включительно, то увеличить его на 5, иначе уменьшить на 10. (+тернарный)
            int num4 = int.Parse(Console.ReadLine());
            if (num4 > -10 && num4 < 10)
            {
                num4 += 5;
            }
            else
            {
                num4 -= 10;
            }

            Console.WriteLine(num4 > -10 && num4 < 10 ? num4 += 5 : num4 -= 10);


            // Пользователь вводит три числа. Если все числа больше 10 и первые два числа делятся на 3, то вывести yes, иначе no.
            int n1 = int.Parse(Console.ReadLine());
            int n2 = int.Parse(Console.ReadLine());
            int n3 = int.Parse(Console.ReadLine());

            if (n1 > 10 && n2 > 10 && n3 > 10 && n1 % 3 == 0 && n2 % 3 == 0)
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }


            // Дано четыре числа, если первые два числа больше 5, третье число делится на 6, четвертое число не делится на 3, то вывести yes, иначе no.
            int m1 = int.Parse(Console.ReadLine());
            int m2 = int.Parse(Console.ReadLine());
            int m3 = int.Parse(Console.ReadLine());
            int m4 = int.Parse(Console.ReadLine());

            if (m1 > 5 && m2 > 5 && m3 % 6 == 0 && m4 % 3 != 0)
            {
                Console.WriteLine("yes");
            }
            else
            {
                Console.WriteLine("no");
            }


            // Дано три числа. Найти количество положительных чисел среди них.
            int p1 = int.Parse(Console.ReadLine());
            int p2 = int.Parse(Console.ReadLine());
            int p3 = int.Parse(Console.ReadLine());

            int count = 0;
            if (p1 > 0) count++;
            if (p2 > 0) count++;
            if (p3 > 0) count++;

            Console.WriteLine(count);


            // Дана дата из трех чисел (день, месяц и год). Вывести yes, если такая дата существует (например, 12 02 1999 - yes, 22 13 2001 - no). Считать, что в феврале всегда 28 дней.
            int d = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());

            bool isValidDate = false;
            if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12)
            {
                isValidDate = d <= 31;
            }
            else if (m == 4 || m == 6 || m == 9 || m == 11)
            {
                isValidDate = d <= 30;
            }
            else if (m == 2)
            {
                isValidDate = d <= 28;
            }

            Console.WriteLine(isValidDate ? "yes" : "no");


            // Даны действительные положительные числа a, b, с. По трем сторонам с длинами а, Ь, с можно построить треугольник. Найти углы треугольника.
            double a = Convert.ToDouble(Console.ReadLine());
            double b = Convert.ToDouble(Console.ReadLine());
            double c = Convert.ToDouble(Console.ReadLine());

            double A = Math.Acos((b * b + c * c - a * a) / (2 * b * c));
            double B = Math.Acos((a * a + c * c - b * b) / (2 * a * c));
            double C = Math.Acos((a * a + b * b - c * c) / (2 * a * b));

            Console.WriteLine($"A = {A}, B = {B}, C = {C}");



            // Дано трехзначное число. Переставьте первую и последнюю цифры.
            int number_ = int.Parse(Console.ReadLine());
            int hundreds = number_ / 100;
            int tens = (number_ / 10) % 10;
            int units = number_ % 10;
            int newNumber = units * 100 + tens * 10 + hundreds;
            Console.WriteLine(newNumber);


            // Пользователь вводит целое число. Проверьте является ли это число четырехзначным, если является, то выведите строку "Успешно", иначе "Неудача". (+тернарный)
            int number = int.Parse(Console.ReadLine());
            string result_ = number >= 1000 && number < 10000 ? "Успешно" : "Неудача";
            Console.WriteLine(result_);



            // Итоговое задание - написать свой калькулятор. Подумать, как можно добавить тернарный оператор
            Console.WriteLine("Введите первое число: ");
            double num1_ = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите второе число: ");
            double num2_ = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите операцию (+, -, *, /): ");
            string operation = Console.ReadLine();


            if (operation == "+")
            {
                Console.WriteLine($"Результат: {num1_ + num2_}");
            }
            else if (operation == "-")
            {
                Console.WriteLine($"Результат: {num1_ - num2_}");
            }
            else if (operation == "*")
            {
                Console.WriteLine($"Результат: {num1_ * num2_}");
            }
            else if (operation == "/")
            {
                Console.WriteLine($"Результат: {num1_ / num2_}");
            }
            else
            {
                Console.WriteLine("Неизвестная операция");
            }


            double result = operation == "+" ? num1 + num2 : operation == "-" ? num1 - num2 : operation == "*" ? num1 * num2 : operation == "/" ? num1 / num2 : 0;
            Console.WriteLine($"Результат: {result}");

        }

    }
}
