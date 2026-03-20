namespace CshBook.Answers.Chapter0.Lesson04WhileLoop
{
    public static class AnswerLesson04WhileLoop
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 4. Цикл while");
            Console.WriteLine("==================");
            Console.WriteLine();

            {
                int number = 1;

                Console.WriteLine("1. Числа от 1 до 10");
                while (number <= 10)
                {
                    Console.Write($"{number} ");
                    number++;
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            {
                int n = 7;
                int current = 1;

                Console.WriteLine("2. Числа от 1 до n");
                while (current <= n)
                {
                    Console.Write($"{current} ");
                    current++;
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            {
                int n = 6;
                int current = 1;
                int sum = 0;

                while (current <= n)
                {
                    sum += current;
                    current++;
                }

                Console.WriteLine("3. Сумма от 1 до n");
                Console.WriteLine($"Сумма = {sum}");
                Console.WriteLine();
            }

            {
                int[] enteredNumbers = { 5, 8, 3, 0, 10 };
                int index = 0;
                int sum = 0;

                while (index < enteredNumbers.Length)
                {
                    int current = enteredNumbers[index];
                    if (current == 0)
                    {
                        break;
                    }

                    sum += current;
                    index++;
                }

                Console.WriteLine("4. Сумма до сигнала остановки");
                Console.WriteLine($"Сумма введенных чисел: {sum}");
                Console.WriteLine();
            }

            {
                int[] enteredNumbers = { 7, 4, 9, 12, 0, 6 };
                int index = 0;
                int evenCount = 0;

                while (index < enteredNumbers.Length)
                {
                    int current = enteredNumbers[index];
                    if (current == 0)
                    {
                        break;
                    }

                    if (current % 2 == 0)
                    {
                        evenCount++;
                    }

                    index++;
                }

                Console.WriteLine("5. Подсчет четных чисел");
                Console.WriteLine($"Количество четных чисел: {evenCount}");
                Console.WriteLine();
            }

            {
                int number = 50724;
                int digits = 0;

                while (number > 0)
                {
                    digits++;
                    number /= 10;
                }

                Console.WriteLine("6. Количество цифр");
                Console.WriteLine($"Количество цифр: {digits}");
                Console.WriteLine();
            }

            {
                int target = 1000;
                int[] replenishments = { 150, 200, 300, 400 };
                int total = 0;
                int count = 0;
                int index = 0;

                while (index < replenishments.Length && total < target)
                {
                    total += replenishments[index];
                    count++;
                    index++;
                }

                Console.WriteLine("7. Цель накоплений");
                Console.WriteLine($"Пополнений понадобилось: {count}");
                Console.WriteLine($"Накопленная сумма: {total}");
                Console.WriteLine();
            }

            {
                string[] attempts = { "qwerty", "0000", "12345" };
                int index = 0;
                string password;

                Console.WriteLine("8. Проверка пароля");
                do
                {
                    password = attempts[index];
                    Console.WriteLine($"Попытка {index + 1}: {password}");
                    index++;

                    if (password != "12345")
                    {
                        Console.WriteLine("Пароль неверный");
                    }
                }
                while (password != "12345" && index < attempts.Length);

                Console.WriteLine("Доступ открыт");
                Console.WriteLine();
            }

            {
                int[] attempts = { -4, 0, 9 };
                int index = 0;
                int number;

                Console.WriteLine("9. Проверка положительного числа");
                do
                {
                    number = attempts[index];
                    Console.WriteLine($"Введено: {number}");
                    index++;
                }
                while (number <= 0 && index < attempts.Length);

                Console.WriteLine("Число принято");
                Console.WriteLine();
            }

            {
                string[] answers = { "да", "да", "нет" };
                int index = 0;
                string answer;

                Console.WriteLine("10. Мини-анкета");
                do
                {
                    answer = answers[index];
                    Console.WriteLine($"Ответ пользователя: {answer}");
                    index++;

                    if (answer == "да")
                    {
                        Console.WriteLine("Заполняем следующий блок анкеты...");
                    }
                }
                while (answer == "да" && index < answers.Length);

                Console.WriteLine("Анкета завершена");
                Console.WriteLine();
            }
        }
    }
}
