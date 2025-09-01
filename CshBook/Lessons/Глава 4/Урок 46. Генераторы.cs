using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_4
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о генераторах в C#:
     * 
     * - Что такое генераторы и итераторы
     * - Ключевое слово yield
     * - Создание пользовательских итераторов
     * - Ленивые вычисления с генераторами
     * - Практические применения генераторов
     */

    /*
       Что такое генераторы и итераторы
       =============================
       
       Генератор (или итератор) - это специальный метод, который возвращает
       последовательность элементов по одному, а не все сразу.
       
       Основные особенности генераторов:
       
       1. Ленивое выполнение (Lazy Evaluation)
          - Элементы генерируются только когда они нужны
          - Экономия памяти при работе с большими последовательностями
          - Возможность создания бесконечных последовательностей
       
       2. Сохранение состояния
          - Генератор "запоминает" где он остановился
          - При следующем вызове продолжает с того же места
          - Локальные переменные сохраняются между вызовами
       
       3. Простота создания
          - Не нужно реализовывать интерфейсы IEnumerable/IEnumerator
          - Компилятор автоматически создает необходимый код
       
       Генераторы возвращают типы:
       - IEnumerable<T> - для методов без параметров
       - IEnumerator<T> - для более точного контроля итерации
    */

    /*
       Ключевое слово yield
       ==================
       
       yield - это специальное ключевое слово для создания генераторов.
       
       Два основных использования yield:
       
       1. yield return - возвращает элемент и приостанавливает выполнение
          
          yield return element;
          
          - Возвращает элемент вызывающему коду
          - Сохраняет текущее состояние метода
          - При следующем вызове продолжает выполнение после yield return
       
       2. yield break - завершает итерацию
          
          yield break;
          
          - Прекращает генерацию элементов
          - Аналог return в обычных методах
          - Используется для досрочного завершения итерации
       
       Ограничения yield:
       - Нельзя использовать в методах с ref/out параметрами
       - Нельзя использовать в анонимных методах и лямбда-выражениях
       - Нельзя использовать в блоках try-catch (но можно в try-finally)
       - Метод с yield не может возвращать void
       
       Пример простого генератора:
       
       public static IEnumerable<int> GetNumbers()
       {
           yield return 1;
           yield return 2;
           yield return 3;
       }
    */

    /*
       Создание пользовательских итераторов
       =================================
       
       Генераторы позволяют легко создавать пользовательские итераторы
       без необходимости реализации интерфейсов IEnumerable и IEnumerator.
       
       Примеры пользовательских итераторов:
       
       1. Генератор диапазона чисел:
          
          public static IEnumerable<int> Range(int start, int count)
          {
              for (int i = 0; i < count; i++)
              {
                  yield return start + i;
              }
          }
       
       2. Генератор последовательности Фибоначчи:
          
          public static IEnumerable<int> Fibonacci()
          {
              int a = 0, b = 1;
              while (true)
              {
                  yield return a;
                  int temp = a + b;
                  a = b;
                  b = temp;
              }
          }
       
       3. Генератор с условиями:
          
          public static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> predicate)
          {
              foreach (T item in source)
              {
                  if (predicate(item))
                      yield return item;
              }
          }
    */

    /*
       Ленивые вычисления с генераторами
       ==============================
       
       Ленивые вычисления (Lazy Evaluation) - это стратегия выполнения,
       при которой вычисления откладываются до момента, когда результат
       действительно нужен.
       
       Преимущества ленивых вычислений:
       
       1. Экономия памяти
          - Не нужно хранить всю последовательность в памяти
          - Особенно важно для больших наборов данных
       
       2. Экономия времени
          - Вычисления выполняются только для нужных элементов
          - Можно прервать итерацию в любой момент
       
       3. Композиция операций
          - Можно объединять несколько генераторов
          - Создание цепочек обработки данных
       
       Пример ленивых вычислений:
       
       var numbers = GenerateNumbers(1000000); // Генератор не выполняется
       var evenNumbers = numbers.Where(x => x % 2 == 0); // Тоже не выполняется
       var first10 = evenNumbers.Take(10); // И это не выполняется
       
       // Вычисления начинаются только здесь:
       foreach (var num in first10)
       {
           Console.WriteLine(num); // Только сейчас генерируются числа
       }
    */

    /*
       Практические применения генераторов
       ===============================
       
       Генераторы полезны во многих сценариях:
       
       1. Обработка больших файлов
          - Чтение файла по строкам без загрузки всего файла в память
          - Обработка CSV, логов, текстовых данных
       
       2. Математические последовательности
          - Простые числа, числа Фибоначчи, факториалы
          - Бесконечные последовательности
       
       3. Обход структур данных
          - Обход деревьев, графов
          - Рекурсивный поиск в файловой системе
       
       4. Пайплайны обработки данных
          - Цепочки трансформаций данных
          - Фильтрация, преобразование, агрегация
       
       5. Тестирование
          - Генерация тестовых данных
          - Создание различных сценариев для тестов
       
       6. Алгоритмы поиска
          - Поиск в глубину, поиск в ширину
          - Генерация возможных решений
    */
    #endregion

    public static class GeneratorsLesson
    {
        public static void Main_()
        {
            Console.WriteLine("*** Урок 45: Генераторы ***\n");

            // Пример 1: Простой генератор чисел
            Console.WriteLine("--- Пример 1: Простой генератор чисел ---");
            
            Console.WriteLine("Генерация чисел от 1 до 5:");
            foreach (int number in SimpleNumberGenerator())
            {
                Console.WriteLine($"Число: {number}");
            }
            
            Console.WriteLine();

            // Пример 2: Генератор диапазона с параметрами
            Console.WriteLine("--- Пример 2: Генератор диапазона ---");
            
            Console.WriteLine("Четные числа от 10 до 20:");
            foreach (int number in Range(10, 6).Where(x => x % 2 == 0))
            {
                Console.WriteLine($"Четное число: {number}");
            }
            
            Console.WriteLine();

            // Пример 3: Бесконечная последовательность Фибоначчи
            Console.WriteLine("--- Пример 3: Последовательность Фибоначчи ---");
            
            Console.WriteLine("Первые 10 чисел Фибоначчи:");
            foreach (long fibNumber in Fibonacci().Take(10))
            {
                Console.WriteLine($"Фибоначчи: {fibNumber}");
            }
            
            Console.WriteLine();

            // Пример 4: Генератор простых чисел
            Console.WriteLine("--- Пример 4: Генератор простых чисел ---");
            
            Console.WriteLine("Первые 10 простых чисел:");
            foreach (int prime in PrimeNumbers().Take(10))
            {
                Console.WriteLine($"Простое число: {prime}");
            }
            
            Console.WriteLine();

            // Пример 5: Генератор с условным завершением
            Console.WriteLine("--- Пример 5: Генератор с условным завершением ---");
            
            Console.WriteLine("Случайные числа до первого четного:");
            foreach (int randomNum in RandomNumbersUntilEven())
            {
                Console.WriteLine($"Случайное число: {randomNum}");
            }
            
            Console.WriteLine();

            // Пример 6: Композиция генераторов
            Console.WriteLine("--- Пример 6: Композиция генераторов ---");
            
            var processedNumbers = Range(1, 20)
                .Where(x => x % 3 == 0)  // Числа, кратные 3
                .Select(x => x * x)      // Возводим в квадрат
                .Take(5);                // Берем первые 5
            
            Console.WriteLine("Квадраты первых 5 чисел, кратных 3:");
            foreach (int number in processedNumbers)
            {
                Console.WriteLine($"Результат: {number}");
            }
            
            Console.WriteLine();

            // Пример 7: Генератор для обхода дерева
            Console.WriteLine("--- Пример 7: Обход дерева ---");
            
            // Создаем простое дерево
            var root = new TreeNode<string>("Root")
            {
                Children = new List<TreeNode<string>>
                {
                    new TreeNode<string>("Child1")
                    {
                        Children = new List<TreeNode<string>>
                        {
                            new TreeNode<string>("Grandchild1"),
                            new TreeNode<string>("Grandchild2")
                        }
                    },
                    new TreeNode<string>("Child2"),
                    new TreeNode<string>("Child3")
                    {
                        Children = new List<TreeNode<string>>
                        {
                            new TreeNode<string>("Grandchild3")
                        }
                    }
                }
            };
            
            Console.WriteLine("Обход дерева в глубину:");
            foreach (string value in TraverseTreeDepthFirst(root))
            {
                Console.WriteLine($"Узел: {value}");
            }
            
            Console.WriteLine();

            // Пример 8: Генератор пакетов (батчей)
            Console.WriteLine("--- Пример 8: Разбиение на пакеты ---");
            
            var numbers = Range(1, 15);
            Console.WriteLine("Разбиение чисел 1-15 на пакеты по 4:");
            
            int batchNumber = 1;
            foreach (var batch in Batch(numbers, 4))
            {
                Console.WriteLine($"Пакет {batchNumber}: [{string.Join(", ", batch)}]");
                batchNumber++;
            }
            
            Console.WriteLine();

            // Пример 9: Демонстрация ленивых вычислений
            Console.WriteLine("--- Пример 9: Ленивые вычисления ---");
            
            Console.WriteLine("Создание генератора (вычисления еще не начались)...");
            var lazySequence = ExpensiveCalculations();
            
            Console.WriteLine("Генератор создан. Начинаем итерацию:");
            foreach (int result in lazySequence.Take(3))
            {
                Console.WriteLine($"Получен результат: {result}");
            }
            
            Console.WriteLine("Итерация завершена. Остальные вычисления не выполнялись.");
            
            Console.WriteLine("\nЗавершение урока по генераторам.");
        }
        
        // Вспомогательные методы-генераторы
        
        private static IEnumerable<int> SimpleNumberGenerator()
        {
            Console.WriteLine("Генератор: начало работы");
            
            yield return 1;
            Console.WriteLine("Генератор: возвращено 1");
            
            yield return 2;
            Console.WriteLine("Генератор: возвращено 2");
            
            yield return 3;
            Console.WriteLine("Генератор: возвращено 3");
            
            yield return 4;
            Console.WriteLine("Генератор: возвращено 4");
            
            yield return 5;
            Console.WriteLine("Генератор: возвращено 5");
            
            Console.WriteLine("Генератор: завершение работы");
        }
        
        private static IEnumerable<int> Range(int start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return start + i;
            }
        }
        
        private static IEnumerable<long> Fibonacci()
        {
            long a = 0, b = 1;
            
            while (true) // Бесконечная последовательность
            {
                yield return a;
                
                long temp = a + b;
                a = b;
                b = temp;
            }
        }
        
        private static IEnumerable<int> PrimeNumbers()
        {
            yield return 2; // Первое простое число
            
            int candidate = 3;
            while (true)
            {
                if (IsPrime(candidate))
                {
                    yield return candidate;
                }
                candidate += 2; // Проверяем только нечетные числа
            }
        }
        
        private static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            
            int sqrt = (int)Math.Sqrt(number);
            for (int i = 3; i <= sqrt; i += 2)
            {
                if (number % i == 0) return false;
            }
            
            return true;
        }
        
        private static IEnumerable<int> RandomNumbersUntilEven()
        {
            Random random = new Random();
            
            while (true)
            {
                int number = random.Next(1, 20);
                yield return number;
                
                if (number % 2 == 0)
                {
                    Console.WriteLine("Найдено четное число, завершаем генерацию");
                    yield break; // Завершаем итерацию
                }
            }
        }
        
        private static IEnumerable<string> TraverseTreeDepthFirst<T>(TreeNode<T> node)
        {
            if (node == null) yield break;
            
            yield return node.Value.ToString();
            
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    foreach (var descendant in TraverseTreeDepthFirst(child))
                    {
                        yield return descendant;
                    }
                }
            }
        }
        
        private static IEnumerable<IEnumerable<T>> Batch<T>(IEnumerable<T> source, int batchSize)
        {
            var batch = new List<T>(batchSize);
            
            foreach (T item in source)
            {
                batch.Add(item);
                
                if (batch.Count == batchSize)
                {
                    yield return batch;
                    batch = new List<T>(batchSize);
                }
            }
            
            // Возвращаем последний неполный пакет, если он есть
            if (batch.Count > 0)
            {
                yield return batch;
            }
        }
        
        private static IEnumerable<int> ExpensiveCalculations()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Выполняется дорогостоящее вычисление #{i}...");
                
                // Имитация сложного вычисления
                System.Threading.Thread.Sleep(200);
                
                yield return i * i;
            }
        }
    }
    
    // Вспомогательный класс для демонстрации обхода дерева
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }
        
        public TreeNode(T value)
        {
            Value = value;
            Children = new List<TreeNode<T>>();
        }
    }

    #region Задачи
    /*
        # Создайте генератор, который читает большой текстовый файл построчно
          и возвращает только строки, содержащие определенное ключевое слово.
          Генератор должен быть эффективным по памяти и не загружать весь файл сразу.
        
        # Реализуйте генератор для создания всех возможных комбинаций элементов
          из нескольких коллекций (декартово произведение). Например, для коллекций
          [1,2] и ['a','b'] должны генерироваться пары (1,'a'), (1,'b'), (2,'a'), (2,'b').
        
        # Напишите генератор, который создает последовательность дат между двумя
          заданными датами с определенным интервалом (дни, недели, месяцы).
          Добавьте возможность пропускать выходные дни и праздники.
        
        # Создайте генератор для обхода файловой системы, который рекурсивно
          возвращает все файлы в директории и поддиректориях, соответствующие
          заданному шаблону (например, *.txt). Реализуйте возможность ограничения
          глубины поиска и исключения определенных директорий.
        
        # Реализуйте генератор паролей, который создает пароли заданной длины
          с различными требованиями (заглавные буквы, цифры, специальные символы).
          Генератор должен гарантировать, что каждый пароль уникален в рамках сессии.
    */
    #endregion
}
