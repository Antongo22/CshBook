using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.L
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о лямбда-выражениях в C#:
     * 
     * - Что такое лямбда-выражения и зачем они нужны
     * - Синтаксис лямбда-выражений
     * - Замыкания (closures) в лямбдах
     * - Использование лямбда-выражений с делегатами
     * - Применение лямбда-выражений в LINQ
     * - Выражения деревьев выражений (Expression Trees)
     */

    /*
       Что такое лямбда-выражения и зачем они нужны
       ======================================
       
       Лямбда-выражения — это компактный способ представления анонимных функций,
       которые можно передавать в качестве аргументов методам, сохранять в переменных
       или возвращать из методов.
       
       Основные преимущества лямбда-выражений:
       
       1. Краткость и читаемость кода
       2. Упрощение работы с коллекциями (особенно в LINQ)
       3. Простая реализация обратных вызовов (callbacks)
       4. Улучшение функциональности делегатов
       5. Более ясное выражение намерений в коде
       
       Лямбды позволяют писать функциональный код в стиле, близком к математическому.
    */

    /*
       Синтаксис лямбда-выражений
       =====================
       
       Основной синтаксис лямбда-выражений в C#:
       
       (параметры) => выражение_или_блок_кода
       
       Где:
       - Параметры - список входных параметров (может быть пустым)
       - => - "стрелка", разделяющая параметры и тело лямбды
       - Выражение или блок кода - результат или действия лямбды
       
       Примеры:
       
       // Лямбда без параметров
       () => Console.WriteLine("Привет, мир!");
       
       // Лямбда с одним параметром (можно опустить скобки для одного параметра)
       x => x * x;
       
       // Лямбда с несколькими параметрами
       (x, y) => x + y;
       
       // Лямбда с блоком кода
       (x, y) => {
           int sum = x + y;
           return sum * sum;
       };
       
       // Лямбда с явным указанием типов параметров
       (int x, double y) => x + y;
    */

    /*
       Замыкания (closures) в лямбдах
       =========================
       
       Лямбда-выражения могут "захватывать" переменные из внешней области видимости.
       Это называется "замыканием" (closure).
       
       Пример:
       
       int factor = 10;
       Func<int, int> multiplier = x => x * factor;
       
       // Использование
       int result = multiplier(5);  // 5 * 10 = 50
       
       // Изменение захваченной переменной
       factor = 20;
       result = multiplier(5);  // 5 * 20 = 100
       
       При захвате переменных компилятор создаёт скрытый класс для хранения 
       этих переменных, что позволяет лямбда-выражению работать с ними даже после
       выхода из области видимости, где они были объявлены.
    */

    /*
       Использование лямбда-выражений с делегатами
       ===================================
       
       Лямбда-выражения часто используются вместе с делегатами:
       
       // Стандартные делегаты
       Func<int, int, int> add = (x, y) => x + y;
       Action<string> print = message => Console.WriteLine(message);
       Predicate<int> isEven = number => number % 2 == 0;
       
       // Использование
       int sum = add(3, 5);  // 8
       print("Привет!");     // Выводит "Привет!"
       bool result = isEven(4);  // true
       
       Основные стандартные делегаты:
       
       1. Action<T1, T2, ...> - делегат для методов, которые не возвращают значение
       2. Func<T1, T2, ..., TResult> - делегат для методов, возвращающих значение
       3. Predicate<T> - делегат для методов, возвращающих bool (эквивалентен Func<T, bool>)
    */

    /*
       Применение лямбда-выражений в LINQ
       ============================
       
       LINQ (Language Integrated Query) активно использует лямбда-выражения:
       
       var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
       
       // Фильтрация с помощью лямбды
       var evenNumbers = numbers.Where(n => n % 2 == 0);
       
       // Трансформация с помощью лямбды
       var squares = numbers.Select(n => n * n);
       
       // Проверка с помощью лямбды
       bool hasLargeNumbers = numbers.Any(n => n > 9);
       
       // Агрегация с помощью лямбды
       int sum = numbers.Sum(n => n * 2);
       
       // Сортировка с помощью лямбды
       var sortedPeople = people.OrderBy(p => p.LastName)
                                .ThenBy(p => p.FirstName);
    */

    /*
       Выражения деревьев выражений (Expression Trees)
       =======================================
       
       Лямбда-выражения могут быть преобразованы в деревья выражений,
       которые представляют код как структуру данных, а не исполняемый код.
       
       // Лямбда как исполняемый код
       Func<int, int, int> addFunc = (x, y) => x + y;
       
       // Лямбда как дерево выражений
       System.Linq.Expressions.Expression<Func<int, int, int>> addExpr = (x, y) => x + y;
       
       Деревья выражений используются:
       1. В ORM (Object-Relational Mapping) системах, таких как Entity Framework
       2. Для динамической генерации кода во время выполнения
       3. В библиотеках для метапрограммирования
       4. Для трансляции C# кода в другие языки (например, в SQL)
    */

    /*
       Лямбда-выражения в обработчиках событий
       =================================
       
       Лямбды упрощают регистрацию обработчиков событий:
       
       // Традиционный подход
       button.Click += new EventHandler(Button_Click);
       
       private void Button_Click(object sender, EventArgs e)
       {
           Console.WriteLine("Кнопка нажата!");
       }
       
       // С использованием лямбда-выражения
       button.Click += (sender, e) => Console.WriteLine("Кнопка нажата!");
    */

    /*
       Асинхронные лямбда-выражения
       ======================
       
       Начиная с C# 5.0, лямбда-выражения могут быть асинхронными:
       
       Func<Task> asyncLambda = async () => {
           await Task.Delay(1000);
           Console.WriteLine("Прошла 1 секунда");
       };
       
       // Использование
       await asyncLambda();
    */

    /*
       Рекомендации по использованию лямбда-выражений
       =====================================
       
       1. Используйте лямбды для простых, краткосрочных операций
       2. Предпочитайте именованные методы для сложной логики или повторно используемого кода
       3. Соблюдайте баланс между краткостью и читаемостью
       4. Будьте осторожны с захватом переменных в многопоточной среде
       5. Избегайте побочных эффектов в лямбдах, особенно в LINQ-запросах
       6. Предпочитайте более выразительный стиль для сложных преобразований
    */
    #endregion

    #region Примеры использования лямбд
    // Класс "Человек" для демонстрации
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        
        public Person(string firstName, string lastName, int age, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            City = city;
        }
        
        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age} лет, г. {City}";
        }
    }
    
    // Класс "Продукт" для демонстрации
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool InStock { get; set; }
        
        public Product(string name, decimal price, string category, bool inStock)
        {
            Name = name;
            Price = price;
            Category = category;
            InStock = inStock;
        }
        
        public override string ToString()
        {
            return $"{Name} ({Category}) - {Price:C} {(InStock ? "в наличии" : "нет в наличии")}";
        }
    }
    
    // Класс для демонстрации работы с событиями
    public class Button
    {
        public event EventHandler Click;
        
        public string Text { get; set; }
        
        public Button(string text)
        {
            Text = text;
        }
        
        public void PerformClick()
        {
            Console.WriteLine($"Кнопка '{Text}' нажата!");
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion

    internal class LambdasLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по лямбда-выражениям ====\n");
            
            Console.WriteLine("--- Основы лямбда-выражений ---");
            
            // Простые примеры лямбд
            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) => x + y;
            Func<int, int, int> max = (x, y) => x > y ? x : y;
            
            Console.WriteLine($"Квадрат 5: {square(5)}");
            Console.WriteLine($"Сумма 3 и 7: {add(3, 7)}");
            Console.WriteLine($"Максимум из 10 и 8: {max(10, 8)}");
            
            // Лямбда с блоком кода
            Func<int, int, int> calculatePower = (base_, exponent) => {
                int result = 1;
                for (int i = 0; i < exponent; i++)
                {
                    result *= base_;
                }
                return result;
            };
            
            Console.WriteLine($"2 в степени 3: {calculatePower(2, 3)}");
            
            // Action - лямбда без возвращаемого значения
            Action<string> greet = name => Console.WriteLine($"Привет, {name}!");
            greet("Иван");
            
            // Predicate - лямбда, возвращающая bool
            Predicate<int> isPositive = number => number > 0;
            Console.WriteLine($"5 положительное? {isPositive(5)}");
            Console.WriteLine($"-3 положительное? {isPositive(-3)}");
            
            Console.WriteLine("\n--- Замыкания в лямбдах ---");
            
            // Демонстрация замыкания
            int multiplier = 10;
            Func<int, int> multiply = x => x * multiplier;
            Console.WriteLine($"5 * {multiplier} = {multiply(5)}");
            
            multiplier = 20;
            Console.WriteLine($"После изменения множителя: 5 * {multiplier} = {multiply(5)}");
            
            // Создание счетчика с помощью замыкания
            Func<int> createCounter()
            {
                int count = 0;
                return () => ++count;
            }
            
            var counter1 = createCounter();
            var counter2 = createCounter();
            
            Console.WriteLine($"Счетчик 1: {counter1()}, {counter1()}, {counter1()}");
            Console.WriteLine($"Счетчик 2: {counter2()}, {counter2()}");
            
            Console.WriteLine("\n--- Лямбды с делегатами ---");
            
            // Использование стандартных делегатов
            Func<double, double, double> calculateArea = (width, height) => width * height;
            Action<int> printSquare = n => Console.WriteLine($"Квадрат {n} равен {n * n}");
            Predicate<string> isEmpty = s => string.IsNullOrWhiteSpace(s);
            
            Console.WriteLine($"Площадь прямоугольника 4x3: {calculateArea(4, 3)}");
            printSquare(6);
            Console.WriteLine($"Строка пустая? {isEmpty("")}, {isEmpty("текст")}");
            
            // Передача лямбды как параметра метода
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            // Поиск с использованием лямбды
            int foundNumber = Array.Find(numbers, n => n > 5 && n < 9);
            Console.WriteLine($"Найденное число между 5 и 9: {foundNumber}");
            
            // Фильтрация с использованием лямбды
            int[] evenNumbers = Array.FindAll(numbers, n => n % 2 == 0);
            Console.WriteLine($"Четные числа: {string.Join(", ", evenNumbers)}");
            
            Console.WriteLine("\n--- Лямбды в LINQ ---");
            
            // Создаем тестовые данные
            List<Person> people = new List<Person>
            {
                new Person("Анна", "Иванова", 25, "Москва"),
                new Person("Петр", "Петров", 35, "Санкт-Петербург"),
                new Person("Иван", "Сидоров", 17, "Москва"),
                new Person("Елена", "Смирнова", 42, "Казань"),
                new Person("Алексей", "Кузнецов", 30, "Санкт-Петербург")
            };
            
            // Фильтрация: люди старше 30 лет
            var olderThan30 = people.Where(p => p.Age > 30).ToList();
            Console.WriteLine("Люди старше 30 лет:");
            olderThan30.ForEach(p => Console.WriteLine($"- {p}"));
            
            // Сортировка по возрасту (по убыванию)
            var sortedByAge = people.OrderByDescending(p => p.Age).ToList();
            Console.WriteLine("\nЛюди, отсортированные по возрасту (по убыванию):");
            sortedByAge.ForEach(p => Console.WriteLine($"- {p}"));
            
            // Группировка по городу
            var groupedByCity = people.GroupBy(p => p.City);
            Console.WriteLine("\nЛюди, сгруппированные по городу:");
            foreach (var group in groupedByCity)
            {
                Console.WriteLine($"Город: {group.Key}");
                foreach (var person in group)
                {
                    Console.WriteLine($"  - {person.FirstName} {person.LastName}, {person.Age} лет");
                }
            }
            
            // Проекция: только имена
            var namesOnly = people.Select(p => $"{p.FirstName} {p.LastName}").ToList();
            Console.WriteLine("\nТолько имена:");
            Console.WriteLine(string.Join(", ", namesOnly));
            
            // Сложный запрос: молодые люди из Москвы, отсортированные по имени
            var youngMoscowites = people
                .Where(p => p.Age < 30 && p.City == "Москва")
                .OrderBy(p => p.FirstName)
                .ToList();
                
            Console.WriteLine("\nМолодые люди из Москвы (до 30 лет):");
            youngMoscowites.ForEach(p => Console.WriteLine($"- {p}"));
            
            Console.WriteLine("\n--- Лямбды в обработке событий ---");
            
            // Создаем кнопки с обработчиками событий на основе лямбд
            Button okButton = new Button("OK");
            Button cancelButton = new Button("Отмена");
            
            // Добавляем обработчики событий с использованием лямбд
            okButton.Click += (sender, e) => Console.WriteLine("Операция подтверждена");
            
            cancelButton.Click += (sender, e) => {
                Console.WriteLine("Операция отменена");
                Console.WriteLine("Возвращаемся к предыдущему шагу");
            };
            
            // Имитируем нажатия кнопок
            okButton.PerformClick();
            cancelButton.PerformClick();
            
            Console.WriteLine("\n--- Дополнительные примеры использования лямбд ---");
            
            List<Product> products = new List<Product>
            {
                new Product("Ноутбук", 65000, "Электроника", true),
                new Product("Смартфон", 35000, "Электроника", true),
                new Product("Наушники", 5000, "Аксессуары", false),
                new Product("Клавиатура", 3500, "Периферия", true),
                new Product("Монитор", 25000, "Периферия", true)
            };
            
            // Композиция предикатов с помощью лямбд
            Predicate<Product> isInStock = p => p.InStock;
            Predicate<Product> isExpensive = p => p.Price > 30000;
            Predicate<Product> isElectronics = p => p.Category == "Электроника";
            
            // Комбинирование предикатов для сложной фильтрации
            Predicate<Product> isPremiumElectronics = p => isInStock(p) && isExpensive(p) && isElectronics(p);
            
            Console.WriteLine("Премиальная электроника в наличии:");
            products
                .FindAll(isPremiumElectronics)
                .ForEach(p => Console.WriteLine($"- {p}"));
            
            // Создание преобразователей с помощью лямбд
            Func<decimal, decimal> applyDiscount = amount => amount * 0.9m;  // 10% скидка
            Func<decimal, decimal> addVAT = amount => amount * 1.2m;        // 20% НДС
            
            // Композиция функций
            Func<decimal, decimal> applyDiscountAndVAT = amount => addVAT(applyDiscount(amount));
            
            decimal originalPrice = 1000;
            Console.WriteLine($"\nИсходная цена: {originalPrice:C}");
            Console.WriteLine($"После скидки: {applyDiscount(originalPrice):C}");
            Console.WriteLine($"После скидки и НДС: {applyDiscountAndVAT(originalPrice):C}");
            
            // Создание фабричного метода с помощью лямбды
            Func<string, int, Person> createPerson = (name, age) => 
                new Person(name, "", age, "Неизвестен");
            
            var person1 = createPerson("Максим", 28);
            Console.WriteLine($"\nСозданный человек: {person1}");
        }
    }

    #region Задачи
    /*
        Напишите метод FindByName, принимающий коллекцию объектов Person и строку name,
          который возвращает первый объект с указанным именем. Используйте лямбда-выражение.
        
        Создайте метод SortProducts, принимающий коллекцию объектов Product и строку sortBy.
          В зависимости от значения sortBy ("name", "price", "category"), метод должен возвращать
          отсортированную коллекцию. Используйте лямбда-выражения и LINQ.
        
        Напишите метод GenerateSequence, который принимает два параметра: начальное значение start
          и функцию-преобразователь Func<int, int> transformer. Метод должен создавать и возвращать 
          массив из 10 чисел, где первый элемент равен start, а каждый следующий получается 
          применением функции transformer к предыдущему.
        
        Создайте метод ProcessItems<T>, который принимает массив элементов типа T и три делегата:
          - Predicate<T> filter - для фильтрации элементов
          - Func<T, R> converter - для преобразования элементов
          - Action<R> action - для выполнения действия с преобразованными элементами
          Метод должен применить фильтр, затем преобразовать каждый элемент и выполнить действие.

    */
    #endregion
}
