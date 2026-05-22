namespace CshBook.Lessons.Chapter2.Lesson20StaticClasses
{
    #region Теория
    /*
        Обычный класс нужен, когда мы создаем объекты.

        Например:
        Pet bobik = new Pet();
        Pet sharik = new Pet();

        У каждого объекта свои поля и свое состояние.
     */

    /*
        Статический класс нужен для логики,
        которой не нужен отдельный объект.

        Например:
        - математические операции;
        - работа со строками;
        - простые проверки;
        - методы для массивов;
        - вывод служебных сообщений.
     */

    /*
        Статический класс объявляется через static:

        static class MathHelper
        {
            public static int Add(int a, int b)
            {
                return a + b;
            }
        }

        Вызывается такой метод через имя класса:

        MathHelper.Add(2, 3);
     */

    /*
        Важное отличие:
        статический класс нельзя создать через new.

        Нельзя:
        MathHelper helper = new MathHelper();

        Потому что у статического класса нет экземпляров.
     */

    /*
        Статические поля существуют в одном экземпляре на всю программу.

        Это удобно для констант и настроек,
        но опасно для изменяемых глобальных данных.

        Если все части программы меняют одно статическое поле,
        становится сложнее понять, кто и когда изменил значение.
     */

    /*
        Простое правило:

        static хорошо подходит для чистых вспомогательных методов,
        которые получают данные через параметры
        и возвращают результат.

        Если объект должен хранить свое состояние,
        лучше использовать обычный класс.
     */
    #endregion

    class Calculator
    {
        public int Add(int left, int right)
        {
            return left + right;
        }
    }

    static class MathHelper
    {
        public static int Add(int left, int right)
        {
            return left + right;
        }

        public static int Multiply(int left, int right)
        {
            return left * right;
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }
    }

    static class ArrayHelper
    {
        public static string Separator = ", ";

        public static int FindMax(int[] numbers)
        {
            int max = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        public static string ConvertToString(int[] numbers)
        {
            string result = "";

            for (int i = 0; i < numbers.Length; i++)
            {
                result += numbers[i];

                if (i < numbers.Length - 1)
                {
                    result += Separator;
                }
            }

            return result;
        }
    }

    internal static class Lesson20StaticClasses
    {
        public static void Main_()
        {
            Calculator calculator = new Calculator();
            Console.WriteLine(calculator.Add(2, 3));

            Console.WriteLine("----");

            Console.WriteLine(MathHelper.Add(2, 3));
            Console.WriteLine(MathHelper.Multiply(4, 5));
            Console.WriteLine(MathHelper.IsEven(10));

            Console.WriteLine("----");

            int[] numbers = { 4, 9, 2, 7 };
            Console.WriteLine(ArrayHelper.FindMax(numbers));
            Console.WriteLine(ArrayHelper.ConvertToString(numbers));

            ArrayHelper.Separator = " | ";
            Console.WriteLine(ArrayHelper.ConvertToString(numbers));
        }
    }

    #region Задачи
    /*
        Разминка

        1. Математический помощник.
           Создай статический класс MathUtils
           с методами Add, Subtract и Multiply.

        2. Проверка числа.
           Добавь в MathUtils метод IsEven(int value),
           который возвращает true, если число четное.

        3. Помощник для строк.
           Создай статический класс StringUtils
           с методом Repeat(string text, int count),
           который повторяет строку несколько раз.

        Основные задачи

        4. Максимум массива.
           Создай статический класс ArrayUtils
           с методом FindMax(int[] numbers).

        5. Сумма массива.
           Добавь метод Sum(int[] numbers),
           который возвращает сумму элементов.

        6. Строка из массива.
           Добавь метод ConvertToString(int[] numbers),
           который превращает массив в строку.

        7. Разделитель.
           Добавь в ArrayUtils статическое поле Separator.
           Используй его в ConvertToString.

        8. Настройки приложения.
           Создай статический класс AppSettings
           со статическими полями AppName и Version.

        Задачи на перенос

        9. Проверки пользователя.
           Создай статический класс UserValidator
           с методами IsValidAge(int age) и IsValidName(string name).

        10. Геометрия.
            Создай статический класс GeometryUtils
            с методами GetRectangleArea и GetCircleArea.
     */
    #endregion
}
