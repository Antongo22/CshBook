using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons
{
    /* Кортежи (Tuples)

       В C# кортеж — это структура, позволяющая хранить несколько значений разных типов в одной переменной.
       Кортежи удобны, когда нужно быстро сгруппировать несколько значений без создания отдельного класса.

       Современные кортежи (ValueTuple) были введены начиная с C# 7.0.
       Они поддерживают именованные элементы и могут использоваться без явного вызова методов.

       Пример простого кортежа:
       var tuple = (1, "apple", 3.14); // тип (int, string, double)

       Доступ к элементам:
       tuple.Item1, tuple.Item2, tuple.Item3

       Или с именами:
       var person = (name: "Анна", age: 25);
       person.name, person.age

       Кортежи можно использовать:
       - для возврата нескольких значений из метода (будет в следующих уроках);
       - как временное объединение данных;
       - для упрощения кода без создания новых классов или структур.
    */

    internal class FifthLesson
    {
        public static void Main_()
        {


            var tuple1 = (1, "яблоко", 3.14);
            Console.WriteLine($"tuple1: {tuple1.Item1}, {tuple1.Item2}, {tuple1.Item3}");

            // Пример с именованными элементами
            var person = (name: "Анна", age: 25);
            Console.WriteLine($"Имя: {person.name}, Возраст: {person.age}");

            // Кортеж для координат
            var point = (x: 10, y: 20);
            Console.WriteLine($"Координаты точки: x = {point.x}, y = {point.y}");

            // Обновление значений кортежа
            point.x = 15;  // можно изменить, так как это переменная
            point.y = 30;
            Console.WriteLine($"Обновлённые координаты: x = {point.x}, y = {point.y}");

            // Кортеж из одного значения
            var single = (42,);  // запятая обязательна
            Console.WriteLine($"Один элемент в кортеже: {single.Item1}");

            // Кортеж из двух чисел и их сумма
            var numbers = (4, 5);
            int sum = numbers.Item1 + numbers.Item2;
            Console.WriteLine($"Сумма {numbers.Item1} и {numbers.Item2} = {sum}");

            // Вложенный кортеж
            var nested = ((1, 2), (3, 4));
            Console.WriteLine($"nested: {nested.Item1.Item1}, {nested.Item1.Item2}, {nested.Item2.Item1}, {nested.Item2.Item2}");

            // Кортеж можно использовать в условии
            var grade = (score: 85, passed: true);
            if (grade.passed)
            {
                Console.WriteLine($"Оценка: {grade.score} — зачёт");
            }

            // Кортеж для хранения даты
            var date = (day: 21, month: 5, year: 2025);
            Console.WriteLine($"Дата: {date.day}.{date.month}.{date.year}");



            // Работа с Random и Math
            Random random = new Random();
            int a = random.Next(0, 10);
            int b = random.Next(0, 10);
            int c = random.Next(0, 10);
            int d = random.Next(0, 10);
            Console.WriteLine($"{a}, {b}, {c}, {d}");

            // Методы Random:
            // Next(): Возвращает неотрицательное случайное целое число.
            // Next(int maxValue): Возвращает неотрицательное случайное целое число, меньшее maxValue.
            // Next(int minValue, int maxValue): Возвращает случайное целое число в диапазоне от minValue до maxValue-1.
            // NextDouble(): Возвращает случайное число с плавающей точкой в диапазоне от 0.0 до 1.0.

            // Пример использования NextDouble()
            double randomDouble = random.NextDouble();
            Console.WriteLine(randomDouble);

            // Работа с Math
            double sqrtResult = Math.Sqrt(4);  // Корень квадратный
            double powResult = Math.Pow(2, 3);  // Возведение в степень
            double pi = Math.PI;  // Число Пи
            double e = Math.E;  // Число E
            Console.WriteLine($"{sqrtResult}, {powResult}, {pi}, {e}");

            // Дополнительные методы Math:
            double absValue = Math.Abs(-5);  // Абсолютное значение
            double sinValue = Math.Sin(Math.PI / 2);  // Синус
            double cosValue = Math.Cos(Math.PI);  // Косинус
            double tanValue = Math.Tan(Math.PI / 4);  // Тангенс
            Console.WriteLine($"{absValue}, {sinValue}, {cosValue}, {tanValue}");

            // Проверка, целое ли число
            double numberToCheck = 42.0;
            bool isInteger = numberToCheck % 1 == 0;
            Console.WriteLine(isInteger);  // True

            // Работа с DateTime
            DateTime now = DateTime.Now;
            Console.WriteLine($"Текущая дата и время: {now}");

            DateTime tomorrow = now.AddDays(1);
            Console.WriteLine($"Завтрашняя дата: {tomorrow}");

            DateTime dateTime = new DateTime();
            

            TimeSpan difference = tomorrow - now;
            Console.WriteLine($"Разница во времени: {difference.TotalHours} часов");

            // Форматирование даты
            string formattedDate = now.ToString("dd.MM.yyyy HH:mm:ss");
            Console.WriteLine($"Отформатированная дата: {formattedDate}");


            // Конкретная дата
            DateTime specificDate1 = new DateTime(2023, 5, 15); // 15 мая 2023 года
            DateTime specificDate2 = new DateTime(2023, 5, 15, 14, 30, 0); // 15 мая 2023, 14:30:00
            Console.WriteLine($"Конкретная дата 1: {specificDate1}");
            Console.WriteLine($"Конкретная дата 2: {specificDate2}");

            // Дата из строки
            DateTime parsedDate = DateTime.Parse("2023-12-31");
            Console.WriteLine($"Дата из строки: {parsedDate}");

            // UTC время
            DateTime utcNow = DateTime.UtcNow;
            Console.WriteLine($"Текущее UTC время: {utcNow}");


            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 12, 31);

            // Разница как TimeSpan
            difference = endDate - startDate;

            // Вывод разницы в разных форматах
            Console.WriteLine("\nРазница между датами:");
            Console.WriteLine($"Всего дней: {difference.TotalDays}");
            Console.WriteLine($"Всего часов: {difference.TotalHours}");
            Console.WriteLine($"Всего минут: {difference.TotalMinutes}");
            Console.WriteLine($"Всего секунд: {difference.TotalSeconds}");
            Console.WriteLine($"Всего миллисекунд: {difference.TotalMilliseconds}");

            Console.WriteLine($"\nРазница в компонентах:");
            Console.WriteLine($"Дни: {difference.Days}");
            Console.WriteLine($"Часы: {difference.Hours}");
            Console.WriteLine($"Минуты: {difference.Minutes}");
            Console.WriteLine($"Секунды: {difference.Seconds}");
            Console.WriteLine($"Миллисекунды: {difference.Milliseconds}");

            // Форматированная разница
            Console.WriteLine($"\nФорматированная разница: {difference.Days} дней, {difference.Hours} часов, {difference.Minutes} минут");
        }

        /* Задачи на кортежи

        1. Создайте кортеж, содержащий три элемента разных типов (например, int, string, double).

        2. Напишите программу, который принимает кортеж из двух элементов и возвращает их сумму (если это числа) или их конкатенацию (если это строки).

        3. Создайте кортеж, содержащий два вложенных кортежа. Доступ к элементам вложенных кортежей.

        4. Напишите программу, которая генерирует случайный кортеж из трех элементов (int, double, string) и выводит их на экран.

        5. Используя кортежи, напишите программу, которая запрашивает у пользователя три числа и возвращает кортеж с их квадратами.

        6. Создайте кортеж, содержащий координаты точки на плоскости (x, y). Напишите , который принимает этот кортеж и возвращает расстояние от точки до начала координат.

        7. Напишите программу, которая использует кортежи для представления даты (день, месяц, год) и выводит её в формате "dd.mm.yyyy".

        8. Создайте кортеж, содержащий имя и возраст человека. Напишите программу, которая принимает этот кортеж и возвращает строку с приветствием, включающую имя и возраст.

        9. Используя кортежи, напишите программу, которая запрашивает у пользователя два числа и возвращает кортеж с их суммой, разностью, произведением и частным.

        10. Создайте кортеж, содержащий три числа. Напишите программу, которая принимает этот кортеж и возвращает кортеж с этими же числами, отсортированными по возрастанию.
        */

        /* Задачи на DateTime

        1. Напишите программу, которая выводит текущую дату и время в формате "dd.MM.yyyy HH:mm:ss".

        2. Напишите программу, которая запрашивает у пользователя дату рождения и вычисляет его возраст.

        3. Напишите программу, которая вычисляет количество дней до нового года.

        4. Напишите программу, которая запрашивает у пользователя две даты и вычисляет разницу между ними в днях.

        5. Напишите программу, которая выводит дату через неделю от текущей даты.

        6. Напишите программу, которая запрашивает у пользователя дату и время и выводит её в формате "dd.MM.yyyy HH:mm:ss".

        7. Напишите программу, которая вычисляет количество секунд, прошедших с начала текущего дня.

        8. Напишите программу, которая запрашивает у пользователя дату и выводит день недели для этой даты.

        9. Напишите программу, которая выводит текущую дату и время в формате "yyyy-MM-ddTHH:mm:ss".

        10. Напишите программу, которая запрашивает у пользователя дату и вычисляет количество дней до этой даты от текущей даты.
        */

        /* Задачи на Math

         1. Напишите программу, которая рандомно генерирует два числа и вычисляет их среднее арифметическое.

         2. Напишите программу, которая рандомно генерирует радиус круга и вычисляет его площадь.

         3. Напишите программу, которая рандомно генерирует три числа и находит максимальное из них.

         4. Напишите программу, которая рандомно генерирует угол в градусах и вычисляет его синус, косинус и тангенс.

         5. Напишите программу, которая рандомно генерирует число и проверяет, является ли оно простым.

         6. Напишите программу, которая рандомно генерирует два числа и вычисляет их наибольший общий делитель (НОД).

         7. Напишите программу, которая рандомно генерирует два числа и вычисляет их наименьшее общее кратное (НОК).

         8. Напишите программу, которая рандомно генерирует число и вычисляет его факториал.

         9. Напишите программу, которая рандомно генерирует число и проверяет, является ли оно палиндромом.

         10. Напишите программу, которая рандомно генерирует число и вычисляет сумму его цифр.
        */
    }
}