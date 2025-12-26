using CshBook.Answers.Answ0;
using CshBook.Lessons;
using CshBook.Lessons.Глава_3;
using CshBook.Lessons.Глава_4;
using CshBook.Lessons.Глава_5;
using CshBook.Lessons.Дополнительно;
using CshBook.Ответы. Глава_0;

SixthLesson.Main_();

public enum TemperatureUnit
{
    Kelvin,
    Celsius,
    Fahrenheit
}

public class Temperature : IComparable<Temperature>
{
    // Внутреннее значение хранится в Кельвинах для единообразия
    private readonly double _kelvin;

    // Конструктор по умолчанию (0 K)
    public Temperature() : this(0.0, TemperatureUnit.Kelvin) { }

    // Основной конструктор
    public Temperature(double value, TemperatureUnit unit)
    {
        _kelvin = unit switch
        {
            TemperatureUnit.Kelvin => value,
            TemperatureUnit.Celsius => value + 273.15,
            TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9 + 273.15,
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };

        if (_kelvin < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "Температура не может быть ниже абсолютного нуля (0 K).");
    }

    // Свойства для чтения в разных шкалах
    public double Kelvin => _kelvin;
    public double Celsius => _kelvin - 273.15;
    public double Fahrenheit => (_kelvin - 273.15) * 9 / 5 + 32;

    // Удобные статические фабрики
    public static Temperature FromKelvin(double k) => new Temperature(k, TemperatureUnit.Kelvin);
    public static Temperature FromCelsius(double c) => new Temperature(c, TemperatureUnit.Celsius);
    public static Temperature FromFahrenheit(double f) => new Temperature(f, TemperatureUnit.Fahrenheit);

    // Реализация IComparable<Temperature>
    public int CompareTo(Temperature? other)
    {
        if (other is null) return 1;
        return _kelvin.CompareTo(other._kelvin);
    }

    // Операторы сравнения
    public static bool operator <(Temperature left, Temperature right) => left._kelvin < right._kelvin;
    public static bool operator >(Temperature left, Temperature right) => left._kelvin > right._kelvin;
    public static bool operator <=(Temperature left, Temperature right) => left._kelvin <= right._kelvin;
    public static bool operator >=(Temperature left, Temperature right) => left._kelvin >= right._kelvin;

    // Операторы равенства (для полноты)
    public static bool operator ==(Temperature? left, Temperature? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return Math.Abs(left._kelvin - right._kelvin) < 1e-10;
    }

    public static bool operator !=(Temperature? left, Temperature? right) => !(left == right);

    // Обязательные переопределения object
    public override bool Equals(object? obj) => obj is Temperature other && this == other;
    public override int GetHashCode() => _kelvin.GetHashCode();

    public override string ToString() => $"{Kelvin:F2} K ({Celsius:F2} °C, {Fahrenheit:F2} °F)";
}

// Дополнительно: неявные/явные преобразования (по желанию)
// Ниже — примеры преобразований температуры к double (в указанной шкале) и обратно.

// Но лучше избегать неявных преобразований double ↔ Temperature из-за неоднозначности шкал.
// Вместо этого используем явные фабрики и свойства.

// Пример использования в Main (для теста):
/*
class Program
{
    static void Main()
    {
        var t1 = Temperature.FromCelsius(0);      // 273.15 K
        var t2 = Temperature.FromFahrenheit(32); // тоже 273.15 K
        var t3 = Temperature.FromKelvin(300);    // ~26.85 °C

        Console.WriteLine(t1); // 273.15 K (0.00 °C, 32.00 °F)
        Console.WriteLine(t2); // 273.15 K (0.00 °C, 32.00 °F)
        Console.WriteLine(t3); // 300.00 K (26.85 °C, 80.33 °F)

        Console.WriteLine(t1 == t2); // True
        Console.WriteLine(t3 > t1);  // True
        Console.WriteLine(t1 <= t3); // True
    }
}
*/