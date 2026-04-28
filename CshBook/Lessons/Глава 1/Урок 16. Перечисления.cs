namespace CshBook.Lessons.Chapter1.Lesson16Enumerations
{
    #region Теория
    /*
        enum - это перечисление.

        Оно нужно тогда, когда у нас есть
        фиксированный набор возможных значений.

        Например:
        - времена года;
        - дни недели;
        - статус заказа;
        - уровень сложности;
        - сигнал светофора.
     */

    /*
        Без enum такие вещи часто хранят как числа:

        1 - новый заказ
        2 - в пути
        3 - доставлен

        Это неудобно, потому что числа сами по себе
        ничего не объясняют.

        enum делает код понятнее:
        OrderStatus.New
        OrderStatus.InDelivery
        OrderStatus.Delivered
     */

    /*
        По умолчанию элементы enum имеют числовые значения:

        enum Season
        {
            Winter,   // 0
            Spring,   // 1
            Summer,   // 2
            Autumn    // 3
        }

        При необходимости числа можно задать явно.
     */

    /*
        С enum обычно делают четыре вещи:

        - хранят значение;
        - сравнивают в if или switch;
        - преобразуют из числа или строки;
        - перебирают все значения через Enum.GetValues(...).
     */

    /*
        На этом уроке важнее всего понять идею:
        enum - это не "еще один сложный тип",
        а способ заменить магические числа
        понятными именами.
     */

    /*
        Флаги через [Flags] тоже полезны,
        но это уже следующий уровень.
        Здесь достаточно увидеть базовую идею:
        одно значение хранит набор прав или возможностей.
     */
    #endregion

    internal static class Lesson16Enumerations
    {
        public enum Season
        {
            Winter,
            Spring,
            Summer,
            Autumn
        }

        public enum OrderStatus
        {
            New = 1,
            Paid = 2,
            InDelivery = 3,
            Delivered = 4
        }

        [Flags]
        public enum UserPermission
        {
            None = 0,
            Read = 1,
            Write = 2,
            Delete = 4
        }

        public static string GetMonthExample(Season season)
        {
            switch (season)
            {
                case Season.Winter:
                    return "Декабрь";
                case Season.Spring:
                    return "Апрель";
                case Season.Summer:
                    return "Июль";
                case Season.Autumn:
                    return "Октябрь";
                default:
                    return "Неизвестный сезон";
            }
        }

        public static string GetOrderMessage(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.New:
                    return "Заказ создан";
                case OrderStatus.Paid:
                    return "Заказ оплачен";
                case OrderStatus.InDelivery:
                    return "Заказ в доставке";
                case OrderStatus.Delivered:
                    return "Заказ доставлен";
                default:
                    return "Неизвестный статус";
            }
        }

        public static void Main_()
        {
            Season season = Season.Summer;
            Console.WriteLine($"Сезон: {season}");
            Console.WriteLine($"Пример месяца: {GetMonthExample(season)}");

            Console.WriteLine("----");

            OrderStatus status = OrderStatus.Paid;
            Console.WriteLine($"Статус: {(int)status} -> {status}");
            Console.WriteLine(GetOrderMessage(status));

            Console.WriteLine("----");

            if (Enum.TryParse("Autumn", out Season parsedSeason))
            {
                Console.WriteLine($"Из строки получили: {parsedSeason}");
            }

            Console.WriteLine("----");

            Console.WriteLine("Все сезоны:");
            foreach (Season item in Enum.GetValues(typeof(Season)))
            {
                Console.WriteLine($"{(int)item} - {item}");
            }

            Console.WriteLine("----");

            UserPermission permission = UserPermission.Read | UserPermission.Write;
            Console.WriteLine($"Права: {permission}");
            Console.WriteLine($"Есть Write: {permission.HasFlag(UserPermission.Write)}");
            Console.WriteLine($"Есть Delete: {permission.HasFlag(UserPermission.Delete)}");
        }
    }

    #region Задачи
    /*
        Разминка

        1. Времена года.
           Создай enum Season с элементами Winter, Spring, Summer, Autumn.

        2. Пример месяца.
           Напиши метод GetMonthExample(Season season),
           который возвращает пример месяца для выбранного сезона.

        3. Статус заказа.
           Создай enum OrderStatus и метод GetOrderMessage(OrderStatus status),
           который возвращает текст по статусу.

        Основные задачи

        4. Тип дня.
           Создай enum DayType со значениями Workday и Weekend.
           Напиши метод, который по дню недели возвращает тип дня.

        5. Сложность игры.
           Создай enum GameDifficulty со значениями Easy, Normal, Hard.
           Напиши метод, который возвращает описание сложности.

        6. Светофор.
           Создай enum TrafficLight со значениями Red, Yellow, Green.
           Напиши метод, который возвращает действие для пешехода.

        7. HTTP-статусы.
           Создай enum HttpStatusCode с несколькими явными числовыми значениями,
           например 200, 400, 404.
           Напиши метод, который возвращает описание статуса.

        8. Перебор значений.
           Выведи все значения и имена любого enum через Enum.GetValues(...).

        Задачи на перенос

        9. Права пользователя.
           Создай [Flags] enum UserPermission с правами Read, Write, Delete.
           Напиши метод проверки наличия права.

        10. Транспорт.
            Создай enum TransportMode и метод,
            который возвращает пример времени в пути для каждого варианта.
     */
    #endregion
}
