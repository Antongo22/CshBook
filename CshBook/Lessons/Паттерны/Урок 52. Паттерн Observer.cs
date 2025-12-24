using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_5
{
    #region Теория
    /*
     * Паттерн Observer (Наблюдатель)
     * ===============================
     *
     * Наблюдатель — поведенческий паттерн, при котором объекты-издатели (Subject)
     * оповещают объекты-подписчики (Observers) об изменениях своего состояния.
     * Подписчики сами подписываются на события издателя и получают уведомления.
     *
     * Задачи, которые решает:
     * - Реакция многих объектов на изменение состояния другого объекта
     * - Ослабление связности: издатель не знает конкретных типов подписчиков
     * - Динамическое добавление/удаление подписчиков во время выполнения
     *
     * Состав:
     * - Subject (издатель): хранит список подписчиков, предоставляет методы Subscribe/Unsubscribe,
     *   оповещает всех при изменении
     * - Observer (подписчик): интерфейс с методом Update(...)
     *
     * В C# есть несколько идиоматических способов реализовать Observer:
     * 1) Классическая реализация (интерфейсы ISubject/IObserver) — обучающая, переносимая
     * 2) События и делегаты (event) — идиоматический способ в .NET
     * 3) IObservable<T>/IObserver<T> — реактивный контракт, совместим с Rx (System.Reactive)
     *
     * Плюсы:
     * - Ослабленная связность
     * - Динамические подписки
     * - Расширяемость
     *
     * Минусы:
     * - Возможны утечки памяти при некорректной отписке
     * - Трудно отладить порядок уведомлений
     * - Риск каскадных событий
     */
    #endregion

    #region Классическая реализация (Subject/Observer)
    public interface IObserver<T>
    {
        void Update(T data);
    }

    public interface ISubject<T>
    {
        void Subscribe(IObserver<T> observer);
        void Unsubscribe(IObserver<T> observer);
        void Notify(T data);
    }

    // Издатель: Тикер акций
    public class StockTicker : ISubject<StockPrice>
    {
        private readonly List<IObserver<StockPrice>> _observers = new();

        public void Subscribe(IObserver<StockPrice> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));
            if (!_observers.Contains(observer)) _observers.Add(observer);
        }

        public void Unsubscribe(IObserver<StockPrice> observer)
        {
            if (observer == null) return;
            _observers.Remove(observer);
        }

        public void Notify(StockPrice data)
        {
            foreach (var obs in _observers.ToList())
            {
                obs.Update(data);
            }
        }

        // Изменение цены (имитация внешнего события)
        public void SetPrice(StockPrice price)
        {
            Notify(price);
        }
    }

    public record StockPrice(string Ticker, decimal Price, DateTime TimeUtc);

    // Подписчик: оповещение при достижении целевой цены
    public class TargetAlertObserver : IObserver<StockPrice>
    {
        private readonly string _ticker;
        private readonly decimal _target;
        public TargetAlertObserver(string ticker, decimal target)
        {
            _ticker = ticker;
            _target = target;
        }
        public void Update(StockPrice data)
        {
            if (data.Ticker == _ticker && data.Price >= _target)
            {
                Console.WriteLine($"[ALERT] {_ticker} достигла цели {_target}, сейчас {data.Price}");
            }
        }
    }

    // Подписчик: логгер цен
    public class PriceLoggerObserver : IObserver<StockPrice>
    {
        public void Update(StockPrice data)
        {
            Console.WriteLine($"[LOG] {data.TimeUtc:HH:mm:ss} {data.Ticker} = {data.Price}");
        }
    }
    #endregion

    #region Реализация на событиях (event)
    // Издатель: Метеостанция на событиях
    public class WeatherStation
    {
        // Идиоматический .NET-подход — события с делегатами
        public event Action<WeatherReading> OnReading;

        public void Push(WeatherReading reading)
        {
            OnReading?.Invoke(reading);
        }
    }

    public record WeatherReading(double TemperatureC, int Humidity, double PressureHpa, DateTime TimeUtc);

    // Подписчик: дисплей текущих показаний
    public class CurrentConditionsDisplay
    {
        public void Subscribe(WeatherStation station)
        {
            station.OnReading += Handle;
        }
        public void Unsubscribe(WeatherStation station)
        {
            station.OnReading -= Handle;
        }
        private void Handle(WeatherReading r)
        {
            Console.WriteLine($"Темп: {r.TemperatureC:F1}°C, Влажн: {r.Humidity}%, Давл: {r.PressureHpa:F0} hPa");
        }
    }

    // Подписчик: предупреждение о жаре
    public class HeatAlert
    {
        private readonly double _threshold;
        public HeatAlert(double threshold) { _threshold = threshold; }
        public void Subscribe(WeatherStation station) => station.OnReading += Handle;
        public void Unsubscribe(WeatherStation station) => station.OnReading -= Handle;
        private void Handle(WeatherReading r)
        {
            if (r.TemperatureC >= _threshold)
                Console.WriteLine($"[HEAT ALERT] {r.TemperatureC:F1}°C >= {_threshold:F1}°C");
        }
    }
    #endregion

    #region IObservable<T>/IObserver<T> (концептуально)
    /*
     * В стандартной библиотеке есть интерфейсы IObservable<T>/IObserver<T>.
     * Они определяют контракт подписки с IDisposable на отписку.
     * Реактивные расширения (System.Reactive) предоставляют богатую экосистему операторов.
     * В этом уроке ограничимся упоминанием концепции без реализации Rx.
     */
    #endregion

    #region Best Practices
    /*
     * Рекомендации:
     * - Отписывайтесь от событий (иначе утечки памяти)
     * - Защищайте Notify от исключений подписчиков (try/catch вокруг вызовов)
     * - По возможности отправляйте иммутабельные данные (record/struct)
     * - Документируйте гарантии (порядок уведомлений, поток выполнения)
     * - Рассмотрите слабые ссылки (WeakReference) для подписчиков при длительном жизненном цикле издателя
     */
    #endregion

    #region Демонстрация
    public static class ObserverDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("=== Демонстрация паттерна Observer ===\n");
            DemoClassicObserver();
            Console.WriteLine();
            DemoEventBasedObserver();
        }

        private static void DemoClassicObserver()
        {
            Console.WriteLine("--- Классическая реализация ---");
            var ticker = new StockTicker();
            var logger = new PriceLoggerObserver();
            var alert = new TargetAlertObserver("MSFT", 350m);

            ticker.Subscribe(logger);
            ticker.Subscribe(alert);

            ticker.SetPrice(new StockPrice("MSFT", 345.10m, DateTime.UtcNow));
            ticker.SetPrice(new StockPrice("AAPL", 192.40m, DateTime.UtcNow));
            ticker.SetPrice(new StockPrice("MSFT", 351.00m, DateTime.UtcNow));

            ticker.Unsubscribe(logger);
            ticker.SetPrice(new StockPrice("MSFT", 360.00m, DateTime.UtcNow));
        }

        private static void DemoEventBasedObserver()
        {
            Console.WriteLine("--- На событиях (event) ---");
            var station = new WeatherStation();
            var display = new CurrentConditionsDisplay();
            var heat = new HeatAlert(28.0);

            display.Subscribe(station);
            heat.Subscribe(station);

            station.Push(new WeatherReading(22.5, 60, 1012.3, DateTime.UtcNow));
            station.Push(new WeatherReading(29.2, 55, 1009.8, DateTime.UtcNow));

            display.Unsubscribe(station);
            station.Push(new WeatherReading(31.0, 50, 1008.1, DateTime.UtcNow));
        }
    }
    #endregion

    #region Задачи (без ответов)
    /*
     * ЗАДАЧИ ПО ПАТТЕРНУ OBSERVER
     * ============================
     * Реализуйте интерфейсы и классы по требованиям ниже. Ответы/готовые решения не включать.
     */

    // Задача 1 (Базовая): Подписки на новости
    // ---------------------------------------
    // Требования:
    // - Subject: NewsFeed (Subscribe/Unsubscribe/Notify)
    // - Data: NewsItem { string Title; string Category; DateTime TimeUtc }
    // - Observers: CategoryFilterObserver (показывает только нужную категорию), ConsoleNewsObserver
    // - Demo: TestNewsFeed() — продемонстрировать подписки и отписки
    // TODO: объявите интерфейсы/классы и демонстрацию

    // Задача 2 (Средняя): Система событий UI без WinForms/WPF
    // -------------------------------------------------------
    // Требования:
    // - Subject: UiButton (события Click, DoubleClick, LongPress) — можно как отдельные Subject-ы или event-ы
    // - Observers: Logger, Analytics (считает клики и среднее время между кликами)
    // - Demo: TestUiEvents() — имитируйте нажатия и покажите статистику
    // Примечание: используйте Action и события или классический Observer
    // TODO: объявите минимальные модели и заглушки observers

    // Задача 3 (Средняя+): Шина событий (Event Bus)
    // --------------------------------------------
    // Требования:
    // - Реализовать простой EventBus с типизированными событиями (Publish<T>, Subscribe<T>, Unsubscribe<T>)
    // - Подписчики получают события своего типа
    // - Гарантировать потокобезопасность
    // - Demo: TestEventBus() — несколько типов событий, несколько подписчиков
    // TODO: объявите каркас EventBus и интерфейсы

    // Задача 4 (Продвинутая): Свечи и индикаторы на бирже
    // ---------------------------------------------------
    // Сценарий: приходит поток свечей (Candle). Индикаторы (SMA, EMA, RSI — можно упростить) подписаны на поток и
    // публикуют свои значения для других подписчиков (цепочки наблюдателей).
    // Требования:
    // - Subject: CandleFeed
    // - Observers: SmaIndicator(N), EmaIndicator(N) — подписаны на свечи, публикуют IndicatorValue
    // - Другие Observers: SignalPrinter — подписан на индикаторы, печатает сигналы
    // - Demo: TestIndicatorsPipeline()
    // TODO: объявите модели Candle, IndicatorValue и каркасы индикаторов/подписок

    // Задача 5 (Интеграционная): Система уведомлений с отпиской по таймауту
    // --------------------------------------------------------------------
    // Требования:
    // - Subject: NotificationHub (типизированные уведомления: Info, Warning, Error)
    // - Подписчики автоматически отписываются через заданный TTL (Disposable-токены или таймеры)
    // - Потокобезопасность и отсутствие утечек
    // - Demo: TestNotificationHub()
    // TODO: объявите модели уведомлений и каркас NotificationHub

    #endregion
}
