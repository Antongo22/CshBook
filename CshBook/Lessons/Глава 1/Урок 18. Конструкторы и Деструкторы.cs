namespace CshBook.Lessons.Chapter1.Lesson18ConstructorsAndDestructors
{
    #region Теория
    /*
        В прошлом уроке мы создавали объект,
        а потом вручную заполняли его поля:

        Pet pet = new Pet();
        pet.Name = "Бобик";
        pet.Age = 5;

        Это работает, но легко забыть заполнить важное поле.
     */

    /*
        Конструктор - это специальный метод,
        который вызывается при создании объекта.

        Его задача:
        сразу привести объект в нормальное начальное состояние.
     */

    /*
        У конструктора:

        - имя совпадает с именем класса;
        - нет типа возвращаемого значения;
        - он вызывается через new.

        Пример:

        Pet pet = new Pet("Бобик", 5);
     */

    /*
        Конструкторов может быть несколько.

        Это называется перегрузка конструкторов.

        Например:
        - Pet(string name)
        - Pet(string name, int age)

        Первый вариант может поставить возраст по умолчанию.
     */

    /*
        this означает "текущий объект".

        Обычно this помогает отличить поле объекта
        от параметра конструктора:

        this.Name = name;
     */

    /*
        Важно помнить про ссылки.

        Если написать:

        Pet second = first;

        новый объект не создается.
        Обе переменные указывают на один и тот же объект.
     */

    /*
        Деструктор в C# чаще называют финализатором.

        Он вызывается сборщиком мусора,
        но точный момент вызова мы не контролируем.

        Для обычного junior-кода финализаторы почти не нужны.
        На этом уроке важно просто знать, что такая возможность есть,
        но не строить на ней обычную логику программы.
     */
    #endregion

    class PetWithConstructor
    {
        public string Name;
        public int Age;
        public int Mood;

        public PetWithConstructor(string name)
        {
            Name = name;
            Age = 0;
            Mood = 50;
        }

        public PetWithConstructor(string name, int age)
        {
            Name = name;
            Age = age;
            Mood = 50;
        }

        public PetWithConstructor(string name, int age, int mood)
        {
            Name = name;
            Age = age;
            Mood = mood;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name}: возраст {Age}, настроение {Mood}");
        }

        public PetWithConstructor CreateCopy()
        {
            return new PetWithConstructor(Name, Age, Mood);
        }

        ~PetWithConstructor()
        {
            // Финализатор здесь только для демонстрации синтаксиса.
        }
    }

    internal static class Lesson18ConstructorsAndDestructors
    {
        public static void Main_()
        {
            PetWithConstructor bobik = new PetWithConstructor("Бобик");
            PetWithConstructor sharik = new PetWithConstructor("Шарик", 3);
            PetWithConstructor barsik = new PetWithConstructor("Барсик", 2, 80);

            bobik.PrintInfo();
            sharik.PrintInfo();
            barsik.PrintInfo();

            Console.WriteLine("----");

            PetWithConstructor sameBobik = bobik;
            sameBobik.Name = "Бобик через вторую ссылку";
            bobik.PrintInfo();

            Console.WriteLine("----");

            PetWithConstructor copy = bobik.CreateCopy();
            copy.Name = "Копия Бобика";

            bobik.PrintInfo();
            copy.PrintInfo();
        }
    }

    #region Задачи
    /*
        Разминка

        1. Конструктор питомца.
           Создай класс Pet с полями Name, Age и Mood.
           Добавь конструктор Pet(string name),
           который задает имя, возраст 0 и настроение 50.

        2. Конструктор с возрастом.
           Добавь второй конструктор Pet(string name, int age),
           который задает имя и возраст.

        3. Вывод информации.
           Добавь метод PrintInfo(),
           который выводит все поля объекта.

        Основные задачи

        4. Перегрузка конструкторов.
           Добавь третий конструктор Pet(string name, int age, int mood).

        5. this на практике.
           В конструкторах используй this,
           чтобы явно обращаться к полям объекта.

        6. Ссылка на тот же объект.
           Создай один объект Pet.
           Присвой его во вторую переменную.
           Измени поле через вторую переменную
           и выведи первую.

        7. Копия объекта.
           Добавь метод CreateCopy(),
           который возвращает новый объект Pet
           с такими же значениями полей.

        Задачи на перенос

        8. Класс Player.
           Создай класс Player с конструкторами:
           Player(string name)
           Player(string name, int level, int coins)

        9. Класс Product.
           Создай класс Product с полями Title, Price и Count.
           Через конструктор запрети создавать товар с отрицательной ценой.

        10. Обзор финализатора.
            Добавь пустой финализатор в один из классов
            и оставь комментарий, почему не стоит полагаться
            на точное время его вызова.
     */
    #endregion
}
