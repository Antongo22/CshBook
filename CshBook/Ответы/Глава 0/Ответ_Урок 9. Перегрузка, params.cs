namespace CshBook.Answers.Chapter0.Lesson09OverloadsAndParams
{
    internal static class AnswerLesson09OverloadsAndParams
    {
        public static void PrintGreeting(string name)
        {
            Console.WriteLine($"Привет, {name}!");
        }

        public static void PrintGreeting(string name, int age)
        {
            Console.WriteLine($"Привет, {name}! Тебе {age} лет.");
        }

        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        public static int GetMax(int a, int b)
        {
            return a > b ? a : b;
        }

        public static int GetMax(int a, int b, int c)
        {
            int max = a;

            if (b > max)
            {
                max = b;
            }

            if (c > max)
            {
                max = c;
            }

            return max;
        }

        public static void PrintNumbers(params int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write($"{numbers[i]} ");
            }

            Console.WriteLine();
        }

        public static int Sum(params int[] numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        public static int CountEven(params int[] numbers)
        {
            int count = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    count++;
                }
            }

            return count;
        }

        public static void PrintPurchases(string title, params string[] items)
        {
            Console.WriteLine(title);

            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"- {items[i]}");
            }
        }

        public static double GetAverage(params int[] grades)
        {
            if (grades.Length == 0)
            {
                return 0;
            }

            int sum = 0;

            for (int i = 0; i < grades.Length; i++)
            {
                sum += grades[i];
            }

            return (double)sum / grades.Length;
        }

        public static string BuildProfile(string name)
        {
            return $"Пользователь: {name}";
        }

        public static string BuildProfile(string name, int age)
        {
            return $"Пользователь: {name}, возраст: {age}";
        }

        public static void PrintReceipt(string shopName, params string[] products)
        {
            Console.WriteLine($"Магазин: {shopName}");

            for (int i = 0; i < products.Length; i++)
            {
                Console.WriteLine($"Товар {i + 1}: {products[i]}");
            }
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 9. Перегрузка и params");
            Console.WriteLine("===========================");
            Console.WriteLine();

            Console.WriteLine("1. Перегруженное приветствие");
            PrintGreeting("Анна");
            PrintGreeting("Анна", 16);
            Console.WriteLine();

            Console.WriteLine("2. Сумма двух и трех чисел");
            Console.WriteLine(Add(4, 5));
            Console.WriteLine(Add(4, 5, 6));
            Console.WriteLine();

            Console.WriteLine("3. Максимум");
            Console.WriteLine(GetMax(8, 15));
            Console.WriteLine(GetMax(8, 15, 11));
            Console.WriteLine();

            Console.WriteLine("4. Печать чисел через params");
            PrintNumbers(3, 6, 9, 12);
            Console.WriteLine();

            Console.WriteLine("5. Сумма через params");
            Console.WriteLine(Sum(1, 2, 3, 4, 5));
            Console.WriteLine();

            Console.WriteLine("6. Подсчет четных чисел");
            Console.WriteLine(CountEven(2, 5, 8, 11, 14));
            Console.WriteLine();

            Console.WriteLine("7. Печать покупок");
            PrintPurchases("Покупки на вечер:", "Хлеб", "Молоко", "Сыр");
            Console.WriteLine();

            Console.WriteLine("8. Средний балл");
            Console.WriteLine($"{GetAverage(5, 4, 5, 3):F2}");
            Console.WriteLine();

            Console.WriteLine("9. Карточка пользователя");
            Console.WriteLine(BuildProfile("Игорь"));
            Console.WriteLine(BuildProfile("Игорь", 21));
            Console.WriteLine();

            Console.WriteLine("10. Мини-чек");
            PrintReceipt("Минимаркет", "Яблоки", "Сок", "Печенье");
            Console.WriteLine();
        }
    }
}
