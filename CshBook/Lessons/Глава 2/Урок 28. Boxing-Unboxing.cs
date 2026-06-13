namespace CshBook.Lessons.Chapter2.Lesson28BoxingUnboxing
{
    #region Теория
    /*
        В C# есть значимые типы и ссылочные типы.

        int, double, bool, char, struct - значимые типы.
        class, string, object - ссылочные типы.

        Значимый тип хранит само значение.
        Ссылочный тип хранит ссылку на объект.
     */

    /*
        object - самый общий тип в C#.

        В переменную object можно положить почти любое значение:

        object value = 10;
        object text = "Привет";

        Но когда значимый тип попадает в object,
        происходит boxing.
     */

    /*
        Boxing - это упаковка значимого типа в object.

        Пример:

        int number = 10;
        object boxedNumber = number;

        C# берет значение 10 и упаковывает его в объект,
        чтобы с ним можно было работать как с object.
     */

    /*
        Unboxing - это обратная операция:
        достать значимый тип обратно из object.

        object boxedNumber = 10;
        int number = (int)boxedNumber;

        Распаковка требует явного приведения типа.
        Мы должны написать (int), потому что object сам по себе
        не знает, какой тип мы хотим получить.
     */

    /*
        Важная ошибка:

        object value = 10;
        double number = (double)value;

        Так нельзя.
        Внутри лежит именно int, а не double.

        Сначала нужно распаковать в правильный тип:

        int number = (int)value;
     */

    /*
        Чтобы не ошибиться с типом,
        можно использовать is:

        object value = 10;

        if (value is int number)
        {
            Console.WriteLine(number);
        }

        Такая запись сразу проверяет тип
        и создает переменную number нужного типа.
     */

    /*
        Boxing не нужно бояться в учебных примерах.

        Но важно понимать:
        если постоянно складывать значимые типы в object,
        программа делает лишнюю работу.

        Поэтому для обычного кода лучше использовать конкретные типы:
        int для чисел, string для строк, bool для логики.

        object нужен, когда тип заранее неизвестен
        или когда мы специально разбираем смешанные значения.
     */
    #endregion

    internal static class Lesson28BoxingUnboxing
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 28. Boxing и Unboxing");
            Console.WriteLine("==========================");
            Console.WriteLine();

            int number = 10;
            object boxedNumber = number;
            int unboxedNumber = (int)boxedNumber;

            Console.WriteLine($"Обычное число: {number}");
            Console.WriteLine($"Упакованное число как object: {boxedNumber}");
            Console.WriteLine($"Распакованное число: {unboxedNumber}");
            Console.WriteLine();

            number = 99;
            Console.WriteLine($"После изменения number: {number}");
            Console.WriteLine($"В boxedNumber осталась копия: {boxedNumber}");
            Console.WriteLine();

            object[] values =
            {
                15,
                "C#",
                true,
                7.5
            };

            for (int i = 0; i < values.Length; i++)
            {
                object value = values[i];

                if (value is int intValue)
                {
                    Console.WriteLine($"int: {intValue}, плюс 1 = {intValue + 1}");
                }
                else if (value is string text)
                {
                    Console.WriteLine($"string: {text}, длина = {text.Length}");
                }
                else if (value is bool flag)
                {
                    Console.WriteLine($"bool: {flag}, отрицание = {!flag}");
                }
                else if (value is double doubleValue)
                {
                    Console.WriteLine($"double: {doubleValue}");
                }
            }
        }
    }

    #region Задачи
    /*
        1. Создайте переменную int age = 20.
           Упакуйте ее в object.
           Распакуйте обратно в int и выведите результат.

        2. Измените age после упаковки.
           Выведите age и object-переменную.
           Объясните по выводу, почему в object осталась старая копия.

        3. Создайте object[] со значениями:
           10, "hello", true, 2.5.
           В цикле определите тип каждого значения через is.

        4. Если значение в массиве - int,
           выведите число, умноженное на 2.
           Если string - выведите длину строки.
           Если bool - выведите отрицание.
           Если double - выведите число плюс 0.5.

        5. Создайте метод PrintIfNumber(object value).
           Если внутри int - вывести число.
           Если внутри double - вывести число.
           Иначе вывести "Это не число".

        6. Создайте object value = 50.
           Проверьте через is, что внутри int,
           и только после этого распакуйте значение.

        7. Создайте object wrongValue = "50".
           Проверьте через is, что это не int,
           и не выполняйте приведение (int)wrongValue.
    */
    #endregion
}
