using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Глава_5
{
    #region Теория
    /*
     * Паттерн Builder (Строитель)
     * ===========================
     *
     * Builder — это порождающий паттерн проектирования, который позволяет создавать
     * сложные объекты пошагово. Паттерн даёт возможность использовать один и тот же
     * код для создания различных представлений объектов.
     *
     * Проблема:
     * Представь, что у тебя есть класс с большим количеством параметров (10-20 полей).
     * Создание такого объекта превращается в кошмар:
     * - Конструктор с огромным количеством параметров
     * - Множество перегруженных конструкторов для разных комбинаций
     * - Трудно понять, что означает каждый параметр
     * - Сложно добавлять новые параметры
     *
     * Решение:
     * Builder разбивает процесс создания объекта на отдельные шаги. Ты не вызываешь
     * конструктор напрямую, а используешь специальный объект-строитель, который
     * предоставляет методы для установки каждого параметра.
     *
     * Когда использовать Builder:
     * - Объект имеет много параметров (больше 4-5)
     * - Многие параметры необязательные
     * - Нужна валидация перед созданием объекта
     * - Создание объекта — сложный многошаговый процесс
     * - Нужно создавать разные представления одного объекта
     * - Хочется сделать код более читаемым
     *
     * Преимущества:
     * + Пошаговое создание объектов
     * + Читаемый код (self-documenting)
     * + Повторное использование кода для создания разных представлений
     * + Валидация перед созданием объекта
     * + Fluent-интерфейс (цепочка вызовов)
     * + Изоляция сложного кода конструирования
     *
     * Недостатки:
     * - Усложнение кода (дополнительные классы)
     * - Может быть избыточным для простых объектов
     *
     * Отличия от других паттернов:
     * - Factory Method: создаёт объект одним вызовом, Builder — пошагово
     * - Abstract Factory: создаёт семейства связанных объектов
     * - Constructor: Builder — это альтернатива конструкторам с множеством параметров
     */

    /*
       Структура паттерна Builder
       ==========================
       
       1. Product (Продукт)
          - Сложный объект, который нужно создать
          - Может иметь множество полей и параметров
       
       2. Builder (Строитель)
          - Интерфейс или абстрактный класс
          - Определяет методы для создания частей продукта
       
       3. ConcreteBuilder (Конкретный строитель)
          - Реализует интерфейс Builder
          - Создаёт и собирает части продукта
          - Предоставляет метод для получения результата
       
       4. Director (Распорядитель) - необязательный
          - Знает, в какой последовательности вызывать методы строителя
          - Инкапсулирует процесс создания для часто используемых конфигураций
       
       5. Client (Клиент)
          - Использует Builder для создания объекта
          - Может работать с Director или напрямую с Builder
    */
    #endregion

    #region Пример 1: Классический Builder

    // Product — сложный объект для создания
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }

        // Приватный конструктор — объект можно создать только через Builder
        private Person() { }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== Информация о человеке ===");
            sb.AppendLine($"Имя: {FirstName} {LastName}");
            if (Age > 0) sb.AppendLine($"Возраст: {Age}");
            if (!string.IsNullOrEmpty(Email)) sb.AppendLine($"Email: {Email}");
            if (!string.IsNullOrEmpty(Phone)) sb.AppendLine($"Телефон: {Phone}");
            if (!string.IsNullOrEmpty(Address)) sb.AppendLine($"Адрес: {Address}, {City}, {Country}");
            if (!string.IsNullOrEmpty(Occupation)) sb.AppendLine($"Профессия: {Occupation} в {Company}");
            return sb.ToString();
        }

        // Nested Builder — внутренний класс Builder
        public class Builder
        {
            private Person _person = new Person();

            public Builder WithFirstName(string firstName)
            {
                _person.FirstName = firstName;
                return this;
            }

            public Builder WithLastName(string lastName)
            {
                _person.LastName = lastName;
                return this;
            }

            public Builder WithAge(int age)
            {
                _person.Age = age;
                return this;
            }

            public Builder WithEmail(string email)
            {
                _person.Email = email;
                return this;
            }

            public Builder WithPhone(string phone)
            {
                _person.Phone = phone;
                return this;
            }

            public Builder WithAddress(string address)
            {
                _person.Address = address;
                return this;
            }

            public Builder WithCity(string city)
            {
                _person.City = city;
                return this;
            }

            public Builder WithCountry(string country)
            {
                _person.Country = country;
                return this;
            }

            public Builder WithOccupation(string occupation)
            {
                _person.Occupation = occupation;
                return this;
            }

            public Builder WithCompany(string company)
            {
                _person.Company = company;
                return this;
            }

            // Метод для получения готового объекта
            public Person Build()
            {
                // Можно добавить валидацию
                if (string.IsNullOrEmpty(_person.FirstName) || string.IsNullOrEmpty(_person.LastName))
                {
                    throw new InvalidOperationException("Имя и фамилия обязательны");
                }

                return _person;
            }
        }
    }

    #endregion

    #region Пример 2: Fluent Builder для Computer

    // Product — компьютер
    public class Computer
    {
        public string CPU { get; set; }
        public string GPU { get; set; }
        public int RAM { get; set; }
        public int Storage { get; set; }
        public string StorageType { get; set; }
        public string MotherBoard { get; set; }
        public string PowerSupply { get; set; }
        public string Case { get; set; }
        public List<string> ExtraComponents { get; set; } = new List<string>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Конфигурация компьютера ===");
            sb.AppendLine($"Процессор: {CPU}");
            sb.AppendLine($"Видеокарта: {GPU}");
            sb.AppendLine($"Оперативная память: {RAM} GB");
            sb.AppendLine($"Накопитель: {Storage} GB {StorageType}");
            sb.AppendLine($"Материнская плата: {MotherBoard}");
            sb.AppendLine($"Блок питания: {PowerSupply}");
            sb.AppendLine($"Корпус: {Case}");
            if (ExtraComponents.Count > 0)
            {
                sb.AppendLine($"Доп. компоненты: {string.Join(", ", ExtraComponents)}");
            }
            return sb.ToString();
        }
    }

    // Fluent Builder для Computer
    public class ComputerBuilder
    {
        private Computer _computer = new Computer();

        public ComputerBuilder WithCPU(string cpu)
        {
            _computer.CPU = cpu;
            return this;
        }

        public ComputerBuilder WithGPU(string gpu)
        {
            _computer.GPU = gpu;
            return this;
        }

        public ComputerBuilder WithRAM(int ram)
        {
            _computer.RAM = ram;
            return this;
        }

        public ComputerBuilder WithStorage(int storage, string type)
        {
            _computer.Storage = storage;
            _computer.StorageType = type;
            return this;
        }

        public ComputerBuilder WithMotherBoard(string motherBoard)
        {
            _computer.MotherBoard = motherBoard;
            return this;
        }

        public ComputerBuilder WithPowerSupply(string powerSupply)
        {
            _computer.PowerSupply = powerSupply;
            return this;
        }

        public ComputerBuilder WithCase(string computerCase)
        {
            _computer.Case = computerCase;
            return this;
        }

        public ComputerBuilder AddExtraComponent(string component)
        {
            _computer.ExtraComponents.Add(component);
            return this;
        }

        public Computer Build()
        {
            // Валидация минимальных требований
            if (string.IsNullOrEmpty(_computer.CPU))
                throw new InvalidOperationException("Процессор обязателен");
            if (_computer.RAM <= 0)
                throw new InvalidOperationException("Требуется указать объём RAM");

            return _computer;
        }
    }

    #endregion

    #region Пример 3: Builder с валидацией для Email

    // Product — email сообщение
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> CC { get; set; } = new List<string>();
        public List<string> BCC { get; set; } = new List<string>();
        public List<string> Attachments { get; set; } = new List<string>();
        public bool IsHtml { get; set; }
        public int Priority { get; set; } = 0; // 0 - normal, 1 - high, -1 - low

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Email сообщение ===");
            sb.AppendLine($"От: {From}");
            sb.AppendLine($"Кому: {To}");
            if (CC.Count > 0) sb.AppendLine($"Копия: {string.Join(", ", CC)}");
            if (BCC.Count > 0) sb.AppendLine($"Скрытая копия: {string.Join(", ", BCC)}");
            sb.AppendLine($"Тема: {Subject}");
            sb.AppendLine($"Тело: {Body}");
            sb.AppendLine($"HTML: {(IsHtml ? "Да" : "Нет")}");
            sb.AppendLine($"Приоритет: {(Priority == 1 ? "Высокий" : Priority == -1 ? "Низкий" : "Обычный")}");
            if (Attachments.Count > 0) sb.AppendLine($"Вложения: {string.Join(", ", Attachments)}");
            return sb.ToString();
        }
    }

    // Builder с продвинутой валидацией
    public class EmailMessageBuilder
    {
        private EmailMessage _email = new EmailMessage();

        public EmailMessageBuilder From(string from)
        {
            if (!IsValidEmail(from))
                throw new ArgumentException("Некорректный email отправителя");
            _email.From = from;
            return this;
        }

        public EmailMessageBuilder To(string to)
        {
            if (!IsValidEmail(to))
                throw new ArgumentException("Некорректный email получателя");
            _email.To = to;
            return this;
        }

        public EmailMessageBuilder WithSubject(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Тема не может быть пустой");
            _email.Subject = subject;
            return this;
        }

        public EmailMessageBuilder WithBody(string body)
        {
            _email.Body = body ?? "";
            return this;
        }

        public EmailMessageBuilder AddCC(string cc)
        {
            if (!IsValidEmail(cc))
                throw new ArgumentException($"Некорректный email: {cc}");
            _email.CC.Add(cc);
            return this;
        }

        public EmailMessageBuilder AddBCC(string bcc)
        {
            if (!IsValidEmail(bcc))
                throw new ArgumentException($"Некорректный email: {bcc}");
            _email.BCC.Add(bcc);
            return this;
        }

        public EmailMessageBuilder AddAttachment(string filePath)
        {
            _email.Attachments.Add(filePath);
            return this;
        }

        public EmailMessageBuilder AsHtml()
        {
            _email.IsHtml = true;
            return this;
        }

        public EmailMessageBuilder WithHighPriority()
        {
            _email.Priority = 1;
            return this;
        }

        public EmailMessageBuilder WithLowPriority()
        {
            _email.Priority = -1;
            return this;
        }

        public EmailMessage Build()
        {
            // Проверка обязательных полей
            if (string.IsNullOrEmpty(_email.From))
                throw new InvalidOperationException("Не указан отправитель");
            if (string.IsNullOrEmpty(_email.To))
                throw new InvalidOperationException("Не указан получатель");
            if (string.IsNullOrEmpty(_email.Subject))
                throw new InvalidOperationException("Не указана тема письма");

            return _email;
        }

        // Упрощённая валидация email
        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }
    }

    #endregion

    #region Пример 4: Builder для SQL-запросов

    // Product — SQL запрос
    public class SqlQuery
    {
        public List<string> SelectFields { get; set; } = new List<string>();
        public string TableName { get; set; }
        public List<string> WhereConditions { get; set; } = new List<string>();
        public List<string> OrderByFields { get; set; } = new List<string>();
        public int? Limit { get; set; }

        public string ToSql()
        {
            if (string.IsNullOrEmpty(TableName))
                throw new InvalidOperationException("Не указана таблица");

            var sb = new StringBuilder();
            
            // SELECT
            sb.Append("SELECT ");
            if (SelectFields.Count == 0)
                sb.Append("*");
            else
                sb.Append(string.Join(", ", SelectFields));
            
            // FROM
            sb.Append($" FROM {TableName}");
            
            // WHERE
            if (WhereConditions.Count > 0)
            {
                sb.Append(" WHERE ");
                sb.Append(string.Join(" AND ", WhereConditions));
            }
            
            // ORDER BY
            if (OrderByFields.Count > 0)
            {
                sb.Append(" ORDER BY ");
                sb.Append(string.Join(", ", OrderByFields));
            }
            
            // LIMIT
            if (Limit.HasValue)
            {
                sb.Append($" LIMIT {Limit.Value}");
            }
            
            return sb.ToString();
        }
    }

    // Fluent Builder для SQL
    public class SqlQueryBuilder
    {
        private SqlQuery _query = new SqlQuery();

        public SqlQueryBuilder Select(params string[] fields)
        {
            _query.SelectFields.AddRange(fields);
            return this;
        }

        public SqlQueryBuilder From(string tableName)
        {
            _query.TableName = tableName;
            return this;
        }

        public SqlQueryBuilder Where(string condition)
        {
            _query.WhereConditions.Add(condition);
            return this;
        }

        public SqlQueryBuilder OrderBy(string field, bool ascending = true)
        {
            _query.OrderByFields.Add(ascending ? field : $"{field} DESC");
            return this;
        }

        public SqlQueryBuilder Limit(int limit)
        {
            _query.Limit = limit;
            return this;
        }

        public SqlQuery Build()
        {
            return _query;
        }
    }

    #endregion

    #region Пример 5: Builder для HTML

    // Product — HTML элемент
    public class HtmlElement
    {
        public string Tag { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

        public string ToHtml(int indent = 0)
        {
            var sb = new StringBuilder();
            var indentStr = new string(' ', indent * 2);

            // Открывающий тег
            sb.Append($"{indentStr}<{Tag}");

            // Атрибуты
            foreach (var attr in Attributes)
            {
                sb.Append($" {attr.Key}=\"{attr.Value}\"");
            }

            sb.Append(">");

            // Содержимое
            if (!string.IsNullOrEmpty(Content))
            {
                sb.Append(Content);
            }

            // Дочерние элементы
            if (Children.Count > 0)
            {
                sb.AppendLine();
                foreach (var child in Children)
                {
                    sb.Append(child.ToHtml(indent + 1));
                }
                sb.Append(indentStr);
            }

            // Закрывающий тег
            sb.AppendLine($"</{Tag}>");

            return sb.ToString();
        }
    }

    // Builder для HTML
    public class HtmlBuilder
    {
        private HtmlElement _root;

        public HtmlBuilder(string tag)
        {
            _root = new HtmlElement { Tag = tag };
        }

        public HtmlBuilder WithContent(string content)
        {
            _root.Content = content;
            return this;
        }

        public HtmlBuilder WithAttribute(string name, string value)
        {
            _root.Attributes[name] = value;
            return this;
        }

        public HtmlBuilder WithClass(string className)
        {
            if (_root.Attributes.ContainsKey("class"))
                _root.Attributes["class"] += " " + className;
            else
                _root.Attributes["class"] = className;
            return this;
        }

        public HtmlBuilder WithId(string id)
        {
            _root.Attributes["id"] = id;
            return this;
        }

        public HtmlBuilder AddChild(HtmlElement child)
        {
            _root.Children.Add(child);
            return this;
        }

        public HtmlBuilder AddChild(string tag, string content)
        {
            var child = new HtmlElement { Tag = tag, Content = content };
            _root.Children.Add(child);
            return this;
        }

        public HtmlElement Build()
        {
            return _root;
        }
    }

    #endregion

    #region Пример 6: Builder с Director (Pizza)

    // Product — пицца
    public class Pizza
    {
        public string Dough { get; set; }
        public string Sauce { get; set; }
        public List<string> Toppings { get; set; } = new List<string>();
        public string Size { get; set; }
        public bool ExtraCheese { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== Пицца ({Size}) ===");
            sb.AppendLine($"Тесто: {Dough}");
            sb.AppendLine($"Соус: {Sauce}");
            sb.AppendLine($"Топпинги: {string.Join(", ", Toppings)}");
            if (ExtraCheese) sb.AppendLine("+ Дополнительный сыр");
            return sb.ToString();
        }
    }

    // Builder для Pizza
    public class PizzaBuilder
    {
        private Pizza _pizza = new Pizza();

        public PizzaBuilder SetDough(string dough)
        {
            _pizza.Dough = dough;
            return this;
        }

        public PizzaBuilder SetSauce(string sauce)
        {
            _pizza.Sauce = sauce;
            return this;
        }

        public PizzaBuilder AddTopping(string topping)
        {
            _pizza.Toppings.Add(topping);
            return this;
        }

        public PizzaBuilder SetSize(string size)
        {
            _pizza.Size = size;
            return this;
        }

        public PizzaBuilder AddExtraCheese()
        {
            _pizza.ExtraCheese = true;
            return this;
        }

        public Pizza Build()
        {
            if (string.IsNullOrEmpty(_pizza.Dough))
                throw new InvalidOperationException("Не выбрано тесто");
            if (string.IsNullOrEmpty(_pizza.Size))
                throw new InvalidOperationException("Не выбран размер");

            return _pizza;
        }

        public void Reset()
        {
            _pizza = new Pizza();
        }
    }

    // Director — знает, как готовить конкретные виды пиццы
    public class PizzaDirector
    {
        public Pizza MakeMargherita(string size)
        {
            return new PizzaBuilder()
                .SetSize(size)
                .SetDough("Тонкое")
                .SetSauce("Томатный")
                .AddTopping("Моцарелла")
                .AddTopping("Базилик")
                .AddTopping("Томаты")
                .Build();
        }

        public Pizza MakePepperoni(string size)
        {
            return new PizzaBuilder()
                .SetSize(size)
                .SetDough("Традиционное")
                .SetSauce("Томатный")
                .AddTopping("Моцарелла")
                .AddTopping("Пепперони")
                .AddExtraCheese()
                .Build();
        }

        public Pizza MakeVegetarian(string size)
        {
            return new PizzaBuilder()
                .SetSize(size)
                .SetDough("Цельнозерновое")
                .SetSauce("Томатный")
                .AddTopping("Моцарелла")
                .AddTopping("Перец")
                .AddTopping("Лук")
                .AddTopping("Грибы")
                .AddTopping("Оливки")
                .Build();
        }

        public Pizza MakeHawaiian(string size)
        {
            return new PizzaBuilder()
                .SetSize(size)
                .SetDough("Традиционное")
                .SetSauce("Томатный")
                .AddTopping("Моцарелла")
                .AddTopping("Ветчина")
                .AddTopping("Ананас")
                .Build();
        }
    }

    #endregion

    #region Демонстрация

    public static class BuilderDemo
    {
        public static void RunDemo()
        {
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║     ДЕМОНСТРАЦИЯ ПАТТЕРНА BUILDER (СТРОИТЕЛЬ)  ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝\n");

            DemoClassicBuilder();
            DemoFluentComputerBuilder();
            DemoEmailBuilder();
            DemoSqlBuilder();
            DemoHtmlBuilder();
            DemoPizzaWithDirector();
        }

        private static void DemoClassicBuilder()
        {
            Console.WriteLine("\n[1] === Классический Builder для Person ===\n");

            // Создаём объект Person через Builder
            var person = new Person.Builder()
                .WithFirstName("Иван")
                .WithLastName("Петров")
                .WithAge(30)
                .WithEmail("ivan@example.com")
                .WithPhone("+7 (999) 123-45-67")
                .WithAddress("ул. Ленина, 10")
                .WithCity("Москва")
                .WithCountry("Россия")
                .WithOccupation("Разработчик")
                .WithCompany("TechCorp")
                .Build();

            Console.WriteLine(person);

            // Минимальный вариант
            var minimalPerson = new Person.Builder()
                .WithFirstName("Анна")
                .WithLastName("Сидорова")
                .Build();

            Console.WriteLine(minimalPerson);
        }

        private static void DemoFluentComputerBuilder()
        {
            Console.WriteLine("\n[2] === Fluent Builder для Computer ===\n");

            // Игровой компьютер
            var gamingPC = new ComputerBuilder()
                .WithCPU("Intel Core i9-13900K")
                .WithGPU("NVIDIA RTX 4090")
                .WithRAM(32)
                .WithStorage(2000, "NVMe SSD")
                .WithMotherBoard("ASUS ROG Maximus Z790")
                .WithPowerSupply("1000W 80+ Gold")
                .WithCase("NZXT H710i")
                .AddExtraComponent("RGB подсветка")
                .AddExtraComponent("Водяное охлаждение")
                .Build();

            Console.WriteLine(gamingPC);

            // Офисный компьютер
            var officePC = new ComputerBuilder()
                .WithCPU("Intel Core i5-13400")
                .WithGPU("Intel UHD Graphics")
                .WithRAM(16)
                .WithStorage(512, "SSD")
                .WithMotherBoard("MSI B760M")
                .WithPowerSupply("500W")
                .WithCase("Standard Tower")
                .Build();

            Console.WriteLine(officePC);
        }

        private static void DemoEmailBuilder()
        {
            Console.WriteLine("\n[3] === Builder с валидацией для Email ===\n");

            try
            {
                var email = new EmailMessageBuilder()
                    .From("sender@company.com")
                    .To("recipient@example.com")
                    .WithSubject("Важное уведомление")
                    .WithBody("Это тело письма с важной информацией.")
                    .AddCC("manager@company.com")
                    .AddBCC("archive@company.com")
                    .AddAttachment("report.pdf")
                    .AddAttachment("presentation.pptx")
                    .AsHtml()
                    .WithHighPriority()
                    .Build();

                Console.WriteLine(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Попытка создать невалидное письмо
            Console.WriteLine("\n--- Попытка создать письмо без получателя ---");
            try
            {
                var invalidEmail = new EmailMessageBuilder()
                    .From("sender@company.com")
                    .WithSubject("Тест")
                    .Build(); // Ошибка: нет получателя
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ожидаемая ошибка: {ex.Message}");
            }
        }

        private static void DemoSqlBuilder()
        {
            Console.WriteLine("\n[4] === Builder для SQL-запросов ===\n");

            // Простой запрос
            var query1 = new SqlQueryBuilder()
                .Select("id", "name", "email")
                .From("users")
                .Build();

            Console.WriteLine("Запрос 1:");
            Console.WriteLine(query1.ToSql());
            Console.WriteLine();

            // Запрос с фильтрацией и сортировкой
            var query2 = new SqlQueryBuilder()
                .Select("id", "name", "price")
                .From("products")
                .Where("price > 100")
                .Where("category = 'Electronics'")
                .OrderBy("price", ascending: false)
                .Limit(10)
                .Build();

            Console.WriteLine("Запрос 2:");
            Console.WriteLine(query2.ToSql());
            Console.WriteLine();

            // Запрос всех полей
            var query3 = new SqlQueryBuilder()
                .From("orders")
                .Where("status = 'pending'")
                .OrderBy("created_at")
                .Build();

            Console.WriteLine("Запрос 3:");
            Console.WriteLine(query3.ToSql());
        }

        private static void DemoHtmlBuilder()
        {
            Console.WriteLine("\n[5] === Builder для HTML ===\n");

            // Простой элемент
            var paragraph = new HtmlBuilder("p")
                .WithContent("Это простой параграф текста.")
                .WithClass("text-muted")
                .Build();

            Console.WriteLine("HTML элемент 1:");
            Console.WriteLine(paragraph.ToHtml());

            // Сложная структура
            var div = new HtmlBuilder("div")
                .WithId("container")
                .WithClass("main-content")
                .AddChild("h1", "Добро пожаловать")
                .AddChild("p", "Это пример использования HTML Builder.")
                .Build();

            Console.WriteLine("HTML элемент 2:");
            Console.WriteLine(div.ToHtml());

            // Список
            var list = new HtmlBuilder("ul")
                .WithClass("menu")
                .AddChild("li", "Главная")
                .AddChild("li", "О нас")
                .AddChild("li", "Контакты")
                .Build();

            Console.WriteLine("HTML элемент 3:");
            Console.WriteLine(list.ToHtml());
        }

        private static void DemoPizzaWithDirector()
        {
            Console.WriteLine("\n[6] === Builder с Director (Pizza) ===\n");

            var director = new PizzaDirector();

            // Использование Director для создания стандартных пицц
            Console.WriteLine("--- Использование Director ---\n");

            var margherita = director.MakeMargherita("Средняя");
            Console.WriteLine(margherita);

            var pepperoni = director.MakePepperoni("Большая");
            Console.WriteLine(pepperoni);

            var vegetarian = director.MakeVegetarian("Средняя");
            Console.WriteLine(vegetarian);

            // Создание кастомной пиццы напрямую через Builder
            Console.WriteLine("--- Кастомная пицца через Builder ---\n");

            var customPizza = new PizzaBuilder()
                .SetSize("Маленькая")
                .SetDough("Тонкое")
                .SetSauce("Сливочный")
                .AddTopping("Моцарелла")
                .AddTopping("Лосось")
                .AddTopping("Укроп")
                .AddTopping("Каперсы")
                .AddExtraCheese()
                .Build();

            Console.WriteLine(customPizza);
        }

        public static void Main_()
        {
            RunDemo();
        }
    }

    #endregion
}

/*
 * ═══════════════════════════════════════════════════════════════
 * ЗАДАЧИ ДЛЯ САМОСТОЯТЕЛЬНОЙ ПРАКТИКИ
 * ═══════════════════════════════════════════════════════════════
 */

#region Задачи

/*
 * БАЗОВЫЕ ЗАДАЧИ
 * ==============
 * 
 * 1. Car Builder
 *    Создайте класс Car с полями: марка, модель, год выпуска, цвет, тип двигателя, 
 *    мощность, тип коробки передач, количество дверей, тип кузова.
 *    Реализуйте CarBuilder с fluent-интерфейсом.
 *    Добавьте валидацию: год должен быть в диапазоне 1900-2025, мощность > 0.
 * 
 * 2. Resume Builder
 *    Создайте класс Resume для резюме с полями: имя, email, телефон, список опыта работы,
 *    список навыков, список образования, список языков, хобби.
 *    Реализуйте ResumeBuilder с методами для добавления каждого элемента.
 *    Добавьте метод для экспорта резюме в строку в читаемом формате.
 * 
 * 3. HTTP Request Builder
 *    Создайте класс HttpRequest с полями: метод (GET/POST/PUT/DELETE), URL, заголовки,
 *    тело запроса, параметры запроса, таймаут.
 *    Реализуйте HttpRequestBuilder с методами для настройки всех параметров.
 *    Добавьте метод ToString() для вывода готового запроса.
 * 
 * СРЕДНИЕ ЗАДАЧИ
 * ==============
 * 
 * 4. Report Builder
 *    Создайте систему для создания отчётов с разделами:
 *    - Заголовок отчёта
 *    - Список секций (каждая секция имеет название и содержимое)
 *    - Таблицы данных
 *    - Графики/диаграммы (представьте в виде текста)
 *    - Футер с датой создания
 *    Реализуйте ReportBuilder и ReportDirector с предустановленными типами отчётов
 *    (FinancialReport, SalesReport, PerformanceReport).
 * 
 * 5. Application Config Builder
 *    Создайте класс AppConfiguration для хранения настроек приложения:
 *    - Настройки подключения к БД (host, port, username, database)
 *    - Настройки логирования (уровень, путь к файлу, формат)
 *    - Настройки кэширования (включён ли, размер, время жизни)
 *    - Настройки безопасности (timeout сессии, max попыток входа)
 *    Реализуйте ConfigBuilder с валидацией:
 *    - port должен быть в диапазоне 1-65535
 *    - уровень логирования из списка (DEBUG, INFO, WARNING, ERROR)
 *    - timeout > 0
 * 
 * 6. Menu Builder
 *    Создайте систему для построения меню ресторана:
 *    - Класс MenuItem (название, описание, цена, категория, аллергены)
 *    - Класс Menu (название меню, список категорий, список блюд)
 *    - MenuBuilder для добавления блюд и категорий
 *    - MenuDirector с предустановленными меню (BreakfastMenu, LunchMenu, DinnerMenu)
 * 
 * СЛОЖНЫЕ ЗАДАЧИ
 * ==============
 * 
 * 7. Document Builder (вложенные Builder'ы)
 *    Создайте систему для создания документа Word/PDF:
 *    - Document содержит параграфы, таблицы, изображения
 *    - ParagraphBuilder для создания параграфа (текст, шрифт, размер, выравнивание)
 *    - TableBuilder для создания таблицы (строки, колонки, данные)
 *    - DocumentBuilder использует вложенные билдеры:
 *      doc.AddParagraph(p => p.WithText("...").WithBold().WithSize(14))
 *      doc.AddTable(t => t.WithRows(3).WithColumns(2).WithData(...))
 * 
 * 8. Query Builder для разных БД
 *    Расширьте SqlQueryBuilder для поддержки разных СУБД:
 *    - MySqlQueryBuilder
 *    - PostgreSqlQueryBuilder
 *    - SqlServerQueryBuilder
 *    Каждый имеет свои особенности синтаксиса (LIMIT vs TOP, '' vs "", и т.д.)
 *    Реализуйте QueryDirector для создания типовых запросов (поиск, пагинация, агрегация).
 * 
 * 9. Game Character Builder
 *    Создайте систему создания персонажа для RPG:
 *    - Character (имя, раса, класс, атрибуты: сила/ловкость/интеллект/выносливость)
 *    - Инвентарь (оружие, броня, аксессуары)
 *    - Навыки и способности
 *    - CharacterBuilder с валидацией:
 *      * Сумма атрибутов не должна превышать лимит
 *      * Класс определяет доступные навыки
 *      * Раса даёт бонусы к определённым атрибутам
 *    - CharacterDirector с пресетами классов (Warrior, Mage, Rogue, Archer)
 * 
 * 10. Test Data Builder (для Unit-тестов)
 *     Создайте систему Builder'ов для генерации тестовых данных:
 *     - UserTestDataBuilder для создания пользователей
 *     - OrderTestDataBuilder для создания заказов
 *     - ProductTestDataBuilder для создания продуктов
 *     Каждый Builder должен:
 *     - Иметь разумные значения по умолчанию
 *     - Позволять переопределять любое поле
 *     - Иметь методы для создания специфичных сценариев (WithInvalidEmail, WithExpiredCard и т.д.)
 *     Пример использования:
 *     var user = UserTestDataBuilder.Default().WithInvalidEmail().Build();
 */

#endregion

/*
 * ═══════════════════════════════════════════════════════════════
 * ИТОГОВЫЕ ЗАМЕЧАНИЯ
 * ═══════════════════════════════════════════════════════════════
 * 
 * Builder — один из самых полезных паттернов для повседневной разработки.
 * Он делает код более читаемым и понятным, особенно при работе со сложными объектами.
 * 
 * Используйте Builder, когда:
 * ✓ Объект имеет много параметров (>4-5)
 * ✓ Многие параметры необязательные
 * ✓ Нужна валидация перед созданием
 * ✓ Создание объекта — сложный процесс
 * ✓ Нужна читаемость кода
 * 
 * Не используйте Builder, когда:
 * ✗ Объект простой (2-3 параметра)
 * ✗ Все параметры обязательные
 * ✗ Объект не меняется после создания (используйте конструктор с параметрами)
 * 
 * Помните: Builder — это инструмент, а не догма. Используйте его там, где он приносит пользу!
 */

