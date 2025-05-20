using System;

namespace CshBook.Lessons
{
    internal static class ThirteenthLessonSolutions
    {
        // 1. Определить, является ли введенная буква гласной или согласной, используя when.
        public static string CheckVowelOrConsonant(char letter)
        {
            return letter switch
            {
                'a' or 'e' or 'i' or 'o' or 'u' or 'A' or 'E' or 'I' or 'O' or 'U' => "Гласная",
                _ when char.IsLetter(letter) => "Согласная",
                _ => "Не буква"
            };
        }

        // 2. Определить, является ли число четным, нечетным или нулем, используя when.
        public static string CheckNumberType(int number)
        {
            return number switch
            {
                0 => "Ноль",
                _ when number % 2 == 0 => "Чётное",
                _ => "Нечётное"
            };
        }

        // 3. Вывести название месяца по его номеру
        public static string GetMonthName(int month)
        {
            return month switch
            {
                1 => "Январь",
                2 => "Февраль",
                3 => "Март",
                4 => "Апрель",
                5 => "Май",
                6 => "Июнь",
                7 => "Июль",
                8 => "Август",
                9 => "Сентябрь",
                10 => "Октябрь",
                11 => "Ноябрь",
                12 => "Декабрь",
                _ => "Некорректный месяц"
            };
        }

        // 4. Определить, является ли символ цифрой, буквой или специальным знаком, используя when.
        public static string CheckCharacterType(char ch)
        {
            return ch switch
            {
                _ when char.IsDigit(ch) => "Цифра",
                _ when char.IsLetter(ch) => "Буква",
                _ => "Специальный символ"
            };
        }

        // 5. Определить категорию возраста.
        public static string GetAgeCategory(int age)
        {
            return age switch
            {
                < 13 => "Ребенок",
                < 18 => "Подросток",
                < 60 => "Взрослый",
                _ => "Пенсионер"
            };
        }

        // 6. Определить результат математической операции (+, -, *, /) над двумя числамиn.
        public static double PerformOperation(double a, double b, char op)
        {
            return op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' when b != 0 => a / b,
                '/' => throw new ArgumentException("Деление на ноль недопустимо"),
                _ => throw new ArgumentException("Некорректный оператор")
            };
        }

        // 7. Преобразовать числовую оценку (1-5) в текстовое описание.
        public static string ConvertGradeToText(int grade)
        {
            return grade switch
            {
                5 => "Отлично",
                4 => "Хорошо",
                3 => "Удовлетворительно",
                2 => "Неудовлетворительно",
                _ => "Некорректная оценка"
            };
        }

        // 8. Определить пору года по номеру месяца, используя классический switch.
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
                    return "Некорректный месяц";
            }
        }

        // 9. Определить, является ли год високосным или обычным, используя классический switch.
        public static string IsLeapYear(int year)
        {
            switch (year % 400, year % 100, year % 4)
            {
                case (0, _, _): return "Високосный";  // Делится на 400 → високосный
                case (_, 0, _): return "Обычный";     // Делится на 100, но не на 400 → обычный
                case (_, _, 0): return "Високосный";  // Делится на 4, но не на 100 → високосный
                default: return "Обычный";            // Все остальные случаи → обычный
            }

        }

        // 10. Преобразовать день недели в его краткое обозначение, используя классический switch.
        public static string GetShortDayName(int day)
        {
            switch (day)
            {
                case 1: return "Пн";
                case 2: return "Вт";
                case 3: return "Ср";
                case 4: return "Чт";
                case 5: return "Пт";
                case 6: return "Сб";
                case 7: return "Вс";
                default: return "Некорректный день";
            }
        }

        // 11. Определить, является ли введенное число положительным, отрицательным или нулем, используя when.
        public static string CheckNumberSign(int number)
        {
            return number switch
            {
                > 0 => "Положительное",
                < 0 => "Отрицательное",
                _ => "Ноль"
            };
        }

        // 12. Определить, к какому классу животных относится животное, используя классический switch.
        public static string GetAnimalClass(string animal)
        {
            switch (animal.ToLower())
            {
                case "кот":
                case "собака":
                    return "Млекопитающее";
                case "орёл":
                case "воробей":
                    return "Птица";
                case "змея":
                case "ящерица":
                    return "Рептилия";
                default:
                    return "Неизвестный класс";
            }
        }

        // 13. Преобразовать числовую оценку в буквенную, используя when.
        public static string ConvertGradeToLetter(int grade)
        {
            return grade switch
            {
                >= 90 => "A",
                >= 80 => "B",
                >= 70 => "C",
                >= 60 => "D",
                _ => "F"
            };
        }

        // 14. Определить уровень заряда батареи, используя классический switch.
        public static string GetBatteryLevel(int charge)
        {
            switch (charge)
            {
                case >= 80: return "Высокий";
                case >= 40: return "Средний";
                default: return "Низкий";
            }
        }

        // 15. Определить, является ли введенный день выходным или рабочим, используя when.
        public static string IsWeekend(int day)
        {
            return day switch
            {
                6 or 7 => "Выходной",
                >= 1 and <= 5 => "Рабочий день",
                _ => "Некорректный день"
            };
        }


    }
}
