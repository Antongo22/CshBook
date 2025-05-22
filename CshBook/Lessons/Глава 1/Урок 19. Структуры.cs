using System;
using System.Threading;

namespace CshBook.Lessons
{
    /* Урок 19: Структуры в C#
     
    Структуры (struct) - это типы значений, которые позволяют инкапсулировать данные и связанную функциональность.
    В отличие от классов, структуры хранятся в стеке и копируются при присваивании.
     */

    #region Сравнение структур и классов
    // Класс (ссылочный тип)
    class ClassPoint
    {
        public int X;
        public int Y;

        public ClassPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Структура (тип значения)
    struct StructPoint
    {
        public int X;
        public int Y;

        // В структурах нельзя объявлять конструктор без параметров
        public StructPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
    }
    #endregion

    internal class NineteenthLesson
    {
        public static void Main_()
        {
            // Работа с классом
            ClassPoint classPoint1 = new ClassPoint(5, 10);
            ClassPoint classPoint2 = classPoint1; // Копируется ссылка

            classPoint2.X = 20;
            Console.WriteLine($"ClassPoint1: {classPoint1.X}, {classPoint1.Y}"); // 20, 10
            Console.WriteLine($"ClassPoint2: {classPoint2.X}, {classPoint2.Y}\n"); // 20, 10

            // Работа со структурой
            StructPoint structPoint1 = new StructPoint(5, 10);
            StructPoint structPoint2 = structPoint1; // Копируются значения

            structPoint2.X = 20;
            Console.WriteLine($"StructPoint1: {structPoint1.X}, {structPoint1.Y}"); // 5, 10
            Console.WriteLine($"StructPoint2: {structPoint2.X}, {structPoint2.Y}\n");

            // Вызов метода структуры
            structPoint1.Move(3, 4);
            Console.WriteLine($"После перемещения: {structPoint1.X}, {structPoint1.Y}");
        }

        /* Особенности структур:
         1. Не могут иметь конструктора без параметров
         2. Не поддерживают наследование
         3. Могут реализовывать интерфейсы
         4. Копируются при передаче в методы
         5. Идеальны для небольших данных (до 16 байт)
         */
    }

    #region Задание
    /* Создайте структуру Rectangle со следующими возможностями:
     - Поля width и height
     - Конструктор с параметрами
     - Метод GetArea() возвращающий площадь
     - Метод Scale(int factor) увеличивающий размеры
     - Свойство IsSquare (только чтение) проверяющее квадрат
     */

    struct Rectangle
    {
        public int Width;
        public int Height;

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int GetArea() => Width * Height;

        public void Scale(int factor)
        {
            Width *= factor;
            Height *= factor;
        }

        public bool IsSquare => Width == Height;
    }
    #endregion


    /* Советы по использованию структур:
     1. Используйте для небольших неизменяемых данных
     2. Избегайте частого копирования больших структур
     3. Используйте readonly struct для гарантии неизменяемости
     4. Не используйте структуры для сложной логики с наследованием
     */
}