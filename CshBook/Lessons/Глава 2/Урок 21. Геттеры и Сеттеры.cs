namespace CshBook.Lessons.Chapter2.Lesson21GettersAndSetters
{
    #region Теория
    /*
        В первых ООП-уроках мы использовали public-поля:

        public string Name;
        public int Age;

        Это просто, но небезопасно.
        Любой код может записать туда любое значение.
     */

    /*
        Пример проблемы:

        user.Age = -100;

        Для программы это обычная запись в поле,
        но для смысла предметной области такой возраст невозможен.
     */

    /*
        Свойство позволяет управлять чтением и записью значения.

        У свойства есть:

        - get: что делать при чтении;
        - set: что делать при записи.
     */

    /*
        В set есть специальное слово value.

        value - это новое значение,
        которое пытаются записать в свойство.

        Например:
        user.Age = 25;

        Внутри set value будет равно 25.
     */

    /*
        Полный вариант свойства обычно работает
        через приватное поле:

        private int _age;

        public int Age
        {
            get { return _age; }
            set { ... }
        }

        Приватное поле хранит данные,
        а свойство контролирует доступ к ним.
     */

    /*
        Есть и автоматические свойства:

        public string Name { get; set; }

        Они удобны, когда проверка не нужна.
        Компилятор сам создаст скрытое поле.
     */

    /*
        Иногда запись нужно закрыть снаружи:

        public int Balance { get; private set; }

        Читать Balance можно откуда угодно,
        но менять его можно только внутри класса.
     */
    #endregion

    class UserWithFields
    {
        public string Name = "";
        public int Age;
    }

    class UserWithProperties
    {
        private int _age;

        public string Name { get; set; } = "";
        public string Email { get; set; } = "";

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value < 0 || value > 120)
                {
                    Console.WriteLine("Возраст должен быть от 0 до 120.");
                    return;
                }

                _age = value;
            }
        }

        public string Info
        {
            get
            {
                return $"{Name}, {Age} лет, email: {Email}";
            }
        }
    }

    class BankAccountExample
    {
        public string AccountNumber { get; }
        public int Balance { get; private set; }

        public BankAccountExample(string accountNumber, int startBalance)
        {
            AccountNumber = accountNumber;
            Balance = startBalance;
        }

        public void Deposit(int amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }
    }

    internal static class Lesson21GettersAndSetters
    {
        public static void Main_()
        {
            UserWithFields badUser = new UserWithFields();
            badUser.Name = "Иван";
            badUser.Age = -100;
            Console.WriteLine($"{badUser.Name}: {badUser.Age}");

            Console.WriteLine("----");

            UserWithProperties user = new UserWithProperties();
            user.Name = "Мария";
            user.Email = "maria@example.com";
            user.Age = 25;
            Console.WriteLine(user.Info);

            user.Age = -5;
            Console.WriteLine(user.Info);

            Console.WriteLine("----");

            BankAccountExample account = new BankAccountExample("ACC-1", 1000);
            account.Deposit(500);
            Console.WriteLine($"{account.AccountNumber}: {account.Balance}");
        }
    }

    #region Задачи
    /*
        Разминка

        1. Пользователь.
           Создай класс User со свойствами Name и Email.
           Используй автоматические свойства.

        2. Возраст с проверкой.
           Добавь приватное поле _age и свойство Age.
           Возраст должен быть от 0 до 120.

        3. Информация.
           Добавь свойство Info только для чтения,
           которое возвращает строку с именем и возрастом.

        Основные задачи

        4. Банковский счет.
           Создай класс BankAccount.
           Добавь свойство AccountNumber только для чтения.

        5. Баланс.
           Добавь свойство Balance с private set.
           Баланс нельзя менять напрямую снаружи.

        6. Пополнение.
           Добавь метод Deposit(int amount),
           который увеличивает Balance только на положительную сумму.

        7. Снятие.
           Добавь метод Withdraw(int amount),
           который уменьшает Balance,
           если сумма положительная и денег достаточно.

        8. Описание счета.
           Добавь свойство Info только для чтения
           с номером счета и балансом.

        Задачи на перенос

        9. Товар.
           Создай класс Product со свойствами Title, Price и Count.
           Price и Count не должны становиться отрицательными.

        10. Умный массив.
            Создай класс SmartArray с приватным массивом.
            Добавь свойства Length, Sum и Average только для чтения.
     */
    #endregion
}
