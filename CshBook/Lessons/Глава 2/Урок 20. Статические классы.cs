using System;
using System.Collections.Generic;

namespace CshBook.Lessons
{
    /* Урок 20: Статические классы в C#
     
    Статические классы - специальный тип классов, которые:
    - Не могут быть инстанцированы (нельзя создать объект через new)
    - Содержат только статические члены
    - Являются sealed по умолчанию (нельзя наследовать)
    - Часто используются для утилитарных функций и глобальных сервисов
     */

    #region Сравнение обычного и статического классов
    // Обычный класс
    class MathOperations
    {
        public int Add(int a, int b) => a + b;
    }

    // Статический класс
    static class StaticMathOperations
    {
        public static int Add(int a, int b) => a + b;
        public static int Multiply(int a, int b) => a * b;
    }
    #endregion

    #region Пример использования
    static class Logger
    {
        private static List<string> _logEntries = new List<string>();

        // Статический конструктор
        static Logger()
        {
            Console.WriteLine("Logger initialized");
        }

        public static void Log(string message)
        {
            string entry = $"{DateTime.Now:HH:mm:ss} | {message}";
            _logEntries.Add(entry);
            Console.WriteLine(entry);
        }

        public static void ShowLogHistory()
        {
            Console.WriteLine("\nИстория логов:");
            foreach (var entry in _logEntries)
            {
                Console.WriteLine(entry);
            }
        }
    }
    #endregion

    internal class TwentiethLesson
    {
        public static void Main_()
        {
            // Использование обычного класса
            MathOperations math = new MathOperations();
            Console.WriteLine(math.Add(5, 3)); // 8

            // Использование статического класса
            Console.WriteLine(StaticMathOperations.Multiply(5, 3)); // 15

            // Работа с логгером
            Logger.Log("Запуск приложения");
            Logger.Log("Выполнение операции");
            Logger.ShowLogHistory();

            // Попытка создать экземпляр приведёт к ошибке
            // Logger logger = new Logger(); // Ошибка компиляции
        }
    }

    /* Особенности статических классов:
     1. Не могут иметь конструкторов экземпляров
     2. Не поддерживают наследование
     3. Могут содержать статические конструкторы
     4. Все члены должны быть явно объявлены как static
     5. Идеальны для:
        - Утилитарных функций (Math, Convert)
        - Сервисов глобального доступа
        - Фабричных методов
        - Хранения глобальных настроек
     */

    #region Задание
    /* Создайте статический класс ArrayUtils с функционалом:
     - Метод FindMax(int[] array)
     - Метод Sort(int[] array)
     - Метод ConvertToString(int[] array)
     - Свойство Separator (для ConvertToString)
     - Статический конструктор с установкой Separator = "; "
     */

    
    #endregion


    /* Лучшие практики:
     1. Используйте для функционала, не требующего состояния
     2. Избегайте хранить изменяемые глобальные данные
     3. Хорошо подходят для extension-методов
     4. Не злоупотребляйте - избыток статики усложняет тестирование
     5. Используйте readonly для статических полей где возможно
     */

    #region Дополнительные примеры
    static class Geometry
    {
        public static double CircleArea(double radius) => Math.PI * radius * radius;
        public static double TriangleArea(double a, double h) => 0.5 * a * h;

        public static readonly double GoldenRatio = 1.618033988749894;
    }

    static class StringExtensions
    {
        public static string Reverse(this string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }
    #endregion
}