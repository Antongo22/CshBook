using System;

namespace CshBook.Lessons
{
    /* Двенадцатый урок: Рекурсия
     
       Рекурсия – это метод программирования, при котором функция вызывает саму себя.
       Она полезна для решения задач, которые можно разбить на более мелкие подзадачи
       аналогичной структуры.

       Основные компоненты рекурсивной функции:
       1. Базовый случай – условие, при котором рекурсия прекращается.
       2. Рекурсивный вызов – вызов функции самой себя с новыми параметрами.
    */

    internal static class TwelfthLesson
    {
        /* Пример 1: Факториал числа */
        public static int Factorial(int n)
        {
            if (n == 0) return 1; // Базовый случай
            return n * Factorial(n - 1); // Рекурсивный вызов
        }

        /* Пример 2: Числа Фибоначчи */
        public static int Fibonacci(int n)
        {
            if (n == 0) return 0; // Базовый случай
            if (n == 1) return 1; // Базовый случай
            return Fibonacci(n - 1) + Fibonacci(n - 2); // Рекурсивный вызов
        }

        /* Пример 3: Возведение числа в степень */
        public static int Power(int baseNum, int exponent)
        {
            if (exponent == 0) return 1; // Базовый случай
            return baseNum * Power(baseNum, exponent - 1); // Рекурсивный вызов
        }

        /* Пример 4: Сумма чисел от 1 до n */
        public static int SumToN(int n)
        {
            if (n == 0) return 0; // Базовый случай
            return n + SumToN(n - 1); // Рекурсивный вызов
        }

        /* Пример 5: Обратный вывод массива */
        public static void ReverseArray(int[] array, int index)
        {
            if (index < 0) return; // Базовый случай
            Console.Write(array[index] + " ");
            ReverseArray(array, index - 1); // Рекурсивный вызов
        }

        /* Пример 6: Проверка палиндрома */
        public static bool IsPalindrome(string str, int left, int right)
        {
            if (left >= right) return true; // Базовый случай
            if (str[left] != str[right]) return false; // Если символы не совпадают, не палиндром
            return IsPalindrome(str, left + 1, right - 1); // Рекурсивный вызов
        }

        public static void Main()
        {
            Console.WriteLine($"Факториал 5: {Factorial(5)}");
            Console.WriteLine($"10-е число Фибоначчи: {Fibonacci(10)}");
            Console.WriteLine($"2 в 5-й степени: {Power(2, 5)}");
            Console.WriteLine($"Сумма чисел от 1 до 10: {SumToN(10)}");

            int[] array = { 1, 2, 3, 4, 5 };
            Console.Write("Массив в обратном порядке: ");
            ReverseArray(array, array.Length - 1);
            Console.WriteLine();

            Console.WriteLine($"Является ли 'level' палиндромом? {IsPalindrome("level", 0, "level".Length - 1)}");
        }
    }
}

/* Задачи для отработки рекурсии

1. Найти сумму цифр числа.
2. Найти наибольший общий делитель (НОД) двух чисел.
3. Вывести все элементы массива рекурсивно.
4. Подсчитать количество цифр в числе.
5. Найти минимальный элемент массива рекурсивно.
6. Развернуть строку с помощью рекурсии.
7. Проверить, является ли число степенью двойки.
8. Вывести все числа от n до 1.
9. Подсчитать количество гласных в строке рекурсивно.
10. Реализовать бинарный поиск с помощью рекурсии.
*/
