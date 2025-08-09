using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_3
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о современных возможностях ООП в C#:
     * 
     * - Рекорды (records) - новый ссылочный тип в C# 9.0 и новее
     * - Первичные конструкторы (primary constructors)
     * - Сокращенный синтаксис инициализации свойств
     * - Позиционные записи (positional records)
     * - Неизменяемость (immutability) и методы with
     * - Pattern matching и его применение с рекордами
     */

    /*
       Рекорды (Records) в C#
       =================
       
       Рекорды (records) — это новый тип ссылочных типов в C# (начиная с C# 9.0), 
       который предназначен для представления неизменяемых данных.
       
       Основные особенности рекордов:
       
       1. Неизменяемость по умолчанию (immutability)
       2. Встроенные методы для сравнения содержимого (value-based equality)
       3. Улучшенный синтаксис для создания и работы с данными
       4. Удобный механизм неразрушающего изменения (non-destructive mutation)
       
       Синтаксис объявления рекорда:
       
       public record Person
       {
           public string FirstName { get; init; }
           public string LastName { get; init; }
       }
       
       Ключевое слово 'init' позволяет устанавливать значение только при инициализации.
    */

    /*
       Первичные конструкторы (Primary Constructors)
       =======================================
       
       Первичные конструкторы позволяют определить параметры конструктора прямо в объявлении класса/рекорда/структуры.
       
       Синтаксис для класса:
       
       public class Person(string firstName, string lastName)
       {
           public string FirstName { get; set; } = firstName;
           public string LastName { get; set; } = lastName;
       }
       
       Для рекордов синтаксис еще более компактный:
       
       public record Person(string FirstName, string LastName);
       
       В этой строке автоматически определяются:
       - Первичный конструктор с двумя параметрами
       - Свойства с модификатором init
       - Методы Equals(), GetHashCode(), ToString()
       - Деконструктор
       - Оператор == (сравнение по значениям свойств)
    */

    /*
       Сокращенный синтаксис инициализации свойств
       =====================================
       
       В современном C# добавлен более компактный способ объявления и инициализации свойств:
       
       public class Person
       {
           // Объявление свойства с инициализацией значением по умолчанию
           public string Name { get; set; } = "Unknown";
           
           // Свойство только для чтения с инициализацией
           public DateTime CreationDate { get; } = DateTime.Now;
           
           // Свойство с private setter и инициализацией
           public bool IsActive { get; private set; } = true;
       }
    */

    /*
       Позиционные рекорды (Positional Records)
       ==================================
       
       Позиционные рекорды позволяют создавать объекты и извлекать их значения по позициям:
       
       public record Point(int X, int Y);
       
       // Использование:
       var point = new Point(10, 20);
       
       // Деконструкция (распаковка значений)
       var (x, y) = point;
       
       Это удобно для кортежей и представления простых наборов данных.
    */

    /*
       Неизменяемость и метод with
       ======================
       
       Рекорды поддерживают неразрушающее изменение через оператор with:
       
       var person = new Person("Иван", "Иванов");
       
       // Создание копии с изменением свойства
       var updatedPerson = person with { FirstName = "Петр" };
       
       // Исходный объект остается неизменным
       // person.FirstName == "Иван"
       // updatedPerson.FirstName == "Петр"
    */

    /*
       Pattern matching и его применение с рекордами
       ======================================
       
       Pattern matching (сопоставление с образцом) - мощная функция, 
       особенно полезная в сочетании с рекордами:
       
       // Простое сопоставление с типом
       if (data is Person p)
       {
           Console.WriteLine($"Имя: {p.FirstName}");
       }
       
       // Сопоставление с условием
       if (data is Person { FirstName: "Иван" } ivan)
       {
           Console.WriteLine($"Нашли Ивана!");
       }
       
       // Сопоставление в switch
       object data = new Person("Иван", "Иванов");
       
       string result = data switch
       {
           Person { FirstName: "Иван" } p => $"Привет, {p.FirstName}!",
           Person p => $"Привет, {p.FirstName}!",
           _ => "Неизвестный объект"
       };
    */

    /*
       Инициализаторы объектов (Object Initializers)
       ======================================
       
       Инициализаторы объектов позволяют устанавливать свойства объекта сразу после его создания:
       
       var person = new Person
       {
           FirstName = "Иван",
           LastName = "Петров",
           Age = 30
       };
       
       Это сокращенный синтаксис для последовательного присваивания свойств.
    */

    /*
       Автоматически реализуемые свойства (Auto-Implemented Properties)
       ==========================================================
       
       Свойства с автоматической реализацией избавляют от необходимости 
       явно объявлять закрытое поле:
       
       // Вместо:
       private string _name;
       public string Name 
       { 
           get { return _name; } 
           set { _name = value; } 
       }
       
       // Можно писать:
       public string Name { get; set; }
       
       // С C# 6.0 появилась возможность инициализации:
       public string Name { get; set; } = "Unknown";
       
       // Свойство только для чтения:
       public string Id { get; } = Guid.NewGuid().ToString();
    */

    /*
       Выражения в членах тела (Expression-Bodied Members)
       ============================================
       
       Для коротких методов и свойств можно использовать выражения-тела:
       
       // Метод
       public int Add(int a, int b) => a + b;
       
       // Свойство
       public string FullName => $"{FirstName} {LastName}";
       
       // Конструктор (C# 7.0+)
       public Person(string name) => Name = name;
       
       // Деструктор (C# 7.0+)
       ~Person() => Console.WriteLine("Объект удален");
    */

    /*
       Деконструкторы (Deconstruction)
       =========================
       
       Деконструкция позволяет распаковывать свойства объекта в отдельные переменные:
       
       public void Deconstruct(out string firstName, out string lastName)
       {
           firstName = FirstName;
           lastName = LastName;
       }
       
       // Использование:
       var person = new Person("Иван", "Иванов");
       var (first, last) = person;  // Деконструкция
    */
    #endregion

    #region Примеры современных возможностей ООП

    // Традиционный класс для сравнения
    public class TraditionalPerson
    {
        private string _firstName;
        private string _lastName;
        private int _age;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public TraditionalPerson()
        {
            FirstName = "Unknown";
            LastName = "Unknown";
        }

        public TraditionalPerson(string firstName, string lastName, int age = 0)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age} лет";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            TraditionalPerson other = (TraditionalPerson)obj;
            return FirstName == other.FirstName && 
                   LastName == other.LastName && 
                   Age == other.Age;
        }

        public override int GetHashCode()
        {
            return (FirstName, LastName, Age).GetHashCode();
        }
    }

    // Современный класс с автосвойствами и первичным конструктором (C# 12.0+)
    public class ModernPerson(string firstName, string lastName, int age = 0)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public int Age { get; set; } = age;
        
        public string FullName => $"{FirstName} {LastName}";
        
        public void Deconstruct(out string firstName, out string lastName, out int age)
        {
            firstName = FirstName;
            lastName = LastName;
            age = Age;
        }
    }

    // Простой рекорд с позиционными параметрами
    public record PersonRecord(string FirstName, string LastName, int Age = 0)
    {
        // Дополнительные члены можно добавлять
        public string FullName => $"{FirstName} {LastName}";
        
        // Можно переопределять методы, автоматически генерируемые для рекорда
        public override string ToString()
        {
            return $"Запись: {FirstName} {LastName}, {Age} лет";
        }
    }

    // Рекорд с приватными сеттерами и дополнительной логикой
    public record Employee
    {
        // Свойства с инициализацией
        public string Id { get; } = Guid.NewGuid().ToString();
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Department { get; init; } = "General";
        public decimal Salary { get; private set; } = 0;
        public DateTime HireDate { get; init; } = DateTime.Now;

        // Вычисляемое свойство
        public string FullName => $"{FirstName} {LastName}";
        
        // Рекорды могут иметь методы
        public void IncreaseSalary(decimal amount)
        {
            Salary += amount;
        }
        
        // И конструкторы
        public Employee(string firstName, string lastName, string department, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            Salary = salary;
        }
        
        // Конструктор без параметров
        public Employee() { }
    }

    // Демонстрация наследования для рекордов
    public record Manager(string FirstName, string LastName, string Department, decimal Salary, int TeamSize) 
        : PersonRecord(FirstName, LastName)
    {
        // Дополнительные члены для производного рекорда
        public decimal Bonus { get; init; } = 0;
        public decimal TotalCompensation => Salary + Bonus;
    }

    // Структура с первичным конструктором (C# 12.0+)
    public struct Point(double x, double y)
    {
        public double X { get; } = x;
        public double Y { get; } = y;
        
        public double Distance => Math.Sqrt(X * X + Y * Y);
        
        public override string ToString() => $"({X}, {Y})";
    }
    #endregion

    internal class ModernOOPLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по современным возможностям ООП в C# ====\n");
            
            Console.WriteLine("--- Сравнение традиционного и современного подходов ---");
            
            // Традиционный подход
            var traditionalPerson = new TraditionalPerson("Иван", "Иванов", 30);
            Console.WriteLine($"Традиционный класс: {traditionalPerson}");
            
            // Современный класс с первичным конструктором
            var modernPerson = new ModernPerson("Иван", "Иванов", 30);
            modernPerson.FirstName = "Петр"; // Можно изменять свойства
            Console.WriteLine($"Современный класс: {modernPerson.FullName}, {modernPerson.Age} лет");
            
            // Деконструкция объекта
            var (firstName, lastName, age) = modernPerson;
            Console.WriteLine($"После деконструкции: {firstName}, {lastName}, {age}");
            
            Console.WriteLine("\n--- Работа с рекордами ---");
            
            // Создание рекорда
            var person1 = new PersonRecord("Анна", "Смирнова", 25);
            Console.WriteLine($"Рекорд: {person1}");
            
            // Создание копии с изменением (неразрушающая мутация)
            var person2 = person1 with { LastName = "Петрова" };
            Console.WriteLine($"Исходный рекорд: {person1}");
            Console.WriteLine($"Измененная копия: {person2}");
            
            // Сравнение рекордов (по значению)
            var person3 = new PersonRecord("Анна", "Смирнова", 25);
            Console.WriteLine($"person1 == person3: {person1 == person3}"); // True
            Console.WriteLine($"person1 == person2: {person1 == person2}"); // False
            
            Console.WriteLine("\n--- Рекорд с дополнительной логикой ---");
            
            // Создание рекорда с помощью конструктора
            var employee = new Employee("Сергей", "Сидоров", "IT", 100000);
            Console.WriteLine($"Сотрудник: {employee.FullName}, Отдел: {employee.Department}, Зарплата: {employee.Salary}");
            
            // Использование методов
            employee.IncreaseSalary(20000);
            Console.WriteLine($"После повышения: {employee.FullName}, Зарплата: {employee.Salary}");
            
            // Создание с помощью инициализатора объекта
            var employee2 = new Employee 
            { 
                FirstName = "Елена", 
                LastName = "Иванова", 
                Department = "Marketing"
            };
            Console.WriteLine($"Второй сотрудник: {employee2.FullName}, Отдел: {employee2.Department}");
            
            Console.WriteLine("\n--- Наследование рекордов ---");
            
            // Создание производного рекорда
            var manager = new Manager("Алексей", "Петров", "Sales", 150000, 5) 
            { 
                Bonus = 50000 
            };
            
            Console.WriteLine($"Менеджер: {manager.FullName}, Отдел: {manager.Department}");
            Console.WriteLine($"Команда: {manager.TeamSize} человек, Общий доход: {manager.TotalCompensation}");
            
            Console.WriteLine("\n--- Работа с позиционными рекордами ---");
            
            // Позиционное создание и деконструкция
            var customer = new PersonRecord("Михаил", "Кузнецов", 40);
            var (customerFirst, customerLast, customerAge) = customer; // Деконструкция
            Console.WriteLine($"Деконструированные данные: {customerFirst}, {customerLast}, {customerAge}");
            
            Console.WriteLine("\n--- Структуры с первичным конструктором ---");
            
            var point = new Point(3, 4);
            Console.WriteLine($"Точка: {point}, Расстояние от начала координат: {point.Distance}");
            
            Console.WriteLine("\n--- Pattern Matching с рекордами ---");
            
            object[] dataItems = {
                new PersonRecord("Иван", "Иванов", 30),
                new PersonRecord("Анна", "Смирнова", 25),
                new Employee("Сергей", "Петров", "IT", 120000),
                new Manager("Елена", "Сидорова", "HR", 150000, 3),
                new Point(10, 20),
                "Просто строка"
            };
            
            foreach (var item in dataItems)
            {
                string description = item switch
                {
                    PersonRecord { FirstName: "Анна" } => "Это Анна!",
                    PersonRecord { Age: > 25 } p => $"Это {p.FirstName}, возраст больше 25",
                    Employee { Department: "IT" } e => $"IT-специалист: {e.FullName}",
                    Manager m => $"Руководитель команды из {m.TeamSize} человек",
                    Point p when p.Distance > 15 => $"Дальняя точка: {p}",
                    Point p => $"Ближняя точка: {p}",
                    string s => $"Строка: {s}",
                    _ => "Неизвестный тип данных"
                };
                
                Console.WriteLine(description);
            }
        }
    }

    #region Задачи
    /*
        # Создайте рекорд Address с полями: Street, City, PostalCode, Country. 
          Реализуйте метод ToString(), форматирующий адрес в одну строку.
        
        # Создайте рекорд Product, представляющий товар с полями: Id, Name, Price, Category. 
          Реализуйте метод ApplyDiscount(decimal percentage), который возвращает новый рекорд 
          с уменьшенной ценой.
        
        # Создайте базовый рекорд Shape с полем Color, и производные рекорды: Circle (с радиусом) 
          и Rectangle (с шириной и высотой). Для каждой фигуры реализуйте вычисляемое свойство Area.
        
        # Создайте класс Vehicle с первичным конструктором, содержащий поля Model, Year и свойство Age, 
          которое вычисляет возраст автомобиля на основе текущего года.
        
        # Разработайте рекорд Transaction с полями Id, Date, Amount, Description и реализуйте:
          - Метод IsExpense(), возвращающий true, если Amount отрицательный
          - Метод GetFormattedAmount(), возвращающий сумму со знаком валюты
          - Свойство Month, возвращающее месяц транзакции
          Используйте эти рекорды для создания списка транзакций и демонстрации pattern matching 
          для группировки и анализа данных.
    */
    #endregion
}
