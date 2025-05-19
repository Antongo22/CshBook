using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CshBook.Lessons._29
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о boxing и unboxing в C#:
     * - Что такое boxing (упаковка) и unboxing (распаковка)
     * - Когда происходит boxing и unboxing
     * - Операторы is и as для безопасного приведения типов
     * - Влияние на производительность
     * - Как избежать ненужных операций boxing/unboxing
     * - Практические примеры использования
     */

    /*
       Что такое Boxing и Unboxing?
       ==========================
       
       Boxing (упаковка) - это процесс преобразования значимого типа (value type, например int, float, 
       struct) в ссылочный тип (reference type, то есть в object). При боксинге значение 
       помещается в объект System.Object и размещается в куче (heap), а не в стеке (stack).
       
       Unboxing (распаковка) - это обратная операция, то есть извлечение значимого типа из 
       объекта System.Object. При распаковке требуется явное приведение типов.
       
       Эти операции необходимы из-за двойственной природы системы типов в .NET:
       1. Значимые типы (value types) - хранят данные напрямую в стеке
       2. Ссылочные типы (reference types) - хранят в стеке ссылку на данные, размещенные в куче
    */

    /*
       Процесс Boxing (упаковки)
       ========================
       
       Когда происходит упаковка:
       
       int i = 123;         // Значимый тип в стеке
       object o = i;        // Упаковка: значение копируется в объект в куче
       
       При упаковке выполняются следующие шаги:
       1. Выделяется память в куче для нового объекта
       2. Значение копируется из стека в новый объект в куче
       3. Возвращается ссылка на этот объект
       
       Упаковка происходит автоматически в следующих случаях:
       - При явном приведении значимого типа к типу object
       - При передаче значимого типа в параметр с типом object
       - При присваивании значимого типа переменной интерфейсного типа, который этот тип реализует
    */

    /*
       Процесс Unboxing (распаковки)
       ===========================
       
       Когда происходит распаковка:
       
       object o = 123;      // Упаковка int в object
       int i = (int)o;      // Распаковка: извлечение значения из объекта
       
       При распаковке выполняются следующие шаги:
       1. Проверяется, что объект является упакованным значением нужного типа
       2. Значение копируется из объекта в куче обратно в стек
       
       Распаковка всегда требует явного приведения типов и может генерировать исключение
       InvalidCastException, если типы не совпадают.
    */

    /*
       Операторы is и as для безопасного приведения типов
       ==============================================
       
       При работе с боксингом и анбоксингом часто возникает необходимость проверить тип объекта 
       или безопасно привести его к другому типу. Для этого в C# существуют специальные операторы:
       
       1. Оператор is
       ---------------
       Оператор is проверяет, является ли объект экземпляром указанного типа, и возвращает
       булево значение (true или false):
       
       object boxed = 42;
       if (boxed is int)  // Вернет true, так как boxed содержит упакованный int
       {
           // Выполняется, если boxed содержит int
       }
       
       С C# 7.0 оператор is получил дополнительные возможности - сопоставление с образцом
       (pattern matching):
       
       if (boxed is int unboxedValue)  // Проверка и распаковка в одной операции
       {
           // unboxedValue уже содержит распакованное значение типа int
           // Это безопасный unboxing без явного приведения типов
       }
       
       2. Оператор as
       --------------
       Оператор as пытается преобразовать объект к указанному типу и возвращает null,
       если преобразование невозможно (вместо генерации исключения):
       
       object boxed = 42;
       int? unboxed = boxed as int?;  // Для значимых типов нужно использовать nullable версию
       
       // Более распространенный пример с ссылочными типами:
       object obj = "строка";
       string str = obj as string;  // Вернет "строка"
       
       object obj2 = 42;
       string str2 = obj2 as string;  // Вернет null, без исключения
       
       3. Связь с boxing/unboxing
       ------------------------
       Эти операторы особенно полезны при работе с упакованными значениями:
       
       - is позволяет безопасно проверить тип упакованного значения
       - as позволяет избежать исключений при распаковке (для ссылочных типов)
       - В современном C# сопоставление с образцом (boxed is int value) объединяет
         проверку и распаковку в один шаг
       
       Важно помнить, что оператор as работает только с ссылочными типами и nullable
       типами. Для обычных значимых типов нужно использовать явное приведение с предварительной
       проверкой через is.
    */

    /*
       Влияние на производительность
       ===========================
       
       Операции boxing и unboxing являются затратными по следующим причинам:
       
       1. Выделение памяти в куче - относительно медленная операция
       2. Дополнительное копирование данных
       3. Увеличение нагрузки на сборщик мусора
       4. Фрагментация памяти при интенсивном использовании
       
       В критических с точки зрения производительности участках кода следует 
       избегать ненужного boxing/unboxing.
    */

    /*
       Как избежать boxing/unboxing
       ==========================
       
       Способы избежать или минимизировать операции boxing/unboxing:
       
       1. Использование обобщений (Generics) вместо object
          List<int> вместо ArrayList
          
       2. Использование специализированных коллекций для примитивных типов
          
       3. Применение интерфейсов с обобщенными параметрами
          IComparable<T> вместо IComparable
          
       4. Использование структур и классов, работающих с нужными типами напрямую
       
       5. Применение параметров типа в методах, где это возможно
    */

    /*
       Практические ситуации, где происходит boxing/unboxing
       ==================================================
       
       1. Использование необобщенных коллекций (ArrayList, Hashtable)
       
       2. Строковая интерполяция и конкатенация со значимыми типами
          string s = "Значение: " + 42;  // 42 упаковывается в object
          
       3. Использование интерфейсов с value types
          IComparable comp = 42;  // int упаковывается при приведении к интерфейсу
          
       4. Использование params object[] в методах
          Console.WriteLine("{0}, {1}", 42, "text");  // 42 упаковывается
          
       5. Использование System.Collections.Generic уменьшает количество операций 
          boxing и unboxing по сравнению с System.Collections
    */
    
    #endregion

    public class BoxingUnboxingLesson
    {
        public static void Main_()
        {
            Console.WriteLine("=== Демонстрация Boxing и Unboxing ===\n");
            
            // Пример 1: Простой boxing и unboxing
            Console.WriteLine("Пример 1: Простой boxing и unboxing");
            int value = 42;
            object boxed = value;   // Boxing: значение int упаковывается в object
            
            Console.WriteLine($"Оригинальное значение: {value}");
            Console.WriteLine($"Упакованное значение: {boxed}");
            
            // Unboxing требует явного приведения типов
            int unboxed = (int)boxed;   // Unboxing: извлечение значения из object
            Console.WriteLine($"Распакованное значение: {unboxed}");
            
            // Проверка типов упакованного значения
            Console.WriteLine($"Тип упакованного значения: {boxed.GetType()}");
            Console.WriteLine($"Идентичны ли значения: {value == unboxed}");
            Console.WriteLine();
            
            // Пример 2: Boxing при использовании ArrayList (необобщенная коллекция)
            Console.WriteLine("Пример 2: Boxing при использовании ArrayList");
            ArrayList arrayList = new ArrayList();
            arrayList.Add(1);       // Boxing: int -> object
            arrayList.Add(2.5);     // Boxing: double -> object
            arrayList.Add(true);    // Boxing: bool -> object
            
            Console.WriteLine("Элементы ArrayList:");
            foreach (object item in arrayList)
            {
                Console.WriteLine($"  {item} (тип: {item.GetType().Name})");
            }
            
            // Unboxing при извлечении значений из ArrayList
            int firstItem = (int)arrayList[0];    // Unboxing
            double secondItem = (double)arrayList[1];  // Unboxing
            bool thirdItem = (bool)arrayList[2];  // Unboxing
            
            Console.WriteLine($"Первый элемент после распаковки: {firstItem}");
            Console.WriteLine();
            
            // Пример 3: Избегание boxing с использованием List<T> (generic коллекция)
            Console.WriteLine("Пример 3: Использование обобщений для избежания boxing");
            List<int> intList = new List<int>();
            intList.Add(1);     // Нет boxing, так как List<int> работает напрямую с int
            intList.Add(2);
            intList.Add(3);
            
            Console.WriteLine("Элементы List<int>:");
            foreach (int item in intList)
            {
                Console.WriteLine($"  {item}");
            }
            Console.WriteLine();
            
            // Пример 4: Boxing при работе с интерфейсами
            Console.WriteLine("Пример 4: Boxing при работе с интерфейсами");
            
            int number = 10;
            IComparable comparable = number;  // Boxing: int упаковывается для соответствия интерфейсу
            
            Console.WriteLine($"Интерфейсное значение: {comparable}");
            Console.WriteLine($"Результат сравнения с 5: {comparable.CompareTo(5)}");  // > 0, так как 10 > 5
            Console.WriteLine();
            
            // Избегание boxing с использованием обобщенного интерфейса
            IComparable<int> comparableWithoutBoxing = new BoxingFreeInt(10);
            Console.WriteLine($"Обобщенное значение: {comparableWithoutBoxing}");
            Console.WriteLine($"Результат сравнения с 5: {comparableWithoutBoxing.CompareTo(5)}");  // > 0
            Console.WriteLine();
            
            // Пример 5: Boxing при использовании params object[]
            Console.WriteLine("Пример 5: Boxing в методах с params object[]");
            DisplayValues(1, 2.5, "три", true);  // 1, 2.5 и true будут упакованы
            Console.WriteLine();
            
            // Пример 6: Использование операторов is и as
            Console.WriteLine("Пример 6: Использование операторов is и as для безопасного приведения типов");
            DemonstrateIsAndAs();
            Console.WriteLine();
            
            // Пример 7: Pattern matching с is (C# 7.0+)
            Console.WriteLine("Пример 7: Pattern matching с оператором is");
            DemonstratePatternMatching();
            Console.WriteLine();
            
            // Пример 8: Производительность boxing/unboxing
            Console.WriteLine("Пример 8: Сравнение производительности");
            PerformanceTest();
        }
        
        // Метод, демонстрирующий boxing с params
        static void DisplayValues(params object[] values)
        {
            Console.WriteLine("Вывод значений через params object[]:");
            foreach (var value in values)
            {
                Console.WriteLine($"  {value} (тип: {value.GetType().Name})");
            }
        }
        
        // Метод для демонстрации операторов is и as
        static void DemonstrateIsAndAs()
        {
            // Создадим несколько объектов разных типов
            object[] objects = new object[] { 42, "строка", 3.14, true, new DateTime(2025, 5, 19) };
            
            Console.WriteLine("Использование оператора is:");
            foreach (object obj in objects)
            {
                // Использование is для проверки типа
                if (obj is int)
                    Console.WriteLine($"  {obj} - это int");
                else if (obj is string)
                    Console.WriteLine($"  {obj} - это string");
                else if (obj is double)
                    Console.WriteLine($"  {obj} - это double");
                else if (obj is bool)
                    Console.WriteLine($"  {obj} - это bool");
                else
                    Console.WriteLine($"  {obj} - другой тип: {obj.GetType().Name}");
            }
            
            Console.WriteLine("\nБезопасное приведение типов с as (ссылочные типы):");
            
            object strObj = "Привет, мир!";
            // Безопасное приведение к string с помощью as
            string str = strObj as string;
            Console.WriteLine($"  Приведение строки: {(str != null ? str : "null")}");
            
            // Попытка привести int к string (не удастся)
            object intObj = 42;
            string strFromInt = intObj as string;
            Console.WriteLine($"  Приведение числа к строке: {(strFromInt != null ? strFromInt : "null (не удалось)")}\n");
            
            // Попытка привести значимый тип напрямую через as (не скомпилируется)
            // int? num = intObj as int; // Ошибка: Cannot convert type 'object' to 'int' via a reference conversion
            
            Console.WriteLine("Работа с Nullable типами и as:");
            object nullableInt = 42;
            int? extractedInt = nullableInt as int?;
            Console.WriteLine($"  Извлечение int? из object: {extractedInt?.ToString() ?? "null"}");
            
            // Сравнение безопасности приведения типов
            Console.WriteLine("\nСравнение безопасности приведения типов:");
            
            object wrongType = "не число";
            
            // Безопасный способ с is
            Console.WriteLine("  Использование is:");
            if (wrongType is int)
            {
                int extractedWithIs = (int)wrongType;
                Console.WriteLine($"    Значение: {extractedWithIs}");
            }
            else
            {
                Console.WriteLine("    Объект не является int, приведение не выполняется");
            }
            
            // Небезопасный способ с прямым приведением
            Console.WriteLine("  Прямое приведение типов (try-catch):");
            try
            {
                int extractedUnsafe = (int)wrongType;
                Console.WriteLine($"    Значение: {extractedUnsafe}");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"    Произошло исключение: {ex.Message}");
            }
        }
        
        // Метод для демонстрации pattern matching (сопоставления с образцом)
        static void DemonstratePatternMatching()
        {
            // Создадим массив с разными типами объектов
            object[] mixedObjects = new object[] { 10, "Hello", 3.14, true, null, new List<int>() { 1, 2, 3 } };
            
            Console.WriteLine("Pattern matching с оператором is (C# 7.0+):");
            foreach (object item in mixedObjects)
            {
                // Pattern matching для разных типов
                if (item is int value)  // Проверка типа и распаковка в одном выражении
                {
                    Console.WriteLine($"  Целое число: {value}, квадрат: {value * value}");
                }
                else if (item is string text) // Работает и для ссылочных типов
                {
                    Console.WriteLine($"  Строка: \"{text}\", длина: {text.Length}");
                }
                else if (item is double number)
                {
                    Console.WriteLine($"  Дробное число: {number:F2}, округлено: {Math.Round(number)}");
                }
                else if (item is bool flag)
                {
                    Console.WriteLine($"  Логическое значение: {flag}, отрицание: {!flag}");
                }
                else if (item is IList<int> list) // Работает с интерфейсами
                {
                    Console.WriteLine($"  Список: [{string.Join(", ", list)}], сумма: {list.Sum()}");
                }
                else if (item is null) // Проверка на null
                {
                    Console.WriteLine("  Значение null");
                }
                else
                {
                    Console.WriteLine($"  Неопознанный тип: {item.GetType().Name}");
                }
            }
            
            Console.WriteLine("\nSwitch с pattern matching (C# 8.0+):");
            foreach (object item in mixedObjects)
            {
                string result = item switch
                {
                    int i => $"Целое число: {i}",
                    string s => $"Строка: \"{s}\"",
                    double d => $"Дробное число: {d:F2}",
                    bool b => $"Логическое значение: {b}",
                    IList<int> list => $"Список с {list.Count} элементами",
                    null => "Значение null",
                    _ => $"Другой тип: {item.GetType().Name}"
                };
                
                Console.WriteLine($"  {result}");
            }
        }
        
        // Метод для демонстрации разницы в производительности
        static void PerformanceTest()
        {
            const int iterations = 1000000;
            
            // Тест с boxing/unboxing
            Stopwatch sw1 = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                object boxed = i;       // Boxing
                int unboxed = (int)boxed;   // Unboxing
            }
            sw1.Stop();
            TimeSpan elapsed1 = sw1.Elapsed;
            
            // Тест без boxing/unboxing
            Stopwatch sw2 = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                int value = i;          // Без boxing
                int copied = value;     // Без unboxing
            }
            sw2.Stop();
            TimeSpan elapsed2 = sw2.Elapsed;
            
            // Тест с использованием is и pattern matching
            Stopwatch sw3 = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                object boxed = i;       // Boxing
                if (boxed is int value) // Pattern matching с распаковкой
                {
                    int result = value;
                }
            }
            sw3.Stop();
            TimeSpan elapsed3 = sw3.Elapsed;
            
            // Вывод результатов
            Console.WriteLine($"Время с boxing/unboxing:            {elapsed1.TotalMilliseconds} мс");
            Console.WriteLine($"Время без boxing/unboxing:          {elapsed2.TotalMilliseconds} мс");
            Console.WriteLine($"Время с pattern matching:           {elapsed3.TotalMilliseconds} мс");
            Console.WriteLine($"Соотношение (boxing/обычный):      {elapsed1.TotalMilliseconds / elapsed2.TotalMilliseconds:F2}x");
            Console.WriteLine($"Соотношение (pattern/обычный):     {elapsed3.TotalMilliseconds / elapsed2.TotalMilliseconds:F2}x");
        }
    }
    
    // Класс для демонстрации избегания boxing с использованием обобщенного интерфейса
    public class BoxingFreeInt : IComparable<int>
    {
        private readonly int value;
        
        public BoxingFreeInt(int value)
        {
            this.value = value;
        }
        
        public int CompareTo(int other)
        {
            return value.CompareTo(other);  // Нет boxing
        }
        
        public override string ToString()
        {
            return value.ToString();
        }
    }
    
    // Пример структуры, которая требует boxing при использовании с необобщенными интерфейсами
    public struct Point : IComparable
    {
        public int X { get; }
        public int Y { get; }
        
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        // Этот метод вызывает boxing, когда Point используется как IComparable
        public int CompareTo(object obj)
        {
            if (obj is Point other)
            {
                // Сравнение по расстоянию от начала координат
                int thisDistance = X * X + Y * Y;
                int otherDistance = other.X * other.X + other.Y * other.Y;
                return thisDistance.CompareTo(otherDistance);
            }
            throw new ArgumentException("Object is not a Point");
        }
        
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
    
    // Пример той же структуры, но с обобщенным интерфейсом для избежания boxing
    public struct PointGeneric : IComparable<PointGeneric>
    {
        public int X { get; }
        public int Y { get; }
        
        public PointGeneric(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        // Этот метод не вызывает boxing
        public int CompareTo(PointGeneric other)
        {
            int thisDistance = X * X + Y * Y;
            int otherDistance = other.X * other.X + other.Y * other.Y;
            return thisDistance.CompareTo(otherDistance);
        }
        
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    #region Задачи
    /*
        # Создайте метод, который принимает параметр типа object и определяет, является ли он 
          упакованным значением примитивного типа (int, double, bool и т.д.). Используйте 
          операторы is и as для безопасного приведения типов. Выведите результат распаковки.
        
        # Напишите программу, которая демонстрирует разницу в производительности между ArrayList 
          и List<int> при добавлении и извлечении большого количества целых чисел. Измерьте и 
          выведите время выполнения для обоих случаев.
        
        # Создайте метод ProcessValue, который обрабатывает объект в зависимости от его типа:
          - Для целых чисел (int) - вычисляет факториал
          - Для строк - выводит длину и переворачивает строку
          - Для логических значений - выводит отрицание
          - Для дробных чисел - округляет до двух знаков после запятой
          Используйте pattern matching для реализации этой логики.
        
        # Создайте обобщенный класс SafeCast<T> с методом TryCast, который принимает object 
          и возвращает tuple (bool success, T value). Метод должен пытаться безопасно привести 
          объект к типу T. Если приведение невозможно, возвращается (false, default(T)).
          Продемонстрируйте работу с разными типами.
        
        # Разработайте иерархию классов Shape с подклассами Circle, Rectangle и Triangle.
          Создайте метод, который принимает object и проверяет, является ли он фигурой, и если да, 
          то вычисляет и возвращает площадь. Используйте операторы is, as и pattern matching.
          Сравните подходы с точки зрения читаемости и производительности.
    */
    #endregion
}
