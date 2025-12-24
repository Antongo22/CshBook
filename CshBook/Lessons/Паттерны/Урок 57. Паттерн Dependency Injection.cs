using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Для работы с DI контейнером Microsoft.Extensions.DependencyInjection
// Нужно добавить пакет через: dotnet add package Microsoft.Extensions.DependencyInjection
// Или в .csproj: <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
// Для демонстрации создадим упрощённую версию контейнера, но покажем и стандартный подход

namespace CshBook.Lessons.Глава_5
{
    #region Теория
    /*
     * Паттерн Dependency Injection (Внедрение зависимостей)
     * =======================================================
     *
     * Dependency Injection (DI) — это техника проектирования, при которой объект получает
     * свои зависимости извне, а не создаёт их самостоятельно. Это частный случай более
     * общего принципа Inversion of Control (IoC) — инверсии управления.
     *
     * Проблема: Жёсткая связанность (Tight Coupling)
     * ------------------------------------------------
     * 
     * Без DI классы напрямую создают свои зависимости:
     * 
     * class OrderService
     * {
     *     private EmailService emailService = new EmailService(); // Плохо!
     *     
     *     public void ProcessOrder(Order order)
     *     {
     *         // ... логика обработки заказа
     *         emailService.SendEmail(...); // Жёсткая связанность
     *     }
     * }
     * 
     * Проблемы такого подхода:
     * - Невозможно заменить EmailService на другую реализацию (например, SmsService)
     * - Сложно тестировать (нужно реальный EmailService)
     * - Нарушение принципа единственной ответственности (SRP)
     * - Нарушение принципа инверсии зависимостей (DIP)
     *
     * Решение: Dependency Injection
     * ------------------------------
     * 
     * Вместо создания зависимостей внутри класса, мы передаём их извне:
     * 
     * class OrderService
     * {
     *     private IEmailService emailService; // Зависимость через интерфейс
     *     
     *     public OrderService(IEmailService emailService) // Внедрение через конструктор
     *     {
     *         this.emailService = emailService;
     *     }
     * }
     * 
     * Преимущества:
     * + Гибкость: легко заменить реализацию
     * + Тестируемость: можно подставить мок-объект
     * + Слабая связанность: класс не знает о конкретной реализации
     * + Соблюдение SOLID принципов
     * + Переиспользование кода
     *
     * Недостатки:
     * - Усложнение кода (больше классов и интерфейсов)
     * - Необходимость DI контейнера для больших приложений
     * - Может быть избыточным для простых проектов
     *
     * Типы внедрения зависимостей:
     * 
     * 1. Constructor Injection (Внедрение через конструктор) — РЕКОМЕНДУЕТСЯ
     *    - Зависимости передаются через конструктор
     *    - Обязательные зависимости
     *    - Самый распространённый способ
     * 
     * 2. Property Injection (Внедрение через свойства)
     *    - Зависимости устанавливаются через публичные свойства
     *    - Опциональные зависимости
     *    - Менее предпочтителен, чем Constructor Injection
     * 
     * 3. Method Injection (Внедрение через методы)
     *    - Зависимости передаются как параметры метода
     *    - Временные зависимости
     *    - Используется реже
     *
     * Dependency Injection Container (DI Контейнер)
     * -----------------------------------------------
     * 
     * DI контейнер — это библиотека, которая автоматически управляет созданием
     * объектов и их зависимостей. В .NET используется Microsoft.Extensions.DependencyInjection.
     * 
     * Основные понятия:
     * - Registration (Регистрация): указываем, какой интерфейс соответствует какой реализации
     * - Resolution (Разрешение): контейнер создаёт объект и все его зависимости
     * - Lifetime (Время жизни): как долго живёт объект
     * 
     * Service Lifetime (Время жизни сервиса):
     * 
     * 1. Singleton — один экземпляр на всё приложение
     * 2. Scoped — один экземпляр на область (например, один HTTP запрос)
     * 3. Transient — новый экземпляр при каждом запросе
     *
     * Принципы SOLID и DI
     * --------------------
     * 
     * Dependency Inversion Principle (DIP):
     * - Модули высокого уровня не должны зависеть от модулей низкого уровня
     * - Оба должны зависеть от абстракций (интерфейсов)
     * - DI помогает соблюдать этот принцип
     *
     * Отличия от других подходов:
     * 
     * Service Locator (Анти-паттерн):
     * - Объект сам запрашивает зависимости из контейнера
     * - Скрывает зависимости (плохо для тестирования)
     * - Не рекомендуется использовать
     * 
     * Factory Pattern:
     * - Создаёт объекты, но не управляет их жизненным циклом
     * - DI контейнер — это продвинутая фабрика с управлением зависимостями
     */
    #endregion

    #region Пример 1: Проблема без DI (жёсткая связанность)

    // Плохой пример: жёсткая связанность
    public class BadEmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"[BadEmailService] Отправка email на {to}: {subject}");
            // Реальная отправка email
        }
    }

    public class BadOrderService
    {
        // ПРОБЛЕМА: OrderService напрямую создаёт EmailService
        private BadEmailService emailService = new BadEmailService();

        public void ProcessOrder(string orderId, string customerEmail)
        {
            Console.WriteLine($"[BadOrderService] Обработка заказа {orderId}");

            // Проблемы:
            // 1. Невозможно заменить EmailService на SmsService
            // 2. Сложно тестировать (нужен реальный EmailService)
            // 3. Нарушение принципа единственной ответственности
            emailService.SendEmail(customerEmail, "Заказ обработан", $"Ваш заказ {orderId} обработан");
        }
    }

    #endregion

    #region Пример 2: Constructor Injection (внедрение через конструктор)

    // Интерфейс для уведомлений
    public interface INotificationService
    {
        void SendNotification(string recipient, string message);
    }

    // Реализация 1: Email
    public class EmailNotificationService : INotificationService
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"[EmailService] Отправка email на {recipient}: {message}");
        }
    }

    // Реализация 2: SMS
    public class SmsNotificationService : INotificationService
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"[SmsService] Отправка SMS на {recipient}: {message}");
        }
    }

    // Реализация 3: Push-уведомление
    public class PushNotificationService : INotificationService
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"[PushService] Push-уведомление для {recipient}: {message}");
        }
    }

    // Хороший пример: Constructor Injection
    public class OrderService
    {
        private readonly INotificationService _notificationService;

        // Зависимость внедряется через конструктор
        public OrderService(INotificationService notificationService)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public void ProcessOrder(string orderId, string customerEmail)
        {
            Console.WriteLine($"[OrderService] Обработка заказа {orderId}");

            // Теперь можем использовать любую реализацию INotificationService
            _notificationService.SendNotification(customerEmail, $"Ваш заказ {orderId} обработан");
        }
    }

    #endregion

    #region Пример 3: Property Injection (внедрение через свойства)

    // Интерфейс для логирования
    public interface ILogger
    {
        void Log(string message);
    }

    // Реализация логгера
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.Now:HH:mm:ss} - {message}");
        }
    }

    // Класс с Property Injection
    public class PaymentProcessor
    {
        // Опциональная зависимость через свойство
        public ILogger Logger { get; set; }

        public void ProcessPayment(decimal amount, string cardNumber)
        {
            Logger?.Log($"Обработка платежа на сумму {amount:C}");

            Console.WriteLine($"[PaymentProcessor] Платёж обработан: {amount:C}");

            Logger?.Log("Платёж успешно обработан");
        }
    }

    // Альтернатива: обязательное свойство с проверкой
    public class PaymentProcessorWithRequiredLogger
    {
        private ILogger _logger;

        public ILogger Logger
        {
            get => _logger ?? throw new InvalidOperationException("Logger не установлен");
            set => _logger = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void ProcessPayment(decimal amount, string cardNumber)
        {
            Logger.Log($"Обработка платежа на сумму {amount:C}");
            Console.WriteLine($"[PaymentProcessor] Платёж обработан: {amount:C}");
        }
    }

    #endregion

    #region Пример 4: Method Injection (внедрение через методы)

    // Интерфейс для валидации
    public interface IValidator<T>
    {
        bool Validate(T item);
    }

    // Валидатор для заказа
    public class OrderValidator : IValidator<Order>
    {
        public bool Validate(Order order)
        {
            if (order == null) return false;
            if (string.IsNullOrEmpty(order.OrderId)) return false;
            if (order.Amount <= 0) return false;
            return true;
        }
    }

    public class Order
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string CustomerEmail { get; set; }
    }

    // Класс с Method Injection
    public class OrderValidatorService
    {
        // Валидатор передаётся как параметр метода (временная зависимость)
        public bool ValidateOrder(Order order, IValidator<Order> validator)
        {
            Console.WriteLine($"[OrderValidatorService] Валидация заказа {order?.OrderId}");

            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            bool isValid = validator.Validate(order);

            Console.WriteLine($"Результат валидации: {(isValid ? "Успешно" : "Ошибка")}");

            return isValid;
        }
    }

    #endregion

    #region Пример 5: Простой DI Контейнер (упрощённая реализация)

    // Упрощённый DI контейнер для демонстрации
    public class SimpleDIContainer
    {
        private Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();
        private Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
        private Dictionary<Type, Func<object>> _factories = new Dictionary<Type, Func<object>>();

        // Регистрация типа
        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _registrations[typeof(TInterface)] = typeof(TImplementation);
        }

        // Регистрация Singleton
        public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            Register<TInterface, TImplementation>();
            // Создаём экземпляр сразу
            var instance = CreateInstance<TImplementation>();
            _singletons[typeof(TInterface)] = instance;
        }

        // Регистрация через фабрику
        public void RegisterFactory<TInterface>(Func<TInterface> factory)
        {
            _factories[typeof(TInterface)] = () => factory();
        }

        // Разрешение зависимости
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type type)
        {
            // Проверяем Singleton
            if (_singletons.ContainsKey(type))
                return _singletons[type];

            // Проверяем фабрику
            if (_factories.ContainsKey(type))
                return _factories[type]();

            // Проверяем регистрацию
            if (_registrations.ContainsKey(type))
            {
                var implementationType = _registrations[type];
                return CreateInstance(implementationType);
            }

            throw new InvalidOperationException($"Тип {type.Name} не зарегистрирован");
        }

        private object CreateInstance(Type type)
        {
            var constructors = type.GetConstructors();

            // Ищем конструктор с параметрами
            var constructor = constructors
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();

            if (constructor == null)
                return Activator.CreateInstance(type);

            // Получаем параметры конструктора
            var parameters = constructor.GetParameters();
            var args = new object[parameters.Length];

            // Рекурсивно разрешаем зависимости
            for (int i = 0; i < parameters.Length; i++)
            {
                args[i] = Resolve(parameters[i].ParameterType);
            }

            return constructor.Invoke(args);
        }

        private T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }
    }

    #endregion

    #region Пример 6: Сложные зависимости и цепочки

    // Пример цепочки зависимостей: A -> B -> C

    public interface IDataRepository
    {
        string GetData(string key);
    }

    public class DatabaseRepository : IDataRepository
    {
        private readonly ILogger _logger;

        public DatabaseRepository(ILogger logger)
        {
            _logger = logger;
        }

        public string GetData(string key)
        {
            _logger.Log($"Запрос данных для ключа: {key}");
            Console.WriteLine($"[DatabaseRepository] Получение данных из БД для ключа '{key}'");
            return $"Data for {key}";
        }
    }

    public interface IDataService
    {
        string ProcessData(string key);
    }

    public class DataService : IDataService
    {
        private readonly IDataRepository _repository;
        private readonly ILogger _logger;

        // DataService зависит от IDataRepository и ILogger
        public DataService(IDataRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public string ProcessData(string key)
        {
            _logger.Log($"Обработка данных для ключа: {key}");
            var data = _repository.GetData(key);
            Console.WriteLine($"[DataService] Обработка данных: {data}");
            return $"Processed: {data}";
        }
    }

    public class BusinessLogicService
    {
        private readonly IDataService _dataService;
        private readonly INotificationService _notificationService;

        // BusinessLogicService зависит от IDataService и INotificationService
        public BusinessLogicService(IDataService dataService, INotificationService notificationService)
        {
            _dataService = dataService;
            _notificationService = notificationService;
        }

        public void ExecuteBusinessLogic(string key, string userEmail)
        {
            Console.WriteLine($"[BusinessLogicService] Выполнение бизнес-логики для ключа: {key}");

            var result = _dataService.ProcessData(key);

            _notificationService.SendNotification(userEmail, $"Результат: {result}");
        }
    }

    #endregion

    #region Пример 7: Условная регистрация и фабрики

    // Интерфейс для провайдера уведомлений
    public interface INotificationProvider
    {
        void Send(string recipient, string message);
    }

    public class EmailProvider : INotificationProvider
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"[EmailProvider] Email на {recipient}: {message}");
        }
    }

    public class SmsProvider : INotificationProvider
    {
        public void Send(string recipient, string message)
        {
            Console.WriteLine($"[SmsProvider] SMS на {recipient}: {message}");
        }
    }

    // Фабрика для создания провайдеров
    public interface INotificationProviderFactory
    {
        INotificationProvider CreateProvider(string type);
    }

    public class NotificationProviderFactory : INotificationProviderFactory
    {
        public INotificationProvider CreateProvider(string type)
        {
            return type.ToLower() switch
            {
                "email" => new EmailProvider(),
                "sms" => new SmsProvider(),
                _ => throw new ArgumentException($"Неизвестный тип провайдера: {type}")
            };
        }
    }

    // Сервис, использующий фабрику
    public class NotificationManager
    {
        private readonly INotificationProviderFactory _factory;

        public NotificationManager(INotificationProviderFactory factory)
        {
            _factory = factory;
        }

        public void SendNotification(string type, string recipient, string message)
        {
            var provider = _factory.CreateProvider(type);
            provider.Send(recipient, message);
        }
    }

    #endregion

    #region Пример 8: Тестирование с DI

    // Мок-объект для тестирования
    public class MockNotificationService : INotificationService
    {
        public List<(string recipient, string message)> SentNotifications { get; } = new List<(string, string)>();

        public void SendNotification(string recipient, string message)
        {
            SentNotifications.Add((recipient, message));
            Console.WriteLine($"[MockNotificationService] МОК: Уведомление для {recipient}: {message}");
        }

        public void Clear()
        {
            SentNotifications.Clear();
        }
    }

    // Мок-объект для логгера
    public class MockLogger : ILogger
    {
        public List<string> Logs { get; } = new List<string>();

        public void Log(string message)
        {
            Logs.Add(message);
            Console.WriteLine($"[MockLogger] МОК: {message}");
        }
    }

    // Пример "юнит-теста" (упрощённый, без фреймворка)
    public class OrderServiceTests
    {
        public static void TestOrderProcessing()
        {
            Console.WriteLine("\n=== Тест: Обработка заказа ===");

            // Arrange (подготовка)
            var mockNotification = new MockNotificationService();
            var orderService = new OrderService(mockNotification);

            // Act (действие)
            orderService.ProcessOrder("ORDER-123", "test@example.com");

            // Assert (проверка)
            if (mockNotification.SentNotifications.Count == 1)
            {
                var notification = mockNotification.SentNotifications[0];
                if (notification.recipient == "test@example.com" && notification.message.Contains("ORDER-123"))
                {
                    Console.WriteLine("✓ Тест пройден: уведомление отправлено корректно");
                }
                else
                {
                    Console.WriteLine("✗ Тест провален: неверные данные уведомления");
                }
            }
            else
            {
                Console.WriteLine($"✗ Тест провален: ожидалось 1 уведомление, получено {mockNotification.SentNotifications.Count}");
            }
        }
    }

    #endregion

    #region Демонстрация

    public static class DependencyInjectionDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║  ДЕМОНСТРАЦИЯ ПАТТЕРНА DEPENDENCY INJECTION (DI)       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            DemoProblemWithoutDI();
            DemoConstructorInjection();
            DemoPropertyInjection();
            DemoMethodInjection();
            DemoSimpleDIContainer();
            DemoComplexDependencies();
            DemoFactoryPattern();
            DemoTestingWithDI();
        }

        private static void DemoProblemWithoutDI()
        {
            Console.WriteLine("\n[1] === Проблема без DI (жёсткая связанность) ===\n");

            var badService = new BadOrderService();
            badService.ProcessOrder("ORDER-001", "customer@example.com");

            Console.WriteLine("\nПроблемы:");
            Console.WriteLine("✗ Невозможно заменить EmailService на SmsService");
            Console.WriteLine("✗ Сложно тестировать (нужен реальный EmailService)");
            Console.WriteLine("✗ Нарушение принципов SOLID");
        }

        private static void DemoConstructorInjection()
        {
            Console.WriteLine("\n\n[2] === Constructor Injection ===\n");

            // Используем Email
            Console.WriteLine("--- С Email уведомлениями ---");
            var emailService = new EmailNotificationService();
            var orderService1 = new OrderService(emailService);
            orderService1.ProcessOrder("ORDER-002", "customer@example.com");

            // Используем SMS
            Console.WriteLine("\n--- С SMS уведомлениями ---");
            var smsService = new SmsNotificationService();
            var orderService2 = new OrderService(smsService);
            orderService2.ProcessOrder("ORDER-003", "customer@example.com");

            // Используем Push
            Console.WriteLine("\n--- С Push уведомлениями ---");
            var pushService = new PushNotificationService();
            var orderService3 = new OrderService(pushService);
            orderService3.ProcessOrder("ORDER-004", "customer@example.com");

            Console.WriteLine("\nПреимущества:");
            Console.WriteLine("✓ Легко заменить реализацию");
            Console.WriteLine("✓ Можно использовать мок-объекты для тестирования");
            Console.WriteLine("✓ Соблюдение принципов SOLID");
        }

        private static void DemoPropertyInjection()
        {
            Console.WriteLine("\n\n[3] === Property Injection ===\n");

            var paymentProcessor = new PaymentProcessor();

            // Без логгера
            Console.WriteLine("--- Обработка платежа без логгера ---");
            paymentProcessor.ProcessPayment(1000m, "1234-5678-9012-3456");

            // С логгером
            Console.WriteLine("\n--- Обработка платежа с логгером ---");
            paymentProcessor.Logger = new ConsoleLogger();
            paymentProcessor.ProcessPayment(2000m, "9876-5432-1098-7654");

            Console.WriteLine("\n--- Обязательный логгер ---");
            var processorWithRequired = new PaymentProcessorWithRequiredLogger();
            processorWithRequired.Logger = new ConsoleLogger();
            processorWithRequired.ProcessPayment(3000m, "1111-2222-3333-4444");
        }

        private static void DemoMethodInjection()
        {
            Console.WriteLine("\n\n[4] === Method Injection ===\n");

            var validatorService = new OrderValidatorService();
            var validator = new OrderValidator();

            var validOrder = new Order { OrderId = "ORDER-005", Amount = 1500m, CustomerEmail = "test@example.com" };
            var invalidOrder = new Order { OrderId = "", Amount = -100m, CustomerEmail = "test@example.com" };

            Console.WriteLine("--- Валидация корректного заказа ---");
            validatorService.ValidateOrder(validOrder, validator);

            Console.WriteLine("\n--- Валидация некорректного заказа ---");
            validatorService.ValidateOrder(invalidOrder, validator);
        }

        private static void DemoSimpleDIContainer()
        {
            Console.WriteLine("\n\n[5] === Простой DI Контейнер ===\n");

            // Создаём контейнер
            var container = new SimpleDIContainer();

            // Регистрируем сервисы
            Console.WriteLine("Регистрация сервисов...");
            container.Register<ILogger, ConsoleLogger>();
            container.Register<INotificationService, EmailNotificationService>();
            container.Register<IDataRepository, DatabaseRepository>();
            container.Register<IDataService, DataService>();

            // Регистрируем Singleton
            container.RegisterSingleton<ILogger, ConsoleLogger>();

            // Разрешаем зависимости
            Console.WriteLine("\n--- Разрешение зависимостей ---");
            var logger = container.Resolve<ILogger>();
            logger.Log("Тестовое сообщение");

            var notificationService = container.Resolve<INotificationService>();
            notificationService.SendNotification("user@example.com", "Тестовое уведомление");

            Console.WriteLine("\n--- Разрешение сложных зависимостей ---");
            var dataService = container.Resolve<IDataService>();
            dataService.ProcessData("test-key");
        }

        private static void DemoComplexDependencies()
        {
            Console.WriteLine("\n\n[6] === Сложные зависимости и цепочки ===\n");

            var container = new SimpleDIContainer();

            // Регистрируем всю цепочку: BusinessLogicService -> DataService -> DatabaseRepository -> Logger
            container.Register<ILogger, ConsoleLogger>();
            container.Register<INotificationService, EmailNotificationService>();
            container.Register<IDataRepository, DatabaseRepository>();
            container.Register<IDataService, DataService>();

            Console.WriteLine("Разрешение цепочки зависимостей...");
            var businessService = new BusinessLogicService(
                container.Resolve<IDataService>(),
                container.Resolve<INotificationService>()
            );

            businessService.ExecuteBusinessLogic("business-key", "admin@example.com");
        }

        private static void DemoFactoryPattern()
        {
            Console.WriteLine("\n\n[7] === Условная регистрация и фабрики ===\n");

            var factory = new NotificationProviderFactory();
            var manager = new NotificationManager(factory);

            Console.WriteLine("--- Отправка через Email ---");
            manager.SendNotification("email", "user@example.com", "Сообщение по email");

            Console.WriteLine("\n--- Отправка через SMS ---");
            manager.SendNotification("sms", "+7-999-123-45-67", "Сообщение по SMS");

            // Использование с DI контейнером
            var container = new SimpleDIContainer();
            container.RegisterFactory<INotificationProviderFactory>(() => new NotificationProviderFactory());
            container.Register<INotificationService, EmailNotificationService>();

            var resolvedFactory = container.Resolve<INotificationProviderFactory>();
            var provider = resolvedFactory.CreateProvider("email");
            provider.Send("test@example.com", "Тест через фабрику");
        }

        private static void DemoTestingWithDI()
        {
            Console.WriteLine("\n\n[8] === Тестирование с DI ===\n");

            // Демонстрация "юнит-теста"
            OrderServiceTests.TestOrderProcessing();

            Console.WriteLine("\n--- Использование мок-объектов ---");
            var mockNotification = new MockNotificationService();
            var mockLogger = new MockLogger();

            var orderService = new OrderService(mockNotification);
            orderService.ProcessOrder("TEST-ORDER", "test@example.com");

            Console.WriteLine($"\nОтправлено уведомлений: {mockNotification.SentNotifications.Count}");
            foreach (var notification in mockNotification.SentNotifications)
            {
                Console.WriteLine($"  - {notification.recipient}: {notification.message}");
            }
        }

        public static void Main_()
        {
            RunDemo();
        }
    }

    #endregion
}

/*
 * ═══════════════════════════════════════════════════════════════
 * ЗАДАЧИ ДЛЯ САМОСТОЯТЕЛЬНОЙ ПРАКТИКИ
 * ═══════════════════════════════════════════════════════════════
 */

#region Задачи

/*
 * БАЗОВЫЕ ЗАДАЧИ
 * ==============
 * 
 * 1. Рефакторинг с жёсткой связанностью
 *    Есть класс UserService, который напрямую создаёт DatabaseConnection и EmailService.
 *    Рефакторируйте его, используя Constructor Injection.
 *    Создайте интерфейсы IDatabaseConnection и IEmailService.
 * 
 * 2. Настройка DI контейнера
 *    Создайте консольное приложение и настройте DI контейнер:
 *    - Зарегистрируйте ILogger как Singleton
 *    - Зарегистрируйте IRepository как Scoped (в нашем простом контейнере — как обычный)
 *    - Зарегистрируйте IService как Transient
 *    Продемонстрируйте работу с разными lifetime.
 * 
 * 3. Property Injection для опциональных зависимостей
 *    Создайте класс ReportGenerator, который:
 *    - Обязательно требует IDataProvider через конструктор
 *    - Опционально может использовать IFormatter через свойство
 *    - Если IFormatter не установлен, использует формат по умолчанию
 * 
 * СРЕДНИЕ ЗАДАЧИ
 * ==============
 * 
 * 4. Цепочка зависимостей
 *    Создайте следующую цепочку:
 *    OrderController -> OrderService -> OrderRepository -> DatabaseContext -> ILogger
 *    Настройте DI контейнер для автоматического разрешения всех зависимостей.
 * 
 * 5. Фабрика для условного создания объектов
 *    Создайте систему обработки платежей:
 *    - Интерфейс IPaymentProcessor
 *    - Реализации: CreditCardProcessor, PayPalProcessor, CryptoProcessor
 *    - PaymentProcessorFactory, который создаёт нужный процессор на основе типа платежа
 *    - PaymentService, который использует фабрику через DI
 * 
 * 6. Тестирование с моками
 *    Напишите "юнит-тесты" (можно без фреймворка) для OrderService:
 *    - Тест успешной обработки заказа
 *    - Тест обработки заказа с ошибкой уведомления
 *    - Используйте MockNotificationService для проверки вызовов
 * 
 * СЛОЖНЫЕ ЗАДАЧИ
 * ==============
 * 
 * 7. Собственный DI контейнер (расширенная версия)
 *    Расширьте SimpleDIContainer:
 *    - Поддержка Scoped lifetime (словарь для каждой области)
 *    - Поддержка именованных регистраций (несколько реализаций одного интерфейса)
 *    - Валидация циклических зависимостей
 *    - Поддержка декораторов (обёрток вокруг сервисов)
 * 
 * 8. Условная регистрация сервисов
 *    Создайте систему, где:
 *    - В зависимости от конфигурации (appsettings.json или переменные окружения)
 *    - Регистрируется разная реализация IStorageService:
 *      * FileStorageService для разработки
 *      * CloudStorageService для продакшена
 *      * InMemoryStorageService для тестов
 *    Используйте фабрику для создания нужного сервиса.
 * 
 * 9. Декораторы через DI
 *    Создайте систему, где базовый сервис оборачивается декораторами:
 *    - IDataService (базовый интерфейс)
 *    - DataService (реализация)
 *    - CachingDataServiceDecorator (кэширование)
 *    - LoggingDataServiceDecorator (логирование)
 *    - RetryDataServiceDecorator (повтор при ошибках)
 *    Настройте DI контейнер для автоматического оборачивания.
 * 
 * 10. Интеграция с ASP.NET Core
 *     Создайте простое веб-приложение (или опишите настройку):
 *     - Настройка DI в Program.cs / Startup.cs
 *     - Регистрация сервисов в ConfigureServices
 *     - Использование в контроллерах через Constructor Injection
 *     - Использование Scoped lifetime для работы с БД
 *     - Демонстрация работы с IServiceProvider
 */

#endregion

/*
 * ═══════════════════════════════════════════════════════════════
 * MICROSOFT.EXTENSIONS.DEPENDENCYINJECTION
 * ═══════════════════════════════════════════════════════════════
 * 
 * В реальных проектах используется Microsoft.Extensions.DependencyInjection:
 * 
 * 1. Установка пакета:
 *    dotnet add package Microsoft.Extensions.DependencyInjection
 * 
 * 2. Настройка контейнера:
 *    var services = new ServiceCollection();
 *    services.AddSingleton<ILogger, ConsoleLogger>();
 *    services.AddScoped<IRepository, DatabaseRepository>();
 *    services.AddTransient<IService, MyService>();
 *    
 *    var serviceProvider = services.BuildServiceProvider();
 * 
 * 3. Разрешение зависимостей:
 *    var service = serviceProvider.GetService<IService>();
 *    var logger = serviceProvider.GetRequiredService<ILogger>();
 * 
 * 4. Service Lifetime:
 *    - AddSingleton: один экземпляр на всё приложение
 *    - AddScoped: один экземпляр на область (например, HTTP запрос)
 *    - AddTransient: новый экземпляр при каждом запросе
 * 
 * 5. Регистрация с фабрикой:
 *    services.AddSingleton<IService>(sp => new MyService(sp.GetService<ILogger>()));
 * 
 * 6. Регистрация нескольких реализаций:
 *    services.AddSingleton<INotificationService, EmailService>();
 *    services.AddSingleton<INotificationService, SmsService>(); // Перезапишет предыдущую
 *    
 *    // Для нескольких реализаций используйте IEnumerable:
 *    services.AddSingleton<EmailService>();
 *    services.AddSingleton<SmsService>();
 *    var services = serviceProvider.GetServices<INotificationService>();
 */

/*
 * ═══════════════════════════════════════════════════════════════
 * ИТОГОВЫЕ ЗАМЕЧАНИЯ
 * ═══════════════════════════════════════════════════════════════
 * 
 * Dependency Injection — один из самых важных паттернов в современной разработке.
 * Он лежит в основе большинства современных фреймворков (.NET, Spring, Angular и т.д.).
 * 
 * Используйте DI, когда:
 * ✓ Класс имеет зависимости от других классов
 * ✓ Нужна гибкость в замене реализаций
 * ✓ Важна тестируемость кода
 * ✓ Работаете с большим проектом
 * ✓ Используете фреймворки (ASP.NET Core, WPF и т.д.)
 * 
 * Не используйте DI, когда:
 * ✗ Простой проект с минимальными зависимостями
 * ✗ Зависимость — это примитивный тип (string, int)
 * ✗ Зависимость создаётся очень редко
 * 
 * Рекомендации:
 * - Предпочитайте Constructor Injection всем остальным способам
 * - Используйте интерфейсы, а не конкретные классы
 * - Регистрируйте сервисы в одном месте (Composition Root)
 * - Избегайте Service Locator (анти-паттерн)
 * - Используйте DI контейнер для больших проектов
 * 
 * Помните: DI — это не цель, а средство для достижения слабой связанности,
 * тестируемости и соблюдения принципов SOLID!
 */

