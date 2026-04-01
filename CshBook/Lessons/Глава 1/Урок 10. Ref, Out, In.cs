namespace CshBook.Lessons.Chapter1.Lesson10RefOutIn
{
    #region Теория
    /*
        До этого момента параметры методов передавались по значению.

        Это значит:
        метод получает свою копию значения
        и работает уже с ней.

        Поэтому изменение параметра внутри метода
        обычно не меняет исходную переменную снаружи.
     */

    /*
        ref нужен, когда метод должен получить доступ
        к уже существующей переменной и изменить ее.

        Пример:
        int price = 100;
        AddBonus(ref price);

        После вызова изменится именно переменная price.

        Важно:
        переменная должна быть инициализирована до вызова метода.
     */

    /*
        out тоже передает переменную наружу,
        но с другой идеей:
        метод обязан сам записать в нее значение.

        Это удобно, когда метод должен вернуть дополнительный результат
        или когда основной ответ может получиться не всегда.

        Важно:
        переменную до вызова можно не инициализировать,
        но внутри метода out-параметру обязательно надо присвоить значение.
     */

    /*
        in - это "только чтение".

        Метод получает доступ к значению,
        но менять его не может.

        На раннем этапе достаточно понимать идею:
        - ref -> можно читать и менять
        - out -> нужно обязательно записать
        - in -> можно только читать

        in пока воспринимай как обзорный инструмент.
     */

    /*
        Где это реально встречается:

        - ref: обмен значений, изменение числа, накопление результата;
        - out: безопасное деление, TryParse, возврат дополнительного значения;
        - in: аккуратное чтение данных без изменения.
     */

    /*
        Типичная ошибка новичка:
        использовать ref и out там, где можно обойтись обычным return.

        Сначала всегда подумай:
        "можно ли просто вернуть результат из метода?"

        Если нужен один результат, чаще всего return проще.
        ref/out полезны там, где действительно нужен доступ к переменной снаружи.
     */
    #endregion

    internal static class Lesson10RefOutIn
    {
        public static void Swap(ref int left, ref int right)
        {
            int temp = left;
            left = right;
            right = temp;
        }

        public static void AddBonus(ref int balance, int bonus)
        {
            balance += bonus;
        }

        public static bool TryDivide(int left, int right, out int result)
        {
            if (right == 0)
            {
                result = 0;
                return false;
            }

            result = left / right;
            return true;
        }

        public static void GetMinMax(int first, int second, out int min, out int max)
        {
            if (first < second)
            {
                min = first;
                max = second;
            }
            else
            {
                min = second;
                max = first;
            }
        }

        public static void PrintValue(in int value)
        {
            Console.WriteLine($"Только читаем значение: {value}");
        }

        public static void Main_()
        {
            int left = 3;
            int right = 8;
            Console.WriteLine($"До swap: left = {left}, right = {right}");
            Swap(ref left, ref right);
            Console.WriteLine($"После swap: left = {left}, right = {right}");

            Console.WriteLine("----");

            int balance = 100;
            AddBonus(ref balance, 25);
            Console.WriteLine($"Баланс после бонуса: {balance}");

            Console.WriteLine("----");

            if (TryDivide(20, 5, out int divisionResult))
            {
                Console.WriteLine($"Результат деления: {divisionResult}");
            }
            else
            {
                Console.WriteLine("Деление выполнить нельзя");
            }

            Console.WriteLine("----");

            GetMinMax(14, 9, out int min, out int max);
            Console.WriteLine($"Минимум: {min}, максимум: {max}");

            Console.WriteLine("----");

            string text = "27";
            bool parsed = int.TryParse(text, out int age);
            Console.WriteLine($"TryParse сработал: {parsed}, возраст: {age}");

            Console.WriteLine("----");

            int number = 50;
            PrintValue(in number);
            Console.WriteLine($"После in число не изменилось: {number}");
        }
    }

    #region Задачи
    /*
        Разминка

        1. Обмен значений.
           Напиши метод Swap(ref int a, ref int b),
           который меняет местами два числа.

        2. Увеличение значения.
           Напиши метод AddBonus(ref int value, int bonus),
           который увеличивает число на bonus.

        3. Сделать число положительным.
           Напиши метод MakePositive(ref int value),
           который делает число положительным, если оно было отрицательным.

        Основные задачи

        4. Безопасное деление.
           Напиши метод TryDivide(int a, int b, out int result).
           Если деление возможно, метод должен вернуть true,
           а в result записать результат.
           Если b == 0, метод должен вернуть false.

        5. Минимум и максимум.
           Напиши метод GetMinMax(int a, int b, out int min, out int max),
           который находит минимум и максимум двух чисел.

        6. TryParse-обертка.
           Напиши метод TryParseAge(string text, out int age),
           который пытается преобразовать строку в возраст.
           Можно использовать int.TryParse внутри как готовый инструмент.

        7. Курс валют.
           Напиши метод ConvertRubles(int rubles, out int dollars, out int remainder),
           который переводит рубли в условные доллары по фиксированному курсу.

        Задачи на обзор

        8. Чтение через in.
           Напиши метод PrintNumber(in int value),
           который просто выводит число и не изменяет его.

        9. Проверка диапазона через in.
           Напиши метод IsBetween(in int value, in int left, in int right),
           который возвращает true, если число входит в диапазон.

        10. Необязательная задача.
            Напиши метод PrintPair(in int first, in int second),
            который красиво выводит два числа, не изменяя их.
     */
    #endregion
}
