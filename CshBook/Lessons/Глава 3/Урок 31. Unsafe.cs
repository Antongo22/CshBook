using System;
using System.Runtime.InteropServices;

namespace CshBook.Lessons;

/*
============================================================================================================
| ЦЕЛЬ УРОКА                                                                                                 |
|-----------------------------------------------------------------------------------------------------------|
| Этот урок показывает, как управлять памятью напрямую в C# с помощью ключевого слова `unsafe` и указателей. |
| Такие подходы нужны крайне редко — только при интеграции с C, оптимизации, работе с оборудованием и пр.   |
| Однако, понимание этих техник полезно для системного программирования и работы с высокопроизводительным кодом.|
============================================================================================================

| Элемент          | Описание                                                                                                                       |
|------------------|--------------------------------------------------------------------------------------------------------------------------------|
| `unsafe`         | Позволяет использовать указатели, работать напрямую с памятью. Используется, когда важна производительность или нужен низкоуровневый доступ. |
| `*`              | Означает "указатель на тип". Например, `int*` — указатель на `int`. Разыменование через `*ptr` (получение значения по адресу).       |
| `&`              | Получение адреса переменной. Например, `int* ptr = &x;` сохраняет адрес переменной `x` в указателе `ptr`.                         |
| `->`             | Доступ к полям структуры по указателю. Эквивалент `(*ptr).X`, но короче. Используется с указателями на структуры.                 |
| `ref`            | Передаёт ссылку на переменную, но безопасным способом. Управляется CLR, не требует `unsafe`.                                     |
| `stackalloc`     | Выделяет память на стеке — быстро и без участия сборщика мусора. Используется вместе с `Span<T>` для быстрого буфера.              |
| `fixed`          | Закрепляет управляемый объект в памяти, чтобы указатель не стал недействительным во время работы сборщика мусора (GC).             |
| `Span<T>`        | Лёгкая обёртка над массивом или указателем. Работает в управляемом коде, безопасна, быстра. Не хранится в полях и не async.         |
| `Memory<T>`      | Обёртка, похожая на `Span<T>`, но может использоваться в асинхронном коде, храниться в переменных и полях.                          |
| `extern`         | Используется для объявления внешнего метода, реализованного вне C# (например, в C/C++ DLL). Обязателен с атрибутом `DllImport`.     |
| `extern alias`   | Позволяет использовать две сборки с одинаковыми именами типов. Редко применяется, но полезно при конфликте имён в Interop.         |

`Interop` (Interoperation) — взаимодействие с внешним (например, нативным) кодом, чаще всего на C/C++, через указатели и `DllImport`.
    Также используется `extern alias`, если вы подключаете разные версии одной и той же библиотеки, и хотите избежать конфликтов.
    
    Пример использования extern alias:
    // В .csproj
    // <Reference Include="LibraryA" Aliases="LibA" />
    // <Reference Include="LibraryB" Aliases="LibB" />

    // В коде
    extern alias LibA;
    extern alias LibB;

    using A = LibA::SomeNamespace.MyType;
    using B = LibB::SomeNamespace.MyType;

    A a = new A();
    B b = new B();

Пример интеграции с C/C++:
---------------------------------------------
// C++ код (mycpp.cpp):
extern "C" __declspec(dllexport) int Add(int a, int b) {
    return a + b;
}

// Что нужно для подключения в C#:
// 1. Скомпилируй этот C++ код в DLL (например, mycpp.dll)
// 2. В C# добавь [DllImport("mycpp.dll")] и метод

// C# код:
[DllImport("mycpp.dll")]
public static extern int Add(int a, int b);

Console.WriteLine(Add(2, 3)); // Выведет 5
---------------------------------------------
*/


/* Указатели в C#

Указатели в C# — это специальные переменные, которые содержат адреса других переменных в памяти. Они позволяют работать с памятью напрямую и использовать арифметику указателей.

Однако указатели работают только в **небезопасном коде (unsafe)**, который требует специальных разрешений. Такие возможности чаще используются в системном программировании, драйверах или при оптимизации производительности.

---------------------
Основные особенности:
---------------------

- Указатели могут использоваться только в контексте `unsafe`.
- Необходима активация небезопасного кода: `unsafe` и флаг компиляции `/unsafe`.
- Доступны операции: получение адреса (`&`), разыменование (`*`), арифметика указателей (`+`, `-`).
- Можно использовать с простыми типами: `int*`, `char*`, `float*`, и структурами, не содержащими ссылочных типов.

---------------------
Зачем нужны указатели:
---------------------

- Повышение производительности при работе с большим объёмом данных.
- Доступ к памяти на низком уровне.
- Взаимодействие с нативным (неуправляемым) кодом (например, C/C++).
- Написание собственных аллокаторов памяти.
- Управление структурами в `stackalloc`.


*/


public class UnsafeExamples
{
    /// <summary>
    /// Простой пример работы с указателем и разыменованием
    /// </summary>
    public unsafe void PointerBasic()
    {
        int number = 42;
        int* ptr = &number;
        Console.WriteLine($"*ptr = {*ptr}");
        *ptr = 100;
        Console.WriteLine($"number = {number}");
    }

    /// <summary>
    /// Пример получения указателя на массив с помощью fixed
    /// </summary>
    public unsafe void PointerArray()
    {
        int[] array = { 10, 20, 30 };
        fixed (int* ptr = array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(ptr[i]);
            }
        }
    }

    /// <summary>
    /// Пример выделения памяти на стеке с помощью stackalloc
    /// </summary>
    public unsafe void StackAllocDemo()
    {
        int* buffer = stackalloc int[3];
        buffer[0] = 1;
        buffer[1] = 2;
        buffer[2] = 3;

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(buffer[i]);
        }
    }

    /// <summary>
    /// Пример использования Span для безопасной работы с памятью на стеке
    /// </summary>
    public void SpanExample()
    {
        Span<int> numbers = stackalloc int[3] { 1, 2, 3 };
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.WriteLine(numbers[i]);
        }
    }

    /// <summary>
    /// Пример использования Memory для работы с буферами в async-коде
    /// </summary>
    public void MemoryExample()
    {
        Memory<int> memory = new int[] { 5, 10, 15 };
        Span<int> span = memory.Span;
        for (int i = 0; i < span.Length; i++)
        {
            Console.WriteLine(span[i]);
        }
    }

    /// <summary>
    /// Сравнение указателя и ref-ссылки
    /// </summary>
    public unsafe void PointerVsRef()
    {
        int a = 10;
        int b = 20;

        ChangeWithPointer(&a);
        Console.WriteLine($"После указателя: a = {a}");

        ChangeWithRef(ref b);
        Console.WriteLine($"После ref: b = {b}");
    }

    /// <summary>
    /// Изменение значения через указатель
    /// </summary>
    private unsafe void ChangeWithPointer(int* value)
    {
        *value += 100;
    }

    /// <summary>
    /// Изменение значения по ссылке с помощью ref
    /// </summary>
    private void ChangeWithRef(ref int value)
    {
        value += 200;
    }

    /// <summary>
    /// Возвращение указателя из метода (опасно!)
    /// </summary>
    public unsafe void PointerFromMethod()
    {
        int* ptr = GetPointerToInt();
        Console.WriteLine($"Полученный указатель указывает на: {*ptr}");
    }

    /// <summary>
    /// Опасный пример возврата указателя на локальную переменную
    /// </summary>
    private unsafe int* GetPointerToInt()
    {
        int value = 77;
        int* ptr = &value;
        return ptr; // значение будет уничтожено после выхода из метода
    }

    /// <summary>
    /// Работа с указателями на структуру и доступ через ->
    /// </summary>
    public unsafe void PointerToStruct()
    {
        Point* p = stackalloc Point[1];
        p->X = 5;
        p->Y = 10;

        Console.WriteLine($"Point: X = {p->X}, Y = {p->Y}");
    }

    private struct Point
    {
        public int X;
        public int Y;
    }

    /// <summary>
    /// Арифметика указателей (прибавление/вычитание)
    /// </summary>
    public unsafe void PointerArithmetic()
    {       
        int[] nums = { 100, 200, 300, 400 };
        fixed (int* pNums = nums)
        {
            int* ptr1 = pNums;
            int* ptr2 = ptr1 + 2;
            Console.WriteLine($"\nAрифметика указателей:");
            Console.WriteLine($"ptr1 -> {*(ptr1)}");
            Console.WriteLine($"ptr2 -> {*(ptr2)}"); // 300
            Console.WriteLine($"Разница между ptr2 и ptr1: {ptr2 - ptr1}"); // 2
        }
    }
}

internal class UnsafeRunner
{
    public static void Main_()
    {
        var demo = new UnsafeExamples();

        Console.WriteLine("== PointerBasic ==");
        unsafe { demo.PointerBasic(); }

        Console.WriteLine("\n== PointerArray ==");
        unsafe { demo.PointerArray(); }

        Console.WriteLine("\n== StackAllocDemo ==");
        unsafe { demo.StackAllocDemo(); }

        Console.WriteLine("\n== SpanExample ==");
        demo.SpanExample();

        Console.WriteLine("\n== MemoryExample ==");
        demo.MemoryExample();

        Console.WriteLine("\n== Pointer vs Ref ==");
        unsafe { demo.PointerVsRef(); }

        Console.WriteLine("\n== PointerFromMethod ==");
        unsafe { demo.PointerFromMethod(); }

        Console.WriteLine("\n== PointerToStruct ==");
        unsafe { demo.PointerToStruct(); }


        Console.WriteLine("\n== PointerArithmetic ==");
        unsafe { demo.PointerArithmetic(); }

    }
}



/*
     * Задания на практику:
     * 
     * 1. Получите адрес переменной типа int и выведите его значение.
     * 2. Измените значение переменной через указатель.
     * 3. Создайте массив из 5 чисел и выведите их через указатель и арифметику указателей.
     * 4. Используйте stackalloc для создания массива из 4 элементов и инициализируйте их.
     * 5. Создайте структуру `Rectangle { int Width, Height }`, измените её поля через указатель.
     * 6. Реализуйте метод, принимающий указатель на int и увеличивающий значение на 1.
     * 7. Закрепите строку с помощью `fixed` и выведите каждый символ по указателю.
     * 8. Создайте 2 переменные int и поменяйте их значения местами через указатели.
     * 9. Напишите unsafe-метод, возвращающий максимальное число из массива.
     * 10. Используя указатель и stackalloc, реализуйте ручной подсчёт суммы массива.
     */