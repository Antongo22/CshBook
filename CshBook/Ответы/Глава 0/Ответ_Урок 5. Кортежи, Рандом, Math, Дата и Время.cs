namespace CshBook.Answers.Chapter0.Lesson05TuplesRandomMathDateTime
{
    internal static class AnswerLesson05TuplesRandomMathDateTime
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 5. Кортежи, Random, Math, DateTime");
            Console.WriteLine("=======================================");
            Console.WriteLine();

            {
                (string Name, int Points) player = ("Лена", 95);

                Console.WriteLine("1. Карточка игрока");
                Console.WriteLine($"Игрок: {player.Name}, очки: {player.Points}");
                Console.WriteLine();
            }

            {
                (int X, int Y) point = (3, -2);

                Console.WriteLine("2. Координаты точки");
                Console.WriteLine($"({point.X}; {point.Y})");
                Console.WriteLine();
            }

            {
                Random random = new Random(7);
                int randomNumber = random.Next(1, 11);

                Console.WriteLine("3. Случайное число");
                Console.WriteLine($"Случайное число: {randomNumber}");
                Console.WriteLine();
            }

            {
                int firstNumber = -8;
                int secondNumber = 13;

                Console.WriteLine("4. Работа с Math");
                Console.WriteLine($"Большее число: {Math.Max(firstNumber, secondNumber)}");
                Console.WriteLine($"Меньшее число: {Math.Min(firstNumber, secondNumber)}");
                Console.WriteLine($"Модуль первого числа: {Math.Abs(firstNumber)}");
                Console.WriteLine();
            }

            {
                Random random = new Random(11);
                int dice = random.Next(1, 7);

                Console.WriteLine("5. Бросок кубика");
                Console.WriteLine($"Выпало: {dice}");
                Console.WriteLine();
            }

            {
                Random random = new Random(15);
                int discountPercent = random.Next(5, 21);
                int price = 1000;
                double finalPrice = price - price * discountPercent / 100.0;

                Console.WriteLine("6. Случайная скидка");
                Console.WriteLine($"Скидка: {discountPercent}%");
                Console.WriteLine($"Итоговая цена: {finalPrice:F2}");
                Console.WriteLine();
            }

            {
                double radius = 5;
                double circumference = 2 * Math.PI * radius;
                double area = Math.PI * radius * radius;

                Console.WriteLine("7. Длина окружности и площадь круга");
                Console.WriteLine($"Длина окружности: {circumference:F2}");
                Console.WriteLine($"Площадь круга: {area:F2}");
                Console.WriteLine();
            }

            {
                double grade = 4.67;

                Console.WriteLine("8. Округление оценки");
                Console.WriteLine($"Вниз: {Math.Floor(grade)}");
                Console.WriteLine($"Вверх: {Math.Ceiling(grade)}");
                Console.WriteLine($"Обычное округление: {Math.Round(grade)}");
                Console.WriteLine();
            }

            {
                DateTime now = DateTime.Now;

                Console.WriteLine("9. Работа с текущей датой");
                Console.WriteLine($"Сейчас: {now}");
                Console.WriteLine($"Через 7 дней: {now.AddDays(7)}");
                Console.WriteLine($"Через 30 дней: {now.AddDays(30)}");
                Console.WriteLine();
            }

            {
                DateTime today = DateTime.Today;
                DateTime nextNewYear = new DateTime(today.Year + 1, 1, 1);
                int daysLeft = (nextNewYear - today).Days;

                Console.WriteLine("10. До нового года");
                Console.WriteLine($"До 1 января осталось {daysLeft} дн.");
                Console.WriteLine();
            }

            {
                string[] winners = { "Антон", "Марина", "Игорь", "Света" };
                string[] prizes = { "книга", "флешка", "наушники", "сертификат" };
                Random random = new Random(21);

                var giveaway = (
                    Winner: winners[random.Next(winners.Length)],
                    Prize: prizes[random.Next(prizes.Length)]
                );

                Console.WriteLine("11. Мини-розыгрыш");
                Console.WriteLine($"Победитель {giveaway.Winner} получает {giveaway.Prize}.");
                Console.WriteLine();
            }

            {
                DateTime birthDate = new DateTime(2002, 7, 16);
                DateTime today = DateTime.Today;
                int daysPassed = (today - birthDate).Days;

                Console.WriteLine("12. День рождения");
                Console.WriteLine($"Дата рождения: {birthDate:dd.MM.yyyy}");
                Console.WriteLine($"День недели: {birthDate:dddd}");
                Console.WriteLine($"С того дня прошло: {daysPassed} дн.");
                Console.WriteLine();
            }
        }
    }
}
