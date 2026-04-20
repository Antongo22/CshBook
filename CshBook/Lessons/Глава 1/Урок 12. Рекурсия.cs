namespace CshBook.Lessons.Chapter1.Lesson12Recursion
{
    #region Теория
    /*
        Рекурсия - это способ решения задачи,
        при котором метод вызывает сам себя.

        Такой подход подходит не для всего подряд.
        Он полезен там, где задача естественно распадается
        на такой же, но более маленький случай.
     */

    /*
        У любой рекурсии должны быть две части:

        1. Базовый случай.
           Это момент, когда метод больше не вызывает себя.

        2. Рекурсивный шаг.
           Это переход к более маленькой версии той же задачи.
     */

    /*
        Например, сумма чисел от 1 до n:

        SumToN(5)
        = 5 + SumToN(4)
        = 5 + 4 + SumToN(3)
        ...
        = 5 + 4 + 3 + 2 + 1

        Базовый случай здесь можно сделать таким:
        если n <= 0, вернуть 0.
     */

    /*
        При написании рекурсивного метода задавай себе четыре вопроса:

        - где задача заканчивается;
        - как сделать входные данные "меньше";
        - точно ли метод дойдет до остановки;
        - что должно вернуться на самом простом шаге.
     */

    /*
        На раннем этапе рекурсия полезна, чтобы научиться:

        - мыслить шагами;
        - видеть повторяющийся шаблон;
        - аккуратно описывать задачу через метод.

        Но важно помнить:
        если задачу проще решить циклом, цикл часто будет понятнее.
        Рекурсия нужна не "вместо циклов всегда",
        а тогда, когда она делает логику яснее.
     */

    /*
        Где рекурсия встречается уже сейчас:

        - обратный отсчет;
        - сумма чисел;
        - сумма цифр;
        - обход массива;
        - проверка строки с двух сторон;
        - бинарный поиск.
     */
    #endregion

    internal static class Lesson12Recursion
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

        public static void PrintArray(int[] numbers, int index)
        {
            if (index >= numbers.Length)
            {
                return;
            }

            Console.Write($"{numbers[index]} ");
            PrintArray(numbers, index + 1);
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

        public static void Main_()
        {
            Console.WriteLine("Обратный отсчет:");
            CountDown(5);
            Console.WriteLine();

            Console.WriteLine("----");

            Console.WriteLine($"Сумма от 1 до 5: {SumToN(5)}");
            Console.WriteLine($"2 в степени 4: {Power(2, 4)}");
            Console.WriteLine($"Сумма цифр числа 483: {SumDigits(483)}");

            Console.WriteLine("----");

            int[] numbers = { 4, 7, 9, 2 };
            Console.Write("Вывод массива рекурсивно: ");
            PrintArray(numbers, 0);
            Console.WriteLine();

            Console.WriteLine("----");

            string word = "level";
            Console.WriteLine($"\"{word}\" - палиндром: {IsPalindrome(word, 0, word.Length - 1)}");
        }
    }

    #region Задачи
    /*
        Разминка

        1. Обратный отсчет.
           Напиши метод CountDown(int n),
           который выводит числа от n до 1.

        2. Сумма от 1 до n.
           Напиши метод SumToN(int n),
           который рекурсивно находит сумму чисел от 1 до n.

        3. Степень числа.
           Напиши метод Power(int value, int exponent),
           который рекурсивно возводит число в неотрицательную степень.

        Основные задачи

        4. Сумма цифр.
           Напиши метод SumDigits(int number),
           который рекурсивно находит сумму цифр числа.

        5. Количество цифр.
           Напиши метод CountDigits(int number),
           который рекурсивно считает количество цифр в числе.

        6. Вывод массива.
           Напиши метод PrintArray(int[] numbers, int index),
           который рекурсивно выводит все элементы массива слева направо.

        7. Максимум в массиве.
           Напиши метод FindMax(int[] numbers, int index),
           который рекурсивно находит максимальный элемент массива.

        8. Разворот строки.
           Напиши метод ReverseString(string text, int index),
           который возвращает строку в обратном порядке.

        Задачи на перенос

        9. Палиндром.
           Напиши метод IsPalindrome(string text, int left, int right),
           который проверяет строку с двух сторон.

        10. Бинарный поиск.
            Напиши метод BinarySearch(int[] numbers, int left, int right, int target).
            Считай, что массив уже отсортирован по возрастанию.
            Метод должен вернуть индекс элемента или -1, если элемента нет.
     */
    #endregion
}
