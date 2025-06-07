# 🚀 `launchSettings.json` в ASP.NET Core

## 📌 Что это?

`launchSettings.json` — это конфигурационный файл, который используется **только в среде разработки** (Visual Studio / Rider / `dotnet run`). Он задаёт, **как запускать приложение**, в каких условиях, с какими аргументами, переменными среды и адресами.

> 📁 Располагается в:
> `Properties/launchSettings.json`

---

## 🧩 Структура файла

Пример файла:

```json
{
  "profiles": {
    "MyApp": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "https://localhost:5001;http://localhost:5000",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

---

## 🧠 Основные поля

| Поле                   | Назначение                                                     |
| ---------------------- | -------------------------------------------------------------- |
| `profiles`             | Список конфигураций запуска (например: Web, Docker, IIS)       |
| `commandName`          | Что запускать: `Project`, `Executable`, `IISExpress`, `Docker` |
| `applicationUrl`       | URL-адреса, на которых запускается приложение                  |
| `launchBrowser`        | Автоматически ли открывать браузер                             |
| `environmentVariables` | Список переменных окружения при запуске                        |
| `dotnetRunMessages`    | Показывать ли лог о запуске в консоли                          |

---

## 📂 Примеры профилей

### ▶️ ASP.NET Core Web API

```json
"MyApp": {
  "commandName": "Project",
  "applicationUrl": "https://localhost:5001;http://localhost:5000",
  "environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development"
  }
}
```

### 🐳 Docker

```json
"Docker": {
  "commandName": "Docker",
  "launchBrowser": true,
  "environmentVariables": {
    "ASPNETCORE_ENVIRONMENT": "Development"
  }
}
```

### 🧪 С внешним исполняемым файлом

```json
"MyTool": {
  "commandName": "Executable",
  "executablePath": "C:\\tools\\mytool.exe",
  "commandLineArgs": "--mode debug",
  "workingDirectory": "C:\\tools"
}
```

---

## 🧪 Как запустить с нужным профилем?

* **Visual Studio / Rider**: выбираешь нужный профиль в выпадающем списке
* **CLI**: запускай с параметром `--launch-profile`

```bash
dotnet run --launch-profile MyApp
```

---

## 🔄 Где взять переменные среды?

Ты можешь использовать их прямо в коде:

```csharp
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
```

Или через встроенные возможности DI:

```csharp
public MyController(IWebHostEnvironment env)
{
    var isDev = env.IsDevelopment();
}
```

---

## 🔥 Полезные фичи

* Позволяет **задать разные конфигурации** (например: Dev, QA, Production)
* Удобно **отлаживать с разными URL, портами и переменными**
* Используется **только в dev-режиме**, в проде обычно применяют `appsettings.Production.json`, переменные окружения и т.д.

---

## 📚 Заключение

`launchSettings.json` — это:

* 🔧 Конфигурация запуска проекта в dev-среде
* 🌍 Возможность указать URL, ENV, аргументы
* 💻 Удобство для отладки и тестирования


