using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Events
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о событиях (events) в C#:
     * 
     * - Что такое события и для чего они используются
     * - Объявление и использование событий
     * - Паттерн "Издатель-Подписчик" (Publisher-Subscriber)
     * - Стандартные делегаты для событий (EventHandler)
     * - Пользовательские аргументы событий
     * - События и многопоточность
     * - Статические события
     */

    /*
       Что такое события и для чего они используются
       ======================================
       
       Событие (event) — это механизм для классов, который позволяет им 
       уведомлять другие классы, когда происходит что-то интересное.
       
       События в C# основаны на делегатах, но предоставляют дополнительный уровень инкапсуляции.
       Ключевое отличие от делегатов: события могут быть вызваны (triggered) только 
       внутри класса, который их объявляет, но подписываться на них и отписываться 
       от них можно извне.
       
       Основные применения событий:
       
       1. Пользовательские интерфейсы: реагирование на действия пользователя
       2. Асинхронное программирование: уведомление о завершении операций
       3. Обработка состояний: оповещение об изменении состояния объекта
       4. Межкомпонентное взаимодействие: слабосвязанная коммуникация между компонентами
       
       События являются ключевым элементом архитектуры, управляемой событиями 
       (event-driven architecture), и паттерна "Издатель-Подписчик".
    */

    /*
       Объявление и использование событий
       ============================
       
       Объявление события:
       
       // Объявление делегата для события (если не используется стандартный)
       public delegate void MessageReceivedEventHandler(object sender, string message);
       
       // Объявление события на основе делегата
       public event MessageReceivedEventHandler MessageReceived;
       
       Вызов события:
       
       // Проверка на null перед вызовом (предотвращение NullReferenceException)
       protected virtual void OnMessageReceived(string message)
       {
           MessageReceived?.Invoke(this, message);
       }
       
       Подписка на событие:
       
       // Создание обработчика события
       void HandleMessageReceived(object sender, string message)
       {
           Console.WriteLine($"Получено сообщение: {message}");
       }
       
       // Подписка на событие
       messageService.MessageReceived += HandleMessageReceived;
       
       Отписка от события:
       
       // Отписка от события
       messageService.MessageReceived -= HandleMessageReceived;
       
       Важно отписываться от событий, когда объект-подписчик больше не нуждается
       в получении уведомлений, особенно если время жизни подписчика короче,
       чем у издателя, чтобы избежать утечек памяти.
    */

    /*
       Паттерн "Издатель-Подписчик" (Publisher-Subscriber)
       =========================================
       
       События в C# реализуют паттерн "Издатель-Подписчик":
       
       1. Издатель (Publisher) - класс, который определяет события и вызывает их
       2. Подписчик (Subscriber) - класс, который подписывается на события и реагирует на них
       
       Основные преимущества этого паттерна:
       
       1. Слабая связанность (loose coupling) - издатель ничего не знает о подписчиках
       2. Один ко многим - одно событие может иметь множество подписчиков
       3. Динамическая регистрация - подписчики могут подписываться и отписываться во время выполнения
       
       Типичная реализация:
       
       // Издатель
       public class Publisher
       {
           // Объявление события
           public event EventHandler<EventArgs> SomethingHappened;
           
           // Метод для вызова события
           protected virtual void OnSomethingHappened()
           {
               SomethingHappened?.Invoke(this, EventArgs.Empty);
           }
           
           // Метод, который может вызвать событие
           public void DoSomething()
           {
               // ... какая-то логика ...
               
               // Вызываем событие
               OnSomethingHappened();
           }
       }
       
       // Подписчик
       public class Subscriber
       {
           public void Subscribe(Publisher publisher)
           {
               // Подписка на событие
               publisher.SomethingHappened += HandleSomethingHappened;
           }
           
           public void Unsubscribe(Publisher publisher)
           {
               // Отписка от события
               publisher.SomethingHappened -= HandleSomethingHappened;
           }
           
           // Обработчик события
           private void HandleSomethingHappened(object sender, EventArgs e)
           {
               Console.WriteLine("Что-то произошло!");
           }
       }
    */

    /*
       Стандартные делегаты для событий (EventHandler)
       =====================================
       
       .NET предоставляет стандартные делегаты для событий:
       
       1. EventHandler - базовый делегат для событий без дополнительных данных:
          public delegate void EventHandler(object sender, EventArgs e);
       
       2. EventHandler<TEventArgs> - обобщенный делегат для событий с пользовательскими аргументами:
          public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
          где TEventArgs должен наследоваться от EventArgs
       
       Использование стандартных делегатов:
       
       // Событие без дополнительных данных
       public event EventHandler Started;
       
       // Событие с дополнительными данными
       public event EventHandler<ProgressEventArgs> ProgressChanged;
       
       Вызов:
       
       // Вызов события без дополнительных данных
       Started?.Invoke(this, EventArgs.Empty);
       
       // Вызов события с дополнительными данными
       ProgressChanged?.Invoke(this, new ProgressEventArgs(50));
    */

    /*
       Пользовательские аргументы событий
       ===========================
       
       Для передачи дополнительных данных с событием создаются пользовательские 
       классы аргументов, наследующиеся от EventArgs:
       
       public class MessageEventArgs : EventArgs
       {
           public string Message { get; }
           public DateTime Timestamp { get; }
           
           public MessageEventArgs(string message)
           {
               Message = message;
               Timestamp = DateTime.Now;
           }
       }
       
       Использование:
       
       // Объявление события с пользовательскими аргументами
       public event EventHandler<MessageEventArgs> MessageReceived;
       
       // Вызов события
       protected virtual void OnMessageReceived(string message)
       {
           MessageReceived?.Invoke(this, new MessageEventArgs(message));
       }
       
       // Обработчик события
       private void HandleMessageReceived(object sender, MessageEventArgs e)
       {
           Console.WriteLine($"[{e.Timestamp}] {e.Message}");
       }
    */

    /*
       События и многопоточность
       ===================
       
       При работе с событиями в многопоточной среде необходимо учитывать:
       
       1. Thread Safety при вызове события:
          
          // Безопасный вызов события в многопоточной среде
          var handler = MessageReceived;
          handler?.Invoke(this, message);
          
          Сначала копируем ссылку на делегат события в локальную переменную,
          а затем вызываем его. Это предотвращает проблему, если кто-то отпишется
          от события между проверкой на null и вызовом.
       
       2. Синхронизация доступа при необходимости:
          
          // Использование блокировки для синхронизации
          private readonly object _eventLock = new object();
          
          protected virtual void OnMessageReceived(string message)
          {
              EventHandler<MessageEventArgs> handler;
              
              lock (_eventLock)
              {
                  handler = MessageReceived;
              }
              
              handler?.Invoke(this, new MessageEventArgs(message));
          }
       
       3. Учет контекста синхронизации для UI-приложений:
          
          В UI-приложениях события часто вызываются в фоновых потоках,
          а обработчики должны выполняться в UI-потоке. Для этого используется
          SynchronizationContext или специальные методы типа Control.BeginInvoke.
    */

    /*
       Статические события
       =============
       
       События могут быть статическими, что позволяет им существовать
       на уровне класса, а не конкретного экземпляра:
       
       public static event EventHandler<LogEventArgs> LogReceived;
       
       Использование статических событий полезно для глобальных уведомлений,
       но требует осторожности, так как подписчики остаются активными
       на протяжении всего времени работы приложения, если не отписаться явно.
       
       Пример:
       
       public static class Logger
       {
           public static event EventHandler<LogEventArgs> LogReceived;
           
           public static void Log(string message, LogLevel level)
           {
               Console.WriteLine($"[{level}] {message}");
               LogReceived?.Invoke(null, new LogEventArgs(message, level));
           }
       }
       
       // Использование
       Logger.LogReceived += HandleLogMessage;
       Logger.Log("Тестовое сообщение", LogLevel.Info);
    */

    /*
       Пользовательские аксессоры событий (Custom Event Accessors)
       ===========================================
       
       C# позволяет определять пользовательские аксессоры add и remove для событий,
       аналогично свойствам с get и set:
       
       private EventHandler _completed;
       
       public event EventHandler Completed
       {
           add
           {
               // Пользовательская логика при подписке
               Console.WriteLine("Кто-то подписался на событие Completed");
               _completed += value;
           }
           remove
           {
               // Пользовательская логика при отписке
               Console.WriteLine("Кто-то отписался от события Completed");
               _completed -= value;
           }
       }
       
       Это позволяет добавлять дополнительную логику при подписке и отписке,
       например, отслеживание подписчиков или логирование.
    */

    /*
       Рекомендации по использованию событий
       ==============================
       
       1. Следуйте соглашениям об именовании:
          - Имя события должно быть глаголом или фразой глагола (Changed, Closing, Clicked)
          - Имя метода вызова события начинается с On и соответствует имени события (OnChanged)
          - Тип аргументов события должен заканчиваться на EventArgs
       
       2. Всегда проверяйте события на null перед вызовом
       
       3. Создавайте protected virtual методы для вызова событий (позволяет переопределять логику вызова в производных классах)
       
       4. Правильно отписывайтесь от событий, чтобы избежать утечек памяти
       
       5. Делайте события thread-safe в многопоточных сценариях
       
       6. Не выполняйте долгих операций в обработчиках событий
       
       7. Используйте асинхронные обработчики событий для длительных операций
    */
    #endregion

    #region Примеры использования событий

    // Пример 1: Класс таймера с событиями
    public class SimpleTimer
    {
        // Событие, которое срабатывает при каждом "тике" таймера
        public event EventHandler Tick;
        
        // Событие с аргументами, которое срабатывает при завершении работы таймера
        public event EventHandler<TimerEventArgs> Completed;
        
        private int _interval;
        private int _ticks;
        
        public SimpleTimer(int interval, int ticks)
        {
            _interval = interval;
            _ticks = ticks;
        }
        
        public void Start()
        {
            Console.WriteLine($"Таймер запущен. Интервал: {_interval}мс, количество тиков: {_ticks}");
            
            for (int i = 0; i < _ticks; i++)
            {
                // Имитация ожидания
                System.Threading.Thread.Sleep(_interval);
                
                // Вызов события Tick
                OnTick();
            }
            
            // Вызов события Completed
            OnCompleted(DateTime.Now);
        }
        
        // Защищенный метод для вызова события Tick
        protected virtual void OnTick()
        {
            // Безопасный вызов события
            Tick?.Invoke(this, EventArgs.Empty);
        }
        
        // Защищенный метод для вызова события Completed
        protected virtual void OnCompleted(DateTime completionTime)
        {
            // Безопасный вызов события с аргументами
            Completed?.Invoke(this, new TimerEventArgs(completionTime, _ticks));
        }
    }
    
    // Пользовательский класс аргументов для события
    public class TimerEventArgs : EventArgs
    {
        public DateTime CompletionTime { get; }
        public int TotalTicks { get; }
        
        public TimerEventArgs(DateTime completionTime, int totalTicks)
        {
            CompletionTime = completionTime;
            TotalTicks = totalTicks;
        }
    }
    
    // Пример 2: Класс для мониторинга изменения значения
    public class ValueMonitor<T>
    {
        private T _value;
        
        // Событие, которое срабатывает при изменении значения
        public event EventHandler<ValueChangedEventArgs<T>> ValueChanged;
        
        public T Value
        {
            get { return _value; }
            set 
            { 
                // Проверяем, изменилось ли значение
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    // Сохраняем старое значение
                    T oldValue = _value;
                    
                    // Устанавливаем новое значение
                    _value = value;
                    
                    // Вызываем событие
                    OnValueChanged(oldValue, value);
                }
            }
        }
        
        public ValueMonitor(T initialValue)
        {
            _value = initialValue;
        }
        
        // Защищенный метод для вызова события ValueChanged
        protected virtual void OnValueChanged(T oldValue, T newValue)
        {
            // Вызываем событие с аргументами
            ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>(oldValue, newValue));
        }
    }
    
    // Пользовательский класс аргументов для события изменения значения
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T OldValue { get; }
        public T NewValue { get; }
        
        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
    
    // Пример 3: Система логирования со статическими событиями
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }
    
    public class LogEventArgs : EventArgs
    {
        public string Message { get; }
        public LogLevel Level { get; }
        public DateTime Timestamp { get; }
        
        public LogEventArgs(string message, LogLevel level)
        {
            Message = message;
            Level = level;
            Timestamp = DateTime.Now;
        }
    }
    
    public static class Logger
    {
        // Статическое событие для логирования
        public static event EventHandler<LogEventArgs> LogReceived;
        
        // Методы для различных уровней логирования
        public static void Debug(string message) => Log(message, LogLevel.Debug);
        public static void Info(string message) => Log(message, LogLevel.Info);
        public static void Warning(string message) => Log(message, LogLevel.Warning);
        public static void Error(string message) => Log(message, LogLevel.Error);
        
        // Общий метод логирования
        private static void Log(string message, LogLevel level)
        {
            Console.WriteLine($"[{level}] {message}");
            
            // Вызов события
            LogReceived?.Invoke(null, new LogEventArgs(message, level));
        }
    }
    
    // Пример 4: Простое приложение для демонстрации событий
    public class Button
    {
        public string Text { get; set; }
        
        // Событие, которое срабатывает при нажатии кнопки
        public event EventHandler Click;
        
        public Button(string text)
        {
            Text = text;
        }
        
        // Метод для симуляции нажатия кнопки
        public void PerformClick()
        {
            Console.WriteLine($"Кнопка '{Text}' нажата");
            
            // Вызов события Click
            OnClick();
        }
        
        // Защищенный метод для вызова события Click
        protected virtual void OnClick()
        {
            // Вызываем событие
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public class TextBox
    {
        private string _text;
        
        // Событие, которое срабатывает при изменении текста
        public event EventHandler<TextChangedEventArgs> TextChanged;
        
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    string oldValue = _text;
                    _text = value;
                    
                    // Вызов события TextChanged
                    OnTextChanged(oldValue, value);
                }
            }
        }
        
        public TextBox()
        {
            _text = string.Empty;
        }
        
        // Защищенный метод для вызова события TextChanged
        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
            // Вызываем событие с аргументами
            TextChanged?.Invoke(this, new TextChangedEventArgs(oldValue, newValue));
        }
    }
    
    // Пользовательский класс аргументов для события изменения текста
    public class TextChangedEventArgs : EventArgs
    {
        public string OldText { get; }
        public string NewText { get; }
        
        public TextChangedEventArgs(string oldText, string newText)
        {
            OldText = oldText;
            NewText = newText;
        }
    }
    #endregion

    internal class EventsLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по событиям в C# ====\n");

            Console.WriteLine("--- Пример 1: Работа с таймером ---");
            
            // Создаем экземпляр таймера
            SimpleTimer timer = new SimpleTimer(500, 5);
            
            // Подписываемся на события таймера
            timer.Tick += (sender, e) => {
                Console.WriteLine("Тик! " + DateTime.Now.ToString("HH:mm:ss.fff"));
            };
            
            timer.Completed += (sender, e) => {
                Console.WriteLine($"Таймер завершил работу в {e.CompletionTime:HH:mm:ss.fff}");
                Console.WriteLine($"Всего выполнено {e.TotalTicks} тиков");
            };
            
            // Запускаем таймер
            timer.Start();

            Console.WriteLine("\n--- Пример 2: Мониторинг изменения значения ---");
            
            // Создаем монитор значения
            ValueMonitor<int> temperatureMonitor = new ValueMonitor<int>(20);
            
            // Подписываемся на событие изменения значения
            temperatureMonitor.ValueChanged += (sender, e) => {
                Console.WriteLine($"Температура изменилась с {e.OldValue}°C на {e.NewValue}°C");
                
                if (e.NewValue > 30)
                {
                    Console.WriteLine("Внимание! Высокая температура!");
                }
            };
            
            // Изменяем значение
            temperatureMonitor.Value = 25;
            temperatureMonitor.Value = 32;
            temperatureMonitor.Value = 28;

            Console.WriteLine("\n--- Пример 3: Система логирования со статическими событиями ---");
            
            // Подписываемся на событие логирования
            Logger.LogReceived += (sender, e) => {
                if (e.Level >= LogLevel.Warning)
                {
                    Console.WriteLine($"ВАЖНО: {e.Timestamp:HH:mm:ss} - {e.Message}");
                }
            };
            
            // Логируем сообщения
            Logger.Debug("Это отладочное сообщение");
            Logger.Info("Это информационное сообщение");
            Logger.Warning("Это предупреждение");
            Logger.Error("Это сообщение об ошибке");
            
            // Отписываемся от события (правильный способ)
            // В реальных приложениях важно отписываться от событий, когда они больше не нужны
            EventHandler<LogEventArgs> handler = (sender, e) => { };
            Logger.LogReceived += handler;
            Logger.LogReceived -= handler;
            
            Console.WriteLine("Отписались от события Logger.LogReceived");

            Console.WriteLine("\n--- Пример 4: Простое приложение для демонстрации событий ---");
            
            // Создаем элементы управления
            Button submitButton = new Button("Отправить");
            Button cancelButton = new Button("Отмена");
            TextBox nameTextBox = new TextBox();
            
            // Подписываемся на события кнопок и текстового поля
            submitButton.Click += (sender, e) => {
                Button button = (Button)sender;
                Console.WriteLine($"Обработка нажатия кнопки '{button.Text}'");
                Console.WriteLine($"Отправка данных: {nameTextBox.Text}");
            };
            
            cancelButton.Click += (sender, e) => {
                Console.WriteLine("Операция отменена");
                nameTextBox.Text = string.Empty;
            };
            
            nameTextBox.TextChanged += (sender, e) => {
                Console.WriteLine($"Текст изменен с '{e.OldText}' на '{e.NewText}'");
            };
            
            // Симулируем взаимодействие с пользовательским интерфейсом
            nameTextBox.Text = "Иван";
            submitButton.PerformClick();
            nameTextBox.Text = "Иван Петров";
            cancelButton.PerformClick();

            Console.WriteLine("\n--- Пример 5: Пользовательские аксессоры событий ---");
            
            // Создаем экземпляр класса с пользовательскими аксессорами событий
            Counter counter = new Counter();
            
            // Подписываемся на событие
            EventHandler handler1 = (sender, e) => Console.WriteLine("Обработчик 1: счетчик достиг порогового значения");
            EventHandler handler2 = (sender, e) => Console.WriteLine("Обработчик 2: счетчик достиг порогового значения");
            
            Console.WriteLine("Подписываем обработчик 1...");
            counter.Threshold += handler1;
            
            Console.WriteLine("Подписываем обработчик 2...");
            counter.Threshold += handler2;
            
            // Увеличиваем счетчик
            counter.Increment();
            counter.Increment();
            counter.Increment();
            
            // Отписываемся от события
            Console.WriteLine("Отписываем обработчик 1...");
            counter.Threshold -= handler1;
            
            counter.Increment();
        }
    }
    
    // Класс для демонстрации пользовательских аксессоров событий
    public class Counter
    {
        private int _count;
        private EventHandler _threshold;
        
        // Событие с пользовательскими аксессорами
        public event EventHandler Threshold
        {
            add
            {
                Console.WriteLine("Добавление обработчика события Threshold");
                _threshold += value;
            }
            remove
            {
                Console.WriteLine("Удаление обработчика события Threshold");
                _threshold -= value;
            }
        }
        
        public void Increment()
        {
            _count++;
            Console.WriteLine($"Счетчик: {_count}");
            
            // Если счетчик достиг 3, вызываем событие
            if (_count == 3)
            {
                OnThreshold();
            }
        }
        
        protected virtual void OnThreshold()
        {
            Console.WriteLine("Достигнуто пороговое значение!");
            _threshold?.Invoke(this, EventArgs.Empty);
        }
    }

    #region Задачи
    /*
        # Создайте класс Product с событием PriceChanged, которое срабатывает при 
          изменении цены продукта. Для события создайте пользовательский класс 
          аргументов PriceChangedEventArgs, содержащий старую и новую цену. В метод 
          Main_() добавьте код для демонстрации работы события.
        
        # Реализуйте простую систему публикации новостей: создайте класс NewsPublisher, 
          который может публиковать новости разных категорий (спорт, политика, технологии). 
          Добавьте в класс событие NewsPublished, которое принимает пользовательские 
          аргументы с информацией о новости. Создайте несколько подписчиков, которые 
          будут реагировать только на определенные категории новостей.
        
        # Напишите класс FileWatcher, который имитирует мониторинг файлов в директории. 
          Добавьте события FileCreated, FileDeleted и FileModified. Реализуйте методы, 
          которые имитируют эти действия с файлами и вызывают соответствующие события.
          В аргументах события передавайте информацию о файле (имя, размер, путь).
        
        # Создайте систему уведомлений для банковского счета: разработайте класс BankAccount 
          с событиями Deposited, Withdrawn и BalanceBelowThreshold. Для каждого события 
          создайте соответствующий класс аргументов. Добавьте методы для внесения и снятия 
          средств, которые будут вызывать соответствующие события.
        
        # Реализуйте класс Stopwatch (секундомер) с событиями Started, Stopped и Lap 
          (круг). Добавьте методы Start(), Stop() и Lap(), которые будут вызывать 
          соответствующие события. В аргументах событий передавайте соответствующую 
          информацию (например, время старта, остановки, круга).
    */
    #endregion
}
