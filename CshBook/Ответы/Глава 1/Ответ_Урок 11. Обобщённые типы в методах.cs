namespace CshBook.Answers.Chapter1.Lesson11GenericMethods
{
    internal static class AnswerLesson11GenericMethods
    {
        public static void PrintValue<T>(T value)
        {
            Console.WriteLine(value);
        }

        public static void PrintArray<T>(T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Console.Write($"{values[i]} ");
            }

            Console.WriteLine();
        }

        public static T GetFirst<T>(T[] values)
        {
            return values[0];
        }

        public static T GetLast<T>(T[] values)
        {
            return values[values.Length - 1];
        }

        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        public static T[] CreateFilledArray<T>(int size, T value)
        {
            T[] result = new T[size];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = value;
            }

            return result;
        }

        public static bool Contains<T>(T[] values, T target)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (Equals(values[i], target))
                {
                    return true;
                }
            }

            return false;
        }

        public static int CountOccurrences<T>(T[] values, T target)
        {
            int count = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (Equals(values[i], target))
                {
                    count++;
                }
            }

            return count;
        }

        public static T[] CopyArray<T>(T[] source)
        {
            T[] copy = new T[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                copy[i] = source[i];
            }

            return copy;
        }

        public static void PrintReversed<T>(T[] values)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                Console.Write($"{values[i]} ");
            }

            Console.WriteLine();
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 11. Обобщённые типы в методах");
            Console.WriteLine("==================================");
            Console.WriteLine();

            Console.WriteLine("1. Вывод значения");
            PrintValue(42);
            PrintValue("Привет");
            Console.WriteLine();

            Console.WriteLine("2. Вывод массива");
            PrintArray(new int[] { 1, 2, 3, 4 });
            PrintArray(new string[] { "apple", "banana", "cherry" });
            Console.WriteLine();

            Console.WriteLine("3. Первый элемент");
            Console.WriteLine(GetFirst(new int[] { 10, 20, 30 }));
            Console.WriteLine();

            Console.WriteLine("4. Последний элемент");
            Console.WriteLine(GetLast(new string[] { "red", "green", "blue" }));
            Console.WriteLine();

            Console.WriteLine("5. Обмен значений");
            int left = 5;
            int right = 10;
            Swap(ref left, ref right);
            Console.WriteLine($"left = {left}, right = {right}");
            Console.WriteLine();

            Console.WriteLine("6. Создание заполненного массива");
            PrintArray(CreateFilledArray(4, "empty"));
            Console.WriteLine();

            Console.WriteLine("7. Поиск элемента");
            Console.WriteLine(Contains(new int[] { 1, 2, 3, 4 }, 3));
            Console.WriteLine();

            Console.WriteLine("8. Подсчет совпадений");
            Console.WriteLine(CountOccurrences(new string[] { "a", "b", "a", "c", "a" }, "a"));
            Console.WriteLine();

            Console.WriteLine("9. Копирование массива");
            PrintArray(CopyArray(new int[] { 7, 8, 9 }));
            Console.WriteLine();

            Console.WriteLine("10. Вывод в обратном порядке");
            PrintReversed(new string[] { "first", "second", "third" });
            Console.WriteLine();
        }
    }
}
