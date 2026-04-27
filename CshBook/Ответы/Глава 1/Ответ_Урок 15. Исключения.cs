namespace CshBook.Answers.Chapter1.Lesson15ExceptionHandling
{
    internal static class AnswerLesson15ExceptionHandling
    {
        public static void SafeDivide(int left, int right)
        {
            try
            {
                Console.WriteLine(left / right);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Ошибка: деление на ноль.");
            }
        }

        public static void ParseNumber(string text)
        {
            try
            {
                int number = int.Parse(text);
                Console.WriteLine(number);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: строка не является числом.");
            }
        }

        public static void ParseNumberWithTryParse(string text)
        {
            if (int.TryParse(text, out int number))
            {
                Console.WriteLine(number);
            }
            else
            {
                Console.WriteLine("Ошибка: строка не является числом.");
            }
        }

        public static void PrintArrayItem(int[] numbers, int index)
        {
            try
            {
                Console.WriteLine(numbers[index]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Ошибка: индекс вне массива.");
            }
        }

        public static void ValidateAge(int age)
        {
            if (age < 0)
            {
                throw new ArgumentException("Возраст не может быть отрицательным.");
            }

            Console.WriteLine($"Возраст: {age}");
        }

        public static void ReadFile(string path)
        {
            try
            {
                string text = File.ReadAllText(path);
                Console.WriteLine(text);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ошибка: файл не найден.");
            }
        }

        public static string Calculate(double left, double right, char operation)
        {
            try
            {
                switch (operation)
                {
                    case '+':
                        return (left + right).ToString();
                    case '-':
                        return (left - right).ToString();
                    case '*':
                        return (left * right).ToString();
                    case '/':
                        if (right == 0)
                        {
                            throw new DivideByZeroException();
                        }

                        return (left / right).ToString("F2");
                    default:
                        throw new ArgumentException("Неизвестная операция.");
                }
            }
            catch (DivideByZeroException)
            {
                return "Ошибка: деление на ноль.";
            }
            catch (ArgumentException ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }

        public static string GetItemOrMessage(int[] numbers, int index)
        {
            try
            {
                return numbers[index].ToString();
            }
            catch (IndexOutOfRangeException)
            {
                return "Ошибка: индекс вне диапазона.";
            }
        }

        public static void WriteTextToFile(string path, string text)
        {
            StreamWriter? writer = null;

            try
            {
                writer = new StreamWriter(path);
                writer.WriteLine(text);
                Console.WriteLine("Текст записан.");
            }
            finally
            {
                writer?.Close();
                Console.WriteLine("Поток закрыт.");
            }
        }

        public static void ValidateDiscount(int discount)
        {
            if (discount < 0 || discount > 100)
            {
                throw new ArgumentException("Скидка должна быть от 0 до 100.");
            }

            Console.WriteLine($"Скидка: {discount}%");
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 15. Исключения");
            Console.WriteLine("===================");
            Console.WriteLine();

            Console.WriteLine("1. Деление с защитой");
            SafeDivide(10, 2);
            SafeDivide(10, 0);
            Console.WriteLine();

            Console.WriteLine("2. Преобразование строки в число");
            ParseNumber("42");
            ParseNumber("abc");
            Console.WriteLine();

            Console.WriteLine("3. TryParse без исключения");
            ParseNumberWithTryParse("55");
            ParseNumberWithTryParse("text");
            Console.WriteLine();

            Console.WriteLine("4. Доступ к элементу массива");
            PrintArrayItem(new int[] { 3, 5, 7 }, 1);
            PrintArrayItem(new int[] { 3, 5, 7 }, 10);
            Console.WriteLine();

            Console.WriteLine("5. Проверка возраста");
            try
            {
                ValidateAge(20);
                ValidateAge(-2);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();

            Console.WriteLine("6. Чтение файла");
            ReadFile("missing-file.txt");
            Console.WriteLine();

            Console.WriteLine("7. Калькулятор без падения");
            Console.WriteLine(Calculate(12, 3, '/'));
            Console.WriteLine(Calculate(12, 0, '/'));
            Console.WriteLine(Calculate(12, 3, '?'));
            Console.WriteLine();

            Console.WriteLine("8. Безопасный ввод индекса");
            Console.WriteLine(GetItemOrMessage(new int[] { 10, 20, 30 }, 2));
            Console.WriteLine(GetItemOrMessage(new int[] { 10, 20, 30 }, 9));
            Console.WriteLine();

            Console.WriteLine("9. finally на практике");
            WriteTextToFile("lesson15-answer.txt", "Пример записи.");
            Console.WriteLine();

            Console.WriteLine("10. Проверка скидки");
            try
            {
                ValidateDiscount(15);
                ValidateDiscount(150);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();

            Console.WriteLine("11. Безопасная цепочка");
            try
            {
                int number = int.Parse("25");
                ValidateAge(number);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
        }
    }
}
