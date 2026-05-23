namespace CshBook.Answers.Chapter2.Lesson20StaticClasses
{
    static class MathUtils
    {
        public static int Add(int left, int right)
        {
            return left + right;
        }

        public static int Subtract(int left, int right)
        {
            return left - right;
        }

        public static int Multiply(int left, int right)
        {
            return left * right;
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }
    }

    static class StringUtils
    {
        public static string Repeat(string text, int count)
        {
            string result = "";

            for (int i = 0; i < count; i++)
            {
                result += text;
            }

            return result;
        }
    }

    static class ArrayUtils
    {
        public static string Separator = "; ";

        public static int FindMax(int[] numbers)
        {
            int max = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        public static int Sum(int[] numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        public static string ConvertToString(int[] numbers)
        {
            string result = "";

            for (int i = 0; i < numbers.Length; i++)
            {
                result += numbers[i];

                if (i < numbers.Length - 1)
                {
                    result += Separator;
                }
            }

            return result;
        }
    }

    static class AppSettings
    {
        public static string AppName = "CshBook Demo";
        public static string Version = "1.0";
    }

    static class UserValidator
    {
        public static bool IsValidAge(int age)
        {
            return age >= 0 && age <= 120;
        }

        public static bool IsValidName(string name)
        {
            return name.Length >= 2;
        }
    }

    static class GeometryUtils
    {
        public static int GetRectangleArea(int width, int height)
        {
            return width * height;
        }

        public static double GetCircleArea(double radius)
        {
            return Math.PI * radius * radius;
        }
    }

    internal static class AnswerLesson20StaticClasses
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 20. Статические классы");
            Console.WriteLine("===========================");
            Console.WriteLine();

            Console.WriteLine("1-2. MathUtils");
            Console.WriteLine(MathUtils.Add(2, 3));
            Console.WriteLine(MathUtils.Subtract(10, 4));
            Console.WriteLine(MathUtils.Multiply(3, 5));
            Console.WriteLine(MathUtils.IsEven(8));
            Console.WriteLine();

            Console.WriteLine("3. StringUtils");
            Console.WriteLine(StringUtils.Repeat("Hi", 3));
            Console.WriteLine();

            Console.WriteLine("4-7. ArrayUtils");
            int[] numbers = { 4, 9, 2, 7 };
            Console.WriteLine(ArrayUtils.FindMax(numbers));
            Console.WriteLine(ArrayUtils.Sum(numbers));
            Console.WriteLine(ArrayUtils.ConvertToString(numbers));
            ArrayUtils.Separator = " | ";
            Console.WriteLine(ArrayUtils.ConvertToString(numbers));
            Console.WriteLine();

            Console.WriteLine("8. AppSettings");
            Console.WriteLine($"{AppSettings.AppName} {AppSettings.Version}");
            Console.WriteLine();

            Console.WriteLine("9. UserValidator");
            Console.WriteLine(UserValidator.IsValidAge(25));
            Console.WriteLine(UserValidator.IsValidName("A"));
            Console.WriteLine();

            Console.WriteLine("10. GeometryUtils");
            Console.WriteLine(GeometryUtils.GetRectangleArea(4, 5));
            Console.WriteLine(GeometryUtils.GetCircleArea(3).ToString("F2"));
            Console.WriteLine();
        }
    }
}
