namespace CshBook.Lessons.Chapter2.Lesson27Interfaces
{
    #region Теория
    /*
        Интерфейс - это контракт.

        Он говорит:
        "класс, который реализует этот интерфейс,
        обязан иметь такие методы и свойства".

        Интерфейс не описывает, как именно класс будет это делать.
        Он описывает только, что класс должен уметь.
     */

    /*
        Интерфейс объявляется через interface:

        interface INotifier
        {
            void Send(string message);
        }

        По соглашению имена интерфейсов часто начинаются с буквы I:
        INotifier, IMovable, IPayable.
     */

    /*
        Класс реализует интерфейс через двоеточие:

        class EmailNotifier : INotifier
        {
            public void Send(string message)
            {
                Console.WriteLine(message);
            }
        }

        Метод из интерфейса должен быть public,
        потому что внешний код должен иметь возможность вызвать контрактный метод.
     */

    /*
        Интерфейс похож на абстрактный класс тем,
        что заставляет класс реализовать нужные методы.

        Но смысл другой:

        Абстрактный класс - это общая основа для родственных классов.
        Интерфейс - это возможность или роль, которую могут иметь разные классы.

        Например:
        EmailSender и SmsSender не обязаны быть родственниками,
        но оба могут уметь Send().
     */

    /*
        Переменная может иметь тип интерфейса:

        INotifier notifier = new EmailNotifier();
        notifier.Send("Привет");

        Это значит:
        "мне не важно, какой конкретно класс внутри,
        мне важно, что он умеет Send".
     */

    /*
        Один класс может реализовать несколько интерфейсов:

        class Report : IPrintable, ISavable
        {
        }

        Это удобно, когда объект имеет несколько независимых возможностей.

        Например отчет можно напечатать и сохранить,
        а уведомление можно отправить.
     */

    /*
        На этом этапе не нужно усложнять интерфейсы.

        Пока достаточно понимать три вещи:

        1. Интерфейс задает обязательные методы и свойства.
        2. Класс реализует интерфейс и пишет реальный код.
        3. Через интерфейс можно работать с разными классами одинаково.
     */
    #endregion

    interface INotifier
    {
        void Send(string message);
    }

    class EmailNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email: {message}");
        }
    }

    class SmsNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"SMS: {message}");
        }
    }

    interface IPayable
    {
        int CalculatePayment();
    }

    class Employee : IPayable
    {
        public string Name { get; }
        public int Salary { get; }

        public Employee(string name, int salary)
        {
            Name = name;
            Salary = salary;
        }

        public int CalculatePayment()
        {
            return Salary;
        }
    }

    class Freelancer : IPayable
    {
        public string Name { get; }
        public int Hours { get; }
        public int Rate { get; }

        public Freelancer(string name, int hours, int rate)
        {
            Name = name;
            Hours = hours;
            Rate = rate;
        }

        public int CalculatePayment()
        {
            return Hours * Rate;
        }
    }

    interface IPrintable
    {
        void Print();
    }

    interface ISavable
    {
        void Save();
    }

    class Report : IPrintable, ISavable
    {
        public string Title { get; }

        public Report(string title)
        {
            Title = title;
        }

        public void Print()
        {
            Console.WriteLine($"Печатаем отчет: {Title}");
        }

        public void Save()
        {
            Console.WriteLine($"Сохраняем отчет: {Title}");
        }
    }

    internal static class Lesson27Interfaces
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 27. Интерфейсы");
            Console.WriteLine("===================");
            Console.WriteLine();

            INotifier[] notifiers =
            {
                new EmailNotifier(),
                new SmsNotifier()
            };

            for (int i = 0; i < notifiers.Length; i++)
            {
                notifiers[i].Send("Домашнее задание проверено.");
            }

            Console.WriteLine();

            IPayable[] payables =
            {
                new Employee("Анна", 100000),
                new Freelancer("Игорь", 40, 1500)
            };

            for (int i = 0; i < payables.Length; i++)
            {
                Console.WriteLine($"К выплате: {payables[i].CalculatePayment()}");
            }

            Console.WriteLine();

            Report report = new Report("Итоги месяца");
            report.Print();
            report.Save();
        }
    }

    #region Задачи
    /*
        1. Создайте интерфейс INotificationSender
           с методом Send(string message).

        2. Создайте классы EmailSender и TelegramSender.
           Оба класса должны реализовать INotificationSender,
           но выводить сообщение по-разному.

        3. Создайте массив INotificationSender[].
           Положите туда EmailSender и TelegramSender.
           В цикле отправьте одно сообщение через каждого отправителя.

        4. Создайте интерфейс IMovable
           с методом Move().
           Создайте классы Player и Enemy, которые реализуют IMovable.

        5. Создайте интерфейс IDamageable
           с методом TakeDamage(int damage).
           Пусть Enemy реализует и IMovable, и IDamageable.

        6. Создайте интерфейс IReadable
           с методом Read().
           Создайте классы Book и Article.
           В цикле через IReadable[] вызовите Read().

        7. Создайте интерфейс IExportable
           с методом Export().
           Создайте класс Invoice, который умеет Print() обычным методом
           и Export() через интерфейс.
    */
    #endregion
}
