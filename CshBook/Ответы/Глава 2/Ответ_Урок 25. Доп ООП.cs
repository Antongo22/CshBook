namespace CshBook.Answers.Chapter2.Lesson25AdditionalOop
{
    class Vector2
    {
        public int X { get; }
        public int Y { get; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator *(Vector2 vector, int multiplier)
        {
            return new Vector2(vector.X * multiplier, vector.Y * multiplier);
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }

    class NumberBox
    {
        private int[] _numbers;

        public NumberBox(int size)
        {
            _numbers = new int[size];
        }

        public int Length
        {
            get
            {
                return _numbers.Length;
            }
        }

        public int this[int index]
        {
            get
            {
                return _numbers[index];
            }
            set
            {
                _numbers[index] = value;
            }
        }

        public int Sum()
        {
            int sum = 0;

            for (int i = 0; i < _numbers.Length; i++)
            {
                sum += _numbers[i];
            }

            return sum;
        }
    }

    class BookShelf
    {
        private string[] _books;

        public BookShelf(int size)
        {
            _books = new string[size];
        }

        public string this[int index]
        {
            get
            {
                return _books[index];
            }
            set
            {
                _books[index] = value;
            }
        }
    }

    class Money
    {
        public int Amount { get; }

        public Money(int amount)
        {
            Amount = amount;
        }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.Amount + right.Amount);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.Amount - right.Amount);
        }

        public override string ToString()
        {
            return $"{Amount} руб.";
        }
    }

    internal static class AnswerLesson25AdditionalOop
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 25. Дополнительные возможности ООП");
            Console.WriteLine("=======================================");
            Console.WriteLine();

            Console.WriteLine("1-4. Vector2");
            Vector2 first = new Vector2(2, 3);
            Vector2 second = new Vector2(5, 1);
            Console.WriteLine(first + second);
            Console.WriteLine(first - second);
            Console.WriteLine(first * 3);
            Console.WriteLine();

            Console.WriteLine("5-8. NumberBox");
            NumberBox box = new NumberBox(3);
            box[0] = 10;
            box[1] = 20;
            box[2] = 30;

            for (int i = 0; i < box.Length; i++)
            {
                Console.WriteLine(box[i]);
            }

            Console.WriteLine(box.Sum());
            Console.WriteLine();

            Console.WriteLine("9. BookShelf");
            BookShelf shelf = new BookShelf(2);
            shelf[0] = "C# для начинающих";
            shelf[1] = "ООП на практике";
            Console.WriteLine(shelf[0]);
            Console.WriteLine(shelf[1]);
            Console.WriteLine();

            Console.WriteLine("10. Money");
            Money price = new Money(500);
            Money delivery = new Money(100);
            Console.WriteLine(price + delivery);
            Console.WriteLine(price - delivery);
            Console.WriteLine();
        }
    }
}
