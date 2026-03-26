namespace CshBook.Answers.Chapter0.Lesson06Arrays
{
    internal static class AnswerLesson06Arrays
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 6. Массивы");
            Console.WriteLine("================");
            Console.WriteLine();

            {
                int[] temperatures = { 18, 20, 17, 22, 19 };

                Console.WriteLine("1. Температура за 5 дней");
                for (int i = 0; i < temperatures.Length; i++)
                {
                    Console.Write($"{temperatures[i]} ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            {
                int[] numbers = { 3, 5, 7, 2, 8, 1 };
                int sum = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    sum += numbers[i];
                }

                Console.WriteLine("2. Сумма элементов");
                Console.WriteLine($"Сумма = {sum}");
                Console.WriteLine();
            }

            {
                int[] numbers = { 9, 4, 15, 2, 11, 7 };
                int max = numbers[0];
                int min = numbers[0];

                for (int i = 1; i < numbers.Length; i++)
                {
                    if (numbers[i] > max)
                    {
                        max = numbers[i];
                    }

                    if (numbers[i] < min)
                    {
                        min = numbers[i];
                    }
                }

                Console.WriteLine("3. Максимум и минимум");
                Console.WriteLine($"Максимум: {max}");
                Console.WriteLine($"Минимум: {min}");
                Console.WriteLine();
            }

            {
                int[] numbers = { 4, 7, 10, 13, 16, 21 };
                int evenCount = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] % 2 == 0)
                    {
                        evenCount++;
                    }
                }

                Console.WriteLine("4. Количество четных чисел");
                Console.WriteLine($"Четных элементов: {evenCount}");
                Console.WriteLine();
            }

            {
                int[] numbers = { 3, 11, 8, 11, 5 };
                int target = 11;
                int foundIndex = -1;

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (numbers[i] == target)
                    {
                        foundIndex = i;
                        break;
                    }
                }

                Console.WriteLine("5. Поиск по значению");
                Console.WriteLine($"Индекс первого вхождения: {foundIndex}");
                Console.WriteLine();
            }

            {
                int[] grades = { 5, 4, 5, 3, 5 };
                int sum = 0;
                int bestGrade = grades[0];
                int fiveCount = 0;

                for (int i = 0; i < grades.Length; i++)
                {
                    sum += grades[i];

                    if (grades[i] > bestGrade)
                    {
                        bestGrade = grades[i];
                    }

                    if (grades[i] == 5)
                    {
                        fiveCount++;
                    }
                }

                double average = (double)sum / grades.Length;

                Console.WriteLine("6. Оценки ученика");
                Console.Write("Все оценки: ");
                for (int i = 0; i < grades.Length; i++)
                {
                    Console.Write($"{grades[i]} ");
                }
                Console.WriteLine();
                Console.WriteLine($"Средний балл: {average:F2}");
                Console.WriteLine($"Количество пятерок: {fiveCount}");
                Console.WriteLine($"Лучшая оценка: {bestGrade}");
                Console.WriteLine();
            }

            {
                int[] sales = { 12, 15, 9, 18, 7, 21, 10 };
                int totalSales = 0;
                int maxSales = sales[0];
                int bestDay = 1;
                int daysMoreThanTen = 0;

                for (int i = 0; i < sales.Length; i++)
                {
                    totalSales += sales[i];

                    if (sales[i] > maxSales)
                    {
                        maxSales = sales[i];
                        bestDay = i + 1;
                    }

                    if (sales[i] > 10)
                    {
                        daysMoreThanTen++;
                    }
                }

                Console.WriteLine("7. Продажи за неделю");
                Console.WriteLine($"Общая сумма: {totalSales}");
                Console.WriteLine($"Максимальная продажа была в день {bestDay}");
                Console.WriteLine($"Дней с продажами больше 10: {daysMoreThanTen}");
                Console.WriteLine();
            }

            {
                int[] numbers = { 2, 4, 6, 8, 10 };

                Console.WriteLine("8. Массив наоборот");
                for (int i = numbers.Length - 1; i >= 0; i--)
                {
                    Console.Write($"{numbers[i]} ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

            {
                int[,] table =
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 },
                    { 7, 8, 9 }
                };

                int sum = 0;
                int diagonalSum = 0;

                for (int row = 0; row < table.GetLength(0); row++)
                {
                    for (int column = 0; column < table.GetLength(1); column++)
                    {
                        sum += table[row, column];

                        if (row == column)
                        {
                            diagonalSum += table[row, column];
                        }
                    }
                }

                Console.WriteLine("9. Таблица 3 на 3");
                for (int row = 0; row < table.GetLength(0); row++)
                {
                    for (int column = 0; column < table.GetLength(1); column++)
                    {
                        Console.Write($"{table[row, column]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Сумма всех элементов: {sum}");
                Console.WriteLine($"Сумма главной диагонали: {diagonalSum}");
                Console.WriteLine();
            }

            {
                int[,] seats =
                {
                    { 1, 0, 1, 0 },
                    { 0, 0, 1, 0 },
                    { 1, 1, 1, 0 }
                };

                int totalFreeSeats = 0;
                int maxFreeSeats = 0;
                int bestRow = 1;

                for (int row = 0; row < seats.GetLength(0); row++)
                {
                    int freeSeatsInRow = 0;

                    for (int column = 0; column < seats.GetLength(1); column++)
                    {
                        if (seats[row, column] == 0)
                        {
                            totalFreeSeats++;
                            freeSeatsInRow++;
                        }
                    }

                    if (freeSeatsInRow > maxFreeSeats)
                    {
                        maxFreeSeats = freeSeatsInRow;
                        bestRow = row + 1;
                    }
                }

                Console.WriteLine("10. Места в классе");
                Console.WriteLine($"Свободных мест всего: {totalFreeSeats}");
                Console.WriteLine($"Больше всего свободных мест в строке {bestRow}");
                Console.WriteLine();
            }

            {
                int[] boxes = { 10, 0, 7, 15, 4 };
                int totalItems = 0;
                int maxItems = boxes[0];
                int bestBox = 1;
                bool hasEmptyBox = false;

                for (int i = 0; i < boxes.Length; i++)
                {
                    totalItems += boxes[i];

                    if (boxes[i] > maxItems)
                    {
                        maxItems = boxes[i];
                        bestBox = i + 1;
                    }

                    if (boxes[i] == 0)
                    {
                        hasEmptyBox = true;
                    }
                }

                Console.WriteLine("11. Мини-склад");
                Console.WriteLine($"Коробок всего: {boxes.Length}");
                Console.WriteLine($"Товара всего: {totalItems}");
                Console.WriteLine($"Больше всего товара в коробке {bestBox}");
                Console.WriteLine($"Есть пустые коробки: {hasEmptyBox}");
                Console.WriteLine();
            }

            {
                int[][] purchases =
                {
                    new int[] { 120, 80 },
                    new int[] { 50, 30, 20 },
                    new int[] { 500 }
                };

                int totalSum = 0;

                Console.WriteLine("12. Ступенчатый массив");
                for (int client = 0; client < purchases.Length; client++)
                {
                    int clientSum = 0;

                    for (int purchase = 0; purchase < purchases[client].Length; purchase++)
                    {
                        clientSum += purchases[client][purchase];
                    }

                    totalSum += clientSum;
                    Console.WriteLine($"Клиент {client + 1}: покупок = {purchases[client].Length}, сумма = {clientSum}");
                }

                Console.WriteLine($"Общая сумма всех покупок: {totalSum}");
                Console.WriteLine();
            }
        }
    }
}
