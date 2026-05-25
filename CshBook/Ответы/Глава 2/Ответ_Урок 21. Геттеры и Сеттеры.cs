namespace CshBook.Answers.Chapter2.Lesson21GettersAndSetters
{
    class User
    {
        private int _age;

        public string Name { get; set; } = "";
        public string Email { get; set; } = "";

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value >= 0 && value <= 120)
                {
                    _age = value;
                }
            }
        }

        public string Info
        {
            get
            {
                return $"{Name}, возраст {Age}";
            }
        }
    }

    class BankAccount
    {
        public string AccountNumber { get; }
        public int Balance { get; private set; }

        public BankAccount(string accountNumber, int startBalance)
        {
            AccountNumber = accountNumber;
            Balance = startBalance;
        }

        public void Deposit(int amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
            }
        }

        public string Info
        {
            get
            {
                return $"{AccountNumber}: баланс {Balance}";
            }
        }
    }

    class Product
    {
        private int _price;
        private int _count;

        public string Title { get; set; } = "";

        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value >= 0)
                {
                    _price = value;
                }
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                if (value >= 0)
                {
                    _count = value;
                }
            }
        }
    }

    class SmartArray
    {
        private int[] _numbers;

        public SmartArray(int[] numbers)
        {
            _numbers = numbers;
        }

        public int Length
        {
            get
            {
                return _numbers.Length;
            }
        }

        public int Sum
        {
            get
            {
                int sum = 0;

                for (int i = 0; i < _numbers.Length; i++)
                {
                    sum += _numbers[i];
                }

                return sum;
            }
        }

        public double Average
        {
            get
            {
                if (_numbers.Length == 0)
                {
                    return 0;
                }

                return (double)Sum / Length;
            }
        }
    }

    internal static class AnswerLesson21GettersAndSetters
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 21. Геттеры и сеттеры");
            Console.WriteLine("==========================");
            Console.WriteLine();

            Console.WriteLine("1-3. User");
            User user = new User();
            user.Name = "Мария";
            user.Email = "maria@example.com";
            user.Age = 25;
            user.Age = -5;
            Console.WriteLine(user.Info);
            Console.WriteLine();

            Console.WriteLine("4-8. BankAccount");
            BankAccount account = new BankAccount("ACC-100", 1000);
            account.Deposit(500);
            account.Withdraw(200);
            account.Withdraw(5000);
            Console.WriteLine(account.Info);
            Console.WriteLine();

            Console.WriteLine("9. Product");
            Product product = new Product();
            product.Title = "Книга";
            product.Price = 300;
            product.Count = 2;
            product.Price = -10;
            Console.WriteLine($"{product.Title}: {product.Price}, {product.Count}");
            Console.WriteLine();

            Console.WriteLine("10. SmartArray");
            SmartArray array = new SmartArray(new int[] { 2, 4, 6, 8 });
            Console.WriteLine(array.Length);
            Console.WriteLine(array.Sum);
            Console.WriteLine(array.Average);
            Console.WriteLine();
        }
    }
}
