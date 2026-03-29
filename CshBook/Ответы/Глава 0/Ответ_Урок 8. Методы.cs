namespace CshBook.Answers.Chapter0.Lesson08Methods
{
    internal static class AnswerLesson08Methods
    {
        public static void Main_()
        {
            PrintLessonTitle("Урок 8. Методы");

            SolveTask01();
            SolveTask02();
            SolveTask03();
            SolveTask04();
            SolveTask05();
            SolveTask06();
            SolveTask07();
            SolveTask08();
            SolveTask09();
            SolveTask10();
            SolveTask11();
            SolveTask12();
        }

        private static void SolveTask01()
        {
            PrintTaskTitle(1, "Метод приветствия");
            SayHello("Анна");
            Console.WriteLine();
        }

        private static void SolveTask02()
        {
            PrintTaskTitle(2, "Печать чисел");
            PrintNumbers(5);
            Console.WriteLine();
        }

        private static void SolveTask03()
        {
            PrintTaskTitle(3, "Печать массива");
            PrintArray(new int[] { 3, 6, 9, 12 });
            Console.WriteLine();
        }

        private static void SolveTask04()
        {
            PrintTaskTitle(4, "Сумма двух чисел");
            Console.WriteLine(Add(7, 9));
            Console.WriteLine();
        }

        private static void SolveTask05()
        {
            PrintTaskTitle(5, "Максимум из двух чисел");
            Console.WriteLine(GetMax(18, 11));
            Console.WriteLine();
        }

        private static void SolveTask06()
        {
            PrintTaskTitle(6, "Проверка четности");
            Console.WriteLine(IsEven(24));
            Console.WriteLine();
        }

        private static void SolveTask07()
        {
            PrintTaskTitle(7, "Количество положительных чисел");
            Console.WriteLine(CountPositive(new int[] { -3, 4, 0, 7, -1, 5 }));
            Console.WriteLine();
        }

        private static void SolveTask08()
        {
            PrintTaskTitle(8, "Среднее арифметическое");
            Console.WriteLine(GetAverage(new int[] { 4, 5, 3, 5 }));
            Console.WriteLine();
        }

        private static void SolveTask09()
        {
            PrintTaskTitle(9, "Последняя цифра числа");
            Console.WriteLine(GetLastDigit(1287));
            Console.WriteLine();
        }

        private static void SolveTask10()
        {
            string productName = "Тетрадь";
            int price = ReadPrice();
            int quantity = ReadQuantity();
            int total = CalculateTotal(price, quantity);

            PrintTaskTitle(10, "Чек в магазине");
            PrintReceipt(productName, price, quantity, total);
            Console.WriteLine();
        }

        private static void SolveTask11()
        {
            string name = ReadName();
            int age = ReadAge();
            string profile = FormatProfile(name, age);

            PrintTaskTitle(11, "Карточка пользователя");
            Console.WriteLine(profile);
            Console.WriteLine();
        }

        private static void SolveTask12()
        {
            PrintTaskTitle(12, "ReverseString");
            Console.WriteLine(ReverseString("Программирование"));
            Console.WriteLine();
        }

        private static void SayHello(string name)
        {
            Console.WriteLine($"Привет, {name}!");
        }

        private static void PrintNumbers(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }

        private static void PrintArray(int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Console.Write($"{values[i]} ");
            }
            Console.WriteLine();
        }

        private static int Add(int a, int b)
        {
            return a + b;
        }

        private static int GetMax(int a, int b)
        {
            return a > b ? a : b;
        }

        private static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        private static int CountPositive(int[] values)
        {
            int count = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > 0)
                {
                    count++;
                }
            }

            return count;
        }

        private static double GetAverage(int[] values)
        {
            if (values.Length == 0)
            {
                return 0;
            }

            int sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }

            return (double)sum / values.Length;
        }

        private static int GetLastDigit(int number)
        {
            int positiveNumber = number < 0 ? -number : number;
            return positiveNumber % 10;
        }

        private static int ReadPrice()
        {
            return 120;
        }

        private static int ReadQuantity()
        {
            return 3;
        }

        private static int CalculateTotal(int price, int quantity)
        {
            return price * quantity;
        }

        private static void PrintReceipt(string productName, int price, int quantity, int total)
        {
            Console.WriteLine($"Товар: {productName}");
            Console.WriteLine($"Цена: {price}");
            Console.WriteLine($"Количество: {quantity}");
            Console.WriteLine($"Итог: {total}");
        }

        private static string ReadName()
        {
            return "Антон";
        }

        private static int ReadAge()
        {
            return 27;
        }

        private static string FormatProfile(string name, int age)
        {
            return $"Пользователь: {name}, возраст: {age}";
        }

        private static string ReverseString(string text)
        {
            string result = string.Empty;

            for (int i = text.Length - 1; i >= 0; i--)
            {
                result += text[i];
            }

            return result;
        }

        private static void PrintLessonTitle(string title)
        {
            Console.WriteLine(title);
            Console.WriteLine(new string('=', title.Length));
            Console.WriteLine();
        }

        private static void PrintTaskTitle(int taskNumber, string title)
        {
            Console.WriteLine($"{taskNumber}. {title}");
        }
    }
}
