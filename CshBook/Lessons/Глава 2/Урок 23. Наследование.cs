namespace CshBook.Lessons.Chapter2.Lesson23Inheritance
{
    #region Теория
    /*
        Наследование нужно, когда несколько классов
        имеют общую часть.

        Вместо того чтобы копировать одни и те же поля и методы,
        мы выносим общее в базовый класс.
     */

    /*
        Пример:

        У собаки и кошки есть имя, возраст и способность спать.
        Значит, это можно вынести в общий класс Pet.

        А уже Dog и Cat добавят свои особенности.
     */

    /*
        Наследование записывается через двоеточие:

        class Dog : Pet

        Это читается так:
        Dog наследуется от Pet.
        Dog получает доступ к public и protected членам Pet.
     */

    /*
        Конструкторы не наследуются автоматически.

        Если базовый класс требует данные в конструкторе,
        наследник должен передать их через base(...).

        Например:

        public Dog(string name, int age) : base(name, age)
     */

    /*
        protected - это доступ для самого класса и его наследников.

        Снаружи protected-поле не видно,
        но дочерний класс может с ним работать.

        На этом этапе достаточно понимать:
        private - только внутри класса;
        protected - внутри класса и наследников;
        public - доступно снаружи.
     */

    /*
        Важно не использовать наследование "на всякий случай".

        Хороший вопрос:
        "Dog действительно является Pet?"

        Если ответ да, наследование может подойти.
        Если ответ скорее "использует" или "имеет внутри",
        чаще нужна композиция, а не наследование.
     */
    #endregion

    class Pet
    {
        public string Name { get; }
        public int Age { get; private set; }

        public Pet(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void Sleep()
        {
            Console.WriteLine($"{Name} спит.");
        }

        public void GrowOlder()
        {
            Age++;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name}, возраст: {Age}");
        }
    }

    class Dog : Pet
    {
        public string Breed { get; }

        public Dog(string name, int age, string breed) : base(name, age)
        {
            Breed = breed;
        }

        public void Bark()
        {
            Console.WriteLine($"{Name}: гав!");
        }

        public void Fetch()
        {
            Console.WriteLine($"{Name} принес палку.");
        }
    }

    class Cat : Pet
    {
        public bool IsLazy { get; }

        public Cat(string name, int age, bool isLazy) : base(name, age)
        {
            IsLazy = isLazy;
        }

        public void Meow()
        {
            Console.WriteLine(IsLazy ? $"{Name}: мяу..." : $"{Name}: МЯУ!");
        }
    }

    internal static class Lesson23Inheritance
    {
        public static void Main_()
        {
            Dog dog = new Dog("Бобик", 3, "Овчарка");
            Cat cat = new Cat("Муся", 5, true);

            dog.PrintInfo();
            dog.Bark();
            dog.Fetch();
            dog.Sleep();

            Console.WriteLine("----");

            cat.PrintInfo();
            cat.Meow();
            cat.Sleep();

            Console.WriteLine("----");

            dog.GrowOlder();
            dog.PrintInfo();
        }
    }

    #region Задачи
    /*
        Разминка

        1. Базовый класс Vehicle.
           Создай класс Vehicle с полями/свойствами Model и Speed.
           Добавь метод Move(), который выводит движение транспорта.

        2. Автомобиль.
           Создай класс Car : Vehicle.
           Добавь свойство Fuel и метод Refuel(int amount).

        3. Лодка.
           Создай класс Boat : Vehicle.
           Добавь свойство IsSailing и метод StartSailing().

        Основные задачи

        4. Конструктор через base.
           Добавь конструкторы в Vehicle, Car и Boat.
           В наследниках передавай общие данные в base(...).

        5. Общий метод PrintInfo.
           Добавь в Vehicle метод PrintInfo(),
           который выводит модель и скорость.

        6. Уникальный метод наследника.
           Добавь в Car метод Honk(),
           а в Boat метод DropAnchor().

        7. Игровые персонажи.
           Создай базовый класс Character с Name, Health и Attack.
           Создай Warrior и Mage как наследников.

        Задачи на перенос

        8. Магазин.
           Создай базовый класс Product с Title и Price.
           Создай FoodProduct и TechProduct с уникальными полями.

        9. Учебные материалы.
           Создай базовый класс LearningMaterial.
           Создай BookLesson и VideoLesson.

        10. Проверка "является".
            Придумай пример, где наследование подходит,
            и пример, где лучше использовать обычное поле внутри класса.
     */
    #endregion
}
