using System.Text.RegularExpressions;

namespace CshBook.Answers.Chapter1.Lesson14RegularExpressions
{
    internal static class AnswerLesson14RegularExpressions
    {
        public static bool IsThreeDigits(string text)
        {
            return Regex.IsMatch(text, @"^\d{3}$");
        }

        public static MatchCollection FindNumbers(string text)
        {
            return Regex.Matches(text, @"\d+");
        }

        public static MatchCollection FindWordsWithCapitalLetter(string text)
        {
            return Regex.Matches(text, @"\b[А-ЯЁA-Z][а-яёa-zA-ZА-ЯЁ]*\b");
        }

        public static bool IsEmail(string text)
        {
            return Regex.IsMatch(text, @"^[\w.-]+@[\w.-]+\.\w{2,}$");
        }

        public static bool IsPhoneNumber(string text)
        {
            return Regex.IsMatch(text, @"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$");
        }

        public static bool IsStrongPassword(string text)
        {
            return Regex.IsMatch(text, @"^(?=.*[A-Za-zА-Яа-яЁё])(?=.*\d).{8,}$");
        }

        public static string HideDigits(string text)
        {
            return Regex.Replace(text, @"\d", "*");
        }

        public static MatchCollection FindUrls(string text)
        {
            return Regex.Matches(text, @"https?:\/\/[^\s]+");
        }

        public static bool ContainsDate(string text)
        {
            return Regex.IsMatch(text, @"\b(0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-\d{4}\b");
        }

        public static bool IsInn(string text)
        {
            return Regex.IsMatch(text, @"^\d{10}(\d{2})?$");
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 14. Регулярные выражения");
            Console.WriteLine("==============================");
            Console.WriteLine();

            Console.WriteLine("1. Три цифры");
            Console.WriteLine(IsThreeDigits("123"));
            Console.WriteLine(IsThreeDigits("12a"));
            Console.WriteLine();

            Console.WriteLine("2. Поиск чисел");
            MatchCollection numbers = FindNumbers("Заказ 15, скидка 20, код 404.");
            foreach (Match match in numbers)
            {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            Console.WriteLine("3. Слова с заглавной буквы");
            MatchCollection words = FindWordsWithCapitalLetter("Иван встретил Maria и Олега.");
            foreach (Match match in words)
            {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            Console.WriteLine("4. Проверка email");
            Console.WriteLine(IsEmail("test@mail.com"));
            Console.WriteLine();

            Console.WriteLine("5. Проверка телефона");
            Console.WriteLine(IsPhoneNumber("+7(999)123-45-67"));
            Console.WriteLine();

            Console.WriteLine("6. Проверка пароля");
            Console.WriteLine(IsStrongPassword("Pass1234"));
            Console.WriteLine();

            Console.WriteLine("7. Маскирование цифр");
            Console.WriteLine(HideDigits("Код 1234, этаж 9"));
            Console.WriteLine();

            Console.WriteLine("8. Поиск ссылок");
            MatchCollection urls = FindUrls("Сайты: https://example.com и http://test.ru");
            foreach (Match match in urls)
            {
                Console.WriteLine(match.Value);
            }
            Console.WriteLine();

            Console.WriteLine("9. Проверка даты");
            Console.WriteLine(ContainsDate("Встреча 15-08-2025"));
            Console.WriteLine();

            Console.WriteLine("10. Проверка ИНН");
            Console.WriteLine(IsInn("1234567890"));
            Console.WriteLine(IsInn("123456789012"));
            Console.WriteLine();
        }
    }
}
