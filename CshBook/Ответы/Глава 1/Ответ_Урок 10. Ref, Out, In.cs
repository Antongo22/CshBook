namespace CshBook.Answers.Chapter1.Lesson10RefOutIn
{
    internal static class AnswerLesson10RefOutIn
    {
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static void AddBonus(ref int value, int bonus)
        {
            value += bonus;
        }

        public static void MakePositive(ref int value)
        {
            if (value < 0)
            {
                value = -value;
            }
        }

        public static bool TryDivide(int a, int b, out int result)
        {
            if (b == 0)
            {
                result = 0;
                return false;
            }

            result = a / b;
            return true;
        }

        public static void GetMinMax(int a, int b, out int min, out int max)
        {
            if (a < b)
            {
                min = a;
                max = b;
            }
            else
            {
                min = b;
                max = a;
            }
        }

        public static bool TryParseAge(string text, out int age)
        {
            return int.TryParse(text, out age);
        }

        public static void ConvertRubles(int rubles, out int dollars, out int remainder)
        {
            int exchangeRate = 90;
            dollars = rubles / exchangeRate;
            remainder = rubles % exchangeRate;
        }

        public static void PrintNumber(in int value)
        {
            Console.WriteLine(value);
        }

        public static bool IsBetween(in int value, in int left, in int right)
        {
            return value >= left && value <= right;
        }

        public static void PrintPair(in int first, in int second)
        {
            Console.WriteLine($"Пара чисел: {first} и {second}");
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 10. Ref, Out, In");
            Console.WriteLine("=====================");
            Console.WriteLine();

            int first = 3;
            int second = 8;
            Swap(ref first, ref second);
            Console.WriteLine("1. Обмен значений");
            Console.WriteLine($"first = {first}, second = {second}");
            Console.WriteLine();

            int balance = 100;
            AddBonus(ref balance, 25);
            Console.WriteLine("2. Увеличение значения");
            Console.WriteLine(balance);
            Console.WriteLine();

            int number = -17;
            MakePositive(ref number);
            Console.WriteLine("3. Сделать число положительным");
            Console.WriteLine(number);
            Console.WriteLine();

            Console.WriteLine("4. Безопасное деление");
            bool divided = TryDivide(20, 5, out int divisionResult);
            Console.WriteLine($"Успех: {divided}, результат: {divisionResult}");
            Console.WriteLine();

            Console.WriteLine("5. Минимум и максимум");
            GetMinMax(14, 9, out int min, out int max);
            Console.WriteLine($"min = {min}, max = {max}");
            Console.WriteLine();

            Console.WriteLine("6. TryParse-обертка");
            bool parsed = TryParseAge("27", out int age);
            Console.WriteLine($"Успех: {parsed}, возраст: {age}");
            Console.WriteLine();

            Console.WriteLine("7. Курс валют");
            ConvertRubles(365, out int dollars, out int remainder);
            Console.WriteLine($"Доллары: {dollars}, остаток рублей: {remainder}");
            Console.WriteLine();

            Console.WriteLine("8. Чтение через in");
            int value = 50;
            PrintNumber(in value);
            Console.WriteLine();

            Console.WriteLine("9. Проверка диапазона через in");
            Console.WriteLine(IsBetween(7, 1, 10));
            Console.WriteLine();

            Console.WriteLine("10. Пара чисел");
            PrintPair(in first, in second);
            Console.WriteLine();
        }
    }
}
