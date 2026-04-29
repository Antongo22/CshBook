namespace CshBook.Answers.Chapter1.Lesson16Enumerations
{
    internal static class AnswerLesson16Enumerations
    {
        public enum Season
        {
            Winter,
            Spring,
            Summer,
            Autumn
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

        public enum TrafficLight
        {
            Red,
            Yellow,
            Green
        }

        public enum HttpStatusCode
        {
            Ok = 200,
            BadRequest = 400,
            NotFound = 404
        }

        public enum TransportMode
        {
            Car,
            Train,
            Airplane
        }

        [Flags]
        public enum UserPermission
        {
            None = 0,
            Read = 1,
            Write = 2,
            Delete = 4
        }

        public enum OrderStatus
        {
            New = 1,
            Paid = 2,
            InDelivery = 3,
            Delivered = 4
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

        public static DayType GetDayType(DayOfWeek day)
        {
            if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
            {
                return DayType.Weekend;
            }

            return DayType.Workday;
        }

        public static string GetDifficultyDescription(GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    return "Мягкий режим";
                case GameDifficulty.Normal:
                    return "Обычная сложность";
                case GameDifficulty.Hard:
                    return "Повышенная сложность";
                default:
                    return "Неизвестная сложность";
            }
        }

        public static string GetTrafficLightAction(TrafficLight light)
        {
            switch (light)
            {
                case TrafficLight.Red:
                    return "Стой";
                case TrafficLight.Yellow:
                    return "Приготовься";
                case TrafficLight.Green:
                    return "Можно идти";
                default:
                    return "Неизвестный сигнал";
            }
        }

        public static string GetHttpStatusDescription(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.Ok:
                    return "Успешный ответ";
                case HttpStatusCode.BadRequest:
                    return "Некорректный запрос";
                case HttpStatusCode.NotFound:
                    return "Ресурс не найден";
                default:
                    return "Неизвестный статус";
            }
        }

        public static bool HasPermission(UserPermission current, UserPermission required)
        {
            return (current & required) == required;
        }

        public static string GetTravelTime(TransportMode mode)
        {
            switch (mode)
            {
                case TransportMode.Car:
                    return "3-5 часов";
                case TransportMode.Train:
                    return "2-4 часа";
                case TransportMode.Airplane:
                    return "Около 1 часа";
                default:
                    return "Неизвестный транспорт";
            }
        }

        public static void PrintEnumValues<T>() where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"{Convert.ToInt32(value)} - {value}");
            }
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 16. Перечисления");
            Console.WriteLine("=====================");
            Console.WriteLine();

            Console.WriteLine("1. Времена года");
            Console.WriteLine(GetMonthExample(Season.Spring));
            Console.WriteLine();

            Console.WriteLine("2. Статус заказа");
            Console.WriteLine(GetOrderMessage(OrderStatus.Paid));
            Console.WriteLine();

            Console.WriteLine("3. Тип дня");
            Console.WriteLine(GetDayType(DayOfWeek.Saturday));
            Console.WriteLine();

            Console.WriteLine("4. Сложность игры");
            Console.WriteLine(GetDifficultyDescription(GameDifficulty.Hard));
            Console.WriteLine();

            Console.WriteLine("5. Светофор");
            Console.WriteLine(GetTrafficLightAction(TrafficLight.Green));
            Console.WriteLine();

            Console.WriteLine("6. HTTP-статусы");
            Console.WriteLine(GetHttpStatusDescription(HttpStatusCode.NotFound));
            Console.WriteLine();

            Console.WriteLine("7. Перебор значений");
            PrintEnumValues<Season>();
            Console.WriteLine();

            Console.WriteLine("8. Права пользователя");
            UserPermission permission = UserPermission.Read | UserPermission.Write;
            Console.WriteLine(HasPermission(permission, UserPermission.Write));
            Console.WriteLine(HasPermission(permission, UserPermission.Delete));
            Console.WriteLine();

            Console.WriteLine("9. Транспорт");
            Console.WriteLine(GetTravelTime(TransportMode.Train));
            Console.WriteLine();
        }
    }
}
