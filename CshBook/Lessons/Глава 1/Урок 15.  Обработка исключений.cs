namespace CshBook.Lessons.Chapter1.Lesson15ExceptionHandling
{
    #region Теория
    /*
        Исключение - это ошибка, которая происходит во время выполнения программы.

        Например:
        - деление на ноль;
        - неверный формат числа;
        - попытка открыть несуществующий файл;
        - выход за границы массива.
     */

    /*
        Если исключение не обработать,
        программа может аварийно завершиться.

        Для обработки используется конструкция:

        try
        {
            код, в котором может быть ошибка
        }
        catch
        {
            что делать, если ошибка произошла
        }
     */

    /*
        Смысл такой:

        - try -> пробуем выполнить опасный код;
        - catch -> перехватываем ошибку;
        - finally -> выполняем завершающие действия в любом случае.
     */

    /*
        finally особенно полезен,
        когда нужно обязательно освободить ресурс:

        - закрыть файл;
        - завершить работу с потоком;
        - убрать временные данные.

        Позже ты познакомишься с using.
        Он часто закрывает ресурсы удобнее,
        но сначала важно понять саму идею finally.
     */

    /*
        На раннем этапе важно различать две идеи:

        1. Ошибка ожидаемая и понятная.
           Например, пользователь может ввести не число.
           Здесь обработка исключения уместна.

        2. Ошибка проектирования.
           Например, логика программы сама неверная.
           Здесь try/catch не должен скрывать проблему.
     */

    /*
        throw нужен, когда ты сам хочешь явно сообщить об ошибке.

        Например:
        если возраст отрицательный,
        можно выбросить ArgumentException.
     */

    /*
        Если catch несколько, порядок важен.

        Сначала ставят более конкретные ошибки:

        catch (FormatException)
        catch (DivideByZeroException)

        А общий catch (Exception) ставят внизу.

        Почему так:
        Exception - это слишком общий тип.
        Если поставить его первым,
        он перехватит почти все ошибки,
        и более точные catch уже не сработают.
     */

    /*
        Важно помнить про Parse и TryParse.

        int.Parse(text) кидает исключение,
        если строка не похожа на число.

        int.TryParse(text, out int number)
        не кидает исключение,
        а возвращает true или false.

        Для обычной проверки пользовательского ввода
        TryParse часто удобнее.

        Но на этом уроке мы специально разбираем Parse через try/catch,
        чтобы понять сам механизм исключений.
     */

    /*
        Главное правило урока:
        try/catch нужен не для того,
        чтобы "замолчать" любую ошибку,
        а чтобы корректно обработать конкретную ситуацию.

        То есть после catch программа должна вести себя понятно:
        сообщить об ошибке, вернуть безопасный результат
        или завершить текущую операцию без падения.
     */
    #endregion

    internal static class Lesson15ExceptionHandling
    {
        public static void SafeDivide(int left, int right)
        {
            try
            {
                int result = left / right;
                Console.WriteLine($"Результат: {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Делить на ноль нельзя.");
            }
        }

        public static void ParseNumber(string text)
        {
            try
            {
                int number = int.Parse(text);
                Console.WriteLine($"Число: {number}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Строку нельзя преобразовать в число.");
            }
        }

        public static void ParseNumberWithTryParse(string text)
        {
            if (int.TryParse(text, out int number))
            {
                Console.WriteLine($"Число: {number}");
            }
            else
            {
                Console.WriteLine("TryParse: строка не является числом.");
            }
        }

        public static void PrintArrayItem(int[] numbers, int index)
        {
            try
            {
                Console.WriteLine($"Элемент: {numbers[index]}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Такого индекса в массиве нет.");
            }
        }

        public static void ValidateAge(int age)
        {
            if (age < 0)
            {
                throw new ArgumentException("Возраст не может быть отрицательным.");
            }

            Console.WriteLine($"Возраст: {age}");
        }

        public static void ShowFileExample()
        {
            string path = "lesson15-demo.txt";
            StreamWriter? writer = null;

            try
            {
                writer = new StreamWriter(path);
                writer.WriteLine("Пример работы finally.");
                Console.WriteLine("Файл успешно записан.");
            }
            finally
            {
                writer?.Close();
                Console.WriteLine("Поток закрыт.");
            }
        }

        public static void Main_()
        {
            SafeDivide(10, 2);
            SafeDivide(10, 0);

            Console.WriteLine("----");

            ParseNumber("42");
            ParseNumber("abc");
            ParseNumberWithTryParse("55");
            ParseNumberWithTryParse("text");

            Console.WriteLine("----");

            int[] numbers = { 5, 7, 9 };
            PrintArrayItem(numbers, 1);
            PrintArrayItem(numbers, 5);

            Console.WriteLine("----");

            try
            {
                ValidateAge(25);
                ValidateAge(-3);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("----");

            ShowFileExample();
        }
    }

    #region Задачи
    /*
        Разминка

        1. Деление с защитой.
           Напиши метод SafeDivide(int left, int right),
           который делит одно число на другое
           и обрабатывает DivideByZeroException.

        2. Преобразование строки в число.
           Напиши метод ParseNumber(string text),
           который пытается преобразовать строку в int
           и обрабатывает FormatException.

        3. Доступ к элементу массива.
           Напиши метод PrintArrayItem(int[] numbers, int index),
           который выводит элемент массива
           и обрабатывает IndexOutOfRangeException.

        Основные задачи

        4. Проверка возраста.
           Напиши метод ValidateAge(int age),
           который выбрасывает ArgumentException,
           если возраст отрицательный.

        5. TryParse без исключения.
           Напиши метод ParseNumberWithTryParse(string text),
           который делает похожую проверку через int.TryParse
           и не использует try/catch.

        6. Чтение файла.
           Напиши метод ReadFile(string path),
           который пытается прочитать файл
           и обрабатывает FileNotFoundException.

        7. Калькулятор без падения.
           Напиши метод Calculate(double left, double right, char operation),
           который выполняет +, -, *, /
           и не падает на неверной операции или делении на ноль.

        8. Безопасный ввод индекса.
           Напиши метод GetItemOrMessage(int[] numbers, int index),
           который возвращает элемент массива
           или понятное сообщение об ошибке.

        9. finally на практике.
           Напиши метод WriteTextToFile(string path, string text),
           который записывает строку в файл
           и в finally закрывает поток.

        Задачи на перенос

        10. Проверка скидки.
           Напиши метод ValidateDiscount(int discount),
           который выбрасывает ArgumentException,
           если скидка меньше 0 или больше 100.

        11. Безопасная цепочка.
            Напиши небольшой сценарий:
            получить строку, преобразовать ее в число,
            проверить число и вывести результат,
            не допуская аварийного завершения программы.
     */
    #endregion
}
