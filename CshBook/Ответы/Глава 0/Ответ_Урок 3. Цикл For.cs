namespace CshBook.Answers.Chapter0.Lesson03ForLoop
{
    internal static class AnswerLesson03ForLoop
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 3. Цикл for");
            Console.WriteLine("=================");
            Console.WriteLine();

            {
                Console.WriteLine("1. Приветствие по расписанию");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("You are welcome!");
                }
                Console.WriteLine();
            }

            {
                int n = 4;

                Console.WriteLine("2. Повтор фразы");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Silence is golden");
                }
                Console.WriteLine();
            }

            {
                int n = 5;

                Console.WriteLine("3. Прямой и обратный счет");
                Console.Write("От 1 до n: ");
                for (int i = 1; i <= n; i++)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();

                Console.Write("От n до 1: ");
                for (int i = n; i >= 1; i--)
                {
                    Console.Write($"{i} ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            {
                int n = 7;
                int sum = 0;

                for (int i = 1; i <= n; i++)
                {
                    sum += i;
                }

                Console.WriteLine("4. Сумма диапазона");
                Console.WriteLine($"Сумма чисел от 1 до {n} = {sum}");
                Console.WriteLine();
            }

            {
                Console.WriteLine("5. Только нужные числа");
                for (int number = 1; number <= 100; number++)
                {
                    if (number % 3 == 0 || number % 5 == 0)
                    {
                        Console.Write($"{number} ");
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            {
                int n = 7;

                Console.WriteLine("6. Таблица для одного числа");
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine($"{n} * {i} = {n * i}");
                }
                Console.WriteLine();
            }

            {
                int n = 40;

                Console.WriteLine("7. Степени двойки");
                for (int value = 1; value <= n; value *= 2)
                {
                    Console.Write($"{value} ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            {
                int height = 3;
                int width = 6;

                Console.WriteLine("8. Прямоугольник из символов");
                for (int row = 0; row < height; row++)
                {
                    for (int column = 0; column < width; column++)
                    {
                        Console.Write("#");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            {
                int n = 5;

                Console.WriteLine("9. Рамка");
                for (int row = 0; row < n; row++)
                {
                    for (int column = 0; column < n; column++)
                    {
                        bool isBorder = row == 0 || row == n - 1 || column == 0 || column == n - 1;
                        Console.Write(isBorder ? "*" : " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            {
                int rows = 3;
                int seatsPerRow = 5;

                Console.WriteLine("10. Ряды в кинотеатре");
                for (int row = 1; row <= rows; row++)
                {
                    Console.Write($"Ряд {row}: ");
                    for (int seat = 1; seat <= seatsPerRow; seat++)
                    {
                        Console.Write($"{seat} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
