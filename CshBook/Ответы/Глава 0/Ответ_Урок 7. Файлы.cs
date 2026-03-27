namespace CshBook.Answers.Chapter0.Lesson07Files
{
    internal static class AnswerLesson07Files
    {
        public static void Main_()
        {
            string demoFolder = Path.Combine(AppContext.BaseDirectory, "lesson07-files-demo");
            Directory.CreateDirectory(demoFolder);

            Console.WriteLine("Урок 7. Файлы");
            Console.WriteLine("==============");
            Console.WriteLine();

            {
                string path = Path.Combine(demoFolder, "note.txt");

                File.WriteAllText(path, "Это моя первая заметка в файле.");

                Console.WriteLine("1. Первая заметка");
                Console.WriteLine($"Файл создан: {Path.GetFileName(path)}");
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "note.txt");
                string content = File.ReadAllText(path);

                Console.WriteLine("2. Чтение заметки");
                Console.WriteLine(content);
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "note.txt");

                File.AppendAllText(path, Environment.NewLine + "Я учусь работать с файлами.");

                Console.WriteLine("3. Добавление строки");
                Console.WriteLine(File.ReadAllText(path));
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "diary.txt");
                string diaryEntry = "Сегодня я переписал несколько задач по C#.";

                File.WriteAllText(path, diaryEntry);

                Console.WriteLine("4. Мини-дневник");
                Console.WriteLine(File.ReadAllText(path));
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "shopping.txt");
                string[] purchases = { "Хлеб", "Молоко", "Сыр", "Яблоки" };

                File.WriteAllText(path, string.Join(Environment.NewLine, purchases));
                string[] lines = File.ReadAllLines(path);

                Console.WriteLine("5. Список покупок");
                Console.WriteLine($"Количество строк в файле: {lines.Length}");
                Console.WriteLine();
            }

            {
                string sourcePath = Path.Combine(demoFolder, "report.txt");
                string copyPath = Path.Combine(demoFolder, "report_copy.txt");

                File.WriteAllText(sourcePath, "Отчет за день готов.");
                File.Copy(sourcePath, copyPath, true);

                Console.WriteLine("6. Копия файла");
                Console.WriteLine($"Копия создана: {Path.GetFileName(copyPath)}");
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "notes.txt");

                Console.WriteLine("7. Проверка существования файла");
                Console.WriteLine(File.Exists(path) ? "Файл найден" : "Файл не найден");
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "chat.txt");
                string firstMessage = "Привет! Как дела?";
                string secondMessage = "Все хорошо, продолжаю учить C#.";

                File.WriteAllText(path, firstMessage);
                File.AppendAllText(path, Environment.NewLine + secondMessage);

                Console.WriteLine("8. История сообщений");
                Console.WriteLine(File.ReadAllText(path));
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "check.txt");
                string productName = "Ручка";
                int price = 35;
                int quantity = 4;
                int total = price * quantity;
                string checkText =
                    $"Товар: {productName}{Environment.NewLine}" +
                    $"Цена: {price}{Environment.NewLine}" +
                    $"Количество: {quantity}{Environment.NewLine}" +
                    $"Итог: {total}";

                File.WriteAllText(path, checkText);

                Console.WriteLine("9. Мини-чек");
                Console.WriteLine(File.ReadAllText(path));
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "scores.txt");
                string[] scores = { "10", "20", "15", "25" };

                File.WriteAllText(path, string.Join(Environment.NewLine, scores));

                string[] lines = File.ReadAllLines(path);
                int sum = 0;

                for (int i = 0; i < lines.Length; i++)
                {
                    sum += int.Parse(lines[i]);
                }

                Console.WriteLine("10. Таблица результатов");
                Console.WriteLine($"Сумма результатов: {sum}");
                Console.WriteLine();
            }

            {
                string path = Path.Combine(demoFolder, "notebook.txt");

                if (!File.Exists(path))
                {
                    File.WriteAllText(path, string.Empty);
                }

                File.AppendAllText(path, "Повторить работу с условными операторами." + Environment.NewLine);
                File.AppendAllText(path, "Решить еще одну задачу на массивы." + Environment.NewLine);

                Console.WriteLine("11. Простой блокнот");
                Console.WriteLine(File.ReadAllText(path));
                Console.WriteLine();
            }
        }
    }
}
