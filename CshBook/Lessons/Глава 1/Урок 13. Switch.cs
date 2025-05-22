using System;

namespace CshBook.Lessons
{
    /* Тринадцатый урок: Оператор switch
       
       Оператор switch используется для выбора одного из множества возможных вариантов.
       Он работает с целыми числами, строками, перечислениями и некоторыми другими типами данных.
    */

    internal static class ThirteenthLesson
    {
        /* Пример 1: Определение дня недели по номеру (классический switch) */
        public static string GetDayOfWeekClassic(int dayNumber)
        {
            switch (dayNumber)
            {
                case 1: return "Понедельник";
                case 2: return "Вторник";
                case 3: return "Среда";
                case 4: return "Четверг";
                case 5: return "Пятница";
                case 6: return "Суббота";
                case 7: return "Воскресенье";
                default: return "Некорректный номер дня";
            }
        }

        /* Пример 2: Определение типа символа с использованием when */
        public static string GetCharType(char symbol)
        {
            return symbol switch
            {
                char c when "aeiouAEIOU".Contains(c) => "Гласная буква",
                char c when "bcdfgBCDFG".Contains(c) => "Согласная буква",
                _ => "Неизвестный символ"
            };

            // Альтернативный вариант
            switch (symbol)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                    return "Гласная буква";
                case 'b':
                case 'c':
                case 'd':
                case 'f':
                case 'g':
                    return "Согласная буква";
                default:
                    return "Неизвестный символ";
            }
        }

        /* Пример 3: Определение сезона по месяцу с использованием when */
        public static string GetSeason(int month)
        {
            return month switch
            {
                int m when m == 12 || m == 1 || m == 2 => "Зима",
                int m when m >= 3 && m <= 5 => "Весна",
                int m when m >= 6 && m <= 8 => "Лето",
                int m when m >= 9 && m <= 11 => "Осень",
                _ => "Некорректный месяц"
            };

            // Альтернативный вариант

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

        public static void Main_()
        {
            Console.WriteLine(GetDayOfWeekClassic(3));
            Console.WriteLine(GetCharType('e'));
            Console.WriteLine(GetSeason(7));

            // Альтернатива в when
            object obj = "Hello";

            switch (obj)
            {
                case string s when s.Length > 5:
                    Console.WriteLine("Длинная строка");
                    break;
                case string s:
                    Console.WriteLine("Короткая строка");
                    break;
                case int i when i > 0:
                    Console.WriteLine("Положительное число");
                    break;
                case int i:
                    Console.WriteLine("Отрицательное число или ноль");
                    break;
                default:
                    Console.WriteLine("Неизвестный тип");
                    break;
            }

        }
    }
}

/* Задачи для отработки оператора switch

1. Определить, является ли введенная буква гласной или согласной, используя when.
2. Определить, является ли число четным, нечетным или нулем, используя when.
3. Вывести название месяца по его номеру.
4. Определить, является ли символ цифрой, буквой или специальным знаком, используя when.
5. Определить категорию возраста (ребенок, подросток, взрослый, пенсионер).
6. Определить результат математической операции (+, -, *, /).
7. Преобразовать числовую оценку (1-5) в текстовое описание ("Отлично", "Хорошо" и т.д.).
8. Определить пору года по номеру месяца, используя классический switch.
9. Определить, является ли год високосным или обычным, используя классический switch.
10. Преобразовать день недели в его краткое обозначение ("Пн", "Вт" и т.д.), используя классический switch.
11. Определить, является ли введенное число положительным, отрицательным или нулем, используя when.
12. Определить, к какому классу животных относится животное (млекопитающее, птица, рептилия и т.д.), используя классический switch.
13. Преобразовать числовую оценку в буквенную (A, B, C, D, F), используя when.
14. Определить уровень заряда батареи (низкий, средний, высокий), используя классический switch.
15. Определить, является ли введенный день выходным или рабочим, используя when.
*/
