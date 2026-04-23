namespace CshBook.Answers.Chapter1.Lesson13Switch
{
    internal static class AnswerLesson13Switch
    {
        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                case 12:
                    return "Декабрь";
                default:
                    return "Некорректный месяц";
            }
        }

        public static string GetGradeText(int grade)
        {
            switch (grade)
            {
                case 5:
                    return "Отлично";
                case 4:
                    return "Хорошо";
                case 3:
                    return "Удовлетворительно";
                case 2:
                    return "Неудовлетворительно";
                case 1:
                    return "Очень плохо";
                default:
                    return "Некорректная оценка";
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

        public static string GetDayType(int dayNumber)
        {
            switch (dayNumber)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    return "Рабочий день";
                case 6:
                case 7:
                    return "Выходной";
                default:
                    return "Некорректный номер дня";
            }
        }

        public static string GetRoomType(int code)
        {
            switch (code)
            {
                case 1:
                    return "Эконом";
                case 2:
                    return "Стандарт";
                case 3:
                    return "Комфорт";
                case 4:
                    return "Люкс";
                default:
                    return "Неизвестный тип номера";
            }
        }

        public static string GetCommandDescription(char command)
        {
            switch (char.ToUpper(command))
            {
                case 'W':
                    return "Движение вверх";
                case 'A':
                    return "Движение влево";
                case 'S':
                    return "Движение вниз";
                case 'D':
                    return "Движение вправо";
                case 'E':
                    return "Взаимодействие";
                default:
                    return "Неизвестная команда";
            }
        }

        public static string GetOrderStatus(int statusCode)
        {
            switch (statusCode)
            {
                case 1:
                    return "Заказ создан";
                case 2:
                    return "Заказ подтвержден";
                case 3:
                    return "Передан в доставку";
                case 4:
                    return "Доставлен";
                case 5:
                    return "Отменен";
                default:
                    return "Неизвестный статус";
            }
        }

        public static void Main_()
        {
            Console.WriteLine("Урок 13. Switch");
            Console.WriteLine("================");
            Console.WriteLine();

            Console.WriteLine("1. Название месяца");
            Console.WriteLine(GetMonthName(4));
            Console.WriteLine();

            Console.WriteLine("2. Текст по оценке");
            Console.WriteLine(GetGradeText(5));
            Console.WriteLine();

            Console.WriteLine("3. Короткое название дня");
            Console.WriteLine(GetShortDayName(6));
            Console.WriteLine();

            Console.WriteLine("4. Светофор");
            Console.WriteLine(GetTrafficLightAction("Желтый"));
            Console.WriteLine();

            Console.WriteLine("5. Мини-калькулятор");
            Console.WriteLine(Calculate(12, 5, '+'));
            Console.WriteLine(Calculate(12, 5, '/'));
            Console.WriteLine();

            Console.WriteLine("6. Время года");
            Console.WriteLine(GetSeason(10));
            Console.WriteLine();

            Console.WriteLine("7. Выходной или рабочий день");
            Console.WriteLine(GetDayType(7));
            Console.WriteLine();

            Console.WriteLine("8. Тип номера в отеле");
            Console.WriteLine(GetRoomType(3));
            Console.WriteLine();

            Console.WriteLine("9. Команда в игре");
            Console.WriteLine(GetCommandDescription('E'));
            Console.WriteLine();

            Console.WriteLine("10. Статус заказа");
            Console.WriteLine(GetOrderStatus(3));
            Console.WriteLine();
        }
    }
}
