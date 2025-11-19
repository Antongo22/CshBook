using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_5
{
    #region Теория
    /*
     * Паттерн Strategy (Стратегия)
     * ============================
     *
     * Стратегия — поведенческий паттерн, который определяет семейство схожих алгоритмов,
     * инкапсулирует каждый из них и делает их взаимозаменяемыми внутри объекта-контекста.
     *
     * Идея: Выделяем алгоритм в отдельный интерфейс, реализуем различные варианты алгоритма
     * как отдельные классы-стратегии и позволяем контексту (клиентскому коду) подставлять
     * нужную стратегию во время выполнения.
     *
     * Когда применять:
     * - Когда нужно легко переключаться между разными вариантами алгоритма (рантайм-конфигурация)
     * - Когда есть условные конструкции if/else/switch, выбирающие алгоритм
     * - Когда нужно изолировать сложную логику и упростить тестирование
     * - Когда хотите соблюдать принципы SOLID (OCP: открыто для расширения, закрыто для изменения)
     *
     * Плюсы:
     * - Замена алгоритма на лету
     * - Упрощение условий в коде (меньше if/switch)
     * - Лучшее тестирование (каждая стратегия тестируется отдельно)
     * - Соблюдение SRP и OCP
     *
     * Минусы:
     * - Усложнение за счёт дополнительных классов и интерфейсов
     * - Контексту нужно знать о существовании стратегий
     */
    #endregion

    #region Базовая структура
    // Интерфейс стратегии
    public interface IDiscountStrategy
    {
        decimal Calculate(decimal amount);
    }

    // Конкретные стратегии скидок
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal Calculate(decimal amount) => amount;
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _percent; // 0..100
        public PercentageDiscountStrategy(decimal percent)
        {
            if (percent < 0 || percent > 100) throw new ArgumentOutOfRangeException(nameof(percent));
            _percent = percent;
        }
        public decimal Calculate(decimal amount) => amount * (1 - _percent / 100m);
    }

    public class ThresholdDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _threshold;
        private readonly decimal _fixedDiscount;
        public ThresholdDiscountStrategy(decimal threshold, decimal fixedDiscount)
        {
            if (threshold <= 0) throw new ArgumentOutOfRangeException(nameof(threshold));
            if (fixedDiscount < 0) throw new ArgumentOutOfRangeException(nameof(fixedDiscount));
            _threshold = threshold;
            _fixedDiscount = fixedDiscount;
        }
        public decimal Calculate(decimal amount)
        {
            return amount >= _threshold ? Math.Max(0, amount - _fixedDiscount) : amount;
        }
    }

    // Контекст — использует стратегию, но не знает деталей реализации
    public class CheckoutContext
    {
        private IDiscountStrategy _strategy;
        public CheckoutContext(IDiscountStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public void SetStrategy(IDiscountStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public decimal CalculateTotal(decimal amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
            return _strategy.Calculate(amount);
        }
    }
    #endregion

    #region Пример 1: Сортировка со стратегиями
    public interface ISortStrategy<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> items);
    }

    public class AscendingSortStrategy : ISortStrategy<int>
    {
        public IEnumerable<int> Sort(IEnumerable<int> items) => items.OrderBy(x => x);
    }

    public class DescendingSortStrategy : ISortStrategy<int>
    {
        public IEnumerable<int> Sort(IEnumerable<int> items) => items.OrderByDescending(x => x);
    }

    public class EvenFirstSortStrategy : ISortStrategy<int>
    {
        public IEnumerable<int> Sort(IEnumerable<int> items) => items
            .OrderBy(x => x % 2) // чётные сначала (0), потом нечётные (1)
            .ThenBy(x => x);
    }

    public class SortContext
    {
        private ISortStrategy<int> _strategy;
        public SortContext(ISortStrategy<int> strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
        public void SetStrategy(ISortStrategy<int> strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
        public IEnumerable<int> Sort(IEnumerable<int> items) => _strategy.Sort(items);
    }
    #endregion

    #region Пример 2: Сжатие данных (интерфейсы одинаковые, реализации разные)
    public interface ICompressor
    {
        byte[] Compress(byte[] data);
        byte[] Decompress(byte[] data);
    }

    // Упрощённые имитации реализаций
    public class ZipCompressor : ICompressor
    {
        public byte[] Compress(byte[] data)
        {
            // Имитация: добавим префикс "ZIP" для наглядности
            return (new byte[] { 0x5A, 0x49, 0x50 }).Concat(data).ToArray();
        }
        public byte[] Decompress(byte[] data)
        {
            // Уберём префикс, если есть
            if (data.Length >= 3 && data[0] == 0x5A && data[1] == 0x49 && data[2] == 0x50)
                return data.Skip(3).ToArray();
            return data;
        }
    }

    public class GzipCompressor : ICompressor
    {
        public byte[] Compress(byte[] data)
        {
            return (new byte[] { 0x47, 0x5A, 0x49, 0x50 }).Concat(data).ToArray(); // "GZIP"
        }
        public byte[] Decompress(byte[] data)
        {
            if (data.Length >= 4 && data[0] == 0x47 && data[1] == 0x5A && data[2] == 0x49 && data[3] == 0x50)
                return data.Skip(4).ToArray();
            return data;
        }
    }

    public class CompressionContext
    {
        private ICompressor _compressor;
        public CompressionContext(ICompressor compressor)
        {
            _compressor = compressor ?? throw new ArgumentNullException(nameof(compressor));
        }
        public void SetStrategy(ICompressor compressor)
        {
            _compressor = compressor ?? throw new ArgumentNullException(nameof(compressor));
        }
        public byte[] Compress(byte[] data) => _compressor.Compress(data ?? Array.Empty<byte>());
        public byte[] Decompress(byte[] data) => _compressor.Decompress(data ?? Array.Empty<byte>());
    }
    #endregion

    #region Пример 3: Выбор способа оплаты
    public interface IPaymentStrategy
    {
        bool Pay(decimal amount);
    }

    public class CardPayment : IPaymentStrategy
    {
        public bool Pay(decimal amount)
        {
            Console.WriteLine($"Оплата картой: {amount:C}");
            return true;
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        public bool Pay(decimal amount)
        {
            Console.WriteLine($"Оплата PayPal: {amount:C}");
            return true;
        }
    }

    public class CryptoPayment : IPaymentStrategy
    {
        public bool Pay(decimal amount)
        {
            Console.WriteLine($"Оплата криптовалютой: {amount:C}");
            return true;
        }
    }

    public class PaymentProcessor
    {
        private IPaymentStrategy _strategy;
        public PaymentProcessor(IPaymentStrategy strategy)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
        public void SetStrategy(IPaymentStrategy strategy) => _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        public bool Process(decimal amount)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            return _strategy.Pay(amount);
        }
    }
    #endregion

    #region Best Practices и типичные ошибки
    /*
     * Рекомендации:
     * - Храните только поведение в стратегиях, а состояние — в контексте (кроме необходимых параметров)
     * - Инъектируйте стратегии через конструктор (DI) или сеттеры
     * - Используйте фабрики/реестры для выбора стратегии по конфигурации
     * - Делайте стратегии неизменяемыми, где возможно
     *
     * Ошибки:
     * - Слишком раздутый интерфейс стратегии
     * - Утечка деталей конкретной стратегии наружу (нарушение инкапсуляции)
     * - Смешивание выбора стратегии с бизнес-логикой контекста
     */
    #endregion

    #region Демонстрация
    public static class StrategyDemo
    {
        public static void Main_()
        {
            Console.WriteLine("=== Демонстрация паттерна Strategy ===\n");
            DemoDiscounts();
            Console.WriteLine();
            DemoSorting();
            Console.WriteLine();
            DemoCompression();
            Console.WriteLine();
            DemoPayments();
        }

        private static void DemoDiscounts()
        {
            Console.WriteLine("--- Стратегии скидок ---");
            var checkout = new CheckoutContext(new NoDiscountStrategy());
            Console.WriteLine($"Без скидки: {checkout.CalculateTotal(1000m)}");
            checkout.SetStrategy(new PercentageDiscountStrategy(15));
            Console.WriteLine($"Процентная скидка 15%: {checkout.CalculateTotal(1000m)}");
            checkout.SetStrategy(new ThresholdDiscountStrategy(800m, 150m));
            Console.WriteLine($"Пороговая скидка 150 при сумме >= 800: {checkout.CalculateTotal(1000m)}");
        }

        private static void DemoSorting()
        {
            Console.WriteLine("--- Стратегии сортировки ---");
            var data = new List<int> { 7, 2, 5, 4, 9, 1, 6, 3, 8 };

            var sortContext = new SortContext(new AscendingSortStrategy());
            Console.WriteLine("По возрастанию:   " + string.Join(", ", sortContext.Sort(data)));

            sortContext.SetStrategy(new DescendingSortStrategy());
            Console.WriteLine("По убыванию:      " + string.Join(", ", sortContext.Sort(data)));

            sortContext.SetStrategy(new EvenFirstSortStrategy());
            Console.WriteLine("Сначала чётные:   " + string.Join(", ", sortContext.Sort(data)));
        }

        private static void DemoCompression()
        {
            Console.WriteLine("--- Стратегии сжатия ---");
            var content = Encoding.UTF8.GetBytes("Hello Strategy!");
            var context = new CompressionContext(new ZipCompressor());

            var zip = context.Compress(content);
            Console.WriteLine($"ZIP: {BitConverter.ToString(zip.Take(4).ToArray())} ... size={zip.Length}");
            var unzip = context.Decompress(zip);
            Console.WriteLine($"ZIP->data: {Encoding.UTF8.GetString(unzip)}");

            context.SetStrategy(new GzipCompressor());
            var gz = context.Compress(content);
            Console.WriteLine($"GZIP: {BitConverter.ToString(gz.Take(5).ToArray())} ... size={gz.Length}");
            var ungz = context.Decompress(gz);
            Console.WriteLine($"GZIP->data: {Encoding.UTF8.GetString(ungz)}");
        }

        private static void DemoPayments()
        {
            Console.WriteLine("--- Стратегии оплаты ---");
            var processor = new PaymentProcessor(new CardPayment());
            processor.Process(1999.99m);
            processor.SetStrategy(new PayPalPayment());
            processor.Process(499.50m);
            processor.SetStrategy(new CryptoPayment());
            processor.Process(0.025m);
        }
    }
    #endregion
    
    #region Задачи (без ответов)
    /*
     * ЗАДАЧИ ПО ПАТТЕРНУ STRATEGY
     * ===========================
     * В этом разделе — практические задания. Реализуйте интерфейсы и классы согласно описанию.
     * Подсказки допускаются в комментариях, но готовых решений нет.
     */

    // Задача 1 (Базовая): Стратегии форматирования строк
    // ---------------------------------------------------
    // Требования:
    // - Создайте интерфейс IStringFormatStrategy с методом string Format(string input)
    // - Реализуйте стратегии: PlainText, UpperCase, LowerCase, SnakeCase, KebabCase
    // - Создайте контекст StringFormatter с возможностью смены стратегии
    // - Напишите метод демонстрации TestStringFormatter()
    // Подсказка: для SnakeCase/KebabCase преобразуйте пробелы и регистр.

    // TODO: интерфейс и стратегии форматирования
    // public interface IStringFormatStrategy { string Format(string input); }
    // public class PlainTextStrategy : IStringFormatStrategy { ... }
    // public class UpperCaseStrategy : IStringFormatStrategy { ... }
    // public class LowerCaseStrategy : IStringFormatStrategy { ... }
    // public class SnakeCaseStrategy : IStringFormatStrategy { ... }
    // public class KebabCaseStrategy : IStringFormatStrategy { ... }
    // public class StringFormatter { ... SetStrategy(...), Format(...)}
    // public static class StrategyTask1Demo { public static void TestStringFormatter() { /* демонстрация */ } }


    // Задача 2 (Средняя): Селектор маршрута доставки
    // ------------------------------------------------
    // Сценарий: разная доставка (Курьер, Почта, Самовывоз, Дрон) в зависимости от расстояния, веса и приоритета.
    // Требования:
    // - Интерфейс IShippingStrategy { ShippingResult Calculate(ShippingRequest req) }
    // - Реализации: CourierShipping, PostalShipping, PickupShipping, DroneShipping
    // - Контекст ShippingCalculator: SetStrategy(...), Calculate(...)
    // - Валидация входных данных (некорректные значения — исключения)
    // Подсказки по моделям ниже.

    // TODO: модели данных для доставки
    // public enum ShippingPriority { Low, Normal, High }
    // public class ShippingRequest { public double DistanceKm; public double WeightKg; public ShippingPriority Priority; }
    // public class ShippingResult { public decimal Price; public double EtaHours; public string CarrierName = ""; }
    // TODO: интерфейс и реализации стратегий + контекст
    // public interface IShippingStrategy { ShippingResult Calculate(ShippingRequest req); }
    // public class CourierShipping : IShippingStrategy { ... }
    // public class PostalShipping : IShippingStrategy { ... }
    // public class PickupShipping : IShippingStrategy { ... }
    // public class DroneShipping : IShippingStrategy { ... }
    // public class ShippingCalculator { ... }
    // public static class StrategyTask2Demo { public static void TestShipping() { /* демонстрация */ } }


    // Задача 3 (Средняя+): Пайплайн валидации регистрации пользователя
    // ---------------------------------------------------------------
    // Сценарий: набор проверок при регистрации (email-формат, уникальность, парольная политика, возраст >= 13, согласие).
    // Требования:
    // - Интерфейс IValidationStrategy { ValidationResult Validate(UserRegistration input) }
    // - Реализации: EmailFormat, EmailUnique (используйте HashSet для имитации БД), PasswordPolicy, AgePolicy, TermsAccepted
    // - Контекст ValidationPipeline: принимает список стратегий, применяет их по порядку, собирает ошибки
    // - Метод демонстрации TestValidationPipeline()
    // Подсказка: ValidationResult с коллекцией ошибок; IsValid => Errors.Count == 0.

    // TODO: модели и стратегии валидации
    // public class UserRegistration { public string Email = ""; public string Password = ""; public int Age; public bool TermsAccepted; }
    // public class ValidationResult { public bool IsValid => Errors.Count == 0; public List<string> Errors = new(); }
    // public interface IValidationStrategy { ValidationResult Validate(UserRegistration input); }
    // public class EmailFormatValidation : IValidationStrategy { ... }
    // public class EmailUniqueValidation : IValidationStrategy { ... }
    // public class PasswordPolicyValidation : IValidationStrategy { ... }
    // public class AgePolicyValidation : IValidationStrategy { ... }
    // public class TermsAcceptedValidation : IValidationStrategy { ... }
    // public class ValidationPipeline { /* List<IValidationStrategy> */ }
    // public static class StrategyTask3Demo { public static void TestValidationPipeline() { /* демонстрация */ } }


    // Задача 4 (Продвинутая): Поиск пути в графе разными стратегиями
    // --------------------------------------------------------------
    // Сценарий: алгоритмы поиска пути — BFS (невзвешенный), Dijkstra, A*, Greedy Best-First.
    // Требования:
    // - Интерфейс IPathFindingStrategy { PathResult Find(Graph g, int from, int to) }
    // - Реализации: BfsStrategy, DijkstraStrategy, AStarStrategy (эвристика), GreedyStrategy
    // - Контекст PathFinder: SetStrategy(...), Find(...)
    // - Упростите структуры данных при необходимости.

    // TODO: модели графа и стратегии
    // public class Graph { public Dictionary<int, List<(int to, double weight)>> Adj = new(); public Dictionary<int, (double x, double y)> Coords = new(); }
    // public class PathResult { public List<int> Path = new(); public double Cost; }
    // public interface IPathFindingStrategy { PathResult Find(Graph g, int from, int to); }
    // public class BfsStrategy : IPathFindingStrategy { ... }
    // public class DijkstraStrategy : IPathFindingStrategy { ... }
    // public class AStarStrategy : IPathFindingStrategy { ... }
    // public class GreedyStrategy : IPathFindingStrategy { ... }
    // public class PathFinder { ... }
    // public static class StrategyTask4Demo { public static void TestPathFinding() { /* демонстрация */ } }


    // Задача 5 (Интеграционная): Плагинная система конвертации документов
    // -------------------------------------------------------------------
    // Сценарий: вход — текст + метаданные; выход — разные форматы: Markdown, HTML, PDF(имитация), DOCX(имитация).
    // Требования:
    // - Интерфейс IExportStrategy { byte[] Export(Document doc) }
    // - Реализации: MarkdownExport, HtmlExport, PdfExport(Mock), DocxExport(Mock)
    // - Контекст DocumentExporter: SetStrategy(...), Export(...)
    // - Реестр/фабрика стратегий по строковому ключу формата ("md", "html", "pdf", "docx")
    // - Метод демонстрации TestDocumentExporter()
    // Подсказка: PDF/DOCX можно имитировать префиксами в байтовом массиве, как в примере с компрессорами.

    // TODO: модели и контекст экспорта
    // public class Document { public string Title = ""; public string Body = ""; public Dictionary<string,string> Meta = new(); }
    // public interface IExportStrategy { byte[] Export(Document doc); }
    // public class MarkdownExport : IExportStrategy { ... }
    // public class HtmlExport : IExportStrategy { ... }
    // public class PdfExport : IExportStrategy { ... } // mock
    // public class DocxExport : IExportStrategy { ... } // mock
    // public class DocumentExporter { ... }
    // public static class StrategyTask5Demo { public static void TestDocumentExporter() { /* демонстрация */ } }

    #endregion
}
