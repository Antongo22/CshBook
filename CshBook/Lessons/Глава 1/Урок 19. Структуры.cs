namespace CshBook.Lessons.Chapter1.Lesson19Structs
{
    #region Теория
    /*
        struct - это структура.

        Она похожа на класс тем,
        что тоже может хранить поля и методы.

        Но есть важное отличие:
        class - ссылочный тип,
        struct - тип значения.
     */

    /*
        Если присвоить объект класса в другую переменную,
        копируется ссылка на тот же объект.

        Если присвоить структуру в другую переменную,
        копируются сами значения.
     */

    /*
        Поэтому изменение копии структуры
        не меняет исходную структуру.

        Это удобно для небольших данных:

        - точка на экране;
        - координаты;
        - размер;
        - цвет;
        - простой прямоугольник.
     */

    /*
        На старте держи простое правило:

        class используй для сущностей с поведением и состоянием.
        struct используй для маленьких значений,
        которые удобно копировать целиком.
     */

    /*
        Структура может иметь:

        - поля;
        - методы;
        - конструкторы с параметрами.

        Но не стоит делать структуру большой
        и перегружать ее сложной логикой.
     */
    #endregion

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

    struct StructPoint
    {
        public int X;
        public int Y;

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

        public void Print()
        {
            Console.WriteLine($"X = {X}, Y = {Y}");
        }
    }

    internal static class Lesson19Structs
    {
        public static void Main_()
        {
            ClassPoint classPoint1 = new ClassPoint(5, 10);
            ClassPoint classPoint2 = classPoint1;

            classPoint2.X = 20;

            Console.WriteLine("Класс:");
            Console.WriteLine($"classPoint1.X = {classPoint1.X}");
            Console.WriteLine($"classPoint2.X = {classPoint2.X}");

            Console.WriteLine("----");

            StructPoint structPoint1 = new StructPoint(5, 10);
            StructPoint structPoint2 = structPoint1;

            structPoint2.X = 20;

            Console.WriteLine("Структура:");
            Console.WriteLine($"structPoint1.X = {structPoint1.X}");
            Console.WriteLine($"structPoint2.X = {structPoint2.X}");

            Console.WriteLine("----");

            structPoint1.Move(3, 4);
            structPoint1.Print();
        }
    }

    #region Задачи
    /*
        Разминка

        1. Точка.
           Создай структуру Point с полями X и Y.
           Добавь конструктор Point(int x, int y).

        2. Вывод точки.
           Добавь в Point метод Print(),
           который выводит координаты точки.

        3. Перемещение точки.
           Добавь метод Move(int deltaX, int deltaY),
           который изменяет координаты точки.

        Основные задачи

        4. Копирование структуры.
           Создай Point point1 и Point point2 = point1.
           Измени point2.X и выведи обе точки.
           Убедись, что point1 не изменился.

        5. Прямоугольник.
           Создай структуру Rectangle с полями Width и Height.
           Добавь метод GetArea(), который возвращает площадь.

        6. Масштабирование.
           Добавь в Rectangle метод Scale(int factor),
           который увеличивает ширину и высоту.

        7. Квадрат или нет.
           Добавь метод IsSquare(),
           который возвращает true,
           если ширина равна высоте.

        Задачи на перенос

        8. Размер экрана.
           Создай структуру Size с полями Width и Height.
           Добавь метод PrintInfo().

        9. Координаты игрока.
           Создай структуру PlayerPosition с полями X и Y.
           Добавь методы MoveUp(), MoveDown(), MoveLeft(), MoveRight().

        10. Сравнение с классом.
            Создай похожие PointClass и PointStruct.
            Покажи на примере, как отличается присваивание класса и структуры.
     */
    #endregion
}
