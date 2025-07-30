using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о применении обобщенных типов с ограничениями (constraints) 
     * при помощи ключевого слова where в C#. Мы рассмотрим:
     * - Что такое ограничения типов в обобщенных классах и методах
     * - Виды ограничений и их применение
     * - Практические примеры использования where с классами
     */

    /*
       Ограничения типов (Type Constraints) и ключевое слово where
       ===========================================================
       
       В C# при создании обобщенных (generic) классов или методов иногда возникает 
       необходимость ограничить типы, которые могут использоваться в качестве параметров типа.
       Для этого служит ключевое слово where.
       
       Ограничения типов позволяют указать, что тип параметра должен:
       - Быть ссылочным типом или значимым типом
       - Иметь конструктор без параметров
       - Наследоваться от определенного базового класса
       - Реализовывать определенный интерфейс
       - Иметь определенные характеристики
       
       Ограничения типов дают несколько преимуществ:
       1. Повышают безопасность типов
       2. Позволяют использовать специфичные методы и свойства в коде обобщенного класса
       3. Улучшают читаемость кода, делая явными требования к типам
    */

    /*
       Синтаксис where
       ==============
       
       Для класса:
       class ИмяКласса<T> where T : ограничение
       {
           // Реализация класса
       }
       
       Для метода:
       ТипВозврата ИмяМетода<T>(параметры) where T : ограничение
       {
           // Реализация метода
       }
       
       Возможные ограничения:
       - class - тип должен быть ссылочным (классом, интерфейсом, делегатом или массивом)
       - struct - тип должен быть значимым (например, int, double, bool, или структура)
       - new() - тип должен иметь конструктор без параметров
       - ИмяБазовогоКласса - тип должен наследоваться от указанного базового класса
       - ИмяИнтерфейса - тип должен реализовывать указанный интерфейс
       - U - тип должен наследоваться от другого параметра типа
       - unmanaged - тип должен быть неуправляемым типом
       - notnull - тип не должен допускать значение null
       
       Можно комбинировать несколько ограничений через запятую:
       where T : class, IComparable, new()
    */

    /*
       Примеры использования where
       ==========================
       
       1. Ограничение на базовый класс:
       class Repository<T> where T : Entity
       {
           // В этом классе можно использовать свойства и методы Entity
           // так как известно, что T является наследником Entity
       }
       
       2. Ограничение на интерфейс:
       class Sorter<T> where T : IComparable<T>
       {
           // Здесь можно использовать метод CompareTo у объектов типа T
       }
       
       3. Ограничение на наличие конструктора:
       class Factory<T> where T : new()
       {
           // Здесь можно создавать новые экземпляры T через new T()
       }
       
       4. Комбинация ограничений:
       class DataProcessor<T> where T : class, IDataEntity, new()
       {
           // T должен быть классом, реализовывать IDataEntity и 
           // иметь конструктор без параметров
       }
       
       5. Ограничение для нескольких параметров типа:
       class KeyValueStorage<TKey, TValue>
           where TKey : IComparable<TKey>
           where TValue : class, new()
       {
           // TKey должен реализовывать IComparable<TKey>
           // TValue должен быть классом и иметь конструктор без параметров
       }
    */
    
    /*
       Реальные сценарии использования where
       ====================================
       
       1. Создание репозиториев для работы с базами данных:
       
          class Repository<T> where T : DbEntity, new()
          {
              // Можно использовать свойства и методы DbEntity
              // и создавать новые экземпляры T
          }
          
       2. Реализация обобщенных алгоритмов:
       
          class SortingAlgorithm<T> where T : IComparable<T>
          {
              // Можно сортировать элементы типа T, так как они поддерживают сравнение
          }
          
       3. Создание фабрик объектов:
       
          class ObjectFactory<T> where T : class, new()
          {
              // Создание объектов типа T
          }
          
       4. Маппинг объектов:
       
          class Mapper<TSource, TTarget> 
              where TSource : class
              where TTarget : class, new()
          {
              // Конвертация объектов из типа TSource в тип TTarget
          }
    */
    
    #endregion

    public class WhereWithClasses
    {
        public static void Main_()
        {
            Console.WriteLine("=== Демонстрация ограничений типов (where) ===\n");
            
            // Демонстрация обобщенного хранилища данных
            Console.WriteLine("Хранилище данных (только для Entity):");
            var personRepo = new Repository<Person>();
            personRepo.Add(new Person { Id = 1, Name = "Иван", Age = 30 });
            personRepo.Add(new Person { Id = 2, Name = "Мария", Age = 25 });
            personRepo.PrintAll();
            
            // Это не скомпилируется, так как string не является наследником Entity
            // var stringRepo = new Repository<string>();
            
            Console.WriteLine("\nОбработчик сравниваемых объектов:");
            var comparer = new ComparableProcessor<Person>();
            comparer.AddItem(new Person { Id = 1, Name = "Иван", Age = 30 });
            comparer.AddItem(new Person { Id = 2, Name = "Мария", Age = 25 });
            comparer.AddItem(new Person { Id = 3, Name = "Алексей", Age = 35 });
            comparer.PrintAllSorted();
            
            Console.WriteLine("\nФабрика объектов:");
            var factory = new Factory<Person>();
            var newPerson = factory.CreateInstance();
            newPerson.Name = "Новый пользователь";
            newPerson.Age = 20;
            Console.WriteLine($"Создан: {newPerson}");
            
            Console.WriteLine("\nОбобщенный кэш:");
            var cache = new Cache<string, Person>();
            cache.Add("user1", new Person { Id = 4, Name = "Елена", Age = 28 });
            cache.Add("user2", new Person { Id = 5, Name = "Дмитрий", Age = 32 });
            
            var user = cache.Get("user1");
            Console.WriteLine($"Получено из кэша: {user}");
            
            Console.WriteLine("\nРабота с преобразованиями объектов:");
            var mapper = new Mapper<PersonDto, Person>();
            var dto = new PersonDto { Id = 6, FullName = "Сергей Иванов", YearsOld = 40 };
            var entity = mapper.Map(dto);
            Console.WriteLine($"DTO: {dto}");
            Console.WriteLine($"Entity: {entity}");
        }
    }

    // Базовый класс для всех сущностей
    public class Entity
    {
        public int Id { get; set; }
    }

    // Пример сущности
    public class Person : Entity, IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        
        // Реализация интерфейса IComparable<Person>
        public int CompareTo(Person other)
        {
            if (other == null) return 1;
            return Age.CompareTo(other.Age);
        }
        
        public override string ToString()
        {
            return $"Person {{ Id = {Id}, Name = {Name}, Age = {Age} }}";
        }
    }

    // DTO класс для Person
    public class PersonDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int YearsOld { get; set; }
        
        public override string ToString()
        {
            return $"PersonDto {{ Id = {Id}, FullName = {FullName}, YearsOld = {YearsOld} }}";
        }
    }

    // Репозиторий для работы с сущностями
    // Ограничение where T : Entity означает, что T должен быть наследником класса Entity
    public class Repository<T> where T : Entity
    {
        private List<T> items = new List<T>();
        
        public void Add(T item)
        {
            // Можем использовать свойство Id, так как знаем, что T - наследник Entity
            Console.WriteLine($"Добавлен элемент с Id = {item.Id}");
            items.Add(item);
        }
        
        public T GetById(int id)
        {
            // Можем использовать свойство Id при поиске
            return items.FirstOrDefault(item => item.Id == id);
        }
        
        public void PrintAll()
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }

    // Класс для работы с объектами, поддерживающими сравнение
    // Ограничение where T : IComparable<T> означает, что T должен реализовывать интерфейс IComparable<T>
    public class ComparableProcessor<T> where T : IComparable<T>
    {
        private List<T> items = new List<T>();
        
        public void AddItem(T item)
        {
            items.Add(item);
        }
        
        public T FindMin()
        {
            if (items.Count == 0) return default;
            
            T min = items[0];
            for (int i = 1; i < items.Count; i++)
            {
                // Можем использовать метод CompareTo, так как T реализует IComparable<T>
                if (items[i].CompareTo(min) < 0)
                {
                    min = items[i];
                }
            }
            
            return min;
        }
        
        public void PrintAllSorted()
        {
            // Сортировка с использованием метода CompareTo
            var sorted = items.OrderBy(item => item).ToList();
            
            foreach (var item in sorted)
            {
                Console.WriteLine(item);
            }
        }
    }

    // Фабрика для создания объектов
    // Ограничение where T : new() означает, что T должен иметь конструктор без параметров
    public class Factory<T> where T : new()
    {
        public T CreateInstance()
        {
            // Можем создать экземпляр T, так как T имеет конструктор без параметров
            return new T();
        }
    }

    // Обобщенный кэш с несколькими ограничениями типов
    // TKey должен реализовывать IComparable<TKey>
    // TValue должен быть ссылочным типом, быть или наследоваться от Entity, и иметь конструктор без параметров
    public class Cache<TKey, TValue>
        where TKey : IComparable<TKey>
        where TValue : Entity, new()
    {
        private Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
        
        public void Add(TKey key, TValue value)
        {
            // Можем использовать свойство Id, так как TValue - наследник Entity
            Console.WriteLine($"Добавлен в кэш: {key} -> Entity с Id = {value.Id}");
            cache[key] = value;
        }
        
        public TValue Get(TKey key)
        {
            if (cache.TryGetValue(key, out TValue value))
                return value;
                
            // Если элемент не найден, можем создать новый экземпляр, 
            // так как TValue имеет конструктор без параметров
            return new TValue();
        }
    }

    // Маппер для преобразования объектов
    // Ограничения: TSource должен быть ссылочным типом (class),
    // TTarget должен быть ссылочным типом (class) и иметь конструктор без параметров
    public class Mapper<TSource, TTarget>
        where TSource : class
        where TTarget : class, new()
    {
        public TTarget Map(TSource source)
        {
            Console.WriteLine("Преобразование объекта...");
            
            // Создаем новый экземпляр TTarget
            TTarget target = new TTarget();
            
            // С помощью Reflection копируем свойства из source в target
            // В реальном приложении можно использовать библиотеки вроде AutoMapper
            
            // Для примера, свойства копируются вручную:
            if (source is PersonDto dto && target is Person person)
            {
                person.Id = dto.Id;
                person.Name = dto.FullName;
                person.Age = dto.YearsOld;
            }
            
            return target;
        }
    }

    #region Задачи
    /*
        # Создайте обобщенный класс Calculator<T>, который принимает параметр типа T 
          с ограничением where T : struct, IComparable, IConvertible, IFormattable.
          Реализуйте методы Add, Subtract, Multiply и Divide, которые принимают два 
          параметра типа T и возвращают результат соответствующей операции.
        
        # Создайте класс EntityValidator<T> с ограничением where T : Entity, new().
          Реализуйте методы: Validate, который проверяет, что Id сущности больше нуля,
          и ValidateAll, который проверяет коллекцию сущностей.
          
        # Разработайте обобщенный класс для сериализации/десериализации объектов в JSON.
          Класс должен иметь ограничение, что тип T является ссылочным типом и имеет
          конструктор без параметров. Реализуйте методы Serialize и Deserialize.
          
    */
    #endregion
}
