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
     * В этом уроке ты узнаешь о параллельном программировании с использованием:
     * 
     * - Класса Parallel для распараллеливания выполнения кода
     * - PLINQ (Parallel LINQ) для параллельной обработки коллекций
     * - Особенностей и лучших практик параллельного программирования
     * - Методов измерения производительности параллельного кода
     */

    /*
       Класс Parallel
       =============
       
       Класс Parallel из пространства имен System.Threading.Tasks предоставляет 
       высокоуровневые методы для распараллеливания кода. Эти методы разбивают
       работу на части и выполняют их параллельно в пуле потоков.
       
       Основные методы класса Parallel:
       
       1. Parallel.For - параллельный аналог цикла for
          
          Parallel.For(0, n, i => {
              // Код, выполняемый параллельно для каждого i
          });
       
       2. Parallel.ForEach - параллельный аналог цикла foreach
          
          Parallel.ForEach(коллекция, элемент => {
              // Код, выполняемый параллельно для каждого элемента
          });
       
       3. Parallel.Invoke - параллельное выполнение нескольких методов
          
          Parallel.Invoke(
              () => Метод1(),
              () => Метод2(),
              () => Метод3()
          );
       
       Все методы Parallel имеют перегрузки, позволяющие контролировать 
       степень параллелизма, обрабатывать исключения, использовать локальные 
       переменные для каждого потока и т.д.
    */

    /*
       Параметры и опции класса Parallel
       ==============================
       
       Для тонкой настройки параллельного выполнения можно использовать 
       класс ParallelOptions:
       
       ParallelOptions options = new ParallelOptions {
           MaxDegreeOfParallelism = 4, // Ограничение количества используемых потоков
           CancellationToken = token,  // Токен отмены для прерывания выполнения
           TaskScheduler = scheduler   // Планировщик задач для выполнения операций
       };
       
       Parallel.For(0, n, options, i => {
           // Код
       });
       
       Для сбора результатов из параллельных операций можно использовать:
       
       1. Для Parallel.For:
          
          // Инициализация локальных переменных в каждом потоке
          // Выполнение итераций
          // Объединение результатов после всех итераций
          
          long sum = 0;
          Parallel.For(0, n,
              () => 0L,                          // Инициализация для каждого потока
              (i, state, localSum) => localSum + i, // Накопление в локальной переменной
              localSum => Interlocked.Add(ref sum, localSum) // Слияние результатов
          );
       
       2. Для Parallel.ForEach аналогично.
    */

    /*
       PLINQ (Parallel LINQ)
       ===================
       
       PLINQ - это параллельная реализация LINQ, позволяющая выполнять
       запросы к коллекциям параллельно на нескольких процессорах.
       
       Основная идея PLINQ - распределение обработки элементов коллекции
       между несколькими потоками для повышения производительности.
       
       Для использования PLINQ достаточно добавить метод .AsParallel()
       в цепочку LINQ-запроса:
       
       var result = from item in collection.AsParallel()
                    where Predicate(item)
                    select Transform(item);
       
       // или с методами расширения
       var result = collection.AsParallel()
                   .Where(item => Predicate(item))
                   .Select(item => Transform(item));
       
       Особенности PLINQ:
       
       1. Сохранение порядка элементов:
          
          // По умолчанию PLINQ не гарантирует сохранение порядка элементов
          // Для сохранения порядка используется AsOrdered()
          
          var result = collection.AsParallel().AsOrdered()
                      .Where(predicate)
                      .Select(transform);
       
       2. Управление степенью параллелизма:
          
          var result = collection.AsParallel()
                      .WithDegreeOfParallelism(4) // Использовать максимум 4 потока
                      .Where(predicate);
       
       3. Отмена выполнения:
          
          CancellationTokenSource cts = new CancellationTokenSource();
          
          try {
              var result = collection.AsParallel()
                          .WithCancellation(cts.Token)
                          .Where(predicate)
                          .ToArray();
          }
          catch (OperationCanceledException) {
              Console.WriteLine("Запрос был отменен");
          }
          
          // Для отмены запроса
          cts.Cancel();
    */

    /*
       Когда использовать параллельное программирование
       =========================================
       
       Параллельное программирование наиболее эффективно, когда:
       
       1. Задача поддается распараллеливанию (делится на независимые подзадачи)
       2. Задача требует большого объема вычислений (CPU-bound)
       3. Есть многоядерный процессор с достаточным количеством свободных ресурсов
       4. Коллекция содержит много элементов (для Parallel.For/ForEach и PLINQ)
       5. Обработка каждого элемента занимает значительное время
       
       Параллельное программирование может быть менее эффективно или даже снизить
       производительность, когда:
       
       1. Задачи сильно зависят друг от друга (высокая связность)
       2. Задачи в основном ожидают ввода-вывода (I/O-bound)
       3. Количество элементов для обработки невелико
       4. Время обработки каждого элемента очень мало
       5. Есть большая потребность в синхронизации между потоками
    */

    /*
       Лучшие практики параллельного программирования
       ========================================
       
       1. Измеряйте производительность
          - Не предполагайте, что параллельный код всегда быстрее
          - Проводите тестирование на реальных данных
       
       2. Избегайте гонок за данными (race conditions)
          - Используйте потокобезопасные коллекции
          - Применяйте блокировки при необходимости
          - Используйте Interlocked для атомарных операций
       
       3. Выбирайте правильный уровень гранулярности
          - Слишком мелкие задачи → накладные расходы на планирование потоков
          - Слишком крупные задачи → неравномерная нагрузка на процессоры
       
       4. Учитывайте накладные расходы
          - Создание задач имеет свою стоимость
          - Синхронизация между потоками снижает эффективность параллелизма
       
       5. Используйте локальные переменные для уменьшения конкуренции
          - Каждый поток должен иметь свою копию данных, когда это возможно
          - Объединяйте результаты только в конце выполнения
       
       6. Обрабатывайте исключения правильно
          - Параллельные операции могут генерировать AggregateException
          - Проверяйте внутренние исключения
    */

    /*
       Измерение производительности параллельного кода
       =========================================
       
       Для оценки эффективности параллельного кода используют следующие метрики:
       
       1. Ускорение (Speedup):
          
          Speedup = T1 / Tn
          
          где T1 - время выполнения последовательного кода
              Tn - время выполнения параллельного кода на n ядрах
       
       2. Эффективность (Efficiency):
          
          Efficiency = Speedup / n
          
          где n - количество используемых ядер/потоков
       
       3. Закон Амдала:
          
          Максимальное теоретическое ускорение ограничено долей
          кода, который можно распараллелить:
          
          MaxSpeedup = 1 / (s + p/n)
          
          где s - доля последовательного кода
              p - доля параллельного кода (s + p = 1)
              n - количество процессоров/потоков
       
       4. Практические методы измерения:
          
          - Используйте класс Stopwatch для точного измерения времени
          - Проводите несколько запусков для усреднения результатов
          - Учитывайте время разогрева (JIT-компиляция и т.д.)
          - Тестируйте на реалистичных наборах данных
    */
    #endregion

    public static class ParallelProgrammingLesson
    {
        public static void Main_()
        {
            Console.WriteLine("*** Урок 46: Параллельное программирование ***\n");

            // Пример 1: Сравнение последовательного и параллельного цикла
            Console.WriteLine("--- Пример 1: Сравнение последовательного и параллельного цикла ---");
            
            // Количество элементов для обработки
            int count = 10000000;
            
            // Последовательное вычисление
            Stopwatch sw = Stopwatch.StartNew();
            
            double sequentialSum = 0;
            for (int i = 0; i < count; i++)
            {
                sequentialSum += Math.Sqrt(i);
            }
            
            sw.Stop();
            Console.WriteLine($"Последовательное вычисление: {sw.ElapsedMilliseconds} мс, Результат: {sequentialSum:E}");
            
            // Параллельное вычисление
            sw.Restart();
            
            double parallelSum = 0;
            object lockObj = new object(); // Объект для синхронизации
            
            Parallel.For(0, count, i =>
            {
                double temp = Math.Sqrt(i);
                
                // Синхронизация доступа к общей переменной
                lock (lockObj)
                {
                    parallelSum += temp;
                }
            });
            
            sw.Stop();
            Console.WriteLine($"Параллельное вычисление с блокировкой: {sw.ElapsedMilliseconds} мс, Результат: {parallelSum:E}");
            
            // Параллельное вычисление с локальными переменными
            sw.Restart();
            
            double optimizedParallelSum = 0;
            
            Parallel.For<double>(0, count,
                () => 0.0, // Инициализация локальной переменной для каждого потока
                (i, state, localSum) => localSum + Math.Sqrt(i), // Локальное накопление
                localSum => // Объединение результатов
                {
                    lock (lockObj)
                    {
                        optimizedParallelSum += localSum;
                    }
                });
            
            sw.Stop();
            Console.WriteLine($"Параллельное вычисление с локальными переменными: {sw.ElapsedMilliseconds} мс, Результат: {optimizedParallelSum:E}");
            
            Console.WriteLine();

            // Пример 2: Parallel.ForEach для обработки коллекций
            Console.WriteLine("--- Пример 2: Parallel.ForEach для обработки коллекций ---");
            
            List<string> texts = new List<string>
            {
                "Параллельное программирование",
                "многопоточность",
                "синхронизация",
                "потокобезопасность",
                "пул потоков",
                "асинхронные операции",
                "Task Parallel Library",
                "многоядерная обработка",
                "распределённые вычисления",
                "балансировка нагрузки"
            };
            
            // Создадим словарь для подсчета статистики
            Dictionary<char, int> letterCount = new Dictionary<char, int>();
            for (char c = 'а'; c <= 'я'; c++)
            {
                letterCount[c] = 0;
            }
            letterCount['ё'] = 0;
            
            // Последовательная обработка
            sw.Restart();
            
            foreach (var text in texts)
            {
                foreach (char c in text.ToLower())
                {
                    if (letterCount.ContainsKey(c))
                    {
                        letterCount[c]++;
                    }
                }
            }
            
            sw.Stop();
            Console.WriteLine($"Последовательный подсчет букв: {sw.ElapsedMilliseconds} мс");
            
            // Вывод топ-5 самых частых букв
            var topLetters = letterCount.OrderByDescending(pair => pair.Value).Take(5);
            Console.WriteLine("Топ-5 самых частых букв:");
            foreach (var pair in topLetters)
            {
                Console.WriteLine($"'{pair.Key}': {pair.Value} раз");
            }
            
            // Сбросим статистику
            foreach (var key in letterCount.Keys.ToList())
            {
                letterCount[key] = 0;
            }
            
            // Параллельная обработка с синхронизацией
            sw.Restart();
            
            Parallel.ForEach(texts, text =>
            {
                Dictionary<char, int> localCounts = new Dictionary<char, int>();
                
                // Инициализируем локальный словарь
                foreach (char c in letterCount.Keys)
                {
                    localCounts[c] = 0;
                }
                
                // Подсчет в локальном словаре
                foreach (char c in text.ToLower())
                {
                    if (localCounts.ContainsKey(c))
                    {
                        localCounts[c]++;
                    }
                }
                
                // Объединение результатов
                lock (letterCount)
                {
                    foreach (var pair in localCounts)
                    {
                        letterCount[pair.Key] += pair.Value;
                    }
                }
            });
            
            sw.Stop();
            Console.WriteLine($"\nПараллельный подсчет букв: {sw.ElapsedMilliseconds} мс");
            
            // Вывод топ-5 самых частых букв
            topLetters = letterCount.OrderByDescending(pair => pair.Value).Take(5);
            Console.WriteLine("Топ-5 самых частых букв:");
            foreach (var pair in topLetters)
            {
                Console.WriteLine($"'{pair.Key}': {pair.Value} раз");
            }
            
            Console.WriteLine();

            // Пример 3: Parallel.Invoke для запуска нескольких задач
            Console.WriteLine("--- Пример 3: Parallel.Invoke для запуска нескольких задач ---");
            
            sw.Restart();
            
            Parallel.Invoke(
                () => {
                    Console.WriteLine($"Задача 1 запущена в потоке {Thread.CurrentThread.ManagedThreadId}");
                    // Имитация длительной работы
                    Thread.Sleep(1000);
                    Console.WriteLine("Задача 1 завершена");
                },
                () => {
                    Console.WriteLine($"Задача 2 запущена в потоке {Thread.CurrentThread.ManagedThreadId}");
                    // Имитация длительной работы
                    Thread.Sleep(2000);
                    Console.WriteLine("Задача 2 завершена");
                },
                () => {
                    Console.WriteLine($"Задача 3 запущена в потоке {Thread.CurrentThread.ManagedThreadId}");
                    // Имитация длительной работы
                    Thread.Sleep(1500);
                    Console.WriteLine("Задача 3 завершена");
                }
            );
            
            sw.Stop();
            Console.WriteLine($"Выполнение Parallel.Invoke: {sw.ElapsedMilliseconds} мс");
            Console.WriteLine("Все задачи завершены параллельно");
            
            Console.WriteLine();

            // Пример 4: Использование ParallelOptions
            Console.WriteLine("--- Пример 4: Использование ParallelOptions ---");
            
            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2 // Используем половину доступных ядер
            };
            
            Console.WriteLine($"Доступно ядер процессора: {Environment.ProcessorCount}");
            Console.WriteLine($"Устанавливаем ограничение: {options.MaxDegreeOfParallelism} потоков");
            
            sw.Restart();
            
            Parallel.For(0, 10, options, i =>
            {
                Console.WriteLine($"Итерация {i} выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(500); // Имитация работы
            });
            
            sw.Stop();
            Console.WriteLine($"Выполнение с ограниченным параллелизмом: {sw.ElapsedMilliseconds} мс");
            
            Console.WriteLine();

            // Пример 5: PLINQ для параллельной обработки коллекций
            Console.WriteLine("--- Пример 5: PLINQ для параллельной обработки коллекций ---");
            
            // Создаем большую коллекцию данных для обработки
            List<int> numbers = new List<int>();
            for (int i = 0; i < 10000000; i++)
            {
                numbers.Add(i);
            }
            
            // Последовательный LINQ-запрос
            sw.Restart();
            
            var evenSquaresLinq = numbers
                .Where(n => n % 2 == 0)
                .Select(n => n * n)
                .Take(5)
                .ToList();
            
            sw.Stop();
            Console.WriteLine($"Последовательный LINQ: {sw.ElapsedMilliseconds} мс");
            
            Console.WriteLine("Результаты (первые 5 элементов):");
            foreach (var num in evenSquaresLinq)
            {
                Console.WriteLine(num);
            }
            
            // Параллельный PLINQ-запрос
            sw.Restart();
            
            var evenSquaresPlinq = numbers
                .AsParallel()
                .Where(n => n % 2 == 0)
                .Select(n => n * n)
                .Take(5)
                .ToList();
            
            sw.Stop();
            Console.WriteLine($"\nPLINQ без сохранения порядка: {sw.ElapsedMilliseconds} мс");
            
            Console.WriteLine("Результаты (первые 5 элементов, порядок может отличаться):");
            foreach (var num in evenSquaresPlinq)
            {
                Console.WriteLine(num);
            }
            
            // PLINQ с сохранением порядка
            sw.Restart();
            
            var orderedEvenSquaresPlinq = numbers
                .AsParallel()
                .AsOrdered() // Сохраняем порядок элементов
                .Where(n => n % 2 == 0)
                .Select(n => n * n)
                .Take(5)
                .ToList();
            
            sw.Stop();
            Console.WriteLine($"\nPLINQ с сохранением порядка: {sw.ElapsedMilliseconds} мс");
            
            Console.WriteLine("Результаты (первые 5 элементов, порядок сохранен):");
            foreach (var num in orderedEvenSquaresPlinq)
            {
                Console.WriteLine(num);
            }
            
            Console.WriteLine();

            // Пример 6: Управление степенью параллелизма в PLINQ
            Console.WriteLine("--- Пример 6: Управление степенью параллелизма в PLINQ ---");
            
            // PLINQ с ограничением параллелизма
            sw.Restart();
            
            var customParallelismResult = numbers
                .AsParallel()
                .WithDegreeOfParallelism(2) // Используем только 2 потока
                .Where(n => n % 2 == 0 && IsPrime(n))
                .Take(5)
                .ToList();
            
            sw.Stop();
            Console.WriteLine($"PLINQ с 2 потоками: {sw.ElapsedMilliseconds} мс");
            
            Console.WriteLine("Первые 5 четных простых чисел:");
            foreach (var num in customParallelismResult)
            {
                Console.WriteLine(num);
            }
            
            Console.WriteLine();

            // Пример 7: Обработка исключений в параллельном коде
            Console.WriteLine("--- Пример 7: Обработка исключений в параллельном коде ---");
            
            try
            {
                Parallel.For(0, 10, i =>
                {
                    Console.WriteLine($"Обработка элемента {i}");
                    
                    // Искусственно генерируем исключение для нескольких итераций
                    if (i == 3 || i == 7)
                    {
                        throw new Exception($"Ошибка при обработке элемента {i}");
                    }
                });
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"\nПоймано {ae.InnerExceptions.Count} исключений:");
                
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine($"- {ex.Message}");
                }
            }
            
            // PLINQ и исключения
            Console.WriteLine("\nОбработка исключений в PLINQ:");
            
            try
            {
                var result = Enumerable.Range(0, 10)
                    .AsParallel()
                    .Select(i =>
                    {
                        if (i == 5)
                        {
                            throw new InvalidOperationException($"Ошибка в элементе {i}");
                        }
                        return i * 2;
                    })
                    .ToArray(); // Здесь могут возникнуть исключения
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Поймано исключение в PLINQ: {ae.InnerException.Message}");
            }
            
            Console.WriteLine("\nЗавершение урока по параллельному программированию.");
        }
        
        // Вспомогательный метод для проверки простого числа
        private static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;
            
            int i = 5;
            while (i * i <= number)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
                i += 6;
            }
            return true;
        }
    }

    #region Задачи
    /*
        # Реализуйте параллельный алгоритм перемножения матриц с использованием класса 
          Parallel. Сравните производительность последовательного и параллельного 
          алгоритмов для матриц разного размера. Для измерения времени используйте 
          класс Stopwatch.
        
        # Напишите программу, которая с помощью PLINQ находит в большой коллекции 
          целых чисел (от 1 до 10 миллионов) все числа, которые:
          - Являются простыми
          - Сумма их цифр делится на 7
          Сравните производительность последовательного LINQ и PLINQ для этой задачи.
        
        # Создайте программу для параллельной обработки текстовых файлов, которая:
          - Считывает текст из нескольких файлов
          - Разбивает текст на слова и приводит их к нижнему регистру
          - Подсчитывает частоту встречаемости каждого слова
          - Выводит топ-20 самых часто встречающихся слов
          Используйте PLINQ и локальное накопление результатов для повышения 
          эффективности.
        
        # Реализуйте параллельный алгоритм сортировки слиянием (Merge Sort) 
          с использованием класса Parallel. Сортировка должна разделять массив 
          на части и сортировать их параллельно, а затем объединять результаты. 
          Сравните производительность с последовательной сортировкой.
        
        # Напишите программу для параллельной обработки изображений, которая:
          - Загружает несколько изображений
          - Применяет к ним различные фильтры (например, оттенки серого, инверсия цветов)
          - Сохраняет обработанные изображения в новую директорию
          Используйте Parallel.ForEach с настройкой степени параллелизма в зависимости
          от количества ядер процессора.
    */
    #endregion
}
