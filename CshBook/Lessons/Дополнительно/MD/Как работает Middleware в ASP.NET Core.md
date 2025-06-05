# Как работает Middleware в ASP.NET Core

## 📌 Что такое Middleware?

**Middleware** — это компоненты, которые обрабатывают HTTP-запросы и ответы в ASP.NET Core. Каждый middleware:

* получает входящий HTTP-запрос;
* может обработать его, изменить, передать дальше или прервать цепочку;
* может выполнить действия *до* и *после* следующего middleware в цепи.

---

## 🔁 Пайплайн (Pipeline)

ASP.NET Core строит цепочку middleware-компонентов, которая называется **request processing pipeline**.

Пример:

```plaintext
Запрос --> Middleware A --> Middleware B --> Middleware C --> Ответ
```

---

## 🔧 Пример middleware

```csharp
public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Запрос: {context.Request.Path}");
        await _next(context); // передаём управление следующему middleware
        Console.WriteLine($"Ответ: {context.Response.StatusCode}");
    }
}
```

Чтобы подключить этот middleware:

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseMiddleware<LoggingMiddleware>();
}
```

---

## 🧱 Построение пайплайна

Middleware добавляются через методы `Use*` в методе `Configure`:

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseRouting();           // определяет маршрутизацию
    app.UseAuthentication();    // обрабатывает аутентификацию
    app.UseAuthorization();     // обрабатывает авторизацию
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers(); // связывает маршруты с контроллерами
    });
}
```

---

## 🧠 Особенности

| Особенность            | Описание                                                                    |
| ---------------------- | --------------------------------------------------------------------------- |
| Последовательность     | Middleware вызываются в том порядке, в котором они добавлены в `Configure`. |
| Краткая остановка цепи | Middleware может не вызывать `_next()`, чтобы прервать выполнение цепочки.  |
| Асинхронность          | Все middleware работают асинхронно через `Task`.                            |

---

## ✅ Полезные встроенные middleware

* `UseStaticFiles` — раздача статических файлов
* `UseRouting` — маршрутизация
* `UseCors` — настройка CORS
* `UseAuthentication`, `UseAuthorization` — безопасность
* `UseExceptionHandler`, `UseStatusCodePages` — обработка ошибок

---

## 🧪 Минималка: Custom Middleware через `app.Use`

Можно создать inline middleware:

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("До");
    await next();
    Console.WriteLine("После");
});
```

---

## 📚 Вывод

Middleware — ключевой механизм ASP.NET Core, который даёт гибкость в обработке запросов. Ты сам решаешь, какие компоненты участвуют в пайплайне и в каком порядке.

