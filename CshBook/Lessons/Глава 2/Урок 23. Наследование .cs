using System;

namespace CshBook.Lessons.Ch2
{
    /* Урок 23: Наследование в C#
     
    Наследование - фундаментальный принцип ООП, позволяющий:
    - Создавать новые классы на основе существующих
    - Переиспользовать код родительского класса
    - Строить иерархии объектов по принципу "общее -> частное"
    - Реализовывать полиморфное поведение
     */

    #region Базовый пример: Иерархия животных
    // Базовый (родительский) класс
    class Pet
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Pet(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Виртуальный метод - может быть переопределен в наследниках
        public virtual void MakeSound()
        {
            Console.WriteLine("Some generic pet sound");
        }

        public void Sleep()
        {
            Console.WriteLine($"{Name} is sleeping");
        }
    }

    // Производный (дочерний) класс
    class Dog : Pet // Наследование через :
    {
        public string Breed { get; set; }

        // Вызов базового конструктора через base
        public Dog(string name, int age, string breed)
            : base(name, age)
        {
            Breed = breed;
        }

        // Переопределение метода
        public override void MakeSound()
        {
            Console.WriteLine("Woof! Woof!");
        }

        // Новый метод, специфичный для Dog
        public void Fetch()
        {
            Console.WriteLine($"{Name} fetches the stick");
        }
    }

    class Cat : Pet
    {
        public bool IsLazy { get; set; }

        public Cat(string name, int age, bool isLazy)
            : base(name, age)
        {
            IsLazy = isLazy;
        }

        public override void MakeSound()
        {
            Console.WriteLine(IsLazy ? "Meow..." : "MEOW!");
        }
    }
    #endregion

    internal class TwentyThirdLesson
    {
        public static void Main_()
        {
            // Использование наследования
            Dog myDog = new Dog("Rex", 3, "Labrador");
            Cat myCat = new Cat("Whiskers", 5, true);

            myDog.MakeSound(); // Woof! Woof!
            myCat.MakeSound(); // Meow...

            myDog.Sleep();     // Унаследованный метод
            myCat.Sleep();     // Тот же унаследованный метод

            // Полиморфизм через родительский тип
            Pet[] pets = { myDog, myCat };
            foreach (var pet in pets)
            {
                pet.MakeSound(); // Вызовется соответствующая реализация
            }
        }
    }

    /* Ключевые аспекты наследования:
     1. Класс может наследовать только от одного класса
     2. Цепочка наследования: Object -> Pet -> Dog
     3. Ключевые слова:
        - virtual: разрешает переопределение метода
        - override: заменяет реализацию базового класса
        - base: доступ к членам базового класса
        - sealed: запрет на наследование
     */

    /* Задание:
    Создайте иерархию транспортных средств:
    1. Базовый класс Vehicle с:
       - Speed
       - MaxPassengers
       - Move()
    2. Производные: Car, Boat, Airplane
    3. Добавьте уникальные методы для каждого типа
    4. Реализуйте метод GetInfo()
    */

    /* Творческое задание:
    Реализуйте систему классов для RPG-игры:
    - Базовый класс Character (HP, Attack)
    - Подклассы: Warrior, Mage, Archer
    - Уникальные методы для каждого класса
    - Метод SpecialAbility()
    */

    /* Важные ограничения и особенности:
     1. Конструкторы не наследуются, но можно вызывать через base()
     2. private члены недоступны в наследниках
     3. protected члены доступны только наследникам
     4. sealed классы нельзя наследовать
        sealed class FinalClass {}
     5. Можно переопределять свойства и индексаторы
     */

    #region Пример с sealed методом
    class Parent
    {
        public virtual void Method() { }
    }

    class Child : Parent
    {
        public sealed override void Method() { } // Запрет дальнейшего переопределения
    }

    class GrandChild : Child
    {
        // public override void Method() {} // Ошибка! Метод sealed
    }
    #endregion

    /* Почему нет множественного наследования?
     - Проблема ромбовидного наследования:
       A
      / \
     B   C
      \ /
       D
     - Неоднозначность выбора методов
     - В C# разрешено только через цепочку наследования
     */
}