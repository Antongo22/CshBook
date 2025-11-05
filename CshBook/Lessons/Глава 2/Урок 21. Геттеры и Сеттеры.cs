using System;

namespace CshBook.Lessons
{
    /* Урок 21: Геттеры и Сеттеры в C#
     
    Геттеры (get) и Сеттеры (set) - это специальные методы доступа к полям класса,
    которые позволяют контролировать чтение и запись значений.
    
    Основные цели:
    - Инкапсуляция данных
    - Валидация значений
    - Гибкость при изменении внутренней реализации
    - Добавление дополнительной логики при доступе к полям
     */

    #region Пример без геттеров/сеттеров (плохая практика)
    class BadUser
    {
        public string Name; // Публичное поле - можно изменять напрямую
        public int Age;     // Нет контроля за значениями
    }
    #endregion

    #region Пример с геттерами/сеттерами (правильный подход)
    class User
    {
        private string _name;
        private int _age;

        // Свойство с базовым геттером/сеттером
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Свойство с валидацией
        public int Age
        {
            get => _age; // Сокращенная запись геттера
            set
            {
                if (value < 0 || value > 120)
                    throw new ArgumentException("Недопустимый возраст");
                _age = value;
            }
        }

        // Автоматическое свойство (компилятор создаст поле автоматически)
        public string Email { get; set; }

        // Свойство только для чтения
        public string Info => $"{Name}, {Age} лет"; // Вычисляемое свойство
    }
    #endregion

    internal class TwentyFirstLesson
    {
        public static void Main_()
        {
            // Пример с плохим подходом
            BadUser badUser = new BadUser();
            badUser.Name = "Иван";
            badUser.Age = -100; // Некорректный возраст, но ошибки не будет

            // Пример с хорошим подходом
            User user = new User();
            user.Name = "Мария";
            // user.Age = -5; // Выбросит исключение
            user.Age = 25;

            Console.WriteLine(user.Info); // Мария, 25 лет

            // Использование автоматического свойства
            user.Email = "maria@example.com";
            Console.WriteLine(user.Email);
        }
    }

    /* Основные концепции:
     1. get - метод для получения значения
     2. set - метод для установки значения (value - ключевое слово)
     3. Модификаторы доступа (private set, protected get и т.д.)
     */

    #region Задание
    /* Создайте класс BankAccount с:
     - Автоматическим свойством AccountNumber (только чтение)
     - Свойством Balance с private set
     - Методами Deposit и Withdraw (с проверкой на отрицательные значения)
     - Свойством IsClosed (только для записи)
     - Свойством Info (только чтение) с информацией о счете
     */

    class BankAccount
    {
        public string AccountNumber { get; } // Только чтение
        public decimal Balance { get; private set; }
        public bool IsClosed { private get; set; } // Только запись

        public BankAccount(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма должна быть положительной");
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма должна быть положительной");
            if (amount > Balance)
                throw new InvalidOperationException("Недостаточно средств");
            Balance -= amount;
        }

        public string Info =>
            $"Счет: {AccountNumber}, Баланс: {Balance:C}, Статус: {(IsClosed ? "Закрыт" : "Активен")}";
    }
    #endregion

    #region Творческое задание
    /* Реализуйте класс SmartArray:
     - Внутренний массив целых чисел
     - Индексатор с get/set (с проверкой границ)
     - Свойство Length (только чтение)
     - Свойство Sum (только чтение) - возвращает сумму элементов
     - Свойство Average (только чтение) - возвращает среднее
     - Метод Resize(int newSize)
     */

    #endregion

    /* Продвинутые возможности:
     1. init-аксессоры (C# 9.0) - для инициализации только при создании
     2. required модификатор (C# 11) - обязательные свойства
     3. Модификаторы доступа к аксессорам
     4. Ковариантные возвращаемые типы свойств
     */

    #region Примеры продвинутых возможностей
    class AdvancedExample
    {
        // Свойство с разными модификаторами доступа
        public string ImportantData { get; protected set; }

        // Init-only свойство
        public string Config { get; init; }

        // Required свойство (C# 11)
        public required string RequiredField { get; set; }
    }

    class UsageExample
    {
        public static void Demo()
        {
            var example = new AdvancedExample
            {
                Config = "Some config",    // Можно установить только при инициализации
                RequiredField = "Value"   // Обязательное поле
            };

            // example.Config = "New value"; // Ошибка - только для инициализации
        }
    }
    #endregion
}