using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons._29
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о boxing и unboxing в C#:
     * - Что такое boxing (упаковка) и unboxing (распаковка)
     * - Когда происходит boxing и unboxing
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
            
            // Пример 6: Производительность boxing/unboxing
            Console.WriteLine("Пример 6: Сравнение производительности");
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
        
        // Метод для демонстрации разницы в производительности
        static void PerformanceTest()
        {
            const int iterations = 1000000;
            
            // Тест с boxing/unboxing
            DateTime start1 = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                object boxed = i;       // Boxing
                int unboxed = (int)boxed;   // Unboxing
            }
            TimeSpan elapsed1 = DateTime.Now - start1;
            
            // Тест без boxing/unboxing
            DateTime start2 = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                int value = i;          // Без boxing
                int copied = value;     // Без unboxing
            }
            TimeSpan elapsed2 = DateTime.Now - start2;
            
            Console.WriteLine($"Время с boxing/unboxing:    {elapsed1.TotalMilliseconds} мс");
            Console.WriteLine($"Время без boxing/unboxing:  {elapsed2.TotalMilliseconds} мс");
            Console.WriteLine($"Соотношение:                {elapsed1.TotalMilliseconds / elapsed2.TotalMilliseconds:F2}x");
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
          упакованным значением примитивного типа (int, double, bool и т.д.). Если да, то 
          распакуйте значение и выведите его вместе с типом.
        
        # Напишите программу, которая демонстрирует разницу в производительности между ArrayList 
          и List<int> при добавлении и извлечении большого количества целых чисел. Измерьте и 
          выведите время выполнения для обоих случаев.
        
        # Создайте структуру Money с полями Amount (decimal) и Currency (string). Реализуйте 
          интерфейсы IComparable и IComparable<Money>. Сравните производительность обеих 
          реализаций при сортировке массива объектов Money.
        
        # Разработайте метод, который принимает параметр типа object и пытается преобразовать 
          его в различные числовые типы (int, double, decimal) с использованием безопасных методов 
          преобразования. Обработайте все возможные исключения.
        
        # Создайте класс ObjectCache, который хранит обобщенную коллекцию объектов. 
          Реализуйте методы для добавления, получения и удаления объектов. Сравните два подхода: 
          хранение всех объектов как object и хранение с использованием обобщений.
    */
    #endregion
}
