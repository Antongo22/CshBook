﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons
{
    /* Почему не функция
        Ответ простой, это просто терминология. В C# функция - это метод. 
        Разница в том, что функция - самостоятельна, а метод - функция в классе. 
        C# полностью ООП язык, а следовательно любая функция является методом.
     */

    /* Для чего нужны методы
        Предположим, мы хотим дать пользователю вычислять факториал числа.
        Мы напишем это так - 

        int val = int.Parse(Console.ReadLine());
        int res = 1;
        for (int i = 1; i <= val; i++)
        {
            res *= i;
        }
        Console.WriteLine(res);


        Всё хорошо, но теперь мы хотим ещё раз вычислить факториал в той же программе. Напишем так -
        int val = int.Parse(Console.ReadLine());
        int res = 1;
        for (int i = 1; i <= val; i++)
        {
            res *= i;
        }
        Console.WriteLine(res);

        Console.WriteLine("А теперь ещё раз");

        int val1 = int.Parse(Console.ReadLine());
        int res1 = 1;
        for (int i = 1; i <= val1; i++)
        {
            res1 *= i;
        }
        Console.WriteLine(res1);


        Много кода который делает одно и то же. А что если потом опять нужно будет делать то же самое.
        Чтобы не дублировать код, существуют методы. 
        
     */

    /* Что и как они могут
        Метод может принять необходимые параметры для себя и произвести действия. 
        Потом, он может вернуть значение, использовав ключевое слово return. 
        Не обязательно возвращать что-то, как и что-то предавать, но нужно знать что такая возможность есть.
     
     */

    /* Структура написания методом
     
        <модификатор доступа> <ключевое слово(а)> <тип, который возвращает метод> <имя метода>(<параметры>)
        {   
            <функционал>
            return <значение, которое того же типа что и в названии метода>
        }

        Давай разберём всё по отдельности.

        Модификатор доступа:
        Существует много, нам пока нужны только public и private. Если его не писать, то автоматически будет private.
        public - другие классы могут пользоваться.
        private - можно только внутри класса.

        Ключевые слова:
        Дополнительная информация о методе. Пока ты мог встречать static. Но так же есть и другие.
        static - не нужно создавать экземпляр класса для вызова. Например Console.WriteLine() это статический метод класса Console.
        
        Тип метода:
        Например int, string и другие. Если написать void, то такой метод не будет ничего возвращать.
        
        Имя метода:
        В C# принято писать с большой буквы в формате camelCase. 

        Параметры:
        Передаются через запятую, имеют такую структуру - <ключевое слово(а)> <тип переменной> <имя переменной>
        Ключевыми словами могут быть - ref, out, in, params. Но об этом мы поговорим позже.


        Важно сказать про область видимости. 
        Даже если вне есть переменные с идентичными именами, они не будут доступны внутри метода и буду иметь разные значения.
     */


    internal static class EighthLesson
    {
        public static int Factorial(int val)
        {
            int res = 1;
            for (int i = 1; i <= val; i++)
            {
                res *= i;
            }
            return res;
        }

        public static void Main_()
        {
            int val = int.Parse(Console.ReadLine());
            int res = Factorial(val);
            Console.WriteLine(res);

            Console.WriteLine("А теперь ещё раз");
            int val1 = int.Parse(Console.ReadLine());
            Console.WriteLine(Factorial(val1));
        }
    }
}

/* Задачи (без возврата)

1. Вывод приветствия:
   Напишите функцию, которая принимает имя пользователя и выводит на экран приветствие в формате: "Привет, [имя]!".

2. Вывод таблицы умножения:
   Напишите функцию, которая выводит таблицу умножения для числа, переданного в качестве параметра.

3. Вывод массива:
   Напишите функцию, которая принимает массив целых чисел и выводит его элементы на экран.

4. Вывод чисел от 1 до N:
   Напишите функцию, которая принимает число N и выводит все числа от 1 до N.

5. Вывод чётных чисел:
   Напишите функцию, которая принимает число N и выводит все чётные числа от 1 до N.

6. Вывод суммы элементов массива:
   Напишите функцию, которая принимает массив целых чисел и выводит на экран сумму его элементов.

7. Вывод текущей даты и времени:
   Напишите функцию, которая выводит текущую дату и время на экран.

8. Вывод случайного числа:
   Напишите функцию, которая генерирует и выводит случайное число в диапазоне от 1 до 100.

9. Вывод квадратов чисел:
   Напишите функцию, которая принимает число N и выводит квадраты всех чисел от 1 до N.

10. Вывод обратного массива:
    Напишите функцию, которая принимает массив целых чисел и выводит его элементы в обратном порядке.
*/

/* Задачи (с возвратом)

1. Сумма двух чисел:
   Напишите функцию, которая принимает два числа и возвращает их сумму.

2. Факториал числа:
   Напишите функцию, которая принимает число и возвращает его факториал.

3. Максимальное число в массиве:
   Напишите функцию, которая принимает массив целых чисел и возвращает максимальное число в массиве.

4. Минимальное число в массиве:
   Напишите функцию, которая принимает массив целых чисел и возвращает минимальное число в массиве.

5. Среднее арифметическое массива:
   Напишите функцию, которая принимает массив целых чисел и возвращает среднее арифметическое его элементов.

6. Проверка на чётность:
   Напишите функцию, которая принимает число и возвращает `true`, если число чётное, и `false`, если нечётное.

7. Поиск элемента в массиве:
   Напишите функцию, которая принимает массив и число, и возвращает `true`, если число есть в массиве, и `false`, если нет.

8. Подсчёт количества чётных чисел в массиве:
   Напишите функцию, которая принимает массив целых чисел и возвращает количество чётных чисел в массиве.

9. Возврат длины строки:
   Напишите функцию, которая принимает строку и возвращает её длину.

10. Возврат последнего элемента массива:
    Напишите функцию, которая принимает массив и возвращает его последний элемент.

11. Возврат суммы цифр числа:
    Напишите функцию, которая принимает число и возвращает сумму его цифр.

12. Возврат строки в верхнем регистре:
    Напишите функцию, которая принимает строку и возвращает её в верхнем регистре.

13. Возврат строки в нижнем регистре:
    Напишите функцию, которая принимает строку и возвращает её в нижнем регистре.

14. Возврат случайного элемента массива:
    Напишите функцию, которая принимает массив и возвращает случайный элемент из него.

15. Возврат количества слов в строке:
    Напишите функцию, которая принимает строку и возвращает количество слов в ней (слова разделены пробелами).
*/


/* Сложные задачи (для закрепления)

1. Поиск наибольшего общего делителя (НОД):
   Напишите функцию, которая принимает два числа и возвращает их наибольший общий делитель.

2. Поиск простых чисел:
   Напишите функцию, которая принимает число N и возвращает массив всех простых чисел от 1 до N.

3. Реверс строки:
   Напишите функцию, которая принимает строку и возвращает её в обратном порядке.

4. Подсчёт гласных в строке:
   Напишите функцию, которая принимает строку и возвращает количество гласных букв в ней.

5. Сортировка массива:
   Напишите функцию, которая принимает массив целых чисел и возвращает отсортированный массив (по возрастанию).
*/