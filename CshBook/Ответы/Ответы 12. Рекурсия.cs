using System;

namespace CshBook.Lessons
{
    internal static class RecursionSolutions
    {
        // 1. Найти сумму цифр числа
        public static int SumOfDigits(int n)
        {
            if (n == 0) return 0;
            return n % 10 + SumOfDigits(n / 10);
        }

        // 2. Найти наибольший общий делитель (НОД) двух чисел
        public static int GCD(int a, int b)
        {
            if (b == 0) return a;
            return GCD(b, a % b);
        }

        // 3. Вывести все элементы массива рекурсивно
        public static void PrintArray(int[] array, int index)
        {
            if (index >= array.Length) return;
            Console.Write(array[index] + " ");
            PrintArray(array, index + 1);
        }

        // 4. Подсчитать количество цифр в числе
        public static int CountDigits(int n)
        {
            if (n == 0) return 0;
            return 1 + CountDigits(n / 10);
        }

        // 5. Найти минимальный элемент массива рекурсивно
        public static int FindMin(int[] array, int index)
        {
            if (index == array.Length - 1) return array[index];
            return Math.Min(array[index], FindMin(array, index + 1));
        }

        // 6. Развернуть строку с помощью рекурсии
        public static string ReverseString(string str)
        {
            if (str.Length <= 1) return str;
            return str[^1] + ReverseString(str[..^1]);
        }

        // 7. Проверить, является ли число степенью двойки
        public static bool IsPowerOfTwo(int n)
        {
            if (n <= 0) return false;
            if (n == 1) return true;
            return n % 2 == 0 && IsPowerOfTwo(n / 2);
        }

        // 8. Вывести все числа от n до 1
        public static void PrintNumbersDescending(int n)
        {
            if (n < 1) return;
            Console.Write(n + " ");
            PrintNumbersDescending(n - 1);
        }

        // 9. Подсчитать количество гласных в строке рекурсивно
        public static int CountVowels(string str, int index)
        {
            if (index >= str.Length) return 0;
            char c = char.ToLower(str[index]);
            int count = "aeiou".Contains(c) ? 1 : 0;
            return count + CountVowels(str, index + 1);
        }

        // 10. Реализовать бинарный поиск с помощью рекурсии
        public static int BinarySearch(int[] array, int left, int right, int target)
        {
            if (left > right) return -1;
            int mid = left + (right - left) / 2;
            if (array[mid] == target) return mid;
            if (array[mid] > target) return BinarySearch(array, left, mid - 1, target);
            return BinarySearch(array, mid + 1, right, target);
        }
    }
}
