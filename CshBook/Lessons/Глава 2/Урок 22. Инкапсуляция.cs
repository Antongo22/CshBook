namespace CshBook.Lessons.Chapter2.Lesson22Encapsulation
{
    #region Теория
    /*
        Инкапсуляция - это принцип ООП,
        при котором объект сам защищает свое состояние.

        Проще:
        поля объекта не должны свободно меняться откуда угодно.
        Объект должен давать понятные методы и свойства
        для работы с собой.
     */

    /*
        Плохой пример:

        account.Balance = -1000000;

        Если Balance - обычное public-поле,
        программа позволит записать некорректное значение.
     */

    /*
        Хороший подход:

        - поле баланса private;
        - снаружи можно вызвать Deposit или Withdraw;
        - методы сами проверяют правила.

        Так объект не дает привести себя
        в неправильное состояние.
     */

    /*
        Инкапсуляция строится из нескольких инструментов:

        - private-поля;
        - public-методы;
        - свойства с get/set;
        - private-вспомогательные методы;
        - проверка данных в одном месте.
     */

    /*
        Модификаторы доступа на этом этапе:

        private - доступ только внутри класса;
        public - доступ снаружи;
        internal - доступ внутри проекта.

        protected разберем подробнее вместе с наследованием.
     */

    /*
        Главное правило:
        сначала делай поле private.

        Если внешнему коду нужно читать значение,
        добавь свойство get.

        Если внешнему коду нужно менять состояние,
        дай метод с понятным названием и проверками.
     */

    /*
        Инкапсуляция не про "спрятать все".
        Она про четкую границу:
        что объект разрешает делать снаружи,
        а что остается его внутренней логикой.
     */
    #endregion

    class OpenAccount
    {
        public int Balance;
    }

    class SafeAccount
    {
        private int _balance;
        private string _pin;

        public string OwnerName { get; }

        public SafeAccount(string ownerName, string pin, int startBalance)
        {
            OwnerName = ownerName;
            _pin = pin;

            if (startBalance > 0)
            {
                _balance = startBalance;
            }
        }

        public string Info
        {
            get
            {
                return $"{OwnerName}: баланс {_balance}";
            }
        }

        public void Deposit(int amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Сумма пополнения должна быть положительной.");
                return;
            }

            _balance += amount;
        }

        public void Withdraw(int amount, string pin)
        {
            if (!IsCorrectPin(pin))
            {
                Console.WriteLine("Неверный PIN.");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Сумма снятия должна быть положительной.");
                return;
            }

            if (amount > _balance)
            {
                Console.WriteLine("Недостаточно средств.");
                return;
            }

            _balance -= amount;
        }

        private bool IsCorrectPin(string pin)
        {
            return _pin == pin;
        }
    }

    internal static class Lesson22Encapsulation
    {
        public static void Main_()
        {
            OpenAccount openAccount = new OpenAccount();
            openAccount.Balance = -1000000;
            Console.WriteLine($"Открытый счет: {openAccount.Balance}");

            Console.WriteLine("----");

            SafeAccount safeAccount = new SafeAccount("Мария", "1234", 1000);
            safeAccount.Deposit(500);
            safeAccount.Withdraw(300, "1234");
            safeAccount.Withdraw(5000, "1234");
            safeAccount.Withdraw(100, "0000");

            Console.WriteLine(safeAccount.Info);
        }
    }

    #region Задачи
    /*
        Разминка

        1. Закрытый баланс.
           Создай класс Wallet.
           Сделай поле _money private.

        2. Чтение баланса.
           Добавь свойство Money только для чтения.

        3. Пополнение.
           Добавь метод AddMoney(int amount),
           который принимает только положительную сумму.

        Основные задачи

        4. Снятие денег.
           Добавь метод SpendMoney(int amount),
           который тратит деньги только если их достаточно.

        5. Приватная проверка.
           Добавь private-метод IsValidAmount(int amount),
           который проверяет, что сумма больше нуля.

        6. Банковский счет.
           Создай класс BankAccount с private-полями _balance и _pin.
           Добавь методы Deposit и Withdraw.

        7. Проверка PIN.
           В BankAccount добавь private-метод IsCorrectPin(string pin).
           Снятие денег должно работать только с правильным PIN.

        8. Температура.
           Создай класс Temperature.
           Храни градусы Цельсия в private-поле.
           Не разрешай устанавливать температуру ниже -273.

        Задачи на перенос

        9. Умный дом.
           Создай класс SmartHouse.
           Внутри храни температуру, свет и режим охраны.
           Снаружи дай методы SetTemperature, SetLight и ToggleSecurity.

        10. Игровой персонаж.
            Создай класс GameCharacter.
            Закрой здоровье в private-поле.
            Добавь методы TakeDamage и Heal с проверками.
     */
    #endregion
}
