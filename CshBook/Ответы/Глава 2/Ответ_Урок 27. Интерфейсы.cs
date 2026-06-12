namespace CshBook.Answers.Chapter2.Lesson27Interfaces
{
    interface INotificationSender
    {
        void Send(string message);
    }

    class EmailSender : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email отправлен: {message}");
        }
    }

    class TelegramSender : INotificationSender
    {
        public void Send(string message)
        {
            Console.WriteLine($"Telegram отправлен: {message}");
        }
    }

    interface IMovable
    {
        void Move();
    }

    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    class Player : IMovable
    {
        public string Name { get; }

        public Player(string name)
        {
            Name = name;
        }

        public void Move()
        {
            Console.WriteLine($"{Name} идет вперед.");
        }
    }

    class Enemy : IMovable, IDamageable
    {
        public string Name { get; }
        public int Health { get; private set; }

        public Enemy(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void Move()
        {
            Console.WriteLine($"{Name} приближается к игроку.");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} получил урон {damage}. Здоровье: {Health}");
        }
    }

    interface IReadable
    {
        void Read();
    }

    class Book : IReadable
    {
        public string Title { get; }

        public Book(string title)
        {
            Title = title;
        }

        public void Read()
        {
            Console.WriteLine($"Читаем книгу: {Title}");
        }
    }

    class Article : IReadable
    {
        public string Title { get; }

        public Article(string title)
        {
            Title = title;
        }

        public void Read()
        {
            Console.WriteLine($"Читаем статью: {Title}");
        }
    }

    interface IExportable
    {
        void Export();
    }

    class Invoice : IExportable
    {
        public int Number { get; }

        public Invoice(int number)
        {
            Number = number;
        }

        public void Print()
        {
            Console.WriteLine($"Печатаем счет #{Number}");
        }

        public void Export()
        {
            Console.WriteLine($"Экспортируем счет #{Number}");
        }
    }

    internal static class AnswerLesson27Interfaces
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 27. Интерфейсы");
            Console.WriteLine("===================");
            Console.WriteLine();

            INotificationSender[] senders =
            {
                new EmailSender(),
                new TelegramSender()
            };

            for (int i = 0; i < senders.Length; i++)
            {
                senders[i].Send("Новая задача доступна.");
            }

            Console.WriteLine();

            Player player = new Player("Игрок");
            Enemy enemy = new Enemy("Гоблин", 50);

            player.Move();
            enemy.Move();
            enemy.TakeDamage(15);
            Console.WriteLine();

            IReadable[] readableItems =
            {
                new Book("C# для начинающих"),
                new Article("Что такое интерфейсы")
            };

            for (int i = 0; i < readableItems.Length; i++)
            {
                readableItems[i].Read();
            }

            Console.WriteLine();

            Invoice invoice = new Invoice(101);
            invoice.Print();
            invoice.Export();
        }
    }
}
