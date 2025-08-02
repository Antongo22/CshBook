using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Diagnostics;

namespace CshBook.Lessons._29;

#region Теория
/*
 * В этом уроке ты узнаешь о коллекциях в C#:
 * - Обзор основных типов коллекций
 * - List<T> - динамические массивы
 * - Dictionary<TKey, TValue> - словари
 * - HashSet<T> - множества уникальных элементов
 * - Queue<T> и Stack<T> - очереди и стеки
 * - LinkedList<T> - связанные списки
 * - Выбор правильной коллекции для задачи
 */

/*
   Обзор коллекций в C#
   ===================
   
   Коллекции в C# - это классы, которые представляют группы объектов. Они позволяют:
   - Хранить данные
   - Управлять группами объектов
   - Выполнять поиск, сортировку и другие операции
   
   Основные пространства имен:
   - System.Collections - старые необобщенные коллекции (ArrayList, Hashtable и т.д.)
   - System.Collections.Generic - современные обобщенные коллекции (List<T>, Dictionary<K,V> и т.д.)
   - System.Collections.Concurrent - потокобезопасные коллекции
   - System.Collections.Specialized - специализированные коллекции
   
   Современный подход - использовать обобщенные коллекции из System.Collections.Generic.
*/

/*
   List<T> - динамические массивы
   ===========================
   
   List<T> - это динамический массив, который может изменять размер автоматически.
   
   Основные операции:
   - Добавление: Add(), AddRange(), Insert()
   - Удаление: Remove(), RemoveAt(), RemoveAll(), Clear()
   - Поиск: Contains(), Find(), FindAll(), IndexOf()
   - Сортировка: Sort()
   
   Производительность:
   - Доступ по индексу: O(1) - очень быстро
   - Добавление в конец: O(1) (амортизированное время) - обычно быстро
   - Вставка/удаление в начале/середине: O(n) - медленно для больших списков
   - Поиск элемента: O(n) - линейный перебор
*/

/*
   Dictionary<TKey, TValue> - словари
   ================================
   
   Dictionary - это коллекция пар ключ-значение, где каждый ключ уникален.
   
   Основные операции:
   - Добавление: Add(), [] (индексатор)
   - Удаление: Remove(), Clear()
   - Проверка: ContainsKey(), ContainsValue(), TryGetValue()
   - Доступ: [] (индексатор)
   
   Производительность:
   - Доступ/добавление/удаление по ключу: O(1) - очень быстро
   - Поиск по значению: O(n) - необходим перебор всех элементов
*/

/*
   HashSet<T> - множества уникальных элементов
   ========================================
   
   HashSet<T> - это коллекция уникальных элементов без определенного порядка.
   
   Основные операции:
   - Добавление: Add(), UnionWith()
   - Удаление: Remove(), ExceptWith(), Clear()
   - Проверка: Contains()
   - Теоретико-множественные операции: IntersectWith(), UnionWith(), ExceptWith()
   
   Производительность:
   - Добавление/удаление/проверка: O(1) - очень быстро
*/

/*
   Queue<T> и Stack<T> - очереди и стеки
   ===================================
   
   Queue<T> (Очередь) - коллекция, работающая по принципу FIFO (First In, First Out).
   
   Основные операции Queue<T>:
   - Добавление: Enqueue()
   - Извлечение: Dequeue(), Peek()
   
   Stack<T> (Стек) - коллекция, работающая по принципу LIFO (Last In, First Out).
   
   Основные операции Stack<T>:
   - Добавление: Push()
   - Извлечение: Pop(), Peek()
   
   Производительность обоих типов:
   - Добавление/извлечение: O(1) - очень быстро
*/

/*
   LinkedList<T> - связанные списки
   =============================
   
   LinkedList<T> - двусвязный список, где каждый элемент содержит ссылки на предыдущий и следующий.
   
   Основные операции:
   - Добавление: AddFirst(), AddLast(), AddBefore(), AddAfter()
   - Удаление: Remove(), RemoveFirst(), RemoveLast()
   - Поиск: Find(), FindLast()
   
   Производительность:
   - Добавление/удаление в начале/конце: O(1) - очень быстро
   - Добавление/удаление в середине (при наличии узла): O(1)
   - Поиск элемента: O(n) - необходим перебор
   - Доступ по индексу: O(n) - необходим перебор до нужной позиции
*/

/*
   Выбор правильной коллекции
   =========================
   
   Рекомендации по выбору:
   
   - List<T>: когда нужен доступ по индексу, и операции в основном в конце коллекции
   - Dictionary<K,V>: когда нужен быстрый доступ по ключу
   - HashSet<T>: когда важно поддерживать только уникальные значения
   - Queue<T>: для реализации очереди обработки (FIFO)
   - Stack<T>: для отслеживания состояний, отмены действий (LIFO)
   - LinkedList<T>: когда частые вставки/удаления в середине коллекции
*/
#endregion

public class CollectionsLesson
{
    public static void Main_()
    {
        Console.WriteLine("Урок 29: Коллекции");
        Console.WriteLine("===================");
        Console.WriteLine();

        // Пример 1: List<T>
        Console.WriteLine("Пример 1: Работа со списком (List<T>)");
        ListExample();
        Console.WriteLine();

        // Пример 2: Dictionary<TKey, TValue>
        Console.WriteLine("Пример 2: Работа со словарем (Dictionary<TKey, TValue>)");
        DictionaryExample();
        Console.WriteLine();

        // Пример 3: HashSet<T>
        Console.WriteLine("Пример 3: Работа с множеством (HashSet<T>)");
        HashSetExample();
        Console.WriteLine();

        // Пример 4: Queue<T> и Stack<T>
        Console.WriteLine("Пример 4: Работа с очередью и стеком (Queue<T> и Stack<T>)");
        QueueAndStackExample();
        Console.WriteLine();

        // Пример 5: LinkedList<T>
        Console.WriteLine("Пример 5: Работа со связанным списком (LinkedList<T>)");
        LinkedListExample();
        Console.WriteLine();

        // Пример 6: Сравнение производительности коллекций
        Console.WriteLine("Пример 6: Сравнение производительности коллекций");
        PerformanceComparison();
        Console.WriteLine();
    }

    // Пример работы с List<T>
    static void ListExample()
    {
        // Создание списка
        List<string> fruits = new List<string>();
    
        // Добавление элементов
        fruits.Add("Яблоко");
        fruits.Add("Банан");
        fruits.Add("Апельсин");
        fruits.AddRange(new[] { "Груша", "Ананас" });
    
        Console.WriteLine("Список фруктов:");
        foreach (string fruit in fruits)
        {
            Console.WriteLine($"- {fruit}");
        }
    
        // Поиск и фильтрация
        Console.WriteLine($"\nЕсть ли банан в списке: {fruits.Contains("Банан")}");
        Console.WriteLine($"Индекс апельсина: {fruits.IndexOf("Апельсин")}");
    
        // Фрукты, начинающиеся на 'А'
        var aFruits = fruits.FindAll(f => f.StartsWith("А"));
        Console.WriteLine("\nФрукты на 'А':");
        foreach (string fruit in aFruits)
        {
            Console.WriteLine($"- {fruit}");
        }
    
        // Сортировка
        fruits.Sort();
        Console.WriteLine("\nОтсортированный список:");
        foreach (string fruit in fruits)
        {
            Console.WriteLine($"- {fruit}");
        }
    
        // Удаление
        fruits.Remove("Банан");
        fruits.RemoveAt(0); // Удаляем первый элемент
    
        Console.WriteLine("\nСписок после удаления:");
        foreach (string fruit in fruits)
        {
            Console.WriteLine($"- {fruit}");
        }
    }

    // Пример работы с Dictionary<TKey, TValue>
    static void DictionaryExample()
    {
        // Создание словаря
        Dictionary<string, int> ages = new Dictionary<string, int>();
    
        // Добавление элементов
        ages.Add("Анна", 25);
        ages["Борис"] = 30; // Альтернативный способ добавления
        ages["Виктор"] = 22;
        ages["Галина"] = 35;
    
        Console.WriteLine("Возраст людей:");
        foreach (var pair in ages)
        {
            Console.WriteLine($"- {pair.Key}: {pair.Value} лет");
        }
    
        // Проверка наличия ключа
        string name = "Дмитрий";
        if (!ages.ContainsKey(name))
        {
            Console.WriteLine($"\n{name} отсутствует в словаре");
            ages[name] = 40; // Добавляем нового человека
        }
    
        // Безопасное получение значения
        if (ages.TryGetValue("Борис", out int borisAge))
        {
            Console.WriteLine($"\nВозраст Бориса: {borisAge} лет");
        }
    
        // Перебор только ключей или значений
        Console.WriteLine("\nИмена всех людей:");
        foreach (string personName in ages.Keys)
        {
            Console.WriteLine($"- {personName}");
        }
    
        // Удаление элемента
        ages.Remove("Анна");
        Console.WriteLine("\nСловарь после удаления Анны:");
        foreach (var pair in ages)
        {
            Console.WriteLine($"- {pair.Key}: {pair.Value} лет");
        }
    }

    // Пример работы с HashSet<T>
    static void HashSetExample()
    {
        // Создание множеств
        HashSet<int> set1 = new HashSet<int> { 1, 2, 3, 4, 5 };
        HashSet<int> set2 = new HashSet<int> { 3, 4, 5, 6, 7 };
    
        Console.WriteLine("Множество 1: " + string.Join(", ", set1));
        Console.WriteLine("Множество 2: " + string.Join(", ", set2));
    
        // Демонстрация уникальности
        set1.Add(3); // Добавление существующего элемента
        Console.WriteLine("\nМножество 1 после попытки добавить 3: " + string.Join(", ", set1));
    
        // Проверка наличия элемента
        Console.WriteLine($"\n4 присутствует в множестве 1: {set1.Contains(4)}");
        Console.WriteLine($"8 присутствует в множестве 1: {set1.Contains(8)}");
    
        // Операции над множествами
        HashSet<int> union = new HashSet<int>(set1);
        union.UnionWith(set2);
        Console.WriteLine("\nОбъединение множеств: " + string.Join(", ", union));
    
        HashSet<int> intersection = new HashSet<int>(set1);
        intersection.IntersectWith(set2);
        Console.WriteLine("Пересечение множеств: " + string.Join(", ", intersection));
    
        HashSet<int> difference = new HashSet<int>(set1);
        difference.ExceptWith(set2);
        Console.WriteLine("Разность множеств (set1 - set2): " + string.Join(", ", difference));
    }

    // Пример работы с Queue<T> и Stack<T>
    static void QueueAndStackExample()
    {
        // Работа с очередью
        Queue<string> queue = new Queue<string>();
    
        // Добавление в очередь
        queue.Enqueue("Первый");
        queue.Enqueue("Второй");
        queue.Enqueue("Третий");
    
        Console.WriteLine("Элементы очереди:");
        foreach (string item in queue)
        {
            Console.WriteLine($"- {item}");
        }
    
        // Извлечение из очереди (FIFO)
        Console.WriteLine("\nИзвлечение из очереди:");
        Console.WriteLine($"Извлечен: {queue.Dequeue()}"); // Первый
        Console.WriteLine($"Извлечен: {queue.Dequeue()}"); // Второй
    
        // Просмотр элемента без извлечения
        Console.WriteLine($"Следующий элемент: {queue.Peek()}"); // Третий
    
        // Работа со стеком
        Stack<string> stack = new Stack<string>();
    
        // Добавление в стек
        stack.Push("Первый");
        stack.Push("Второй");
        stack.Push("Третий");
    
        Console.WriteLine("\nЭлементы стека:");
        foreach (string item in stack)
        {
            Console.WriteLine($"- {item}");
        }
    
        // Извлечение из стека (LIFO)
        Console.WriteLine("\nИзвлечение из стека:");
        Console.WriteLine($"Извлечен: {stack.Pop()}"); // Третий
        Console.WriteLine($"Извлечен: {stack.Pop()}"); // Второй
    
        // Просмотр элемента без извлечения
        Console.WriteLine($"Следующий элемент: {stack.Peek()}"); // Первый
    }

    // Пример работы с LinkedList<T>
    static void LinkedListExample()
    {
        // Создание связанного списка
        LinkedList<string> linkedList = new LinkedList<string>();
    
        // Добавление элементов
        linkedList.AddLast("Элемент 3");
        linkedList.AddFirst("Элемент 1");
    
        // Получение узлов и вставка между ними
        LinkedListNode<string> firstNode = linkedList.First;
        linkedList.AddAfter(firstNode, "Элемент 2");
    
        Console.WriteLine("Связанный список:");
        foreach (string item in linkedList)
        {
            Console.WriteLine($"- {item}");
        }
    
        // Получение узла и работа с ним
        LinkedListNode<string> node = linkedList.Find("Элемент 2");
        if (node != null)
        {
            Console.WriteLine($"\nНайден: {node.Value}");
            Console.WriteLine($"Предыдущий: {node.Previous.Value}");
            Console.WriteLine($"Следующий: {node.Next.Value}");
        
            // Вставка до и после узла
            linkedList.AddBefore(node, "Элемент 1.5");
            linkedList.AddAfter(node, "Элемент 2.5");
        }
    
        Console.WriteLine("\nСвязанный список после изменений:");
        foreach (string item in linkedList)
        {
            Console.WriteLine($"- {item}");
        }
    
        // Удаление элементов
        linkedList.Remove("Элемент 1.5");
        linkedList.RemoveFirst();
        linkedList.RemoveLast();
    
        Console.WriteLine("\nСвязанный список после удаления:");
        foreach (string item in linkedList)
        {
            Console.WriteLine($"- {item}");
        }
    }

    // Сравнение производительности коллекций
    static void PerformanceComparison()
    {
        const int iterations = 100000;
    
        // Тест List<T>
        Stopwatch sw = Stopwatch.StartNew();
        List<int> list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }
        sw.Stop();
        Console.WriteLine($"Добавление {iterations} элементов в List<T>: {sw.ElapsedMilliseconds} мс");
    
        // Тест Dictionary<TKey, TValue>
        sw.Restart();
        Dictionary<int, int> dict = new Dictionary<int, int>();
        for (int i = 0; i < iterations; i++)
        {
            dict[i] = i;
        }
        sw.Stop();
        Console.WriteLine($"Добавление {iterations} элементов в Dictionary<TKey, TValue>: {sw.ElapsedMilliseconds} мс");
    
        // Тест HashSet<T>
        sw.Restart();
        HashSet<int> hashSet = new HashSet<int>();
        for (int i = 0; i < iterations; i++)
        {
            hashSet.Add(i);
        }
        sw.Stop();
        Console.WriteLine($"Добавление {iterations} элементов в HashSet<T>: {sw.ElapsedMilliseconds} мс");
    
        // Тест LinkedList<T>
        sw.Restart();
        LinkedList<int> linkedList = new LinkedList<int>();
        for (int i = 0; i < iterations; i++)
        {
            linkedList.AddLast(i);
        }
        sw.Stop();
        Console.WriteLine($"Добавление {iterations} элементов в LinkedList<T>: {sw.ElapsedMilliseconds} мс");
    
        // Тест поиска
        Console.WriteLine("\nПоиск элементов:");
    
        // Поиск в List
        sw.Restart();
        bool foundInList = list.Contains(iterations / 2);
        sw.Stop();
        Console.WriteLine($"Поиск в List<T>: {sw.ElapsedMilliseconds} мс");
    
        // Поиск в Dictionary
        sw.Restart();
        bool foundInDict = dict.ContainsKey(iterations / 2);
        sw.Stop();
        Console.WriteLine($"Поиск в Dictionary<TKey, TValue>: {sw.ElapsedMilliseconds} мс");
    
        // Поиск в HashSet
        sw.Restart();
        bool foundInHashSet = hashSet.Contains(iterations / 2);
        sw.Stop();
        Console.WriteLine($"Поиск в HashSet<T>: {sw.ElapsedMilliseconds} мс");
    
        // Поиск в LinkedList
        sw.Restart();
        bool foundInLinkedList = linkedList.Contains(iterations / 2);
        sw.Stop();
        Console.WriteLine($"Поиск в LinkedList<T>: {sw.ElapsedMilliseconds} мс");
    }
}

#region Задачи
/*
    # Создайте программу для работы с записной книжкой контактов. Используйте Dictionary<string, string> 
        для хранения имен и телефонных номеров. Реализуйте методы для добавления, удаления, поиска 
        и вывода всех контактов.
    
    # Напишите метод, который принимает два списка чисел и возвращает новый список, содержащий:
        а) Все элементы обоих списков (объединение)
        б) Только элементы, которые есть в обоих списках (пересечение)
        в) Элементы первого списка, которых нет во втором (разность)
        Используйте HashSet<T> для эффективной реализации.
    
    # Реализуйте структуру данных "Скользящее окно" на основе Queue<T>, которая хранит 
        только N последних добавленных элементов. При добавлении N+1 элемента самый старый 
        должен автоматически удаляться.
    
    # Напишите программу для проверки сбалансированности скобок в выражении. 
        Используйте Stack<char> для отслеживания открывающих скобок. Поддержите несколько 
        типов скобок: (), {}, [].
    
    
    # СЛОЖНОЕ ЗАДАНИЕ: Создайте свой класс MyString, который будет эмулировать некоторые 
        возможности стандартного string. Реализуйте:
        - Хранение символов в массиве char[]
        - Конструкторы для создания из массива символов и из обычной строки
        - Методы Length, Substring, IndexOf, Replace
        - Переопределение ToString() для корректного отображения
        - Операторы + для конкатенации и == для сравнения
*/
#endregion
