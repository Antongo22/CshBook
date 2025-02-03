using System;
using System.Collections.Generic;

namespace CshBook.Lessons
{
    /*Обобщённые методы (Generics)
    
       Обобщённые методы позволяют создавать гибкие и типобезопасные методы, которые могут работать
       с разными типами данных без необходимости дублирования кода. 
       
       Зачем нужны:
       1. Универсальность: Один метод работает с разными типами
       2. Типобезопасность: Избегаем ошибок приведения типов
       3. Производительность: Не требуется упаковка/распаковка значимых типов
       
       Как работают:
       - В объявлении метода указывается тип-параметр в угловых скобках: <T>
       - T заменяется на конкретный тип при вызове метода
       - Можно накладывать ограничения на тип (where T: ...)
    */

    /* Обобщённые методы (Generics) - Продвинутая теория
    
       ЧТО ТАКОЕ ОБОБЩЁННЫЕ МЕТОДЫ?
       Это методы, которые работают с разными типами данных, сохраняя типобезопасность.
       Пример: метод для вывода значения, работающий с int, string, double и т.д.

       ЗАЧЕМ НУЖНЫ?
       1. Универсальность: Один метод вместо множества перегрузок
       2. Безопасность типов: Компилятор проверяет типы на этапе компиляции
       3. Эффективность: Для значимых типов не требуется упаковка/распаковка

       КЛЮЧЕВОЕ СЛОВО WHERE:
       Позволяет накладывать ограничения на обобщённые типы.
       Это нужно, чтобы:
       - Сообщить компилятору, какие операции доступны для типа T
       - Гарантировать наличие определённого функционала у типа

       Базовые ограничения:
       where T : IComparable<T>  // Тип поддерживает сравнение
       where T : IEquatable<T>   // Тип поддерживает проверку равенства
       where T : class           // T должен быть ссылочным типом
       where T : struct          // T должен быть значимым типом
       where T : new()           // Тип имеет конструктор по умолчанию
    */

    internal static class EleventhLesson
    {

        #region База
        // Простейший обобщённый метод
        public static void PrintValue<T>(T value)
        {
            Console.WriteLine($"Value: {value}, Type: {typeof(T)}");
        }

        // Обмен значений переменных любого типа
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        // Поиск элемента в массиве (ограничение where T: IEquatable<T>)
        public static bool FindInArray<T>(T[] array, T target) where T : IEquatable<T>
        {
            foreach (T item in array)
            {
                if (item.Equals(target)) return true;
            }
            return false;
        }

        // Обобщённый метод с возвратом массива
        public static T[] CreateArray<T>(int size, T defaultValue)
        {
            T[] arr = new T[size];
            for (int i = 0; i < size; i++)
                arr[i] = defaultValue;
            return arr;
        }

        // Метод с несколькими типами-параметрами
        public static void PrintTwoTypes<T, U>(T first, U second)
        {
            Console.WriteLine($"First: {first} ({typeof(T)}), Second: {second} ({typeof(U)})");
        }
        #endregion


        // Метод с ограничением IComparable для сравнения
        public static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            // Теперь мы можем использовать CompareTo
            return a.CompareTo(b) > 0 ? a : b;
        }

        // Пример 3: Ограничение new() для создания экземпляров
        public static T CreateInstance<T>() where T : new()
        {
            // Можем создавать объекты: new T()
            return new T();
        }

        //Ограничение struct для значимых типов
        public static string GetTypeKind<T>(T value) where T : struct
        {
            return "Это значимый тип!";
        }

        // Пример 5: Комбинирование ограничений
        public static bool AreEqual<T>(T a, T b) where T : IEquatable<T>, new()
        {
            // Используем Equals из IEquatable и создаём новый объект
            T temp = new T();
            return a.Equals(b);
        }

        // Пример 6: Ограничение class для ссылочных типов
        public static void CheckForNull<T>(T obj) where T : class
        {
            // Можем проверять на null
            Console.WriteLine(obj == null ? "Null!" : "Not null");
        }

        // Демонстрация всех возможностей
        public static void Main()
        {
            #region База
            // Демонстрация PrintValue
            PrintValue(42);                   // int
            PrintValue("Hello Generics!");    // string
            PrintValue(3.14);                 // double

            // Демонстрация Swap
            int x = 5, y = 10;
            Console.WriteLine($"Before Swap: x={x}, y={y}");
            Swap(ref x, ref y);
            Console.WriteLine($"After Swap: x={x}, y={y}");

            string s1 = "First", s2 = "Second";
            Swap(ref s1, ref s2);
            Console.WriteLine($"Swapped strings: {s1}, {s2}");

            // Демонстрация FindInArray
            int[] numbers = { 1, 2, 3, 4, 5 };
            Console.WriteLine("Contains 3? " + FindInArray(numbers, 3));

            string[] words = { "apple", "banana", "cherry" };
            Console.WriteLine("Contains 'orange'? " + FindInArray(words, "orange"));

            // Демонстрация CreateArray
            int[] defaultInts = CreateArray(5, -1);
            Console.WriteLine("Int array: " + string.Join(", ", defaultInts));

            string[] defaultStrings = CreateArray(3, "empty");
            Console.WriteLine("String array: " + string.Join(", ", defaultStrings));

            // Демонстрация PrintTwoTypes
            PrintTwoTypes(42, "Answer");
            PrintTwoTypes(DateTime.Now, Math.PI);

            #endregion

            #region Сложно
            // Сравнение разных типов
            Console.WriteLine(FindMax(5, 10));                 // 10
            Console.WriteLine(FindMax("apple", "banana"));     // banana

            //  Создание экземпляров
            DateTime time = CreateInstance<DateTime>();
            Console.WriteLine(time);

            // Работа только со значимыми типами
            Console.WriteLine(GetTypeKind(42));       // ОК
            // Console.WriteLine(GetTypeKind("test")); // Ошибка компиляции!

            // Комбинирование ограничений
            Console.WriteLine(AreEqual(5, 5));        // True

            // Проверка ссылочных типов
            CheckForNull("test");         // Not null
            CheckForNull<string>(null);   // Null
            #endregion
        }
    }
}

/* Задачи для практики (первые 10 - простые, последние 4 - с WHERE):

1. Метод вывода массива:
   Напишите обобщённый метод, который принимает массив любого типа и выводит его элементы через запятую.

2. Обмен значений:
   Напишите обобщённый метод, который меняет местами два значения любого типа.

3. Поиск элемента в массиве:
   Напишите метод, который проверяет, содержится ли элемент в массиве.

4. Создание массива:
   Напишите метод, который создаёт массив заданного размера и заполняет его значением по умолчанию.

5. Вывод типа:
   Напишите метод, который выводит тип переданного значения.

6. Сравнение двух значений:
   Напишите метод, который сравнивает два значения и возвращает true, если они равны.

7. Подсчёт длины массива:
   Напишите метод, который возвращает длину массива любого типа.

8. Копирование массива:
   Напишите метод, который копирует элементы одного массива в другой.

9. Поиск индекса элемента:
   Напишите метод, который возвращает индекс первого вхождения элемента в массиве.

10. Обнуление массива:
    Напишите метод, который обнуляет все элементы массива (для чисел) или делает их пустыми (для строк).

11. Поиск максимального элемента (с WHERE):
    Создайте метод FindMax<T>, который находит максимальный элемент в массиве. 
    Используйте ограничение where T : IComparable<T>.

12. Подсчёт элементов (с WHERE):
    Создайте метод CountOccurrences<T>, который подсчитывает, сколько раз элемент встречается в массиве.
    Используйте where T : IEquatable<T>.

13. Фильтрация элементов (с WHERE):
    Создайте метод Filter<T>, который возвращает новый массив с элементами, удовлетворяющими условию.
    Используйте where T : IEquatable<T>.

14. Сравнение массивов (с WHERE):
    Создайте метод AreArraysEqual<T>, который проверяет два массива на полное совпадение элементов.
    Используйте where T : IEquatable<T>.
*/

/* Примеры решения (для проверки):

// Задача 1: Метод вывода массива
public static void PrintArray<T>(T[] array)
{
    Console.WriteLine(string.Join(", ", array));
}

// Задача 2: Обмен значений
public static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

// Задача 3: Поиск элемента в массиве
public static bool Contains<T>(T[] array, T target) where T : IEquatable<T>
{
    foreach (T item in array)
        if (item.Equals(target)) return true;
    return false;
}

// Задача 10: Обнуление массива
static void ResetArray<T>(T[] array, T defaultValue)
{
    for (int i = 0; i < array.Length; i++)
    {
        array[i] = defaultValue;
    }
}

// Задача 11: Поиск максимального элемента
public static T FindMax<T>(T[] array) where T : IComparable<T>
{
    if (array.Length == 0) throw new InvalidOperationException();
    
    T max = array[0];
    foreach (T item in array)
        if (item.CompareTo(max) > 0) max = item;
    return max;
}

// Задача 12: Подсчёт элементов
public static int CountOccurrences<T>(T[] array, T target) where T : IEquatable<T>
{
    int count = 0;
    foreach (T item in array)
        if (item.Equals(target)) count++;
    return count;
}

// Задача 13: Фильтрация элементов

// Метод для проверки чётности числа
public static bool IsEven(int number)
{
    return number % 2 == 0;
}

// Метод для проверки, больше ли число 5
public static bool IsGreaterThanFive(int number)
{
    return number > 5;
}

// Метод для проверки, начинается ли строка с 'b'
public static bool StartsWithB(string word)
{
    return word.StartsWith("b", StringComparison.OrdinalIgnoreCase);
}

public static T[] Filter<T>(T[] array, Func<T, bool> predicate) where T : IEquatable<T>
{
    List<T> result = new List<T>();
    foreach (T item in array)
        if (predicate(item)) result.Add(item);
    return result.ToArray();
}

// Задача 14: Сравнение массивов
public static bool AreArraysEqual<T>(T[] array1, T[] array2) where T : IEquatable<T>
{
    if (array1.Length != array2.Length) return false;
    for (int i = 0; i < array1.Length; i++)
        if (!array1[i].Equals(array2[i])) return false;
    return true;
}
*/

/* ЧАСТО ИСПОЛЬЗУЕМЫЕ ИНТЕРФЕЙСЫ ДЛЯ WHERE:
   - IComparable<T>  : Сравнение объектов (CompareTo)
   - IEquatable<T>   : Проверка равенства (Equals)
   - IConvertible    : Преобразование в другие типы
   - IEnumerable<T>  : Работа с коллекциями
   - ICloneable      : Поддержка клонирования
   - IFormattable    : Форматирование в строку
*/

/* КАК ЭТО РАБОТАЕТ С ТИПАМИ?
   Для значимых типов (int, struct):
   - Компилятор генерирует отдельную версию метода для каждого типа
   - Нет накладных расходов на приведение типов

   Для ссылочных типов (классы):
   - Все ссылочные типы используют одну скомпилированную версию
   - Но тип контролируется на этапе компиляции
*/  