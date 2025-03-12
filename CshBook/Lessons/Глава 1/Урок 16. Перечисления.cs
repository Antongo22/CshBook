using System;

namespace CshBook.Lessons
{
    /* Перечисления (Enum) в C#
    
       🔹 **Что такое перечисления?**
       
       Перечисления (`enum`) – это именованные наборы целочисленных констант. Они делают код более читаемым, заменяя "магические числа" осмысленными именами.

       Где использовать `enum`?
       - Фиксированные списки значений (например, дни недели, времена года, статусы заказов).
       - Улучшение читаемости кода (вместо `if (status == 1)`, используем `if (status == OrderStatus.Pending)`).
       - Предотвращение ошибок при передаче некорректных значений.

       Как работают `enum`?
       - По умолчанию значения имеют тип `int` и начинаются с `0`.
       - Можно задать свои числовые значения (`enum HttpStatus { OK = 200, NotFound = 404 }`).
       - Можно преобразовывать `enum` в число и обратно (`(int)EnumValue` и `(EnumType)number`).

       Базовые методы Enum
       В .NET `enum` имеет несколько встроенных методов, которые позволяют работать с ним:
       `Enum.GetValues(typeof(T))` – возвращает массив всех значений `enum`.
       `Enum.GetNames(typeof(T))` – возвращает массив строковых представлений `enum`.
       `Enum.IsDefined(typeof(T), value)` – проверяет, существует ли значение в `enum`.
       `Enum.Parse(typeof(T), "Value")` – преобразует строку в `enum`.
       `Enum.TryParse("Value", out T result)` – безопасный вариант `Parse`, не вызывает исключения.
       
       Что такое `[Flags]`
       - Позволяет использовать `enum` как набор битовых флагов (для прав доступа, настроек и т. д.).
       - Значения должны быть степенями двойки (`1, 2, 4, 8...`), чтобы можно было их комбинировать (`|`).
       - Проверять наличие флага можно через `HasFlag()` или битовые операции `&`.

       Атрибуты и `enum`
       Атрибуты (`[Flags]`, `[Obsolete]`, `[Description]`) позволяют добавлять метаданные. Например, `[Flags]` позволяет `enum` работать с битовыми операциями, а `[Obsolete]` помечает устаревшие значения.
    */

    internal static class SixteenthLesson
    {
        public static void Main()
        {
            Console.WriteLine("Демонстрация работы с перечислениями (enum)\n");

            // 1. Базовое использование enum
            UseBasicEnum();

            // 2. Преобразование enum в число и обратно
            ConvertEnum();

            // 3. Работа с методами Enum
            EnumMethods();

            // 4. Перечисление в switch
            UseEnumInSwitch();

            // 5. Перебор всех значений перечисления
            IterateOverEnum();

            // 6. Перечисления с флагами
            UseFlagsEnum();
        }

        /* Пример 1: Базовое использование перечислений */
        public static void UseBasicEnum()
        {
            DayOfWeek today = DayOfWeek.Wednesday;
            Console.WriteLine($"Сегодня: {today} ({(int)today})");
        }

        /* Пример 2: Преобразование enum в число и обратно */
        public static void ConvertEnum()
        {
            int dayNumber = 4;
            DayOfWeek day = (DayOfWeek)dayNumber;
            Console.WriteLine($"\nЧисло {dayNumber} соответствует дню: {day}");

            string dayString = "Monday";
            DayOfWeek parsedDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayString);
            Console.WriteLine($"Строка \"{dayString}\" преобразована в {parsedDay}");
        }

        /* Пример 3: Базовые методы Enum */
        public static void EnumMethods()
        {
            Console.WriteLine("\nВсе значения enum DayOfWeek:");
            foreach (DayOfWeek value in Enum.GetValues(typeof(DayOfWeek)))
            {
                Console.WriteLine($"{(int)value} - {value}");
            }

            Console.WriteLine("\nПроверка существования значения:");
            bool exists = Enum.IsDefined(typeof(DayOfWeek), 5);
            Console.WriteLine($"Значение 5 есть в enum DayOfWeek? {exists}");

            Console.WriteLine("\nПример TryParse:");
            if (Enum.TryParse("Friday", out DayOfWeek result))
            {
                Console.WriteLine($"Строка \"Friday\" успешно преобразована в {result}");
            }
        }

        /* Пример 4: Перечисления в switch */
        public static void UseEnumInSwitch()
        {
            Console.Write("\nВведите день недели (0-6): ");
            int dayNumber = int.Parse(Console.ReadLine());

            if (Enum.IsDefined(typeof(DayOfWeek), dayNumber))
            {
                DayOfWeek day = (DayOfWeek)dayNumber;

                switch (day)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        Console.WriteLine("Выходной день!");
                        break;
                    default:
                        Console.WriteLine("Рабочий день.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Некорректный день недели.");
            }
        }

        /* Пример 5: Перебор всех значений перечисления */
        public static void IterateOverEnum()
        {
            Console.WriteLine("\nДни недели:");
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                Console.WriteLine($"{(int)day} - {day}");
            }
        }

        /* Пример 6: Использование флагов в enum */
        public static void UseFlagsEnum()
        {
            UserRoles user = UserRoles.User | UserRoles.Moderator;
            Console.WriteLine($"\nРоли пользователя: {user}");

            if (user.HasFlag(UserRoles.Admin))
                Console.WriteLine("Пользователь - администратор.");

            if (user.HasFlag(UserRoles.Moderator))
                Console.WriteLine("Пользователь - модератор.");

            if (user.HasFlag(UserRoles.User))
                Console.WriteLine("Пользователь - обычный пользователь.");
        }
    }

    /* Перечисление для дней недели */
    enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    /* Флаговое перечисление для ролей пользователей */
    [Flags]
    enum UserRoles
    {
        None = 0,
        User = 1,
        Moderator = 2,
        Admin = 4
    }
}



/* Задания на практику:

    1. Создайте перечисление `Seasons` с элементами: `Winter`, `Spring`, `Summer`, `Autumn`.
    2. Напишите метод, который принимает `Seasons` и выводит пример месяца (например, `Winter → Декабрь`).
    3. Реализуйте перечисление `HttpStatusCode` с популярными кодами (например, `OK = 200`, `NotFound = 404`).
       Напишите метод, который принимает `HttpStatusCode` и возвращает его текстовое описание.
    4. Создайте `enum` с флагами `UserPermissions` (Чтение, Запись, Удаление) и реализуйте метод для проверки прав.
    5. Создайте `enum` `DayType` (Рабочий, Выходной) и метод, который определяет тип дня по `DayOfWeek`.
    6. Реализуйте `enum` `GameDifficulty` (Легко, Нормально, Сложно) и настройте параметры игры в зависимости от сложности.
    7. Создайте `enum` `TransportMode` (Автомобиль, Поезд, Самолет) и метод, возвращающий среднее время в пути.
    8. Реализуйте `enum` `TrafficLight` (Красный, Желтый, Зеленый) и метод, определяющий, можно ли двигаться.
    9. Создайте `enum` `CardSuit` (Черви, Бубны, Пики, Крести) и напишите метод для генерации случайной карты.
    10. Используйте `Enum.GetValues()` и `Enum.GetNames()`, чтобы вывести все значения произвольного перечисления.
*/
