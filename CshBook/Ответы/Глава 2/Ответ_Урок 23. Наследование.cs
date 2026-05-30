namespace CshBook.Answers.Chapter2.Lesson23Inheritance
{
    class Vehicle
    {
        public string Model { get; }
        public int Speed { get; private set; }

        public Vehicle(string model, int speed)
        {
            Model = model;
            Speed = speed;
        }

        public void Move()
        {
            Console.WriteLine($"{Model} движется со скоростью {Speed}");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Model}, скорость: {Speed}");
        }
    }

    class Car : Vehicle
    {
        public int Fuel { get; private set; }

        public Car(string model, int speed, int fuel) : base(model, speed)
        {
            Fuel = fuel;
        }

        public void Refuel(int amount)
        {
            if (amount > 0)
            {
                Fuel += amount;
            }
        }

        public void Honk()
        {
            Console.WriteLine($"{Model}: beep!");
        }
    }

    class Boat : Vehicle
    {
        public bool IsSailing { get; private set; }

        public Boat(string model, int speed) : base(model, speed)
        {
        }

        public void StartSailing()
        {
            IsSailing = true;
        }

        public void DropAnchor()
        {
            IsSailing = false;
        }
    }

    class Character
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int Attack { get; }

        public Character(string name, int health, int attack)
        {
            Name = name;
            Health = health;
            Attack = attack;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name}: здоровье {Health}, атака {Attack}");
        }
    }

    class Warrior : Character
    {
        public Warrior(string name) : base(name, 120, 15)
        {
        }

        public void ShieldBlock()
        {
            Console.WriteLine($"{Name} блокирует удар щитом.");
        }
    }

    class Mage : Character
    {
        public Mage(string name) : base(name, 80, 25)
        {
        }

        public void CastSpell()
        {
            Console.WriteLine($"{Name} использует заклинание.");
        }
    }

    class Product
    {
        public string Title { get; }
        public int Price { get; }

        public Product(string title, int price)
        {
            Title = title;
            Price = price;
        }
    }

    class FoodProduct : Product
    {
        public int ExpirationDays { get; }

        public FoodProduct(string title, int price, int expirationDays) : base(title, price)
        {
            ExpirationDays = expirationDays;
        }
    }

    class TechProduct : Product
    {
        public int WarrantyMonths { get; }

        public TechProduct(string title, int price, int warrantyMonths) : base(title, price)
        {
            WarrantyMonths = warrantyMonths;
        }
    }

    internal static class AnswerLesson23Inheritance
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 23. Наследование");
            Console.WriteLine("=====================");
            Console.WriteLine();

            Console.WriteLine("1-6. Vehicle, Car, Boat");
            Car car = new Car("Toyota", 80, 30);
            Boat boat = new Boat("River Boat", 25);
            car.Move();
            car.Honk();
            car.Refuel(20);
            car.PrintInfo();
            boat.Move();
            boat.StartSailing();
            boat.DropAnchor();
            Console.WriteLine();

            Console.WriteLine("7. Character");
            Warrior warrior = new Warrior("Артур");
            Mage mage = new Mage("Мерлин");
            warrior.PrintInfo();
            warrior.ShieldBlock();
            mage.PrintInfo();
            mage.CastSpell();
            Console.WriteLine();

            Console.WriteLine("8. Product");
            FoodProduct apple = new FoodProduct("Яблоко", 10, 7);
            TechProduct phone = new TechProduct("Телефон", 30000, 24);
            Console.WriteLine($"{apple.Title}: {apple.Price}, годен дней: {apple.ExpirationDays}");
            Console.WriteLine($"{phone.Title}: {phone.Price}, гарантия месяцев: {phone.WarrantyMonths}");
            Console.WriteLine();
        }
    }
}
