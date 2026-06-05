namespace CshBook.Answers.Chapter2.Lesson24Polymorphism
{
    class Shape
    {
        public virtual void Draw()
        {
            Console.WriteLine("Рисуем фигуру.");
        }

        public virtual double GetArea()
        {
            return 0;
        }
    }

    class Circle : Shape
    {
        public int Radius { get; }

        public Circle(int radius)
        {
            Radius = radius;
        }

        public override void Draw()
        {
            Console.WriteLine("Рисуем круг.");
        }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    class Square : Shape
    {
        public int Side { get; }

        public Square(int side)
        {
            Side = side;
        }

        public override void Draw()
        {
            Console.WriteLine("Рисуем квадрат.");
        }

        public override double GetArea()
        {
            return Side * Side;
        }
    }

    class Vehicle
    {
        public virtual void Move()
        {
            Console.WriteLine("Транспорт движется.");
        }
    }

    class Car : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine("Машина едет по дороге.");
        }
    }

    class Boat : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine("Лодка плывет по воде.");
        }
    }

    class Airplane : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine("Самолет летит.");
        }
    }

    class Notification
    {
        public virtual void Send()
        {
            Console.WriteLine("Отправка уведомления.");
        }
    }

    class EmailNotification : Notification
    {
        public override void Send()
        {
            Console.WriteLine("Отправка email.");
        }
    }

    class SmsNotification : Notification
    {
        public override void Send()
        {
            Console.WriteLine("Отправка SMS.");
        }
    }

    class Employee
    {
        public virtual int CalculateSalary()
        {
            return 0;
        }
    }

    class Manager : Employee
    {
        public override int CalculateSalary()
        {
            return 120000;
        }
    }

    class Developer : Employee
    {
        public override int CalculateSalary()
        {
            return 100000;
        }
    }

    class Intern : Employee
    {
        public override int CalculateSalary()
        {
            return 30000;
        }
    }

    internal static class AnswerLesson24Polymorphism
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 24. Полиморфизм");
            Console.WriteLine("====================");
            Console.WriteLine();

            Console.WriteLine("1-4. Shape");
            Shape[] shapes =
            {
                new Circle(3),
                new Square(5)
            };

            for (int i = 0; i < shapes.Length; i++)
            {
                shapes[i].Draw();
                Console.WriteLine(shapes[i].GetArea().ToString("F2"));
            }
            Console.WriteLine();

            Console.WriteLine("5. Vehicle");
            Vehicle[] vehicles =
            {
                new Car(),
                new Boat(),
                new Airplane()
            };

            for (int i = 0; i < vehicles.Length; i++)
            {
                vehicles[i].Move();
            }
            Console.WriteLine();

            Console.WriteLine("6. Notification");
            Notification[] notifications =
            {
                new EmailNotification(),
                new SmsNotification()
            };

            for (int i = 0; i < notifications.Length; i++)
            {
                notifications[i].Send();
            }
            Console.WriteLine();

            Console.WriteLine("7. Employee");
            Employee[] employees =
            {
                new Manager(),
                new Developer(),
                new Intern()
            };

            for (int i = 0; i < employees.Length; i++)
            {
                Console.WriteLine(employees[i].CalculateSalary());
            }
            Console.WriteLine();
        }
    }
}
