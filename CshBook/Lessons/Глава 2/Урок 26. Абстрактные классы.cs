using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons._26
{
    #region Теория
    /*
     * В этом уроке ты узнаешь об абстрактных классах в C#:
     * - Что такое абстрактные классы и для чего они нужны
     * - Отличие абстрактных классов от обычных классов
     * - Абстрактные методы и свойства
     * - Особенности наследования абстрактных классов
     * - Когда использовать абстрактные классы
     */

    /*
       Что такое абстрактный класс?
       ===========================
       
       Абстрактный класс — это особый тип класса, который не может быть инстанцирован 
       (нельзя создать его экземпляр напрямую) и предназначен для использования 
       в качестве базового класса для других классов.
       
       Абстрактные классы объявляются с использованием ключевого слова abstract:
       
       abstract class ИмяКласса
       {
           // Содержимое класса
       }
       
       Основные характеристики абстрактных классов:
       
       1. Нельзя создать экземпляр абстрактного класса напрямую (с помощью оператора new)
       2. Могут содержать как абстрактные методы/свойства, так и методы/свойства с реализацией
       3. Если класс содержит хотя бы один абстрактный метод, класс должен быть объявлен абстрактным
       4. Абстрактные классы могут наследоваться от других абстрактных или обычных классов
       5. Неабстрактные (конкретные) классы, наследующие абстрактный класс, должны реализовать 
          все абстрактные методы и свойства базового класса
    */

    /*
       Абстрактные методы и свойства
       ============================
       
       Абстрактный метод или свойство объявляется с использованием модификатора abstract и 
       не содержит реализации. Абстрактные члены класса должны быть реализованы в производных классах.
       
       Синтаксис абстрактного метода:
       
       abstract void ИмяМетода(параметры);
       
       Синтаксис абстрактного свойства:
       
       abstract тип ИмяСвойства { get; set; }
       
       Особенности абстрактных методов и свойств:
       
       1. Не имеют тела метода или реализации аксессоров свойства
       2. Могут быть объявлены только в абстрактных классах
       3. Не могут иметь модификаторы доступа private или static
       4. Все абстрактные члены должны быть реализованы в неабстрактных производных классах
       5. При реализации абстрактного метода или свойства в производном классе используется ключевое слово override
    */

    /*
       Зачем нужны абстрактные классы?
       =============================
       
       Абстрактные классы нужны для создания базовых классов, которые определяют 
       общий контракт для группы связанных подклассов, но при этом сами не могут 
       использоваться непосредственно.
       
       Основные причины использования абстрактных классов:
       
       1. Определение общего поведения
          Абстрактные классы могут содержать реализацию общих методов и свойств, 
          которые используются во всех производных классах.
       
       2. Создание шаблона
          Абстрактные классы определяют, какие методы должны быть реализованы 
          в производных классах, создавая таким образом шаблон для группы объектов.
       
       3. Принуждение к наследованию
          Абстрактные классы гарантируют, что наследники реализуют определенные методы, 
          которые объявлены как абстрактные.
       
       4. Частичная реализация
          В отличие от интерфейсов, абстрактные классы могут содержать не только 
          объявления, но и реализацию методов, полей и свойств.
    */

    /*
       Когда использовать абстрактные классы?
       ====================================
       
       Абстрактные классы следует использовать в следующих случаях:
       
       1. Когда нужно определить базовую функциональность для группы связанных объектов
       
       2. Когда необходимо использовать общие методы и свойства в нескольких классах, 
          но сам базовый класс не должен создаваться как самостоятельный объект
       
       3. Когда нужно определить общий интерфейс, но также предоставить некоторую 
          базовую реализацию
       
       4. Когда требуется использовать нестатические и непубличные члены класса 
          (в отличие от интерфейсов, которые могут содержать только публичные члены)
       
       5. Когда необходимо определить общие поля и константы
    */

    /*
       Абстрактные классы vs обычные классы
       ===================================
       
       Основные отличия от обычных классов:
       
       1. Нельзя создать экземпляр абстрактного класса напрямую
       
       2. Абстрактные классы могут содержать абстрактные методы и свойства, 
          которые не имеют реализации
       
       3. Абстрактные классы спроектированы для наследования и 
          часто содержат незавершенную функциональность
    */

    /*
       Абстрактные классы vs интерфейсы
       ===============================
       
       Хотя абстрактные классы и интерфейсы имеют схожие цели, между ними есть 
       важные различия:
       
       1. Интерфейсы поддерживают множественное наследование, а классы 
          (включая абстрактные) — нет
       
       2. Абстрактные классы могут содержать реализацию методов, полей, 
          свойств и конструкторов, а интерфейсы (до C# 8.0) — только объявления
       
       3. Члены абстрактного класса могут иметь различные модификаторы доступа, 
          а члены интерфейса всегда публичные
       
       4. Абстрактные классы лучше использовать, когда есть иерархия классов с общей 
          функциональностью, а интерфейсы — когда нужно добавить определенное поведение 
          к разнородным классам
    */
    
    #endregion

    public class AbstractClassesLesson
    {
        public static void Main_()
        {
            Console.WriteLine("=== Демонстрация абстрактных классов ===\n");
            
            // Нельзя создать экземпляр абстрактного класса напрямую:
            // Animal animal = new Animal(); // Это вызвало бы ошибку компиляции
            
            // Но можно создать экземпляры классов-наследников:
            Dog dog = new Dog("Бобик", 5);
            Cat cat = new Cat("Мурка", 3);
            Bird bird = new Bird("Кеша", 2);
            
            // Работа с объектами через общие методы:
            Console.WriteLine("Информация о животных:");
            Console.WriteLine(dog.GetInfo());
            Console.WriteLine(cat.GetInfo());
            Console.WriteLine(bird.GetInfo());
            
            Console.WriteLine("\nЗвуки животных:");
            dog.MakeSound();
            cat.MakeSound();
            bird.MakeSound();
            
            Console.WriteLine("\nДвижения животных:");
            dog.Move();
            cat.Move();
            bird.Move();
            
            // Работа с полиморфизмом:
            Console.WriteLine("\nРабота с полиморфизмом:");
            Animal[] animals = new Animal[] { dog, cat, bird };
            
            foreach (Animal animal in animals)
            {
                // Вызов переопределенных методов:
                Console.WriteLine(animal.GetInfo());
                animal.MakeSound();
                animal.Move();
                Console.WriteLine(); // Пустая строка для разделения
            }
            
            // Демонстрация с абстрактным классом фигуры:
            Console.WriteLine("=== Демонстрация с геометрическими фигурами ===");
            
            Circle circle = new Circle(5);
            Rectangle rectangle = new Rectangle(4, 6);
            Triangle triangle = new Triangle(3, 4, 5);
            
            Console.WriteLine($"Круг: Площадь = {circle.CalculateArea()}, Периметр = {circle.CalculatePerimeter()}");
            Console.WriteLine($"Прямоугольник: Площадь = {rectangle.CalculateArea()}, Периметр = {rectangle.CalculatePerimeter()}");
            Console.WriteLine($"Треугольник: Площадь = {triangle.CalculateArea()}, Периметр = {triangle.CalculatePerimeter()}");
            
            // Пример с абстрактным типом данных (ADT):
            Console.WriteLine("\n=== Демонстрация с абстрактным типом данных (коллекция) ===");
            
            CustomCollection<int> stack = new CustomStack<int>();
            CustomCollection<int> queue = new CustomQueue<int>();
            
            Console.WriteLine("Добавление элементов в стек и очередь:");
            for (int i = 1; i <= 5; i++)
            {
                stack.Add(i);
                queue.Add(i);
                Console.WriteLine($"Добавлено число {i}");
            }
            
            Console.WriteLine("\nИзвлечение элементов из стека (LIFO):");
            while (!stack.IsEmpty())
            {
                Console.WriteLine($"Извлечено из стека: {stack.Remove()}");
            }
            
            Console.WriteLine("\nИзвлечение элементов из очереди (FIFO):");
            while (!queue.IsEmpty())
            {
                Console.WriteLine($"Извлечено из очереди: {queue.Remove()}");
            }
        }
    }

    // Абстрактный класс животного
    public abstract class Animal
    {
        // Обычные (неабстрактные) поля и свойства
        public string Name { get; protected set; }
        public int Age { get; protected set; }
        
        // Конструктор абстрактного класса
        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }
        
        // Обычный (неабстрактный) метод с реализацией
        public virtual string GetInfo()
        {
            return $"{GetType().Name}: {Name}, возраст: {Age} лет";
        }
        
        // Абстрактные методы без реализации, которые должны быть
        // переопределены в производных классах
        public abstract void MakeSound();
        public abstract void Move();
    }

    // Производный класс, наследующий от абстрактного класса
    public class Dog : Animal
    {
        public Dog(string name, int age) : base(name, age) { }
        
        // Реализация абстрактных методов
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} говорит: Гав-гав!");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} бежит, виляя хвостом.");
        }
        
        // Дополнительные методы, специфичные для класса Dog
        public void Fetch()
        {
            Console.WriteLine($"{Name} принес палку.");
        }
    }

    // Еще один производный класс
    public class Cat : Animal
    {
        public Cat(string name, int age) : base(name, age) { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} говорит: Мяу-мяу!");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} крадется тихо.");
        }
        
        // Дополнительные методы, специфичные для класса Cat
        public void Purr()
        {
            Console.WriteLine($"{Name} мурлычет.");
        }
    }

    // Еще один производный класс
    public class Bird : Animal
    {
        public Bird(string name, int age) : base(name, age) { }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} говорит: Чирик-чирик!");
        }
        
        public override void Move()
        {
            Console.WriteLine($"{Name} летит в небе.");
        }
        
        // Дополнительные методы, специфичные для класса Bird
        public void Fly()
        {
            Console.WriteLine($"{Name} летит высоко в небе.");
        }
    }

    // Пример 2: Абстрактный класс для геометрических фигур
    public abstract class Shape
    {
        // Абстрактные свойства
        public abstract double Area { get; }
        public abstract double Perimeter { get; }
        
        // Абстрактные методы
        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();
        
        // Неабстрактный метод с реализацией
        public void PrintInfo()
        {
            Console.WriteLine($"Тип фигуры: {GetType().Name}");
            Console.WriteLine($"Площадь: {CalculateArea()}");
            Console.WriteLine($"Периметр: {CalculatePerimeter()}");
        }
    }

    // Производный класс для круга
    public class Circle : Shape
    {
        private double radius;
        
        public Circle(double radius)
        {
            this.radius = radius;
        }
        
        // Реализация абстрактных свойств
        public override double Area => Math.PI * radius * radius;
        public override double Perimeter => 2 * Math.PI * radius;
        
        // Реализация абстрактных методов
        public override double CalculateArea()
        {
            return Area;
        }
        
        public override double CalculatePerimeter()
        {
            return Perimeter;
        }
    }

    // Производный класс для прямоугольника
    public class Rectangle : Shape
    {
        private double width;
        private double height;
        
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }
        
        public override double Area => width * height;
        public override double Perimeter => 2 * (width + height);
        
        public override double CalculateArea()
        {
            return Area;
        }
        
        public override double CalculatePerimeter()
        {
            return Perimeter;
        }
    }

    // Производный класс для треугольника
    public class Triangle : Shape
    {
        private double sideA;
        private double sideB;
        private double sideC;
        
        public Triangle(double sideA, double sideB, double sideC)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }
        
        public override double Area
        {
            get
            {
                // Формула Герона для площади треугольника
                double s = (sideA + sideB + sideC) / 2;
                return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
            }
        }
        
        public override double Perimeter => sideA + sideB + sideC;
        
        public override double CalculateArea()
        {
            return Area;
        }
        
        public override double CalculatePerimeter()
        {
            return Perimeter;
        }
    }

    // Пример 3: Абстрактный класс для работы с коллекциями
    public abstract class CustomCollection<T>
    {
        protected List<T> items = new List<T>();
        
        // Абстрактные методы
        public abstract void Add(T item);
        public abstract T Remove();
        
        // Неабстрактные методы с реализацией
        public bool IsEmpty()
        {
            return items.Count == 0;
        }
        
        public int Count()
        {
            return items.Count;
        }
        
        public void Clear()
        {
            items.Clear();
        }
    }

    // Реализация стека (LIFO - Last In, First Out)
    public class CustomStack<T> : CustomCollection<T>
    {
        public override void Add(T item)
        {
            items.Add(item); // Добавление в конец списка
        }
        
        public override T Remove()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Стек пуст");
                
            int lastIndex = items.Count - 1;
            T item = items[lastIndex];
            items.RemoveAt(lastIndex); // Удаление с конца (последний вошел, первый вышел)
            return item;
        }
    }

    // Реализация очереди (FIFO - First In, First Out)
    public class CustomQueue<T> : CustomCollection<T>
    {
        public override void Add(T item)
        {
            items.Add(item); // Добавление в конец списка
        }
        
        public override T Remove()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Очередь пуста");
                
            T item = items[0];
            items.RemoveAt(0); // Удаление с начала (первый вошел, первый вышел)
            return item;
        }
    }

    #region Задачи
    /*
        # Создайте абстрактный класс Employee с полями Name, Age и абстрактными методами 
          CalculateSalary() и GetInfo(). Реализуйте несколько производных классов: 
          Manager, Developer, Designer с разными формулами расчета зарплаты и разной информацией.
        
        # Разработайте абстрактный класс FileHandler с абстрактными методами ReadFile() и 
          WriteFile(). Создайте производные классы TextFileHandler, CsvFileHandler и 
          JsonFileHandler с соответствующими реализациями для работы с разными типами файлов.
        
        # Создайте абстрактный класс Vehicle с абстрактными свойствами MaxSpeed, FuelType и 
          методами StartEngine(), StopEngine() и Drive(). Реализуйте классы Car, Motorcycle и 
          Truck с разными характеристиками и поведением.
        
        # Разработайте абстрактный класс AbstractFactory с методами CreateProduct() и 
          GetProductInfo(). Затем реализуйте несколько конкретных фабрик для создания 
          различных типов продуктов (например, ElectronicsFactory, FurnitureFactory).
          
        # Создайте абстрактный класс Game с абстрактными методами Initialize(), Start(), 
          Update() и End(). Реализуйте несколько простых игр, используя этот абстрактный класс 
          (например, GuessNumber, RockPaperScissors, SimpleQuiz).
    */
    #endregion
}
