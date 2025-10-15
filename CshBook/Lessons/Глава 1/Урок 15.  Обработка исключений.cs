using System;
using System.IO;

namespace CshBook.Lessons
{
    /* Урок 15: Обработка исключений в C#
    
       Исключения (Exceptions) – это особые ситуации, возникающие во время выполнения программы, 
       которые могут привести к её аварийному завершению, если их не обработать.

       В C# для работы с исключениями используется механизм try-catch-finally.
       
       Зачем нужна обработка исключений?
       - Позволяет перехватывать ошибки и предотвращать аварийное завершение программы.
       - Упрощает отладку, предоставляя информацию об ошибке.
       - Позволяет корректно завершать работу (например, закрывать файлы или соединения).
    */

    /* Основные конструкции:
       - try         – блок, в котором выполняется код, который может вызвать исключение.
       - catch       – блок, перехватывающий исключение и обрабатывающий его.
       - finally     – блок, который выполняется в любом случае (даже если исключение не произошло).
       - throw       – оператор для явного вызова исключения.
    */

    /* Как правильно располагать блоки catch?

       - Специфичные исключения (например, `FormatException`, `IndexOutOfRangeException`)  
         должны идти вверху.
       - Общие исключения (Exception) должны быть в самом низу.  

       Ошибка: 
       ```
       try
       {
           // Код, который может вызвать исключение
       }
       catch (Exception ex)  // Ловит ВСЕ исключения
       {
           Console.WriteLine("Общая ошибка");
       }
       catch (FormatException ex) // Этот код НИКОГДА не выполнится!
       {
           Console.WriteLine("Ошибка формата");
       }
       ```

       Правильный порядок:
       ```
       try
       {
           // Код, который может вызвать исключение
       }
       catch (FormatException ex)  // Более конкретный тип исключения
       {
           Console.WriteLine("Ошибка формата");
       }
       catch (Exception ex)  // Общее исключение в самом низу
       {
           Console.WriteLine("Общая ошибка");
       }
       ```

       Почему важен порядок?
       Если сначала поставить `catch (Exception)`, то он поймает любое исключение,  
       и остальные `catch` никогда не выполнятся.

    */

    internal static class FifteenthLesson
    {
        public static void Main_()
        {
            Console.WriteLine("Демонстрация обработки исключений в C#\n");

            // 1. Простой try-catch
            HandleBasicException();

            // 2. Обработка нескольких исключений
            HandleMultipleExceptions();

            // 3. Использование блока finally
            HandleWithFinally();

            // 4. Создание и выброс собственных исключений
            ThrowCustomException();

            // 5. Catch без параметров
            CatchWithoutParameters();

            // 6. Разница между throw и throw ex
            DemonstrateThrowVsThrowEx();
        }

        /* Пример 1: Простейший try-catch */
        public static void HandleBasicException()
        {
            try
            {
                Console.Write("Введите число: ");
                int number = int.Parse(Console.ReadLine()); // Ошибка, если введён нечисловой ввод
                Console.WriteLine($"Вы ввели число: {number}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка: Введено не число! {ex.Message}");
            }
        }

        /* Пример 2: Обработка нескольких исключений */
        public static void HandleMultipleExceptions()
        {
            try
            {
                int[] numbers = { 1, 2, 3 };
                Console.WriteLine("Введите индекс массива:");
                int index = int.Parse(Console.ReadLine()); // Может вызвать FormatException
                Console.WriteLine($"Значение: {numbers[index]}"); // Может вызвать IndexOutOfRangeException
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введено не число!");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Ошибка: Выход за границы массива!");
            }
        }

        /* Пример 3: Использование блока finally */
        public static void HandleWithFinally()
        {
            StreamReader reader = null;

            try
            {
                reader = new StreamReader("nonexistent.txt");
                Console.WriteLine(reader.ReadToEnd());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ошибка: Файл не найден.");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    Console.WriteLine("Файл закрыт.");
                }
            }
        }

        /* Пример 4: Создание и выброс собственного исключения */
        public static void ThrowCustomException()
        {
            try
            {
                Console.Write("Введите возраст: ");
                int age = int.Parse(Console.ReadLine());

                if (age < 0)
                {
                    throw new ArgumentException("Возраст не может быть отрицательным!");
                }

                Console.WriteLine($"Ваш возраст: {age}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /* Пример 5: Catch без параметров */
        public static void CatchWithoutParameters()
        {
            try
            {
                int x = 10;
                int y = 0;
                Console.WriteLine(x / y); // Деление на ноль
            }
            catch
            {
                Console.WriteLine("Произошла ошибка!"); // Общий catch без указания типа исключения
            }
        }

        /* Пример 6: Разница между throw и throw ex */
        public static void DemonstrateThrowVsThrowEx()
        {
            try
            {
                MethodWithThrow();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Перехвачено в Main():");
                Console.WriteLine(ex);
            }

            Console.WriteLine("\n---\n");

            try
            {
                MethodWithThrowEx();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Перехвачено в Main():");
                Console.WriteLine(ex);
            }
        }

        public static void MethodWithThrow()
        {
            try
            {
                throw new InvalidOperationException("Произошла ошибка в MethodWithThrow.");
            }
            catch (Exception)
            {
                Console.WriteLine("Исключение обработано в MethodWithThrow, но снова выброшено.");
                throw; // Сохраняет стек вызовов
            }
        }

        public static void MethodWithThrowEx()
        {
            try
            {
                throw new InvalidOperationException("Произошла ошибка в MethodWithThrowEx.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение обработано в MethodWithThrowEx, но снова выброшено.");
                throw ex; // Перезаписывает стек вызовов
            }
        }
    }
}

/* Разница между `throw;` и `throw ex;`:
    
    - `throw;` (без параметров)  
      Повторно выбрасывает исключение без изменения информации о нём.  
      Сохраняет оригинальный стек вызовов, что полезно для отладки.

    - throw ex; (с параметром)  
      Перезаписывает стек вызовов, теряя информацию о том, где именно произошло исключение.  
      Использовать **не рекомендуется**, так как это усложняет отладку.

    Вывод:
    - Всегда используйте `throw;`, если хотите повторно выбросить исключение.
    - Использование `throw ex;` оправдано только в редких случаях, например, если вам нужно изменить сообщение об ошибке.
*/

/* Задачи:

    1. Напишите программу, которая запрашивает у пользователя два числа и делит первое на второе. Обработайте `DivideByZeroException`.
    2. Реализуйте метод, который открывает файл и считывает его содержимое. Обработайте `FileNotFoundException`.
    3. Напишите метод, который просит ввести число и проверяет, является ли оно положительным. Если нет, выбросите `ArgumentException`.
    4. Создайте метод, который конвертирует строку в число и ловит `FormatException`.
    5. Реализуйте метод, который запрашивает индекс массива и обрабатывает `IndexOutOfRangeException`.
    6. Сделайте метод, который считывает содержимое файла, а в `finally` закрывает поток.
    7. Сделайте программу-калькулятор, в которой все операции защищены `try-catch`, чтобы не было аварийного завершения.
*/

