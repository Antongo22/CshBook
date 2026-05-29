namespace CshBook.Answers.Chapter2.Lesson22Encapsulation
{
    class Wallet
    {
        private int _money;

        public int Money
        {
            get
            {
                return _money;
            }
        }

        public void AddMoney(int amount)
        {
            if (IsValidAmount(amount))
            {
                _money += amount;
            }
        }

        public void SpendMoney(int amount)
        {
            if (IsValidAmount(amount) && amount <= _money)
            {
                _money -= amount;
            }
        }

        private bool IsValidAmount(int amount)
        {
            return amount > 0;
        }
    }

    class BankAccount
    {
        private int _balance;
        private string _pin;

        public BankAccount(string pin, int startBalance)
        {
            _pin = pin;

            if (startBalance > 0)
            {
                _balance = startBalance;
            }
        }

        public string Info
        {
            get
            {
                return $"Баланс: {_balance}";
            }
        }

        public void Deposit(int amount)
        {
            if (amount > 0)
            {
                _balance += amount;
            }
        }

        public void Withdraw(int amount, string pin)
        {
            if (!IsCorrectPin(pin))
            {
                return;
            }

            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
            }
        }

        private bool IsCorrectPin(string pin)
        {
            return _pin == pin;
        }
    }

    class Temperature
    {
        private double _celsius;

        public double Celsius
        {
            get
            {
                return _celsius;
            }
        }

        public Temperature(double celsius)
        {
            SetCelsius(celsius);
        }

        public void SetCelsius(double value)
        {
            if (value >= -273)
            {
                _celsius = value;
            }
        }
    }

    class SmartHouse
    {
        private int _temperature;
        private int _light;
        private bool _securityEnabled;

        public string Status
        {
            get
            {
                return $"Температура: {_temperature}, свет: {_light}, охрана: {_securityEnabled}";
            }
        }

        public void SetTemperature(int temperature)
        {
            if (temperature >= 10 && temperature <= 35)
            {
                _temperature = temperature;
            }
        }

        public void SetLight(int light)
        {
            if (light >= 0 && light <= 100)
            {
                _light = light;
            }
        }

        public void ToggleSecurity()
        {
            _securityEnabled = !_securityEnabled;
        }
    }

    class GameCharacter
    {
        private int _health = 100;

        public int Health
        {
            get
            {
                return _health;
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
            {
                return;
            }

            _health -= damage;

            if (_health < 0)
            {
                _health = 0;
            }
        }

        public void Heal(int value)
        {
            if (value <= 0)
            {
                return;
            }

            _health += value;

            if (_health > 100)
            {
                _health = 100;
            }
        }
    }

    internal static class AnswerLesson22Encapsulation
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 22. Инкапсуляция");
            Console.WriteLine("=====================");
            Console.WriteLine();

            Console.WriteLine("1-5. Wallet");
            Wallet wallet = new Wallet();
            wallet.AddMoney(500);
            wallet.SpendMoney(120);
            wallet.SpendMoney(1000);
            Console.WriteLine(wallet.Money);
            Console.WriteLine();

            Console.WriteLine("6-7. BankAccount");
            BankAccount account = new BankAccount("1234", 1000);
            account.Deposit(500);
            account.Withdraw(200, "1234");
            account.Withdraw(100, "0000");
            Console.WriteLine(account.Info);
            Console.WriteLine();

            Console.WriteLine("8. Temperature");
            Temperature temperature = new Temperature(20);
            temperature.SetCelsius(-300);
            Console.WriteLine(temperature.Celsius);
            Console.WriteLine();

            Console.WriteLine("9. SmartHouse");
            SmartHouse house = new SmartHouse();
            house.SetTemperature(24);
            house.SetLight(70);
            house.ToggleSecurity();
            Console.WriteLine(house.Status);
            Console.WriteLine();

            Console.WriteLine("10. GameCharacter");
            GameCharacter hero = new GameCharacter();
            hero.TakeDamage(30);
            hero.Heal(10);
            Console.WriteLine(hero.Health);
            Console.WriteLine();
        }
    }
}
