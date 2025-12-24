using CshBook.Answers.Answ0;
using CshBook.Lessons;
using CshBook.Lessons.Глава_3;
using CshBook.Lessons.Глава_4;
using CshBook.Lessons.Глава_5;
using CshBook.Lessons.Дополнительно;
using CshBook.Ответы. Глава_0;

// Создаем 5 вилок (объектов для блокировки)
object[] forks = new object[5];
for (int i = 0; i < 5; i++)
{
    forks[i] = new object();
}

// Создаем и запускаем 5 философов
Thread[] philosophers = new Thread[5];
for (int i = 0; i < 5; i++)
{
    int philosopherId = i;
    philosophers[i] = new Thread(() => PhilosopherLife(philosopherId, forks));
    philosophers[i].Start();
}

// Даем философам пожить 30 секунд
Thread.Sleep(30000);

Console.WriteLine("Программа завершена");
    

static void PhilosopherLife(int id, object[] forks)
{
    Random random = new Random();

    // Определяем индексы вилок (левая и правая)
    int leftFork = id;
    int rightFork = (id + 1) % 5;

    // Чтобы избежать deadlock, философ с четным ID берет сначала левую вилку,
    // а с нечетным - сначала правую
    bool takeLeftFirst = (id % 2 == 0);

    while (true)
    {
        // Философ думает
        Console.WriteLine($"Философ {id} думает...");
        Thread.Sleep(random.Next(1000, 3000));

        // Философ хочет поесть
        Console.WriteLine($"Философ {id} хочет поесть");

        if (takeLeftFirst)
        {
            // Берем сначала левую, потом правую вилку
            lock (forks[leftFork])
            {
                Console.WriteLine($"Философ {id} взял левую вилку");

                lock (forks[rightFork])
                {
                    Console.WriteLine($"Философ {id} взял правую вилку");

                    // Философ ест
                    Console.WriteLine($"Философ {id} ЕСТ!");
                    Thread.Sleep(random.Next(1000, 3000));

                    Console.WriteLine($"Философ {id} закончил есть");
                }
            }
        }
        else
        {
            // Берем сначала правую, потом левую вилку
            lock (forks[rightFork])
            {
                Console.WriteLine($"Философ {id} взял правую вилку");

                lock (forks[leftFork])
                {
                    Console.WriteLine($"Философ {id} взял левую вилку");

                    // Философ ест
                    Console.WriteLine($"Философ {id} ЕСТ!");
                    Thread.Sleep(random.Next(1000, 3000));

                    Console.WriteLine($"Философ {id} закончил есть");
                }
            }
        }
    }
}
