using System;

namespace CshBook.Lessons._24
{
    /* Урок 24: Полиморфизм в C#
     
    Полиморфизм - это способность объектов с одинаковой спецификацией 
    иметь различную реализацию. В C# полиморфизм реализуется через:
    - Переопределение методов (override)
    - Перегрузку методов (overload)
    - Приведение типов
    - Виртуальные методы
     */

    #region Базовый пример с животными
    class Animal
    {
        public string Name { get; set; }

        public Animal(string name)
        {
            Name = name;
        }

        // Виртуальный метод - может быть переопределен
        public virtual void MakeSound()
        {
            Console.WriteLine("Some animal sound");
        }

        // Перегрузка метода
        public void MakeSound(int volume)
        {
            Console.WriteLine($"Animal sound at volume {volume}");
        }
    }

    class Dog : Animal
    {
        public Dog(string name) : base(name) { }

        // Переопределение метода
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Woof!");
        }
    }

    class Cat : Animal
    {
        public Cat(string name) : base(name) { }

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Meow!");
        }

        // Новая реализация для кошки
        public new void MakeSound(int volume)
        {
            Console.WriteLine($"Purring at volume {volume}");
        }
    }
    #endregion

    internal class TwentyFourthLesson
    {
        public static void Main_()
        {
            // Полиморфизм через переопределение
            Animal myDog = new Dog("Rex");
            Animal myCat = new Cat("Whiskers");

            myDog.MakeSound(); // Вызовется Dog.MakeSound()
            myCat.MakeSound(); // Вызовется Cat.MakeSound()

            // Полиморфизм через перегрузку
            myDog.MakeSound(5); // Вызовется Animal.MakeSound(int)
            ((Cat)myCat).MakeSound(3); // Явное приведение к Cat

            // Массив разных животных
            Animal[] zoo = { new Dog("Buddy"), new Cat("Misty") };
            foreach (var animal in zoo)
            {
                animal.MakeSound(); // Вызовется соответствующая реализация
            }
        }
    }

    /* Виды полиморфизма:
     1. Ad-hoc (перегрузка методов)
     2. Параметрический (дженерики)
     3. Подтипов (наследование и переопределение)
     */

    #region Пример с перегрузкой
    class Calculator
    {
        // Перегрузка метода Add
        public int Add(int a, int b) => a + b;
        public double Add(double a, double b) => a + b;
        public string Add(string a, string b) => a + b;
    }
    #endregion

    /* Задание:
    Создайте класс Shape с:
    - Виртуальным методом Draw()
    - Перегруженным методом Draw(int thickness)
    Наследуйте классы Circle и Square, переопределив Draw()
    */

    /* Творческое задание:
    Реализуйте систему для рисования фигур:
    - Базовый класс GraphicObject с виртуальными методами
    - Подклассы: Line, Rectangle, Text
    - Переопределите методы отрисовки
    - Реализуйте перегрузку методов
    */

    /* Особенности полиморфизма в C#:
     1. virtual - разрешает переопределение
     2. override - заменяет реализацию
     3. new - скрывает метод родителя
     4. abstract - требует переопределения
     5. sealed - запрещает дальнейшее переопределение
     */

    #region Пример с new и sealed
    class BaseClass
    {
        public virtual void Method1() { Console.WriteLine("Base Method1"); }
        public virtual void Method2() { Console.WriteLine("Base Method2"); }
    }

    class Derived : BaseClass
    {
        public new void Method1() { Console.WriteLine("Derived Method1"); }
        public sealed override void Method2() { Console.WriteLine("Derived Method2"); }
    }

    class SecondDerived : Derived
    {
        // public override void Method2() {} // Ошибка - метод sealed
    }
    #endregion


    /* Дополнительные задачи для практики:
    
    1. Создайте класс Calculator с:
       - Виртуальным методом int Compute(int a, int b)
       - Перегруженными версиями для double и decimal
       - Наследники: AddCalculator, MultiplyCalculator
    
    2. Реализуйте систему для банковских операций:
       - Базовый класс Account с виртуальными методами Deposit/Withdraw
       - Подклассы: SavingsAccount, CheckingAccount
       - Каждый тип счета должен иметь свою логику операций
    
    3. Создайте иерархию сотрудников компании:
       - Базовый класс Employee с виртуальным методом CalculateSalary()
       - Подклассы: Manager, Developer, Intern
       - Реализуйте разную логику расчета зарплаты
    */

    /* Дополнительные пояснения:
    
    1. Разница между override и new:
       - override заменяет реализацию в иерархии
       - new создает новый метод, скрывая родительский
    
    2. Когда использовать virtual/override:
       - Когда нужна возможность изменения поведения в наследниках
       - Для реализации полиморфных вызовов через базовый класс
    
    3. Особенности производительности:
       - Виртуальные методы немного медленнее из-за vtable
       - Невиртуальные вызовы разрешаются на этапе компиляции
    */

}