namespace CshBook.Answers.Chapter1.Lesson19Structs
{
    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Print()
        {
            Console.WriteLine($"X = {X}, Y = {Y}");
        }

        public void Move(int deltaX, int deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }
    }

    struct Rectangle
    {
        public int Width;
        public int Height;

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int GetArea()
        {
            return Width * Height;
        }

        public void Scale(int factor)
        {
            Width *= factor;
            Height *= factor;
        }

        public bool IsSquare()
        {
            return Width == Height;
        }
    }

    struct Size
    {
        public int Width;
        public int Height;

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Размер: {Width} x {Height}");
        }
    }

    struct PlayerPosition
    {
        public int X;
        public int Y;

        public PlayerPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveUp()
        {
            Y++;
        }

        public void MoveDown()
        {
            Y--;
        }

        public void MoveLeft()
        {
            X--;
        }

        public void MoveRight()
        {
            X++;
        }

        public void Print()
        {
            Console.WriteLine($"Позиция: {X}, {Y}");
        }
    }

    class PointClass
    {
        public int X;

        public PointClass(int x)
        {
            X = x;
        }
    }

    struct PointStruct
    {
        public int X;

        public PointStruct(int x)
        {
            X = x;
        }
    }

    internal static class AnswerLesson19Structs
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 19. Структуры");
            Console.WriteLine("==================");
            Console.WriteLine();

            Console.WriteLine("1-3. Point");
            Point point = new Point(3, 5);
            point.Print();
            point.Move(2, 4);
            point.Print();
            Console.WriteLine();

            Console.WriteLine("4. Копирование структуры");
            Point point1 = new Point(10, 20);
            Point point2 = point1;
            point2.X = 99;
            point1.Print();
            point2.Print();
            Console.WriteLine();

            Console.WriteLine("5-7. Rectangle");
            Rectangle rectangle = new Rectangle(4, 6);
            Console.WriteLine(rectangle.GetArea());
            Console.WriteLine(rectangle.IsSquare());
            rectangle.Scale(2);
            Console.WriteLine(rectangle.GetArea());
            Console.WriteLine();

            Console.WriteLine("8. Size");
            Size size = new Size(1920, 1080);
            size.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("9. PlayerPosition");
            PlayerPosition position = new PlayerPosition(0, 0);
            position.MoveRight();
            position.MoveUp();
            position.Print();
            Console.WriteLine();

            Console.WriteLine("10. Сравнение class и struct");
            PointClass classPoint1 = new PointClass(5);
            PointClass classPoint2 = classPoint1;
            classPoint2.X = 20;
            Console.WriteLine($"classPoint1.X = {classPoint1.X}");

            PointStruct structPoint1 = new PointStruct(5);
            PointStruct structPoint2 = structPoint1;
            structPoint2.X = 20;
            Console.WriteLine($"structPoint1.X = {structPoint1.X}");
            Console.WriteLine();
        }
    }
}
