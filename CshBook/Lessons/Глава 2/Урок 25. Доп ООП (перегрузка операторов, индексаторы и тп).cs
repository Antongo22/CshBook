using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о дополнительных возможностях ООП в C#, таких как:
     * - Перегрузка операторов
     * - Индексаторы
     * - Анонимные типы
     * - Преобразования типов
     */

    /*
       Перегрузка операторов
       ====================
       
       Перегрузка операторов - это механизм, который позволяет определить особое поведение 
       стандартных операторов (+, -, *, /, ==, != и т.д.) при использовании их с объектами 
       пользовательских классов.
       
       Например, если у нас есть класс Vector, то мы можем определить, что означает 
       сложение двух векторов (операция "+"), сравнение векторов (операция "==") и т.д.
       
       Перегруженный оператор реализуется как статический метод внутри класса со специальным 
       ключевым словом operator, за которым следует символ оператора.
       
       Общий синтаксис:
       
       public static ReturnType operator Symbol(Parameters)
       {
           // Реализация
       }
       
       Где:
       - ReturnType - тип возвращаемого значения
       - Symbol - символ оператора (+, -, *, / и т.д.)
       - Parameters - параметры метода
       
       Обратите внимание, что не все операторы могут быть перегружены. Например, 
       операторы присваивания (=, +=, -=, и т.д.) нельзя перегрузить напрямую.
       
       Также, если вы перегружаете операторы сравнения, то необходимо соблюдать правила:
       - Если перегружен ==, то должен быть перегружен и !=
       - Если перегружен <, то должен быть перегружен и >
       - Если перегружен <=, то должен быть перегружен и >=
    */

    /*
       Индексаторы
       ==========
       
       Индексатор - это специальный член класса, который позволяет обращаться к объекту 
       класса как к массиву, используя индексы.
       
       Индексаторы в C# объявляются с помощью ключевого слова this и квадратных скобок, 
       содержащих параметры индекса.
       
       Общий синтаксис:
       
       public Type this[IndexType index]
       {
           get { // Код для получения элемента }
           set { // Код для установки элемента }
       }
       
       Где:
       - Type - тип значения, которое возвращает индексатор
       - IndexType - тип индекса
       
       Индексаторы могут иметь несколько параметров, как и обычные методы. Например, для 
       доступа к элементу двумерной структуры можно использовать индексатор с двумя параметрами:
       
       public Type this[IndexType1 index1, IndexType2 index2]
       {
           get { // Код для получения элемента }
           set { // Код для установки элемента }
       }
    */

    /*
       Анонимные типы
       =============
       
       Анонимный тип - это тип без явного имени, который создается компилятором C# "на лету" 
       на основе выражения инициализации.
       
       Анонимные типы позволяют создавать объекты с набором свойств без необходимости 
       определять класс заранее.
       
       Свойства анонимного типа доступны только для чтения, т.е. их нельзя изменить после 
       создания объекта.
       
       Синтаксис:
       
       var объект = new { Свойство1 = значение1, Свойство2 = значение2, ... };
       
       Например:
       
       var person = new { Name = "Иван", Age = 25 };
       
       Анонимные типы часто используются в LINQ-запросах и в других ситуациях, когда 
       нужно временно сгруппировать данные без создания отдельного класса.
    */

    /*
       Явные и неявные преобразования типов
       ==================================
       
       В C# можно определить собственные правила преобразования объектов одного типа в другой.
       
       Есть два типа преобразований:
       
       1. Неявные (implicit) - преобразования, которые можно выполнить безопасно без потери данных.
          Например, преобразование int в long.
       
       2. Явные (explicit) - преобразования, которые могут привести к потере данных или исключениям.
          Такие преобразования требуют явного указания с помощью оператора приведения типа (Type).
          Например, преобразование double в int.
       
       Чтобы определить собственные преобразования для своих типов, нужно использовать 
       операторы implicit и explicit.
       
       Синтаксис неявного преобразования:
       
       public static implicit operator ЦелевойТип(ИсходныйТип значение)
       {
           // Реализация преобразования
       }
       
       Синтаксис явного преобразования:
       
       public static explicit operator ЦелевойТип(ИсходныйТип значение)
       {
           // Реализация преобразования
       }
    */
    
    #endregion

    public class AdditionalOOP
    {
        public static void Main_()
        {
            Console.WriteLine("=== Перегрузка операторов ===");
            
            // Пример использования перегруженных операторов
            ComplexNumber a = new ComplexNumber(3, 4);
            ComplexNumber b = new ComplexNumber(1, 2);
            
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            
            ComplexNumber sum = a + b;
            Console.WriteLine($"a + b = {sum}");
            
            ComplexNumber diff = a - b;
            Console.WriteLine($"a - b = {diff}");
            
            bool areEqual = a == b;
            Console.WriteLine($"a == b: {areEqual}");
            
            bool areNotEqual = a != b;
            Console.WriteLine($"a != b: {areNotEqual}");
            
            Console.WriteLine("\n=== Индексаторы ===");
            
            // Пример использования индексаторов
            Matrix matrix = new Matrix(3, 3);
            
            // Заполняем матрицу
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = i * 3 + j + 1;
                }
            }
            
            // Выводим матрицу
            Console.WriteLine("Матрица 3x3:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
            
            // Пример использования индексатора у строки
            StringWrapper str = new StringWrapper("Hello, World!");
            Console.WriteLine($"\nСтрока: {str.Value}");
            Console.WriteLine($"Первый символ: {str[0]}");
            Console.WriteLine($"Последний символ: {str[str.Length - 1]}");
            
            // Изменяем символы в строке через индексатор
            str[0] = 'h';
            str[7] = 'w';
            Console.WriteLine($"Измененная строка: {str.Value}");
            
            Console.WriteLine("\n=== Анонимные типы ===");
            
            // Пример использования анонимных типов
            var person = new { Name = "Иван", Age = 25, City = "Москва" };
            
            Console.WriteLine($"Имя: {person.Name}");
            Console.WriteLine($"Возраст: {person.Age}");
            Console.WriteLine($"Город: {person.City}");
            
            // Нельзя изменить свойство анонимного типа
            // person.Age = 26; // Это вызовет ошибку компиляции
            
            // Создание нового анонимного объекта на основе существующего
            var olderPerson = new { person.Name, Age = person.Age + 5, person.City };
            Console.WriteLine($"Через 5 лет: {olderPerson.Name}, {olderPerson.Age} лет, {olderPerson.City}");
            
            Console.WriteLine("\n=== Преобразования типов ===");
            
            // Пример использования пользовательских преобразований типов
            Celsius celsius = new Celsius(25);
            Console.WriteLine($"Температура в Цельсиях: {celsius.Degrees}°C");
            
            // Неявное преобразование Celsius -> Fahrenheit
            Fahrenheit fahrenheit = celsius;
            Console.WriteLine($"Температура в Фаренгейтах: {fahrenheit.Degrees}°F");
            
            // Явное преобразование Fahrenheit -> Celsius
            Celsius backToCelsius = (Celsius)fahrenheit;
            Console.WriteLine($"Обратно в Цельсии: {backToCelsius.Degrees}°C");
            
            // Преобразование в double
            double tempValue = celsius;
            Console.WriteLine($"Температура как double: {tempValue}");
            
            // Создание Celsius из double
            Celsius fromDouble = 30.5;
            Console.WriteLine($"Celsius из double: {fromDouble.Degrees}°C");
        }
    }

    // Класс для демонстрации перегрузки операторов
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }
        
        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }
        
        // Перегрузка оператора сложения
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }
        
        // Перегрузка оператора вычитания
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }
        
        // Перегрузка оператора равенства
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            // Проверка на null
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return ReferenceEquals(a, b);
                
            return a.Real == b.Real && a.Imaginary == b.Imaginary;
        }
        
        // Если перегружаем ==, нужно перегрузить и !=
        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }
        
        // Переопределение методов для корректной работы с equals и хэш-кодами
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
                return this == other;
                
            return false;
        }
        
        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }
        
        // Переопределение метода ToString для красивого вывода
        public override string ToString()
        {
            if (Imaginary == 0)
                return Real.ToString();
                
            if (Imaginary < 0)
                return $"{Real} - {Math.Abs(Imaginary)}i";
                
            return $"{Real} + {Imaginary}i";
        }
    }

    // Класс для демонстрации индексаторов
    public class Matrix
    {
        private double[,] data;
        
        public Matrix(int rows, int columns)
        {
            data = new double[rows, columns];
        }
        
        // Индексатор для доступа к элементам матрицы
        public double this[int row, int column]
        {
            get 
            { 
                // Проверка границ
                if (row < 0 || row >= data.GetLength(0) || column < 0 || column >= data.GetLength(1))
                    throw new IndexOutOfRangeException("Индексы выходят за пределы матрицы");
                    
                return data[row, column]; 
            }
            set 
            { 
                // Проверка границ
                if (row < 0 || row >= data.GetLength(0) || column < 0 || column >= data.GetLength(1))
                    throw new IndexOutOfRangeException("Индексы выходят за пределы матрицы");
                    
                data[row, column] = value; 
            }
        }
        
        // Свойства для получения размеров матрицы
        public int Rows => data.GetLength(0);
        public int Columns => data.GetLength(1);
    }

    // Еще один пример использования индексаторов - обертка над строкой
    public class StringWrapper
    {
        private StringBuilder stringBuilder;
        
        public StringWrapper(string value)
        {
            stringBuilder = new StringBuilder(value);
        }
        
        // Индексатор для доступа к символам строки
        public char this[int index]
        {
            get 
            { 
                if (index < 0 || index >= stringBuilder.Length)
                    throw new IndexOutOfRangeException("Индекс выходит за пределы строки");
                    
                return stringBuilder[index]; 
            }
            set 
            { 
                if (index < 0 || index >= stringBuilder.Length)
                    throw new IndexOutOfRangeException("Индекс выходит за пределы строки");
                    
                stringBuilder[index] = value; 
            }
        }
        
        public string Value => stringBuilder.ToString();
        public int Length => stringBuilder.Length;
    }

    // Классы для демонстрации преобразования типов
    public class Celsius
    {
        public double Degrees { get; }
        
        public Celsius(double degrees)
        {
            Degrees = degrees;
        }
        
        // Неявное преобразование из double в Celsius
        public static implicit operator Celsius(double degrees)
        {
            return new Celsius(degrees);
        }
        
        // Неявное преобразование из Celsius в double
        public static implicit operator double(Celsius celsius)
        {
            return celsius.Degrees;
        }
        
        // Явное преобразование из Fahrenheit в Celsius
        public static explicit operator Celsius(Fahrenheit fahrenheit)
        {
            // Формула: (°F - 32) / 1.8 = °C
            double celsius = (fahrenheit.Degrees - 32) / 1.8;
            return new Celsius(celsius);
        }
        
        // Неявное преобразование из Celsius в Fahrenheit
        public static implicit operator Fahrenheit(Celsius celsius)
        {
            // Формула: (°C × 1.8) + 32 = °F
            double fahrenheit = (celsius.Degrees * 1.8) + 32;
            return new Fahrenheit(fahrenheit);
        }
    }

    public class Fahrenheit
    {
        public double Degrees { get; }
        
        public Fahrenheit(double degrees)
        {
            Degrees = degrees;
        }
    }

    #region Задачи
    /*
        # Создайте класс Fraction (дробь), который будет иметь два поля: числитель (Numerator) и знаменатель (Denominator).
          Реализуйте перегрузку операторов +, -, *, /, == и != для этого класса.

          
        # Создайте класс Temperature с полем value типа double. Реализуйте операторы 
          сравнения (<, >, <=, >=) и преобразования между единицами измерения температуры 
          (Кельвины, Цельсии, Фаренгейты).
          
        # Создайте класс Book с полями Title, Author и Price. Реализуйте перегрузку оператора + 
          таким образом, чтобы при сложении двух книг получалась новая книга, содержащая 
          объединение авторов и названий, а цена равнялась сумме цен исходных книг.
          
        # Реализуйте класс WebPage с индексатором, который позволяет получать доступ к любому 
          HTML-элементу по его id или className. Например, webPage["#header"] должен возвращать 
          элемент с id="header", а webPage[".menu"] должен возвращать все элементы с className="menu".
    */
    #endregion
}
