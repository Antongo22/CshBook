using System;
using System.Collections.Generic;
using System.IO;

class ExceptionTasks
{
    // 1. Деление двух чисел с обработкой DivideByZeroException
    public static double DivideNumbers(int a, int b)
    {
        try
        {
            return a / b;
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Ошибка: Деление на ноль невозможно!");
            return double.NaN; // Возвращаем специальное значение
        }
    }

    // 2. Чтение файла с обработкой FileNotFoundException
    public static string ReadFileContent(string filePath)
    {
        try
        {
            return File.ReadAllText(filePath);
        }
        catch (FileNotFoundException)
        {
            return "Ошибка: Файл не найден.";
        }
    }

    // 3. Проверка числа на положительность с выбросом ArgumentException
    public static void ValidatePositiveNumber(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException("Число должно быть положительным!");
        }
    }

    // 4. Конвертация строки в число с обработкой FormatException
    public static int ConvertToInt(string input)
    {
        try
        {
            return int.Parse(input);
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введено некорректное число.");
            return 0; // Возвращаем значение по умолчанию
        }
    }

    // 5. Доступ к элементу массива с обработкой IndexOutOfRangeException
    public static int GetArrayElement(int[] array, int index)
    {
        try
        {
            return array[index];
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Ошибка: Индекс выходит за границы массива.");
            return -1;
        }
    }

    // 6. Работа с Dictionary с обработкой KeyNotFoundException
    public static string GetDictionaryValue(Dictionary<string, string> dict, string key)
    {
        try
        {
            return dict[key];
        }
        catch (KeyNotFoundException)
        {
            return "Ошибка: Ключ не найден.";
        }
    }

    // 7. Чтение файла с использованием finally для закрытия потока
    public static void ReadFileWithFinally(string filePath)
    {
        StreamReader reader = null;
        try
        {
            reader = new StreamReader(filePath);
            Console.WriteLine(reader.ReadToEnd());
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Ошибка: Файл не найден.");
        }
        finally
        {
            reader?.Close();
            Console.WriteLine("Файл закрыт.");
        }
    }


    // 8. Калькулятор с защитой try-catch
    public static double SafeCalculator(double a, double b, string operation)
    {
        try
        {
            return operation switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => b != 0 ? a / b : throw new DivideByZeroException(),
                _ => throw new ArgumentException("Некорректная операция")
            };
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Ошибка: Деление на ноль!");
            return double.NaN;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return double.NaN;
        }
    }

    public static void Main()
    {
        // Проверка деления
        Console.WriteLine("Деление: " + DivideNumbers(10, 0));

        // Чтение файла
        Console.WriteLine("Чтение файла: " + ReadFileContent("nonexistent.txt"));

        // Проверка числа
        try
        {
            ValidatePositiveNumber(-5);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Конвертация строки
        Console.WriteLine("Конвертация: " + ConvertToInt("abc"));

        // Доступ к массиву
        int[] numbers = { 1, 2, 3 };
        Console.WriteLine("Элемент массива: " + GetArrayElement(numbers, 5));

        // Работа с Dictionary
        var dict = new Dictionary<string, string> { { "ключ", "значение" } };
        Console.WriteLine("Dictionary: " + GetDictionaryValue(dict, "неизвестный"));

        // Чтение файла с finally
        ReadFileWithFinally("nonexistent.txt");


        // Калькулятор
        Console.WriteLine("Калькулятор: " + SafeCalculator(10, 0, "/"));
    }
}
