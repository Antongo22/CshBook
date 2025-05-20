using System;

namespace CshBook.Answers.Answ0
{
    public class BaseSolutions
    {
        public static void Main_()
        {
            // 1 Пользователь вводит два числа, нужно найти их сумму
            Console.Write("Введите первое число: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Введите второе число: ");
            int num2 = int.Parse(Console.ReadLine());
            int sum1 = num1 + num2;
            Console.WriteLine("Сумма = " + sum1);

            Console.WriteLine("\n\n");


            // 2 Пользователь вводит число, поделить это число на 5 без остатка
            Console.Write("Введите число: ");
            int num3 = int.Parse(Console.ReadLine());
            int res1 = num3 / 5;
            Console.WriteLine("Деление без остатка: " + res1);

            Console.WriteLine("\n\n");
            Console.WriteLine("\n\n");


            // 3 Пользователь вводит число, найти остаток от деления 100 на это число
            Console.Write("Введите число: ");
            int num4 = int.Parse(Console.ReadLine());
            int res2 = num4 % 100;
            Console.WriteLine("Деление с остатком на 100: " + res1);

            Console.WriteLine("\n\n");

            // 4 Записать данные из задач 1 - 3 и красиво вывести их через f-строку в консоль

            Console.WriteLine($"{num1} + {num2} = {sum1}");
            Console.WriteLine($"{num3} / 5 = {res1}");
            Console.WriteLine($"{num4} % 100 = {res2}");

            Console.WriteLine("\n\n");

            // 5 Пользователь вводит возраст. Вывести True, если чему больше 18, иначе else
            Console.Write("Введите втзраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine(age > 18);

        }

    }
}
