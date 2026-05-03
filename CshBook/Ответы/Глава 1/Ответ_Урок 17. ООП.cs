namespace CshBook.Answers.Chapter1.Lesson17OopBasics
{
    class Pet
    {
        public string Name = "";
        public int Age;
        public int Mood = 50;
        public int PlayCount;

        public void PrintInfo()
        {
            Console.WriteLine($"Питомец: {Name}, возраст: {Age}, настроение: {Mood}, игр: {PlayCount}");
        }

        public void Play()
        {
            PlayCount++;
            Mood += 10;

            if (PlayCount % 10 == 0)
            {
                Age++;
            }
        }

        public void GrowOlder()
        {
            Age++;
        }
    }

    class Player
    {
        public string Name = "";
        public int Level;
        public int Coins;

        public void AddCoins(int count)
        {
            Coins += count;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Игрок: {Name}, уровень: {Level}, монеты: {Coins}");
        }
    }

    class InventoryItem
    {
        public string Title = "";
        public int Count;
        public int Price;

        public void PrintInfo()
        {
            Console.WriteLine($"{Title}: {Count} шт., цена {Price}");
        }
    }

    class Hero
    {
        public string Name = "";
        public int Strength;
        public int TrainingCount;

        public void Train()
        {
            Strength += 2;
            TrainingCount++;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name}: сила {Strength}, тренировок {TrainingCount}");
        }
    }

    class Quest
    {
        public string Title = "";
        public int Reward;
        public bool IsCompleted;

        public void Complete()
        {
            IsCompleted = true;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Title}: награда {Reward}, выполнен: {IsCompleted}");
        }
    }

    internal static class AnswerLesson17OopBasics
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 17. ООП");
            Console.WriteLine("============");
            Console.WriteLine();

            Pet bobik = new Pet();
            bobik.Name = "Бобик";
            bobik.Age = 5;

            Pet sharik = new Pet();
            sharik.Name = "Шарик";
            sharik.Age = 2;

            Console.WriteLine("1-6. Питомцы");
            bobik.PrintInfo();
            sharik.PrintInfo();

            for (int i = 0; i < 10; i++)
            {
                bobik.Play();
            }

            bobik.GrowOlder();
            bobik.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("7. Игрок");
            Player player = new Player();
            player.Name = "Anton";
            player.Level = 1;
            player.AddCoins(50);
            player.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("8. Инвентарь");
            InventoryItem item = new InventoryItem();
            item.Title = "Зелье";
            item.Count = 3;
            item.Price = 25;
            item.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("9. Тренировка персонажа");
            Hero hero = new Hero();
            hero.Name = "Knight";
            hero.Strength = 10;
            hero.Train();
            hero.Train();
            hero.PrintInfo();
            Console.WriteLine();

            Console.WriteLine("10. Свой класс");
            Quest quest = new Quest();
            quest.Title = "Найти ключ";
            quest.Reward = 100;
            quest.Complete();
            quest.PrintInfo();
            Console.WriteLine();
        }
    }
}
