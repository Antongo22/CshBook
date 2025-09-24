using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace CshBook.Lessons.Глава_5
{
    /*
     * ЗАДАЧИ НА ПАТТЕРН SINGLETON
     * ==========================
     * 
     * В этом файле собраны практические задачи для изучения паттерна Singleton.
     * Каждая задача содержит описание, требования и шаблон для решения.
     * 
     * Рекомендуется решать задачи по порядку, так как сложность возрастает.
     */

    #region Задача 1: Базовый Singleton - Менеджер подключений

    /*
     * ЗАДАЧА 1: Менеджер подключений к базе данных
     * ============================================
     * 
     * Создайте класс DatabaseManager, который:
     * 1. Реализует паттерн Singleton
     * 2. Хранит строку подключения к базе данных
     * 3. Имеет методы для установки и получения строки подключения
     * 4. Имеет метод Connect() который имитирует подключение к БД
     * 5. Имеет метод Disconnect() который имитирует отключение от БД
     * 6. Отслеживает статус подключения (подключен/отключен)
     * 
     * Требования:
     * - Используйте потокобезопасную реализацию с Lazy<T>
     * - Добавьте валидацию строки подключения
     * - Логируйте все операции подключения/отключения
     */

    public class DatabaseManager
    {
        // TODO: Реализуйте Singleton паттерн
        // TODO: Добавьте поля для хранения строки подключения и статуса
        // TODO: Реализуйте методы Connect, Disconnect, SetConnectionString, GetConnectionString
        // TODO: Добавьте валидацию и логирование

        // Пример использования (раскомментируйте после реализации):
        /*
        public static void TestDatabaseManager()
        {
            var db = DatabaseManager.Instance;
            db.SetConnectionString("Server=localhost;Database=TestDB;");
            db.Connect();
            Console.WriteLine($"Статус: {db.IsConnected}");
            db.Disconnect();
        }
        */
    }

    #endregion

    #region Задача 2: Кэш-менеджер

    /*
     * ЗАДАЧА 2: Кэш-менеджер
     * ======================
     * 
     * Создайте класс CacheManager, который:
     * 1. Реализует паттерн Singleton
     * 2. Хранит данные в виде пар ключ-значение
     * 3. Поддерживает время жизни (TTL) для кэшированных элементов
     * 4. Автоматически удаляет устаревшие элементы
     * 5. Имеет ограничение на максимальное количество элементов
     * 6. При превышении лимита удаляет самые старые элементы (LRU)
     * 
     * Методы:
     * - Set(key, value, ttlSeconds) - добавить/обновить элемент
     * - Get(key) - получить элемент (null если не найден или устарел)
     * - Remove(key) - удалить элемент
     * - Clear() - очистить весь кэш
     * - GetStatistics() - получить статистику (количество элементов, попаданий, промахов)
     */

    public class CacheManager
    {
        // TODO: Реализуйте Singleton паттерн
        // TODO: Создайте внутренний класс CacheItem для хранения значения и времени истечения
        // TODO: Используйте Dictionary для хранения кэша
        // TODO: Реализуйте логику TTL и LRU
        // TODO: Добавьте потокобезопасность
        // TODO: Реализуйте статистику попаданий/промахов

        // Пример использования:
        /*
        public static void TestCacheManager()
        {
            var cache = CacheManager.Instance;
            
            cache.Set("user:123", "John Doe", 60); // TTL 60 секунд
            cache.Set("config:theme", "dark", 300); // TTL 5 минут
            
            var user = cache.Get("user:123");
            Console.WriteLine($"Пользователь: {user}");
            
            cache.GetStatistics();
        }
        */
    }

    #endregion

    #region Задача 3: Система уведомлений

    /*
     * ЗАДАЧА 3: Система уведомлений
     * =============================
     * 
     * Создайте класс NotificationSystem, который:
     * 1. Реализует паттерн Singleton
     * 2. Поддерживает различные типы уведомлений (Email, SMS, Push)
     * 3. Имеет очередь уведомлений для отправки
     * 4. Поддерживает приоритеты уведомлений (High, Medium, Low)
     * 5. Имеет фоновый поток для обработки очереди
     * 6. Ведет статистику отправленных уведомлений
     * 7. Поддерживает настройки для каждого типа уведомлений
     * 
     * Классы для реализации:
     * - NotificationType (enum): Email, SMS, Push
     * - Priority (enum): High, Medium, Low
     * - Notification (класс): содержит тип, приоритет, получателя, сообщение, время создания
     * - NotificationSettings (класс): настройки для каждого типа уведомлений
     */

    public enum NotificationType
    {
        Email,
        SMS,
        Push
    }

    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public class Notification
    {
        // TODO: Реализуйте класс Notification
        // Поля: Id, Type, Priority, Recipient, Message, CreatedAt, ProcessedAt
    }

    public class NotificationSettings
    {
        // TODO: Реализуйте класс настроек
        // Поля: IsEnabled, MaxRetries, RetryDelaySeconds для каждого типа уведомлений
    }

    public class NotificationSystem
    {
        // TODO: Реализуйте Singleton паттерн
        // TODO: Создайте очередь с приоритетом (PriorityQueue или SortedDictionary)
        // TODO: Реализуйте фоновый поток для обработки очереди
        // TODO: Добавьте методы Send, Configure, GetStatistics, Start, Stop
        // TODO: Реализуйте имитацию отправки уведомлений с задержками

        // Пример использования:
        /*
        public static void TestNotificationSystem()
        {
            var notificationSystem = NotificationSystem.Instance;
            
            notificationSystem.Start();
            
            notificationSystem.Send(NotificationType.Email, Priority.High, 
                "admin@example.com", "Критическая ошибка в системе!");
            
            notificationSystem.Send(NotificationType.SMS, Priority.Medium, 
                "+1234567890", "Ваш код: 123456");
            
            Thread.Sleep(5000); // Ждем обработки
            
            notificationSystem.GetStatistics();
            notificationSystem.Stop();
        }
        */
    }

    #endregion

    #region Задача 4: Файловый менеджер с блокировками

    /*
     * ЗАДАЧА 4: Файловый менеджер
     * ===========================
     * 
     * Создайте класс FileManager, который:
     * 1. Реализует паттерн Singleton
     * 2. Управляет доступом к файлам (чтение/запись)
     * 3. Предотвращает одновременную запись в один файл
     * 4. Ведет лог всех операций с файлами
     * 5. Поддерживает блокировки файлов
     * 6. Имеет методы для работы с текстовыми и бинарными файлами
     * 7. Автоматически создает резервные копии перед изменением
     * 8. Поддерживает откат изменений
     * 
     * Дополнительные требования:
     * - Потокобезопасность
     * - Обработка исключений
     * - Валидация путей к файлам
     * - Статистика операций
     */

    public class FileOperation
    {
        public string FilePath { get; set; }
        public string Operation { get; set; } // Read, Write, Delete, etc.
        public DateTime Timestamp { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }

    public class FileManager
    {
        // TODO: Реализуйте Singleton паттерн
        // TODO: Используйте Dictionary<string, object> для блокировок файлов
        // TODO: Реализуйте методы ReadText, WriteText, ReadBytes, WriteBytes
        // TODO: Добавьте методы CreateBackup, RestoreBackup
        // TODO: Реализуйте логирование операций
        // TODO: Добавьте валидацию и обработку исключений

        // Пример использования:
        /*
        public static void TestFileManager()
        {
            var fileManager = FileManager.Instance;
            
            string testFile = "test.txt";
            
            fileManager.WriteText(testFile, "Hello, World!");
            string content = fileManager.ReadText(testFile);
            Console.WriteLine($"Содержимое файла: {content}");
            
            fileManager.CreateBackup(testFile);
            fileManager.WriteText(testFile, "Modified content");
            fileManager.RestoreBackup(testFile);
            
            fileManager.GetOperationHistory();
        }
        */
    }

    #endregion

    #region Задача 5: Игровой менеджер состояния

    /*
     * ЗАДАЧА 5: Игровой менеджер состояния
     * ====================================
     * 
     * Создайте класс GameStateManager для простой игры, который:
     * 1. Реализует паттерн Singleton
     * 2. Управляет состоянием игры (Menu, Playing, Paused, GameOver)
     * 3. Хранит информацию об игроке (имя, счет, уровень, жизни)
     * 4. Поддерживает сохранение/загрузку состояния в файл
     * 5. Ведет таблицу рекордов (топ-10)
     * 6. Имеет систему достижений
     * 7. Поддерживает откат к предыдущему состоянию (undo)
     * 8. Логирует все изменения состояния
     * 
     * Перечисления и классы:
     * - GameState (enum): Menu, Playing, Paused, GameOver
     * - Player (класс): Name, Score, Level, Lives, PlayTime
     * - Achievement (класс): Id, Name, Description, IsUnlocked, UnlockedAt
     * - HighScore (класс): PlayerName, Score, Level, Date
     */

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }

    public class Player
    {
        // TODO: Реализуйте класс Player
        // Поля: Name, Score, Level, Lives, PlayTime, CreatedAt
    }

    public class Achievement
    {
        // TODO: Реализуйте класс Achievement
        // Поля: Id, Name, Description, IsUnlocked, UnlockedAt
    }

    public class HighScore
    {
        // TODO: Реализуйте класс HighScore
        // Поля: PlayerName, Score, Level, Date
    }

    public class GameStateManager
    {
        // TODO: Реализуйте Singleton паттерн
        // TODO: Добавьте поля для текущего состояния, игрока, достижений, рекордов
        // TODO: Реализуйте методы управления состоянием
        // TODO: Добавьте сохранение/загрузку в JSON или XML
        // TODO: Реализуйте систему достижений с проверками
        // TODO: Добавьте возможность отката состояния

        // Методы для реализации:
        // - ChangeState(GameState newState)
        // - UpdateScore(int points)
        // - LevelUp()
        // - LoseLife()
        // - CheckAchievements()
        // - SaveGame(string filePath)
        // - LoadGame(string filePath)
        // - AddHighScore()
        // - GetHighScores()
        // - Undo()

        // Пример использования:
        /*
        public static void TestGameStateManager()
        {
            var gameManager = GameStateManager.Instance;
            
            gameManager.StartNewGame("Player1");
            gameManager.ChangeState(GameState.Playing);
            
            gameManager.UpdateScore(100);
            gameManager.UpdateScore(250);
            gameManager.LevelUp();
            
            gameManager.SaveGame("savegame.json");
            
            gameManager.ChangeState(GameState.GameOver);
            gameManager.AddHighScore();
            
            var highScores = gameManager.GetHighScores();
            foreach (var score in highScores)
            {
                Console.WriteLine($"{score.PlayerName}: {score.Score}");
            }
        }
        */
    }

    #endregion

    #region Задача 6: Многопоточный Singleton

    /*
     * ЗАДАЧА 6: Тестирование потокобезопасности
     * =========================================
     * 
     * Создайте тестовый класс ThreadSafetyTest, который:
     * 1. Тестирует различные реализации Singleton в многопоточной среде
     * 2. Создает множество потоков, которые одновременно обращаются к Singleton
     * 3. Проверяет, что создается только один экземпляр
     * 4. Измеряет производительность различных реализаций
     * 5. Выводит статистику по времени выполнения
     * 
     * Реализуйте несколько версий Singleton для сравнения:
     * - Небезопасная версия (для демонстрации проблемы)
     * - Версия с lock
     * - Версия с double-check locking
     * - Версия с Lazy<T>
     * - Версия со статическим конструктором
     */

    // Небезопасная версия (только для демонстрации!)
    public class UnsafeSingleton
    {
        // TODO: Реализуйте небезопасную версию Singleton
    }

    // Версия с простой блокировкой
    public class LockSingleton
    {
        // TODO: Реализуйте версию с lock
    }

    // Версия с двойной проверкой
    public class DoubleCheckLockSingleton
    {
        // TODO: Реализуйте версию с double-check locking
    }

    // Версия с Lazy<T>
    public class LazyThreadSafeSingleton
    {
        // TODO: Реализуйте версию с Lazy<T>
    }

    // Версия со статическим конструктором
    public class StaticConstructorSingleton
    {
        // TODO: Реализуйте версию со статическим конструктором
    }

    public class ThreadSafetyTest
    {
        // TODO: Реализуйте методы тестирования
        // - TestSingletonCreation<T>() где T : Singleton
        // - MeasurePerformance<T>(int threadCount, int operationsPerThread)
        // - RunAllTests()

        // Пример использования:
        /*
        public static void RunThreadSafetyTests()
        {
            var tester = new ThreadSafetyTest();
            
            Console.WriteLine("Тестирование потокобезопасности Singleton...\n");
            
            tester.TestSingletonCreation<UnsafeSingleton>();
            tester.TestSingletonCreation<LockSingleton>();
            tester.TestSingletonCreation<DoubleCheckLockSingleton>();
            tester.TestSingletonCreation<LazyThreadSafeSingleton>();
            tester.TestSingletonCreation<StaticConstructorSingleton>();
            
            Console.WriteLine("\nТестирование производительности...\n");
            
            tester.MeasurePerformance<LockSingleton>(100, 1000);
            tester.MeasurePerformance<DoubleCheckLockSingleton>(100, 1000);
            tester.MeasurePerformance<LazyThreadSafeSingleton>(100, 1000);
        }
        */
    }

    #endregion

    #region Бонусная задача: Singleton с параметрами

    /*
     * БОНУСНАЯ ЗАДАЧА: Параметризованный Singleton
     * ===========================================
     * 
     * Создайте класс ParameterizedSingleton, который:
     * 1. Может быть инициализирован с параметрами только один раз
     * 2. При повторных попытках инициализации с другими параметрами выбрасывает исключение
     * 3. Предоставляет метод для проверки, инициализирован ли экземпляр
     * 4. Поддерживает сброс и повторную инициализацию (с подтверждением)
     * 
     * Это более сложная версия Singleton, которая показывает,
     * как можно расширить базовый паттерн для специфических нужд.
     */

    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }
        public int TimeoutSeconds { get; set; }
        public int MaxConnections { get; set; }
        public bool EnableLogging { get; set; }
    }

    public class ParameterizedSingleton
    {
        // TODO: Реализуйте Singleton с возможностью инициализации параметрами
        // TODO: Добавьте проверку на повторную инициализацию
        // TODO: Реализуйте методы Initialize, Reset, IsInitialized
        // TODO: Добавьте валидацию параметров

        // Методы для реализации:
        // - Initialize(DatabaseConfig config)
        // - Reset(bool confirm = false)
        // - IsInitialized { get; }
        // - GetConfig()

        // Пример использования:
        /*
        public static void TestParameterizedSingleton()
        {
            var singleton = ParameterizedSingleton.Instance;
            
            var config = new DatabaseConfig
            {
                ConnectionString = "Server=localhost;Database=Test;",
                TimeoutSeconds = 30,
                MaxConnections = 100,
                EnableLogging = true
            };
            
            singleton.Initialize(config);
            
            // Попытка повторной инициализации должна вызвать исключение
            try
            {
                singleton.Initialize(new DatabaseConfig());
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ожидаемое исключение: {ex.Message}");
            }
            
            Console.WriteLine($"Инициализирован: {singleton.IsInitialized}");
            
            // Сброс с подтверждением
            singleton.Reset(confirm: true);
            Console.WriteLine($"После сброса: {singleton.IsInitialized}");
        }
        */
    }

    #endregion

    /*
     * РЕКОМЕНДАЦИИ ПО РЕШЕНИЮ ЗАДАЧ:
     * ==============================
     * 
     * 1. Начинайте с простых задач и постепенно переходите к сложным
     * 
     * 2. Всегда используйте потокобезопасные реализации Singleton:
     *    - Lazy<T> (рекомендуется для большинства случаев)
     *    - Double-check locking (если нужен контроль над инициализацией)
     *    - Статический конструктор (для простых случаев)
     * 
     * 3. Не забывайте про обработку исключений и валидацию входных данных
     * 
     * 4. Добавляйте логирование для отладки и мониторинга
     * 
     * 5. Тестируйте многопоточность с помощью Task.Run или Thread
     * 
     * 6. Документируйте ваш код и добавляйте примеры использования
     * 
     * 7. Помните о недостатках Singleton и рассматривайте альтернативы:
     *    - Dependency Injection
     *    - Статические классы
     *    - Фабричные методы
     * 
     * КРИТЕРИИ ОЦЕНКИ:
     * ================
     * 
     * ✅ Правильная реализация паттерна Singleton
     * ✅ Потокобезопасность
     * ✅ Обработка исключений
     * ✅ Валидация входных данных
     * ✅ Читаемость и документированность кода
     * ✅ Тестирование функциональности
     * ✅ Следование принципам SOLID (где применимо)
     */
}
