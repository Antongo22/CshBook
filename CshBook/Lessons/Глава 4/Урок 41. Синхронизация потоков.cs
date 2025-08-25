using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CshBook.Lessons.Глава_4
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о синхронизации потоков в C#:
     * 
     * - Проблемы совместного доступа к данным в многопоточном программировании
     * - Понятия блокировки и синхронизации
     * - Класс Monitor и оператор lock
     * - Класс Interlocked для атомарных операций
     * - Классы для более сложной синхронизации (Mutex, Semaphore)
     * - Deadlock и способы его избежания
     * - Блоки критических секций и потокобезопасность
     */

    /*
       Проблемы совместного доступа к данным
       =================================
       
       В многопоточных приложениях возникают специфические проблемы, связанные с 
       параллельным доступом к общим ресурсам (переменным, объектам, файлам и т.д.):
       
       1. Race Condition (состояние гонки) - ситуация, когда результат выполнения 
          программы зависит от непредсказуемого порядка выполнения потоков.
       
       2. Data Corruption (повреждение данных) - ситуация, когда несколько потоков 
          одновременно модифицируют данные, что приводит к их несогласованности.
       
       3. Неатомарные операции - операции, которые могут быть прерваны в середине выполнения.
          Даже такая простая операция как i++ в многопоточной среде не является атомарной:
          - она состоит из чтения значения, инкремента и записи нового значения
          - между этими шагами другой поток может успеть изменить переменную
       
       4. Видимость изменений - изменения, сделанные в одном потоке, могут быть 
          не сразу видны другим потокам из-за кэширования значений в регистрах процессора
          или в кэше потока.
    */

    /*
       Понятия блокировки и синхронизации
       ===============================
       
       Синхронизация потоков - механизмы, обеспечивающие координацию работы потоков 
       и предотвращающие одновременный доступ к общим ресурсам.
       
       Основные стратегии синхронизации:
       
       1. Блокировка (Locking) - ограничение доступа к ресурсу только одному потоку в момент времени.
          Потоки, пытающиеся получить доступ к заблокированному ресурсу, ожидают его освобождения.
       
       2. Атомарные операции - операции, которые выполняются как единое целое и не могут быть прерваны.
       
       3. Сигнальные примитивы - механизмы для координации действий потоков 
          (один поток сигнализирует другим о наступлении определенного события).
       
       4. Потокобезопасные коллекции - специальные реализации коллекций, 
          безопасные для использования в многопоточной среде.
    */

    /*
       Класс Monitor и оператор lock
       ==========================
       
       Класс Monitor представляет собой инструмент синхронизации, обеспечивающий 
       эксклюзивный доступ к объекту через блокировку.
       
       Основные методы Monitor:
       
       - Enter(object) - попытка получить блокировку объекта, если блокировка недоступна, 
         поток блокируется до ее освобождения
       - TryEnter(object) - попытка получить блокировку, возвращает true при успехе
       - Exit(object) - освобождение блокировки
       - Wait(object) - освобождение блокировки и ожидание сигнала
       - Pulse(object) / PulseAll(object) - отправка сигнала одному/всем ожидающим потокам
       
       Оператор lock - это синтаксический сахар для использования Monitor. Он автоматически 
       вызывает Monitor.Enter в начале блока и Monitor.Exit в конце, даже при возникновении исключений.
       
       lock (объект)
       {
           // Защищенный код, доступный только одному потоку одновременно
       }
       
       Эквивалент без использования lock:
       
       object obj = new object();
       Monitor.Enter(obj);
       try
       {
           // Защищенный код
       }
       finally
       {
           Monitor.Exit(obj);
       }
    */

    /*
       Класс Interlocked для атомарных операций
       ===================================
       
       Класс Interlocked предоставляет атомарные операции для распространенных сценариев 
       работы с примитивными типами в многопоточной среде.
       
       Основные методы Interlocked:
       
       - Increment(ref int) - атомарно увеличивает значение на 1
       - Decrement(ref int) - атомарно уменьшает значение на 1
       - Add(ref int, int) - атомарно добавляет значение
       - Exchange(ref T, T) - атомарно заменяет значение и возвращает исходное
       - CompareExchange(ref T, T, T) - атомарно заменяет значение, если оно равно сравниваемому
       
       Пример использования:
       
       int counter = 0;
       
       // Безопасно в многопоточной среде
       Interlocked.Increment(ref counter);  // counter++
       
       // Тоже безопасно
       int original = Interlocked.Exchange(ref counter, 10);  // заменяет counter на 10
    */

    /*
       Классы для более сложной синхронизации
       =================================
       
       C# предоставляет набор классов для более сложных сценариев синхронизации:
       
       1. Mutex (взаимное исключение) - примитив синхронизации, который может 
          использоваться для синхронизации между процессами.
          
          Mutex mutex = new Mutex(false, "MyMutex");
          mutex.WaitOne();  // Получение блокировки
          try { ... } finally { mutex.ReleaseMutex(); }
       
       2. Semaphore (семафор) - ограничивает количество потоков, одновременно 
          обращающихся к ресурсу.
          
          Semaphore semaphore = new Semaphore(2, 4);  // Начальное=2, максимальное=4
          semaphore.WaitOne();  // Занимает одно место
          try { ... } finally { semaphore.Release(); }
       
       3. ManualResetEvent / AutoResetEvent - события для сигнализации между потоками.
          
          AutoResetEvent autoEvent = new AutoResetEvent(false);  // Изначально несигнальное
          autoEvent.WaitOne();  // Ожидание сигнала
          autoEvent.Set();  // Подача сигнала
       
       4. ReaderWriterLockSlim - блокировка, различающая операции чтения и записи.
          Позволяет нескольким потокам одновременно читать, но только одному записывать.
    */

    /*
       Deadlock и способы его избежания
       ============================
       
       Deadlock (взаимоблокировка) - ситуация, когда два или более потоков ожидают 
       освобождения ресурсов, захваченных друг другом, что приводит к полной остановке.
       
       Классический пример: 
       Поток 1 захватил ресурс A и ожидает ресурс B
       Поток 2 захватил ресурс B и ожидает ресурс A
       
       Способы избежания deadlock:
       
       1. Всегда захватывать ресурсы в одинаковом порядке
          Пример: всегда сначала блокировать A, потом B
       
       2. Использовать временные ограничения при захвате блокировок
          Пример: Monitor.TryEnter с таймаутом, освобождая все блокировки при неудаче
       
       3. Избегать вложенных блокировок
          Не вызывать код, который может захватывать блокировки, внутри уже захваченной блокировки
       
       4. Использовать более высокоуровневые абстракции синхронизации
          Например, async/await вместо прямых блокировок
    */

    /*
       Блоки критических секций и потокобезопасность
       ========================================
       
       Критическая секция - участок кода, который должен выполняться атомарно,
       без одновременного доступа из других потоков.
       
       Правила создания потокобезопасного кода:
       
       1. Идентифицировать разделяемые данные между потоками
       
       2. Выявить операции, которые должны быть атомарными
       
       3. Выбрать подходящий механизм синхронизации:
          - Для простых операций: Interlocked
          - Для блоков кода: lock или Monitor
          - Для более сложных сценариев: другие примитивы синхронизации
       
       4. Минимизировать размер критических секций для повышения параллелизма
       
       5. Рассмотреть использование потокобезопасных коллекций вместо
          ручной синхронизации (ConcurrentDictionary, ConcurrentQueue и т.д.)
       
       6. Помнить о возможных исключениях и гарантировать освобождение блокировок
    */
    #endregion

    public static class ThreadSynchronizationLesson
    {
        // Пример 1: Демонстрация проблемы состояния гонки (race condition)
        static private int unsafeCounter = 0;
        static private object lockObject = new object();
        static private int safeCounter = 0;
        static private int interlockedCounter = 0;

        public static void Main_()
        {
            Console.WriteLine("*** Урок 41: Синхронизация потоков ***\n");

            // Пример 1: Демонстрация проблемы race condition и ее решение
            Console.WriteLine("--- Пример 1: Проблема состояния гонки ---");
            
            // Сбрасываем счетчики
            unsafeCounter = 0;
            safeCounter = 0;
            interlockedCounter = 0;
            
            // Создаем и запускаем несколько потоков, которые будут инкрементировать счетчики
            const int threadCount = 5;
            const int incrementsPerThread = 1000000;
            Thread[] threads = new Thread[threadCount];
            
            // Запускаем потоки
            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(() =>
                {
                    // Небезопасный инкремент - возможно состояние гонки
                    for (int j = 0; j < incrementsPerThread; j++)
                    {
                        unsafeCounter++;
                    }
                    
                    // Безопасный инкремент с использованием lock
                    for (int j = 0; j < incrementsPerThread; j++)
                    {
                        lock (lockObject)
                        {
                            safeCounter++;
                        }
                    }
                    
                    // Безопасный инкремент с использованием Interlocked
                    for (int j = 0; j < incrementsPerThread; j++)
                    {
                        Interlocked.Increment(ref interlockedCounter);
                    }
                });
                
                threads[i].Start();
            }
            
            // Ждем завершения всех потоков
            foreach (var thread in threads)
            {
                thread.Join();
            }
            
            // Выводим результаты
            Console.WriteLine($"Ожидаемое значение: {threadCount * incrementsPerThread}");
            Console.WriteLine($"Небезопасный счетчик: {unsafeCounter}");
            Console.WriteLine($"Безопасный счетчик с lock: {safeCounter}");
            Console.WriteLine($"Безопасный счетчик с Interlocked: {interlockedCounter}\n");

            // Пример 2: Использование Monitor для более сложной синхронизации
            Console.WriteLine("--- Пример 2: Использование Monitor ---");
            
            object productObject = new object();
            Queue<string> products = new Queue<string>();
            bool productionCompleted = false;
            
            // Создаем поток производителя
            Thread producer = new Thread(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    // Имитация работы
                    Thread.Sleep(100);
                    
                    Monitor.Enter(productObject);
                    try
                    {
                        products.Enqueue($"Товар {i}");
                        Console.WriteLine($"Производитель: произведен товар {i}");
                        
                        // Сигнализируем о наличии нового товара
                        Monitor.Pulse(productObject);
                    }
                    finally
                    {
                        Monitor.Exit(productObject);
                    }
                }
                
                // Сигнализируем о завершении производства
                Monitor.Enter(productObject);
                try
                {
                    productionCompleted = true;
                    // Будим всех потребителей, чтобы они могли проверить условие завершения
                    Monitor.PulseAll(productObject);
                }
                finally
                {
                    Monitor.Exit(productObject);
                }
            });
            
            // Создаем поток потребителя
            Thread consumer = new Thread(() =>
            {
                while (true)
                {
                    Monitor.Enter(productObject);
                    try
                    {
                        // Ждем, пока появится товар или завершится производство
                        while (products.Count == 0)
                        {
                            if (productionCompleted)
                            {
                                Monitor.Exit(productObject);
                                return; // Завершаем работу потребителя
                            }
                            
                            // Ждем сигнала от производителя
                            Monitor.Wait(productObject);
                        }
                        
                        // Забираем товар из очереди
                        string product = products.Dequeue();
                        Console.WriteLine($"Потребитель: получен {product}");
                    }
                    finally
                    {
                        if (Monitor.IsEntered(productObject))
                            Monitor.Exit(productObject);
                    }
                }
            });
            
            // Запускаем потоки
            producer.Start();
            consumer.Start();
            
            // Ждем завершения потоков
            producer.Join();
            consumer.Join();
            
            Console.WriteLine("Производство и потребление завершены\n");

            // Пример 3: Использование различных примитивов синхронизации
            Console.WriteLine("--- Пример 3: Различные примитивы синхронизации ---");
            
            // Демонстрация использования Semaphore
            Semaphore semaphore = new Semaphore(2, 2); // Максимум 2 потока одновременно
            
            for (int i = 1; i <= 5; i++)
            {
                int id = i;
                new Thread(() => 
                {
                    Console.WriteLine($"Поток {id}: ожидание доступа к ресурсу...");
                    semaphore.WaitOne(); // Попытка получить доступ
                    
                    try 
                    {
                        Console.WriteLine($"Поток {id}: получил доступ к ресурсу");
                        Thread.Sleep(1000); // Имитация работы с ресурсом
                    } 
                    finally 
                    {
                        Console.WriteLine($"Поток {id}: освобождает ресурс");
                        semaphore.Release();
                    }
                }).Start();
            }
            
            // Даем время на выполнение примера с семафором
            Thread.Sleep(6000);

            // Демонстрация использования ManualResetEvent
            Console.WriteLine("\nИспользование ManualResetEvent:");
            
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            
            for (int i = 1; i <= 3; i++)
            {
                int id = i;
                new Thread(() => 
                {
                    Console.WriteLine($"Поток {id}: ожидание сигнала...");
                    resetEvent.WaitOne();
                    Console.WriteLine($"Поток {id}: сигнал получен, продолжаем работу");
                }).Start();
            }
            
            // Даем время потокам начать ожидание
            Thread.Sleep(1000);
            
            // Подаем сигнал всем потокам
            Console.WriteLine("Основной поток: подаем сигнал всем потокам");
            resetEvent.Set();
            
            // Даем время на обработку сигнала
            Thread.Sleep(1000);
            
            // Сбрасываем событие
            resetEvent.Reset();
            
            // Запускаем еще один поток для демонстрации сброса
            new Thread(() => 
            {
                Console.WriteLine("Новый поток: ожидание сигнала (который был сброшен)...");
                resetEvent.WaitOne(2000);
                Console.WriteLine("Новый поток: таймаут ожидания");
            }).Start();
            
            // Даем время для завершения всех потоков
            Thread.Sleep(3000);

            // Пример 4: Избегание deadlock
            Console.WriteLine("\n--- Пример 4: Избегание deadlock ---");
            
            object resourceA = new object();
            object resourceB = new object();
            
            // Функция, которая может вызвать deadlock
            Action potentialDeadlockAction = () =>
            {
                // Создаем два потока, которые захватывают ресурсы в разном порядке
                Thread thread1 = new Thread(() =>
                {
                    lock (resourceA)
                    {
                        Console.WriteLine("Поток 1: захватил ресурс A, ожидание ресурса B...");
                        Thread.Sleep(1000); // Имитируем работу, чтобы увеличить вероятность deadlock
                        
                        lock (resourceB)
                        {
                            Console.WriteLine("Поток 1: захватил ресурс B");
                        }
                    }
                });
                
                Thread thread2 = new Thread(() =>
                {
                    lock (resourceB)
                    {
                        Console.WriteLine("Поток 2: захватил ресурс B, ожидание ресурса A...");
                        Thread.Sleep(1000); // Имитируем работу
                        
                        lock (resourceA)
                        {
                            Console.WriteLine("Поток 2: захватил ресурс A");
                        }
                    }
                });
                
                // Запускаем потоки
                thread1.Start();
                thread2.Start();
                
                // Ждем завершения потоков с таймаутом, чтобы избежать реального deadlock
                bool thread1Joined = thread1.Join(3000);
                bool thread2Joined = thread2.Join(3000);
                
                if (!thread1Joined || !thread2Joined)
                {
                    Console.WriteLine("Обнаружена взаимоблокировка (deadlock)!");
                }
            };
            
            // Функция с исправленным порядком блокировок
            Action fixedDeadlockAction = () =>
            {
                // Исправленная версия - оба потока захватывают ресурсы в одинаковом порядке
                Thread thread1 = new Thread(() =>
                {
                    lock (resourceA)
                    {
                        Console.WriteLine("Поток 1 (исправл.): захватил ресурс A, ожидание ресурса B...");
                        Thread.Sleep(500);
                        
                        lock (resourceB)
                        {
                            Console.WriteLine("Поток 1 (исправл.): захватил ресурс B, выполняется работа...");
                            Thread.Sleep(500);
                            Console.WriteLine("Поток 1 (исправл.): освобождает ресурсы");
                        }
                    }
                });
                
                Thread thread2 = new Thread(() =>
                {
                    Thread.Sleep(100); // Небольшая задержка для демонстрации
                    // Важно: блокировки в том же порядке, что и в потоке 1
                    lock (resourceA)
                    {
                        Console.WriteLine("Поток 2 (исправл.): захватил ресурс A, ожидание ресурса B...");
                        Thread.Sleep(500);
                        
                        lock (resourceB)
                        {
                            Console.WriteLine("Поток 2 (исправл.): захватил ресурс B, выполняется работа...");
                            Thread.Sleep(500);
                            Console.WriteLine("Поток 2 (исправл.): освобождает ресурсы");
                        }
                    }
                });
                
                thread1.Start();
                thread2.Start();
                
                // Добавляем таймаут для безопасности
                bool completed = thread1.Join(5000) && thread2.Join(5000);
                
                if (completed)
                {
                    Console.WriteLine("Потоки успешно завершились без взаимоблокировки");
                }
                else
                {
                    Console.WriteLine("Внимание: потоки не завершились в ожидаемое время");
                    // Прерываем потоки если они зависли
                    if (thread1.IsAlive) thread1.Interrupt();
                    if (thread2.IsAlive) thread2.Interrupt();
                }
            };
            
            // Сначала демонстрируем потенциальный deadlock
            Console.WriteLine("Демонстрация потенциального deadlock:");
            potentialDeadlockAction();
            
            // Затем показываем исправленную версию
            Console.WriteLine("\nВерсия без deadlock:");
            fixedDeadlockAction();

            // Пример 5: Потокобезопасная коллекция
            Console.WriteLine("\n--- Пример 5: Потокобезопасные операции с коллекцией ---");
            
            // Создаем обычный список и потокобезопасный список с блокировкой
            List<int> unsafeList = new List<int>();
            List<int> safeList = new List<int>();
            object safeListLock = new object();
            
            // Запускаем несколько потоков, добавляющих элементы в списки
            const int threadCountForLists = 10;
            const int itemsPerThread = 1000;
            
            Thread[] threadsForLists = new Thread[threadCountForLists];
            for (int i = 0; i < threadCountForLists; i++)
            {
                threadsForLists[i] = new Thread(() =>
                {
                    for (int j = 0; j < itemsPerThread; j++)
                    {
                        // Небезопасное добавление - может вызвать исключение
                        try
                        {
                            unsafeList.Add(j);
                        }
                        catch (Exception)
                        {
                            // Возможны исключения из-за одновременного доступа
                        }
                        
                        // Безопасное добавление с использованием блокировки
                        lock (safeListLock)
                        {
                            safeList.Add(j);
                        }
                    }
                });
                
                threadsForLists[i].Start();
            }
            
            // Ждем завершения всех потоков
            foreach (var thread in threadsForLists)
            {
                thread.Join();
            }
            
            // Проверяем результаты
            Console.WriteLine($"Ожидаемое количество элементов: {threadCountForLists * itemsPerThread}");
            Console.WriteLine($"Фактическое количество элементов в небезопасном списке: {unsafeList.Count}");
            Console.WriteLine($"Фактическое количество элементов в безопасном списке: {safeList.Count}");
            
            Console.WriteLine("\nЗавершение урока по синхронизации потоков.");
        }
    }

    #region Задачи
    /*
        # Реализуйте класс ThreadSafeCounter, который обеспечивает потокобезопасный
          инкремент, декремент и получение текущего значения счетчика. Используйте 
          разные подходы для реализации: с помощью lock, Monitor и Interlocked. 
          Сравните производительность разных реализаций.
        
        # Создайте класс BankAccount с потокобезопасными методами Deposit и Withdraw.
          Реализуйте также метод Transfer для перевода денег между двумя счетами.
          Учтите возможность deadlock при переводе между счетами.
        
        # Напишите программу, моделирующую задачу "Обедающие философы" - классическую 
          задачу синхронизации. Пять философов сидят за круглым столом, перед каждым 
          есть тарелка спагетти. Между каждыми двумя тарелками лежит одна вилка.
          Философ может либо думать, либо есть. Для того чтобы поесть, философу нужны 
          две вилки (левая и правая). Реализуйте решение, избегающее deadlock.
        
        # Разработайте систему кеширования с использованием ReaderWriterLockSlim.
          Ваша система должна позволять нескольким потокам одновременно читать данные 
          из кеша, но только одному потоку записывать в него. Также реализуйте политику
          устаревания кеша.
        
        # Создайте простую систему "издатель-подписчик" (publisher-subscriber) с 
          использованием примитивов синхронизации. Система должна позволять нескольким 
          издателям публиковать сообщения, а подписчикам - получать только те типы 
          сообщений, на которые они подписаны.
    */
    #endregion
}
