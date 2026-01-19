using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace CshBook.Lessons.Глава_4
{
    #region Теория
    /*
     * В этом уроке ты узнаешь основы асинхронного программирования в C#:
     * 
     * - Что такое асинхронность и зачем она нужна
     * - Ключевые слова async и await
     * - Класс Task и Task<T>
     * - Основные принципы работы с асинхронными методами
     * - Обработка исключений в асинхронном коде
     */

    /*
       Что такое асинхронность и зачем она нужна
       =====================================
       
       Асинхронное программирование позволяет выполнять длительные операции
       (например, чтение файлов, сетевые запросы, обращения к базе данных)
       без блокировки основного потока выполнения.
       
       Основные преимущества асинхронности:
       
       1. Отзывчивость пользовательского интерфейса
          - UI остается активным во время выполнения длительных операций
          - Пользователь может продолжать взаимодействие с приложением
       
       2. Эффективное использование ресурсов
          - Поток не блокируется в ожидании завершения I/O операций
          - Один поток может обрабатывать множество асинхронных операций
       
       3. Масштабируемость серверных приложений
          - Сервер может обрабатывать больше одновременных запросов
          - Меньше потоков = меньше накладных расходов
       
       Когда использовать асинхронность:
       - Операции ввода-вывода (файлы, сеть, база данных)
       - Длительные вычисления, которые можно распараллелить
       - Операции, которые могут занять неопределенное время
    */

    /*
       Ключевые слова async и await
       =========================
       
       async и await - это основа асинхронного программирования в C#.
       
       async:
       - Модификатор метода, указывающий, что метод содержит асинхронные операции
       - Позволяет использовать await внутри метода
       - Метод с async должен возвращать Task, Task<T> или void (только для обработчиков событий)
       
       Пример объявления асинхронного метода:
       
       public async Task<string> ReadFileAsync(string path)
       {
           // Асинхронный код
       }
       
       await:
       - Оператор, который приостанавливает выполнение метода до завершения асинхронной операции
       - Освобождает поток для выполнения других задач
       - Возвращает результат асинхронной операции
       - Может использоваться только внутри async методов
       
       Пример использования await:
       
       string content = await ReadFileAsync("file.txt");
       
       Важные правила:
       1. Методы с async должны содержать хотя бы один await (иначе предупреждение компилятора)
       2. await можно использовать только с "awaitable" типами (Task, Task<T>, и др.)
       3. Исключения в асинхронных методах передаются через возвращаемый Task
    */

    /*
       Класс Task и Task<T>
       ==================
       
       Task - это основной тип для представления асинхронных операций в .NET.
       
       Task:
       - Представляет асинхронную операцию, которая не возвращает значение
       - Аналог void для асинхронных методов
       
       Task<T>:
       - Представляет асинхронную операцию, которая возвращает значение типа T
       - Аналог T для асинхронных методов
       
       Основные методы для создания Task:
       
       1. Task.Run() - запуск кода в пуле потоков
          Task<int> task = Task.Run(() => ComputeValue());
       
       2. Task.FromResult() - создание уже завершенной задачи с результатом
          Task<string> task = Task.FromResult("Hello");
       
       3. Task.Delay() - асинхронная задержка
          await Task.Delay(1000); // Ждать 1 секунду
       
       4. Task.WhenAll() - ожидание завершения всех задач
          await Task.WhenAll(task1, task2, task3);
       
       5. Task.WhenAny() - ожидание завершения любой из задач
          Task completedTask = await Task.WhenAny(task1, task2, task3);
       
       Свойства Task:
       - IsCompleted - завершена ли задача
       - IsFaulted - завершилась ли задача с ошибкой
       - IsCanceled - была ли задача отменена
       - Result - результат выполнения (только для Task<T>, блокирует поток)
    */

    /*
       Основные принципы работы с асинхронными методами
       ========================================
       
       1. Соглашения об именовании:
          - Асинхронные методы должны заканчиваться на "Async"
          - Например: ReadFileAsync, SendEmailAsync, CalculateAsync
       
       2. Возвращаемые типы:
          - Task - для методов, которые не возвращают значение
          - Task<T> - для методов, которые возвращают значение типа T
          - void - только для обработчиков событий (избегайте в других случаях)
       
       3. ConfigureAwait:
          - По умолчанию await пытается вернуться в исходный контекст
          - В библиотеках используйте ConfigureAwait(false) для производительности
          - await SomeMethodAsync().ConfigureAwait(false);
       
       4. Избегайте async void:
          - Используйте async void только для обработчиков событий
          - В остальных случаях используйте async Task
          - async void усложняет обработку исключений и тестирование
       
       5. Не блокируйте асинхронный код:
          - Не используйте .Result или .Wait() в асинхронном контексте
          - Это может привести к deadlock
          - Используйте await вместо блокирующих вызовов
    */

    /*
       Обработка исключений в асинхронном коде
       ====================================
       
       Исключения в асинхронных методах обрабатываются особым образом:
       
       1. Исключения "упаковываются" в возвращаемый Task
       2. Исключение возникает при await или обращении к .Result
       3. Используйте try-catch вокруг await для обработки исключений
       
       Пример обработки исключений:
       
       try
       {
           string result = await SomeAsyncMethod();
       }
       catch (HttpRequestException ex)
       {
           // Обработка сетевой ошибки
       }
       catch (Exception ex)
       {
           // Обработка других ошибок
       }
       
       Особенности:
       - AggregateException используется только при блокирующих вызовах (.Result, .Wait())
       - При использовании await получаете оригинальное исключение
       - Необработанные исключения в async void могут завершить приложение
    */
    #endregion

    public static class AsyncBasicsLesson
    {
        public static async Task Main_()
        {
            Console.WriteLine("*** Урок 44: Основы асинхронности ***\n");

            // Пример 1: Простой асинхронный метод
            Console.WriteLine("--- Пример 1: Простой асинхронный метод ---");
            
            Console.WriteLine("Начало выполнения асинхронной операции...");
            
            string result = await SimpleAsyncMethod();
            Console.WriteLine($"Результат: {result}");
            
            Console.WriteLine();

            // Пример 2: Сравнение синхронного и асинхронного выполнения
            Console.WriteLine("--- Пример 2: Сравнение синхронного и асинхронного выполнения ---");
            
            // Синхронное выполнение
            Console.WriteLine("Синхронное выполнение:");
            var startTime = DateTime.Now;
            
            SynchronousOperation();
            SynchronousOperation();
            SynchronousOperation();
            
            var syncTime = DateTime.Now - startTime;
            Console.WriteLine($"Время синхронного выполнения: {syncTime.TotalMilliseconds} мс\n");
            
            // Асинхронное выполнение
            Console.WriteLine("Асинхронное выполнение:");
            startTime = DateTime.Now;
            
            Task task1 = AsynchronousOperation("Операция 1");
            Task task2 = AsynchronousOperation("Операция 2");
            Task task3 = AsynchronousOperation("Операция 3");
            
            await Task.WhenAll(task1, task2, task3);
            
            var asyncTime = DateTime.Now - startTime;
            Console.WriteLine($"Время асинхронного выполнения: {asyncTime.TotalMilliseconds} мс");
            
            Console.WriteLine();

            // Пример 3: Работа с Task<T>
            Console.WriteLine("--- Пример 3: Работа с Task<T> ---");
            
            // Запускаем несколько асинхронных вычислений
            Task<int> calculation1 = CalculateAsync(5, 1000);
            Task<int> calculation2 = CalculateAsync(10, 1500);
            Task<int> calculation3 = CalculateAsync(15, 800);
            
            Console.WriteLine("Вычисления запущены параллельно...");
            
            // Ждем завершения всех вычислений
            int[] results = await Task.WhenAll(calculation1, calculation2, calculation3);
            
            Console.WriteLine("Результаты вычислений:");
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine($"Вычисление {i + 1}: {results[i]}");
            }
            
            Console.WriteLine();

            // Пример 4: Task.WhenAny - ожидание первого завершения
            Console.WriteLine("--- Пример 4: Task.WhenAny - ожидание первого завершения ---");
            
            Task<string> fastTask = DelayedStringAsync("Быстрая задача", 1000);
            Task<string> slowTask = DelayedStringAsync("Медленная задача", 3000);
            Task<string> mediumTask = DelayedStringAsync("Средняя задача", 2000);
            
            Console.WriteLine("Ожидание завершения любой из задач...");
            
            Task<string> completedTask = await Task.WhenAny(fastTask, slowTask, mediumTask);
            string firstResult = await completedTask;
            
            Console.WriteLine($"Первая завершившаяся задача: {firstResult}");
            
            // Ждем завершения остальных задач для демонстрации
            Console.WriteLine("Ожидание завершения остальных задач...");
            await Task.WhenAll(fastTask, slowTask, mediumTask);
            Console.WriteLine("Все задачи завершены");
            
            Console.WriteLine();

            // Пример 5: Обработка исключений в асинхронном коде
            Console.WriteLine("--- Пример 5: Обработка исключений ---");
            
            // Метод, который может выбросить исключение
            try
            {
                await MethodThatMightThrowAsync();
                Console.WriteLine("Метод выполнился успешно");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Поймано исключение: {ex.Message}");
            }
            
            // Обработка исключений в нескольких задачах
            Console.WriteLine("\nОбработка исключений в нескольких задачах:");
            
            Task task1WithException = TaskThatThrowsAsync("Задача 1", false);
            Task task2WithException = TaskThatThrowsAsync("Задача 2", true);  // Эта выбросит исключение
            Task task3WithException = TaskThatThrowsAsync("Задача 3", false);
            
            try
            {
                await Task.WhenAll(task1WithException, task2WithException, task3WithException);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Одна из задач завершилась с ошибкой: {ex.Message}");
                
                // Проверяем статус каждой задачи
                CheckTaskStatus("Задача 1", task1WithException);
                CheckTaskStatus("Задача 2", task2WithException);
                CheckTaskStatus("Задача 3", task3WithException);
            }
            
            Console.WriteLine();

            // Пример 6: Использование CancellationToken
            Console.WriteLine("--- Пример 6: Отмена асинхронных операций ---");
            
            CancellationTokenSource cts = new CancellationTokenSource();
            
            // Запускаем длительную операцию
            Task longRunningTask = LongRunningOperationAsync(cts.Token);
            
            // Отменяем операцию через 2 секунды
            Task.Delay(2000).ContinueWith(_ => 
            {
                Console.WriteLine("Отмена операции...");
                cts.Cancel();
            });
            
            try
            {
                await longRunningTask;
                Console.WriteLine("Длительная операция завершена");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Операция была отменена");
            }
            
            Console.WriteLine("\nЗавершение урока по основам асинхронности.");
        }
        
        // Вспомогательные методы для демонстрации
        
        private static async Task<string> SimpleAsyncMethod()
        {
            Console.WriteLine("Выполняется асинхронная работа...");
            
            // Имитация асинхронной операции
            await Task.Delay(1000);
            
            Console.WriteLine("Асинхронная работа завершена");
            return "Результат асинхронной операции";
        }
        
        private static void SynchronousOperation()
        {
            Console.WriteLine($"Синхронная операция начата в потоке {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000); // Блокирующая операция
            Console.WriteLine($"Синхронная операция завершена в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
        
        private static async Task AsynchronousOperation(string name)
        {
            Console.WriteLine($"{name} начата в потоке {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(1000); // Неблокирующая операция
            Console.WriteLine($"{name} завершена в потоке {Thread.CurrentThread.ManagedThreadId}");
        }
        
        private static async Task<int> CalculateAsync(int input, int delayMs)
        {
            Console.WriteLine($"Начало вычисления для {input}...");
            
            // Имитация длительного вычисления
            await Task.Delay(delayMs);
            
            int result = input * input + input;
            Console.WriteLine($"Вычисление для {input} завершено: {result}");
            
            return result;
        }
        
        private static async Task<string> DelayedStringAsync(string message, int delayMs)
        {
            await Task.Delay(delayMs);
            return message;
        }
        
        private static async Task MethodThatMightThrowAsync()
        {
            await Task.Delay(500);
            
            // Случайно выбрасываем исключение
            Random random = new Random();
            if (random.Next(2) == 0)
            {
                throw new InvalidOperationException("Случайная ошибка в асинхронном методе");
            }
        }
        
        private static async Task TaskThatThrowsAsync(string taskName, bool shouldThrow)
        {
            Console.WriteLine($"{taskName} запущена");
            await Task.Delay(1000);
            
            if (shouldThrow)
            {
                throw new Exception($"Ошибка в {taskName}");
            }
            
            Console.WriteLine($"{taskName} завершена успешно");
        }
        
        private static void CheckTaskStatus(string taskName, Task task)
        {
            if (task.IsCompletedSuccessfully)
            {
                Console.WriteLine($"{taskName}: завершена успешно");
            }
            else if (task.IsFaulted)
            {
                Console.WriteLine($"{taskName}: завершена с ошибкой - {task.Exception?.InnerException?.Message}");
            }
            else if (task.IsCanceled)
            {
                Console.WriteLine($"{taskName}: была отменена");
            }
        }
        
        private static async Task LongRunningOperationAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Начало длительной операции...");
            
            for (int i = 0; i < 10; i++)
            {
                // Проверяем, не была ли запрошена отмена
                cancellationToken.ThrowIfCancellationRequested();
                
                Console.WriteLine($"Шаг {i + 1} из 10");
                await Task.Delay(1000, cancellationToken);
            }
            
            Console.WriteLine("Длительная операция завершена");
        }
    }

    #region Задачи
    /*
        # Создайте асинхронный метод для чтения нескольких файлов параллельно.
          Метод должен принимать массив путей к файлам и возвращать словарь,
          где ключ - имя файла, а значение - содержимое файла. Обработайте
          случаи, когда файл не существует или недоступен для чтения.
        
        # Реализуйте асинхронный метод для загрузки данных с нескольких веб-сайтов.
          Используйте HttpClient для выполнения HTTP-запросов. Метод должен
          возвращать результаты по мере их получения, а не ждать завершения всех запросов.
        
        # Напишите асинхронный метод для вычисления суммы квадратов чисел в диапазоне,
          который можно отменить с помощью CancellationToken. Метод должен периодически
          проверять токен отмены и корректно завершаться при получении запроса на отмену.
        
        # Создайте асинхронную версию метода поиска файлов в директории.
          Метод должен рекурсивно обходить поддиректории и возвращать список файлов,
          соответствующих заданному шаблону. Реализуйте возможность отмены операции
          и отчета о прогрессе выполнения.
    */
    #endregion
}
