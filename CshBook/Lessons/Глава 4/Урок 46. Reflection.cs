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
     * В этом уроке ты узнаешь о Reflection в C#:
     * 
     * - Что такое Reflection и зачем он нужен
     * - Получение информации о типах
     * - Работа с методами, свойствами и полями
     * - Создание объектов через Reflection
     * - Вызов методов через Reflection
     * - Практические применения Reflection
     */

    /*
       Что такое Reflection и зачем он нужен
       ==================================
       
       Reflection (рефлексия) - это возможность программы исследовать и изменять
       свою собственную структуру и поведение во время выполнения.
       
       С помощью Reflection можно:
       
       1. Получать информацию о типах
          - Узнать какие методы, свойства, поля есть у класса
          - Получить информацию о параметрах методов
          - Узнать базовые классы и интерфейсы
       
       2. Создавать объекты динамически
          - Создать экземпляр класса по его имени
          - Вызвать конструктор с параметрами
       
       3. Вызывать методы и обращаться к свойствам
          - Вызвать метод по его имени
          - Получить или установить значение свойства/поля
       
       4. Работать с атрибутами
          - Читать метаданные, добавленные через атрибуты
       
       Основные применения Reflection:
       - Сериализация и десериализация объектов
       - Создание универсальных компонентов
       - Инверсия управления (IoC) контейнеры
       - ORM (Object-Relational Mapping)
       - Тестирование приватных методов
       - Создание инструментов разработки
       
       Важно: Reflection работает медленнее обычного кода, поэтому используй
       его только когда это действительно необходимо.
    */

    /*
       Получение информации о типах
       ===========================
       
       Для работы с Reflection используется класс Type, который содержит
       информацию о типе данных.
       
       Способы получения объекта Type:
       
       1. Через оператор typeof:
          Type stringType = typeof(string);
          Type intType = typeof(int);
       
       2. Через метод GetType() у объекта:
          string text = "Hello";
          Type textType = text.GetType();
       
       3. Через Type.GetType() по имени типа:
          Type listType = Type.GetType("System.Collections.Generic.List`1");
       
       Основные свойства Type:
       - Name - имя типа
       - FullName - полное имя типа с namespace
       - Namespace - пространство имен
       - BaseType - базовый тип
       - IsClass, IsInterface, IsEnum - проверка типа
       - IsPublic, IsAbstract, IsSealed - модификаторы доступа
    */

    /*
       Работа с методами, свойствами и полями
       ====================================
       
       Type предоставляет методы для получения информации о членах класса:
       
       1. Методы:
          - GetMethods() - все публичные методы
          - GetMethod(string name) - метод по имени
          - GetMethods(BindingFlags) - методы с определенными флагами
       
       2. Свойства:
          - GetProperties() - все публичные свойства
          - GetProperty(string name) - свойство по имени
       
       3. Поля:
          - GetFields() - все публичные поля
          - GetField(string name) - поле по имени
       
       4. Конструкторы:
          - GetConstructors() - все публичные конструкторы
          - GetConstructor(Type[] types) - конструктор с определенными параметрами
       
       BindingFlags позволяет указать какие члены искать:
       - Public, NonPublic - публичные/приватные
       - Instance, Static - экземплярные/статические
       - DeclaredOnly - только объявленные в этом типе
       
       Пример:
       BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
       MethodInfo[] methods = typeof(string).GetMethods(flags);
    */

    public class ReflectionExamples
    {
        // Пример класса для демонстрации Reflection
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            private string secret = "Секретная информация";

            public Person() { }

            public Person(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public void SayHello()
            {
                Console.WriteLine($"Привет, меня зовут {Name}");
            }

            public void SetAge(int newAge)
            {
                Age = newAge;
            }

            private void PrivateMethod()
            {
                Console.WriteLine("Это приватный метод");
            }
        }

        public static void GetTypeInformation()
        {
            Console.WriteLine("=== Получение информации о типе ===");

            Type personType = typeof(Person);

            Console.WriteLine($"Имя типа: {personType.Name}");
            Console.WriteLine($"Полное имя: {personType.FullName}");
            Console.WriteLine($"Пространство имен: {personType.Namespace}");
            Console.WriteLine($"Базовый тип: {personType.BaseType?.Name}");
            Console.WriteLine($"Это класс: {personType.IsClass}");
            Console.WriteLine($"Это публичный: {personType.IsPublic}");
            Console.WriteLine();

            // Получение всех публичных свойств
            Console.WriteLine("Публичные свойства:");
            PropertyInfo[] properties = personType.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                Console.WriteLine($"  {prop.PropertyType.Name} {prop.Name}");
            }
            Console.WriteLine();

            // Получение всех публичных методов
            Console.WriteLine("Публичные методы:");
            MethodInfo[] methods = personType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                string parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                Console.WriteLine($"  {method.ReturnType.Name} {method.Name}({parameters})");
            }
        }

        public static void CreateObjectsWithReflection()
        {
            Console.WriteLine("=== Создание объектов через Reflection ===");

            Type personType = typeof(Person);

            // Создание объекта через конструктор без параметров
            object person1 = Activator.CreateInstance(personType);
            Console.WriteLine($"Создан объект: {person1.GetType().Name}");

            // Создание объекта через конструктор с параметрами
            object person2 = Activator.CreateInstance(personType, "Анна", 25);
            Console.WriteLine($"Создан объект: {person2.GetType().Name}");

            // Альтернативный способ через конструктор
            ConstructorInfo constructor = personType.GetConstructor(new Type[] { typeof(string), typeof(int) });
            object person3 = constructor.Invoke(new object[] { "Петр", 30 });
            Console.WriteLine($"Создан объект через конструктор: {person3.GetType().Name}");
            Console.WriteLine();
        }

        public static void WorkWithPropertiesAndFields()
        {
            Console.WriteLine("=== Работа со свойствами и полями ===");

            Person person = new Person("Мария", 28);
            Type personType = typeof(Person);

            // Работа со свойствами
            PropertyInfo nameProperty = personType.GetProperty("Name");
            PropertyInfo ageProperty = personType.GetProperty("Age");

            // Получение значений свойств
            string name = (string)nameProperty.GetValue(person);
            int age = (int)ageProperty.GetValue(person);
            Console.WriteLine($"Имя: {name}, Возраст: {age}");

            // Установка значений свойств
            nameProperty.SetValue(person, "Елена");
            ageProperty.SetValue(person, 32);
            Console.WriteLine($"Новые значения - Имя: {person.Name}, Возраст: {person.Age}");

            // Работа с приватными полями
            FieldInfo secretField = personType.GetField("secret", BindingFlags.NonPublic | BindingFlags.Instance);
            if (secretField != null)
            {
                string secretValue = (string)secretField.GetValue(person);
                Console.WriteLine($"Приватное поле: {secretValue}");
                
                secretField.SetValue(person, "Новый секрет");
                string newSecretValue = (string)secretField.GetValue(person);
                Console.WriteLine($"Обновленное приватное поле: {newSecretValue}");
            }
            Console.WriteLine();
        }

        public static void InvokeMethodsWithReflection()
        {
            Console.WriteLine("=== Вызов методов через Reflection ===");

            Person person = new Person("Алексей", 35);
            Type personType = typeof(Person);

            // Вызов метода без параметров
            MethodInfo sayHelloMethod = personType.GetMethod("SayHello");
            sayHelloMethod.Invoke(person, null);

            // Вызов метода с параметрами
            MethodInfo setAgeMethod = personType.GetMethod("SetAge");
            setAgeMethod.Invoke(person, new object[] { 40 });
            Console.WriteLine($"Возраст после изменения: {person.Age}");

            // Вызов приватного метода
            MethodInfo privateMethod = personType.GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            if (privateMethod != null)
            {
                privateMethod.Invoke(person, null);
            }
            Console.WriteLine();
        }

        public static void WorkWithGenericTypes()
        {
            Console.WriteLine("=== Работа с обобщенными типами ===");

            // Создание List<string> через Reflection
            Type listType = typeof(List<>);
            Type stringListType = listType.MakeGenericType(typeof(string));
            object stringList = Activator.CreateInstance(stringListType);

            // Получение метода Add
            MethodInfo addMethod = stringListType.GetMethod("Add");
            addMethod.Invoke(stringList, new object[] { "Первый элемент" });
            addMethod.Invoke(stringList, new object[] { "Второй элемент" });

            // Получение свойства Count
            PropertyInfo countProperty = stringListType.GetProperty("Count");
            int count = (int)countProperty.GetValue(stringList);
            Console.WriteLine($"Количество элементов в списке: {count}");

            // Получение элементов через индексатор
            PropertyInfo indexer = stringListType.GetProperty("Item");
            for (int i = 0; i < count; i++)
            {
                string item = (string)indexer.GetValue(stringList, new object[] { i });
                Console.WriteLine($"Элемент {i}: {item}");
            }
            Console.WriteLine();
        }

        public static void GetAssemblyInformation()
        {
            Console.WriteLine("=== Информация о сборке ===");

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Console.WriteLine($"Имя сборки: {currentAssembly.GetName().Name}");
            Console.WriteLine($"Версия: {currentAssembly.GetName().Version}");
            Console.WriteLine($"Полное имя: {currentAssembly.FullName}");

            // Получение всех типов в сборке
            Console.WriteLine("\nТипы в текущей сборке:");
            Type[] types = currentAssembly.GetTypes();
            foreach (Type type in types.Take(5)) // Показываем только первые 5
            {
                Console.WriteLine($"  {type.FullName}");
            }
            Console.WriteLine($"  ... и еще {types.Length - 5} типов");
            Console.WriteLine();
        }

        public static void PracticalExample_ObjectCloner()
        {
            Console.WriteLine("=== Практический пример: Клонирование объектов ===");

            Person original = new Person("Оригинал", 25);
            Person clone = CloneObject(original);

            Console.WriteLine($"Оригинал: {original.Name}, {original.Age}");
            Console.WriteLine($"Клон: {clone.Name}, {clone.Age}");

            // Изменяем клон
            clone.Name = "Клон";
            clone.Age = 30;

            Console.WriteLine("После изменения клона:");
            Console.WriteLine($"Оригинал: {original.Name}, {original.Age}");
            Console.WriteLine($"Клон: {clone.Name}, {clone.Age}");
        }

        // Универсальный метод клонирования объектов
        public static T CloneObject<T>(T source) where T : new()
        {
            if (source == null) return default(T);

            T clone = new T();
            Type type = typeof(T);

            // Копируем все публичные свойства
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(source);
                    property.SetValue(clone, value);
                }
            }

            return clone;
        }

        public static void Main_()
        {
            GetTypeInformation();
            CreateObjectsWithReflection();
            WorkWithPropertiesAndFields();
            InvokeMethodsWithReflection();
            WorkWithGenericTypes();
            GetAssemblyInformation();
            PracticalExample_ObjectCloner();
        }
    }

    /*
       Практические применения Reflection
       ===============================
       
       1. Сериализация и десериализация
          - JSON.NET использует Reflection для преобразования объектов
          - Автоматическое сопоставление свойств объекта с JSON
       
       2. ORM (Object-Relational Mapping)
          - Entity Framework использует Reflection для работы с моделями
          - Автоматическое создание SQL запросов на основе свойств класса
       
       3. Dependency Injection контейнеры
          - Автоматическое создание объектов и внедрение зависимостей
          - Регистрация сервисов по типам
       
       4. Валидация данных
          - Проверка атрибутов валидации на свойствах
          - Автоматическая генерация правил валидации
       
       5. Тестирование
          - Доступ к приватным методам для unit-тестирования
          - Создание mock-объектов
       
       6. Плагины и расширения
          - Динамическая загрузка сборок
          - Поиск и создание объектов определенного типа
       
       Рекомендации по использованию:
       - Кэшируй результаты Reflection операций
       - Используй Expression Trees для лучшей производительности
       - Рассмотри альтернативы (generics, interfaces) перед использованием Reflection
       - Обрабатывай исключения при работе с Reflection
    */
    #endregion

    #region Задачи
    /*
        # Создайте класс PropertyCopier, который может копировать значения свойств
          из одного объекта в другой, если у них есть свойства с одинаковыми именами
          и совместимыми типами. Метод должен работать с любыми типами объектов.
        
        # Реализуйте простой IoC контейнер, который может регистрировать типы
          и создавать их экземпляры через Reflection. Контейнер должен поддерживать
          внедрение зависимостей через конструктор (если зависимости уже зарегистрированы).
        
        # Напишите класс ObjectValidator, который проверяет объекты на основе
          пользовательских атрибутов валидации. Создайте атрибуты RequiredAttribute
          и RangeAttribute, и реализуйте их проверку через Reflection.
        
        # Создайте универсальный метод ToJson, который преобразует любой объект
          в JSON строку, используя Reflection для получения всех публичных свойств.
          Метод должен корректно обрабатывать вложенные объекты и коллекции.
        
        # Реализуйте класс MethodProfiler, который может измерять время выполнения
          любого метода объекта через Reflection. Создайте атрибут ProfileAttribute
          и автоматически профилируйте все методы, помеченные этим атрибутом.
    */
    #endregion
}
