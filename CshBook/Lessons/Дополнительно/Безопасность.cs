using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Дополнительно
{
    /*
    | Элемент    | Описание                                                                                                                         |
    | ---------- | -------------------------------------------------------------------------------------------------------------------------------- |
    | `sealed`   | Запрещает наследование от класса.                                                                                                |
    | `const`    | Константа времени компиляции. Всегда `static` по сути. Нельзя изменить.                                                          |
    | `readonly` | Значение можно задать только в конструкторе или при инициализации. Отлично подходит для значений времени запуска, токенов и т.д. |
    | `static`   | Поле принадлежит всему классу, а не конкретному объекту.                                                                         |
    | `init`     | Автосвойство доступно только для установки в инициализации или в конструкторе. Позволяет создавать **иммутабельные объекты**.    |
    | `private`  | Доступ только внутри класса.                                                                                                     |
    | `public`   | Доступен из любого места.                                                                                                        |

     */

    public sealed class AppConfig // sealed — запрещает наследование
    {
        // Константа (значение должно быть известно на момент компиляции)
        public const string Version = "1.0.0";

        // Readonly поле — значение можно задать в конструкторе или при объявлении
        public readonly DateTime StartTime;

        // Статическое поле — общее для всех экземпляров
        public static int InstanceCount = 0;

        // Автосвойство только для чтения с инициализацией
        public string AppName { get; init; }

        // Приватное поле
        private readonly string _secretKey;

        // Конструктор
        public AppConfig(string appName, string secretKey)
        {
            AppName = appName;           // init — можно задать только здесь (или через инициализатор)
            _secretKey = secretKey;      // readonly — можно присвоить
            StartTime = DateTime.Now;    // readonly — можно присвоить
            InstanceCount++;             // static — общее для всех объектов
        }

        // Метод только для чтения данных
        public void PrintInfo()
        {
            Console.WriteLine($"App: {AppName}, Started: {StartTime}, Version: {Version}");
        }

        // Приватный метод (вне класса не видно)
        private void LogSecret()
        {
            Console.WriteLine($"Secret: {_secretKey}");
        }
    }


    internal class Safety
    {
        public static void Main_()
        {
            var config = new AppConfig("Мой Проект", "123456");

            config.PrintInfo();
            // config.AppName = "Новый";  // ❌ ошибка: init-only
            // AppConfig.Version = "2.0"; // ❌ ошибка: const нельзя изменить
            // config.StartTime = DateTime.Now; // ❌ ошибка: readonly

            Console.WriteLine(AppConfig.InstanceCount); // Статическое поле
        }
    }
}
