using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_4
{
    #region Теория
    /*
     * В этом уроке ты узнаешь о LINQ (Language Integrated Query):
     * 
     * - Что такое LINQ и зачем он нужен
     * - Синтаксис запросов и методы расширения
     * - Основные операторы LINQ
     * - Отложенное выполнение (Deferred Execution)
     * - LINQ to Objects, практические примеры
     */

    /*
       Что такое LINQ и зачем он нужен
       =============================
       
       LINQ (Language Integrated Query) - это технология в .NET, которая
       добавляет возможности запросов непосредственно в языки C# и VB.NET.
       
       Основные преимущества LINQ:
       
       1. Единый синтаксис для разных источников данных
          - Коллекции в памяти (LINQ to Objects)
          - Базы данных (LINQ to SQL, Entity Framework)
          - XML документы (LINQ to XML)
          - Параллельные запросы (PLINQ)
       
       2. Строгая типизация
          - Проверка типов на этапе компиляции
          - IntelliSense поддержка в IDE
          - Рефакторинг безопасен
       
       3. Читаемость и выразительность
          - Декларативный стиль программирования
          - Код читается как естественный язык
          - Меньше циклов и условных конструкций
       
       4. Композиция операций
          - Можно объединять операции в цепочки
          - Каждая операция возвращает IEnumerable<T>
          - Легко создавать сложные запросы из простых частей
    */

    /*
       Синтаксис запросов и методы расширения
       ===================================
       
       LINQ предоставляет два способа написания запросов:
       
       1. Синтаксис запросов (Query Syntax)
          - Похож на SQL
          - Использует ключевые слова: from, where, select, orderby и др.
          
          var result = from item in collection
                       where item.Property > 10
                       select item.Name;
       
       2. Синтаксис методов (Method Syntax)
          - Использует методы расширения
          - Более гибкий, поддерживает все операторы LINQ
          
          var result = collection
                      .Where(item => item.Property > 10)
                      .Select(item => item.Name);
       
       Оба синтаксиса компилируются в одинаковый код и имеют одинаковую производительность.
       Можно смешивать оба подхода в одном запросе.
       
       Основные ключевые слова синтаксиса запросов:
       - from - определяет источник данных и переменную диапазона
       - where - фильтрация элементов
       - select - проекция (выбор данных для результата)
       - orderby - сортировка
       - group by - группировка
       - join - соединение коллекций
       - let - введение промежуточной переменной
    */

    /*
       Основные операторы LINQ
       =====================
       
       LINQ предоставляет множество операторов для работы с данными:
       
       1. Операторы фильтрации:
          - Where() - фильтрация по условию
          - OfType<T>() - фильтрация по типу
       
       2. Операторы проекции:
          - Select() - преобразование элементов
          - SelectMany() - "сплющивание" вложенных коллекций
       
       3. Операторы сортировки:
          - OrderBy() / OrderByDescending() - сортировка по возрастанию/убыванию
          - ThenBy() / ThenByDescending() - дополнительная сортировка
       
       4. Операторы группировки:
          - GroupBy() - группировка элементов
       
       5. Операторы соединения:
          - Join() - внутреннее соединение
          - GroupJoin() - групповое соединение
       
       6. Операторы агрегации:
          - Count() / LongCount() - подсчет элементов
          - Sum() / Average() / Min() / Max() - математические операции
          - Aggregate() - пользовательская агрегация
       
       7. Операторы элементов:
          - First() / FirstOrDefault() - первый элемент
          - Last() / LastOrDefault() - последний элемент
          - Single() / SingleOrDefault() - единственный элемент
          - ElementAt() / ElementAtOrDefault() - элемент по индексу
       
       8. Операторы проверки:
          - Any() - есть ли элементы, удовлетворяющие условию
          - All() - все ли элементы удовлетворяют условию
          - Contains() - содержит ли коллекция элемент
       
       9. Операторы множеств:
          - Distinct() - уникальные элементы
          - Union() - объединение множеств
          - Intersect() - пересечение множеств
          - Except() - разность множеств
       
       10. Операторы разбиения:
           - Take() - взять первые N элементов
           - Skip() - пропустить первые N элементов
           - TakeWhile() - брать элементы пока условие истинно
           - SkipWhile() - пропускать элементы пока условие истинно
    */

    /*
       Отложенное выполнение (Deferred Execution)
       =======================================
       
       Одна из ключевых особенностей LINQ - отложенное выполнение.
       
       Принцип отложенного выполнения:
       - LINQ запрос не выполняется в момент его создания
       - Выполнение происходит при итерации по результату
       - Каждая итерация выполняет запрос заново
       
       Пример:
       
       var numbers = new[] { 1, 2, 3, 4, 5 };
       var query = numbers.Where(x => x > 2); // Запрос НЕ выполняется
       
       foreach (var num in query) // Запрос выполняется ЗДЕСЬ
       {
           Console.WriteLine(num);
       }
       
       Операторы с немедленным выполнением:
       - ToArray(), ToList(), ToDictionary() - материализация в коллекцию
       - Count(), Sum(), Average(), Min(), Max() - агрегатные функции
       - First(), Last(), Single() - получение конкретного элемента
       - Any(), All() - проверочные функции
       
       Преимущества отложенного выполнения:
       - Эффективность - выполняется только необходимый код
       - Композиция - можно строить сложные запросы из простых частей
       - Актуальность данных - запрос всегда работает с текущими данными
       
       Недостатки:
       - Множественное выполнение - каждая итерация выполняет запрос заново
       - Изменение источника данных может повлиять на результат
    */
    #endregion

    public static class LinqLesson
    {
        public static void Main_()
        {
            Console.WriteLine("*** Урок 46: LINQ ***\n");

            // Подготовка тестовых данных
            var students = GetStudents();
            var courses = GetCourses();

            // Пример 1: Основные операторы фильтрации и проекции
            Console.WriteLine("--- Пример 1: Фильтрация и проекция ---");
            
            // Синтаксис методов
            var topStudents = students
                .Where(s => s.Grade >= 4.0)
                .Select(s => new { s.Name, s.Grade })
                .OrderByDescending(s => s.Grade);
            
            Console.WriteLine("Отличники (синтаксис методов):");
            foreach (var student in topStudents)
            {
                Console.WriteLine($"{student.Name}: {student.Grade:F1}");
            }
            
            // Синтаксис запросов
            var topStudentsQuery = from s in students
                                  where s.Grade >= 4.0
                                  orderby s.Grade descending
                                  select new { s.Name, s.Grade };
            
            Console.WriteLine("\nОтличники (синтаксис запросов):");
            foreach (var student in topStudentsQuery)
            {
                Console.WriteLine($"{student.Name}: {student.Grade:F1}");
            }
            
            Console.WriteLine();

            // Пример 2: Группировка данных
            Console.WriteLine("--- Пример 2: Группировка ---");
            
            var studentsByAge = students
                .GroupBy(s => s.Age)
                .OrderBy(g => g.Key);
            
            Console.WriteLine("Студенты по возрастам:");
            foreach (var ageGroup in studentsByAge)
            {
                Console.WriteLine($"Возраст {ageGroup.Key}: {ageGroup.Count()} студентов");
                foreach (var student in ageGroup)
                {
                    Console.WriteLine($"  - {student.Name} ({student.Grade:F1})");
                }
            }
            
            Console.WriteLine();

            // Пример 3: Соединение коллекций (Join)
            Console.WriteLine("--- Пример 3: Соединение коллекций ---");
            
            var studentCourses = students
                .Join(courses,
                      student => student.CourseId,
                      course => course.Id,
                      (student, course) => new 
                      { 
                          StudentName = student.Name,
                          CourseName = course.Name,
                          Grade = student.Grade,
                          Credits = course.Credits
                      })
                .OrderBy(sc => sc.StudentName);
            
            Console.WriteLine("Студенты и их курсы:");
            foreach (var sc in studentCourses)
            {
                Console.WriteLine($"{sc.StudentName} изучает {sc.CourseName} " +
                                $"(оценка: {sc.Grade:F1}, кредитов: {sc.Credits})");
            }
            
            Console.WriteLine();

            // Пример 4: Агрегатные функции
            Console.WriteLine("--- Пример 4: Агрегатные функции ---");
            
            Console.WriteLine($"Общее количество студентов: {students.Count()}");
            Console.WriteLine($"Средний балл: {students.Average(s => s.Grade):F2}");
            Console.WriteLine($"Максимальный балл: {students.Max(s => s.Grade):F1}");
            Console.WriteLine($"Минимальный балл: {students.Min(s => s.Grade):F1}");
            
            var totalCredits = students
                .Join(courses, s => s.CourseId, c => c.Id, (s, c) => c.Credits)
                .Sum();
            Console.WriteLine($"Общее количество кредитов: {totalCredits}");
            
            Console.WriteLine();

            // Пример 5: Операторы множеств
            Console.WriteLine("--- Пример 5: Операторы множеств ---");
            
            var mathStudents = students.Where(s => s.CourseId == 1).Select(s => s.Name);
            var physicsStudents = students.Where(s => s.CourseId == 2).Select(s => s.Name);
            
            Console.WriteLine("Студенты математики:");
            Console.WriteLine(string.Join(", ", mathStudents));
            
            Console.WriteLine("\nСтуденты физики:");
            Console.WriteLine(string.Join(", ", physicsStudents));
            
            Console.WriteLine("\nСтуденты, изучающие и математику, и физику:");
            var bothSubjects = mathStudents.Intersect(physicsStudents);
            Console.WriteLine(string.Join(", ", bothSubjects));
            
            Console.WriteLine("\nВсе студенты (уникальные):");
            var allStudents = mathStudents.Union(physicsStudents);
            Console.WriteLine(string.Join(", ", allStudents));
            
            Console.WriteLine();

            // Пример 6: Сложные запросы с несколькими операциями
            Console.WriteLine("--- Пример 6: Сложные запросы ---");
            
            var courseStatistics = students
                .Join(courses, s => s.CourseId, c => c.Id, (s, c) => new { Student = s, Course = c })
                .GroupBy(sc => sc.Course.Name)
                .Select(g => new
                {
                    CourseName = g.Key,
                    StudentCount = g.Count(),
                    AverageGrade = g.Average(sc => sc.Student.Grade),
                    TopStudent = g.OrderByDescending(sc => sc.Student.Grade).First().Student.Name,
                    TotalCredits = g.First().Course.Credits
                })
                .OrderByDescending(cs => cs.AverageGrade);
            
            Console.WriteLine("Статистика по курсам:");
            foreach (var stat in courseStatistics)
            {
                Console.WriteLine($"Курс: {stat.CourseName}");
                Console.WriteLine($"  Студентов: {stat.StudentCount}");
                Console.WriteLine($"  Средний балл: {stat.AverageGrade:F2}");
                Console.WriteLine($"  Лучший студент: {stat.TopStudent}");
                Console.WriteLine($"  Кредитов: {stat.TotalCredits}");
                Console.WriteLine();
            }

            // Пример 7: Отложенное выполнение
            Console.WriteLine("--- Пример 7: Отложенное выполнение ---");
            
            var numbers = new List<int> { 1, 2, 3, 4, 5 };
            
            Console.WriteLine("Создание LINQ запроса...");
            var evenNumbers = numbers.Where(n => 
            {
                Console.WriteLine($"Проверяется число {n}");
                return n % 2 == 0;
            });
            
            Console.WriteLine("Запрос создан, но еще не выполнен.");
            
            Console.WriteLine("\nПервая итерация:");
            foreach (var num in evenNumbers)
            {
                Console.WriteLine($"Четное число: {num}");
            }
            
            Console.WriteLine("\nДобавляем новые числа в коллекцию...");
            numbers.AddRange(new[] { 6, 7, 8 });
            
            Console.WriteLine("\nВторая итерация (запрос выполняется заново):");
            foreach (var num in evenNumbers)
            {
                Console.WriteLine($"Четное число: {num}");
            }
            
            Console.WriteLine("\nМатериализация запроса в список:");
            var materializedNumbers = evenNumbers.ToList();
            Console.WriteLine($"Материализованный список: [{string.Join(", ", materializedNumbers)}]");
            
            Console.WriteLine();

            // Пример 8: Практические применения
            Console.WriteLine("--- Пример 8: Практические применения ---");
            
            // Поиск дубликатов
            var grades = students.Select(s => s.Grade).ToList();
            var duplicateGrades = grades
                .GroupBy(g => g)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
            
            Console.WriteLine("Повторяющиеся оценки:");
            Console.WriteLine(string.Join(", ", duplicateGrades.Select(g => g.ToString("F1"))));
            
            // Разбиение на страницы
            int pageSize = 3;
            int pageNumber = 1;
            
            var pagedStudents = students
                .OrderBy(s => s.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
            
            Console.WriteLine($"\nСтраница {pageNumber} (размер страницы: {pageSize}):");
            foreach (var student in pagedStudents)
            {
                Console.WriteLine($"- {student.Name}");
            }
            
            // Условная фильтрация
            string nameFilter = "А"; // Фильтр по имени
            bool applyFilter = !string.IsNullOrEmpty(nameFilter);
            
            var filteredStudents = students.AsQueryable();
            if (applyFilter)
            {
                filteredStudents = filteredStudents.Where(s => s.Name.StartsWith(nameFilter));
            }
            
            Console.WriteLine($"\nСтуденты, имена которых начинаются на '{nameFilter}':");
            foreach (var student in filteredStudents)
            {
                Console.WriteLine($"- {student.Name}");
            }
            
            Console.WriteLine("\nЗавершение урока по LINQ.");
        }
        
        // Вспомогательные методы для создания тестовых данных
        
        private static List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { Id = 1, Name = "Анна Иванова", Age = 20, Grade = 4.5, CourseId = 1 },
                new Student { Id = 2, Name = "Борис Петров", Age = 21, Grade = 3.8, CourseId = 2 },
                new Student { Id = 3, Name = "Виктория Сидорова", Age = 19, Grade = 4.9, CourseId = 1 },
                new Student { Id = 4, Name = "Григорий Козлов", Age = 22, Grade = 3.2, CourseId = 3 },
                new Student { Id = 5, Name = "Дарья Морозова", Age = 20, Grade = 4.1, CourseId = 2 },
                new Student { Id = 6, Name = "Евгений Волков", Age = 21, Grade = 3.7, CourseId = 1 },
                new Student { Id = 7, Name = "Жанна Лебедева", Age = 19, Grade = 4.3, CourseId = 3 },
                new Student { Id = 8, Name = "Игорь Новikov", Age = 22, Grade = 3.9, CourseId = 2 },
                new Student { Id = 9, Name = "Анастасия Попова", Age = 20, Grade = 4.7, CourseId = 1 },
                new Student { Id = 10, Name = "Кирилл Соколов", Age = 21, Grade = 3.5, CourseId = 3 }
            };
        }
        
        private static List<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course { Id = 1, Name = "Математика", Credits = 6 },
                new Course { Id = 2, Name = "Физика", Credits = 5 },
                new Course { Id = 3, Name = "Информатика", Credits = 4 }
            };
        }
    }
    
    // Вспомогательные классы для демонстрации
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
        public int CourseId { get; set; }
    }
    
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
    }

    #region Задачи
    /*
        # Создайте коллекцию заказов (Order) с товарами (Product) и клиентами (Customer).
          Используя LINQ, найдите:
          - Топ-5 самых дорогих заказов
          - Клиентов, сделавших заказы на сумму более 1000 рублей
          - Самый популярный товар (по количеству заказов)
          - Среднюю стоимость заказа по каждому клиенту
        
        # Реализуйте систему анализа текста с помощью LINQ. Для заданного текста найдите:
          - 10 самых часто встречающихся слов
          - Слова, которые встречаются только один раз
          - Среднюю длину слова
          - Количество слов, начинающихся с каждой буквы алфавита
        
        # Создайте коллекцию сотрудников с информацией о зарплате, отделе и дате найма.
          Используя LINQ, создайте отчеты:
          - Средняя зарплата по отделам
          - Сотрудники с зарплатой выше средней по их отделу
          - Топ-3 самых "старых" сотрудника в каждом отделе
          - Общий фонд заработной платы по годам найма
        
        # Реализуйте группировку и агрегацию данных о продажах. Для коллекции продаж найдите:
          - Продажи по месяцам с общей суммой
          - Товары, которые не продавались в текущем месяце
          - Менеджеров с продажами выше среднего
          - Динамику продаж (рост/падение) по месяцам
    */
    #endregion
}
