# 📘 Работа с SQL в C# без Entity Framework

---

## 🔌 Подключение к базе данных

```csharp
using System.Data.SqlClient;

string connectionString = "Server=localhost;Database=MyAppDb;Trusted_Connection=True;";
using var connection = new SqlConnection(connectionString);
connection.Open();

Console.WriteLine("Успешное подключение к базе данных");
```

Для `Microsoft.Data.SqlClient`, подключение аналогично. 

---

## 📄 Выполнение SQL-запросов (INSERT, UPDATE, DELETE)

```csharp
string sql = "INSERT INTO Users (Name, Age) VALUES (@name, @age)";
using var command = new SqlCommand(sql, connection);

command.Parameters.AddWithValue("@name", "Anton");
command.Parameters.AddWithValue("@age", 18);

int rowsAffected = command.ExecuteNonQuery();
Console.WriteLine($"Добавлено строк: {rowsAffected}");
```

✅ **`ExecuteNonQuery()`** — используется для INSERT/UPDATE/DELETE.

---

## 🔍 Чтение данных из базы (SELECT)

```csharp
string sql = "SELECT Id, Name, Age FROM Users";
using var command = new SqlCommand(sql, connection);
using SqlDataReader reader = command.ExecuteReader();

while (reader.Read())
{
    int id = reader.GetInt32(0);
    string name = reader.GetString(1);
    int age = reader.GetInt32(2);

    Console.WriteLine($"{id}: {name}, {age}");
}
```

📌 Можно обращаться к столбцам как по индексу, так и по имени: `reader["Name"]`.

---

## 🔒 Параметры и защита от SQL-инъекций

**Никогда** не вставляй данные напрямую в SQL-строку!

**❌ Опасно:**

```csharp
string sql = $"SELECT * FROM Users WHERE Name = '{userInput}'";
```

**✅ Безопасно:**

```csharp
string sql = "SELECT * FROM Users WHERE Name = @name";
command.Parameters.AddWithValue("@name", userInput);
```

---

## 🔁 Выполнение скалярных запросов (SELECT COUNT, MAX и т.п.)

```csharp
string sql = "SELECT COUNT(*) FROM Users";
using var command = new SqlCommand(sql, connection);
int count = (int)command.ExecuteScalar();

Console.WriteLine($"Пользователей: {count}");
```

📌 `ExecuteScalar()` возвращает **первое значение первой строки**.

---

## 💾 Транзакции

```csharp
using var transaction = connection.BeginTransaction();

try
{
    var cmd1 = new SqlCommand("UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1", connection, transaction);
    var cmd2 = new SqlCommand("UPDATE Accounts SET Balance = Balance + 100 WHERE Id = 2", connection, transaction);

    cmd1.ExecuteNonQuery();
    cmd2.ExecuteNonQuery();

    transaction.Commit();
    Console.WriteLine("Транзакция успешно завершена");
}
catch
{
    transaction.Rollback();
    Console.WriteLine("Ошибка, транзакция откатилась");
}
```

---

## 🛠 Создание таблицы

```csharp
string sql = """
CREATE TABLE IF NOT EXISTS Users (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Age INT
)
""";

using var command = new SqlCommand(sql, connection);
command.ExecuteNonQuery();
```

---

## 📂 Работа с SQLite (через Microsoft.Data.Sqlite)

1. Установи пакет:

```bash
dotnet add package Microsoft.Data.Sqlite
```

2. Пример работы:

```csharp
using Microsoft.Data.Sqlite;

string connectionString = "Data Source=app.db";
using var connection = new SqliteConnection(connectionString);
connection.Open();

var command = connection.CreateCommand();
command.CommandText = "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Name TEXT)";
command.ExecuteNonQuery();
```

3. Вставка и чтение:

```csharp
var insert = connection.CreateCommand();
insert.CommandText = "INSERT INTO Users (Name) VALUES (@name)";
insert.Parameters.AddWithValue("@name", "Anton");
insert.ExecuteNonQuery();

var select = connection.CreateCommand();
select.CommandText = "SELECT Id, Name FROM Users";

using var reader = select.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader.GetInt32(0)}: {reader.GetString(1)}");
}
```

---

## 📌 Полезные советы

| Ситуация                      | Метод                          |
| ----------------------------- | ------------------------------ |
| INSERT/UPDATE/DELETE          | `ExecuteNonQuery()`            |
| SELECT COUNT, MIN, MAX и т.п. | `ExecuteScalar()`              |
| SELECT c результатом          | `ExecuteReader()`              |
| Параметры в SQL               | `AddWithValue`, `Add()`        |
| Безопасность                  | Никогда не конкатенируй строки |
| Много операций вместе         | Используй транзакции           |

---

## 🔚 Заключение

Работа с SQL без EF:

* Даёт полный контроль над SQL-запросами.
* Позволяет писать производительные и прозрачные запросы.
* Подходит для небольших проектов, CLI-инструментов и быстрого прототипирования.

Но:

* Не предоставляет абстракций — всю работу с запросами, параметрами и конвертацией данных нужно делать вручную.


