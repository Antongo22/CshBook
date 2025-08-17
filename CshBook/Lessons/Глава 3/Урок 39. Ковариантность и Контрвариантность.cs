using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_3
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о ковариантности и контрвариантности в C#:
     * 
     * - Что такое вариантность типов и зачем она нужна
     * - Инвариантность, ковариантность и контрвариантность
     * - Ковариантность и контрвариантность в массивах
     * - Ковариантность и контрвариантность в делегатах
     * - Ковариантность и контрвариантность в обобщенных интерфейсах
     * - Ключевые слова out и in для обозначения вариантности
     * - Практическое применение вариантности
     */

    /*
       Что такое вариантность типов и зачем она нужна
       ======================================
       
       Вариантность типов — это свойство типов, которое определяет правила
       преобразования более специфичных типов в более общие (и наоборот)
       при работе с обобщенными типами, интерфейсами и делегатами.
       
       Вариантность позволяет использовать более конкретные или более общие типы,
       чем изначально объявленные, в определенных контекстах, что делает код
       более гибким и переиспользуемым.
       
       Существует три вида вариантности:
       1. Инвариантность (invariance) - не допускается никаких преобразований
       2. Ковариантность (covariance) - преобразование от более конкретного к более общему типу
       3. Контрвариантность (contravariance) - преобразование от более общего к более конкретному типу
    */

    /*
       Инвариантность, ковариантность и контрвариантность
       =========================================
       
       1. Инвариантность (invariance)
       
          При инвариантности типов не допускается никаких преобразований типов.
          Тип A<T> считается инвариантным относительно T, если A<Base> и A<Derived>
          не имеют отношений наследования между собой, даже если Derived наследуется от Base.
          
          Пример: большинство обобщенных классов в C# инвариантны:
          
          List<string> strings = new List<string>();
          List<object> objects = strings;  // Ошибка компиляции, несмотря на то, что string является подтипом object
       
       2. Ковариантность (covariance)
       
          При ковариантности допускается преобразование от более конкретного типа
          к более общему (от производного к базовому).
          Тип A<T> считается ковариантным относительно T, если A<Derived> можно
          рассматривать как подтип A<Base>, если Derived является подтипом Base.
          
          Пример: интерфейс IEnumerable<T> является ковариантным:
          
          IEnumerable<string> strings = new List<string>();
          IEnumerable<object> objects = strings;  // Допустимо, IEnumerable<T> ковариантен
       
       3. Контрвариантность (contravariance)
       
          При контрвариантности допускается преобразование от более общего типа
          к более конкретному (от базового к производному).
          Тип A<T> считается контрвариантным относительно T, если A<Base> можно
          рассматривать как подтип A<Derived>, если Derived является подтипом Base.
          
          Пример: интерфейс IComparer<T> является контрвариантным:
          
          IComparer<object> objectComparer = new ObjectComparer();
          IComparer<string> stringComparer = objectComparer;  // Допустимо, IComparer<T> контрвариантен
    */

    /*
       Ковариантность и контрвариантность в массивах
       ====================================
       
       Массивы в C# являются ковариантными, но это может привести к проблемам
       во время выполнения, поскольку проверка типов для массивов происходит во время выполнения:
       
       string[] strings = { "один", "два", "три" };
       object[] objects = strings;  // Допустимо, массивы ковариантны
       
       // Но это может привести к ошибке во время выполнения
       // objects[0] = 42;  // ArrayTypeMismatchException
       
       Ковариантность массивов была включена в язык C# по историческим причинам
       и для совместимости с более ранними версиями. В общем случае, лучше
       полагаться на ковариантность интерфейсов, а не массивов.
    */

    /*
       Ковариантность и контрвариантность в делегатах
       ====================================
       
       Делегаты в C# поддерживают как ковариантность для возвращаемых типов,
       так и контрвариантность для типов параметров:
       
       1. Ковариантность возвращаемого значения:
          
          delegate Base CovarianceDelegate();
          
          Derived DerivedMethod() { return new Derived(); }
          CovarianceDelegate del = DerivedMethod;  // Допустимо, ковариантность
       
       2. Контрвариантность параметров:
          
          delegate void ContravarianceDelegate(Derived param);
          
          void BaseMethod(Base param) { /* ... */ }
          ContravarianceDelegate del = BaseMethod;  // Допустимо, контрвариантность
       
       Примеры со стандартными делегатами:
       
       Func<object> funcObject = () => "строка";  // Ковариантность: string более конкретный, чем object
       Action<string> actionString = (object o) => { };  // Контрвариантность: object более общий, чем string
    */

    /*
       Ковариантность и контрвариантность в обобщенных интерфейсах
       ===============================================
       
       Начиная с C# 4.0, можно объявлять обобщенные интерфейсы как ковариантные
       или контрвариантные, используя ключевые слова out и in:
       
       1. Ковариантность (ключевое слово out):
          
          interface IProducer<out T>  // T используется только в качестве возвращаемого значения
          {
              T Produce();
          }
          
          // Использование:
          IProducer<string> stringProducer = new StringProducer();
          IProducer<object> objectProducer = stringProducer;  // Допустимо, ковариантность
       
       2. Контрвариантность (ключевое слово in):
          
          interface IConsumer<in T>  // T используется только в качестве параметра
          {
              void Consume(T item);
          }
          
          // Использование:
          IConsumer<object> objectConsumer = new ObjectConsumer();
          IConsumer<string> stringConsumer = objectConsumer;  // Допустимо, контрвариантность
       
       Стандартные интерфейсы, поддерживающие вариантность:
       
       - IEnumerable<out T> - ковариантный
       - IEnumerator<out T> - ковариантный
       - IQueryable<out T> - ковариантный
       - IGrouping<out TKey, out TElement> - ковариантный
       - IComparer<in T> - контрвариантный
       - IEqualityComparer<in T> - контрвариантный
       - IComparable<in T> - контрвариантный
    */

    /*
       Ключевые слова out и in для обозначения вариантности
       =========================================
       
       1. out - Обозначает ковариантный типовой параметр
          
          - Типовой параметр, обозначенный как out, может использоваться только
            в позиции "возвращаемого значения" (как возвращаемый тип метода или
            свойства, но не как тип параметра метода)
          - Позволяет выполнять преобразования от более конкретного типа к более общему
       
       2. in - Обозначает контрвариантный типовой параметр
          
          - Типовой параметр, обозначенный как in, может использоваться только
            в позиции "входного параметра" (как тип параметра метода, но не как
            возвращаемый тип метода или свойства)
          - Позволяет выполнять преобразования от более общего типа к более конкретному
       
       Ограничения:
       
       - Вариантность применима только к интерфейсам и делегатам, но не к классам
       - Нельзя использовать out и in для типовых параметров, которые используются
         и как входные параметры, и как возвращаемые значения
       - Нельзя использовать вариантность с ref и out параметрами методов
       - Нельзя объявлять статические методы в интерфейсах с вариантными типовыми параметрами
    */

    /*
       Практическое применение вариантности
       =============================
       
       1. Работа с коллекциями разных типов:
          
          IEnumerable<Derived> derivedCollection = new List<Derived>();
          IEnumerable<Base> baseCollection = derivedCollection;  // Благодаря ковариантности
          
          // Можно передать коллекцию строк в метод, ожидающий коллекцию объектов
          void ProcessItems(IEnumerable<object> items) { /* ... */ }
          List<string> strings = new List<string>();
          ProcessItems(strings);  // Работает благодаря ковариантности IEnumerable<T>
       
       2. Обработка событий и делегатов:
          
          // Более общий обработчик событий для разных типов данных
          void HandleEvent(object sender, EventArgs e) { /* ... */ }
          
          // Подписка на конкретное событие с более специфичным типом
          button.Click += HandleEvent;  // EventHandler<EventArgs> из HandleEvent
       
       3. Реализация фабрик и стратегий:
          
          interface IFactory<out T>
          {
              T Create();
          }
          
          class DerivedFactory : IFactory<Derived>
          {
              public Derived Create() => new Derived();
          }
          
          IFactory<Base> factory = new DerivedFactory();  // Работает благодаря ковариантности
       
       4. Сравнители и равенство:
          
          IComparer<object> generalComparer = new GeneralComparer();
          IComparer<string> stringComparer = generalComparer;  // Работает благодаря контрвариантности
          
          // Можно использовать один сравнитель для разных типов
    */

    /*
       Вариантность и ограничения типов
       =========================
       
       При использовании вариантности с обобщенными типами важно учитывать
       ограничения типов:
       
       1. Ковариантные типовые параметры (out) могут иметь ограничения class или new():
          
          interface IProducer<out T> where T : class
          {
              T Produce();
          }
       
       2. Контрвариантные типовые параметры (in) могут иметь любые ограничения:
          
          interface IConsumer<in T> where T : struct
          {
              void Consume(T item);
          }
       
       3. Вариантность и наследование:
          
          При реализации или наследовании вариантных интерфейсов необходимо
          соблюдать аннотации вариантности:
          
          interface IDerived<out T> : IBase<T>  // Если IBase<T> объявлен с out T
          {
              // ...
          }
    */
    #endregion

    #region Примеры использования ковариантности и контрвариантности

    // Базовый и производный классы для демонстрации
    public class Animal
    {
        public string Name { get; set; }
        
        public Animal(string name)
        {
            Name = name;
        }
        
        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} издает звук");
        }
        
        public override string ToString() => $"Животное: {Name}";
    }
    
    public class Dog : Animal
    {
        public string Breed { get; set; }
        
        public Dog(string name, string breed) : base(name)
        {
            Breed = breed;
        }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} (порода {Breed}) гавкает");
        }
        
        public void Fetch()
        {
            Console.WriteLine($"{Name} приносит палку");
        }
        
        public override string ToString() => $"Собака: {Name}, порода: {Breed}";
    }
    
    public class Cat : Animal
    {
        public bool IsIndoor { get; set; }
        
        public Cat(string name, bool isIndoor) : base(name)
        {
            IsIndoor = isIndoor;
        }
        
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} ({(IsIndoor ? "домашний" : "уличный")}) мяукает");
        }
        
        public void Purr()
        {
            Console.WriteLine($"{Name} мурлычет");
        }
        
        public override string ToString() => $"Кот: {Name}, {(IsIndoor ? "домашний" : "уличный")}";
    }
    
    // Ковариантный интерфейс
    public interface IProducer<out T>
    {
        T Produce();
        
        // Нельзя использовать T как параметр в ковариантном интерфейсе:
        // void Consume(T item); // Ошибка компиляции
    }
    
    // Реализация ковариантного интерфейса для разных типов
    public class DogProducer : IProducer<Dog>
    {
        private readonly string[] _breeds = { "Лабрадор", "Овчарка", "Бульдог", "Пудель", "Хаски" };
        private readonly Random _random = new Random();
        
        public Dog Produce()
        {
            string name = $"Собака_{_random.Next(1, 100)}";
            string breed = _breeds[_random.Next(_breeds.Length)];
            
            return new Dog(name, breed);
        }
    }
    
    public class CatProducer : IProducer<Cat>
    {
        private readonly Random _random = new Random();
        
        public Cat Produce()
        {
            string name = $"Кот_{_random.Next(1, 100)}";
            bool isIndoor = _random.Next(2) == 0;
            
            return new Cat(name, isIndoor);
        }
    }
    
    // Контрвариантный интерфейс
    public interface IConsumer<in T>
    {
        void Consume(T item);
        
        // Нельзя использовать T как возвращаемый тип в контрвариантном интерфейсе:
        // T Produce(); // Ошибка компиляции
    }
    
    // Реализация контрвариантного интерфейса для разных типов
    public class AnimalConsumer : IConsumer<Animal>
    {
        public void Consume(Animal animal)
        {
            Console.WriteLine($"Обрабатываем животное: {animal.Name}");
            animal.MakeSound();
        }
    }
    
    public class DogConsumer : IConsumer<Dog>
    {
        public void Consume(Dog dog)
        {
            Console.WriteLine($"Обрабатываем собаку: {dog.Name}, порода: {dog.Breed}");
            dog.MakeSound();
            dog.Fetch();
        }
    }
    
    // Интерфейс с вариантностью в нескольких параметрах
    public interface IConverter<in TSource, out TResult>
    {
        TResult Convert(TSource source);
    }
    
    // Реализация интерфейса с вариантностью в нескольких параметрах
    public class AnimalToStringConverter : IConverter<Animal, string>
    {
        public string Convert(Animal source)
        {
            return source.ToString();
        }
    }
    
    // Делегаты для демонстрации вариантности
    public delegate TResult Transformer<in TSource, out TResult>(TSource source);
    public delegate void AnimalAction<in T>(T animal) where T : Animal;
    
    #endregion

    internal class VarianceLesson
    {
        public static void Main_()
        {
            Console.WriteLine("==== Урок по ковариантности и контрвариантности в C# ====\n");

            Console.WriteLine("--- Вариантность в массивах ---");
            
            // Ковариантность массивов
            Dog[] dogs = {
                new Dog("Бобик", "Дворняжка"),
                new Dog("Рекс", "Овчарка"),
                new Dog("Шарик", "Пудель")
            };
            
            // Массивы ковариантны - можно присвоить массив собак переменной типа массив животных
            Animal[] animals = dogs;
            
            Console.WriteLine("Доступ к массиву animals (ссылается на массив dogs):");
            foreach (Animal animal in animals)
            {
                Console.WriteLine(animal);
                animal.MakeSound();
            }
            
            // Но это может привести к ошибке времени выполнения
            try
            {
                Console.WriteLine("\nПопытка добавить кота в массив animals (который на самом деле содержит собак):");
                animals[0] = new Cat("Мурзик", true);  // Вызовет ArrayTypeMismatchException
            }
            catch (ArrayTypeMismatchException ex)
            {
                Console.WriteLine($"Поймано исключение: {ex.GetType().Name}");
                Console.WriteLine("Причина: массив animals ссылается на массив типа Dog[], поэтому нельзя добавить Cat");
            }

            Console.WriteLine("\n--- Ковариантность в интерфейсах ---");
            
            // Создаем производителя собак
            IProducer<Dog> dogProducer = new DogProducer();
            Dog dog = dogProducer.Produce();
            Console.WriteLine($"Произведена собака: {dog}");
            
            // Благодаря ковариантности (out T), можно присвоить производителя собак переменной типа производитель животных
            IProducer<Animal> animalProducer = dogProducer;  // Ковариантность
            Animal animal = animalProducer.Produce();
            Console.WriteLine($"Произведено животное (на самом деле собака): {animal}");
            
            // Создаем производителя кошек
            IProducer<Cat> catProducer = new CatProducer();
            
            // Демонстрация использования ковариантности с методами
            Console.WriteLine("\nРабота метода с ковариантными параметрами:");
            ProduceAndShowAnimals(dogProducer);  // Передаем IProducer<Dog> в метод, ожидающий IProducer<Animal>
            ProduceAndShowAnimals(catProducer);  // Передаем IProducer<Cat> в метод, ожидающий IProducer<Animal>

            Console.WriteLine("\n--- Контрвариантность в интерфейсах ---");
            
            // Создаем потребителя животных
            IConsumer<Animal> animalConsumer = new AnimalConsumer();
            animalConsumer.Consume(dog);
            
            // Создаем потребителя собак
            IConsumer<Dog> dogConsumer = new DogConsumer();
            dogConsumer.Consume(dog);
            
            // Благодаря контрвариантности (in T), можно присвоить потребителя животных переменной типа потребитель собак
            dogConsumer = animalConsumer;  // Контрвариантность
            dogConsumer.Consume(dog);
            
            // Демонстрация использования контрвариантности с методами
            Console.WriteLine("\nРабота метода с контрвариантными параметрами:");
            Dog myDog = new Dog("Бим", "Сеттер");
            FeedAnimalConsumer(myDog, animalConsumer);  // Передаем IConsumer<Animal> в метод, ожидающий IConsumer<Dog>

            Console.WriteLine("\n--- Ковариантность и контрвариантность в делегатах ---");
            
            // Ковариантность делегатов по возвращаемому значению
            Func<Dog> getDog = () => new Dog("Тузик", "Такса");
            Func<Animal> getAnimal = getDog;  // Ковариантность по возвращаемому типу
            
            Animal retrievedAnimal = getAnimal();
            Console.WriteLine($"Полученное животное: {retrievedAnimal}");
            
            // Контрвариантность делегатов по типам параметров
            Action<Animal> processAnimal = a => Console.WriteLine($"Обработка животного: {a.Name}");
            Action<Dog> processDog = processAnimal;  // Контрвариантность по типу параметра
            
            processDog(new Dog("Жучка", "Колли"));
            
            // Использование кастомного делегата с вариантностью
            Transformer<Animal, string> animalToStringTransformer = a => $"Строковое представление: {a}";
            Transformer<Dog, object> dogToObjectTransformer = animalToStringTransformer;  // И ко-, и контрвариантность
            
            Console.WriteLine(dogToObjectTransformer(new Dog("Мухтар", "Овчарка")));

            Console.WriteLine("\n--- Ковариантность и контрвариантность в обобщенных интерфейсах ---");
            
            // Ковариантность в коллекциях (IEnumerable<T> является ковариантным)
            List<Dog> dogList = new List<Dog>
            {
                new Dog("Барон", "Ротвейлер"),
                new Dog("Белка", "Далматинец")
            };
            
            IEnumerable<Animal> animalList = dogList;  // Ковариантность
            
            Console.WriteLine("Список животных (на самом деле собак):");
            foreach (Animal a in animalList)
            {
                Console.WriteLine(a);
            }
            
            // Контрвариантность с компараторами (IComparer<T> является контрвариантным)
            IComparer<Animal> animalComparer = new AnimalNameComparer();
            IComparer<Dog> dogComparer = animalComparer;  // Контрвариантность
            
            Dog dog1 = new Dog("Аркан", "Бульдог");
            Dog dog2 = new Dog("Зевс", "Мопс");
            
            int comparisonResult = dogComparer.Compare(dog1, dog2);
            Console.WriteLine($"Сравнение {dog1.Name} и {dog2.Name}: {(comparisonResult < 0 ? "первая меньше" : "вторая меньше или равна")}");

            Console.WriteLine("\n--- Совместное использование ковариантности и контрвариантности ---");
            
            // Использование интерфейса с ковариантным и контрвариантным параметрами
            IConverter<Animal, string> animalToString = new AnimalToStringConverter();
            IConverter<Dog, object> dogToObject = animalToString;  // Оба вида вариантности сразу
            
            object result = dogToObject.Convert(new Dog("Альфа", "Доберман"));
            Console.WriteLine($"Результат преобразования: {result}");
        }
        
        // Метод, демонстрирующий использование ковариантности
        private static void ProduceAndShowAnimals(IProducer<Animal> producer)
        {
            Console.WriteLine($"Использование {producer.GetType().Name}:");
            
            for (int i = 0; i < 3; i++)
            {
                Animal animal = producer.Produce();
                Console.WriteLine($"- {animal}");
            }
        }
        
        // Метод, демонстрирующий использование контрвариантности
        private static void FeedAnimalConsumer(Dog dog, IConsumer<Dog> consumer)
        {
            Console.WriteLine($"Кормим {dog.Name}:");
            consumer.Consume(dog);
        }
    }
    
    // Компаратор животных для демонстрации контрвариантности
    public class AnimalNameComparer : IComparer<Animal>
    {
        public int Compare(Animal x, Animal y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }

    #region Задачи
    /*
        # Создайте ковариантный интерфейс IAnimalShelter<out T>, где T : Animal,
          с методом T GetAnimal(). Реализуйте этот интерфейс в классах DogShelter и
          CatShelter. Затем создайте метод, который принимает IAnimalShelter<Animal>
          и выводит информацию о животном. Продемонстрируйте, как можно передать в этот
          метод экземпляры DogShelter и CatShelter.
        
        # Реализуйте контрвариантный интерфейс IAnimalHandler<in T>, где T : Animal,
          с методом void HandleAnimal(T animal). Создайте класс VetDoctor, который
          реализует IAnimalHandler<Animal>. Покажите, как можно использовать экземпляр
          VetDoctor там, где требуется IAnimalHandler<Dog> или IAnimalHandler<Cat>.
        
        # Напишите обобщенный метод ProcessAnimals<T>(IEnumerable<T> animals, Action<T> action),
          где T : Animal. Создайте разные коллекции животных (собак, кошек) и действия
          для них. Используя ковариантность и контрвариантность, покажите, как можно
          вызвать этот метод с разными типами коллекций и действий.
        
        # Создайте класс SafeConverter<TSource, TTarget>, реализующий интерфейс 
          IConverter<TSource, TTarget> (определенный в уроке). Класс должен безопасно
          преобразовывать объекты из TSource в TTarget, используя проверку типа
          и приведение. Продемонстрируйте работу этого класса с разными типами,
          используя вариантность.
        
        # Реализуйте свой собственный ковариантный интерфейс ICache<out T> с методами
          T GetItem(string key) и bool TryGetItem(string key, out T item).
          Создайте его реализацию для определенных типов и продемонстрируйте
          преимущества ковариантности при работе с этим интерфейсом.
    */
    #endregion
}
