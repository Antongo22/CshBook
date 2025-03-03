using System;

[Flags]
public enum UserPermissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Delete = 4
}

public enum Seasons
{
    Winter,
    Spring,
    Summer,
    Autumn
}

public enum HttpStatusCode
{
    OK = 200,
    NotFound = 404,
    BadRequest = 400,
    Unauthorized = 401
}

public enum DayType
{
    Workday,
    Weekend
}

public enum GameDifficulty
{
    Easy,
    Normal,
    Hard
}

public enum TransportMode
{
    Car,
    Train,
    Airplane
}

public enum TrafficLight
{
    Red,
    Yellow,
    Green
}

public enum CardSuit
{
    Hearts,
    Diamonds,
    Spades,
    Clubs
}

class EnumTasks
{
    // 1. Пример месяца по сезону
    public static string GetMonthBySeason(Seasons season)
    {
        return season switch
        {
            Seasons.Winter => "Декабрь",
            Seasons.Spring => "Март",
            Seasons.Summer => "Июнь",
            Seasons.Autumn => "Сентябрь",
            _ => "Неизвестный сезон"
        };
    }

    // 2. Получение описания HTTP-кода
    public static string GetHttpStatusDescription(HttpStatusCode code)
    {
        return code switch
        {
            HttpStatusCode.OK => "Успешный запрос",
            HttpStatusCode.NotFound => "Страница не найдена",
            HttpStatusCode.BadRequest => "Некорректный запрос",
            HttpStatusCode.Unauthorized => "Неавторизованный доступ",
            _ => "Неизвестный статус"
        };
    }

    // 3. Проверка прав пользователя
    public static bool HasPermission(UserPermissions user, UserPermissions required)
    {
        return (user & required) == required;
    }

    // 4. Определение типа дня по дню недели
    public static DayType GetDayType(DayOfWeek day)
    {
        return (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday) ? DayType.Weekend : DayType.Workday;
    }

    // 5. Настройка игры по сложности
    public static string ConfigureGame(GameDifficulty difficulty)
    {
        return difficulty switch
        {
            GameDifficulty.Easy => "Враги слабее, ресурсов больше.",
            GameDifficulty.Normal => "Стандартный уровень сложности.",
            GameDifficulty.Hard => "Мало ресурсов, враги сильнее!",
            _ => "Неизвестный уровень"
        };
    }

    // 6. Время в пути в зависимости от транспорта
    public static string GetTravelTime(TransportMode mode)
    {
        return mode switch
        {
            TransportMode.Car => "Среднее время: 3-5 часов",
            TransportMode.Train => "Среднее время: 2-4 часа",
            TransportMode.Airplane => "Среднее время: 1 час",
            _ => "Неизвестный транспорт"
        };
    }

    // 7. Действия светофора
    public static string GetTrafficLightAction(TrafficLight light)
    {
        return light switch
        {
            TrafficLight.Red => "Стоп!",
            TrafficLight.Yellow => "Приготовьтесь!",
            TrafficLight.Green => "Можно ехать!",
            _ => "Неизвестный сигнал"
        };
    }

    // 8. Генерация случайной карты
    public static string GetRandomCard()
    {
        Random rand = new();
        CardSuit suit = (CardSuit)rand.Next(0, 4);
        int rank = rand.Next(2, 15);
        string rankName = rank switch
        {
            11 => "Валет",
            12 => "Дама",
            13 => "Король",
            14 => "Туз",
            _ => rank.ToString()
        };
        return $"{rankName} {suit}";
    }

    // 9. Вывод всех значений enum
    public static void PrintEnumValues<T>() where T : Enum
    {
        Console.WriteLine($"Значения {typeof(T).Name}:");
        foreach (var name in Enum.GetNames(typeof(T)))
        {
            Console.WriteLine(name);
        }
        Console.WriteLine();
    }

    public static void Main()
    {
        Console.WriteLine("Пример месяца по сезону: " + GetMonthBySeason(Seasons.Spring));
        Console.WriteLine("Описание HTTP-кода 404: " + GetHttpStatusDescription(HttpStatusCode.NotFound));

        UserPermissions user = UserPermissions.Read | UserPermissions.Write;
        Console.WriteLine("Есть ли право на удаление? " + HasPermission(user, UserPermissions.Delete));

        Console.WriteLine("Тип дня для пятницы: " + GetDayType(DayOfWeek.Friday));
        Console.WriteLine("Настройки игры для Hard: " + ConfigureGame(GameDifficulty.Hard));

        Console.WriteLine("Время в пути на поезде: " + GetTravelTime(TransportMode.Train));
        Console.WriteLine("Сигнал светофора (Красный): " + GetTrafficLightAction(TrafficLight.Red));

        Console.WriteLine("Случайная карта: " + GetRandomCard());

        PrintEnumValues<Seasons>();
        PrintEnumValues<HttpStatusCode>();
    }
}
