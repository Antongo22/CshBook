using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace CshBook.Lessons.Глава_4
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о пуле потоков и паттернах многопоточного программирования:
     * 
     * - Что такое пул потоков и зачем он нужен
     * - Класс ThreadPool в .NET
     * - Компонент BackgroundWorker для UI-приложений
     * - Основные паттерны многопоточного программирования
     * - Асинхронные делегаты и шаблон асинхронного вызова
     * - Task Parallel Library (основы)
     */

    /*
       Что такое пул потоков и зачем он нужен
       ================================
       
       Пул потоков - это коллекция потоков, которые могут повторно использоваться 
       для выполнения различных задач. Вместо создания новых потоков для каждой задачи,
       система переиспользует уже созданные потоки из пула.
       
       Преимущества использования пула потоков:
       
       1. Уменьшение накладных расходов на создание и уничтожение потоков
          - Создание потока - дорогостоящая операция в терминах ресурсов
          - Потоки из пула уже созданы и готовы к использованию
       
       2. Ограничение количества одновременно выполняющихся потоков
          - Слишком много потоков могут создать чрезмерную нагрузку на систему
          - Пул контролирует оптимальное количество рабочих потоков
       
       3. Повышение производительности приложений
          - Потоки используются эффективно и распределяются равномерно
          - Снижение конкуренции за ресурсы между потоками
       
       4. Упрощение управления потоками
          - Автоматическое распределение задач между потоками
          - Не нужно явно создавать, запускать и завершать потоки
    */

    /*
       Класс ThreadPool в .NET
       ====================
       
       Класс ThreadPool в .NET предоставляет готовый пул рабочих потоков, который 
       управляется средой выполнения CLR и автоматически подстраивается под нагрузку.
       
       Основные методы ThreadPool:
       
       1. QueueUserWorkItem - постановка работы в очередь пула потоков
          
          ThreadPool.QueueUserWorkItem(callBack, state);
          
          где:
          - callBack - метод, который будет вызван в потоке из пула (WaitCallback)
          - state - объект, передаваемый методу callBack
       
       2. GetAvailableThreads - получение информации о доступных потоках в пуле
          
          int workerThreads, completionPortThreads;
          ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
       
       3. GetMaxThreads - получение информации о максимальном количестве потоков в пуле
          
          int maxWorkerThreads, maxCompletionPortThreads;
          ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);
       
       4. SetMaxThreads - установка максимального количества потоков в пуле
          
          ThreadPool.SetMaxThreads(workerThreads, completionPortThreads);
    */

    /*
       BackgroundWorker для UI-приложений
       ==============================
       
       Класс BackgroundWorker из пространства имен System.ComponentModel предоставляет
       простой способ выполнения длительных операций в фоновом режиме, особенно полезный 
       для приложений с графическим интерфейсом (Windows Forms, WPF).
       
       BackgroundWorker автоматически создает отдельный поток для выполнения задачи и
       предоставляет механизмы для:
       - Запуска фоновой операции
       - Отмены операции
       - Получения обновлений о прогрессе
       - Обработки завершения операции
       
       Основные события и методы:
       
       - DoWork - событие, которое происходит при запуске фоновой операции
       - ProgressChanged - событие, происходящее при обновлении прогресса
       - RunWorkerCompleted - событие, происходящее при завершении операции
       
       - RunWorkerAsync - метод для запуска фоновой операции
       - CancelAsync - метод для запроса отмены операции
       - ReportProgress - метод для отчета о прогрессе выполнения
    */

    /*
       Основные паттерны многопоточного программирования
       ===========================================
       
       1. Producer-Consumer (Производитель-Потребитель)
          - Один или несколько потоков производят данные
          - Один или несколько потоков потребляют данные
          - Потоки взаимодействуют через общую буферизованную очередь
          - Производитель сигнализирует о наличии данных
          - Потребитель ожидает появления данных
       
       2. Master-Worker (Мастер-Работник)
          - Мастер разделяет задачу на подзадачи и распределяет их между рабочими
          - Рабочие выполняют подзадачи и возвращают результаты
          - Мастер собирает и объединяет результаты
          - Также известен как Fork-Join или MapReduce
       
       3. Read-Write Lock (Блокировка чтения-записи)
          - Несколько потоков могут одновременно читать данные
          - Только один поток может записывать данные
          - При записи никто не может читать
          - Реализован через ReaderWriterLockSlim
       
       4. Pipeline (Конвейер)
          - Задача разбивается на последовательные этапы
          - Каждый этап обрабатывается отдельным потоком
          - Данные перемещаются от одного этапа к другому
          - Повышение производительности за счет параллельного выполнения этапов
    */

    /*
       Асинхронные делегаты и шаблон асинхронного вызова
       ===========================================
       
       Асинхронные делегаты - механизм в .NET для асинхронного вызова методов с
       использованием стандартных делегатов.
       
       Шаблон асинхронного вызова включает:
       
       1. BeginInvoke - начинает асинхронный вызов метода
          
          delegateInstance.BeginInvoke(параметры, AsyncCallback, объект_состояния);
          
          где:
          - AsyncCallback - делегат, который будет вызван по завершении операции
          - объект_состояния - любой объект, который будет доступен при завершении
       
       2. EndInvoke - получает результат асинхронного вызова
          
          результат = delegateInstance.EndInvoke(IAsyncResult);
       
       3. IAsyncResult - интерфейс для работы с асинхронными операциями
          
          - AsyncWaitHandle - WaitHandle для ожидания завершения операции
          - IsCompleted - флаг завершения операции
          - AsyncState - объект состояния, переданный в BeginInvoke
       
       Примечание: Этот механизм считается устаревшим в современном .NET. 
       Рекомендуется использовать Task Parallel Library и async/await вместо него.
    */

    /*
       Task Parallel Library (основы)
       =========================
       
       Task Parallel Library (TPL) - это набор типов и методов для эффективного 
       параллельного и асинхронного программирования в .NET.
       
       Основные компоненты TPL:
       
       1. Класс Task - представляет асинхронную операцию
          
          - Task.Run(Action) - запуск задачи в пуле потоков
          - Task.Factory.StartNew(Action) - более гибкий способ запуска задачи
          - task.Wait() - ожидание завершения задачи
          - task.Result - получение результата (блокирует до завершения)
          - Task.WhenAll() - ожидание завершения нескольких задач
          - Task.WhenAny() - ожидание завершения любой из задач
       
       2. Класс Task<TResult> - представляет асинхронную операцию с результатом
          
          Task<int> task = Task.Run(() => ComputeValue());
          int result = task.Result; // Блокирует до получения результата
       
       3. Класс Parallel - высокоуровневые параллельные операции
          
          - Parallel.For - параллельная версия цикла for
          - Parallel.ForEach - параллельная версия цикла foreach
          - Parallel.Invoke - параллельное выполнение нескольких методов
       
       4. Класс TaskScheduler - управление выполнением задач
          
          - TaskScheduler.Default - использует ThreadPool
          - TaskScheduler.FromCurrentSynchronizationContext - для UI-потока
       
       5. CancellationToken - механизм для отмены операций
          
          CancellationTokenSource cts = new CancellationTokenSource();
          Task task = Task.Run(() => LongOperation(cts.Token));
          cts.Cancel(); // Запрос отмены
    */
    #endregion

    public static class ThreadPoolAndPatternsLesson
    {
        public static void Main_()
        {
            Console.WriteLine("*** Урок 42: Пул потоков и паттерны ***\n");

            // Пример 1: Использование ThreadPool
            Console.WriteLine("--- Пример 1: Использование ThreadPool ---");
            
            // Получаем информацию о пуле потоков
            int workerThreads, completionPortThreads;
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
            
            Console.WriteLine($"Максимальное количество потоков в пуле:");
            Console.WriteLine($"  Рабочих потоков: {workerThreads}");
            Console.WriteLine($"  Потоков портов завершения I/O: {completionPortThreads}");
            
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            
            Console.WriteLine($"Доступное количество потоков в пуле:");
            Console.WriteLine($"  Рабочих потоков: {workerThreads}");
            Console.WriteLine($"  Потоков портов завершения I/O: {completionPortThreads}");
            
            // Ставим в очередь работу для потоков из пула
            Console.WriteLine("\nПостановка задач в очередь пула потоков...");
            
            // Используем ManualResetEvent для синхронизации между потоками
            ManualResetEvent[] doneEvents = new ManualResetEvent[5];
            
            for (int i = 0; i < doneEvents.Length; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                int taskNum = i;
                
                // Ставим работу в очередь пула потоков
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Console.WriteLine($"Задача {taskNum} начала выполнение в потоке {Thread.CurrentThread.ManagedThreadId}");
                    
                    // Имитация работы
                    Thread.Sleep(1000);
                    
                    Console.WriteLine($"Задача {taskNum} завершила выполнение");
                    
                    // Сигнализируем о завершении
                    ((ManualResetEvent)state).Set();
                }, doneEvents[i]);
            }
            
            // Ждем завершения всех задач
            WaitHandle.WaitAll(doneEvents);
            
            Console.WriteLine("Все задачи из пула потоков завершены\n");

            // Пример 2: Использование BackgroundWorker
            Console.WriteLine("--- Пример 2: Использование BackgroundWorker ---");
            
            // Создаем новый экземпляр BackgroundWorker
            BackgroundWorker worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            
            // Настраиваем обработчики событий
            worker.DoWork += (sender, e) =>
            {
                // Получаем ссылку на объект BackgroundWorker
                var bw = sender as BackgroundWorker;
                
                // Имитируем длительную операцию с отчетом о прогрессе
                for (int i = 1; i <= 10; i++)
                {
                    // Проверяем, была ли запрошена отмена
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    
                    // Выполняем работу
                    Console.WriteLine($"BackgroundWorker: выполнение шага {i} из 10");
                    Thread.Sleep(300);
                    
                    // Отчет о прогрессе
                    bw.ReportProgress(i * 10);
                }
                
                // Возвращаем результат
                e.Result = "Обработка успешно завершена";
            };
            
            worker.ProgressChanged += (sender, e) =>
            {
                Console.WriteLine($"BackgroundWorker: прогресс {e.ProgressPercentage}%");
            };
            
            worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Cancelled)
                {
                    Console.WriteLine("BackgroundWorker: операция была отменена");
                }
                else if (e.Error != null)
                {
                    Console.WriteLine($"BackgroundWorker: произошла ошибка: {e.Error.Message}");
                }
                else
                {
                    Console.WriteLine($"BackgroundWorker: результат - {e.Result}");
                }
            };
            
            // Запускаем фоновую работу
            Console.WriteLine("Запуск BackgroundWorker...");
            worker.RunWorkerAsync();
            
            // Даем время для выполнения нескольких шагов
            Thread.Sleep(1500);
            
            // Решаем, отменять ли операцию
            bool cancelOperation = false;
            if (cancelOperation)
            {
                Console.WriteLine("Отмена BackgroundWorker...");
                worker.CancelAsync();
            }
            
            // Ждем завершения работы
            while (worker.IsBusy)
            {
                Thread.Sleep(100);
            }
            
            Console.WriteLine("BackgroundWorker завершил работу\n");

            // Пример 3: Реализация паттерна Producer-Consumer
            Console.WriteLine("--- Пример 3: Паттерн Producer-Consumer ---");
            
            // Создаем общий буфер с ограниченным размером
            ProducerConsumerBuffer<int> buffer = new ProducerConsumerBuffer<int>(5);
            
            // Поток производителя
            Thread producer = new Thread(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    buffer.Add(i);
                    Console.WriteLine($"Производитель: добавлен элемент {i}");
                    Thread.Sleep(200);
                }
                
                // Сигнализируем о завершении производства
                buffer.CompleteAdding();
                Console.WriteLine("Производитель: работа завершена");
            });
            
            // Поток потребителя
            Thread consumer = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        int item = buffer.Take();
                        Console.WriteLine($"Потребитель: получен элемент {item}");
                        Thread.Sleep(500); // Потребитель медленнее производителя
                    }
                }
                catch (InvalidOperationException)
                {
                    // Буфер закрыт - завершаем работу
                    Console.WriteLine("Потребитель: работа завершена");
                }
            });
            
            // Запускаем потоки
            producer.Start();
            consumer.Start();
            
            // Ждем завершения потоков
            producer.Join();
            consumer.Join();
            
            Console.WriteLine("Паттерн Producer-Consumer завершен\n");

            // Пример 4: Реализация паттерна Master-Worker
            Console.WriteLine("--- Пример 4: Паттерн Master-Worker ---");
            
            // Создаем задачу для вычисления сумм в диапазонах чисел
            int[] ranges = { 1000000, 2000000, 3000000, 4000000, 5000000 };
            long[] results = new long[ranges.Length];
            
            // Мастер создает и запускает рабочих
            Console.WriteLine("Мастер: создание задач для рабочих");
            
            Thread[] workers = new Thread[ranges.Length];
            for (int i = 0; i < ranges.Length; i++)
            {
                int rangeIndex = i;
                workers[i] = new Thread(() =>
                {
                    // Рабочий вычисляет сумму чисел в диапазоне
                    Console.WriteLine($"Рабочий {rangeIndex}: начало вычислений");
                    long sum = 0;
                    for (int j = 1; j <= ranges[rangeIndex]; j++)
                    {
                        sum += j;
                    }
                    results[rangeIndex] = sum;
                    Console.WriteLine($"Рабочий {rangeIndex}: завершение вычислений, результат = {sum}");
                });
            }
            
            // Запускаем всех рабочих
            Console.WriteLine("Мастер: запуск всех рабочих");
            foreach (Thread worker_ in workers)
            {
                worker_.Start();
            }
            
            // Мастер ждет завершения всех рабочих
            foreach (Thread worker_ in workers)
            {
                worker_.Join();
            }
            
            // Мастер собирает результаты
            Console.WriteLine("Мастер: все рабочие завершили вычисления, сбор результатов");
            
            long totalSum = 0;
            for (int i = 0; i < results.Length; i++)
            {
                totalSum += results[i];
            }
            
            Console.WriteLine($"Мастер: общий результат = {totalSum}");
            Console.WriteLine("Паттерн Master-Worker завершен\n");

            // Пример 5: Асинхронные делегаты (устаревший подход)
            Console.WriteLine("--- Пример 5: Асинхронные делегаты ---");
            
            // Определяем делегат для длительной операции
            Func<int, int, int> longOperation = (a, b) =>
            {
                Console.WriteLine("Начало длительной операции...");
                Thread.Sleep(2000); // Имитация длительной работы
                Console.WriteLine("Длительная операция завершена");
                return a + b;
            };
            
            Console.WriteLine("Запуск асинхронного делегата...");
            
            // Вместо устаревшего подхода с BeginInvoke/EndInvoke используем Task
            var task = Task.Factory.StartNew(() => longOperation(10, 20));
            
            // Устанавливаем продолжение, которое выполнится после завершения задачи
            task.ContinueWith(t => 
            {
                Console.WriteLine("Callback: операция завершилась");
                Console.WriteLine($"Callback: результат = {t.Result}");
            });
            
            Console.WriteLine("Основной поток продолжает работу, не дожидаясь результата");
            
            // Можно проверить, завершилась ли операция
            Console.WriteLine($"Операция завершена? {task.IsCompleted}");
            
            // Ждем завершения операции
            task.Wait();
            
            Console.WriteLine("Асинхронный делегат завершен\n");

            // Пример 6: Основы Task Parallel Library
            Console.WriteLine("--- Пример 6: Task Parallel Library ---");
            
            // Создание и запуск задачи
            Console.WriteLine("Создание и запуск задачи с Task.Run...");
            
            Task task_ = Task.Run(() =>
            {
                Console.WriteLine($"Задача выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(1000);
                Console.WriteLine("Задача завершена");
            });
            
            Console.WriteLine("Основной поток продолжает работу...");
            
            // Ожидание завершения задачи
            task_.Wait();
            
            // Создание задачи с результатом
            Console.WriteLine("\nСоздание задачи с результатом...");
            
            Task<int> taskWithResult = Task.Run(() =>
            {
                Console.WriteLine("Вычисление результата...");
                Thread.Sleep(1000);
                return 42;
            });
            
            Console.WriteLine("Ожидание результата...");
            int result = taskWithResult.Result; // Блокирует до получения результата
            Console.WriteLine($"Результат задачи: {result}");
            
            // Цепочка задач с продолжением
            Console.WriteLine("\nЦепочка задач с продолжением...");
            
            Task<int> firstTask = Task.Run(() =>
            {
                Console.WriteLine("Выполняется первая задача...");
                Thread.Sleep(1000);
                return 10;
            });
            
            Task<int> continuationTask = firstTask.ContinueWith(antecedent =>
            {
                Console.WriteLine("Выполняется задача-продолжение...");
                return antecedent.Result * 2;
            });
            
            Console.WriteLine($"Результат цепочки задач: {continuationTask.Result}");
            
            // Параллельное выполнение операций
            Console.WriteLine("\nПараллельное выполнение с Parallel.For...");
            
            Parallel.For(0, 5, i =>
            {
                Console.WriteLine($"Итерация {i} выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(500);
            });
            
            // Параллельное выполнение нескольких методов
            Console.WriteLine("\nПараллельное выполнение методов с Parallel.Invoke...");
            
            Parallel.Invoke(
                () => { Console.WriteLine("Метод 1 выполняется..."); Thread.Sleep(1000); },
                () => { Console.WriteLine("Метод 2 выполняется..."); Thread.Sleep(800); },
                () => { Console.WriteLine("Метод 3 выполняется..."); Thread.Sleep(600); }
            );
            
            Console.WriteLine("Все параллельные операции завершены");
            
            Console.WriteLine("\nЗавершение урока по пулу потоков и паттернам.");
        }
    }
    
    // Реализация буфера для паттерна Producer-Consumer
    public class ProducerConsumerBuffer<T>
    {
        private Queue<T> _queue = new Queue<T>();
        private int _maxSize;
        private bool _completed;
        private object _lock = new object();
        
        public ProducerConsumerBuffer(int maxSize)
        {
            _maxSize = maxSize;
        }
        
        public void Add(T item)
        {
            lock (_lock)
            {
                // Ждем, пока в буфере появится место
                while (_queue.Count >= _maxSize && !_completed)
                {
                    Console.WriteLine("Производитель: буфер полон, ожидание...");
                    Monitor.Wait(_lock);
                }
                
                if (_completed)
                    throw new InvalidOperationException("Буфер закрыт для добавления");
                
                _queue.Enqueue(item);
                
                // Сигнализируем потребителю о появлении данных
                Monitor.Pulse(_lock);
            }
        }
        
        public T Take()
        {
            lock (_lock)
            {
                // Ждем, пока в буфере появятся данные
                while (_queue.Count == 0)
                {
                    if (_completed)
                        throw new InvalidOperationException("Буфер пуст и закрыт");
                    
                    Console.WriteLine("Потребитель: буфер пуст, ожидание...");
                    Monitor.Wait(_lock);
                }
                
                T item = _queue.Dequeue();
                
                // Сигнализируем производителю о наличии места
                Monitor.Pulse(_lock);
                
                return item;
            }
        }
        
        public void CompleteAdding()
        {
            lock (_lock)
            {
                _completed = true;
                
                // Разбудить всех ожидающих
                Monitor.PulseAll(_lock);
            }
        }
    }

    #region Задачи
    /*
        # Разработайте класс ThreadPoolManager, который позволяет управлять количеством
          потоков в пуле и отслеживать их использование. Реализуйте методы для:
          - Выполнения задачи в пуле потоков с отчетом о завершении
          - Получения статистики использования потоков
          - Ограничения максимального количества потоков в пуле
        
        # Создайте приложение для обработки коллекции больших файлов, используя паттерн 
          Producer-Consumer. Поток-производитель должен читать имена файлов из директории,
          а потоки-потребители - обрабатывать их (например, подсчитывать количество строк).
          Используйте пул потоков для потоков-потребителей.
        
        # Реализуйте паттерн Master-Worker для вычисления факториалов больших чисел.
          Мастер должен разбивать вычисление на подзадачи, распределять их между рабочими,
          и собирать результаты. Используйте Task Parallel Library для управления задачами.
        
        # Создайте систему мониторинга веб-сайтов с использованием BackgroundWorker.
          Система должна периодически проверять доступность списка веб-сайтов, сообщать
          о прогрессе и результатах проверки, а также поддерживать отмену операции.
        
        # Реализуйте паттерн Pipeline (конвейер) для обработки изображений. Конвейер
          должен состоять из нескольких этапов (например, загрузка, масштабирование,
          фильтрация, сохранение), каждый из которых выполняется в отдельном потоке.
          Для передачи данных между этапами используйте потокобезопасные очереди.
    */
    #endregion
}
