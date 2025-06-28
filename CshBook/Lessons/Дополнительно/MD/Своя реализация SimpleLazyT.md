# 🛠 Своя реализация `SimpleLazy<T>`

```csharp
public class SimpleLazy<T>
{
    private T _value;
    private bool _isValueCreated = false;
    private readonly Func<T> _valueFactory;
    private readonly object _lock = new object();

    public SimpleLazy(Func<T> valueFactory)
    {
        _valueFactory = valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));
    }

    public T Value
    {
        get
        {
            if (_isValueCreated)
                return _value;

            lock (_lock)
            {
                if (!_isValueCreated)
                {
                    _value = _valueFactory();
                    _isValueCreated = true;
                }
            }

            return _value;
        }
    }

    public bool IsValueCreated => _isValueCreated;
}
```

---

## 🔍 Объяснение

| Элемент           | Назначение                                                                                |
| ----------------- | ----------------------------------------------------------------------------------------- |
| `_valueFactory`   | Делегат `Func<T>`, передаваемый в конструктор. Содержит логику инициализации.             |
| `_value`          | Хранит результат выполнения делегата.                                                     |
| `_isValueCreated` | Флаг, чтобы не вызывать фабрику повторно.                                                 |
| `_lock`           | Объект блокировки для потокобезопасности.                                                 |
| `Value`           | Свойство, через которое производится доступ к значению. Инициализирует при первом вызове. |

---

## 📌 Использование

```csharp
var lazy = new SimpleLazy<string>(() =>
{
    Console.WriteLine("Инициализация...");
    return "Привет, мир!";
});

Console.WriteLine("До доступа к значению");
Console.WriteLine(lazy.IsValueCreated); // false

var value = lazy.Value; // Тут произойдёт инициализация
Console.WriteLine(value); // Привет, мир!

Console.WriteLine(lazy.IsValueCreated); // true
```

**Вывод:**

```
До доступа к значению
False
Инициализация...
Привет, мир!
True
```

---

## 🧵 Вариант без блокировок (непотокобезопасный)

```csharp
public class FastLazy<T>
{
    private T _value;
    private bool _created = false;
    private readonly Func<T> _factory;

    public FastLazy(Func<T> factory)
    {
        _factory = factory;
    }

    public T Value => _created ? _value : (_value = Create());

    private T Create()
    {
        _value = _factory();
        _created = true;
        return _value;
    }

    public bool IsValueCreated => _created;
}
```

> ⚠️ Такой класс использовать **только в однопоточном контексте**!

---

## 💡 Что делает настоящая `Lazy<T>` из .NET?

* Поддерживает **режимы потокобезопасности** через `LazyThreadSafetyMode`.
* Может хранить исключения, возникшие в делегате, и повторно выбрасывать их при каждом `Value`.
* Гарантирует, что значение создаётся **ровно один раз**, даже в многопоточном окружении.

