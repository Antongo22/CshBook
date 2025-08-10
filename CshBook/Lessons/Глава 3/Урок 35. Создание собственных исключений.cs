using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace CshBook.Lessons.Глава_3
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о создании собственных исключений в C#:
     * 
     * - Зачем нужны собственные исключения
     * - Иерархия исключений в .NET
     * - Как создавать собственные классы исключений
     * - Стандарты именования и лучшие практики
     * - Сериализация исключений
     * - Разработка исключений с дополнительными данными
     */

    /*
       Зачем нужны собственные исключения
       ===========================
       
       Собственные исключения помогают:
       
       1. Улучшить читаемость кода - из типа исключения сразу понятно, что произошло
       2. Упростить обработку ошибок - можно перехватывать конкретные типы исключений
       3. Предоставить дополнительную информацию об ошибке - через специфические свойства
       4. Разделить логику приложения и логику обработки ошибок
       5. Создать многоуровневый подход к обработке исключений
       
       Например, вместо генерации общего ArgumentException с сообщением вида 
       "Invalid email format", лучше создать специализированное исключение
       InvalidEmailFormatException, что сделает код более понятным и читаемым.
    */

    /*
       Иерархия исключений в .NET
       =====================
       
       Все исключения в .NET наследуются от базового класса System.Exception.
       
       Основные встроенные классы исключений:
       
       - Exception - базовый класс для всех исключений
         - SystemException - базовый класс для исключений, генерируемых CLR
           - ArgumentException - недопустимый аргумент
             - ArgumentNullException - аргумент равен null
             - ArgumentOutOfRangeException - аргумент вне допустимого диапазона
           - InvalidOperationException - недопустимая операция для текущего состояния
           - NullReferenceException - попытка обращения к null-ссылке
           - IndexOutOfRangeException - индекс за пределами массива/коллекции
           - IOException - ошибка ввода/вывода
           - ...и многие другие
         - ApplicationException - базовый класс для пользовательских исключений
           (хотя Microsoft рекомендует наследоваться напрямую от Exception)
       
       При создании собственных исключений следует выбирать подходящий базовый класс.
    */

    /*
       Как создавать собственные классы исключений
       ===================================
       
       Минимальные требования для создания собственного исключения:
       
       1. Наследование от Exception или подходящего производного класса
       2. Реализация трех стандартных конструкторов:
          - Без параметров
          - С сообщением об ошибке
          - С сообщением об ошибке и внутренним исключением (innerException)
       3. Поддержка сериализации (атрибут [Serializable])
       
       Пример простого класса исключения:
       
       [Serializable]
       public class MyCustomException : Exception
       {
           public MyCustomException() : base() { }
           
           public MyCustomException(string message) : base(message) { }
           
           public MyCustomException(string message, Exception innerException) 
               : base(message, innerException) { }
           
           // Конструктор для сериализации
           protected MyCustomException(SerializationInfo info, StreamingContext context) 
               : base(info, context) { }
       }
    */

    /*
       Стандарты именования и лучшие практики
       ===============================
       
       1. Именование:
          - Всегда заканчивайте название класса исключения словом "Exception"
          - Название должно указывать на причину или тип ошибки
          - Примеры: InvalidEmailException, UserNotFoundException
          
       2. Наследование:
          - Наследуйтесь напрямую от Exception или от наиболее близкого по смыслу исключения
          - Не создавайте слишком глубокие иерархии исключений
          
       3. Документация:
          - Добавляйте XML-документацию с описанием причин возникновения исключения
          - Указывайте, как избежать или исправить эту ошибку
          
       4. Свойства и данные:
          - Добавляйте специфичные свойства для предоставления дополнительной информации
          - Все дополнительные поля должны быть сериализуемыми
          
       5. Перехват и генерация:
          - Генерируйте исключения, только когда ситуация действительно исключительная
          - Не используйте исключения для управления стандартным потоком выполнения
          - Всегда предоставляйте информативное сообщение об ошибке
    */

    /*
       Сериализация исключений
       =================
       
       Сериализация необходима, если исключение может пересекать границы домена приложения
       или передаваться по сети (например, в веб-сервисах).
       
       Для поддержки сериализации:
       
       1. Добавьте атрибут [Serializable] к классу исключения
       2. Реализуйте конструктор для десериализации
       3. При необходимости переопределите метод GetObjectData
       
       Пример:
       
       [Serializable]
       public class SerializableException : Exception, ISerializable
       {
           public string AdditionalData { get; set; }
           
           public SerializableException() : base() { }
           
           public SerializableException(string message) : base(message) { }
           
           public SerializableException(string message, Exception innerException) 
               : base(message, innerException) { }
           
           protected SerializableException(SerializationInfo info, StreamingContext context) 
               : base(info, context)
           {
               AdditionalData = info.GetString("AdditionalData");
           }
           
           public override void GetObjectData(SerializationInfo info, StreamingContext context)
           {
               base.GetObjectData(info, context);
               info.AddValue("AdditionalData", AdditionalData);
           }
       }
    */

    /*
       Разработка исключений с дополнительными данными
       ======================================
       
       Для повышения информативности исключений добавляйте специфичные свойства:
       
       [Serializable]
       public class OrderProcessingException : Exception
       {
           public string OrderId { get; }
           public OrderErrorCode ErrorCode { get; }
           
           public OrderProcessingException(string orderId, OrderErrorCode errorCode, string message)
               : base(message)
           {
               OrderId = orderId;
               ErrorCode = errorCode;
           }
           
           public OrderProcessingException(string orderId, OrderErrorCode errorCode, 
                                          string message, Exception innerException)
               : base(message, innerException)
           {
               OrderId = orderId;
               ErrorCode = errorCode;
           }
           
           // Остальные конструкторы...
       }
       
       // Использование:
       try
       {
           ProcessOrder(order);
       }
       catch (OrderProcessingException ex)
       {
           Logger.Log($"Ошибка обработки заказа {ex.OrderId}, код: {ex.ErrorCode}");
       }
    */
    #endregion

    #region Примеры создания собственных исключений
    
    // Базовое приложение-специфичное исключение
    [Serializable]
    public class AppBaseException : Exception
    {
        public AppBaseException() : base() { }
        
        public AppBaseException(string message) : base(message) { }
        
        public AppBaseException(string message, Exception innerException) 
            : base(message, innerException) { }
        
        protected AppBaseException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
    
    // Специализированные исключения для бизнес-логики
    
    // Исключение для валидации пользовательских данных
    [Serializable]
    public class ValidationException : AppBaseException
    {
        public string PropertyName { get; }
        
        public ValidationException(string propertyName, string message)
            : base(message)
        {
            PropertyName = propertyName;
        }
        
        public ValidationException(string propertyName, string message, Exception innerException)
            : base(message, innerException)
        {
            PropertyName = propertyName;
        }
        
        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            PropertyName = info.GetString("PropertyName");
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("PropertyName", PropertyName);
        }
        
        public override string ToString()
        {
            return $"Ошибка валидации для поля '{PropertyName}': {Message}";
        }
    }
    
    // Исключение для отсутствующего ресурса
    [Serializable]
    public class ResourceNotFoundException : AppBaseException
    {
        public string ResourceType { get; }
        public string ResourceId { get; }
        
        public ResourceNotFoundException(string resourceType, string resourceId)
            : base($"Ресурс типа '{resourceType}' с идентификатором '{resourceId}' не найден.")
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
        
        public ResourceNotFoundException(string resourceType, string resourceId, string message)
            : base(message)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
        
        public ResourceNotFoundException(string resourceType, string resourceId, 
                                        string message, Exception innerException)
            : base(message, innerException)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
        
        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceType = info.GetString("ResourceType");
            ResourceId = info.GetString("ResourceId");
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ResourceType", ResourceType);
            info.AddValue("ResourceId", ResourceId);
        }
    }
    
    // Специализированное исключение для операций с банковским счётом
    [Serializable]
    public enum BankErrorCode
    {
        InsufficientFunds,
        AccountLocked,
        DailyLimitExceeded,
        InvalidOperation
    }
    
    [Serializable]
    public class BankAccountException : AppBaseException
    {
        public string AccountNumber { get; }
        public decimal AvailableBalance { get; }
        public decimal RequestedAmount { get; }
        public BankErrorCode ErrorCode { get; }
        
        public BankAccountException(string accountNumber, 
                                   decimal availableBalance,
                                   decimal requestedAmount,
                                   BankErrorCode errorCode)
            : base(GenerateMessage(accountNumber, availableBalance, requestedAmount, errorCode))
        {
            AccountNumber = accountNumber;
            AvailableBalance = availableBalance;
            RequestedAmount = requestedAmount;
            ErrorCode = errorCode;
        }
        
        public BankAccountException(string accountNumber, 
                                   decimal availableBalance,
                                   decimal requestedAmount,
                                   BankErrorCode errorCode,
                                   Exception innerException)
            : base(GenerateMessage(accountNumber, availableBalance, requestedAmount, errorCode), innerException)
        {
            AccountNumber = accountNumber;
            AvailableBalance = availableBalance;
            RequestedAmount = requestedAmount;
            ErrorCode = errorCode;
        }
        
        protected BankAccountException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            AccountNumber = info.GetString("AccountNumber");
            AvailableBalance = info.GetDecimal("AvailableBalance");
            RequestedAmount = info.GetDecimal("RequestedAmount");
            ErrorCode = (BankErrorCode)info.GetInt32("ErrorCode");
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("AccountNumber", AccountNumber);
            info.AddValue("AvailableBalance", AvailableBalance);
            info.AddValue("RequestedAmount", RequestedAmount);
            info.AddValue("ErrorCode", (int)ErrorCode);
        }
        
        private static string GenerateMessage(string accountNumber, 
                                            decimal availableBalance,
                                            decimal requestedAmount,
                                            BankErrorCode errorCode)
        {
            switch (errorCode)
            {
                case BankErrorCode.InsufficientFunds:
                    return $"Недостаточно средств на счёте {accountNumber}. " + 
                           $"Доступно: {availableBalance}, запрошено: {requestedAmount}.";
                
                case BankErrorCode.AccountLocked:
                    return $"Счёт {accountNumber} заблокирован.";
                
                case BankErrorCode.DailyLimitExceeded:
                    return $"Превышен дневной лимит для счёта {accountNumber}.";
                
                case BankErrorCode.InvalidOperation:
                    return $"Недопустимая операция для счёта {accountNumber}.";
                
                default:
                    return $"Произошла ошибка при выполнении операции со счётом {accountNumber}.";
            }
        }
    }
    
    // Демонстрационные классы для использования исключений
    
    // Класс пользователя с валидацией
    public class User
    {
        private string _email;
        public string Email 
        { 
            get => _email; 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ValidationException("Email", "Email не может быть пустым.");
                }
                
                if (!value.Contains("@") || !value.Contains("."))
                {
                    throw new ValidationException("Email", "Email имеет неверный формат.");
                }
                
                _email = value;
            }
        }
        
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ValidationException("Password", "Пароль не может быть пустым.");
                }
                
                if (value.Length < 8)
                {
                    throw new ValidationException("Password", 
                        "Пароль должен содержать не менее 8 символов.");
                }
                
                _password = value;
            }
        }
        
        public string Username { get; set; }
        
        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;      // Может вызвать ValidationException
            Password = password; // Может вызвать ValidationException
        }
    }
    
    // Имитация хранилища пользователей
    public class UserRepository
    {
        private Dictionary<string, User> _users = new Dictionary<string, User>();
        
        public void AddUser(User user)
        {
            if (_users.ContainsKey(user.Username))
            {
                throw new InvalidOperationException($"Пользователь с именем {user.Username} уже существует.");
            }
            
            _users[user.Username] = user;
        }
        
        public User GetUser(string username)
        {
            if (!_users.TryGetValue(username, out User user))
            {
                throw new ResourceNotFoundException("User", username);
            }
            
            return user;
        }
    }
    
    // Имитация банковского счёта
    public class BankAccount
    {
        public string AccountNumber { get; }
        public decimal Balance { get; private set; }
        public bool IsLocked { get; set; }
        public decimal DailyLimit { get; set; } = 10000;
        public decimal TodayWithdrawal { get; private set; } = 0;
        
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }
        
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма депозита должна быть больше нуля.", nameof(amount));
            }
            
            if (IsLocked)
            {
                throw new BankAccountException(AccountNumber, Balance, amount, BankErrorCode.AccountLocked);
            }
            
            Balance += amount;
        }
        
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Сумма снятия должна быть больше нуля.", nameof(amount));
            }
            
            if (IsLocked)
            {
                throw new BankAccountException(AccountNumber, Balance, amount, BankErrorCode.AccountLocked);
            }
            
            if (Balance < amount)
            {
                throw new BankAccountException(AccountNumber, Balance, amount, BankErrorCode.InsufficientFunds);
            }
            
            if (TodayWithdrawal + amount > DailyLimit)
            {
                throw new BankAccountException(AccountNumber, Balance, amount, BankErrorCode.DailyLimitExceeded);
            }
            
            Balance -= amount;
            TodayWithdrawal += amount;
        }
        
        public void Transfer(BankAccount targetAccount, decimal amount)
        {
            try
            {
                Withdraw(amount);
                targetAccount.Deposit(amount);
            }
            catch (BankAccountException ex)
            {
                throw new BankAccountException(
                    AccountNumber,
                    Balance,
                    amount,
                    ex.ErrorCode,
                    ex
                );
            }
            catch (Exception ex)
            {
                throw new BankAccountException(
                    AccountNumber,
                    Balance,
                    amount,
                    BankErrorCode.InvalidOperation,
                    ex
                );
            }
        }
    }
    #endregion

    internal class CustomExceptionsLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по созданию собственных исключений ====\n");
            
            Console.WriteLine("--- Пример 1: Валидация данных пользователя ---");
            
            try
            {
                // Попытка создать пользователя с некорректным email
                User user = new User("johndoe", "not-an-email", "password123");
                Console.WriteLine("Пользователь создан успешно."); // Эта строка не выполнится
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"Ошибка валидации: {ex.Message}");
                Console.WriteLine($"Проблемное поле: {ex.PropertyName}");
            }
            
            try
            {
                // Попытка создать пользователя с коротким паролем
                User user = new User("johndoe", "john@example.com", "short");
                Console.WriteLine("Пользователь создан успешно."); // Эта строка не выполнится
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"Ошибка валидации: {ex.Message}");
                Console.WriteLine($"Проблемное поле: {ex.PropertyName}");
            }
            
            // Создаем корректного пользователя
            User validUser = new User("johndoe", "john@example.com", "securepassword");
            Console.WriteLine("Корректный пользователь создан успешно.");
            
            Console.WriteLine("\n--- Пример 2: Работа с хранилищем пользователей ---");
            
            UserRepository userRepo = new UserRepository();
            userRepo.AddUser(validUser);
            
            try
            {
                // Пытаемся добавить пользователя с тем же именем
                User duplicateUser = new User("johndoe", "another@example.com", "anotherpassword");
                userRepo.AddUser(duplicateUser);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            
            try
            {
                // Пытаемся найти несуществующего пользователя
                User notFound = userRepo.GetUser("nonexistent");
            }
            catch (ResourceNotFoundException ex)
            {
                Console.WriteLine($"Ресурс не найден: {ex.Message}");
                Console.WriteLine($"Тип ресурса: {ex.ResourceType}, ID: {ex.ResourceId}");
            }
            
            Console.WriteLine("\n--- Пример 3: Банковские операции с обработкой исключений ---");
            
            // Создаем банковские счета
            BankAccount account1 = new BankAccount("ACC001", 1000);
            BankAccount account2 = new BankAccount("ACC002", 500);
            
            try
            {
                // Пытаемся снять больше денег, чем есть на счёте
                Console.WriteLine($"Баланс счёта 1: {account1.Balance}");
                Console.WriteLine("Попытка снять 1500...");
                account1.Withdraw(1500);
            }
            catch (BankAccountException ex)
            {
                Console.WriteLine($"Банковская ошибка: {ex.Message}");
                Console.WriteLine($"Код ошибки: {ex.ErrorCode}");
                Console.WriteLine($"Доступный баланс: {ex.AvailableBalance}, запрошено: {ex.RequestedAmount}");
            }
            
            // Блокируем счёт и пытаемся сделать депозит
            account2.IsLocked = true;
            
            try
            {
                Console.WriteLine($"\nСчёт 2 заблокирован: {account2.IsLocked}");
                Console.WriteLine("Попытка внести деньги на заблокированный счёт...");
                account2.Deposit(200);
            }
            catch (BankAccountException ex)
            {
                Console.WriteLine($"Банковская ошибка: {ex.Message}");
                Console.WriteLine($"Код ошибки: {ex.ErrorCode}");
            }
            
            // Разблокируем счёт и выполняем перевод
            account2.IsLocked = false;
            
            try
            {
                Console.WriteLine($"\nСчёт 2 разблокирован: {account2.IsLocked}");
                Console.WriteLine($"Баланс счёта 1: {account1.Balance}, счёта 2: {account2.Balance}");
                Console.WriteLine("Перевод 200 со счёта 1 на счёт 2...");
                account1.Transfer(account2, 200);
                Console.WriteLine($"Новый баланс счёта 1: {account1.Balance}, счёта 2: {account2.Balance}");
            }
            catch (BankAccountException ex)
            {
                Console.WriteLine($"Ошибка перевода: {ex.Message}");
            }
            
            Console.WriteLine("\n--- Многоуровневая обработка исключений ---");
            
            try
            {
                ProcessBusinessOperation();
            }
            catch (AppBaseException ex)
            {
                Console.WriteLine($"Поймано базовое исключение приложения: {ex.Message}");
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутреннее исключение: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Поймано общее исключение: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Операция завершена. Очистка ресурсов.");
            }
        }
        
        // Метод для демонстрации цепочки исключений
        private static void ProcessBusinessOperation()
        {
            Console.WriteLine("Выполнение бизнес-операции...");
            
            try
            {
                ValidateBusinessData();
            }
            catch (ValidationException ex)
            {
                // Перехватываем и оборачиваем в бизнес-исключение
                throw new AppBaseException("Бизнес-операция не может быть выполнена из-за ошибок валидации.", ex);
            }
            
            Console.WriteLine("Бизнес-операция успешно завершена.");
        }
        
        private static void ValidateBusinessData()
        {
            // Имитируем ошибку валидации
            throw new ValidationException("BusinessCode", "Неверный бизнес-код операции.");
        }
    }

    #region Задачи
    /*
        # Создайте исключение OrderNotFoundException с полями OrderId и CustomerName.
          Реализуйте все необходимые конструкторы и поддержку сериализации.
        
        # Разработайте класс Product с полями Name, Price, Quantity. Добавьте валидацию
          с выбрасыванием соответствующих исключений (например, ProductValidationException)
          при попытке установить отрицательную цену или количество.
        
        # Создайте класс Inventory, который управляет коллекцией Product.
          Реализуйте методы AddProduct и RemoveQuantity, которые выбрасывают соответствующие
          исключения при возникновении ошибок (продукт уже существует, не существует,
          недостаточно количества и т.д.).
        
        # Разработайте иерархию исключений для обработки ошибок файловой системы:
          - FileOperationException (базовый класс)
          - FileReadException (для ошибок чтения)
          - FileWriteException (для ошибок записи)
          - FilePermissionException (для ошибок доступа)
          Каждое исключение должно содержать информацию о пути к файлу и операции.
          
        # Напишите метод ReadConfigFile, который читает файл конфигурации, 
          обрабатывая и преобразовывая исключения FileNotFoundException, 
          UnauthorizedAccessException и IOException в ваши собственные типы исключений с 
          более понятными сообщениями.
    */
    #endregion
}
