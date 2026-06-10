namespace CshBook.Answers.Chapter2.Lesson26AbstractClasses
{
    abstract class Worker
    {
        public string Name { get; }

        protected Worker(string name)
        {
            Name = name;
        }

        public void PrintName()
        {
            Console.WriteLine($"Работник: {Name}");
        }

        public abstract void Work();

        public abstract int CalculateSalary();
    }

    class Programmer : Worker
    {
        public Programmer(string name) : base(name)
        {
        }

        public override void Work()
        {
            Console.WriteLine($"{Name} пишет код.");
        }

        public override int CalculateSalary()
        {
            return 120000;
        }
    }

    class Designer : Worker
    {
        public Designer(string name) : base(name)
        {
        }

        public override void Work()
        {
            Console.WriteLine($"{Name} проектирует интерфейс.");
        }

        public override int CalculateSalary()
        {
            return 90000;
        }
    }

    abstract class GameCharacter
    {
        public string Name { get; }
        public int Health { get; private set; }

        protected GameCharacter(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < 0)
            {
                Health = 0;
            }

            Console.WriteLine($"{Name} получил урон {damage}. Здоровье: {Health}");
        }

        public abstract void Attack();
    }

    class Warrior : GameCharacter
    {
        public Warrior(string name, int health) : base(name, health)
        {
        }

        public override void Attack()
        {
            Console.WriteLine($"{Name} атакует мечом.");
        }
    }

    class Mage : GameCharacter
    {
        public Mage(string name, int health) : base(name, health)
        {
        }

        public override void Attack()
        {
            Console.WriteLine($"{Name} выпускает огненный шар.");
        }
    }

    internal static class AnswerLesson26AbstractClasses
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 26. Абстрактные классы");
            Console.WriteLine("===========================");
            Console.WriteLine();

            Worker[] workers =
            {
                new Programmer("Антон"),
                new Designer("Мария")
            };

            for (int i = 0; i < workers.Length; i++)
            {
                workers[i].PrintName();
                workers[i].Work();
                Console.WriteLine($"Зарплата: {workers[i].CalculateSalary()}");
                Console.WriteLine();
            }

            GameCharacter[] characters =
            {
                new Warrior("Рыцарь", 100),
                new Mage("Маг", 70)
            };

            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].TakeDamage(25);
                characters[i].Attack();
                Console.WriteLine();
            }
        }
    }
}
