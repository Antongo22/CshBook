# 📦 Шпаргалка по dotnet CLI

`.NET CLI (Command Line Interface)` — мощный инструмент для работы с .NET-проектами через терминал.

---

## 📁 Работа с проектами и решениями

| Команда                                 | Описание                               |
|----------------------------------------|----------------------------------------|
| `dotnet new console`                   | Создать новый консольный проект        |
| `dotnet new webapi`                    | Создать новый проект Web API           |
| `dotnet new sln`                       | Создать пустое решение `.sln`          |
| `dotnet sln add <путь>`                | Добавить проект в решение              |
| `dotnet new list`                      | Показать список шаблонов               |

---

## ⚙️ Сборка и запуск

| Команда                   | Описание                                     |
|--------------------------|----------------------------------------------|
| `dotnet build`           | Собрать проект                               |
| `dotnet run`             | Запустить проект                             |
| `dotnet clean`           | Очистить папку `bin/` и `obj/`               |
| `dotnet publish`         | Скомпилировать готовое приложение для деплоя |
| `dotnet run --project <путь>` | Запустить указанный проект              |

---

## 🔍 Работа с зависимостями (NuGet)

| Команда                                         | Описание                              |
|------------------------------------------------|---------------------------------------|
| `dotnet add package <имя>`                     | Установить NuGet-пакет                |
| `dotnet remove package <имя>`                  | Удалить NuGet-пакет                   |
| `dotnet list package`                          | Посмотреть установленные пакеты       |
| `dotnet restore`                               | Восстановить пакеты (`*.csproj`)      |

---

## 🧪 Тестирование

| Команда                | Описание                           |
|------------------------|------------------------------------|
| `dotnet new xunit`     | Создать проект с тестами (XUnit)   |
| `dotnet test`          | Запустить все тесты                |
| `dotnet test --filter` | Фильтрация по категории/имени и т.д.|

---

## 🔁 Работа с миграциями (EF Core)

> Убедись, что установлен пакет `Microsoft.EntityFrameworkCore.Tools`.

| Команда                                                       | Описание                                |
|--------------------------------------------------------------|-----------------------------------------|
| `dotnet ef migrations add <Имя>`                            | Создать миграцию                        |
| `dotnet ef database update`                                 | Применить миграции к базе данных        |
| `dotnet ef migrations remove`                               | Удалить последнюю миграцию              |
| `dotnet ef database drop`                                   | Удалить базу данных                     |
| `dotnet ef dbcontext info`                                  | Информация о текущем DbContext          |
| `dotnet ef migrations list`                                 | Показать список миграций                |

---

## 🧰 Инструменты и информация

| Команда                     | Описание                                 |
|----------------------------|------------------------------------------|
| `dotnet --info`            | Информация о текущем SDK и среде         |
| `dotnet --version`         | Версия установленного .NET SDK           |
| `dotnet workload list`     | Список установленных workload'ов         |
| `dotnet workload install <имя>` | Установка workload (например, MAUI) |
| `dotnet help`              | Общая справка по командам                |

---

## 🛠 Полезные параметры

- `--configuration Release` — сборка в режиме Release
- `--framework net8.0` — указание целевой платформы
- `--output <путь>` — вывод в указанную папку
- `--verbosity detailed` — подробный вывод

---

## 🔗 Примеры

```bash
dotnet new console -n MyApp
cd MyApp
dotnet build
dotnet run
```

```bash
dotnet new sln -n MySolution
dotnet sln add ./MyApp/MyApp.csproj
```

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 🧠 Рекомендации

* Используй `dotnet watch run` для автоперезапуска при изменении кода
* Сохраняй проекты в решении (`.sln`) для удобной навигации в IDE
* Никогда не редактируй `*.csproj` вручную без необходимости — используй `dotnet add/remove`

---

## 📚 Полезные ссылки

* [Команды dotnet CLI](https://learn.microsoft.com/dotnet/core/tools/)
* [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet)
