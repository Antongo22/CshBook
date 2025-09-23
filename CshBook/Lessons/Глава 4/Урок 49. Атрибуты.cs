using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_4
{
    #region Теория
    /*
     * В этом уроке ты узнаешь об атрибутах в C#:
     * 
     * - Что такое атрибуты и зачем они нужны
     * - Встроенные атрибуты в .NET
     * - Создание пользовательских атрибутов
     * - Чтение атрибутов через Reflection
     * - Практические применения атрибутов
     * - Лучшие практики работы с атрибутами
     */

    /*
       Что такое атрибуты и зачем они нужны
       =================================
       
       Атрибуты (Attributes) - это специальные классы, которые позволяют добавлять
       метаданные к элементам кода (классам, методам, свойствам, полям и т.д.).
       
       Атрибуты НЕ влияют на выполнение кода напрямую, но предоставляют информацию,
       которую можно прочитать во время выполнения через Reflection.
       
       Зачем нужны атрибуты:
       
       1. Декларативное программирование
          - Описание поведения через метаданные, а не код
          - Разделение логики и конфигурации
       
       2. Автоматизация задач
          - Сериализация/десериализация объектов
          - Валидация данных
          - Маппинг объектов на базу данных
       
       3. Инструменты разработки
          - Подсказки для IDE
          - Анализаторы кода
          - Генерация документации
       
       4. Фреймворки и библиотеки
          - ASP.NET Core использует атрибуты для маршрутизации
          - Entity Framework - для настройки моделей
          - JSON.NET - для настройки сериализации
       
       Синтаксис применения атрибутов:
       [AttributeName]
       [AttributeName(parameter)]
       [AttributeName(parameter1, parameter2)]
       [AttributeName(NamedParameter = value)]
    */

    /*
       Встроенные атрибуты в .NET
       ========================
       
       .NET предоставляет множество готовых атрибутов:
       
       1. [Obsolete] - помечает устаревший код
          [Obsolete("Используйте NewMethod вместо этого")]
          public void OldMethod() { }
       
       2. [Serializable] - указывает, что класс можно сериализовать
          [Serializable]
          public class MyClass { }
       
       3. [NonSerialized] - исключает поле из сериализации
          [NonSerialized]
          private string temporaryData;
       
       4. [Conditional] - условная компиляция методов
          [Conditional("DEBUG")]
          public void DebugMethod() { }
       
       5. [DllImport] - импорт функций из неуправляемых библиотек
          [DllImport("user32.dll")]
          public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
       
       6. [MethodImpl] - указания компилятору о методе
          [MethodImpl(MethodImplOptions.NoInlining)]
          public void MyMethod() { }
       
       7. [CallerMemberName], [CallerFilePath], [CallerLineNumber] - информация о вызывающем коде
          public void Log(string message, [CallerMemberName] string memberName = "")
          {
              Console.WriteLine($"{memberName}: {message}");
          }
    */

    /*
       Создание пользовательских атрибутов
       =================================
       
       Чтобы создать свой атрибут, нужно:
       
       1. Создать класс, наследующийся от System.Attribute
       2. Добавить суффикс "Attribute" к имени класса (необязательно, но рекомендуется)
       3. Применить атрибут [AttributeUsage] для настройки использования
       
       Пример простого атрибута:
       
       [AttributeUsage(AttributeTargets.Class)]
       public class AuthorAttribute : Attribute
       {
           public string Name { get; }
           public string Email { get; set; }
           
           public AuthorAttribute(string name)
           {
               Name = name;
           }
       }
       
       AttributeUsage настраивает:
       - AttributeTargets - к чему можно применять атрибут
       - AllowMultiple - можно ли применять несколько раз
       - Inherited - наследуется ли атрибут
    */

    // Пример пользовательских атрибутов
    
    // Атрибут для описания автора класса
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AuthorAttribute : Attribute
    {
        public string Name { get; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        public AuthorAttribute(string name)
        {
            Name = name;
            CreatedDate = DateTime.Now;
        }
    }

    // Атрибут для валидации свойств
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredAttribute : Attribute
    {
        public string ErrorMessage { get; set; } = "Поле обязательно для заполнения";
    }

    // Атрибут для проверки диапазона значений
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeAttribute : Attribute
    {
        public int Min { get; }
        public int Max { get; }
        public string ErrorMessage { get; set; }

        public RangeAttribute(int min, int max)
        {
            Min = min;
            Max = max;
            ErrorMessage = $"Значение должно быть от {min} до {max}";
        }
    }

    // Атрибут для настройки сериализации
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonPropertyAttribute : Attribute
    {
        public string PropertyName { get; }
        public bool Ignore { get; set; }

        public JsonPropertyAttribute(string propertyName = null)
        {
            PropertyName = propertyName;
        }
    }

    // Атрибут для пометки методов как тестовых
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodAttribute : Attribute
    {
        public string Description { get; set; }
        public string Category { get; set; } = "General";
        public int Priority { get; set; } = 1;
    }

    // Атрибут для настройки кэширования
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CacheableAttribute : Attribute
    {
        public int DurationMinutes { get; }
        public string CacheKey { get; set; }

        public CacheableAttribute(int durationMinutes = 5)
        {
            DurationMinutes = durationMinutes;
        }
    }

    // Примеры использования атрибутов
    [Author("Иван Петров", Email = "ivan@example.com")]
    public class Person
    {
        [Required(ErrorMessage = "Имя обязательно")]
        [JsonProperty("full_name")]
        public string Name { get; set; }

        [Range(0, 150, ErrorMessage = "Возраст должен быть от 0 до 150")]
        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty(Ignore = true)]
        public string InternalId { get; set; }

        [Author("Мария Сидорова")]
        [Cacheable(10, CacheKey = "person_info")]
        public string GetInfo()
        {
            return $"Имя: {Name}, Возраст: {Age}";
        }

        [TestMethod(Description = "Проверка валидности возраста", Category = "Validation", Priority = 2)]
        public bool IsValidAge()
        {
            return Age >= 0 && Age <= 150;
        }
    }

    public class AttributeExamples
    {
        public static void ReadClassAttributes()
        {
            Console.WriteLine("=== Чтение атрибутов класса ===");

            Type personType = typeof(Person);

            // Получение атрибута Author у класса
            AuthorAttribute authorAttr = personType.GetCustomAttribute<AuthorAttribute>();
            if (authorAttr != null)
            {
                Console.WriteLine($"Автор класса: {authorAttr.Name}");
                Console.WriteLine($"Email: {authorAttr.Email}");
                Console.WriteLine($"Дата создания: {authorAttr.CreatedDate}");
            }
            Console.WriteLine();
        }

        public static void ReadPropertyAttributes()
        {
            Console.WriteLine("=== Чтение атрибутов свойств ===");

            Type personType = typeof(Person);
            PropertyInfo[] properties = personType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine($"Свойство: {property.Name}");

                // Проверка атрибута Required
                RequiredAttribute requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttr != null)
                {
                    Console.WriteLine($"  - Обязательное поле: {requiredAttr.ErrorMessage}");
                }

                // Проверка атрибута Range
                RangeAttribute rangeAttr = property.GetCustomAttribute<RangeAttribute>();
                if (rangeAttr != null)
                {
                    Console.WriteLine($"  - Диапазон: {rangeAttr.Min}-{rangeAttr.Max}");
                }

                // Проверка атрибута JsonProperty
                JsonPropertyAttribute jsonAttr = property.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonAttr != null)
                {
                    if (jsonAttr.Ignore)
                    {
                        Console.WriteLine($"  - Игнорировать при сериализации");
                    }
                    else if (!string.IsNullOrEmpty(jsonAttr.PropertyName))
                    {
                        Console.WriteLine($"  - JSON имя: {jsonAttr.PropertyName}");
                    }
                }

                Console.WriteLine();
            }
        }

        public static void ReadMethodAttributes()
        {
            Console.WriteLine("=== Чтение атрибутов методов ===");

            Type personType = typeof(Person);
            MethodInfo[] methods = personType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                Console.WriteLine($"Метод: {method.Name}");

                // Проверка атрибута Author
                AuthorAttribute authorAttr = method.GetCustomAttribute<AuthorAttribute>();
                if (authorAttr != null)
                {
                    Console.WriteLine($"  - Автор: {authorAttr.Name}");
                }

                // Проверка атрибута Cacheable
                CacheableAttribute cacheAttr = method.GetCustomAttribute<CacheableAttribute>();
                if (cacheAttr != null)
                {
                    Console.WriteLine($"  - Кэшировать на {cacheAttr.DurationMinutes} минут");
                    if (!string.IsNullOrEmpty(cacheAttr.CacheKey))
                    {
                        Console.WriteLine($"  - Ключ кэша: {cacheAttr.CacheKey}");
                    }
                }

                // Проверка атрибута TestMethod
                TestMethodAttribute testAttr = method.GetCustomAttribute<TestMethodAttribute>();
                if (testAttr != null)
                {
                    Console.WriteLine($"  - Тестовый метод: {testAttr.Description}");
                    Console.WriteLine($"  - Категория: {testAttr.Category}");
                    Console.WriteLine($"  - Приоритет: {testAttr.Priority}");
                }

                Console.WriteLine();
            }
        }

        public static void ValidateObject()
        {
            Console.WriteLine("=== Валидация объекта через атрибуты ===");

            Person person1 = new Person { Name = "Алексей", Age = 25 };
            Person person2 = new Person { Name = "", Age = 200 };

            ValidatePerson(person1);
            ValidatePerson(person2);
        }

        private static void ValidatePerson(Person person)
        {
            Console.WriteLine($"Валидация объекта: Name='{person.Name}', Age={person.Age}");

            Type personType = typeof(Person);
            PropertyInfo[] properties = personType.GetProperties();
            bool isValid = true;

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(person);

                // Проверка Required
                RequiredAttribute requiredAttr = property.GetCustomAttribute<RequiredAttribute>();
                if (requiredAttr != null)
                {
                    if (value == null || (value is string str && string.IsNullOrEmpty(str)))
                    {
                        Console.WriteLine($"  ❌ {property.Name}: {requiredAttr.ErrorMessage}");
                        isValid = false;
                    }
                }

                // Проверка Range
                RangeAttribute rangeAttr = property.GetCustomAttribute<RangeAttribute>();
                if (rangeAttr != null && value is int intValue)
                {
                    if (intValue < rangeAttr.Min || intValue > rangeAttr.Max)
                    {
                        Console.WriteLine($"  ❌ {property.Name}: {rangeAttr.ErrorMessage}");
                        isValid = false;
                    }
                }
            }

            if (isValid)
            {
                Console.WriteLine("  ✅ Объект валиден");
            }

            Console.WriteLine();
        }

        public static void SerializeToJson()
        {
            Console.WriteLine("=== Сериализация в JSON через атрибуты ===");

            Person person = new Person 
            { 
                Name = "Анна Иванова", 
                Age = 30, 
                InternalId = "INTERNAL_123" 
            };

            string json = ConvertToJson(person);
            Console.WriteLine($"JSON: {json}");
            Console.WriteLine();
        }

        private static string ConvertToJson(object obj)
        {
            if (obj == null) return "null";

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            List<string> jsonParts = new List<string>();

            foreach (PropertyInfo property in properties)
            {
                JsonPropertyAttribute jsonAttr = property.GetCustomAttribute<JsonPropertyAttribute>();
                
                // Пропускаем свойства с Ignore = true
                if (jsonAttr?.Ignore == true)
                    continue;

                object value = property.GetValue(obj);
                string propertyName = jsonAttr?.PropertyName ?? property.Name;
                
                string jsonValue;
                if (value == null)
                {
                    jsonValue = "null";
                }
                else if (value is string)
                {
                    jsonValue = $"\"{value}\"";
                }
                else
                {
                    jsonValue = value.ToString();
                }

                jsonParts.Add($"\"{propertyName}\": {jsonValue}");
            }

            return "{" + string.Join(", ", jsonParts) + "}";
        }

        public static void RunTestMethods()
        {
            Console.WriteLine("=== Запуск тестовых методов ===");

            Person person = new Person { Name = "Тестовый пользователь", Age = 25 };
            Type personType = typeof(Person);
            MethodInfo[] methods = personType.GetMethods();

            foreach (MethodInfo method in methods)
            {
                TestMethodAttribute testAttr = method.GetCustomAttribute<TestMethodAttribute>();
                if (testAttr != null)
                {
                    Console.WriteLine($"Запуск теста: {testAttr.Description}");
                    Console.WriteLine($"Категория: {testAttr.Category}, Приоритет: {testAttr.Priority}");
                    
                    try
                    {
                        object result = method.Invoke(person, null);
                        Console.WriteLine($"Результат: {result}");
                        Console.WriteLine(result.Equals(true) ? "✅ PASSED" : "❌ FAILED");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ ERROR: {ex.Message}");
                    }
                    
                    Console.WriteLine();
                }
            }
        }

        public static void GetAllAttributesInfo()
        {
            Console.WriteLine("=== Полная информация об атрибутах ===");

            Type personType = typeof(Person);

            // Все атрибуты класса
            Console.WriteLine("Атрибуты класса:");
            Attribute[] classAttributes = Attribute.GetCustomAttributes(personType);
            foreach (Attribute attr in classAttributes)
            {
                Console.WriteLine($"  - {attr.GetType().Name}: {attr}");
            }
            Console.WriteLine();

            // Все атрибуты свойств
            Console.WriteLine("Атрибуты свойств:");
            PropertyInfo[] properties = personType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Attribute[] propAttributes = Attribute.GetCustomAttributes(property);
                if (propAttributes.Length > 0)
                {
                    Console.WriteLine($"  {property.Name}:");
                    foreach (Attribute attr in propAttributes)
                    {
                        Console.WriteLine($"    - {attr.GetType().Name}");
                    }
                }
            }
            Console.WriteLine();

            // Все атрибуты методов
            Console.WriteLine("Атрибуты методов:");
            MethodInfo[] methods = personType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                Attribute[] methodAttributes = Attribute.GetCustomAttributes(method);
                if (methodAttributes.Length > 0)
                {
                    Console.WriteLine($"  {method.Name}:");
                    foreach (Attribute attr in methodAttributes)
                    {
                        Console.WriteLine($"    - {attr.GetType().Name}");
                    }
                }
            }
        }

        public static void RunAllExamples()
        {
            ReadClassAttributes();
            ReadPropertyAttributes();
            ReadMethodAttributes();
            ValidateObject();
            SerializeToJson();
            RunTestMethods();
            GetAllAttributesInfo();
        }
    }

    /*
       Практические применения атрибутов
       ==============================
       
       1. Валидация данных
          - Data Annotations в ASP.NET Core
          - Автоматическая проверка моделей
          - Пользовательские правила валидации
       
       2. Сериализация
          - JSON.NET использует атрибуты для настройки
          - XML сериализация
          - Пользовательские форматы данных
       
       3. ORM (Object-Relational Mapping)
          - Entity Framework использует атрибуты для настройки моделей
          - Маппинг свойств на колонки БД
          - Настройка связей между таблицами
       
       4. Web API
          - Атрибуты маршрутизации [Route], [HttpGet], [HttpPost]
          - Авторизация [Authorize]
          - Фильтры действий
       
       5. Тестирование
          - [Test], [TestMethod] для тестовых фреймворков
          - [SetUp], [TearDown] для инициализации
          - [Category] для группировки тестов
       
       6. Документация
          - XML комментарии с атрибутами
          - Swagger/OpenAPI документация
          - Автогенерация справки
       
       Лучшие практики:
       - Используй атрибуты для декларативного программирования
       - Не злоупотребляй атрибутами - они должны быть понятными
       - Кэшируй результаты чтения атрибутов для производительности
       - Документируй свои пользовательские атрибуты
       - Используй AttributeUsage для ограничения применения
    */
    #endregion

    #region Задачи
    /*
        # Создайте атрибут [DisplayName] для задания отображаемого имени свойств
          и метод GetDisplayNames, который возвращает словарь с именами свойств
          и их отображаемыми именами. Если атрибут не задан, используйте имя свойства.
        
        # Реализуйте атрибут [Benchmark] для измерения времени выполнения методов.
          Создайте класс BenchmarkRunner, который находит все методы с этим атрибутом
          и выводит статистику их выполнения (время, количество вызовов).
        
        # Напишите атрибут [ConfigValue] для автоматической загрузки значений
          из конфигурации в свойства класса. Атрибут должен принимать ключ конфигурации
          и значение по умолчанию. Создайте метод LoadConfig для заполнения объекта.
        
        # Создайте систему атрибутов для описания REST API: [ApiController], [ApiMethod],
          [ApiParameter]. Реализуйте класс ApiDocumentationGenerator, который генерирует
          документацию API в текстовом формате на основе этих атрибутов.
        
        # Реализуйте атрибут [Retry] для автоматического повтора выполнения методов
          при возникновении исключений. Атрибут должен принимать количество попыток
          и задержку между ними. Создайте прокси-класс для перехвата вызовов методов.
    */
    #endregion
}
