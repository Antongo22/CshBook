using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_5;
#region Теория
/*
 * В этом уроке ты узнаешь о паттерне проектирования Singleton:
 * 
 * - Что такое паттерны проектирования
 * - Описание паттерна Singleton
 * - Когда использовать Singleton
 * - Различные способы реализации Singleton
 * - Потокобезопасная реализация Singleton
 * - Преимущества и недостатки паттерна
 * - Практические примеры использования
 */

/*
   Что такое паттерны проектирования
   ================================
   
   Паттерн проектирования (Design Pattern) - это типовое решение часто встречающейся
   проблемы в проектировании программного обеспечения. Это не готовый код, который
   можно скопировать, а концепция решения, которую можно применить в различных ситуациях.
   
   Паттерны проектирования помогают:
   - Решать типовые проблемы архитектуры
   - Делать код более читаемым и понятным
   - Облегчать коммуникацию между разработчиками
   - Повторно использовать проверенные решения
   
   Существует три основные категории паттернов:
   1. Порождающие (Creational) - управляют созданием объектов
   2. Структурные (Structural) - определяют отношения между объектами
   3. Поведенческие (Behavioral) - определяют взаимодействие между объектами
*/

/*
   Паттерн Singleton (Одиночка)
   ============================
   
   Singleton - это порождающий паттерн проектирования, который гарантирует, что у класса
   есть только один экземпляр, и предоставляет к нему глобальную точку доступа.
   
   Основные характеристики:
   1. Класс имеет только один экземпляр
   2. Экземпляр создается самим классом
   3. Предоставляется глобальный доступ к экземпляру
   4. Конструктор класса закрыт (private)
   
   Когда использовать Singleton:
   - Логгеры (системы журналирования)
   - Настройки приложения (конфигурация)
   - Пулы соединений с базой данных
   - Кэши
   - Драйверы устройств
   - Фабрики объектов
   - Реестры объектов
*/

/*
   Простая реализация Singleton (не потокобезопасная)
   =================================================
   
   Это базовая реализация, которая работает только в однопоточной среде.
   В многопоточном приложении может создаться несколько экземпляров.
*/
public class SimpleSingleton
{
    // Статическое поле для хранения единственного экземпляра
    private static SimpleSingleton _instance;

    // Приватный конструктор предотвращает создание экземпляров извне
    private SimpleSingleton()
    {
        Console.WriteLine("SimpleSingleton создан");
    }

    // Публичный статический метод для получения экземпляра
    public static SimpleSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SimpleSingleton();
            }
            return _instance;
        }
    }

    public void DoSomething()
    {
        Console.WriteLine("SimpleSingleton выполняет работу");
    }
}

/*
   Потокобезопасная реализация с блокировкой
   ========================================
   
   Эта реализация использует блокировку (lock) для обеспечения потокобезопасности.
   Недостаток: блокировка происходит при каждом обращении к свойству Instance.
*/
public class ThreadSafeSingleton
{
    private static ThreadSafeSingleton _instance;
    private static readonly object _lock = new object();

    private ThreadSafeSingleton()
    {
        Console.WriteLine("ThreadSafeSingleton создан");
    }

    public static ThreadSafeSingleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ThreadSafeSingleton();
                }
                return _instance;
            }
        }
    }

    public void DoSomething()
    {
        Console.WriteLine("ThreadSafeSingleton выполняет работу");
    }
}

/*
   Потокобезопасная реализация с двойной проверкой блокировки
   =========================================================
   
   Эта реализация оптимизирует производительность, проверяя _instance дважды:
   сначала без блокировки, затем с блокировкой.
*/
public class DoubleCheckSingleton
{
    private static DoubleCheckSingleton _instance;
    private static readonly object _lock = new object();

    private DoubleCheckSingleton()
    {
        Console.WriteLine("DoubleCheckSingleton создан");
    }

    public static DoubleCheckSingleton Instance
    {
        get
        {
            // Первая проверка без блокировки (быстрая)
            if (_instance == null)
            {
                lock (_lock)
                {
                    // Вторая проверка с блокировкой (безопасная)
                    if (_instance == null)
                    {
                        _instance = new DoubleCheckSingleton();
                    }
                }
            }
            return _instance;
        }
    }

    public void DoSomething()
    {
        Console.WriteLine("DoubleCheckSingleton выполняет работу");
    }
}

/*
   Ленивая инициализация с использованием Lazy<T>
   =============================================
   
   Самый простой и эффективный способ создания потокобезопасного Singleton
   в .NET Framework 4.0 и выше.
*/
public class LazySingleton
{
    // Lazy<T> автоматически обеспечивает потокобезопасность и ленивую инициализацию
    private static readonly Lazy<LazySingleton> _instance = 
        new Lazy<LazySingleton>(() => new LazySingleton());

    private LazySingleton()
    {
        Console.WriteLine("LazySingleton создан");
    }

    public static LazySingleton Instance => _instance.Value;

    public void DoSomething()
    {
        Console.WriteLine("LazySingleton выполняет работу");
    }
}

/*
   Статический конструктор (Eager Initialization)
   ==============================================
   
   Экземпляр создается при первом обращении к классу.
   Потокобезопасно по умолчанию, но не ленивое (создается сразу).
*/
public class StaticSingleton
{
    // Экземпляр создается сразу при загрузке класса
    private static readonly StaticSingleton _instance = new StaticSingleton();

    private StaticSingleton()
    {
        Console.WriteLine("StaticSingleton создан");
    }

    public static StaticSingleton Instance => _instance;

    public void DoSomething()
    {
        Console.WriteLine("StaticSingleton выполняет работу");
    }
}

#endregion

#region Практические примеры

/*
   Пример 1: Логгер (система журналирования)
   =========================================
   
   Логгер - классический пример использования паттерна Singleton.
   Нам нужен единый экземпляр для записи логов во всем приложении.
*/
public class Logger
{
    private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());
    private readonly List<string> _logs = new List<string>();

    private Logger() { }

    public static Logger Instance => _instance.Value;

    public void Log(string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        _logs.Add(logEntry);
        Console.WriteLine(logEntry);
    }

    public void LogError(string error)
    {
        Log($"ERROR: {error}");
    }

    public void LogInfo(string info)
    {
        Log($"INFO: {info}");
    }

    public List<string> GetAllLogs()
    {
        return new List<string>(_logs); // Возвращаем копию для безопасности
    }

    public void ClearLogs()
    {
        _logs.Clear();
        Log("Логи очищены");
    }
}

/*
   Пример 2: Конфигурация приложения
   =================================
   
   Singleton для хранения настроек приложения, которые должны быть
   доступны из любой части программы.
*/
public class AppConfig
{
    private static readonly Lazy<AppConfig> _instance = new Lazy<AppConfig>(() => new AppConfig());
    private readonly Dictionary<string, string> _settings = new Dictionary<string, string>();

    private AppConfig()
    {
        // Загружаем настройки по умолчанию
        LoadDefaultSettings();
    }

    public static AppConfig Instance => _instance.Value;

    private void LoadDefaultSettings()
    {
        _settings["DatabaseConnection"] = "Server=localhost;Database=MyApp;";
        _settings["LogLevel"] = "Info";
        _settings["MaxRetries"] = "3";
        _settings["Timeout"] = "30";
    }

    public string GetSetting(string key)
    {
        return _settings.TryGetValue(key, out string value) ? value : string.Empty;
    }

    public void SetSetting(string key, string value)
    {
        _settings[key] = value;
    }

    public bool GetBoolSetting(string key)
    {
        string value = GetSetting(key);
        return bool.TryParse(value, out bool result) && result;
    }

    public int GetIntSetting(string key)
    {
        string value = GetSetting(key);
        return int.TryParse(value, out int result) ? result : 0;
    }

    public void DisplayAllSettings()
    {
        Console.WriteLine("Настройки приложения:");
        foreach (var setting in _settings)
        {
            Console.WriteLine($"  {setting.Key}: {setting.Value}");
        }
    }
}

/*
   Пример 3: Счетчик операций
   ==========================
   
   Singleton для подсчета различных операций в приложении.
*/
public class OperationCounter
{
    private static readonly Lazy<OperationCounter> _instance = 
        new Lazy<OperationCounter>(() => new OperationCounter());
    
    private readonly Dictionary<string, int> _counters = new Dictionary<string, int>();
    private readonly object _lock = new object();

    private OperationCounter() { }

    public static OperationCounter Instance => _instance.Value;

    public void Increment(string operationName)
    {
        lock (_lock)
        {
            if (_counters.ContainsKey(operationName))
            {
                _counters[operationName]++;
            }
            else
            {
                _counters[operationName] = 1;
            }
        }
    }

    public int GetCount(string operationName)
    {
        lock (_lock)
        {
            return _counters.TryGetValue(operationName, out int count) ? count : 0;
        }
    }

    public void Reset(string operationName)
    {
        lock (_lock)
        {
            _counters[operationName] = 0;
        }
    }

    public void ResetAll()
    {
        lock (_lock)
        {
            _counters.Clear();
        }
    }

    public void DisplayStatistics()
    {
        lock (_lock)
        {
            Console.WriteLine("Статистика операций:");
            foreach (var counter in _counters)
            {
                Console.WriteLine($"  {counter.Key}: {counter.Value}");
            }
        }
    }
}

#endregion

#region Демонстрация работы

public static class SingletonDemo
{
    public static void Main_()
    {
        Console.WriteLine("=== Демонстрация паттерна Singleton ===\n");

        // Демонстрация различных реализаций Singleton
        DemonstrateBasicSingleton();
        Console.WriteLine();

        DemonstrateThreadSafeSingleton();
        Console.WriteLine();

        DemonstrateLazySingleton();
        Console.WriteLine();

        // Практические примеры
        DemonstrateLogger();
        Console.WriteLine();

        DemonstrateAppConfig();
        Console.WriteLine();

        DemonstrateOperationCounter();
    }

    private static void DemonstrateBasicSingleton()
    {
        Console.WriteLine("--- Простой Singleton ---");
        
        var singleton1 = SimpleSingleton.Instance;
        var singleton2 = SimpleSingleton.Instance;
        
        Console.WriteLine($"singleton1 == singleton2: {ReferenceEquals(singleton1, singleton2)}");
        singleton1.DoSomething();
    }

    private static void DemonstrateThreadSafeSingleton()
    {
        Console.WriteLine("--- Потокобезопасный Singleton ---");
        
        var singleton1 = ThreadSafeSingleton.Instance;
        var singleton2 = ThreadSafeSingleton.Instance;
        
        Console.WriteLine($"singleton1 == singleton2: {ReferenceEquals(singleton1, singleton2)}");
        singleton1.DoSomething();
    }

    private static void DemonstrateLazySingleton()
    {
        Console.WriteLine("--- Ленивый Singleton ---");
        
        var singleton1 = LazySingleton.Instance;
        var singleton2 = LazySingleton.Instance;
        
        Console.WriteLine($"singleton1 == singleton2: {ReferenceEquals(singleton1, singleton2)}");
        singleton1.DoSomething();
    }

    private static void DemonstrateLogger()
    {
        Console.WriteLine("--- Пример: Логгер ---");
        
        var logger = Logger.Instance;
        
        logger.LogInfo("Приложение запущено");
        logger.LogError("Произошла ошибка подключения");
        logger.LogInfo("Ошибка исправлена");
        
        Console.WriteLine($"Всего записей в логе: {logger.GetAllLogs().Count}");
    }

    private static void DemonstrateAppConfig()
    {
        Console.WriteLine("--- Пример: Конфигурация ---");
        
        var config = AppConfig.Instance;
        
        config.DisplayAllSettings();
        
        // Изменяем настройку
        config.SetSetting("LogLevel", "Debug");
        Console.WriteLine($"Новый уровень логирования: {config.GetSetting("LogLevel")}");
    }

    private static void DemonstrateOperationCounter()
    {
        Console.WriteLine("--- Пример: Счетчик операций ---");
        
        var counter = OperationCounter.Instance;
        
        // Имитируем различные операции
        counter.Increment("Login");
        counter.Increment("Login");
        counter.Increment("Logout");
        counter.Increment("FileUpload");
        counter.Increment("Login");
        
        counter.DisplayStatistics();
    }
}

#endregion

/*
   Преимущества паттерна Singleton:
   ===============================
   
   1. Гарантированно один экземпляр класса
   2. Глобальная точка доступа к экземпляру
   3. Ленивая инициализация (создание по требованию)
   4. Контроль над созданием экземпляра
   5. Экономия памяти (один объект вместо множества)
   
   Недостатки паттерна Singleton:
   =============================
   
   1. Нарушает принцип единственной ответственности
   2. Может маскировать плохой дизайн (скрытые зависимости)
   3. Усложняет юнит-тестирование
   4. Проблемы в многопоточной среде (если не реализован правильно)
   5. Может привести к тесной связанности компонентов
   6. Сложно расширять (наследование ограничено)
   
   Альтернативы Singleton:
   ======================
   
   1. Dependency Injection (внедрение зависимостей)
   2. Статические классы (для утилит без состояния)
   3. Фабричные методы
   4. Реестр объектов (Registry pattern)
*/


// Задача - написать свой правильный синглтон для класса, который будет работать с конкретной базой данных
// (за бд можно взять текстовый файл, но лучше будет попрактиковаться с SQLite)