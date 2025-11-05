using System;

namespace CshBook.Lessons
{
    /* Урок 22: Инкапсуляция в C#
     
    Инкапсуляция - один из ключевых принципов ООП, заключающийся в:
    - Сокрытии внутренней реализации объекта
    - Защите данных от некорректного доступа
    - Управлении доступом к состоянию объекта
    - Объединении данных и методов работы с ними в единую сущность
     */

    #region Пример без инкапсуляции (проблемный подход)
    class NakedAccount
    {
        public decimal Balance; // Публичное поле - опасно!

        public void PrintBalance()
        {
            Console.WriteLine($"Баланс: {Balance}");
        }
    }
    #endregion

    #region Пример с инкапсуляцией (правильный подход)
    class ProtectedAccount
    {
        private decimal _balance; // Приватное поле
        private string _password;

        public ProtectedAccount(string password, decimal initialBalance)
        {
            _password = password;
            _balance = initialBalance;
        }

        // Публичные методы для работы с балансом
        public void Deposit(decimal amount, string password)
        {
            ValidatePassword(password);
            if (amount <= 0) throw new ArgumentException("Сумма должна быть положительной");
            _balance += amount;
        }

        public void Withdraw(decimal amount, string password)
        {
            ValidatePassword(password);
            if (amount <= 0) throw new ArgumentException("Сумма должна быть положительной");
            if (amount > _balance) throw new InvalidOperationException("Недостаточно средств");
            _balance -= amount;
        }

        public void PrintBalance(string password)
        {
            ValidatePassword(password);
            Console.WriteLine($"Баланс: {_balance}");
        }

        private void ValidatePassword(string password)
        {
            if (_password != password)
                throw new UnauthorizedAccessException("Неверный пароль");
        }
    }
    #endregion

    internal class TwentySecondLesson
    {
        public static void Main_()
        {
            // Пример без инкапсуляции
            NakedAccount acc1 = new NakedAccount();
            acc1.Balance = -1000000; // Некорректное значение
            acc1.PrintBalance();     // Баланс: -1000000

            // Пример с инкапсуляцией
            ProtectedAccount acc2 = new ProtectedAccount("qwerty", 1000);

            // Попытка прямого доступа к полям:
            // acc2._balance = 5000; // Ошибка компиляции
            // acc2._password = "new"; // Ошибка компиляции

            acc2.Deposit(500, "qwerty");
            // acc2.Withdraw(2000, "wrong_pass"); // Выбросит исключение
            acc2.PrintBalance("qwerty"); // Баланс: 1500
        }
    }

    /* Ключевые аспекты инкапсуляции:
     1. Модификаторы доступа (private, protected, internal)
     2. Свойства вместо публичных полей
     3. Методы для управления состоянием
     4. Валидация данных при изменении состояния
     5. Сокрытие сложной внутренней логики
     */


    /*
     * Модификаторы доступа в C#
     * 
     * Модификаторы доступа определяют видимость и доступность членов класса:
     * 
     * 1. private (по умолчанию для полей класса)
     *    - Доступ только внутри класса/структуры
     *    - Пример:
    */
    class Example
    {
        private int _secret; // Видно только внутри Example1
    }

    /*
     * 2. protected
     *    - Доступ внутри класса и производных классов
     *    - Пример:
    */
    class BaseClass
    {
        protected int x; // Видно в BaseClass и наследниках
    }

    class DerivedClass : BaseClass
    {
        void Method() => x = 10; // OK
    }

    /*
     * 3. internal
     *    - Доступ в пределах сборки (проекта)
     *    - Пример:
    */
    internal class InternalClass { } // Видна во всей сборке

    /*
     * 4. protected internal
     *    - Объединение protected и internal
     *    - Доступен: 
     *      - В текущей сборке
     *      - В производных классах других сборок
     *    - Пример:
    */
    public class Example4
    {
        protected internal int hybridValue;
    }

    /*
     * 5. public
     *    - Полный доступ из любого места
     *    - Пример:
    */
    public class PublicClass
    {
        public string OpenData; // Доступна везде
    }

    /*
     * Особые случаи:
     * - Для членов интерфейса: по умолчанию public (нельзя указывать модификаторы)
     * - Для структур: по умолчанию private (как у классов)
     * - Для пространств имен: всегда public (неявно)
     * 
     * Таблица видимости:
     * 
     * | Модификатор     | Текущий класс | Наследники | Сборка  | Вне сборки |
     * |-----------------|---------------|------------|---------|------------|
     * | private         |       ✓       |     ✗      |    ✗    |     ✗      |
     * | protected       |       ✓       |     ✓      |    ✗    |     ✗      |
     * | internal        |       ✓       |     ✗      |    ✓    |     ✗      |
     * | protected internal|     ✓       |     ✓      |    ✓    |     ✗      |
     * | public          |       ✓       |     ✓      |    ✓    |     ✓      |
     */



    #region Задание
    /* Создайте класс Temperature с инкапсуляцией:
     - Приватное поле _celsius
     - Публичное свойство Celsius с валидацией (-273.15 <= value)
     - Свойство Fahrenheit (только чтение) с преобразованием
     - Метод SetFromFahrenheit(double fahr)
     - Конструктор с установкой в градусах Цельсия
     */

    #endregion

    #region Творческое задание
    /* Реализуйте класс SmartHouse с инкапсуляцией:
     - Приватные поля: температура, освещенность, безопасный режим
     - Публичные методы: SetTemperature, AdjustLight, ToggleSecurity
     - Приватные методы: CheckClimateConditions, UpdateSecurityLog
     - Свойства только для чтения: CurrentStatus, EnergyConsumption
     - Валидация входных значений в методах
     */

    
    #endregion

    /* Преимущества инкапсуляции:
     1. Защита от некорректных данных
     2. Возможность изменения внутренней реализации без влияния на клиентов
     3. Упрощение использования сложных систем
     4. Улучшение безопасности данных
     5. Централизация логики управления состоянием
     */

    #region Советы по инкапсуляции
    class EncapsulationTips
    {
        // 1. Всегда делайте поля приватными по умолчанию
        private int _hiddenData;

        // 2. Используйте свойства вместо публичных полей
        public string SafeData { get; private set; }

        // 3. Группируйте связанные поля в классы
        private class EngineInternals
        {
            public int RPM;
            public double OilPressure;
        }

        // 4. Разделяйте методы на публичные и приватные
        public void PublicMethod()
        {
            // Вызывает внутренние методы
            PrivateMethod();
        }

        private void PrivateMethod()
        {
            // Скрытая реализация
        }

        // 5. Используйте фабричные методы для сложной инициализации
        public static EncapsulationTips CreateAdvanced()
        {
            var obj = new EncapsulationTips();
            // Сложная логика инициализации
            return obj;
        }
    }
    #endregion
}