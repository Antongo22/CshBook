using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_5
{
    #region Теория
    /*
     * Паттерн Factory Method (Фабричный метод)
     * =========================================
     *
     * Фабричный метод — порождающий паттерн проектирования, который определяет общий интерфейс
     * для создания объектов в суперклассе, позволяя подклассам изменять тип создаваемых объектов.
     *
     * Суть паттерна:
     * Вместо прямого вызова конструктора (new Product()) создаём объект через метод-фабрику,
     * который может быть переопределён в подклассах. Таким образом, логика создания объектов
     * делегируется подклассам.
     *
     * Структура:
     * - Product (интерфейс или абстрактный класс) — общий тип создаваемых объектов
     * - ConcreteProduct — конкретная реализация Product
     * - Creator (абстрактный класс) — объявляет фабричный метод CreateProduct()
     * - ConcreteCreator — переопределяет CreateProduct() для создания ConcreteProduct
     *
     * Когда применять:
     * - Когда заранее неизвестен тип создаваемых объектов
     * - Когда хотите делегировать логику создания объектов подклассам
     * - Когда хотите локализовать логику создания в одном месте
     * - Когда нужна гибкость в выборе типа создаваемого объекта
     *
     * Отличие от простой фабрики:
     * - Простая фабрика — это метод/класс, который создаёт объекты по параметру (обычно switch/if)
     * - Фабричный метод — шаблон с наследованием, где подклассы переопределяют создание
     *
     * Отличие от абстрактной фабрики:
     * - Фабричный метод создаёт один тип объектов
     * - Абстрактная фабрика создаёт семейства связанных объектов
     *
     * Плюсы:
     * - Избавляет от привязки к конкретным классам продуктов
     * - Следует принципу открытости/закрытости (OCP)
     * - Упрощает добавление новых типов продуктов
     * - Локализует код создания объектов
     *
     * Минусы:
     * - Увеличение количества классов
     * - Усложнение кода при простых сценариях
     */
    #endregion

    #region Базовая структура
    // Product — интерфейс продукта
    public interface ITransport
    {
        void Deliver(string destination, string cargo);
    }

    // ConcreteProduct — конкретные реализации
    public class Truck : ITransport
    {
        public void Deliver(string destination, string cargo)
        {
            Console.WriteLine($"[TRUCK] Доставка '{cargo}' в {destination} по дороге");
        }
    }

    public class Ship : ITransport
    {
        public void Deliver(string destination, string cargo)
        {
            Console.WriteLine($"[SHIP] Доставка '{cargo}' в {destination} по морю");
        }
    }

    public class Airplane : ITransport
    {
        public void Deliver(string destination, string cargo)
        {
            Console.WriteLine($"[AIRPLANE] Доставка '{cargo}' в {destination} по воздуху");
        }
    }

    // Creator — абстрактный создатель (объявляет фабричный метод)
    public abstract class Logistics
    {
        // Фабричный метод (переопределяется в подклассах)
        protected abstract ITransport CreateTransport();

        // Бизнес-логика, использующая фабричный метод
        public void PlanDelivery(string destination, string cargo)
        {
            ITransport transport = CreateTransport();
            Console.WriteLine($"Планирование доставки...");
            transport.Deliver(destination, cargo);
        }
    }

    // ConcreteCreator — конкретные создатели
    public class RoadLogistics : Logistics
    {
        protected override ITransport CreateTransport() => new Truck();
    }

    public class SeaLogistics : Logistics
    {
        protected override ITransport CreateTransport() => new Ship();
    }

    public class AirLogistics : Logistics
    {
        protected override ITransport CreateTransport() => new Airplane();
    }
    #endregion

    #region Пример 1: Кроссплатформенный UI
    // Product
    public interface IButton
    {
        void Render();
        void OnClick();
    }

    // ConcreteProducts
    public class WindowsButton : IButton
    {
        public void Render() => Console.WriteLine("[Windows] Отрисовка кнопки в стиле Windows");
        public void OnClick() => Console.WriteLine("[Windows] Обработка клика Windows-стиль");
    }

    public class MacButton : IButton
    {
        public void Render() => Console.WriteLine("[Mac] Отрисовка кнопки в стиле macOS");
        public void OnClick() => Console.WriteLine("[Mac] Обработка клика macOS-стиль");
    }

    public class LinuxButton : IButton
    {
        public void Render() => Console.WriteLine("[Linux] Отрисовка кнопки в стиле Linux");
        public void OnClick() => Console.WriteLine("[Linux] Обработка клика Linux-стиль");
    }

    // Creator
    public abstract class Dialog
    {
        protected abstract IButton CreateButton();

        public void Render()
        {
            IButton button = CreateButton();
            button.Render();
            button.OnClick();
        }
    }

    // ConcreteCreators
    public class WindowsDialog : Dialog
    {
        protected override IButton CreateButton() => new WindowsButton();
    }

    public class MacDialog : Dialog
    {
        protected override IButton CreateButton() => new MacButton();
    }

    public class LinuxDialog : Dialog
    {
        protected override IButton CreateButton() => new LinuxButton();
    }
    #endregion

    #region Пример 2: Парсеры документов
    // Product
    public interface IDocumentParser
    {
        string Parse(byte[] data);
    }

    // ConcreteProducts
    public class PdfParser : IDocumentParser
    {
        public string Parse(byte[] data)
        {
            // Имитация парсинга PDF
            return $"[PDF Parser] Извлечено {data.Length} байт из PDF";
        }
    }

    public class WordParser : IDocumentParser
    {
        public string Parse(byte[] data)
        {
            // Имитация парсинга Word
            return $"[Word Parser] Извлечено {data.Length} байт из Word";
        }
    }

    public class ExcelParser : IDocumentParser
    {
        public string Parse(byte[] data)
        {
            // Имитация парсинга Excel
            return $"[Excel Parser] Извлечено {data.Length} байт из Excel";
        }
    }

    // Creator
    public abstract class DocumentProcessor
    {
        protected abstract IDocumentParser CreateParser();

        public string ProcessDocument(byte[] data)
        {
            IDocumentParser parser = CreateParser();
            string content = parser.Parse(data);
            Console.WriteLine($"Обработка документа: {content}");
            return content;
        }
    }

    // ConcreteCreators
    public class PdfProcessor : DocumentProcessor
    {
        protected override IDocumentParser CreateParser() => new PdfParser();
    }

    public class WordProcessor : DocumentProcessor
    {
        protected override IDocumentParser CreateParser() => new WordParser();
    }

    public class ExcelProcessor : DocumentProcessor
    {
        protected override IDocumentParser CreateParser() => new ExcelParser();
    }
    #endregion

    #region Пример 3: Простая фабрика vs Фабричный метод
    /*
     * Простая фабрика (Simple Factory) — это не паттерн GoF, а идиома.
     * Это статический метод или класс, который создаёт объекты по параметру.
     */

    // Простая фабрика (для сравнения)
    public static class SimpleTransportFactory
    {
        public static ITransport Create(string type)
        {
            return type.ToLower() switch
            {
                "truck" => new Truck(),
                "ship" => new Ship(),
                "airplane" => new Airplane(),
                _ => throw new ArgumentException($"Неизвестный тип транспорта: {type}")
            };
        }
    }

    /*
     * Простая фабрика проще, но менее гибкая:
     * - Все типы хардкодятся в одном месте
     * - Нарушается принцип открытости/закрытости (нужно менять фабрику при добавлении типов)
     * 
     * Фабричный метод более гибкий:
     * - Новые типы добавляются через новые подклассы
     * - Не нужно менять существующий код
     * - Следует принципу OCP
     */
    #endregion

    #region Пример 4: Логгеры с разными форматами
    // Product
    public interface ILogger
    {
        void Log(string message, string level);
    }

    // ConcreteProducts
    public class JsonLogger : ILogger
    {
        public void Log(string message, string level)
        {
            Console.WriteLine($"{{\"timestamp\":\"{DateTime.UtcNow:o}\",\"level\":\"{level}\",\"message\":\"{message}\"}}");
        }
    }

    public class PlainTextLogger : ILogger
    {
        public void Log(string message, string level)
        {
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{level}] {message}");
        }
    }

    public class XmlLogger : ILogger
    {
        public void Log(string message, string level)
        {
            Console.WriteLine($"<log time=\"{DateTime.UtcNow:o}\" level=\"{level}\">{message}</log>");
        }
    }

    // Creator
    public abstract class LoggingService
    {
        protected abstract ILogger CreateLogger();

        public void LogInfo(string message)
        {
            ILogger logger = CreateLogger();
            logger.Log(message, "INFO");
        }

        public void LogError(string message)
        {
            ILogger logger = CreateLogger();
            logger.Log(message, "ERROR");
        }
    }

    // ConcreteCreators
    public class JsonLoggingService : LoggingService
    {
        protected override ILogger CreateLogger() => new JsonLogger();
    }

    public class PlainTextLoggingService : LoggingService
    {
        protected override ILogger CreateLogger() => new PlainTextLogger();
    }

    public class XmlLoggingService : LoggingService
    {
        protected override ILogger CreateLogger() => new XmlLogger();
    }
    #endregion

    #region Best Practices
    /*
     * Рекомендации:
     * - Используйте фабричный метод, когда нужна гибкость в выборе типа создаваемого объекта
     * - Для простых случаев рассмотрите простую фабрику (проще и понятнее)
     * - Называйте фабричные методы понятно: CreateXxx(), BuildXxx(), MakeXxx()
     * - Возвращайте интерфейсы/абстрактные классы, а не конкретные типы
     * - Комбинируйте с другими паттернами (Strategy, Dependency Injection)
     *
     * Типичные ошибки:
     * - Излишнее усложнение простых сценариев
     * - Возвращение конкретных типов вместо интерфейсов
     * - Смешивание создания объектов с бизнес-логикой
     * - Забывать про принцип единственной ответственности
     */
    #endregion

    #region Демонстрация
    public static class FactoryMethodDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Демонстрация паттерна Factory Method ===\n");
            DemoLogistics();
            Console.WriteLine();
            DemoCrossPlatformUI();
            Console.WriteLine();
            DemoDocumentParsers();
            Console.WriteLine();
            DemoSimpleFactoryVsFactoryMethod();
            Console.WriteLine();
            DemoLoggers();
        }

        private static void DemoLogistics()
        {
            Console.WriteLine("--- Логистика (транспорт) ---");
            Logistics roadLogistics = new RoadLogistics();
            roadLogistics.PlanDelivery("Москва", "Мебель");

            Logistics seaLogistics = new SeaLogistics();
            seaLogistics.PlanDelivery("Владивосток", "Контейнеры");

            Logistics airLogistics = new AirLogistics();
            airLogistics.PlanDelivery("Лондон", "Документы");
        }

        private static void DemoCrossPlatformUI()
        {
            Console.WriteLine("--- Кроссплатформенный UI ---");
            Dialog windowsDialog = new WindowsDialog();
            windowsDialog.Render();

            Dialog macDialog = new MacDialog();
            macDialog.Render();

            Dialog linuxDialog = new LinuxDialog();
            linuxDialog.Render();
        }

        private static void DemoDocumentParsers()
        {
            Console.WriteLine("--- Парсеры документов ---");
            byte[] sampleData = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };

            DocumentProcessor pdfProc = new PdfProcessor();
            pdfProc.ProcessDocument(sampleData);

            DocumentProcessor wordProc = new WordProcessor();
            wordProc.ProcessDocument(sampleData);

            DocumentProcessor excelProc = new ExcelProcessor();
            excelProc.ProcessDocument(sampleData);
        }

        private static void DemoSimpleFactoryVsFactoryMethod()
        {
            Console.WriteLine("--- Простая фабрика (для сравнения) ---");
            ITransport truck = SimpleTransportFactory.Create("truck");
            truck.Deliver("Казань", "Товары");

            ITransport ship = SimpleTransportFactory.Create("ship");
            ship.Deliver("Сочи", "Нефть");

            Console.WriteLine("(Простая фабрика — хороша для простых случаев, но менее гибкая)");
        }

        private static void DemoLoggers()
        {
            Console.WriteLine("--- Логгеры с разными форматами ---");
            LoggingService jsonService = new JsonLoggingService();
            jsonService.LogInfo("Приложение запущено");
            jsonService.LogError("Ошибка подключения");

            Console.WriteLine();
            LoggingService plainTextService = new PlainTextLoggingService();
            plainTextService.LogInfo("Обработка запроса");

            Console.WriteLine();
            LoggingService xmlService = new XmlLoggingService();
            xmlService.LogError("Не удалось сохранить файл");
        }
    }
    #endregion

    #region Задачи (без ответов)
    /*
     * ЗАДАЧИ ПО ПАТТЕРНУ FACTORY METHOD
     * ==================================
     * Реализуйте интерфейсы и классы по требованиям. Ответы не включать.
     */

    // Задача 1 (Базовая): Фабрика уведомлений
    // ---------------------------------------
    // Требования:
    // - Product: INotification { void Send(string recipient, string message) }
    // - ConcreteProducts: EmailNotification, SmsNotification, PushNotification
    // - Creator: NotificationService { abstract CreateNotification(), SendNotification(...) }
    // - ConcreteCreators: EmailNotificationService, SmsNotificationService, PushNotificationService
    // - Demo: TestNotificationFactory()
    // TODO: объявите интерфейсы, классы и демо

    // Задача 2 (Средняя): Генераторы отчётов
    // --------------------------------------
    // Требования:
    // - Product: IReport { string Generate(ReportData data) }
    // - ConcreteProducts: PdfReport, ExcelReport, CsvReport, HtmlReport
    // - ReportData: класс с данными (Title, Columns: List<string>, Rows: List<List<object>>)
    // - Creator: ReportGenerator { abstract CreateReport(), string GenerateReport(ReportData data) }
    // - ConcreteCreators: PdfReportGenerator, ExcelReportGenerator, etc.
    // - Demo: TestReportGenerators() — создайте образец ReportData и сгенерируйте отчёты разных форматов
    // TODO: объявите модели, интерфейсы и реализации

    // Задача 3 (Средняя+): Экспортёры данных с конфигурацией
    // -----------------------------------------------------
    // Требования:
    // - Product: IDataExporter { byte[] Export(DataSet data, ExportConfig config) }
    // - ConcreteProducts: JsonExporter, XmlExporter, YamlExporter, CsvExporter
    // - ExportConfig: класс с настройками (Indent, IncludeSchema, DateFormat, etc.)
    // - Creator: DataExportService { abstract CreateExporter(), byte[] ExportData(...) }
    // - ConcreteCreators для каждого формата
    // - Добавить метод выбора экспортёра по строковому ключу (простая фабрика внутри)
    // - Demo: TestDataExporters()
    // TODO: объявите модели и каркасы классов

    // Задача 4 (Продвинутая): Игровые персонажи с вариациями
    // -----------------------------------------------------
    // Сценарий: создание игровых персонажей (Warrior, Mage, Archer).
    // У каждого типа есть варианты снаряжения: Light, Medium, Heavy (влияет на характеристики).
    // Требования:
    // - Product: ICharacter { string Name; int Health; int Attack; int Defense; void DisplayStats() }
    // - ConcreteProducts: LightWarrior, MediumWarrior, HeavyWarrior, LightMage, MediumMage, HeavyMage, ...
    // - Creator: CharacterFactory { abstract CreateCharacter(EquipmentLevel level), ... }
    // - ConcreteCreators: WarriorFactory, MageFactory, ArcherFactory
    // - EquipmentLevel (enum): Light, Medium, Heavy
    // - Demo: TestCharacterFactory() — создайте персонажей разных классов и снаряжения
    // TODO: объявите модели и фабрики

    // Задача 5 (Интеграционная): Плагинная система с динамической загрузкой
    // -------------------------------------------------------------------
    // Сценарий: система плагинов для обработки изображений.
    // Плагины загружаются по строковому идентификатору, могут регистрироваться динамически.
    // Требования:
    // - Product: IImageProcessor { byte[] Process(byte[] image, Dictionary<string,object> params) }
    // - ConcreteProducts: ResizeProcessor, FilterProcessor, WatermarkProcessor, CompressProcessor
    // - PluginRegistry: словарь фабричных функций (Func<IImageProcessor>) по строковым ключам
    // - Методы: RegisterPlugin(string key, Func<IImageProcessor> factory), CreateProcessor(string key)
    // - Creator: ImageProcessingService { использует PluginRegistry для создания процессоров }
    // - Demo: TestPluginSystem() — зарегистрируйте плагины и примените разные процессоры
    // Подсказка: используйте Dictionary<string, Func<IImageProcessor>> для хранения фабрик
    // TODO: объявите модели, PluginRegistry и демо

    #endregion
}
