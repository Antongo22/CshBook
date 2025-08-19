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
     * В этом уроке ты узнаешь об основах многопоточного программирования в C#:
     * 
     * - Что такое потоки и зачем они нужны
     * - Класс Thread и его основные свойства и методы
     * - Создание и запуск потоков
     * - Жизненный цикл потока
     * - Передача параметров в поток
     * - Фоновые и основные потоки
     * - Базовое управление потоками (ожидание, прерывание)
     */

    /*
       Что такое потоки и зачем они нужны
       ================================
       
       Поток выполнения (thread) - это наименьшая единица обработки, которой операционная
       система выделяет процессорное время. Каждый процесс имеет как минимум один поток 
       (главный поток), но может создавать и дополнительные потоки.
       
       Многопоточность позволяет программе выполнять несколько операций одновременно 
       (параллельно) или почти одновременно (конкурентно). Это особенно полезно для:
       
       1. Повышения отзывчивости пользовательского интерфейса
          - Длительные операции выполняются в отдельном потоке, чтобы UI не "зависал"
       
       2. Увеличения производительности на многоядерных процессорах
          - Распараллеливание вычислительно сложных задач на несколько ядер
       
       3. Выполнения асинхронных операций
          - Ввод/вывод (работа с файлами, сетью, базами данных)
          - Ожидание внешних событий
       
       4. Фоновой обработки данных
          - Периодические задачи, не блокирующие основной поток
    */

    /*
       Класс Thread и его основные свойства и методы
       ========================================
       
       Класс Thread из пространства имен System.Threading представляет собой поток 
       выполнения и предоставляет методы для управления им.
       
       Основные свойства класса Thread:
       
       - CurrentThread - статическое свойство, возвращающее текущий выполняющийся поток
       - IsAlive - показывает, выполняется ли поток в данный момент
       - IsBackground - определяет, является ли поток фоновым
       - ManagedThreadId - уникальный идентификатор потока
       - Name - имя потока (полезно для отладки)
       - Priority - приоритет потока (влияет на время, выделяемое ОС)
       - ThreadState - текущее состояние потока
       
       Основные методы класса Thread:
       
       - Start() - запускает выполнение потока
       - Join() - блокирует вызывающий поток до завершения этого потока
       - Sleep() - приостанавливает текущий поток на указанное время
       - Interrupt() - прерывает поток, находящийся в состоянии WaitSleepJoin
       - Abort() - принудительно завершает поток (устаревший и не рекомендуемый)
    */

    /*
       Создание и запуск потоков
       ======================
       
       Для создания нового потока используется конструктор класса Thread, 
       которому передается делегат ThreadStart (без параметров) или 
       ParameterizedThreadStart (с одним параметром типа object).
       
       Пример создания и запуска простого потока:
       
       Thread thread = new Thread(WorkerMethod);
       thread.Start();
       
       Где WorkerMethod - это метод, который будет выполняться в новом потоке:
       
       void WorkerMethod()
       {
           // Код, выполняемый в отдельном потоке
       }
    */

    /*
       Жизненный цикл потока
       =================
       
       Поток может находиться в одном из следующих состояний:
       
       1. Unstarted - поток создан, но метод Start() еще не вызван
       2. Running - поток выполняется
       3. WaitSleepJoin - поток заблокирован (Sleep, Wait или Join)
       4. Suspended - поток приостановлен (устаревшее)
       5. Stopped - поток завершил выполнение
       6. Aborted - поток принудительно завершен (устаревшее)
       
       Обычный жизненный цикл потока:
       
       Создание -> Запуск -> Выполнение -> Завершение
    */

    /*
       Передача параметров в поток
       =========================
       
       Для передачи данных в поток можно использовать:
       
       1. ParameterizedThreadStart делегат:
       
          Thread thread = new Thread(WorkerWithParam);
          thread.Start(parameter);
          
          void WorkerWithParam(object param)
          {
              // Используем переданный параметр
          }
       
       2. Замыкания (лямбда-выражения):
       
          int value = 10;
          Thread thread = new Thread(() => 
          {
              // Здесь доступна переменная value из внешней области
          });
          thread.Start();
       
       3. Специальные классы для передачи нескольких параметров:
       
          var parameters = new ThreadParams { Value1 = 1, Value2 = "text" };
          Thread thread = new Thread(WorkerWithComplexParam);
          thread.Start(parameters);
    */

    /*
       Фоновые и основные потоки
       ======================
       
       По умолчанию все потоки, созданные пользователем, являются основными (foreground).
       Программа не завершится, пока работает хотя бы один основной поток.
       
       Фоновые потоки (background) автоматически завершаются при выходе из программы,
       когда все основные потоки завершают работу. Это полезно для служебных задач,
       которые не должны блокировать завершение программы.
       
       Чтобы сделать поток фоновым:
       
       Thread thread = new Thread(Worker);
       thread.IsBackground = true;
       thread.Start();
    */

    /*
       Базовое управление потоками
       =======================
       
       1. Ожидание завершения потока:
       
          thread.Join(); // Блокирует текущий поток до завершения thread
          thread.Join(1000); // Ожидает завершения не более 1000 мс
       
       2. Приостановка выполнения текущего потока:
       
          Thread.Sleep(1000); // Останавливает текущий поток на 1 секунду
       
       3. Прерывание потока, находящегося в состоянии ожидания:
       
          thread.Interrupt(); // Генерирует ThreadInterruptedException в потоке
       
       4. Проверка, не запрошено ли прерывание:
       
          if (Thread.CurrentThread.IsAlive)
          {
              // Выполнить действия
          }
    */
    #endregion

    public class MultithreadingBasicsLesson
    {
        public void Main_()
        {
            Console.WriteLine("*** Урок 40: Основы многопоточности ***\n");

            // Пример 1: Создание и запуск простого потока
            Console.WriteLine("--- Пример 1: Создание и запуск потока ---");
            
            // Создаем новый поток, выполняющий метод SimpleThreadMethod
            Thread thread1 = new Thread(SimpleThreadMethod);
            thread1.Name = "ПростойПоток";
            
            Console.WriteLine("Основной поток: Запуск нового потока");
            thread1.Start(); // Запускаем поток
            
            // Выводим информацию из основного потока
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Основной поток: шаг {i}");
                Thread.Sleep(100); // Небольшая задержка
            }
            
            // Дожидаемся завершения дочернего потока
            thread1.Join();
            Console.WriteLine("Основной поток: дочерний поток завершился\n");

            // Пример 2: Получение информации о потоках
            Console.WriteLine("--- Пример 2: Информация о потоках ---");
            
            Thread currentThread = Thread.CurrentThread;
            currentThread.Name = "ГлавныйПоток";
            
            Console.WriteLine($"Текущий поток: ID = {currentThread.ManagedThreadId}, Имя = {currentThread.Name}");
            Console.WriteLine($"Приоритет: {currentThread.Priority}");
            Console.WriteLine($"Фоновый: {currentThread.IsBackground}");
            Console.WriteLine($"Состояние: {currentThread.ThreadState}\n");

            // Пример 3: Передача параметров в поток
            Console.WriteLine("--- Пример 3: Передача параметров в поток ---");
            
            // Вариант 1: Через ParameterizedThreadStart
            Thread paramThread = new Thread(ParameterizedThreadMethod);
            paramThread.Start(42);
            paramThread.Join();
            
            // Вариант 2: Через анонимный метод с замыканием
            string message = "Привет из потока с замыканием!";
            Thread closureThread = new Thread(() => 
            {
                Console.WriteLine(message);
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} получил сообщение через замыкание");
            });
            closureThread.Start();
            closureThread.Join();
            
            // Вариант 3: Через класс с параметрами
            var complexParam = new ThreadParameters 
            { 
                Id = 100, 
                Name = "Комплексный параметр", 
                Values = new List<int> { 1, 2, 3, 4, 5 } 
            };
            
            Thread complexThread = new Thread(ComplexParameterThreadMethod);
            complexThread.Start(complexParam);
            complexThread.Join();
            Console.WriteLine();

            // Пример 4: Фоновые потоки
            Console.WriteLine("--- Пример 4: Фоновые потоки ---");
            
            Thread backgroundThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Фоновый поток: шаг {i}");
                    Thread.Sleep(200);
                }
            });
            
            backgroundThread.IsBackground = true; // Делаем поток фоновым
            backgroundThread.Start();
            
            Console.WriteLine("Основной поток: запущен фоновый поток");
            Console.WriteLine("Основной поток: ожидаем 1 секунду...");
            Thread.Sleep(1000);
            Console.WriteLine("Основной поток: продолжаем работу");
            Console.WriteLine("Фоновый поток не завершится, но это нормально - он будет автоматически остановлен при завершении программы\n");

            // Пример 5: Управление выполнением потока
            Console.WriteLine("--- Пример 5: Управление выполнением потока ---");
            
            // Создаем и запускаем поток, который можем прервать
            Thread interruptibleThread = new Thread(InterruptibleMethod);
            interruptibleThread.Start();
            
            // Даем потоку немного поработать
            Thread.Sleep(1500);
            
            // Прерываем поток
            Console.WriteLine("Основной поток: прерывание дочернего потока");
            interruptibleThread.Interrupt();
            
            // Ожидаем завершения
            interruptibleThread.Join();
            Console.WriteLine("Основной поток: дочерний поток завершился после прерывания\n");

            // Пример 6: Измерение производительности многопоточного приложения
            Console.WriteLine("--- Пример 6: Производительность многопоточного приложения ---");
            
            // Выполнение задачи в одном потоке
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            long singleThreadSum = SumNumbersInRange(1, 10000000);
            
            stopwatch.Stop();
            Console.WriteLine($"Сумма чисел в одном потоке: {singleThreadSum}");
            Console.WriteLine($"Время выполнения в одном потоке: {stopwatch.ElapsedMilliseconds} мс");
            
            // Выполнение той же задачи в нескольких потоках
            stopwatch.Restart();
            
            // Разбиваем задачу на 4 части
            int rangeSize = 10000000 / 4;
            long[] partialSums = new long[4];
            Thread[] threads = new Thread[4];
            
            for (int i = 0; i < 4; i++)
            {
                int index = i;
                int start = index * rangeSize + 1;
                int end = (index == 3) ? 10000000 : (index + 1) * rangeSize;
                
                threads[i] = new Thread(() => 
                {
                    partialSums[index] = SumNumbersInRange(start, end);
                });
                
                threads[i].Start();
            }
            
            // Ожидаем завершения всех потоков
            foreach (Thread t in threads)
            {
                t.Join();
            }
            
            // Объединяем результаты
            long multiThreadSum = 0;
            foreach (long sum in partialSums)
            {
                multiThreadSum += sum;
            }
            
            stopwatch.Stop();
            Console.WriteLine($"Сумма чисел в 4 потоках: {multiThreadSum}");
            Console.WriteLine($"Время выполнения в 4 потоках: {stopwatch.ElapsedMilliseconds} мс");
            
            Console.WriteLine("\nЗавершение урока по основам многопоточности.");
        }
        
        // Метод, выполняемый в отдельном потоке
        private void SimpleThreadMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.Name}: шаг {i}");
                Thread.Sleep(200); // Имитация работы
            }
        }
        
        // Метод, принимающий один параметр
        private void ParameterizedThreadMethod(object param)
        {
            int value = (int)param;
            Console.WriteLine($"Поток с параметром получил значение: {value}");
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} выполняет работу с параметром");
        }
        
        // Метод, принимающий сложный параметр
        private void ComplexParameterThreadMethod(object param)
        {
            var parameters = (ThreadParameters)param;
            Console.WriteLine($"Поток получил комплексный параметр: Id={parameters.Id}, Name={parameters.Name}");
            Console.WriteLine($"Значения: {string.Join(", ", parameters.Values)}");
        }
        
        // Метод, который можно прервать
        private void InterruptibleMethod()
        {
            try
            {
                Console.WriteLine("Прерываемый поток: начало работы");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"Прерываемый поток: шаг {i}");
                    
                    // Потенциально прерываемый метод
                    Thread.Sleep(500);
                }
                Console.WriteLine("Прерываемый поток: нормальное завершение");
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("Прерываемый поток: был прерван!");
            }
        }
        
        // Метод для суммирования чисел в диапазоне
        private long SumNumbersInRange(int start, int end)
        {
            long sum = 0;
            for (int i = start; i <= end; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
    
    // Класс для передачи нескольких параметров в поток
    public class ThreadParameters
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Values { get; set; }
    }

    #region Задачи
    /*
        # Создайте программу, которая запускает два потока. Первый поток должен 
          вычислять сумму чисел от 1 до 1000, а второй - произведение чисел от 1 до 20.
          Выведите результаты работы потоков после их завершения.
        
        # Разработайте класс CountdownTimer, который отсчитывает время в отдельном потоке 
          от заданного значения до нуля, выводя текущее значение на консоль каждую секунду.
          Таймер должен иметь методы Start(), Stop() и Reset(). Добавьте в класс обработку 
          прерывания потока и корректное завершение работы.
        
        # Напишите программу для вычисления числа π методом Монте-Карло в многопоточном 
          режиме. Создайте несколько потоков, каждый из которых будет генерировать случайные 
          точки в квадрате со стороной 2 и центром в начале координат, и подсчитывать, 
          сколько точек попало в круг радиусом 1. Объедините результаты и вычислите 
          приближенное значение π.
        
        # Реализуйте симуляцию работы банкомата с использованием потоков. Создайте класс 
          BankAccount с методами Deposit и Withdraw. Запустите несколько потоков, которые 
          одновременно будут вызывать эти методы. Обратите внимание на возникновение 
          состояния гонки (race condition) и подумайте, как можно было бы решить эту 
          проблему (которую мы рассмотрим в следующем уроке о синхронизации потоков).
        
        # Напишите программу, которая создает "наблюдателя" в отдельном потоке. Этот 
          поток должен отслеживать создание, изменение и удаление файлов в указанной 
          директории и выводить информацию об этих событиях. Используйте класс 
          FileSystemWatcher из пространства имен System.IO.
    */
    #endregion
}
