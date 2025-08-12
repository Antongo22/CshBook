using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_3
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о делегатах в C#:
     * 
     * - Что такое делегаты и для чего они используются
     * - Синтаксис объявления и использования делегатов
     * - Многоадресные делегаты (Multicast delegates)
     * - Встроенные типы делегатов (Action, Func, Predicate)
     * - Анонимные методы и их связь с делегатами
     * - Преобразования делегатов
     */

    /*
       Что такое делегаты и для чего они используются
       ======================================
       
       Делегат — это тип, который представляет ссылку на метод с определённым списком
       параметров и возвращаемым значением. Можно рассматривать делегат как типизированный
       указатель на метод.
       
       Основные применения делегатов:
       
       1. Обратные вызовы (callbacks) - передача метода в качестве аргумента другому методу
       2. Обработка событий - регистрация методов, вызываемых при наступлении события
       3. Стратегии и алгоритмы - выбор разных реализаций поведения во время выполнения
       4. Асинхронное программирование - определение действий после завершения асинхронной операции
       
       Делегаты позволяют создавать более гибкий и модульный код, отделяя механизм
       выполнения от конкретных реализаций.
    */

    /*
       Синтаксис объявления и использования делегатов
       ======================================
       
       Объявление делегата:
       
       delegate возвращаемый_тип ИмяДелегата(параметры);
       
       Пример:
       
       public delegate int BinaryOperation(int x, int y);
       
       После объявления, делегат можно использовать для создания экземпляров,
       ссылающихся на методы с совместимой сигнатурой:
       
       BinaryOperation add = Add;
       
       где Add - это метод:
       
       public static int Add(int x, int y)
       {
           return x + y;
       }
       
       Вызов метода через делегат:
       
       int result = add(3, 4);  // результат будет 7
    */

    /*
       Многоадресные делегаты (Multicast delegates)
       ====================================
       
       Делегаты в C# могут содержать ссылки на несколько методов одновременно.
       Это называется многоадресными делегатами.
       
       Для добавления и удаления методов используются операторы += и -=:
       
       // Создаем делегаты, указывающие на отдельные методы
       Action printHello = PrintHello;
       Action printWorld = PrintWorld;
       
       // Создаем многоадресный делегат
       Action printBoth = printHello + printWorld;
       
       // Или так:
       Action printCombined = null;
       printCombined += PrintHello;
       printCombined += PrintWorld;
       
       // Удаление метода
       printCombined -= PrintWorld;
       
       При вызове многоадресного делегата методы выполняются в порядке добавления.
       Для делегатов, возвращающих значение, результатом будет результат последнего
       вызванного метода.
    */

    /*
       Встроенные типы делегатов (Action, Func, Predicate)
       =========================================
       
       В .NET есть готовые типы делегатов для распространённых случаев:
       
       1. Action - делегат для методов, не возвращающих значение (void):
          - Action - метод без параметров
          - Action<T> - метод с одним параметром
          - Action<T1, T2, ...> - метод с несколькими параметрами (до 16)
       
       2. Func - делегат для методов, возвращающих значение:
          - Func<TResult> - метод без параметров, возвращающий TResult
          - Func<T, TResult> - метод с одним параметром, возвращающий TResult
          - Func<T1, T2, ..., TResult> - метод с несколькими параметрами (до 16)
       
       3. Predicate<T> - специальный делегат для методов, принимающих T и возвращающих bool
          Эквивалентен Func<T, bool>
       
       Примеры:
       
       Action<string> print = Console.WriteLine;
       Func<int, int, int> add = (x, y) => x + y;
       Predicate<int> isEven = x => x % 2 == 0;
    */

    /*
       Анонимные методы и их связь с делегатами
       ==================================
       
       Анонимные методы — это методы без имени, которые можно использовать
       для создания делегатов на месте, без предварительного объявления метода:
       
       // Создание делегата с использованием анонимного метода
       BinaryOperation multiply = delegate(int x, int y) {
           return x * y;
       };
       
       // Использование
       int result = multiply(5, 3);  // результат будет 15
       
       С появлением лямбда-выражений в C# 3.0, анонимные методы используются реже:
       
       BinaryOperation multiply = (x, y) => x * y;
    */

    /*
       Преобразования делегатов
       ===================
       
       В C# можно выполнять преобразования между совместимыми типами делегатов:
       
       1. Ковариантность возвращаемого значения:
          - Метод, возвращающий производный тип, можно присвоить делегату, 
            требующему базовый тип.
            
       2. Контравариантность параметров:
          - Метод, принимающий базовый тип, можно присвоить делегату, 
            требующему производный тип.
       
       Пример:
       
       // Базовый и производный классы
       class Animal { }
       class Dog : Animal { }
       
       // Делегаты
       delegate Animal AnimalFactory(string name);
       delegate Dog DogFactory(string name);
       
       // Методы
       static Dog CreateDog(string name) { return new Dog(); }
       static Animal CreateAnimal(string name) { return new Animal(); }
       
       // Ковариантность (возвращаемое значение)
       AnimalFactory animalCreator = CreateDog;  // Dog -> Animal
       
       // Контравариантность (параметры)
       Action<Animal> animalAction = a => Console.WriteLine("Animal");
       Action<Dog> dogAction = animalAction;  // Animal -> Dog
    */

    /*
       Практические сценарии использования делегатов
       ====================================
       
       1. Обработка коллекций
          
          void ProcessItems<T>(IEnumerable<T> items, Action<T> processor)
          {
              foreach (T item in items)
              {
                  processor(item);
              }
          }
          
          // Использование
          ProcessItems(new[] { 1, 2, 3 }, x => Console.WriteLine(x * 2));
          
       2. Фильтрация
          
          List<T> Filter<T>(IEnumerable<T> items, Predicate<T> condition)
          {
              List<T> result = new List<T>();
              foreach (T item in items)
              {
                  if (condition(item))
                  {
                      result.Add(item);
                  }
              }
              return result;
          }
          
          // Использование
          var evenNumbers = Filter(new[] { 1, 2, 3, 4, 5 }, x => x % 2 == 0);
          
       3. Сортировка с пользовательским компаратором
          
          void Sort<T>(T[] array, Func<T, T, int> comparer)
          {
              // Реализация сортировки с использованием переданного компаратора
          }
          
          // Использование
          Sort(people, (p1, p2) => p1.Age.CompareTo(p2.Age));
    */
    #endregion

    #region Примеры использования делегатов

    // Объявление простого делегата
    public delegate int MathOperation(int a, int b);

    // Делегат с произвольным числом параметров
    public delegate void Logger(string format, params object[] args);

    // Делегат с возвращаемым значением и параметром по ссылке
    public delegate bool TryParser<T>(string input, out T result);

    // Класс с методами для демонстрации
    public class Calculator
    {
        // Методы для использования с делегатом MathOperation
        public static int Add(int a, int b) => a + b;
        public static int Subtract(int a, int b) => a - b;
        public static int Multiply(int a, int b) => a * b;
        public static int Divide(int a, int b) => b != 0 ? a / b : throw new DivideByZeroException();

        // Метод высшего порядка, принимающий делегат
        public static int[] ProcessArray(int[] array, MathOperation operation, int operand)
        {
            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = operation(array[i], operand);
            }
            return result;
        }
    }

    // Класс для обработки различных типов данных с делегатами
    public class DataProcessor
    {
        // Делегат для фильтрации данных
        public delegate bool DataFilter<T>(T item);

        // Метод для фильтрации коллекции с использованием делегата
        public static List<T> Filter<T>(IEnumerable<T> collection, DataFilter<T> filter)
        {
            List<T> result = new List<T>();
            foreach (T item in collection)
            {
                if (filter(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        // Метод для преобразования коллекции с использованием делегата
        public static List<TResult> Transform<TSource, TResult>(
            IEnumerable<TSource> collection, 
            Func<TSource, TResult> transformer)
        {
            List<TResult> result = new List<TResult>();
            foreach (TSource item in collection)
            {
                result.Add(transformer(item));
            }
            return result;
        }
    }

    // Класс для демонстрации цепочки обработки данных с делегатами
    public class ProcessingPipeline
    {
        private delegate string StringProcessor(string input);

        private static string ToUpperCase(string input) => input.ToUpper();
        private static string RemoveSpaces(string input) => input.Replace(" ", "");
        private static string AddPrefix(string input) => "Processed: " + input;

        public static string Process(string input)
        {
            // Создаем цепочку обработки
            StringProcessor pipeline = ToUpperCase;
            pipeline += RemoveSpaces;
            pipeline += AddPrefix;

            // Применяем все обработчики
            return pipeline(input);
        }
    }

    // Класс для демонстрации с делегатом для обратного вызова
    public class LongRunningOperation
    {
        // Делегат для оповещения о прогрессе
        public delegate void ProgressCallback(int percentComplete);

        public static void Execute(int iterations, ProgressCallback progressCallback)
        {
            for (int i = 0; i <= iterations; i++)
            {
                // Выполняем какую-то длительную работу
                System.Threading.Thread.Sleep(50); // Имитация работы

                // Вычисляем процент выполнения
                int percent = (i * 100) / iterations;

                // Вызываем делегат обратного вызова, если он не null
                progressCallback?.Invoke(percent);
            }
        }
    }

    // Простой класс для демонстрации
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString() => $"{Name}, {Age} лет";
    }
    #endregion

    internal class DelegatesLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по делегатам в C# ====\n");

            Console.WriteLine("--- Базовые операции с делегатами ---");

            // Создаем экземпляры делегатов, указывающие на статические методы
            MathOperation add = Calculator.Add;
            MathOperation subtract = Calculator.Subtract;
            MathOperation multiply = Calculator.Multiply;
            MathOperation divide = Calculator.Divide;

            // Вызываем методы через делегаты
            Console.WriteLine($"5 + 3 = {add(5, 3)}");
            Console.WriteLine($"5 - 3 = {subtract(5, 3)}");
            Console.WriteLine($"5 * 3 = {multiply(5, 3)}");
            Console.WriteLine($"6 / 3 = {divide(6, 3)}");

            // Обработка исключения при делении на ноль
            try
            {
                Console.WriteLine($"5 / 0 = {divide(5, 0)}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Ошибка: деление на ноль");
            }

            Console.WriteLine("\n--- Передача делегата как аргумента метода ---");

            int[] numbers = { 1, 2, 3, 4, 5 };
            
            // Передаем делегаты в метод высшего порядка
            int[] multipliedBy2 = Calculator.ProcessArray(numbers, multiply, 2);
            int[] added10 = Calculator.ProcessArray(numbers, add, 10);

            Console.WriteLine($"Исходный массив: {string.Join(", ", numbers)}");
            Console.WriteLine($"После умножения на 2: {string.Join(", ", multipliedBy2)}");
            Console.WriteLine($"После добавления 10: {string.Join(", ", added10)}");

            Console.WriteLine("\n--- Многоадресные делегаты ---");

            // Создаем делегат для логирования
            Logger log = LogToConsole;
            
            // Добавляем еще один метод к делегату
            log += LogWithTimestamp;
            
            // Вызываем оба метода через делегат
            log("Сообщение: {0}", "Привет, мир!");
            
            // Удаляем один из методов
            log -= LogToConsole;
            
            // Теперь будет вызван только LogWithTimestamp
            log("Сообщение: {0}", "Только с временем");

            Console.WriteLine("\n--- Встроенные типы делегатов ---");

            // Использование Action
            Action<string> printMessage = Console.WriteLine;
            printMessage("Привет через Action!");

            // Использование Func
            Func<int, int, int> addFunc = (a, b) => a + b;
            Console.WriteLine($"7 + 3 = {addFunc(7, 3)} (через Func)");

            // Использование Predicate
            Predicate<int> isEven = n => n % 2 == 0;
            Console.WriteLine($"4 четное? {isEven(4)}");
            Console.WriteLine($"5 четное? {isEven(5)}");

            Console.WriteLine("\n--- Фильтрация данных с делегатами ---");

            List<Person> people = new List<Person>
            {
                new Person("Анна", 25),
                new Person("Иван", 17),
                new Person("Мария", 30),
                new Person("Петр", 15),
                new Person("Елена", 22)
            };

            // Использование нашего делегата для фильтрации
            DataProcessor.DataFilter<Person> isAdult = p => p.Age >= 18;
            var adults = DataProcessor.Filter(people, isAdult);

            Console.WriteLine("Совершеннолетние люди:");
            foreach (var person in adults)
            {
                Console.WriteLine($"- {person}");
            }

            // Использование Func для преобразования
            var names = DataProcessor.Transform(people, p => p.Name);
            Console.WriteLine("\nТолько имена:");
            Console.WriteLine(string.Join(", ", names));

            Console.WriteLine("\n--- Цепочка обработки с делегатами ---");
            
            string originalText = "Привет, мир!";
            string processed = ProcessingPipeline.Process(originalText);
            
            Console.WriteLine($"Оригинальный текст: {originalText}");
            Console.WriteLine($"После обработки: {processed}");

            Console.WriteLine("\n--- Делегаты для обратных вызовов ---");
            
            // Используем делегат для отслеживания прогресса
            Console.WriteLine("Запуск операции с обратным вызовом:");
            LongRunningOperation.Execute(20, progress => {
                Console.Write($"\rВыполнено: {progress}%   ");
                if (progress == 100)
                {
                    Console.WriteLine("\nОперация завершена!");
                }
            });

            Console.WriteLine("\n--- Анонимные методы и лямбда-выражения ---");
            
            // Анонимный метод
            MathOperation powerAnonymous = delegate(int x, int y)
            {
                return (int)Math.Pow(x, y);
            };
            
            // Лямбда-выражение
            MathOperation powerLambda = (x, y) => (int)Math.Pow(x, y);
            
            Console.WriteLine($"2 в степени 3 (анонимный метод): {powerAnonymous(2, 3)}");
            Console.WriteLine($"2 в степени 3 (лямбда): {powerLambda(2, 3)}");

            Console.WriteLine("\n--- Делегаты в составе алгоритмов ---");
            
            // Сортировка объектов с использованием делегата для сравнения
            var peopleArray = people.ToArray();
            Array.Sort(peopleArray, (p1, p2) => p1.Age.CompareTo(p2.Age));
            
            Console.WriteLine("Люди, отсортированные по возрасту:");
            foreach (var person in peopleArray)
            {
                Console.WriteLine($"- {person}");
            }
            
            // Поиск с использованием делегата для условия
            var youngestAdult = Array.Find(peopleArray, p => p.Age >= 18);
            Console.WriteLine($"\nСамый молодой взрослый: {youngestAdult}");
        }

        // Методы для использования с делегатом Logger
        private static void LogToConsole(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        private static void LogWithTimestamp(string format, params object[] args)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {string.Format(format, args)}");
        }
    }

    #region Задачи
    /*
        # Создайте делегат StringModifier, который принимает строку и возвращает 
          модифицированную строку. Напишите несколько методов, соответствующих этому 
          делегату: ToUpperCase, ToLowerCase, ReverseString. Затем создайте метод 
          ProcessString, который принимает строку и массив делегатов StringModifier, 
          и последовательно применяет каждый делегат к строке.
        
        # Реализуйте класс Calculator с методами Add, Subtract, Multiply, Divide. Создайте
          делегат MathOperation, соответствующий этим методам. Напишите метод, который
          принимает два числа и делегат, выполняет операцию и выводит результат.
        
        # Создайте делегат ComparisonResult, который принимает два целых числа и возвращает
          результат их сравнения в виде строки ("Больше", "Меньше", "Равно"). Напишите
          несколько методов, соответствующих этому делегату. Затем создайте метод
          Compare, который использует этот делегат для сравнения чисел.
        
        # Создайте класс Student с полями Name и Grade. Напишите метод для сортировки 
          массива студентов по имени и по оценке с использованием делегатов. Создайте
          делегат для сравнения студентов и используйте его с методом Array.Sort.
        
        # Реализуйте простой калькулятор с динамическим выбором операции. Создайте
          Dictionary, где ключом будет строка (например, "+", "-", "*"), а значением - 
          делегат, соответствующий этой операции. Пользователь должен вводить два числа 
          и операцию, после чего программа находит нужный делегат в словаре и вызывает его.
    */
    #endregion
}
