using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CshBook.Lessons.Глава_3
{
    #region Теория
    /*
     * В этом уроке ты узнаешь об Extension методах (методах-расширениях) в C#:
     * 
     * - Что такое методы-расширения и зачем они нужны
     * - Как создавать собственные методы-расширения
     * - Особенности и ограничения методов-расширений
     * - Примеры практического использования
     * - Встроенные методы-расширения в .NET
     */

    /*
       Что такое методы-расширения и зачем они нужны
       ==========================================
       
       Extension методы (методы-расширения) - это специальные статические методы, 
       которые позволяют добавлять новую функциональность к существующим типам 
       без изменения их исходного кода или создания наследников.
       
       Основные преимущества методов-расширений:
       
       1. Расширение функциональности существующих типов, включая:
          - Типы из стандартной библиотеки .NET
          - Запечатанные классы (sealed)
          - Примитивные типы (int, string и т.д.)
          - Интерфейсы
          
       2. Организация кода - группировка связанных операций в отдельные классы
       
       3. Повышение читаемости кода - использование более выразительного, "цепочечного" 
          (fluent) синтаксиса
          
       4. Поддержка функционального стиля программирования
    */

    /*
       Как создавать собственные методы-расширения
       =======================================
       
       Для создания метода-расширения необходимо:
       
       1. Определить статический класс
       2. Создать статический метод в этом классе
       3. В качестве первого параметра указать расширяемый тип с ключевым словом this
       
       Синтаксис:
       
       public static class МояКлассРасширений
       {
           public static ТипРезультата МетодРасширения(this ТипДляРасширения объект, параметры...)
           {
               // реализация
           }
       }
       
       Использование:
       
       var объект = new ТипДляРасширения();
       var результат = объект.МетодРасширения(аргументы);
       
       При вызове метода-расширения первый параметр (с ключевым словом this) 
       не указывается в списке аргументов - он является объектом, для которого 
       вызывается метод.
    */

    /*
       Особенности и ограничения методов-расширений
       ========================================
       
       1. Метод-расширение всегда статический
       2. Первый параметр с ключевым словом this указывает тип, для которого создается расширение
       3. Класс, содержащий метод-расширение, также должен быть статическим
       4. Методы-расширения доступны только если в область видимости импортировано пространство имен, 
          в котором они определены
       5. Методы экземпляров класса имеют приоритет над методами-расширениями
       6. С помощью методов-расширений нельзя:
          - Переопределять существующие методы типа
          - Получать доступ к приватным полям и методам расширяемого типа
          - Добавлять новые поля, свойства или события к типу
    */

    /*
       Примеры практического использования
       ==============================
       
       Методы-расширения широко используются в реальных проектах:
       
       1. Расширения для строк:
           - Проверка формата (IsValidEmail, IsValidPhone)
           - Преобразование (ToCamelCase, ToSentenceCase)
           
       2. Расширения для коллекций:
           - Дополнительные операции (AddRange для всех коллекций)
           - Безопасный доступ (GetValueOrDefault для словарей)
           
       3. Расширения для преобразования типов:
           - ToEnum<T>(this string value)
           - TryConvert методы для безопасного преобразования
           
       4. Расширения для работы с датами:
           - StartOfMonth, EndOfMonth
           - IsWeekend, IsWorkDay
           
       5. Расширения для различных интерфейсов, позволяющие добавлять 
          общую функциональность всем их реализациям
    */

    /*
       Встроенные методы-расширения в .NET
       ===============================
       
       .NET Framework и .NET Core содержат множество встроенных методов-расширений:
       
       1. System.Linq - важнейший набор методов-расширений для работы с коллекциями:
          - Where, Select, OrderBy, GroupBy
          - Any, All, First, FirstOrDefault, Single
          - Aggregate, Sum, Average, Min, Max
          
       2. System.IO:
          - ReadLines, ReadAllBytes для StreamReader
          
       3. Entity Framework Core:
          - Include, ThenInclude для загрузки связанных сущностей
          
       4. ASP.NET Core:
          - UseStartup, UseMvc, UseAuthentication и другие методы для настройки приложения
          
       Если вы видите метод, который вызывается у объекта, но не найден в определении его класса,
       вероятно, это метод-расширение. Для поиска его определения исследуйте соответствующие импорты.
    */
    #endregion

    #region Примеры методов-расширений
    
    // Статический класс для хранения методов-расширений
    public static class StringExtensions
    {
        // Метод-расширение для проверки, является ли строка палиндромом
        public static bool IsPalindrome(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
                
            string normalized = str.ToLower().Replace(" ", "");
            
            for (int i = 0; i < normalized.Length / 2; i++)
            {
                if (normalized[i] != normalized[normalized.Length - i - 1])
                    return false;
            }
            
            return true;
        }
        
        // Метод-расширение для подсчета слов в строке
        public static int WordCount(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
                
            return str.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        
        // Метод-расширение для усечения строки с добавлением многоточия
        public static string Truncate(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
                return str;
                
            if (str.Length <= maxLength)
                return str;
                
            return str.Substring(0, maxLength - 3) + "...";
        }
    }
    
    // Методы-расширения для числовых типов
    public static class NumberExtensions
    {
        // Проверка, является ли число простым
        public static bool IsPrime(this int number)
        {
            if (number <= 1)
                return false;
                
            if (number == 2 || number == 3)
                return true;
                
            if (number % 2 == 0)
                return false;
                
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0)
                    return false;
            }
            
            return true;
        }
        
        // Получение факториала числа
        public static long Factorial(this int number)
        {
            if (number < 0)
                throw new ArgumentException("Факториал определен только для неотрицательных чисел");
                
            if (number == 0 || number == 1)
                return 1;
                
            long result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            
            return result;
        }
        
        // Метод для проверки, находится ли значение в диапазоне
        public static bool IsInRange(this int number, int min, int max)
        {
            return number >= min && number <= max;
        }
    }
    
    // Методы-расширения для коллекций
    public static class CollectionExtensions
    {
        // Безопасное получение элемента списка или значения по умолчанию
        public static T GetOrDefault<T>(this IList<T> list, int index, T defaultValue = default)
        {
            if (list == null || index < 0 || index >= list.Count)
                return defaultValue;
                
            return list[index];
        }
        
        // Безопасное получение значения из словаря
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, 
            TKey key, 
            TValue defaultValue = default)
        {
            if (dictionary == null || key == null || !dictionary.TryGetValue(key, out TValue value))
                return defaultValue;
                
            return value;
        }
        
        // Преобразование списка в читабельную строку
        public static string ToReadableString<T>(this IEnumerable<T> collection, string separator = ", ")
        {
            if (collection == null)
                return string.Empty;
                
            return string.Join(separator, collection);
        }
    }
    
    // Методы-расширения для работы с датами
    public static class DateTimeExtensions
    {
        // Получение начала месяца для указанной даты
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        
        // Получение конца месяца для указанной даты
        public static DateTime EndOfMonth(this DateTime date)
        {
            return date.StartOfMonth().AddMonths(1).AddDays(-1);
        }
        
        // Проверка, является ли дата выходным днем
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == System.DayOfWeek.Saturday || date.DayOfWeek == System.DayOfWeek.Sunday;
        }
    }
    
    // Методы-расширения для файлов
    public static class FileExtensions
    {
        // Получение размера файла в читабельном формате
        public static string GetReadableSize(this FileInfo file)
        {
            long size = file.Length;
            string[] units = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
            
            int unitIndex = 0;
            double fileSize = size;
            
            while (fileSize >= 1024 && unitIndex < units.Length - 1)
            {
                fileSize /= 1024;
                unitIndex++;
            }
            
            return $"{fileSize:0.##} {units[unitIndex]}";
        }
    }
    #endregion

    internal class ExtensionMethodsLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по методам-расширениям (Extension Methods) ====\n");
            
            // Демонстрация использования методов-расширений для строк
            Console.WriteLine("--- Методы-расширения для строк ---");
            
            string text = "Аргентина манит негра";
            Console.WriteLine($"Исходная строка: \"{text}\"");
            Console.WriteLine($"Это палиндром? {text.IsPalindrome()}");
            Console.WriteLine($"Количество слов: {text.WordCount()}");
            
            string longText = "Это очень длинная строка, которую нужно усечь для отображения в ограниченном пространстве";
            Console.WriteLine($"Усечённая строка: \"{longText.Truncate(30)}\"");
            
            Console.WriteLine("\n--- Методы-расширения для чисел ---");
            
            int number = 17;
            Console.WriteLine($"Число {number} - простое? {number.IsPrime()}");
            
            number = 5;
            Console.WriteLine($"{number}! = {number.Factorial()}");
            
            number = 42;
            Console.WriteLine($"Число {number} находится в диапазоне [1-100]? {number.IsInRange(1, 100)}");
            Console.WriteLine($"Число {number} находится в диапазоне [50-100]? {number.IsInRange(50, 100)}");
            
            Console.WriteLine("\n--- Методы-расширения для коллекций ---");
            
            List<string> fruits = new List<string> { "Яблоко", "Банан", "Апельсин", "Груша" };
            Console.WriteLine($"Список фруктов: {fruits.ToReadableString()}");
            
            Console.WriteLine($"Элемент с индексом 1: {fruits.GetOrDefault(1)}");
            Console.WriteLine($"Элемент с индексом 10 (не существует): {fruits.GetOrDefault(10, "Фрукт не найден")}");
            
            Dictionary<string, int> ages = new Dictionary<string, int>
            {
                { "Анна", 25 },
                { "Иван", 30 },
                { "Мария", 22 }
            };
            
            Console.WriteLine($"Возраст Ивана: {ages.GetValueOrDefault("Иван")}");
            Console.WriteLine($"Возраст Сергея (нет в словаре): {ages.GetValueOrDefault("Сергей", -1)}");
            
            Console.WriteLine("\n--- Методы-расширения для дат ---");
            
            DateTime today = DateTime.Today;
            Console.WriteLine($"Сегодня: {today:d}");
            Console.WriteLine($"Сегодня выходной? {today.IsWeekend()}");
            Console.WriteLine($"Начало текущего месяца: {today.StartOfMonth():d}");
            Console.WriteLine($"Конец текущего месяца: {today.EndOfMonth():d}");
            
            Console.WriteLine("\n--- Стандартные методы-расширения LINQ ---");
            
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            // Фильтрация чисел (Where) - выбираем только четные
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            Console.WriteLine($"Четные числа: {evenNumbers.ToReadableString()}");
            
            // Трансформация (Select) - возведение в квадрат
            var squares = numbers.Select(n => n * n);
            Console.WriteLine($"Квадраты чисел: {squares.ToReadableString()}");
            
            // Комбинирование методов-расширений в цепочку
            var squaredEvenSum = numbers
                .Where(n => n % 2 == 0)  // Выбираем четные
                .Select(n => n * n)      // Возводим в квадрат
                .Sum();                  // Суммируем
                
            Console.WriteLine($"Сумма квадратов четных чисел: {squaredEvenSum}");
            
            Console.WriteLine("\n--- Практический пример: обработка файлов ---");
            
            // Используем метод-расширение для FileInfo из класса FileExtensions
            // Уже определенный выше в отдельном статическом классе
            
            try
            {
                string currentFile = System.Reflection.Assembly.GetExecutingAssembly().Location;
                FileInfo fileInfo = new FileInfo(currentFile);
                
                Console.WriteLine($"Текущий файл: {fileInfo.Name}");
                Console.WriteLine($"Размер в байтах: {fileInfo.Length}");
                Console.WriteLine($"Размер (читабельный): {fileInfo.GetReadableSize()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении информации о файле: {ex.Message}");
            }
            
            Console.WriteLine("\nКак видите, методы-расширения делают код более читаемым и выразительным!");
        }
    }

    #region Задачи
    /*
        # Создайте метод-расширение ToTitleCase для строк, который преобразует первую 
          букву каждого слова в строке в верхний регистр, а остальные буквы - в нижний.
          Пример: "привет мир" -> "Привет Мир"
        
        # Напишите метод-расширение Shuffle<T> для списков, который случайным образом 
          перемешивает элементы списка. Используйте класс Random.
        
        # Реализуйте метод-расширение IsValidEmail для строк, который проверяет, 
          является ли строка корректным email адресом. Используйте регулярные выражения.
    */
    #endregion
}
