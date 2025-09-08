using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics;

namespace CshBook.Lessons.Глава_3
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о дженериках (обобщениях) в C# и как создавать свои собственные
     * обобщенные классы, методы и коллекции:
     * 
     * - Что такое дженерики и зачем они нужны
     * - Создание обобщенных классов
     * - Создание обобщенных методов
     * - Ограничения типов (constraints)
     * - Создание своих обобщенных коллекций
     * - Реализация основных интерфейсов коллекций
     */

    /*
       Что такое дженерики и зачем они нужны
       ====================================
       
       Дженерики (обобщения) в C# позволяют создавать классы, методы и интерфейсы,
       которые работают с разными типами данных, но при этом сохраняют типобезопасность.
       
       Основные преимущества дженериков:
       
       1. Типобезопасность - компилятор проверяет типы, предотвращая ошибки приведения типов
       2. Устранение необходимости в явном приведении типов (как было в необобщенных коллекциях)
       3. Повышение производительности - избегают упаковки/распаковки для типов-значений
       4. Повторное использование кода - одна реализация для разных типов данных
       
       Синтаксис обобщений использует угловые скобки < > с именами параметров типа (обычно T):
       List<int>, Dictionary<string, User>, MyClass<T>, и т.д.
    */

    /*
       Создание обобщенных классов
       ==========================
       
       Обобщенный класс объявляется с параметрами типа в угловых скобках.
       Параметр типа - это заполнитель, который будет заменен реальным типом при создании экземпляра.
       
       Пример объявления обобщенного класса:
       
       public class Box<T>
       {
           private T item;
           
           public void SetItem(T newItem)
           {
               item = newItem;
           }
           
           public T GetItem()
           {
               return item;
           }
       }
       
       Использование:
       
       Box<int> intBox = new Box<int>();
       intBox.SetItem(123);
       int value = intBox.GetItem(); // 123
       
       Box<string> stringBox = new Box<string>();
       stringBox.SetItem("Hello");
       string text = stringBox.GetItem(); // "Hello"
    */

    /*
       Создание обобщенных методов
       =========================
       
       Обобщенные методы могут быть объявлены как внутри обобщенных классов,
       так и внутри обычных классов.
       
       Синтаксис:
       
       public TResult MethodName<T, TResult>(T param)
       {
           // реализация
       }
       
       Компилятор часто может вывести тип параметра из аргументов,
       поэтому при вызове можно опустить явное указание типа:
       
       // Явное указание типа
       Swap<int>(ref a, ref b);
       
       // Неявный вывод типа
       Swap(ref a, ref b); // Компилятор определит, что T = int
    */
    
    /*
       Ограничения типов (constraints)
       =============================
       
       Ограничения типов позволяют указать, какие типы могут использоваться в качестве
       аргументов для параметров типа.
       
       Основные ограничения:
       
       where T : class - T должен быть ссылочным типом
       where T : struct - T должен быть типом-значением
       where T : new() - T должен иметь конструктор без параметров
       where T : BaseClass - T должен быть или наследоваться от BaseClass
       where T : IInterface - T должен реализовывать интерфейс IInterface
       where T : U - T должен быть или наследоваться от другого параметра типа U
       
       Пример:
       
       public class Repository<T> where T : class, IEntity, new()
       {
           // T должен быть классом, реализовывать IEntity и иметь конструктор без параметров
       }
    */

    /*
       Создание своих обобщенных коллекций
       ==================================
       
       При создании собственных коллекций полезно реализовать стандартные интерфейсы:
       
       - IEnumerable<T> - базовый интерфейс для всех коллекций, позволяющий перечислять элементы
       - ICollection<T> - добавляет базовые операции: Add, Remove, Contains, Count
       - IList<T> - добавляет доступ по индексу и управление порядком элементов
       - IDictionary<TKey, TValue> - для коллекций с ключами и значениями
       
       IEnumerable<T> - минимальный интерфейс для любой коллекции, позволяет использовать foreach:
       
       public interface IEnumerable<T>
       {
           IEnumerator<T> GetEnumerator();
       }
       
       Для реализации этого интерфейса нужно создать метод GetEnumerator(),
       который возвращает объект IEnumerator<T>.
    */
    
    /*
       Реализация основных интерфейсов коллекций
       ======================================
       
       Создание обобщенной коллекции включает несколько шагов:
       
       1. Определить внутреннее хранилище данных (массив, связанный список и т.д.)
       2. Реализовать нужные интерфейсы (IEnumerable<T>, ICollection<T>, IList<T>)
       3. Реализовать методы добавления, удаления, поиска элементов
       4. Реализовать необходимые свойства (Count, Capacity и т.д.)
       5. Реализовать правильное управление памятью и изменение размера
       
       Мы рассмотрим примеры реализации в практической части урока.
    */
    #endregion

    // Пример обобщенного класса для хранения одного элемента
    public class Box<T>
    {
        private T _item;
        
        public T Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public Box() { }
        
        public Box(T item)
        {
            _item = item;
        }

        public override string ToString()
        {
            return _item != null ? _item.ToString() : "[пусто]";
        }
    }

    // Пример обобщенного класса для хранения пар ключ-значение
    public class Pair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Key}] = {Value}";
        }
    }

    // Пример простой реализации собственной коллекции типа список
    public class SimpleList<T>
    {
        private T[] _items;
        private int _count;
        private const int DefaultCapacity = 4;

        public int Count { get { return _count; } }
        public int Capacity { get { return _items.Length; } }

        public SimpleList()
        {
            _items = new T[DefaultCapacity];
            _count = 0;
        }

        public SimpleList(int capacity)
        {
            _items = new T[capacity > 0 ? capacity : DefaultCapacity];
            _count = 0;
        }

        // Индексатор для доступа к элементам по индексу
        public T this[int index]
        {
            get 
            { 
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException("Индекс находится за пределами диапазона");
                return _items[index]; 
            }
            set 
            { 
                if (index < 0 || index >= _count)
                    throw new IndexOutOfRangeException("Индекс находится за пределами диапазона");
                _items[index] = value; 
            }
        }

        // Добавление элемента в конец списка
        public void Add(T item)
        {
            if (_count == _items.Length)
                Resize(_items.Length * 2);
                
            _items[_count] = item;
            _count++;
        }

        // Вставка элемента по индексу
        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException("Индекс находится за пределами диапазона");

            if (_count == _items.Length)
                Resize(_items.Length * 2);

            // Сдвигаем элементы вправо, чтобы освободить место для вставки
            if (index < _count)
            {
                Array.Copy(_items, index, _items, index + 1, _count - index);
            }

            _items[index] = item;
            _count++;
        }

        // Удаление элемента по индексу
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException("Индекс находится за пределами диапазона");

            // Сдвигаем элементы влево, чтобы заполнить пробел
            _count--;
            if (index < _count)
            {
                Array.Copy(_items, index + 1, _items, index, _count - index);
            }

            // Очищаем последнюю ссылку, чтобы помочь сборщику мусора
            _items[_count] = default(T);
        }

        // Проверяем наличие элемента в списке
        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                // Используем EqualityComparer для корректного сравнения даже с null
                if (EqualityComparer<T>.Default.Equals(_items[i], item))
                    return true;
            }
            return false;
        }

        // Изменение размера внутреннего массива
        private void Resize(int newCapacity)
        {
            T[] newItems = new T[newCapacity];
            if (_count > 0)
            {
                Array.Copy(_items, 0, newItems, 0, _count);
            }
            _items = newItems;
        }

        // Очистка списка
        public void Clear()
        {
            if (_count > 0)
            {
                // Очищаем ссылки, чтобы помочь сборщику мусора
                Array.Clear(_items, 0, _count);
                _count = 0;
            }
        }
    }

    // Более полная реализация собственной коллекции с поддержкой IEnumerable<T>
    public class GenericCollection<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _count;
        private const int DefaultCapacity = 4;

        public int Count { get { return _count; } }

        public GenericCollection()
        {
            _items = new T[DefaultCapacity];
            _count = 0;
        }

        public GenericCollection(int capacity)
        {
            _items = new T[capacity > 0 ? capacity : DefaultCapacity];
            _count = 0;
        }

        public void Add(T item)
        {
            if (_count == _items.Length)
                Resize(_items.Length * 2);
                
            _items[_count] = item;
            _count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException("Индекс находится за пределами диапазона");

            _count--;
            if (index < _count)
            {
                Array.Copy(_items, index + 1, _items, index, _count - index);
            }
            _items[_count] = default(T);
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_items[i], item))
                    return i;
            }
            return -1;
        }

        private void Resize(int newCapacity)
        {
            T[] newItems = new T[newCapacity];
            if (_count > 0)
            {
                Array.Copy(_items, 0, newItems, 0, _count);
            }
            _items = newItems;
        }

        // Реализация IEnumerable<T> для поддержки foreach
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        // Реализация необобщенного IEnumerable (требуется для IEnumerable<T>)
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class GenericLessonMain
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по дженерикам (обобщениям) ====");
            
            // Демонстрация обобщенного класса Box<T>
            Console.WriteLine("\n--- Пример использования обобщенного класса Box<T> ---");
            
            // Создаем экземпляры Box для разных типов
            Box<int> intBox = new Box<int>(42);
            Box<string> stringBox = new Box<string>("Hello, Generics!");
            Box<double> doubleBox = new Box<double>(3.14159);
            
            Console.WriteLine($"Box<int>: {intBox}");
            Console.WriteLine($"Box<string>: {stringBox}");
            Console.WriteLine($"Box<double>: {doubleBox}");
            
            // Изменяем содержимое через свойство
            intBox.Item = 100;
            stringBox.Item = "C# is awesome!";

            Console.WriteLine($"\nПосле изменения:");
            Console.WriteLine($"Box<int>: {intBox}");
            Console.WriteLine($"Box<string>: {stringBox}");

            // Демонстрация класса Pair<TKey, TValue>
            Console.WriteLine("\n--- Пример использования обобщенного класса с несколькими параметрами типа ---");
            
            Pair<int, string> pair1 = new Pair<int, string>(1, "Один");
            Pair<string, bool> pair2 = new Pair<string, bool>("Готово", true);
            
            Console.WriteLine(pair1);  // [1] = Один
            Console.WriteLine(pair2);  // [Готово] = True

            // Демонстрация собственной коллекции SimpleList<T>
            Console.WriteLine("\n--- Использование собственной коллекции SimpleList<T> ---");
            
            SimpleList<string> names = new SimpleList<string>();
            names.Add("Алексей");
            names.Add("Мария");
            names.Add("Иван");
            names.Add("Анна");
            
            Console.WriteLine($"В списке {names.Count} элемента(ов):");
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($"[{i}] = {names[i]}");
            }
            
            Console.WriteLine("\nВставка 'Елена' по индексу 2:");
            names.Insert(2, "Елена");
            
            Console.WriteLine($"В списке {names.Count} элемента(ов):");
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($"[{i}] = {names[i]}");
            }
            
            Console.WriteLine("\nУдаление элемента по индексу 1:");
            names.RemoveAt(1);
            
            Console.WriteLine($"В списке {names.Count} элемента(ов):");
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine($"[{i}] = {names[i]}");
            }

            // Демонстрация коллекции с поддержкой IEnumerable<T>
            Console.WriteLine("\n--- Использование GenericCollection<T> с поддержкой foreach ---");
            
            GenericCollection<int> numbers = new GenericCollection<int>();
            numbers.Add(10);
            numbers.Add(20);
            numbers.Add(30);
            numbers.Add(40);
            numbers.Add(50);
            
            Console.WriteLine("Элементы коллекции через foreach:");
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
            
            Console.WriteLine("\nУдаление элемента со значением 30:");
            numbers.Remove(30);
            
            Console.WriteLine("Элементы коллекции после удаления:");
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }

            // Демонстрация производительности
            Console.WriteLine("\n--- Сравнение производительности собственной коллекции с List<T> ---");
            
            int itemCount = 100000;
            
            // Тестирование стандартного List<T>
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            List<int> standardList = new List<int>();
            for (int i = 0; i < itemCount; i++)
            {
                standardList.Add(i);
            }
            
            sw.Stop();
            Console.WriteLine($"Добавление {itemCount} элементов в List<T>: {sw.ElapsedMilliseconds} мс");
            
            // Тестирование нашей GenericCollection<T>
            sw.Restart();
            
            GenericCollection<int> customCollection = new GenericCollection<int>();
            for (int i = 0; i < itemCount; i++)
            {
                customCollection.Add(i);
            }
            
            sw.Stop();
            Console.WriteLine($"Добавление {itemCount} элементов в GenericCollection<T>: {sw.ElapsedMilliseconds} мс");
            
            Console.WriteLine("\nОбратите внимание, что наша реализация может быть медленнее стандартной библиотеки,");
            Console.WriteLine("так как стандартные коллекции в .NET оптимизированы командой Microsoft.");
        }
    }

    #region Задачи
    /*
        # Создайте обобщенный класс Stack<T>, реализующий операции:
          - Push(T item) - добавить элемент в стек
          - T Pop() - извлечь элемент из стека
          - T Peek() - просмотреть верхний элемент без извлечения
          - bool IsEmpty() - проверить, пуст ли стек
          - int Count - свойство, возвращающее количество элементов
        
        # Создайте обобщенный класс Pair<T> для хранения пары однотипных значений.
          Класс должен содержать:
          - Два поля/свойства First и Second типа T
          - Конструктор, принимающий два значения типа T
          - Метод Swap(), который меняет местами значения First и Second
        
        # Реализуйте обобщенный метод Swap<T>(ref T a, ref T b), который меняет
          местами значения двух переменных. Напишите примеры использования этого метода
          с разными типами данных.
        
        # Создайте обобщенный класс CircularBuffer<T>, реализующий циклический буфер
          фиксированной емкости. Когда буфер заполняется, новые элементы записываются
          поверх старых. Реализуйте методы:
          - Add(T item) - добавить элемент в буфер
          - T Get(int index) - получить элемент по индексу
          - int Count - текущее количество элементов
          - int Capacity - емкость буфера
        
        # Реализуйте обобщенный класс Repository<T> с ограничением where T : class, new(),
          который эмулирует простое хранилище объектов. Класс должен иметь методы:
          - Add(T item) - добавить объект в хранилище
          - Remove(T item) - удалить объект
          - GetAll() - вернуть все объекты
        
          
        # СЛОЖНОЕ ЗАДАНИЕ: Реализуйте свою версию LinkedList<T> с поддержкой
          двунаправленного списка. Создайте внутренний класс Node<T> для узлов.
          Реализуйте методы:
          - AddFirst(T item), AddLast(T item) - добавление в начало и конец списка
          - RemoveFirst(), RemoveLast() - удаление из начала и конца
          - Find(T item) - поиск узла, содержащего значение
          - Реализуйте IEnumerable<T> для поддержки foreach

        # СЛОЖНОЕ ЗАДАНИЕ: Создайте обобщенный класс BinarySearchTree<T>,
          реализующий бинарное дерево поиска, с ограничением where T : IComparable<T>.
          Реализуйте методы:
          - Add(T item) - добавление элемента
          - Contains(T item) - проверка наличия элемента
          - Remove(T item) - удаление элемента
          - InOrderTraversal() - обход дерева по возрастанию значений
    */
    #endregion
}
