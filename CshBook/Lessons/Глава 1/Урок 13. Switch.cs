namespace CshBook.Lessons.Chapter1.Lesson13Switch
{
    #region Теория
    /*
        switch нужен тогда, когда одна переменная
        сравнивается с несколькими конкретными вариантами.

        То есть вопрос обычно звучит так:
        "если значение вот такое - сделать одно,
        если другое - сделать второе".
     */

    /*
        Это особенно удобно в задачах вроде:

        - номер дня недели;
        - номер месяца;
        - код команды;
        - символ операции;
        - состояние светофора;
        - текст по числовой оценке.
     */

    /*
        Базовая форма:

        switch (value)
        {
            case 1:
                ...
                break;
            case 2:
                ...
                break;
            default:
                ...
                break;
        }

        case - отдельный вариант.
        default - запасная ветка на случай,
        если ни один вариант не подошел.
     */

    /*
        В C# удобно объединять несколько вариантов
        в одну ветку:

        case 12:
        case 1:
        case 2:
            return "Зима";

        Это полезно, когда несколько значений
        должны давать один и тот же результат.
     */

    /*
        Когда лучше брать switch:

        - когда проверяются точные значения;
        - когда вариантов много;
        - когда длинная цепочка if/else становится тяжелой для чтения.

        Когда лучше if/else:
        - когда проверяются диапазоны;
        - когда условия сложные и зависят от нескольких сравнений сразу.
     */

    /*
        На этом уроке держим фокус на классическом switch.
        Есть и более компактные современные формы,
        но сначала нужно уверенно читать и писать базовый вариант.
     */
    #endregion

    internal static class Lesson13Switch
    {
        public static string GetDayName(int dayNumber)
        {
            switch (dayNumber)
            {
                case 1:
                    return "Понедельник";
                case 2:
                    return "Вторник";
                case 3:
                    return "Среда";
                case 4:
                    return "Четверг";
                case 5:
                    return "Пятница";
                case 6:
                    return "Суббота";
                case 7:
                    return "Воскресенье";
                default:
                    return "Некорректный номер дня";
            }
        }

        public static string GetSeason(int month)
        {
            switch (month)
            {
                case 12:
                case 1:
                case 2:
                    return "Зима";
                case 3:
                case 4:
                case 5:
                    return "Весна";
                case 6:
                case 7:
                case 8:
                    return "Лето";
                case 9:
                case 10:
                case 11:
                    return "Осень";
                default:
                    return "Некорректный номер месяца";
            }
        }

        public static string Calculate(double left, double right, char operation)
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
                        return "На ноль делить нельзя";
                    }

                    return (left / right).ToString("F2");
                default:
                    return "Неизвестная операция";
            }
        }

        public static string GetTrafficLightAction(string color)
        {
            switch (color.ToLower())
            {
                case "красный":
                    return "Стой";
                case "желтый":
                    return "Приготовься";
                case "зеленый":
                    return "Можно идти";
                default:
                    return "Неизвестный сигнал";
            }
        }

        public static string GetShortDayName(int dayNumber)
        {
            switch (dayNumber)
            {
                case 1:
                    return "Пн";
                case 2:
                    return "Вт";
                case 3:
                    return "Ср";
                case 4:
                    return "Чт";
                case 5:
                    return "Пт";
                case 6:
                    return "Сб";
                case 7:
                    return "Вс";
                default:
                    return "Ошибка";
            }
        }

        public static void Main_()
        {
            Console.WriteLine(GetDayName(3));
            Console.WriteLine(GetSeason(7));
            Console.WriteLine(Calculate(10, 4, '/'));
            Console.WriteLine(GetTrafficLightAction("Желтый"));
            Console.WriteLine(GetShortDayName(6));
        }
    }

    #region Задачи
    /*
        Разминка

        1. Название месяца.
           Напиши метод GetMonthName(int month),
           который по номеру месяца возвращает его название.

        2. Текст по оценке.
           Напиши метод GetGradeText(int grade),
           который превращает оценку от 1 до 5 в текст.

        3. Короткое название дня.
           Напиши метод GetShortDayName(int dayNumber),
           который возвращает "Пн", "Вт", "Ср" и так далее.

        Основные задачи

        4. Светофор.
           Напиши метод GetTrafficLightAction(string color),
           который возвращает действие для пешехода.

        5. Мини-калькулятор.
           Напиши метод Calculate(double left, double right, char operation),
           который умеет работать с +, -, * и /.

        6. Время года.
           Напиши метод GetSeason(int month),
           который по номеру месяца возвращает время года.

        7. Выходной или рабочий день.
           Напиши метод GetDayType(int dayNumber),
           который возвращает "Рабочий день" или "Выходной".

        8. Тип номера в отеле.
           Напиши метод GetRoomType(int code),
           который по коду 1, 2, 3, 4 возвращает тип номера.

        Задачи на перенос

        9. Команда в игре.
           Напиши метод GetCommandDescription(char command),
           который описывает команды W, A, S, D, E.

        10. Статус заказа.
            Напиши метод GetOrderStatus(int statusCode),
            который по коду возвращает понятный статус заказа.
     */
    #endregion
}
