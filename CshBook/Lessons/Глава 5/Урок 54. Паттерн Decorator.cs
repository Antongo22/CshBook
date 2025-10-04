using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CshBook.Lessons.Глава_5
{
    #region Теория
    /*
     * Паттерн Decorator (Декоратор)
     * ==============================
     *
     * Декоратор — структурный паттерн проектирования, который позволяет динамически добавлять
     * объектам новую функциональность, оборачивая их в полезные "обёртки".
     *
     * Суть паттерна:
     * Вместо изменения класса или создания множества подклассов для каждой комбинации функций,
     * мы создаём объекты-декораторы, которые оборачивают исходный объект и добавляют ему
     * дополнительное поведение. Декораторы реализуют тот же интерфейс, что и оборачиваемый объект.
     *
     * Структура:
     * - Component (интерфейс) — общий интерфейс для оборачиваемых объектов и декораторов
     * - ConcreteComponent — конкретная реализация компонента
     * - Decorator (абстрактный) — базовый класс декоратора, содержит ссылку на Component
     * - ConcreteDecorator — конкретные декораторы, добавляющие функциональность
     *
     * Когда применять:
     * - Когда нужно динамически добавлять обязанности объектам
     * - Когда расширение через наследование непрактично (комбинаторный взрыв подклассов)
     * - Когда нужно добавлять/удалять функциональность во время выполнения
     * - Когда изменение базового класса невозможно или нежелательно
     *
     * Примеры использования:
     * - Java I/O Streams (BufferedReader, InputStreamReader — классические декораторы)
     * - C# Stream (GZipStream, CryptoStream оборачивают другие потоки)
     * - UI компоненты с прокруткой, рамками, тенями
     * - Логирование, кеширование, валидация вызовов методов
     *
     * Плюсы:
     * - Гибкость: комбинируйте декораторы для разных комбинаций функций
     * - Следует принципу единственной ответственности (SRP)
     * - Следует принципу открытости/закрытости (OCP)
     * - Добавление функциональности без изменения существующего кода
     *
     * Минусы:
     * - Множество мелких классов (усложнение архитектуры)
     * - Сложная инициализация при множестве слоёв декораторов
     * - Трудность отладки многослойных декораторов
     * - Порядок оборачивания может быть важен
     */
    #endregion

    #region Базовая структура
    // Component — интерфейс компонента
    public interface INotifier
    {
        void Send(string message);
    }

    // ConcreteComponent — базовая реализация
    public class EmailNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"[EMAIL] Отправка: {message}");
        }
    }

    // Базовый декоратор
    public abstract class NotifierDecorator : INotifier
    {
        protected INotifier _wrapped;

        protected NotifierDecorator(INotifier notifier)
        {
            _wrapped = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        public virtual void Send(string message)
        {
            _wrapped.Send(message);
        }
    }

    // ConcreteDecorator — добавляет SMS
    public class SmsDecorator : NotifierDecorator
    {
        public SmsDecorator(INotifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            base.Send(message); // Сначала вызываем обёрнутый объект
            SendSms(message);   // Затем добавляем свою функциональность
        }

        private void SendSms(string message)
        {
            Console.WriteLine($"[SMS] Отправка: {message}");
        }
    }

    // ConcreteDecorator — добавляет Slack
    public class SlackDecorator : NotifierDecorator
    {
        public SlackDecorator(INotifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            base.Send(message);
            SendSlack(message);
        }

        private void SendSlack(string message)
        {
            Console.WriteLine($"[SLACK] Отправка: {message}");
        }
    }

    // ConcreteDecorator — добавляет Facebook
    public class FacebookDecorator : NotifierDecorator
    {
        public FacebookDecorator(INotifier notifier) : base(notifier) { }

        public override void Send(string message)
        {
            base.Send(message);
            SendFacebook(message);
        }

        private void SendFacebook(string message)
        {
            Console.WriteLine($"[FACEBOOK] Отправка: {message}");
        }
    }
    #endregion

    #region Пример 1: Кофейня (классический пример)
    // Component
    public interface ICoffee
    {
        string GetDescription();
        decimal GetCost();
    }

    // ConcreteComponent
    public class Espresso : ICoffee
    {
        public string GetDescription() => "Эспрессо";
        public decimal GetCost() => 2.00m;
    }

    public class Americano : ICoffee
    {
        public string GetDescription() => "Американо";
        public decimal GetCost() => 2.50m;
    }

    // Базовый декоратор
    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;
        protected CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee ?? throw new ArgumentNullException(nameof(coffee));
        }

        public virtual string GetDescription() => _coffee.GetDescription();
        public virtual decimal GetCost() => _coffee.GetCost();
    }

    // ConcreteDecorators — добавки
    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee) { }
        public override string GetDescription() => _coffee.GetDescription() + ", Молоко";
        public override decimal GetCost() => _coffee.GetCost() + 0.50m;
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee) { }
        public override string GetDescription() => _coffee.GetDescription() + ", Сахар";
        public override decimal GetCost() => _coffee.GetCost() + 0.20m;
    }

    public class WhippedCreamDecorator : CoffeeDecorator
    {
        public WhippedCreamDecorator(ICoffee coffee) : base(coffee) { }
        public override string GetDescription() => _coffee.GetDescription() + ", Взбитые сливки";
        public override decimal GetCost() => _coffee.GetCost() + 0.70m;
    }

    public class CaramelDecorator : CoffeeDecorator
    {
        public CaramelDecorator(ICoffee coffee) : base(coffee) { }
        public override string GetDescription() => _coffee.GetDescription() + ", Карамель";
        public override decimal GetCost() => _coffee.GetCost() + 0.60m;
    }
    #endregion

    #region Пример 2: Текстовые процессоры
    // Component
    public interface ITextProcessor
    {
        string Process(string text);
    }

    // ConcreteComponent
    public class PlainTextProcessor : ITextProcessor
    {
        public string Process(string text) => text;
    }

    // Базовый декоратор
    public abstract class TextDecorator : ITextProcessor
    {
        protected ITextProcessor _processor;
        protected TextDecorator(ITextProcessor processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public virtual string Process(string text) => _processor.Process(text);
    }

    // ConcreteDecorators
    public class UpperCaseDecorator : TextDecorator
    {
        public UpperCaseDecorator(ITextProcessor processor) : base(processor) { }
        public override string Process(string text) => base.Process(text).ToUpper();
    }

    public class TrimDecorator : TextDecorator
    {
        public TrimDecorator(ITextProcessor processor) : base(processor) { }
        public override string Process(string text) => base.Process(text).Trim();
    }

    public class HtmlEncodeDecorator : TextDecorator
    {
        public HtmlEncodeDecorator(ITextProcessor processor) : base(processor) { }
        public override string Process(string text)
        {
            string processed = base.Process(text);
            return processed
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;");
        }
    }

    public class PrefixDecorator : TextDecorator
    {
        private readonly string _prefix;
        public PrefixDecorator(ITextProcessor processor, string prefix) : base(processor)
        {
            _prefix = prefix ?? "";
        }
        public override string Process(string text) => _prefix + base.Process(text);
    }

    public class SuffixDecorator : TextDecorator
    {
        private readonly string _suffix;
        public SuffixDecorator(ITextProcessor processor, string suffix) : base(processor)
        {
            _suffix = suffix ?? "";
        }
        public override string Process(string text) => base.Process(text) + _suffix;
    }
    #endregion

    #region Пример 3: Потоки данных (как в .NET Stream)
    // Component
    public interface IDataStream
    {
        byte[] Read();
        void Write(byte[] data);
    }

    // ConcreteComponent
    public class FileDataStream : IDataStream
    {
        private byte[] _data;
        public FileDataStream()
        {
            _data = Encoding.UTF8.GetBytes("Original file data");
        }

        public byte[] Read()
        {
            Console.WriteLine("[FileStream] Чтение данных");
            return _data;
        }

        public void Write(byte[] data)
        {
            Console.WriteLine("[FileStream] Запись данных");
            _data = data;
        }
    }

    // Базовый декоратор
    public abstract class DataStreamDecorator : IDataStream
    {
        protected IDataStream _stream;
        protected DataStreamDecorator(IDataStream stream)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public virtual byte[] Read() => _stream.Read();
        public virtual void Write(byte[] data) => _stream.Write(data);
    }

    // ConcreteDecorators
    public class CompressionDecorator : DataStreamDecorator
    {
        public CompressionDecorator(IDataStream stream) : base(stream) { }

        public override byte[] Read()
        {
            byte[] data = base.Read();
            Console.WriteLine("[Compression] Распаковка данных");
            // Имитация распаковки
            return data;
        }

        public override void Write(byte[] data)
        {
            Console.WriteLine("[Compression] Сжатие данных");
            // Имитация сжатия
            base.Write(data);
        }
    }

    public class EncryptionDecorator : DataStreamDecorator
    {
        public EncryptionDecorator(IDataStream stream) : base(stream) { }

        public override byte[] Read()
        {
            byte[] data = base.Read();
            Console.WriteLine("[Encryption] Расшифровка данных");
            // Имитация расшифровки
            return data;
        }

        public override void Write(byte[] data)
        {
            Console.WriteLine("[Encryption] Шифрование данных");
            // Имитация шифрования
            base.Write(data);
        }
    }

    public class BufferingDecorator : DataStreamDecorator
    {
        public BufferingDecorator(IDataStream stream) : base(stream) { }

        public override byte[] Read()
        {
            Console.WriteLine("[Buffer] Чтение из буфера");
            return base.Read();
        }

        public override void Write(byte[] data)
        {
            Console.WriteLine("[Buffer] Запись в буфер");
            base.Write(data);
        }
    }
    #endregion

    #region Best Practices
    /*
     * Рекомендации:
     * - Декораторы и оборачиваемые объекты должны иметь общий интерфейс
     * - Декоратор должен делегировать основную работу обёрнутому объекту
     * - Порядок оборачивания может быть важен (например, сжатие → шифрование или наоборот)
     * - Используйте композицию вместо наследования для добавления функциональности
     * - Рассмотрите Fluent API для удобного создания цепочки декораторов
     *
     * Типичные ошибки:
     * - Слишком глубокая вложенность декораторов (трудно отлаживать)
     * - Нарушение принципа Liskov Substitution (декоратор должен быть взаимозаменяем с компонентом)
     * - Забывать вызывать base.Method() в декораторах
     * - Смешивание логики декорирования с бизнес-логикой
     */
    #endregion

    #region Демонстрация
    public static class DecoratorDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Демонстрация паттерна Decorator ===\n");
            DemoNotifications();
            Console.WriteLine();
            DemoCoffeeShop();
            Console.WriteLine();
            DemoTextProcessing();
            Console.WriteLine();
            DemoDataStreams();
        }

        private static void DemoNotifications()
        {
            Console.WriteLine("--- Уведомления (комбинации каналов) ---");

            // Базовое уведомление — только Email
            INotifier notifier1 = new EmailNotifier();
            notifier1.Send("Базовое уведомление");
            Console.WriteLine();

            // Email + SMS
            INotifier notifier2 = new SmsDecorator(new EmailNotifier());
            notifier2.Send("Важное уведомление");
            Console.WriteLine();

            // Email + SMS + Slack
            INotifier notifier3 = new SlackDecorator(
                new SmsDecorator(
                    new EmailNotifier()));
            notifier3.Send("Критическое уведомление");
            Console.WriteLine();

            // Email + SMS + Slack + Facebook
            INotifier notifier4 = new FacebookDecorator(
                new SlackDecorator(
                    new SmsDecorator(
                        new EmailNotifier())));
            notifier4.Send("Максимальное распространение");
        }

        private static void DemoCoffeeShop()
        {
            Console.WriteLine("--- Кофейня (добавки к напиткам) ---");

            // Простой эспрессо
            ICoffee coffee1 = new Espresso();
            Console.WriteLine($"{coffee1.GetDescription()}: {coffee1.GetCost():C}");

            // Эспрессо с молоком
            ICoffee coffee2 = new MilkDecorator(new Espresso());
            Console.WriteLine($"{coffee2.GetDescription()}: {coffee2.GetCost():C}");

            // Американо с молоком, сахаром и взбитыми сливками
            ICoffee coffee3 = new WhippedCreamDecorator(
                new SugarDecorator(
                    new MilkDecorator(
                        new Americano())));
            Console.WriteLine($"{coffee3.GetDescription()}: {coffee3.GetCost():C}");

            // Эспрессо с двойным молоком и карамелью
            ICoffee coffee4 = new CaramelDecorator(
                new MilkDecorator(
                    new MilkDecorator(
                        new Espresso())));
            Console.WriteLine($"{coffee4.GetDescription()}: {coffee4.GetCost():C}");
        }

        private static void DemoTextProcessing()
        {
            Console.WriteLine("--- Обработка текста (цепочка преобразований) ---");

            string input = "  <Hello World>  ";

            // Простая обработка
            ITextProcessor proc1 = new PlainTextProcessor();
            Console.WriteLine($"Без обработки: '{proc1.Process(input)}'");

            // Trim
            ITextProcessor proc2 = new TrimDecorator(new PlainTextProcessor());
            Console.WriteLine($"С Trim: '{proc2.Process(input)}'");

            // Trim + UpperCase
            ITextProcessor proc3 = new UpperCaseDecorator(
                new TrimDecorator(
                    new PlainTextProcessor()));
            Console.WriteLine($"Trim + Upper: '{proc3.Process(input)}'");

            // Trim + HtmlEncode + Prefix + Suffix
            ITextProcessor proc4 = new SuffixDecorator(
                new PrefixDecorator(
                    new HtmlEncodeDecorator(
                        new TrimDecorator(
                            new PlainTextProcessor())),
                    ">>> "),
                " <<<");
            Console.WriteLine($"Полная цепочка: '{proc4.Process(input)}'");
        }

        private static void DemoDataStreams()
        {
            Console.WriteLine("--- Потоки данных (вложенные декораторы) ---");

            // Простой поток
            IDataStream stream1 = new FileDataStream();
            stream1.Write(Encoding.UTF8.GetBytes("Test data"));
            byte[] data1 = stream1.Read();
            Console.WriteLine();

            // С буферизацией
            IDataStream stream2 = new BufferingDecorator(new FileDataStream());
            stream2.Write(Encoding.UTF8.GetBytes("Buffered data"));
            byte[] data2 = stream2.Read();
            Console.WriteLine();

            // С шифрованием и буферизацией
            IDataStream stream3 = new EncryptionDecorator(
                new BufferingDecorator(
                    new FileDataStream()));
            stream3.Write(Encoding.UTF8.GetBytes("Encrypted data"));
            byte[] data3 = stream3.Read();
            Console.WriteLine();

            // Полная цепочка: сжатие → шифрование → буферизация
            IDataStream stream4 = new CompressionDecorator(
                new EncryptionDecorator(
                    new BufferingDecorator(
                        new FileDataStream())));
            stream4.Write(Encoding.UTF8.GetBytes("Complex data"));
            byte[] data4 = stream4.Read();
        }
    }
    #endregion

    #region Задачи (без ответов)
    /*
     * ЗАДАЧИ ПО ПАТТЕРНУ DECORATOR
     * =============================
     * Реализуйте интерфейсы и классы по требованиям. Ответы не включать.
     */

    // Задача 1 (Базовая): Декораторы для пиццы
    // ----------------------------------------
    // Требования:
    // - Component: IPizza { string GetDescription(); decimal GetPrice(); }
    // - ConcreteComponents: MargheritaPizza, PepperoniPizza
    // - Базовый декоратор: PizzaDecorator
    // - ConcreteDecorators: CheeseDecorator, MushroomDecorator, OliveDecorator, BaconDecorator
    // - Каждый декоратор добавляет описание и цену ингредиента
    // - Demo: TestPizzaDecorators() — создайте пиццы с разными комбинациями добавок
    // TODO: объявите интерфейсы, классы и демо

    // Задача 2 (Средняя): Логирование с разными уровнями детализации
    // -------------------------------------------------------------
    // Требования:
    // - Component: ILogger { void Log(string message, LogLevel level); }
    // - ConcreteComponent: ConsoleLogger (простой вывод в консоль)
    // - Базовый декоратор: LoggerDecorator
    // - ConcreteDecorators:
    //   * TimestampDecorator — добавляет временную метку
    //   * ThreadIdDecorator — добавляет ID потока
    //   * ColorDecorator — раскрашивает вывод по уровню (имитация цветов строками)
    //   * FileDecorator — дублирует вывод в файл
    // - LogLevel (enum): Debug, Info, Warning, Error
    // - Demo: TestLoggerDecorators() — комбинируйте декораторы и логируйте сообщения
    // TODO: объявите модели и реализации

    // Задача 3 (Средняя+): HTTP-клиент с middleware
    // -------------------------------------------
    // Требования:
    // - Component: IHttpClient { HttpResponse Send(HttpRequest request); }
    // - ConcreteComponent: BasicHttpClient (имитация отправки запроса)
    // - Модели: HttpRequest { string Url; string Method; Dictionary<string,string> Headers; string Body; }
    //          HttpResponse { int StatusCode; string Body; }
    // - Базовый декоратор: HttpClientDecorator
    // - ConcreteDecorators:
    //   * LoggingDecorator — логирует запросы и ответы
    //   * RetryDecorator — повторяет запрос при ошибке (N попыток)
    //   * CachingDecorator — кеширует GET-запросы
    //   * AuthDecorator — добавляет заголовок авторизации
    // - Demo: TestHttpDecorators() — постройте цепочку middleware и отправьте запросы
    // TODO: объявите модели, интерфейсы и декораторы

    // Задача 4 (Продвинутая): UI компоненты с границами и прокруткой
    // ------------------------------------------------------------
    // Сценарий: текстовые UI-компоненты (для консольного приложения).
    // Требования:
    // - Component: IComponent { void Render(); int GetWidth(); int GetHeight(); }
    // - ConcreteComponents: TextBox, Label, Panel
    // - Базовый декоратор: ComponentDecorator
    // - ConcreteDecorators:
    //   * BorderDecorator — добавляет рамку (увеличивает размер на 2 по каждому измерению)
    //   * ScrollDecorator — добавляет скролл (имитация)
    //   * PaddingDecorator — добавляет отступы
    //   * BackgroundDecorator — добавляет фон (имитация цветом)
    // - Метод Render() выводит компонент в консоль в виде ASCII-графики
    // - Demo: TestUiDecorators() — создайте компоненты с разными декораторами
    // TODO: объявите модели и реализации

    // Задача 5 (Интеграционная): Конвейер обработки изображений
    // --------------------------------------------------------
    // Сценарий: обработка изображений через цепочку фильтров/операций.
    // Требования:
    // - Component: IImageProcessor { ImageData Process(ImageData image); }
    // - ConcreteComponent: IdentityProcessor (возвращает изображение без изменений)
    // - ImageData: класс с данными изображения (Width, Height, Pixels: byte[], Metadata: Dictionary)
    // - Базовый декоратор: ImageProcessorDecorator
    // - ConcreteDecorators:
    //   * ResizeDecorator(int width, int height) — изменяет размер (имитация)
    //   * GrayscaleDecorator — преобразует в оттенки серого (имитация)
    //   * BlurDecorator(int radius) — размывает (имитация)
    //   * WatermarkDecorator(string text) — добавляет водяной знак (имитация)
    //   * MetadataDecorator — добавляет метаданные об обработке
    // - Каждый декоратор обновляет Metadata с информацией о применённой операции
    // - Demo: TestImagePipeline() — создайте конвейер обработки и обработайте изображение
    // TODO: объявите модели, интерфейсы и декораторы

    #endregion
}
