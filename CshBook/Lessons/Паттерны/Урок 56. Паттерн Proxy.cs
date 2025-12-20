using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace CshBook.Lessons.Глава_5
{
    #region Теория
    /*
     * Паттерн Proxy (Заместитель)
     * ===========================
     *
     * Proxy — это структурный паттерн проектирования, который позволяет подставлять
     * вместо реальных объектов специальные объекты-заменители. Эти объекты перехватывают
     * вызовы к оригинальному объекту, позволяя сделать что-то до или после передачи вызова.
     *
     * Проблема:
     * - Нужно контролировать доступ к объекту
     * - Объект создаётся долго или потребляет много ресурсов
     * - Объект находится в другом адресном пространстве (удалённый сервис)
     * - Нужно логировать обращения к объекту
     * - Нужно кэшировать результаты работы объекта
     * - Нужно отложить создание объекта до первого обращения
     *
     * Решение:
     * Proxy создаёт объект-заместитель с тем же интерфейсом, что и у реального объекта.
     * Клиент работает с Proxy, думая, что работает с реальным объектом.
     * Proxy контролирует доступ к реальному объекту и может добавлять дополнительное поведение.
     *
     * Виды Proxy:
     * 
     * 1. Virtual Proxy (Виртуальный заместитель)
     *    - Откладывает создание "тяжёлого" объекта до момента, когда он действительно нужен
     *    - Пример: ленивая загрузка больших изображений
     * 
     * 2. Protection Proxy (Защитный заместитель)
     *    - Контролирует доступ к объекту на основе прав доступа
     *    - Пример: проверка прав пользователя перед выполнением операции
     * 
     * 3. Remote Proxy (Удалённый заместитель)
     *    - Представляет объект, находящийся в другом адресном пространстве
     *    - Пример: веб-сервисы, RPC, .NET Remoting
     * 
     * 4. Logging Proxy (Логирующий заместитель)
     *    - Логирует все вызовы к реальному объекту
     *    - Пример: аудит операций с базой данных
     * 
     * 5. Caching Proxy (Кэширующий заместитель)
     *    - Кэширует результаты вызовов дорогих операций
     *    - Пример: кэширование результатов запросов к API
     * 
     * 6. Smart Reference Proxy (Умная ссылка)
     *    - Выполняет дополнительные действия при обращении к объекту
     *    - Пример: подсчёт ссылок, освобождение ресурсов
     *
     * Когда использовать Proxy:
     * - Ленивая инициализация (отложенное создание объекта)
     * - Контроль доступа к объекту
     * - Логирование обращений к объекту
     * - Кэширование результатов
     * - Работа с удалёнными объектами
     * - Управление жизненным циклом объекта
     *
     * Преимущества:
     * + Контроль над объектом без изменения его кода
     * + Возможность работать с объектом, который ещё не создан
     * + Добавление функциональности без изменения клиентского кода
     * + Снижение потребления ресурсов
     *
     * Недостатки:
     * - Усложнение кода из-за дополнительных классов
     * - Возможна задержка ответа от сервиса
     * - Дополнительный слой абстракции
     *
     * Отличия от других паттернов:
     * - Adapter: изменяет интерфейс, Proxy — сохраняет тот же интерфейс
     * - Decorator: добавляет поведение, Proxy — контролирует доступ
     * - Facade: упрощает интерфейс, Proxy — дублирует интерфейс
     */

    /*
       Структура паттерна Proxy
       ========================
       
       1. Subject (Субъект)
          - Общий интерфейс для RealSubject и Proxy
          - Определяет методы, которые должны быть у обоих
       
       2. RealSubject (Реальный субъект)
          - Настоящий объект, содержащий основную бизнес-логику
          - Может быть "тяжёлым" или требовать контроля доступа
       
       3. Proxy (Заместитель)
          - Хранит ссылку на RealSubject
          - Реализует тот же интерфейс, что и RealSubject
          - Контролирует доступ к RealSubject
          - Может создавать RealSubject при первом обращении
       
       4. Client (Клиент)
          - Работает с Subject через общий интерфейс
          - Не знает, работает ли с Proxy или RealSubject
    */
    #endregion

    #region Пример 1: Virtual Proxy (Ленивая загрузка изображения)

    // Subject — общий интерфейс
    public interface IImage
    {
        void Display();
        string GetInfo();
    }

    // RealSubject — реальное изображение (тяжёлый объект)
    public class RealImage : IImage
    {
        private string _fileName;
        private byte[] _imageData; // Представим, что это большой массив данных

        public RealImage(string fileName)
        {
            _fileName = fileName;
            LoadImageFromDisk(); // Долгая операция
        }

        private void LoadImageFromDisk()
        {
            Console.WriteLine($"[RealImage] Загрузка изображения '{_fileName}' с диска...");
            Thread.Sleep(2000); // Имитация долгой загрузки
            _imageData = new byte[1024 * 1024]; // Имитация 1MB данных
            Console.WriteLine($"[RealImage] Изображение '{_fileName}' загружено!");
        }

        public void Display()
        {
            Console.WriteLine($"[RealImage] Отображение изображения '{_fileName}'");
        }

        public string GetInfo()
        {
            return $"Изображение '{_fileName}', размер: {_imageData.Length / 1024} KB";
        }
    }

    // Proxy — ленивая загрузка
    public class ImageProxy : IImage
    {
        private string _fileName;
        private RealImage _realImage; // Создаётся только при необходимости

        public ImageProxy(string fileName)
        {
            _fileName = fileName;
            Console.WriteLine($"[ImageProxy] Создан прокси для '{_fileName}' (изображение ещё не загружено)");
        }

        public void Display()
        {
            // Ленивая инициализация — создаём объект только при первом вызове
            if (_realImage == null)
            {
                Console.WriteLine($"[ImageProxy] Первое обращение к изображению — загружаем...");
                _realImage = new RealImage(_fileName);
            }
            _realImage.Display();
        }

        public string GetInfo()
        {
            if (_realImage == null)
            {
                return $"Прокси для '{_fileName}' (изображение не загружено)";
            }
            return _realImage.GetInfo();
        }
    }

    #endregion

    #region Пример 2: Protection Proxy (Контроль доступа)

    // Subject — интерфейс для работы с документом
    public interface IDocument
    {
        void View();
        void Edit(string newContent);
        void Delete();
    }

    // RealSubject — реальный документ
    public class SecretDocument : IDocument
    {
        private string _content;
        private string _name;

        public SecretDocument(string name, string content)
        {
            _name = name;
            _content = content;
        }

        public void View()
        {
            Console.WriteLine($"[SecretDocument] Просмотр документа '{_name}':");
            Console.WriteLine($"Содержимое: {_content}");
        }

        public void Edit(string newContent)
        {
            Console.WriteLine($"[SecretDocument] Редактирование документа '{_name}'");
            _content = newContent;
            Console.WriteLine("Документ изменён.");
        }

        public void Delete()
        {
            Console.WriteLine($"[SecretDocument] Документ '{_name}' удалён!");
            _content = null;
        }
    }

    // Proxy с проверкой прав доступа
    public class DocumentProxy : IDocument
    {
        private SecretDocument _document;
        private string _userRole;

        public DocumentProxy(string name, string content, string userRole)
        {
            _document = new SecretDocument(name, content);
            _userRole = userRole;
        }

        public void View()
        {
            Console.WriteLine($"[DocumentProxy] Проверка прав для просмотра (роль: {_userRole})...");
            
            // Просмотр доступен всем
            _document.View();
        }

        public void Edit(string newContent)
        {
            Console.WriteLine($"[DocumentProxy] Проверка прав для редактирования (роль: {_userRole})...");
            
            if (_userRole == "Admin" || _userRole == "Editor")
            {
                Console.WriteLine("[DocumentProxy] Доступ разрешён.");
                _document.Edit(newContent);
            }
            else
            {
                Console.WriteLine("[DocumentProxy] ДОСТУП ЗАПРЕЩЁН! Недостаточно прав для редактирования.");
            }
        }

        public void Delete()
        {
            Console.WriteLine($"[DocumentProxy] Проверка прав для удаления (роль: {_userRole})...");
            
            if (_userRole == "Admin")
            {
                Console.WriteLine("[DocumentProxy] Доступ разрешён.");
                _document.Delete();
            }
            else
            {
                Console.WriteLine("[DocumentProxy] ДОСТУП ЗАПРЕЩЁН! Только Admin может удалять документы.");
            }
        }
    }

    #endregion

    #region Пример 3: Logging Proxy (Логирование вызовов)

    // Subject — интерфейс базы данных
    public interface IDatabase
    {
        string GetUser(int id);
        void SaveUser(int id, string name);
        void DeleteUser(int id);
    }

    // RealSubject — реальная база данных
    public class Database : IDatabase
    {
        private Dictionary<int, string> _users = new Dictionary<int, string>
        {
            { 1, "Иван" },
            { 2, "Мария" },
            { 3, "Алексей" }
        };

        public string GetUser(int id)
        {
            Thread.Sleep(100); // Имитация задержки запроса к БД
            return _users.ContainsKey(id) ? _users[id] : null;
        }

        public void SaveUser(int id, string name)
        {
            Thread.Sleep(150); // Имитация задержки
            _users[id] = name;
        }

        public void DeleteUser(int id)
        {
            Thread.Sleep(120); // Имитация задержки
            _users.Remove(id);
        }
    }

    // Proxy с логированием
    public class DatabaseLoggingProxy : IDatabase
    {
        private Database _database = new Database();
        private List<string> _operationLog = new List<string>();

        public string GetUser(int id)
        {
            var timestamp = DateTime.Now;
            Console.WriteLine($"[LOG {timestamp:HH:mm:ss}] Вызов GetUser(id: {id})");
            
            var stopwatch = Stopwatch.StartNew();
            var result = _database.GetUser(id);
            stopwatch.Stop();
            
            var logEntry = $"{timestamp:HH:mm:ss} | GetUser(id: {id}) | Результат: {result ?? "NULL"} | Время: {stopwatch.ElapsedMilliseconds}ms";
            _operationLog.Add(logEntry);
            Console.WriteLine($"[LOG] Завершено за {stopwatch.ElapsedMilliseconds}ms");
            
            return result;
        }

        public void SaveUser(int id, string name)
        {
            var timestamp = DateTime.Now;
            Console.WriteLine($"[LOG {timestamp:HH:mm:ss}] Вызов SaveUser(id: {id}, name: {name})");
            
            var stopwatch = Stopwatch.StartNew();
            _database.SaveUser(id, name);
            stopwatch.Stop();
            
            var logEntry = $"{timestamp:HH:mm:ss} | SaveUser(id: {id}, name: {name}) | Время: {stopwatch.ElapsedMilliseconds}ms";
            _operationLog.Add(logEntry);
            Console.WriteLine($"[LOG] Завершено за {stopwatch.ElapsedMilliseconds}ms");
        }

        public void DeleteUser(int id)
        {
            var timestamp = DateTime.Now;
            Console.WriteLine($"[LOG {timestamp:HH:mm:ss}] Вызов DeleteUser(id: {id})");
            
            var stopwatch = Stopwatch.StartNew();
            _database.DeleteUser(id);
            stopwatch.Stop();
            
            var logEntry = $"{timestamp:HH:mm:ss} | DeleteUser(id: {id}) | Время: {stopwatch.ElapsedMilliseconds}ms";
            _operationLog.Add(logEntry);
            Console.WriteLine($"[LOG] Завершено за {stopwatch.ElapsedMilliseconds}ms");
        }

        public void ShowLog()
        {
            Console.WriteLine("\n=== История операций ===");
            foreach (var entry in _operationLog)
            {
                Console.WriteLine(entry);
            }
        }
    }

    #endregion

    #region Пример 4: Caching Proxy (Кэширующий заместитель)

    // Subject — интерфейс API
    public interface IWeatherApi
    {
        string GetWeather(string city);
        double GetTemperature(string city);
    }

    // RealSubject — реальный API (медленный)
    public class WeatherApi : IWeatherApi
    {
        private Random _random = new Random();

        public string GetWeather(string city)
        {
            Console.WriteLine($"[WeatherAPI] Запрос к внешнему API для города '{city}'...");
            Thread.Sleep(1500); // Имитация сетевой задержки
            
            string[] conditions = { "Солнечно", "Облачно", "Дождь", "Снег", "Туман" };
            return conditions[_random.Next(conditions.Length)];
        }

        public double GetTemperature(string city)
        {
            Console.WriteLine($"[WeatherAPI] Запрос температуры для города '{city}'...");
            Thread.Sleep(1500); // Имитация сетевой задержки
            
            return _random.Next(-20, 35);
        }
    }

    // Proxy с кэшированием
    public class CachingWeatherProxy : IWeatherApi
    {
        private WeatherApi _api = new WeatherApi();
        private Dictionary<string, (string weather, DateTime cachedAt)> _weatherCache = 
            new Dictionary<string, (string, DateTime)>();
        private Dictionary<string, (double temp, DateTime cachedAt)> _temperatureCache = 
            new Dictionary<string, (double, DateTime)>();
        private TimeSpan _cacheLifetime = TimeSpan.FromSeconds(10);

        public string GetWeather(string city)
        {
            // Проверяем кэш
            if (_weatherCache.ContainsKey(city))
            {
                var cached = _weatherCache[city];
                var age = DateTime.Now - cached.cachedAt;
                
                if (age < _cacheLifetime)
                {
                    Console.WriteLine($"[CachingProxy] Возврат из кэша для '{city}' (возраст: {age.TotalSeconds:F1}s)");
                    return cached.weather;
                }
                else
                {
                    Console.WriteLine($"[CachingProxy] Кэш устарел для '{city}' (возраст: {age.TotalSeconds:F1}s)");
                }
            }

            // Запрашиваем у реального API
            Console.WriteLine($"[CachingProxy] Кэш отсутствует — обращаемся к API...");
            var result = _api.GetWeather(city);
            
            // Сохраняем в кэш
            _weatherCache[city] = (result, DateTime.Now);
            Console.WriteLine($"[CachingProxy] Результат сохранён в кэш");
            
            return result;
        }

        public double GetTemperature(string city)
        {
            // Проверяем кэш
            if (_temperatureCache.ContainsKey(city))
            {
                var cached = _temperatureCache[city];
                var age = DateTime.Now - cached.cachedAt;
                
                if (age < _cacheLifetime)
                {
                    Console.WriteLine($"[CachingProxy] Температура из кэша для '{city}': {cached.temp}°C");
                    return cached.temp;
                }
            }

            // Запрашиваем у реального API
            Console.WriteLine($"[CachingProxy] Запрос температуры к API...");
            var result = _api.GetTemperature(city);
            
            // Сохраняем в кэш
            _temperatureCache[city] = (result, DateTime.Now);
            
            return result;
        }

        public void ClearCache()
        {
            _weatherCache.Clear();
            _temperatureCache.Clear();
            Console.WriteLine("[CachingProxy] Кэш очищен");
        }
    }

    #endregion

    #region Пример 5: Smart Reference Proxy (Подсчёт обращений)

    // Subject — интерфейс для работы с файлом
    public interface IFileManager
    {
        string ReadFile(string fileName);
        void WriteFile(string fileName, string content);
    }

    // RealSubject — реальный менеджер файлов
    public class FileManager : IFileManager
    {
        private Dictionary<string, string> _files = new Dictionary<string, string>
        {
            { "config.txt", "server=localhost;port=5432" },
            { "readme.txt", "Это файл README" }
        };

        public string ReadFile(string fileName)
        {
            Console.WriteLine($"[FileManager] Чтение файла '{fileName}'");
            return _files.ContainsKey(fileName) ? _files[fileName] : null;
        }

        public void WriteFile(string fileName, string content)
        {
            Console.WriteLine($"[FileManager] Запись в файл '{fileName}'");
            _files[fileName] = content;
        }
    }

    // Smart Reference Proxy — подсчёт обращений и статистика
    public class SmartFileProxy : IFileManager
    {
        private FileManager _fileManager = new FileManager();
        private Dictionary<string, int> _readCount = new Dictionary<string, int>();
        private Dictionary<string, int> _writeCount = new Dictionary<string, int>();
        private int _totalOperations = 0;

        public string ReadFile(string fileName)
        {
            Console.WriteLine($"[SmartProxy] Перехват запроса на чтение '{fileName}'");
            
            // Увеличиваем счётчик
            if (!_readCount.ContainsKey(fileName))
                _readCount[fileName] = 0;
            _readCount[fileName]++;
            _totalOperations++;

            // Вызываем реальный объект
            var result = _fileManager.ReadFile(fileName);
            
            Console.WriteLine($"[SmartProxy] Файл '{fileName}' прочитан {_readCount[fileName]} раз(а)");
            
            return result;
        }

        public void WriteFile(string fileName, string content)
        {
            Console.WriteLine($"[SmartProxy] Перехват запроса на запись '{fileName}'");
            
            // Увеличиваем счётчик
            if (!_writeCount.ContainsKey(fileName))
                _writeCount[fileName] = 0;
            _writeCount[fileName]++;
            _totalOperations++;

            // Вызываем реальный объект
            _fileManager.WriteFile(fileName, content);
            
            Console.WriteLine($"[SmartProxy] Файл '{fileName}' записан {_writeCount[fileName]} раз(а)");
        }

        public void ShowStatistics()
        {
            Console.WriteLine("\n=== Статистика обращений к файлам ===");
            Console.WriteLine($"Всего операций: {_totalOperations}");
            
            Console.WriteLine("\nЧтение:");
            foreach (var kvp in _readCount)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} раз(а)");
            }
            
            Console.WriteLine("\nЗапись:");
            foreach (var kvp in _writeCount)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value} раз(а)");
            }
        }
    }

    #endregion

    #region Пример 6: Remote Proxy (Имитация удалённого сервиса)

    // Subject — интерфейс сервиса
    public interface IPaymentService
    {
        bool ProcessPayment(string cardNumber, decimal amount);
        string GetTransactionStatus(string transactionId);
    }

    // RealSubject — реальный удалённый сервис
    public class RemotePaymentService : IPaymentService
    {
        public bool ProcessPayment(string cardNumber, decimal amount)
        {
            Console.WriteLine($"[RemoteService] Обработка платежа на сумму {amount:C}...");
            Thread.Sleep(2000); // Имитация сетевой задержки
            Console.WriteLine("[RemoteService] Платёж успешно обработан");
            return true;
        }

        public string GetTransactionStatus(string transactionId)
        {
            Console.WriteLine($"[RemoteService] Проверка статуса транзакции {transactionId}...");
            Thread.Sleep(1000);
            return "Completed";
        }
    }

    // Proxy для удалённого сервиса
    public class PaymentServiceProxy : IPaymentService
    {
        private RemotePaymentService _service;
        private bool _isConnected = false;

        public bool ProcessPayment(string cardNumber, decimal amount)
        {
            Console.WriteLine($"[PaymentProxy] Получен запрос на платёж {amount:C}");
            
            // Валидация перед отправкой
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 16)
            {
                Console.WriteLine("[PaymentProxy] Ошибка: Некорректный номер карты");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine("[PaymentProxy] Ошибка: Сумма должна быть положительной");
                return false;
            }

            // Подключение к сервису
            EnsureConnection();

            // Маскируем номер карты в логах
            string maskedCard = $"****-****-****-{cardNumber.Substring(12)}";
            Console.WriteLine($"[PaymentProxy] Отправка запроса (карта: {maskedCard})...");

            try
            {
                return _service.ProcessPayment(cardNumber, amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PaymentProxy] Ошибка при обработке платежа: {ex.Message}");
                return false;
            }
        }

        public string GetTransactionStatus(string transactionId)
        {
            EnsureConnection();
            return _service.GetTransactionStatus(transactionId);
        }

        private void EnsureConnection()
        {
            if (!_isConnected)
            {
                Console.WriteLine("[PaymentProxy] Установка соединения с удалённым сервисом...");
                Thread.Sleep(500);
                _service = new RemotePaymentService();
                _isConnected = true;
                Console.WriteLine("[PaymentProxy] Соединение установлено");
            }
        }
    }

    #endregion

    #region Пример 7: Комбинированный Proxy (Кэширование + Логирование + Защита)

    // Subject — интерфейс репозитория данных
    file interface IDataRepository
    {
        string GetData(string key);
        void SetData(string key, string value);
    }

    // RealSubject — реальный репозиторий
    public class DataRepository : IDataRepository
    {
        private Dictionary<string, string> _storage = new Dictionary<string, string>();

        public string GetData(string key)
        {
            Console.WriteLine($"    [DataRepository] Получение данных для ключа '{key}' из хранилища");
            Thread.Sleep(500); // Имитация задержки
            return _storage.ContainsKey(key) ? _storage[key] : null;
        }

        public void SetData(string key, string value)
        {
            Console.WriteLine($"    [DataRepository] Сохранение данных для ключа '{key}' в хранилище");
            Thread.Sleep(500); // Имитация задержки
            _storage[key] = value;
        }
    }

    // Комбинированный Proxy с множественными функциями
    public class AdvancedDataRepositoryProxy : IDataRepository
    {
        private DataRepository _repository = new DataRepository();
        private Dictionary<string, string> _cache = new Dictionary<string, string>();
        private List<string> _accessLog = new List<string>();
        private string _userRole;

        public AdvancedDataRepositoryProxy(string userRole)
        {
            _userRole = userRole;
            Console.WriteLine($"[AdvancedProxy] Инициализация прокси для роли '{userRole}'");
        }

        public string GetData(string key)
        {
            // 1. Логирование
            var logEntry = $"[{DateTime.Now:HH:mm:ss}] {_userRole} -> GetData('{key}')";
            _accessLog.Add(logEntry);
            Console.WriteLine($"[AdvancedProxy] LOG: {logEntry}");

            // 2. Проверка прав доступа
            if (!HasReadPermission())
            {
                Console.WriteLine($"[AdvancedProxy] ЗАЩИТА: Доступ запрещён для роли '{_userRole}'");
                return null;
            }

            // 3. Проверка кэша
            if (_cache.ContainsKey(key))
            {
                Console.WriteLine($"[AdvancedProxy] КЭШ: Данные найдены в кэше");
                return _cache[key];
            }

            // 4. Обращение к реальному объекту
            Console.WriteLine($"[AdvancedProxy] Данные не в кэше — обращение к репозиторию");
            var result = _repository.GetData(key);

            // 5. Сохранение в кэш
            if (result != null)
            {
                _cache[key] = result;
                Console.WriteLine($"[AdvancedProxy] Результат сохранён в кэш");
            }

            return result;
        }

        public void SetData(string key, string value)
        {
            // 1. Логирование
            var logEntry = $"[{DateTime.Now:HH:mm:ss}] {_userRole} -> SetData('{key}', '{value}')";
            _accessLog.Add(logEntry);
            Console.WriteLine($"[AdvancedProxy] LOG: {logEntry}");

            // 2. Проверка прав доступа
            if (!HasWritePermission())
            {
                Console.WriteLine($"[AdvancedProxy] ЗАЩИТА: Запись запрещена для роли '{_userRole}'");
                return;
            }

            // 3. Инвалидация кэша
            if (_cache.ContainsKey(key))
            {
                _cache.Remove(key);
                Console.WriteLine($"[AdvancedProxy] КЭШ: Данные для ключа '{key}' удалены из кэша");
            }

            // 4. Запись в репозиторий
            _repository.SetData(key, value);
        }

        private bool HasReadPermission()
        {
            return _userRole == "Admin" || _userRole == "User" || _userRole == "Guest";
        }

        private bool HasWritePermission()
        {
            return _userRole == "Admin" || _userRole == "User";
        }

        public void ShowAccessLog()
        {
            Console.WriteLine("\n=== Журнал доступа ===");
            foreach (var entry in _accessLog)
            {
                Console.WriteLine(entry);
            }
        }

        public void ClearCache()
        {
            _cache.Clear();
            Console.WriteLine("[AdvancedProxy] Кэш очищен");
        }
    }

    #endregion

    #region Демонстрация

    public static class ProxyDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║       ДЕМОНСТРАЦИЯ ПАТТЕРНА PROXY (ЗАМЕСТИТЕЛЬ)║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            DemoVirtualProxy();
            DemoProtectionProxy();
            DemoLoggingProxy();
            DemoCachingProxy();
            DemoSmartReferenceProxy();
            DemoAdvancedProxy();
        }

        private static void DemoVirtualProxy()
        {
            Console.WriteLine("\n[1] === Virtual Proxy (Ленивая загрузка) ===\n");

            Console.WriteLine("--- Создание прокси для изображений ---");
            IImage image1 = new ImageProxy("photo1.jpg");
            IImage image2 = new ImageProxy("photo2.jpg");
            IImage image3 = new ImageProxy("photo3.jpg");

            Console.WriteLine("\nПрокси созданы, но изображения ещё не загружены!");
            Console.WriteLine("Получение информации о прокси:");
            Console.WriteLine(image1.GetInfo());

            Console.WriteLine("\n--- Первое обращение к изображению ---");
            image1.Display(); // Здесь произойдёт загрузка

            Console.WriteLine("\n--- Повторное обращение к тому же изображению ---");
            image1.Display(); // Загрузка уже не нужна

            Console.WriteLine("\n--- Информация после загрузки ---");
            Console.WriteLine(image1.GetInfo());
        }

        private static void DemoProtectionProxy()
        {
            Console.WriteLine("\n\n[2] === Protection Proxy (Контроль доступа) ===\n");

            Console.WriteLine("--- Пользователь с ролью 'Guest' ---");
            IDocument guestDoc = new DocumentProxy("Секретный отчёт", "Конфиденциальная информация", "Guest");
            guestDoc.View();
            guestDoc.Edit("Попытка изменить");
            guestDoc.Delete();

            Console.WriteLine("\n--- Пользователь с ролью 'Editor' ---");
            IDocument editorDoc = new DocumentProxy("Отчёт Q4", "Данные за квартал", "Editor");
            editorDoc.View();
            editorDoc.Edit("Обновлённые данные");
            editorDoc.Delete();

            Console.WriteLine("\n--- Пользователь с ролью 'Admin' ---");
            IDocument adminDoc = new DocumentProxy("Системные настройки", "config data", "Admin");
            adminDoc.View();
            adminDoc.Edit("Новые настройки");
            adminDoc.Delete();
        }

        private static void DemoLoggingProxy()
        {
            Console.WriteLine("\n\n[3] === Logging Proxy (Логирование операций) ===\n");

            var db = new DatabaseLoggingProxy();

            // Выполняем различные операции
            var user1 = db.GetUser(1);
            Console.WriteLine($"Получен пользователь: {user1}\n");

            var user2 = db.GetUser(2);
            Console.WriteLine($"Получен пользователь: {user2}\n");

            db.SaveUser(4, "Дмитрий");
            Console.WriteLine();

            db.DeleteUser(3);
            Console.WriteLine();

            var user3 = db.GetUser(3);
            Console.WriteLine($"Получен пользователь: {user3}\n");

            // Показываем полный лог операций
            db.ShowLog();
        }

        private static void DemoCachingProxy()
        {
            Console.WriteLine("\n\n[4] === Caching Proxy (Кэширование результатов) ===\n");

            var weatherApi = new CachingWeatherProxy();

            Console.WriteLine("--- Первый запрос для Москвы ---");
            var weather1 = weatherApi.GetWeather("Москва");
            Console.WriteLine($"Погода в Москве: {weather1}\n");

            Console.WriteLine("--- Повторный запрос для Москвы (из кэша) ---");
            var weather2 = weatherApi.GetWeather("Москва");
            Console.WriteLine($"Погода в Москве: {weather2}\n");

            Console.WriteLine("--- Запрос для Санкт-Петербурга ---");
            var weather3 = weatherApi.GetWeather("Санкт-Петербург");
            Console.WriteLine($"Погода в Санкт-Петербурге: {weather3}\n");

            Console.WriteLine("--- Ждём истечения кэша (10 секунд) ---");
            Thread.Sleep(11000);

            Console.WriteLine("\n--- Запрос для Москвы после истечения кэша ---");
            var weather4 = weatherApi.GetWeather("Москва");
            Console.WriteLine($"Погода в Москве: {weather4}");
        }

        private static void DemoSmartReferenceProxy()
        {
            Console.WriteLine("\n\n[5] === Smart Reference Proxy (Подсчёт обращений) ===\n");

            var fileManager = new SmartFileProxy();

            // Читаем файлы
            fileManager.ReadFile("config.txt");
            Console.WriteLine();

            fileManager.ReadFile("readme.txt");
            Console.WriteLine();

            fileManager.ReadFile("config.txt"); // Повторное чтение
            Console.WriteLine();

            // Записываем файлы
            fileManager.WriteFile("log.txt", "Новая запись в логе");
            Console.WriteLine();

            fileManager.WriteFile("config.txt", "server=newhost;port=3306");
            Console.WriteLine();

            fileManager.ReadFile("config.txt"); // Ещё одно чтение
            Console.WriteLine();

            // Показываем статистику
            fileManager.ShowStatistics();
        }

        private static void DemoAdvancedProxy()
        {
            Console.WriteLine("\n\n[6] === Advanced Proxy (Комбинация: Кэш + Лог + Защита) ===\n");

            Console.WriteLine("--- Admin ---");
            var adminProxy = new AdvancedDataRepositoryProxy("Admin");
            
            adminProxy.SetData("api_key", "secret123");
            Console.WriteLine();

            var data1 = adminProxy.GetData("api_key");
            Console.WriteLine($"Полученные данные: {data1}\n");

            var data2 = adminProxy.GetData("api_key"); // Из кэша
            Console.WriteLine($"Полученные данные: {data2}\n");

            adminProxy.ShowAccessLog();

            Console.WriteLine("\n\n--- Guest (ограниченные права) ---");
            var guestProxy = new AdvancedDataRepositoryProxy("Guest");
            
            guestProxy.SetData("test", "value"); // Будет запрещено
            Console.WriteLine();

            guestProxy.ShowAccessLog();
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
 * 1. Virtual Proxy для видео
 *    Создайте интерфейс IVideo с методами Play(), Pause(), Stop().
 *    Реализуйте класс RealVideo, который "загружается" 3 секунды при создании.
 *    Создайте VideoProxy, который откладывает загрузку до первого вызова Play().
 *    Продемонстрируйте, что создание прокси происходит мгновенно.
 * 
 * 2. Protection Proxy для банковского счёта
 *    Создайте интерфейс IBankAccount с методами GetBalance(), Deposit(), Withdraw().
 *    Реализуйте класс BankAccount с реальной логикой.
 *    Создайте BankAccountProxy, который:
 *    - Проверяет PIN-код перед каждой операцией
 *    - Блокирует счёт после 3 неудачных попыток
 *    - Разрешает Deposit() всем, но Withdraw() только после проверки PIN
 * 
 * 3. Logging Proxy для калькулятора
 *    Создайте интерфейс ICalculator с методами Add(), Subtract(), Multiply(), Divide().
 *    Реализуйте Calculator с обычной логикой.
 *    Создайте LoggingCalculatorProxy, который:
 *    - Логирует каждую операцию с параметрами и результатом
 *    - Сохраняет историю вычислений
 *    - Имеет метод ShowHistory() для вывода всех операций
 * 
 * СРЕДНИЕ ЗАДАЧИ
 * ==============
 * 
 * 4. Caching Proxy для API переводчика
 *    Создайте интерфейс ITranslator с методом Translate(string text, string fromLang, string toLang).
 *    Реализуйте TranslatorApi, который "обращается к API" с задержкой 2 секунды.
 *    Создайте CachingTranslatorProxy, который:
 *    - Кэширует переводы (ключ: комбинация текста и языков)
 *    - Выводит информацию о проценте попаданий в кэш
 *    - Имеет ограничение размера кэша (например, 100 записей, при превышении удаляет старые)
 * 
 * 5. Smart Reference Proxy для управления соединениями
 *    Создайте интерфейс IDatabaseConnection с методами Open(), Close(), ExecuteQuery(string sql).
 *    Реализуйте DatabaseConnection с реальной логикой.
 *    Создайте ConnectionProxy, который:
 *    - Подсчитывает количество активных соединений
 *    - Автоматически закрывает соединение после N запросов
 *    - Предупреждает, если количество соединений превышает лимит
 *    - Ведёт статистику по времени выполнения запросов
 * 
 * 6. Validation Proxy для формы регистрации
 *    Создайте интерфейс IRegistrationService с методом Register(string email, string password, int age).
 *    Реализуйте RegistrationService с базовой логикой.
 *    Создайте ValidationProxy, который:
 *    - Проверяет формат email (регулярное выражение)
 *    - Проверяет сложность пароля (минимум 8 символов, цифра, буква)
 *    - Проверяет возраст (>= 18)
 *    - Выбрасывает понятные исключения при ошибках валидации
 * 
 * СЛОЖНЫЕ ЗАДАЧИ
 * ==============
 * 
 * 7. Multi-level Proxy (Цепочка прокси)
 *    Создайте систему, где несколько прокси оборачивают друг друга:
 *    RealService -> LoggingProxy -> CachingProxy -> SecurityProxy -> Client
 *    
 *    Каждый прокси добавляет свою функциональность:
 *    - SecurityProxy: проверяет токен авторизации
 *    - CachingProxy: кэширует результаты
 *    - LoggingProxy: логирует все вызовы
 *    
 *    Продемонстрируйте, как запрос проходит через все слои.
 * 
 * 8. Dynamic Proxy с использованием Reflection
 *    Создайте универсальный LoggingProxy<T>, который:
 *    - Работает с любым интерфейсом T
 *    - Использует Reflection для перехвата всех вызовов методов
 *    - Автоматически логирует имя метода, параметры и результат
 *    - Измеряет время выполнения каждого метода
 *    
 *    Подсказка: используйте DispatchProxy (или создайте wrapper вручную)
 * 
 * 9. Rate Limiting Proxy
 *    Создайте интерфейс IApiClient с методами Get(), Post(), Put(), Delete().
 *    Реализуйте ApiClient с базовой логикой.
 *    Создайте RateLimitingProxy, который:
 *    - Ограничивает количество запросов (например, 10 запросов в минуту)
 *    - Отслеживает временные окна для подсчёта запросов
 *    - Блокирует запросы при превышении лимита
 *    - Показывает, сколько запросов осталось до лимита
 *    - Автоматически сбрасывает счётчик через минуту
 * 
 * 10. Composite Proxy (сложный сценарий)
 *     Создайте систему управления облачным хранилищем:
 *     
 *     Интерфейс ICloudStorage:
 *     - UploadFile(string fileName, byte[] data)
 *     - DownloadFile(string fileName)
 *     - DeleteFile(string fileName)
 *     
 *     Создайте CloudStorageProxy, который комбинирует:
 *     - Компрессию данных перед загрузкой
 *     - Шифрование данных
 *     - Проверку прав доступа
 *     - Кэширование часто запрашиваемых файлов
 *     - Логирование всех операций
 *     - Retry-механизм при ошибках сети (3 попытки с задержкой)
 *     
 *     Каждая функция должна быть реализована как отдельный слой прокси,
 *     которые можно комбинировать.
 */

#endregion

/*
 * ═══════════════════════════════════════════════════════════════
 * СРАВНЕНИЕ PROXY С ДРУГИМИ ПАТТЕРНАМИ
 * ═══════════════════════════════════════════════════════════════
 * 
 * PROXY vs DECORATOR:
 * -------------------
 * Proxy:
 * - Контролирует доступ к объекту
 * - Может создавать объект самостоятельно
 * - Обычно управляет жизненным циклом объекта
 * - Клиент часто не знает о существовании Proxy
 * 
 * Decorator:
 * - Добавляет новое поведение
 * - Не управляет созданием объекта
 * - Множественные декораторы можно комбинировать
 * - Клиент явно создаёт цепочку декораторов
 * 
 * PROXY vs ADAPTER:
 * -----------------
 * Proxy:
 * - Имеет тот же интерфейс, что и реальный объект
 * - Предоставляет контроль доступа
 * 
 * Adapter:
 * - Преобразует один интерфейс в другой
 * - Обеспечивает совместимость
 * 
 * PROXY vs FACADE:
 * ----------------
 * Proxy:
 * - Один-к-одному (один прокси для одного объекта)
 * - Тот же интерфейс
 * 
 * Facade:
 * - Упрощает сложную подсистему
 * - Предоставляет новый, упрощённый интерфейс
 */

/*
 * ═══════════════════════════════════════════════════════════════
 * ИТОГОВЫЕ ЗАМЕЧАНИЯ
 * ═══════════════════════════════════════════════════════════════
 * 
 * Proxy — мощный паттерн для контроля доступа к объектам. Он встречается
 * повсеместно в реальной разработке, часто даже незаметно для разработчика.
 * 
 * Примеры Proxy в реальном мире:
 * ✓ ORM-фреймворки (Entity Framework) — ленивая загрузка связанных сущностей
 * ✓ Веб-прокси серверы — кэширование и фильтрация запросов
 * ✓ Удалённые вызовы (WCF, gRPC, REST clients) — локальный объект для удалённого сервиса
 * ✓ Кэширующие библиотеки — прозрачное кэширование методов
 * ✓ Системы логирования — автоматический аудит операций
 * 
 * Используйте Proxy, когда:
 * ✓ Нужна ленивая инициализация тяжёлого объекта
 * ✓ Требуется контроль доступа
 * ✓ Нужно логировать обращения
 * ✓ Важно кэширование результатов
 * ✓ Работаете с удалёнными объектами
 * 
 * Не используйте Proxy, когда:
 * ✗ Объект простой и создаётся быстро
 * ✗ Нет необходимости в дополнительном контроле
 * ✗ Дополнительный слой только усложняет код
 * 
 * Помните: Proxy должен быть прозрачным для клиента — клиент не должен знать,
 * что работает с заместителем, а не с реальным объектом!
 */

