namespace CshBook.Lessons.Chapter1.Lesson11GenericMethods
{
    #region Теория
    /*
        До этого момента, если логика была одинаковой,
        но типы отличались, мы писали несколько похожих методов.

        Например:
        - один метод для int;
        - второй почти такой же для string;
        - третий для double.

        Обобщенный метод позволяет не дублировать такую логику.
     */

    /*
        Обобщенный метод - это метод с параметром типа.

        Обычно его записывают так:
        public static void PrintValue<T>(T value)

        Здесь T - это не конкретный тип,
        а временное имя для "какого-то типа",
        который станет известен при вызове метода.
     */

    /*
        Ментальная модель простая:
        компилятор как будто подставляет конкретный тип на место T.

        То есть:
        PrintValue(10)
        PrintValue("Привет")

        Один и тот же метод работает и для int, и для string.
     */

    /*
        Обобщенные методы особенно полезны, когда логика одна и та же:

        - вывести значение;
        - пройти по массиву;
        - поменять местами два значения;
        - вернуть первый элемент массива;
        - создать массив, заполненный одним значением.
     */

    /*
        Важно:
        обобщенный метод не делает "что угодно".
        Он все равно должен решать одну понятную задачу.

        Если логика сама по себе разная для разных типов,
        generics не нужны.
     */

    /*
        На этом уроке держим только базу:
        - один параметр типа T;
        - простые методы для значений и массивов;
        - без сложных ограничений where.

        Сейчас важно почувствовать саму идею универсального метода,
        а не учиться писать библиотеку общего назначения.
     */
    #endregion

    internal static class Lesson11GenericMethods
    {
        public static void PrintValue<T>(T value)
        {
            Console.WriteLine($"Значение: {value}");
        }

        public static void PrintArray<T>(T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Console.Write($"{values[i]} ");
            }
            Console.WriteLine();
        }

        public static T GetFirst<T>(T[] values)
        {
            return values[0];
        }

        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        public static T[] CreateFilledArray<T>(int size, T value)
        {
            T[] result = new T[size];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = value;
            }

            return result;
        }

        public static bool Contains<T>(T[] values, T target)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (Equals(values[i], target))
                {
                    return true;
                }
            }

            return false;
        }

        public static void Main_()
        {
            PrintValue(42);
            PrintValue("Привет, generics!");
            PrintValue(true);

            Console.WriteLine("----");

            int[] numbers = { 1, 2, 3, 4 };
            string[] words = { "apple", "banana", "cherry" };

            PrintArray(numbers);
            PrintArray(words);

            Console.WriteLine("----");

            Console.WriteLine($"Первый элемент numbers: {GetFirst(numbers)}");
            Console.WriteLine($"Первый элемент words: {GetFirst(words)}");

            Console.WriteLine("----");

            int left = 5;
            int right = 10;
            Swap(ref left, ref right);
            Console.WriteLine($"После Swap: left = {left}, right = {right}");

            Console.WriteLine("----");

            string[] emptyWords = CreateFilledArray(3, "empty");
            PrintArray(emptyWords);

            Console.WriteLine("----");

            Console.WriteLine($"Есть ли 3 в numbers: {Contains(numbers, 3)}");
            Console.WriteLine($"Есть ли 'orange' в words: {Contains(words, "orange")}");
        }
    }

    #region Задачи
    /*
        Разминка

        1. Вывод значения.
           Напиши метод PrintValue<T>(T value),
           который выводит переданное значение.

        2. Вывод массива.
           Напиши метод PrintArray<T>(T[] values),
           который выводит все элементы массива.

        3. Первый элемент.
           Напиши метод GetFirst<T>(T[] values),
           который возвращает первый элемент массива.
           Считай, что массив не пустой.

        Основные задачи

        4. Последний элемент.
           Напиши метод GetLast<T>(T[] values),
           который возвращает последний элемент массива.

        5. Обмен значений.
           Напиши метод Swap<T>(ref T left, ref T right),
           который меняет местами два значения любого типа.

        6. Создание заполненного массива.
           Напиши метод CreateFilledArray<T>(int size, T value),
           который создает массив заданной длины и заполняет его одним значением.

        7. Поиск элемента.
           Напиши метод Contains<T>(T[] values, T target),
           который возвращает true, если элемент есть в массиве.

        8. Подсчет совпадений.
           Напиши метод CountOccurrences<T>(T[] values, T target),
           который считает, сколько раз элемент встречается в массиве.

        Задача на перенос

        9. Копирование массива.
           Напиши метод CopyArray<T>(T[] source),
           который возвращает новый массив с теми же элементами.

        10. Необязательная задача.
            Напиши метод PrintReversed<T>(T[] values),
            который выводит элементы массива в обратном порядке.
     */
    #endregion
}
