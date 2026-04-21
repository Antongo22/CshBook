namespace CshBook.Answers.Chapter1.Lesson12Recursion
{
    internal static class AnswerLesson12Recursion
    {
        public static void CountDown(int n)
        {
            if (n < 1)
            {
                return;
            }

            Console.Write($"{n} ");
            CountDown(n - 1);
        }

        public static int SumToN(int n)
        {
            if (n <= 0)
            {
                return 0;
            }

            return n + SumToN(n - 1);
        }

        public static int Power(int value, int exponent)
        {
            if (exponent == 0)
            {
                return 1;
            }

            return value * Power(value, exponent - 1);
        }

        public static int SumDigits(int number)
        {
            number = Math.Abs(number);

            if (number < 10)
            {
                return number;
            }

            return number % 10 + SumDigits(number / 10);
        }

        public static int CountDigits(int number)
        {
            number = Math.Abs(number);

            if (number < 10)
            {
                return 1;
            }

            return 1 + CountDigits(number / 10);
        }

        public static void PrintArray(int[] numbers, int index)
        {
            if (index >= numbers.Length)
            {
                return;
            }

            Console.Write($"{numbers[index]} ");
            PrintArray(numbers, index + 1);
        }

        public static int FindMax(int[] numbers, int index)
        {
            if (index == numbers.Length - 1)
            {
                return numbers[index];
            }

            int maxInTail = FindMax(numbers, index + 1);

            if (numbers[index] > maxInTail)
            {
                return numbers[index];
            }

            return maxInTail;
        }

        public static string ReverseString(string text, int index)
        {
            if (index < 0)
            {
                return string.Empty;
            }

            return text[index] + ReverseString(text, index - 1);
        }

        public static bool IsPalindrome(string text, int left, int right)
        {
            if (left >= right)
            {
                return true;
            }

            if (text[left] != text[right])
            {
                return false;
            }

            return IsPalindrome(text, left + 1, right - 1);
        }

        public static int BinarySearch(int[] numbers, int left, int right, int target)
        {
            if (left > right)
            {
                return -1;
            }

            int middle = left + (right - left) / 2;

            if (numbers[middle] == target)
            {
                return middle;
            }

            if (target < numbers[middle])
            {
                return BinarySearch(numbers, left, middle - 1, target);
            }

            return BinarySearch(numbers, middle + 1, right, target);
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 12. Рекурсия");
            Console.WriteLine("=================");
            Console.WriteLine();

            Console.WriteLine("1. Обратный отсчет");
            CountDown(5);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("2. Сумма от 1 до n");
            Console.WriteLine(SumToN(6));
            Console.WriteLine();

            Console.WriteLine("3. Степень числа");
            Console.WriteLine(Power(3, 4));
            Console.WriteLine();

            Console.WriteLine("4. Сумма цифр");
            Console.WriteLine(SumDigits(4832));
            Console.WriteLine();

            Console.WriteLine("5. Количество цифр");
            Console.WriteLine(CountDigits(4832));
            Console.WriteLine();

            Console.WriteLine("6. Вывод массива");
            PrintArray(new int[] { 4, 7, 9, 2 }, 0);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("7. Максимум в массиве");
            Console.WriteLine(FindMax(new int[] { 4, 7, 9, 2 }, 0));
            Console.WriteLine();

            Console.WriteLine("8. Разворот строки");
            Console.WriteLine(ReverseString("кот", 2));
            Console.WriteLine();

            Console.WriteLine("9. Палиндром");
            Console.WriteLine(IsPalindrome("level", 0, "level".Length - 1));
            Console.WriteLine();

            Console.WriteLine("10. Бинарный поиск");
            Console.WriteLine(BinarySearch(new int[] { 2, 4, 6, 8, 10, 12 }, 0, 5, 8));
            Console.WriteLine();
        }
    }
}
