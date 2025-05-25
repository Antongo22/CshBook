using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Дополнительно
{

    /*
    | Ключевое слово | Назначение                                                                                          |
    | -------------- | --------------------------------------------------------------------------------------------------- |
    | `typeof(T)`    | Возвращает объект `System.Type` для типа `T`. Используется в рефлексии и при работе с типами.       |
    | `nameof(expr)` | Возвращает строковое имя переменной, свойства, метода и т. д. Безопасно при рефакторинге.           |
    | `sizeof(T)`    | Возвращает размер типа `T` в байтах. Только для **неуправляемых типов**. Для других нужен `unsafe`. |
    | `default(T)`   | Возвращает значение по умолчанию для типа `T` (0, null, false и т. д.).                             |
    | `checked`      | Включает проверку переполнения целочисленных операций.                                              |
    | `unchecked`    | Отключает такую проверку.                                                                           |

     */


    class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }



    internal class СompileTime_Helpers
    {
        public static void Main_()
        {
            // typeof — получить тип объекта (Type) во время компиляции
            Type typeInfo = typeof(MyClass);
            Console.WriteLine($"Тип: {typeInfo.Name}"); // Выведет: Тип: MyClass

            // nameof — безопасно получить имя переменной, свойства, класса как строку
            string propertyName = nameof(MyClass.Name);
            Console.WriteLine($"Имя свойства: {propertyName}"); // Выведет: Имя свойства: Name

            // sizeof — получить размер типа в байтах (работает только с примитивами, struct)
            int intSize = sizeof(int);
            Console.WriteLine($"Размер int: {intSize} байт"); // Выведет: Размер int: 4 байт

            // default — получить значение по умолчанию
            int defaultInt = default; // 0
            MyClass defaultObj = default; // null
            Console.WriteLine($"default int: {defaultInt}, default объект: {defaultObj}");

            // checked — генерирует исключение при переполнении
            try
            {
                int big = int.MaxValue;
                int overflowed = checked(big + 1); // Ошибка: ArithmeticException
            }
            catch (OverflowException)
            {
                Console.WriteLine("Переполнение обнаружено с checked!");
            }

            // unchecked — отключает проверку переполнения (по умолчанию отключено)
            int unsafeOverflow = unchecked(int.MaxValue + 1);
            Console.WriteLine($"Без проверки: {unsafeOverflow}"); // Выведет: -2147483648
        }
    }
}
