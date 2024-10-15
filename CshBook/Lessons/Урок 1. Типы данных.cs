﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CshBook.Lessons
{
    #region Теория
    /*
     В этом уроке ты базово узнаешь, как работать в C# с стандартными типами данных,
     а так же чуть чуть о том, как вообще работать в этом языке
    */

    // Как ты видишь, тут представлены два основных типа комментариев в коде.
    // Выше - для нескольких строк сразу. А тут построчно.

    /*
        Язык C# обладает статической типизацией. 
        Это значит, что когда мы заводим переменную, 
        то сразу должны указать её тип данных.

        Так же C# - строго типизированный. Если у тебя переменна одного типа, то она не может стать другим.
        В обход этому есть dynamic. Но, его использование пока что нам не нужно.
        
        По совместительству типизация тут явная, что значит что перед названием переменой нужно писать её тип.
        Можно испльзовать неявную, тогда надо писать "var"

        Так же C# - полностью объектно-ориентируемый. Но, об этом мы поговорим, когда затронем ООП.
        Пока что не обращай внимание на то, где есть слова "class", "void", "namespace", "using" и тд.

        Давай чуть поговорим про принятые стандарты "красоты".
        Принято блоки кода, которые находятся в {} делать с отступами.
        Для того, чтобы сделать отступ, нужно нажать TAB.
        Для обратного действия Shift + TAB.

        Так же все "строчки" нужно заканчивать ";", такие тут правила.



        **Переменная - контейнер для хранения информации, а так же работы с данными. 
        Пример: int a = 5;
        Тут a - переменная целочисленного типа, которая хранит значение 5.
        Переменная может называться почти как угодно. Желательно начинать писать со строчной буквы.
        Если имя переменной содержит несколько слов в себе, то в C# принято следующее слово начинать с заглавной. Например 
        string someText = "какой-то текст". 
        Имя переменной обязательно должно писаться слитно. Есть другой стандарт написания через нижнее подчёркивание, например: some_text.
        Однако, в C# чаще используют первый вариант.
        
     */

    /*
        Итак, наша тема - "Типы данных". Они тебе хорошо известны, давай просто повторим их.

        Основные, которые ты точно будешь часто встречать:

        int (integer): хранит целое число от -2147483648 до 2147483647 и занимает 4 байта.
        string: хранит набор символов Unicode. 
        char: хранит одиночный символ в кодировке Unicode и занимает 2 байта.
        bool: хранит значение true или false
        double: хранит число с плавающей точкой от ±5.0*10-324 до ±1.7*10308 и занимает 8 байта.
        
        Все остальные, тоже бывают полезными, но встречаются не так часто:
        
        byte: хранит целое число от 0 до 255 и занимает 1 байт. 
        sbyte: хранит целое число от -128 до 127 и занимает 1 байт. 
        short: хранит целое число от -32768 до 32767 и занимает 2 байта. 
        ushort: хранит целое число от 0 до 65535 и занимает 2 байта.
        uint: хранит целое число от 0 до 4294967295 и занимает 4 байта. 
        long: хранит целое число от –9 223 372 036 854 775 808 до 9 223 372 036 854 775 807 и занимает 8 байт.
        ulong: хранит целое число от 0 до 18 446 744 073 709 551 615 и занимает 8 байт. 
        float: хранит число с плавающей точкой от -3.4*1038 до 3.4*1038 и занимает 4 байта. 
        decimal: хранит десятичное дробное число. Если употребляется без десятичной запятой, имеет значение от ±1.0*10-28 до ±7.9228*1028, может хранить 28 знаков после запятой и занимает 16 байт.
        
     */

    /*
        Для преобразования одного типа данных в другой есть несколько способов.

        Расскажу тебе про самый простой - Parse. Вот его структура - <тип данных в который преобразуем>.Parse(<строка, которую преобразуем>).
        Например, у нас есть строка - 
        string strToInt = "123"
        Тогда, если мы напишем следующее - 
        int num = int.Parse(strToInt); 
        Однако, если в строке буде ошибка, например "123Б", то это приведёт к ошибке



        С числами всё проще. Можно просто неявно преобразовать из более обширного типа в менее, который включает в себя более обширный.
        Например, 
        int a = 1;
        double b = a;
        Однако, чтобы преобразовать наоборот, нужно использовать такую конструкцию: (<тип, к которому меняем>).<значение>.
        Например,
        int c = (int)b;
        В таком случае часть данных будет отброшена. Лучше не злоупотреблять этим, так как это может привести к ошибкам.

        У всех типов данных есть функция ToString(). 
        Например,
        string cstring = c.ToString();
        Тут "c" запишется как строка
     */

    /*
        Вроде разобрались. Но, нам же нужно как-то получать от пользователя данные, а так же выводить их.
        Для получения используется функция Console.ReadLine(), которая получает данные, введённые пользователям в консоли в строку.
        
        Пример - 
        string text = Console.ReadLine();

        Для вывода данных используется Console.Write(), или, если мы сразу хотим после вывода перейти на другую строку, то Console.WriteLine().
        Пример - 
        Console.WriteLine("Привет, мир!") - выведет в консоль "Привет, мир!".
     */

    #endregion

    internal static class FirstLesson // Урок 1. Типы данных
    {
        static void Main()
        {
            // Пройдёмся по всем типам данных
            int a = 23131;
            string b = "some text"; // строка всегда пишется с двойными кавычками
            char c = 'a'; // символ всегда пишется с одинарными кавычками
            bool d = true;
            double e = 3.4;

            byte f = 23;
            sbyte g = -12;
            short h = 533;
            ushort i = 0;
            uint j = 0;
            long k = 0;
            ulong l = 0;
            float m = 0.4f; // у float в конце числа нужно ставить F/f
            decimal n = 324.12M; // у decimal в конце числа нужно ставить M/m


            // В C# можно сначала объявить переменную, а потом уже присвоить им значение
            // Так же можно сразу объявлять несколько переменных одного типа
            int it_1, it_2 = 5;
            it_1 = 0;


            // Теперь, поговорим про математические операции
            int int_1 = 10 + 5; // сложение. ответ - 15
            int int_2 = 10 - 5; // вычитание. ответ - 5
            int int_3 = 10 * 5; // умножение. ответ - 50
            int int_4 = 10 / 5; // деление. ответ - 2
            int int_5 = 10 % 3; // остаток от деления. ответ - 1


            // Операции со строками
            string str_1 = "Hello" + " " + "World" + "!"; // Сложение. Ответ - Hello World! 
            string str_2 = "Hello " + 10; // Сложение не со строкой. У не строки неявно вызывается ToString(). Ответ - Hello 10
            string str_3 = "Hello " + 10 + 10; // Тут будет Hello 1010. Тк всё приводится к строке
            string name_ = "Anton";
            int age_ = 18;
            string res_4 = $"Hello {name_} you are {age_}"; // f-строки. Позволяют используя фигурные скобки указывать в строке переменные, у которых неявно будет применён ToString(). Для использования f-строк, нужно поставить знак $
            // есть ещё много способов работать со строками, он это основные. Остальные можно будет найти в интернете, если они будут нужны.


            // Логические операции
            bool bool_1 = 2 == 1; // Сравнение чисел. Ответ - false
            bool bool_2 = "hello" == "Hello"; // Сравнение строк. Вернёт - false
            bool bool_3 = 10 != 5; // отрицание равенства. Вернёт - true
            bool bool_4 = 5 == 5.0 || 10 < 5; // логическое ИЛИ, возвращает true если хоть одно условие выполняется. Вернёт - true
            bool bool_5 = 10 == 10.0 && 5 == 10; // логическое И. Возвращает true только если все условия выполнились. Вернёт - false
            // Так же есть знаки: < (меньше), > (больше), <= (меньше или равно), >= ()


            // Напишем программу, которая примет имя и возраст пользователя, и поприветствует его, используя то, что мы прошли.
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());

            Console.WriteLine($"Привет {name}! Тебе {age} лет, ты уже такой большой!");           
        }
    }

    #region Задачи
    /*
        # Пользователь вводит два числа, нужно найти их сумму
        # Пользователь вводит число, поделить это число на 5 без остатка
        # Пользователь вводит число, найти остаток от деления 100 на это число
        # Записать данные из задач 1-3 и красиво вывести их через f-строку в консоль
        # Пользователь вводит возраст. Вывести True, если чему больше 18, иначе else
     */
    #endregion

}
