namespace CshBook.Answers.Chapter2.Lesson28BoxingUnboxing
{
    internal static class AnswerLesson28BoxingUnboxing
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 28. Boxing и Unboxing");
            Console.WriteLine("==========================");
            Console.WriteLine();

            int age = 20;
            object boxedAge = age;
            int unboxedAge = (int)boxedAge;

            Console.WriteLine($"age: {age}");
            Console.WriteLine($"boxedAge: {boxedAge}");
            Console.WriteLine($"unboxedAge: {unboxedAge}");
            Console.WriteLine();

            age = 25;
            Console.WriteLine($"age после изменения: {age}");
            Console.WriteLine($"boxedAge хранит старую копию: {boxedAge}");
            Console.WriteLine();

            object[] values =
            {
                10,
                "hello",
                true,
                2.5
            };

            for (int i = 0; i < values.Length; i++)
            {
                object value = values[i];

                if (value is int number)
                {
                    Console.WriteLine($"int: {number * 2}");
                }
                else if (value is string text)
                {
                    Console.WriteLine($"string length: {text.Length}");
                }
                else if (value is bool flag)
                {
                    Console.WriteLine($"bool opposite: {!flag}");
                }
                else if (value is double doubleValue)
                {
                    Console.WriteLine($"double plus 0.5: {doubleValue + 0.5}");
                }
            }

            Console.WriteLine();

            PrintIfNumber(15);
            PrintIfNumber(3.14);
            PrintIfNumber("не число");
            Console.WriteLine();

            object valueForCast = 50;

            if (valueForCast is int checkedNumber)
            {
                int number = checkedNumber;
                Console.WriteLine($"Безопасно распаковали int: {number}");
            }

            object wrongValue = "50";

            if (wrongValue is int wrongNumber)
            {
                Console.WriteLine(wrongNumber);
            }
            else
            {
                Console.WriteLine("wrongValue не является int, приведение не выполняем.");
            }
        }

        static void PrintIfNumber(object value)
        {
            if (value is int intValue)
            {
                Console.WriteLine($"Число int: {intValue}");
            }
            else if (value is double doubleValue)
            {
                Console.WriteLine($"Число double: {doubleValue}");
            }
            else
            {
                Console.WriteLine("Это не число");
            }
        }
    }
}
