using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons._28
{
    #region Теория
    /*
     * В этом уроке ты узнаешь об интерфейсах в C#:
     * - Что такое интерфейсы и для чего они нужны
     * - Синтаксис объявления и реализации интерфейсов
     * - Множественная реализация интерфейсов
     * - Явная реализация интерфейсов
     * - Интерфейсы как типы
     * - Стандартные интерфейсы в .NET
     * - Интерфейсы в C# 8.0 и выше (реализации по умолчанию)
     */

    /*
       Что такое интерфейс?
       ===================
       
       Интерфейс — это контракт, который определяет набор методов, свойств, индексаторов и событий, 
       которые должен реализовать класс или структура. Интерфейсы используются для определения 
       общего поведения для разных типов.
       
       Интерфейсы объявляются с использованием ключевого слова interface:
       
       interface IИмяИнтерфейса
       {
           // Объявления методов, свойств, индексаторов и событий
       }
       
       Основные характеристики интерфейсов:
       
       1. Интерфейсы не содержат реализации методов (до C# 8.0)
       2. Интерфейсы не могут содержать поля (переменные)
       3. Члены интерфейса неявно являются публичными (public)
       4. Класс или структура могут реализовывать несколько интерфейсов
       5. Интерфейс может наследоваться от других интерфейсов
       6. Интерфейсы не могут быть инстанцированы напрямую
    */

    /*
       Синтаксис объявления и реализации интерфейсов
       ============================================
       
       Объявление интерфейса:
       
       interface IИмяИнтерфейса
       {
           // Методы
           ТипВозврата ИмяМетода(параметры);
           
           // Свойства
           ТипСвойства ИмяСвойства { get; set; }
           
           // Индексаторы
           ТипЭлемента this[ТипИндекса индекс] { get; set; }
           
           // События
           event ТипДелегата ИмяСобытия;
       }
       
       Реализация интерфейса в классе:
       
       class ИмяКласса : IИмяИнтерфейса
       {
           // Реализация всех членов интерфейса
           public ТипВозврата ИмяМетода(параметры)
           {
               // Реализация метода
           }
           
           public ТипСвойства ИмяСвойства { get; set; }
       }
       
       Реализация нескольких интерфейсов:
       
       class ИмяКласса : IИнтерфейс1, IИнтерфейс2, IИнтерфейс3
       {
           // Реализация всех членов всех интерфейсов
       }
    */

    /*
       Множественная реализация интерфейсов
       ===================================
       
       Одно из ключевых преимуществ интерфейсов — возможность реализации нескольких интерфейсов 
       одним классом. Это позволяет классу иметь несколько "контрактов" поведения.
       
       Пример:
       
       interface IDrawable
       {
           void Draw();
       }
       
       interface IMovable
       {
           void Move(int x, int y);
       }
       
       class Shape : IDrawable, IMovable
       {
           public void Draw()
           {
               // Реализация отрисовки
           }
           
           public void Move(int x, int y)
           {
               // Реализация перемещения
           }
       }
       
       Это особенно полезно, когда нужно, чтобы класс имел несколько разных поведений,
       не связанных напрямую с его основной иерархией наследования.
    */

    /*
       Явная реализация интерфейсов
       ===========================
       
       Если класс реализует несколько интерфейсов, которые имеют методы с одинаковыми 
       сигнатурами, или если нужно скрыть реализацию интерфейса от пользователей класса, 
       можно использовать явную реализацию интерфейса:
       
       class MyClass : IInterface1, IInterface2
       {
           // Явная реализация метода из IInterface1
           void IInterface1.Method()
           {
               // Реализация для IInterface1
           }
           
           // Явная реализация метода из IInterface2
           void IInterface2.Method()
           {
               // Реализация для IInterface2
           }
       }
       
       При явной реализации:
       1. Не указывается модификатор доступа (public)
       2. Доступ к методу возможен только через ссылку на интерфейс
       3. Метод не доступен через ссылку на класс напрямую
    */

    /*
       Интерфейсы как типы
       =================
       
       Интерфейсы могут использоваться как типы переменных, параметров и возвращаемых значений:
       
       IDrawable drawable = new Circle();  // Circle реализует IDrawable
       
       void DrawShape(IDrawable shape)
       {
           shape.Draw();  // Полиморфизм через интерфейсы
       }
       
       IComparable GetComparable()
       {
           return new MyComparableClass();
       }
       
       Это позволяет использовать полиморфизм без наследования классов.
    */

    /*
       Стандартные интерфейсы в .NET
       ===========================
       
       .NET Framework и .NET Core предоставляют множество стандартных интерфейсов:
       
       1. IComparable, IComparable<T> - для сравнения объектов
       2. IEnumerable, IEnumerable<T> - для перечисления коллекций
       3. IDisposable - для освобождения ресурсов
       4. ICloneable - для клонирования объектов
       5. IEquatable<T> - для сравнения объектов на равенство
       6. ICollection, ICollection<T> - для работы с коллекциями
       7. IList, IList<T> - для работы со списками
       8. IDictionary, IDictionary<TKey, TValue> - для работы со словарями
       
       Использование этих стандартных интерфейсов делает код более совместимым 
       с существующими библиотеками и фреймворками.
    */

    /*
       Интерфейсы в C# 8.0 и выше
       =========================
       
       Начиная с C# 8.0, интерфейсы могут содержать:
       
       1. Реализации методов по умолчанию
       2. Статические методы
       3. Приватные методы
       4. Приватные статические методы
       5. Виртуальные методы с реализацией по умолчанию
       
       Пример:
       
       interface ILogger
       {
           // Метод без реализации (как в предыдущих версиях C#)
           void Log(string message);
           
           // Метод с реализацией по умолчанию
           void LogError(string message)
           {
               Log($"ERROR: {message}");
           }
           
           // Статический метод
           static string FormatMessage(string message)
           {
               return $"[{DateTime.Now}] {message}";
           }
           
           // Приватный метод (доступен только внутри интерфейса)
           private void InternalLog(string message)
           {
               // Внутренняя реализация
           }
       }
       
       Эти новые возможности делают интерфейсы более гибкими и позволяют 
       добавлять новую функциональность без нарушения обратной совместимости.
    */
    
    /*
       Когда использовать интерфейсы?
       ============================
       
       Интерфейсы следует использовать в следующих случаях:
       
       1. Когда нужно определить контракт, который должны реализовать разные классы
       
       2. Когда требуется множественное наследование поведения
       
       3. Когда нужно обеспечить полиморфизм между несвязанными классами
       
       4. Когда нужно разделить дизайн и реализацию
       
       5. Когда нужно определить общее поведение для разных иерархий классов
       
       6. Для создания слабосвязанных компонентов (Dependency Injection)
    */
    
    #endregion

    public class InterfacesLesson
    {
        public static void Main_()
        {
            Console.WriteLine("=== Демонстрация интерфейсов ===\n");
            
            // Создание объектов, реализующих интерфейсы
            IDrawable circle = new Circle(5);
            IDrawable rectangle = new Rectangle(4, 6);
            
            // Использование интерфейсов для вызова методов
            Console.WriteLine("Рисование фигур:");
            circle.Draw();
            rectangle.Draw();
            
            // Использование интерфейса как параметра метода
            Console.WriteLine("\nРисование через метод DrawShape:");
            DrawShape(circle);
            DrawShape(rectangle);
            
            // Множественные интерфейсы
            Console.WriteLine("\nДемонстрация множественных интерфейсов:");
            Shape shape = new Shape("Многоугольник");
            shape.Draw();
            shape.Move(10, 20);
            shape.Resize(2.0);
            
            // Явная реализация интерфейсов
            Console.WriteLine("\nДемонстрация явной реализации интерфейсов:");
            MultiPurposeDevice device = new MultiPurposeDevice();
            
            // Использование через ссылки на интерфейсы
            IPrinter printer = device;
            IScanner scanner = device;
            IFax fax = device;
            
            printer.Print("Документ для печати");
            scanner.Scan("Документ для сканирования");
            fax.Send("Документ для отправки по факсу");
            
            // Стандартные интерфейсы .NET
            Console.WriteLine("\nДемонстрация стандартных интерфейсов .NET:");
            
            // IComparable
            Person[] people = new Person[]
            {
                new Person("Иван", 30),
                new Person("Мария", 25),
                new Person("Алексей", 35)
            };
            
            Array.Sort(people); // Использует IComparable.CompareTo
            
            Console.WriteLine("Отсортированный список людей по возрасту:");
            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
            
            // IDisposable
            Console.WriteLine("\nДемонстрация IDisposable:");
            using (ResourceManager resource = new ResourceManager())
            {
                resource.DoWork();
            } // Автоматически вызывается Dispose()
            
            // IEnumerable
            Console.WriteLine("\nДемонстрация IEnumerable:");
            CustomCollection collection = new CustomCollection();
            
            Console.WriteLine("Элементы коллекции:");
            foreach (var item in collection) // Использует IEnumerable
            {
                Console.WriteLine(item);
            }
            
            // Интерфейсы с реализацией по умолчанию (C# 8.0+)
            Console.WriteLine("\nДемонстрация интерфейсов с реализацией по умолчанию:");
            IModernLogger fileLogger = new FileLogger();
            fileLogger.Log("Обычное сообщение");
            fileLogger.LogError("Произошла ошибка"); // Использует реализацию по умолчанию
            fileLogger.LogWarning("Предупреждение"); // Использует реализацию по умолчанию
        }
        
        // Метод, принимающий параметр типа интерфейса
        private static void DrawShape(IDrawable shape)
        {
            shape.Draw();
        }
    }

    // Объявление интерфейсов
    public interface IDrawable
    {
        void Draw();
    }
    
    public interface IMovable
    {
        void Move(int x, int y);
    }
    
    public interface IResizable
    {
        void Resize(double factor);
    }
    
    // Класс, реализующий интерфейс
    public class Circle : IDrawable
    {
        private double radius;
        
        public Circle(double radius)
        {
            this.radius = radius;
        }
        
        // Реализация метода из интерфейса IDrawable
        public void Draw()
        {
            Console.WriteLine($"Рисуем круг с радиусом {radius}");
        }
    }
    
    public class Rectangle : IDrawable
    {
        private double width;
        private double height;
        
        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }
        
        // Реализация метода из интерфейса IDrawable
        public void Draw()
        {
            Console.WriteLine($"Рисуем прямоугольник с шириной {width} и высотой {height}");
        }
    }
    
    // Класс, реализующий несколько интерфейсов
    public class Shape : IDrawable, IMovable, IResizable
    {
        private string name;
        private int x = 0;
        private int y = 0;
        private double scale = 1.0;
        
        public Shape(string name)
        {
            this.name = name;
        }
        
        // Реализация IDrawable
        public void Draw()
        {
            Console.WriteLine($"Рисуем фигуру '{name}' в позиции ({x}, {y}) с масштабом {scale}");
        }
        
        // Реализация IMovable
        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
            Console.WriteLine($"Перемещаем '{name}' в позицию ({x}, {y})");
        }
        
        // Реализация IResizable
        public void Resize(double factor)
        {
            this.scale *= factor;
            Console.WriteLine($"Изменяем размер '{name}', новый масштаб: {scale}");
        }
    }
    
    // Интерфейсы для демонстрации явной реализации
    public interface IPrinter
    {
        void Print(string document);
    }
    
    public interface IScanner
    {
        void Scan(string document);
    }
    
    public interface IFax
    {
        void Send(string document);
    }
    
    // Класс с явной реализацией интерфейсов
    public class MultiPurposeDevice : IPrinter, IScanner, IFax
    {
        // Явная реализация IPrinter
        void IPrinter.Print(string document)
        {
            Console.WriteLine($"Печать: {document}");
        }
        
        // Явная реализация IScanner
        void IScanner.Scan(string document)
        {
            Console.WriteLine($"Сканирование: {document}");
        }
        
        // Явная реализация IFax
        void IFax.Send(string document)
        {
            Console.WriteLine($"Отправка по факсу: {document}");
        }
    }
    
    // Класс, реализующий стандартный интерфейс IComparable
    public class Person : IComparable<Person>
    {
        public string Name { get; }
        public int Age { get; }
        
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        
        // Реализация IComparable<Person>
        public int CompareTo(Person other)
        {
            if (other == null) return 1;
            return Age.CompareTo(other.Age);
        }
        
        public override string ToString()
        {
            return $"{Name}, {Age} лет";
        }
    }
    
    // Класс, реализующий стандартный интерфейс IDisposable
    public class ResourceManager : IDisposable
    {
        private bool disposed = false;
        
        public ResourceManager()
        {
            Console.WriteLine("ResourceManager: Ресурс создан");
        }
        
        public void DoWork()
        {
            if (disposed)
                throw new ObjectDisposedException("ResourceManager");
                
            Console.WriteLine("ResourceManager: Выполняем работу с ресурсом");
        }
        
        // Реализация IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Освобождаем управляемые ресурсы
                }
                
                // Освобождаем неуправляемые ресурсы
                Console.WriteLine("ResourceManager: Ресурс освобожден");
                
                disposed = true;
            }
        }
        
        ~ResourceManager()
        {
            Dispose(false);
        }
    }
    
    // Класс, реализующий стандартный интерфейс IEnumerable
    public class CustomCollection : IEnumerable<string>
    {
        private List<string> items = new List<string> { "Элемент 1", "Элемент 2", "Элемент 3", "Элемент 4", "Элемент 5" };
        
        // Реализация IEnumerable<string>
        public IEnumerator<string> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        
        // Реализация IEnumerable (необходимо для IEnumerable<T>)
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    // Интерфейс с реализацией по умолчанию (C# 8.0+)
    public interface IModernLogger
    {
        void Log(string message);
        
        // Метод с реализацией по умолчанию
        void LogError(string message)
        {
            Log($"ERROR: {message}");
        }
        
        // Еще один метод с реализацией по умолчанию
        void LogWarning(string message)
        {
            Log($"WARNING: {message}");
        }
    }
    
    // Реализация интерфейса с методами по умолчанию
    public class FileLogger : IModernLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[FILE] {DateTime.Now}: {message}");
        }
        
        // Мы не реализуем LogError и LogWarning, используются реализации по умолчанию
    }
    
    #region Задачи
    /*
        # Создайте интерфейс IVehicle с методами Start(), Stop() и свойством Model. 
          Реализуйте этот интерфейс в классах Car, Motorcycle и Bus. Создайте метод, 
          который принимает IVehicle и выполняет с ним некоторые действия.
        
        # Создайте интерфейс IRepository<T> с методами Add(T item), Delete(int id), 
          GetById(int id) и GetAll(). Реализуйте этот интерфейс для работы с разными 
          типами данных (например, User, Product).
        
        # Создайте класс, реализующий несколько интерфейсов: IPlayable, IRecordable, IStorable. 
          Каждый интерфейс должен содержать методы с одинаковыми именами, но разной 
          функциональностью. Используйте явную реализацию интерфейсов.
        
        # Реализуйте стандартный интерфейс IEnumerable<T> для собственного класса коллекции. 
          Продемонстрируйте использование foreach для перебора элементов вашей коллекции.
        
        # Создайте интерфейс с реализацией методов по умолчанию (C# 8.0+). Реализуйте 
          этот интерфейс в нескольких классах, переопределив некоторые методы в одних 
          классах и используя реализации по умолчанию в других.
    */
    #endregion
}