namespace CshBook.Lessons.Chapter0.Lesson09OverloadsAndParams
{
    #region Теория
    /*
        Этот урок продолжает тему методов.

        До этого мы писали отдельный метод под каждую задачу.
        Теперь посмотрим на два удобных инструмента:

        - перегрузка - когда у методов одно имя, но разные параметры;
        - params - когда метод умеет принимать переменное количество аргументов.
     */

    /*
        Перегрузка нужна, когда действие по смыслу одно и то же,
        но входные данные немного отличаются.

        Например:
        - вывести приветствие по имени;
        - вывести приветствие по имени и возрасту;
        - посчитать сумму двух чисел;
        - посчитать сумму трех чисел.

        Во всех этих случаях удобно сохранить одно имя метода.
     */

    /*
        При перегрузке компилятор смотрит именно на параметры:

        - количество параметров;
        - типы параметров;
        - порядок параметров.

        Только тип возвращаемого значения для перегрузки не подходит.

        То есть так нельзя:
        int Add(int a, int b)
        double Add(int a, int b)
     */

    /*
        params нужен тогда, когда заранее неизвестно,
        сколько значений передадут в метод.

        Пример:
        Sum(1, 2)
        Sum(1, 2, 3, 4)
        Sum()

        Внутри метода params работает как обычный массив.
        Значит его можно обходить циклом.
     */

    /*
        Важные правила для params:

        - params может быть только один;
        - параметр params должен стоять последним;
        - params принимает значения одного типа.

        Пример:
        public static int Sum(params int[] numbers)
     */

    /*
        Когда использовать:

        - перегрузку, если вариантов немного и они действительно про одно действие;
        - params, если различается только количество однотипных аргументов.

        Не стоит делать перегрузки просто ради красоты.
        Если методы начинают делать разную логику, лучше дать им разные имена.
     */
    #endregion

    internal static class Lesson09OverloadsAndParams
    {
        public static void PrintGreeting(string name)
        {
            Console.WriteLine($"Привет, {name}!");
        }

        public static void PrintGreeting(string name, int age)
        {
            Console.WriteLine($"Привет, {name}! Тебе {age} лет.");
        }

        public static int Add(int a, int b)
        {
            return a + b;
        }

        public static int Add(int a, int b, int c)
        {
            return a + b + c;
        }

        public static int SumAll(params int[] numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        public static void PrintPurchases(string title, params string[] items)
        {
            Console.WriteLine(title);

            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine($"- {items[i]}");
            }
        }

        public static void Main_()
        {
            PrintGreeting("Анна");
            PrintGreeting("Анна", 16);

            Console.WriteLine("----");

            Console.WriteLine($"Сумма двух чисел: {Add(4, 5)}");
            Console.WriteLine($"Сумма трех чисел: {Add(4, 5, 6)}");

            Console.WriteLine("----");

            Console.WriteLine($"Сумма через params: {SumAll(1, 2, 3, 4)}");
            Console.WriteLine($"Сумма пустого набора: {SumAll()}");

            int[] values = { 10, 20, 30 };
            Console.WriteLine($"Сумма массива: {SumAll(values)}");

            Console.WriteLine("----");

            PrintPurchases("Покупки:", "Хлеб", "Молоко", "Сыр");
        }
    }

    #region Задачи
    /*
        Разминка

        1. Перегруженное приветствие.
           Напиши два метода с одним именем:
           - первый принимает имя и выводит "Привет, [имя]!";
           - второй принимает имя и возраст и выводит строку с обоими значениями.

        2. Сумма двух и трех чисел.
           Напиши перегруженные методы Add:
           - Add(int a, int b)
           - Add(int a, int b, int c)

        3. Максимум.
           Напиши перегруженные методы GetMax:
           - для двух чисел;
           - для трех чисел.

        Основные задачи

        4. Печать чисел через params.
           Напиши метод PrintNumbers(params int[] numbers),
           который выводит все переданные числа в одну строку.

        5. Сумма через params.
           Напиши метод Sum(params int[] numbers),
           который возвращает сумму всех переданных чисел.

        6. Подсчет четных чисел.
           Напиши метод CountEven(params int[] numbers),
           который возвращает количество четных чисел.

        7. Печать покупок.
           Напиши метод PrintPurchases(string title, params string[] items),
           который сначала выводит заголовок, а потом список покупок.

        8. Средний балл.
           Напиши метод GetAverage(params int[] grades),
           который возвращает средний балл.
           Если оценок нет, верни 0.

        Задача на перенос

        9. Карточка пользователя.
           Напиши перегруженные методы BuildProfile:
           - один принимает только имя;
           - второй принимает имя и возраст.
           Оба метода должны возвращать готовую строку.

        10. Мини-чек.
            Напиши метод PrintReceipt(string shopName, params string[] products),
            который печатает название магазина и список товаров.
     */
    #endregion
}
