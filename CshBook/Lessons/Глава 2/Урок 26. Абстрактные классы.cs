namespace CshBook.Lessons.Chapter2.Lesson26AbstractClasses
{
    #region Теория
    /*
        Абстрактный класс - это базовый класс, от которого можно наследоваться,
        но нельзя создать объект напрямую.

        То есть так нельзя:

        Animal animal = new Animal();

        Если Animal абстрактный, он описывает общую идею животного,
        но конкретным объектом должен быть Dog, Cat, Bird и так далее.
     */

    /*
        Абстрактный класс объявляется через abstract:

        abstract class Animal
        {
        }

        Главная мысль:
        абстрактный класс нужен, когда базовый тип слишком общий,
        чтобы существовать сам по себе.
     */

    /*
        Внутри абстрактного класса могут быть обычные поля, свойства,
        конструкторы и методы.

        Это удобно, когда у всех наследников есть общие данные
        и общее поведение.

        Например:
        у всех работников есть Name;
        у всех фигур можно вывести название;
        у всех персонажей есть Health.
     */

    /*
        Абстрактный метод - это метод без тела.

        abstract void Work();

        Он означает:
        "каждый наследник обязан сам написать, как это работает".

        Абстрактный метод можно объявить только внутри абстрактного класса.
     */

    /*
        Наследник реализует абстрактный метод через override:

        class Developer : Employee
        {
            public override void Work()
            {
                Console.WriteLine("Пишет код");
            }
        }

        Если обычный класс наследуется от абстрактного класса,
        он обязан реализовать все абстрактные методы.
     */

    /*
        Отличие от virtual/override:

        virtual - базовый класс уже дал поведение,
        но разрешил наследнику заменить его.

        abstract - базовый класс не дал поведения,
        а заставил наследника написать его.

        Поэтому abstract строже:
        без реализации в наследнике код не скомпилируется.
     */

    /*
        Когда использовать абстрактный класс:

        1. Есть общая основа для родственных классов.
        2. Базовый объект сам по себе не имеет смысла.
        3. Часть поведения общая, а часть должна отличаться.
        4. Нужно работать с наследниками через один общий тип.

        Если классы не являются родственниками,
        в следующем уроке для этого будут интерфейсы.
     */
    #endregion

    abstract class CourseMember
    {
        public string Name { get; }

        protected CourseMember(string name)
        {
            Name = name;
        }

        public void PrintName()
        {
            Console.WriteLine($"Участник: {Name}");
        }

        public abstract void DoMainAction();
    }

    class Student : CourseMember
    {
        public Student(string name) : base(name)
        {
        }

        public override void DoMainAction()
        {
            Console.WriteLine($"{Name} решает задачи и задает вопросы.");
        }
    }

    class Mentor : CourseMember
    {
        public Mentor(string name) : base(name)
        {
        }

        public override void DoMainAction()
        {
            Console.WriteLine($"{Name} объясняет тему и проверяет решения.");
        }
    }

    abstract class Shape
    {
        public string Title { get; }

        protected Shape(string title)
        {
            Title = title;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Title}. Площадь: {GetArea()}");
        }

        public abstract double GetArea();
    }

    class Rectangle : Shape
    {
        public int Width { get; }
        public int Height { get; }

        public Rectangle(int width, int height) : base("Прямоугольник")
        {
            Width = width;
            Height = height;
        }

        public override double GetArea()
        {
            return Width * Height;
        }
    }

    class Circle : Shape
    {
        public int Radius { get; }

        public Circle(int radius) : base("Круг")
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    internal static class Lesson26AbstractClasses
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 26. Абстрактные классы");
            Console.WriteLine("===========================");
            Console.WriteLine();

            CourseMember[] members =
            {
                new Student("Антон"),
                new Mentor("Мария")
            };

            for (int i = 0; i < members.Length; i++)
            {
                members[i].PrintName();
                members[i].DoMainAction();
                Console.WriteLine();
            }

            Shape[] shapes =
            {
                new Rectangle(4, 5),
                new Circle(3)
            };

            for (int i = 0; i < shapes.Length; i++)
            {
                shapes[i].PrintInfo();
            }
        }
    }

    #region Задачи
    /*
        1. Создайте абстрактный класс Worker.
           В нем должно быть свойство Name и конструктор, который принимает имя.

        2. Добавьте в Worker обычный метод PrintName(),
           который выводит имя работника.

        3. Добавьте в Worker абстрактный метод Work().
           Создайте классы Programmer и Designer.
           Каждый класс должен по-своему реализовать Work().

        4. Создайте массив Worker[] из программиста и дизайнера.
           В цикле вызовите PrintName() и Work() для каждого работника.

        5. Добавьте в Worker абстрактный метод CalculateSalary().
           Programmer пусть возвращает 120000, Designer - 90000.
           Выведите зарплату каждого работника.

        6. Создайте абстрактный класс GameCharacter.
           У него должны быть Name, Health и обычный метод TakeDamage(int damage).

        7. Добавьте в GameCharacter абстрактный метод Attack().
           Создайте Warrior и Mage с разной атакой.

        8. Создайте массив GameCharacter[].
           Нанесите каждому персонажу урон и вызовите Attack().
    */
    #endregion
}
