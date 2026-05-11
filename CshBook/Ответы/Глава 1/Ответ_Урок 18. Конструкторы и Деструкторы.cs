namespace CshBook.Answers.Chapter1.Lesson18ConstructorsAndDestructors
{
    class Pet
    {
        public string Name;
        public int Age;
        public int Mood;

        public Pet(string name)
        {
            this.Name = name;
            this.Age = 0;
            this.Mood = 50;
        }

        public Pet(string name, int age)
        {
            this.Name = name;
            this.Age = age;
            this.Mood = 50;
        }

        public Pet(string name, int age, int mood)
        {
            this.Name = name;
            this.Age = age;
            this.Mood = mood;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name}: возраст {Age}, настроение {Mood}");
        }

        public Pet CreateCopy()
        {
            return new Pet(Name, Age, Mood);
        }

        ~Pet()
        {
            // Финализатор нельзя использовать как обычный предсказуемый метод завершения.
        }
    }

    class Player
    {
        public string Name;
        public int Level;
        public int Coins;

        public Player(string name)
        {
            this.Name = name;
            this.Level = 1;
            this.Coins = 0;
        }

        public Player(string name, int level, int coins)
        {
            this.Name = name;
            this.Level = level;
            this.Coins = coins;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name}: уровень {Level}, монеты {Coins}");
        }
    }

    class Product
    {
        public string Title;
        public int Price;
        public int Count;

        public Product(string title, int price, int count)
        {
            this.Title = title;
            this.Count = count;

            if (price < 0)
            {
                this.Price = 0;
            }
            else
            {
                this.Price = price;
            }
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Title}: цена {Price}, количество {Count}");
        }
    }

    internal static class AnswerLesson18ConstructorsAndDestructors
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 18. Конструкторы и деструкторы");
            Console.WriteLine("===================================");
            Console.WriteLine();

            Console.WriteLine("1-5. Конструкторы Pet");
            Pet bobik = new Pet("Бобик");
            Pet sharik = new Pet("Шарик", 3);
            Pet barsik = new Pet("Барсик", 2, 80);
            bobik.PrintInfo();
            sharik.PrintInfo();
            barsik.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("6. Ссылка на тот же объект");
            Pet sameBobik = bobik;
            sameBobik.Name = "Бобик через вторую переменную";
            bobik.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("7. Копия объекта");
            Pet copy = bobik.CreateCopy();
            copy.Name = "Копия Бобика";
            bobik.PrintInfo();
            copy.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("8. Player");
            Player player1 = new Player("Anton");
            Player player2 = new Player("Max", 5, 300);
            player1.PrintInfo();
            player2.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("9. Product");
            Product product = new Product("Книга", -100, 2);
            product.PrintInfo();
            Console.WriteLine();
        }
    }
}
