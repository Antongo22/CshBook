using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_3
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о Nullable типах и операторах null-объединения в C#:
     * 
     * - Что такое Nullable типы и зачем они нужны
     * - Синтаксис объявления и использования Nullable типов
     * - Операторы для работы с Nullable: ??, ?.
     * - Паттерн "защита от null" (null guard pattern)
     * - Null-условные операторы в современном C#
     */

    /*
       Что такое Nullable типы и зачем они нужны
       =====================================
       
       В C# типы делятся на две категории:
       
       1. Типы-значения (value types) - структуры, перечисления, примитивные типы (int, bool, double и т.д.)
          Они всегда имеют значение и не могут быть null.
          
       2. Ссылочные типы (reference types) - классы, интерфейсы, делегаты, массивы, string
          Они могут быть null, что означает отсутствие значения или ссылки на объект.
       
       Иногда нам нужно, чтобы типы-значения могли принимать значение null,
       например, для представления "неизвестного" или "отсутствующего" значения.
       Для этого в C# были введены Nullable типы.
       
       Примеры ситуаций, когда нужны Nullable типы:
       
       1. Работа с базами данных, где поле может иметь значение NULL
       2. Указание необязательных параметров числового типа (например, возраст, который может быть не указан)
       3. Логика, требующая различать отсутствие значения от нулевого значения
    */

    /*
       Синтаксис объявления и использования Nullable типов
       =============================================
       
       Существует два способа объявления Nullable типов:
       
       1. С использованием типа Nullable<T>:
          Nullable<int> nullableInt = null;
          
       2. С использованием сокращенного синтаксиса "?":
          int? nullableInt = null;
       
       Оба варианта эквивалентны, но второй более распространен благодаря краткости.
       
       Nullable тип имеет два ключевых свойства:
       
       1. HasValue - логическое значение, указывающее, есть ли значение (не null)
       2. Value - само значение (если HasValue == true)
       
       Пример использования:
       
       int? number = null;
       
       if (number.HasValue)
       {
           Console.WriteLine($"Значение: {number.Value}");
       }
       else
       {
           Console.WriteLine("Значение отсутствует (null)");
       }
       
       Важно! Попытка обращения к Value, когда HasValue == false,
       вызовет исключение InvalidOperationException.
    */

    /*
       Оператор null-объединения (??)
       =========================
       
       Оператор ?? (null-coalescing operator) позволяет указать значение по умолчанию,
       которое будет использовано, если выражение слева от оператора равно null.
       
       Синтаксис: <выражение> ?? <значение_по_умолчанию>
       
       Примеры:
       
       int? nullableNumber = null;
       int number = nullableNumber ?? 0;  // number = 0, так как nullableNumber == null
       
       string name = null;
       string displayName = name ?? "Гость";  // displayName = "Гость"
       
       Можно также использовать цепочку операторов ??:
       
       string result = firstChoice ?? secondChoice ?? thirdChoice ?? "Default";
       
       С C# 8.0 появился оператор ??= (null-coalescing assignment),
       который присваивает значение переменной только если она равна null:
       
       string name = null;
       name ??= "Гость";  // name = "Гость", так как name был null
       
       string title = "Мистер";
       title ??= "Гость";  // title остается "Мистер", так как title не был null
    */

    /*
       Null-условный оператор (?.)
       =======================
       
       Оператор ?. (null-conditional operator) позволяет безопасно обращаться к членам объекта,
       который может быть null. Если объект равен null, вся выражение возвращает null
       вместо генерации исключения NullReferenceException.
       
       Примеры:
       
       // Без null-условного оператора
       if (person != null)
       {
           string name = person.Name;
       }
       
       // С null-условным оператором
       string name = person?.Name;  // name будет null, если person == null
       
       Можно комбинировать с оператором ??:
       
       string name = person?.Name ?? "Неизвестно";
       
       Оператор ?. также работает с индексаторами:
       
       string firstItem = items?[0];  // Не выбросит исключение, если items == null
       
       И может использоваться в цепочке вызовов:
       
       string city = person?.Address?.City;  // Безопасная цепочка вызовов
    */

    /*
       Null-условный оператор для вызова событий
       ====================================
       
       Специальный синтаксис ?. используется для безопасного вызова событий:
       
       PropertyChanged?.Invoke(this, args);
       
       Это эквивалентно проверке:
       
       if (PropertyChanged != null)
       {
           PropertyChanged(this, args);
       }
       
       Такой подход защищает от гонки данных (race condition) при многопоточной работе
       и делает код более кратким.
    */

    /*
       Паттерн "защита от null" (null guard pattern)
       =======================================
       
       Часто в методах необходимо проверять аргументы на null в начале:
       
       public void ProcessData(Data data)
       {
           if (data == null)
           {
               throw new ArgumentNullException(nameof(data));
           }
           
           // Остальной код метода...
       }
       
       Начиная с C# 7.0 можно использовать is pattern:
       
       if (!(data is Data validData))
       {
           throw new ArgumentNullException(nameof(data));
       }
       
       Или более современный синтаксис с C# 9.0:
       
       public void ProcessData(Data data)
       {
           ArgumentNullException.ThrowIfNull(data);
           
           // Остальной код метода...
       }
    */

    /*
       Nullable-контекст (C# 8.0 и выше)
       ===========================
       
       Начиная с C# 8.0, в языке появилась возможность включать "nullable context" 
       для более строгой проверки работы с null.
       
       Это можно сделать с помощью директив компилятора:
       
       #nullable enable   // Включение проверок на null
       #nullable disable  // Отключение проверок
       #nullable restore  // Восстановление предыдущего состояния
       
       Или на уровне проекта через файл .csproj:
       
       <PropertyGroup>
         <Nullable>enable</Nullable>
       </PropertyGroup>
       
       При включенном контексте:
       - Ссылочные типы по умолчанию считаются non-nullable (не должны быть null)
       - Для nullable ссылочных типов используется тот же синтаксис "?": string?
       - Компилятор выдает предупреждения при возможном null-reference
       
       Пример:
       
       #nullable enable
       
       public void Process(string text)  // text не должен быть null
       {
           Console.WriteLine(text.Length); // Безопасно, компилятор гарантирует
       }
       
       public void MaybeProcess(string? text)  // text может быть null
       {
           if (text != null)
           {
               Console.WriteLine(text.Length);
           }
       }
    */
    #endregion

    #region Примеры использования Nullable типов
    // Класс для демонстрации
    public class Person
    {
        public string Name { get; set; }
        public int? Age { get; set; }  // Возраст может быть неизвестен (null)
        public DateTime? BirthDate { get; set; }  // Дата рождения может быть неизвестна
        public Address Address { get; set; }

        public override string ToString()
        {
            // Используем оператор null-объединения для вывода текста по умолчанию
            string ageText = Age.HasValue ? Age.ToString() : "неизвестен";
            string birthDateText = BirthDate?.ToString("dd.MM.yyyy") ?? "не указана";
            
            return $"Имя: {Name}, Возраст: {ageText}, Дата рождения: {birthDateText}";
        }
    }

    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        
        public override string ToString()
        {
            return $"{City}, {Street}, {Building}";
        }
    }
    
    // Демонстрация работы с базой данных (имитация)
    public class DatabaseSimulator
    {
        // Метод, имитирующий запрос к БД
        public static int? GetUserAge(int userId)
        {
            // Предположим, что в БД есть только пользователи с ID 1, 2, 3
            if (userId >= 1 && userId <= 3)
            {
                return userId * 10;  // Просто для примера возвращаем возраст
            }
            
            // Если пользователь не найден - возвращаем null
            return null;
        }
        
        public static Person GetUser(int userId)
        {
            // Для пользователя с ID 4 вернем null
            if (userId == 4) return null;
            
            // Для остальных создаем объект Person с разными данными
            var person = new Person { Name = $"Пользователь {userId}" };
            
            // ID 1: все данные заполнены
            if (userId == 1)
            {
                person.Age = 25;
                person.BirthDate = new DateTime(1998, 5, 10);
                person.Address = new Address 
                { 
                    City = "Москва", 
                    Street = "Ленина", 
                    Building = "10" 
                };
            }
            // ID 2: только имя и возраст
            else if (userId == 2)
            {
                person.Age = 30;
            }
            // ID 3: только имя
            
            return person;
        }
    }
    #endregion

    internal class NullableTypesLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по Nullable типам и операторам null-объединения ====\n");
            
            Console.WriteLine("--- Основы Nullable типов ---");
            
            // Демонстрация объявления Nullable типов
            int regularInt = 5;
            int? nullableInt = null;  // Используем краткий синтаксис
            Nullable<double> nullableDouble = 3.14;  // Используем полный синтаксис
            
            Console.WriteLine($"regularInt = {regularInt}");
            Console.WriteLine($"nullableInt имеет значение? {nullableInt.HasValue}");
            Console.WriteLine($"nullableDouble = {nullableDouble.Value}");
            
            // Попытка присвоения null обычному типу-значению вызовет ошибку компиляции
            // int wrongInt = null;  // Не скомпилируется
            
            // Присвоение значения null Nullable типу
            nullableDouble = null;
            Console.WriteLine($"После присвоения null: nullableDouble имеет значение? {nullableDouble.HasValue}");
            
            // Проверка на наличие значения перед использованием Value
            if (nullableInt.HasValue)
            {
                Console.WriteLine($"nullableInt = {nullableInt.Value}");
            }
            else
            {
                Console.WriteLine("nullableInt равен null");
            }
            
            // Присваивание значения и проверка заново
            nullableInt = 42;
            Console.WriteLine($"После присвоения: nullableInt = {nullableInt.Value}");
            
            Console.WriteLine("\n--- Оператор null-объединения (??) ---");
            
            // Использование ?? для задания значения по умолчанию
            int? possiblyNullValue = null;
            int definitelyNotNullValue = possiblyNullValue ?? 100;
            
            Console.WriteLine($"possiblyNullValue = {(possiblyNullValue.HasValue ? possiblyNullValue.ToString() : "null")}");
            Console.WriteLine($"definitelyNotNullValue = {definitelyNotNullValue}");
            
            // Цепочка операторов ??
            string firstName = null;
            string middleName = null;
            string lastName = "Смирнов";
            
            string displayName = firstName ?? middleName ?? lastName ?? "Неизвестный пользователь";
            Console.WriteLine($"Отображаемое имя: {displayName}");
            
            // Демонстрация оператора ??=
            string greeting = null;
            greeting ??= "Здравствуйте";  // Присваивается "Здравствуйте", так как greeting == null
            Console.WriteLine($"Приветствие: {greeting}");
            
            greeting ??= "Привет";  // Не изменяется, так как greeting уже имеет значение
            Console.WriteLine($"Приветствие после второго ??=: {greeting}");
            
            Console.WriteLine("\n--- Null-условный оператор (?.) ---");
            
            // Пример безопасного доступа к свойствам через ?.
            Person person = new Person { Name = "Иван", Age = 30 };
            
            // Безопасный доступ к Age
            int? age = person?.Age;
            Console.WriteLine($"Возраст: {age ?? 0}");
            
            // Безопасный доступ к несуществующему свойству Address
            string city = person?.Address?.City;
            Console.WriteLine($"Город: {city ?? "не указан"}");
            
            // Безопасный доступ к свойствам с использованием цепочки и ??
            string addressDisplay = person?.Address?.ToString() ?? "Адрес не указан";
            Console.WriteLine($"Адрес: {addressDisplay}");
            
            // Добавим адрес и проверим снова
            person.Address = new Address { City = "Санкт-Петербург", Street = "Невский пр.", Building = "1" };
            addressDisplay = person?.Address?.ToString() ?? "Адрес не указан";
            Console.WriteLine($"Адрес после добавления: {addressDisplay}");
            
            Console.WriteLine("\n--- Практический пример: работа с \"базой данных\" ---");
            
            // Имитируем запросы к базе данных
            for (int userId = 1; userId <= 5; userId++)
            {
                // Получаем возраст из "БД"
                int? userAge = DatabaseSimulator.GetUserAge(userId);
                
                // Используем null-объединение для отображения результата
                Console.WriteLine($"Пользователь ID {userId}: Возраст = {userAge ?? 0} " +
                                  $"({(userAge.HasValue ? "известен" : "неизвестен")})");
            }
            
            Console.WriteLine("\nПолучение информации о пользователях:");
            
            // Получаем и выводим информацию о нескольких пользователях
            for (int userId = 1; userId <= 4; userId++)
            {
                Person user = DatabaseSimulator.GetUser(userId);
                
                // Безопасная работа с возможно-null объектом
                if (user != null)
                {
                    Console.WriteLine(user);
                    
                    // Демонстрация цепочки null-условных операторов
                    Console.WriteLine($"  Город: {user?.Address?.City ?? "не указан"}");
                }
                else
                {
                    Console.WriteLine($"Пользователь с ID {userId} не найден");
                }
            }
            
            Console.WriteLine("\n--- Паттерн \"защита от null\" ---");
            
            // Метод с защитой от null (определен ниже)
            try
            {
                ProcessPerson(null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Поймано исключение: {ex.Message}");
            }
            
            // Успешный вызов
            ProcessPerson(new Person { Name = "Алексей", Age = 25 });
        }
        
        // Пример метода с защитой от null
        private static void ProcessPerson(Person person)
        {
            // Проверка аргумента на null
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person), "Объект Person не может быть null");
            }
            
            // Теперь безопасно работаем с person
            Console.WriteLine($"Обработка данных для: {person.Name}");
        }
    }

    #region Задачи
    /*
        # Создайте класс UserProfile с полями Name (string), Age (int?), Email (string), 
          и LastLoginDate (DateTime?). Реализуйте метод ToString(), который красиво выводит 
          информацию о пользователе, используя операторы ?? и ?. для обработки null-значений.
        
        # Напишите метод GetAverageAge, который принимает массив объектов UserProfile и 
          вычисляет средний возраст пользователей. Используйте Nullable типы и учитывайте,
          что поле Age может быть null.
        
        # Создайте класс Calculator с методом Divide(int? a, int? b), который делит первое число
          на второе. Обработайте все возможные ситуации с null-значениями и делением на ноль,
          возвращая null в случае невозможности выполнить операцию.
        
        # Реализуйте класс SafeConfig для безопасной работы с настройками. Создайте методы:
          - GetString(string key) - возвращает строку или null
          - GetInt(string key) - возвращает int? (null если настройка отсутствует или не число)
          - GetBool(string key) - возвращает bool? (null если настройка отсутствует)
          Храните настройки в Dictionary<string, string>.
          
        # Создайте методы для безопасной конвертации строк в числа:
          - TryParseInt(string text) - возвращает int?
          - TryParseDouble(string text) - возвращает double?
          - TryParseDateTime(string text) - возвращает DateTime?
          Методы должны возвращать null при невозможности выполнить конвертацию.
    */
    #endregion
}
