using System.Text.RegularExpressions;

namespace CshBook.Lessons.Chapter1.Lesson14RegularExpressions
{
    #region Теория
    /*
        Регулярные выражения нужны для работы со строками по шаблону.

        То есть мы не просто ищем точный текст,
        а описываем правило:

        - строка должна состоять только из цифр;
        - в тексте нужно найти все числа;
        - строка должна быть похожа на email;
        - пароль должен содержать буквы и цифры.
     */

    /*
        В C# для этого используется класс Regex.

        Чаще всего на старте нужны три операции:

        - Regex.IsMatch(...)  -> проверить, подходит ли строка под шаблон;
        - Regex.Matches(...)  -> найти все совпадения в тексте;
        - Regex.Replace(...)  -> заменить все совпадения.
     */

    /*
        Самые полезные обозначения на этом этапе:

        \d   - любая цифра
        \w   - буква, цифра или _
        \s   - пробел
        .    - любой символ
        +    - один или больше
        *    - ноль или больше
        ?    - ноль или один
        {n}  - ровно n раз
        {n,} - не меньше n раз
        []   - один символ из набора
        ^    - начало строки
        $    - конец строки
     */

    /*
        Например:

        ^\d{3}$

        Это значит:
        - начало строки;
        - ровно три цифры;
        - конец строки.

        То есть строка должна состоять только из трех цифр.
     */

    /*
        В регулярных выражениях символ \ часто встречается часто,
        поэтому шаблоны обычно пишут как verbatim-строки:

        @"\d+"

        Так удобнее читать и не нужно дублировать экранирование.
     */

    /*
        Важная методическая мысль:
        regex не заменяет понимание строк.

        Сначала попробуй словами сформулировать правило,
        которое ты хочешь проверить.
        И только потом записывай его шаблоном.

        Иначе регулярное выражение быстро превращается
        в набор непонятных символов.
     */

    /*
        На этом уроке держим только базу:

        - проверка формата;
        - поиск совпадений;
        - простая замена;
        - без тяжелых lookaround и других продвинутых конструкций.
     */
    #endregion

    internal static class Lesson14RegularExpressions
    {
        public static bool IsThreeDigits(string text)
        {
            return Regex.IsMatch(text, @"^\d{3}$");
        }

        public static bool IsEmail(string text)
        {
            return Regex.IsMatch(text, @"^[\w.-]+@[\w.-]+\.\w{2,}$");
        }

        public static MatchCollection FindNumbers(string text)
        {
            return Regex.Matches(text, @"\d+");
        }

        public static MatchCollection FindWordsWithCapitalLetter(string text)
        {
            return Regex.Matches(text, @"\b[А-ЯЁA-Z][а-яёa-zA-ZА-ЯЁ]*\b");
        }

        public static string HideDigits(string text)
        {
            return Regex.Replace(text, @"\d", "*");
        }

        public static void Main_()
        {
            Console.WriteLine($"\"123\" -> три цифры: {IsThreeDigits("123")}");
            Console.WriteLine($"\"12a\" -> три цифры: {IsThreeDigits("12a")}");

            Console.WriteLine("----");

            Console.WriteLine($"Email test@mail.com: {IsEmail("test@mail.com")}");
            Console.WriteLine($"Email test_mail: {IsEmail("test_mail")}");

            Console.WriteLine("----");

            string textWithNumbers = "Заказ 15, скидка 20 процентов, код 404.";
            Console.WriteLine("Числа в тексте:");
            MatchCollection numbers = FindNumbers(textWithNumbers);
            foreach (Match match in numbers)
            {
                Console.WriteLine(match.Value);
            }

            Console.WriteLine("----");

            string textWithNames = "Иван и Maria встретили Олега в Moscow.";
            Console.WriteLine("Слова с заглавной буквы:");
            MatchCollection words = FindWordsWithCapitalLetter(textWithNames);
            foreach (Match match in words)
            {
                Console.WriteLine(match.Value);
            }

            Console.WriteLine("----");

            Console.WriteLine(HideDigits("Телефон: 8 999 123 45 67"));
        }
    }

    #region Задачи
    /*
        Разминка

        1. Три цифры.
           Напиши метод IsThreeDigits(string text),
           который проверяет, состоит ли строка ровно из трех цифр.

        2. Поиск чисел.
           Напиши метод FindNumbers(string text),
           который находит в строке все числа.

        3. Слова с заглавной буквы.
           Напиши метод FindWordsWithCapitalLetter(string text),
           который находит все слова, начинающиеся с заглавной буквы.

        Основные задачи

        4. Проверка email.
           Напиши метод IsEmail(string text),
           который проверяет строку на простой формат email.

        5. Проверка телефона.
           Напиши метод IsPhoneNumber(string text),
           который проверяет формат +7(XXX)XXX-XX-XX.

        6. Проверка пароля.
           Напиши метод IsStrongPassword(string text),
           который проверяет пароль:
           минимум 8 символов, хотя бы одна буква и хотя бы одна цифра.

        7. Маскирование цифр.
           Напиши метод HideDigits(string text),
           который заменяет все цифры символом *.

        8. Поиск ссылок.
           Напиши метод FindUrls(string text),
           который находит ссылки, начинающиеся с http:// или https://.

        Задачи на перенос

        9. Проверка даты.
           Напиши метод ContainsDate(string text),
           который находит дату в формате dd-mm-yyyy.

        10. Проверка ИНН.
            Напиши метод IsInn(string text),
            который проверяет, состоит ли строка из 10 или 12 цифр.
     */
    #endregion
}
