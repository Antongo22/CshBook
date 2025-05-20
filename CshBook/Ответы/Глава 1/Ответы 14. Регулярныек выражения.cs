using System;
using System.Text.RegularExpressions;

class RegexTasks
{
    // 1. Проверить, является ли строка номером телефона в формате "+7(XXX)XXX-XX-XX".
    public static bool IsValidPhoneNumber(string input)
    {
        string pattern = @"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$";
        return Regex.IsMatch(input, pattern);
    }

    // 2. Найти все слова в строке, начинающиеся с заглавной буквы.
    public static MatchCollection FindCapitalizedWords(string input)
    {
        string pattern = @"\b[A-ZА-ЯЁ][a-zа-яё]*\b";
        return Regex.Matches(input, pattern);
    }

    // 3. Проверить, является ли строка корректным паролем (минимум 8 символов, хотя бы одна цифра и одна буква).
    public static bool IsValidPassword(string input)
    {
        string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        return Regex.IsMatch(input, pattern);
    }

    // 4. Найти в тексте все email-адреса.
    public static MatchCollection FindEmails(string input)
    {
        string pattern = @"[\w.-]+@[\w.-]+\.\w{2,}";
        return Regex.Matches(input, pattern);
    }

    // 5. Проверить, содержит ли строка дату в формате "dd-mm-yyyy".
    public static bool ContainsDate(string input)
    {
        string pattern = @"\b(0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-\d{4}\b";
        return Regex.IsMatch(input, pattern);
    }

    // 6. Найти все ссылки (URL) в строке.
    public static MatchCollection FindUrls(string input)
    {
        string pattern = @"https?:\/\/[^\s]+";
        return Regex.Matches(input, pattern);
    }

    // 7. Проверить, является ли строка российским ИНН (10 или 12 цифр).
    public static bool IsValidINN(string input)
    {
        string pattern = @"^\d{10}(\d{2})?$";
        return Regex.IsMatch(input, pattern);
    }

    public static void Main()
    {
        Console.WriteLine("Проверка номера телефона: " + IsValidPhoneNumber("+7(123)456-78-90")); // true

        Console.WriteLine("\nСлова с заглавной буквы:");
        foreach (Match match in FindCapitalizedWords("Привет, это Иван и Мария"))
            Console.WriteLine(match.Value);

        Console.WriteLine("\nПроверка пароля:");
        Console.WriteLine(IsValidPassword("Password123")); // true
        Console.WriteLine(IsValidPassword("12345678")); // false

        Console.WriteLine("\nНайденные email-адреса:");
        foreach (Match match in FindEmails("Почта: example@mail.com, другой email: test123@gmail.com"))
            Console.WriteLine(match.Value);

        Console.WriteLine("\nПроверка наличия даты:");
        Console.WriteLine(ContainsDate("Сегодня 15-08-2023")); // true

        Console.WriteLine("\nНайденные ссылки:");
        foreach (Match match in FindUrls("Мой сайт: https://example.com и http://test.ru"))
            Console.WriteLine(match.Value);

        Console.WriteLine("\nПроверка ИНН:");
        Console.WriteLine(IsValidINN("1234567890")); // true
        Console.WriteLine(IsValidINN("123456789012")); // true
        Console.WriteLine(IsValidINN("12345")); // false
    }
}
