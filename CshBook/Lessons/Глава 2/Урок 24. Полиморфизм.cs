namespace CshBook.Lessons.Chapter2.Lesson24Polymorphism
{
    #region Теория
    /*
        Полиморфизм - это возможность работать с разными объектами
        через один общий тип.

        Главное ощущение:
        команда одна, а поведение разное.
     */

    /*
        Например, есть базовый класс Animal
        и наследники Dog и Cat.

        Мы можем хранить их в массиве Animal[],
        вызывать MakeSound(),
        но каждый объект издаст свой звук.
     */

    /*
        Для такого поведения нужны два ключевых слова:

        virtual - базовый класс разрешает переопределить метод;
        override - наследник дает свою реализацию метода.
     */

    /*
        Если метод не virtual,
        обычный наследник не может переопределить его через override.

        Поэтому полиморфизм проектируют заранее:
        базовый класс сам показывает,
        какие методы наследники могут менять.
     */

    /*
        Полиморфизм особенно полезен,
        когда код должен работать не с конкретным Dog или Cat,
        а с любым Animal.

        Так программа становится гибче:
        можно добавить новый класс Bird,
        и общий код почти не изменится.
     */

    /*
        Не смешивай сейчас полиморфизм с перегрузкой методов.

        Перегрузка - это несколько методов с одним именем,
        но разными параметрами.

        Полиморфизм в этом уроке - это virtual/override
        и работа через базовый тип.
     */
    #endregion

    class Animal
    {
        public string Name { get; }

        public Animal(string name)
        {
            Name = name;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} издает обычный звук.");
        }

        public virtual void Move()
        {
            Console.WriteLine($"{Name} двигается.");
        }
    }

    class Dog : Animal
    {
        public Dog(string name) : base(name)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name}: гав!");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} бежит.");
        }
    }

    class Cat : Animal
    {
        public Cat(string name) : base(name)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name}: мяу!");
        }

        public override void Move()
        {
            Console.WriteLine($"{Name} крадется.");
        }
    }

    internal static class Lesson24Polymorphism
    {
        public static void Main_()
        {
            Animal[] animals =
            {
                new Dog("Бобик"),
                new Cat("Муся"),
                new Animal("Неизвестное животное")
            };

            for (int i = 0; i < animals.Length; i++)
            {
                animals[i].MakeSound();
                animals[i].Move();
                Console.WriteLine("----");
            }
        }
    }

    #region Задачи
    /*
        Разминка

        1. Базовая фигура.
           Создай класс Shape с виртуальным методом Draw().

        2. Круг и квадрат.
           Создай классы Circle и Square,
           которые наследуются от Shape
           и переопределяют Draw().

        3. Массив фигур.
           Создай массив Shape[] с кругом и квадратом.
           В цикле вызови Draw() у каждой фигуры.

        Основные задачи

        4. Площадь.
           Добавь в Shape виртуальный метод GetArea(),
           который возвращает 0.
           Переопредели его в Circle и Square.

        5. Транспорт.
           Создай базовый класс Vehicle с виртуальным методом Move().
           Создай Car, Boat и Airplane с разным поведением.

        6. Уведомления.
           Создай базовый класс Notification с виртуальным методом Send().
           Создай EmailNotification и SmsNotification.

        7. Зарплата сотрудников.
           Создай базовый класс Employee с виртуальным методом CalculateSalary().
           Создай Manager, Developer и Intern.

        Задачи на перенос

        8. Игровые атаки.
           Создай базовый класс AttackAction с виртуальным методом Execute().
           Создай SwordAttack, FireballAttack и BowAttack.

        9. Документы.
           Создай базовый класс Document с виртуальным методом Print().
           Создай PdfDocument и TextDocument.

        10. Новый наследник без изменения цикла.
            Добавь новый класс-наследник к любой задаче выше
            и проверь, что общий цикл по базовому массиву работает без переписывания.
     */
    #endregion
}
