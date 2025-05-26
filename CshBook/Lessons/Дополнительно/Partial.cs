using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons.Дополнительно
{

    #region Partial classes

    /*
     Помогает нам разбивать класс на несколько частей (файлов).
     Главное, что они должны быть в одном и том же пространстве имен.
     */


    public partial class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string GetFullName() => $"{FirstName} {LastName}";
    }


    public partial class User
    {
        public int Age { get; set; }

        public void SayHello()
        {
            Console.WriteLine($"Привет, меня зовут {GetFullName()} и мне {Age} лет.");
        }
    }
    #endregion


    #region Partial methods

    /*
     Можно объявить частичный метод в одном месте и реализовать его (или не реализовать) в другом:
     */

    public partial class User
    {
        partial void OnCreated();

        public User()
        {
            OnCreated(); // если не реализован — просто убирается компилятором
        }
    }


    public partial class User
    {
        partial void OnCreated()
        {
            Console.WriteLine("Пользователь создан.");
        }
    }
    #endregion



    internal class Partial
    {
        public static void Main() 
        {
            var user = new User { FirstName = "Антон", LastName = "Алейниченко", Age = 18 };
            user.SayHello();
            // Вывод: Привет, меня зовут Антон Алейниченко и мне 18 лет.

        }

    }
}
