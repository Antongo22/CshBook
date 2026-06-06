namespace CshBook.Lessons.Chapter2.Lesson25AdditionalOop
{
    #region Теория
    /*
        В этом уроке собраны дополнительные возможности ООП,
        которые встречаются реже, чем классы, свойства и наследование,
        но помогают делать свои типы удобнее.

        На этом этапе берем только две темы:

        - перегрузка операторов;
        - индексаторы.
     */

    /*
        Перегрузка операторов позволяет объяснить C#,
        как оператор должен работать с твоим типом.

        Например, если есть Vector2,
        можно сделать так:

        Vector2 sum = first + second;

        Без перегрузки операторов C# не знает,
        как складывать два объекта Vector2.
     */

    /*
        Оператор перегружается как static-метод:

        public static Vector2 operator +(Vector2 left, Vector2 right)

        Такой метод должен вернуть результат операции.
     */

    /*
        Перегружать оператор стоит только тогда,
        когда его смысл очевиден.

        Сложение векторов через + понятно.
        Сложение двух пользователей через + уже выглядит странно.

        Хорошая перегрузка делает код понятнее,
        плохая перегрузка делает код загадочным.
     */

    /*
        Индексатор позволяет обращаться к объекту как к массиву:

        collection[0]

        Это удобно, если класс внутри хранит набор элементов
        и ты хочешь дать короткий доступ по индексу.
     */

    /*
        Индексатор похож на свойство,
        но вместо имени используется this[index]:

        public int this[int index]
        {
            get { ... }
            set { ... }
        }

        На старте достаточно уметь делать простой индексатор
        для доступа к внутреннему массиву.
     */

    /*
        Анонимные типы, implicit/explicit-преобразования
        и сложные операторы сравнения лучше изучать позже.

        Они полезны, но на текущем этапе важнее уверенно понять
        базовую идею операторов и индексаторов.
     */
    #endregion

    class Vector2
    {
        public int X { get; }
        public int Y { get; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }

    class NumberBox
    {
        private int[] _numbers;

        public NumberBox(int size)
        {
            _numbers = new int[size];
        }

        public int Length
        {
            get
            {
                return _numbers.Length;
            }
        }

        public int this[int index]
        {
            get
            {
                return _numbers[index];
            }
            set
            {
                _numbers[index] = value;
            }
        }
    }

    internal static class Lesson25AdditionalOop
    {
        public static void Main_()
        {
            Vector2 first = new Vector2(2, 3);
            Vector2 second = new Vector2(5, 1);

            Vector2 sum = first + second;
            Vector2 difference = first - second;

            Console.WriteLine(sum);
            Console.WriteLine(difference);

            Console.WriteLine("----");

            NumberBox box = new NumberBox(3);
            box[0] = 10;
            box[1] = 20;
            box[2] = 30;

            for (int i = 0; i < box.Length; i++)
            {
                Console.WriteLine(box[i]);
            }
        }
    }

    #region Задачи
    /*
        Разминка

        1. Вектор.
           Создай класс Vector2 с X и Y.
           Добавь конструктор и метод ToString().

        2. Сложение векторов.
           Перегрузи оператор +,
           чтобы складывать координаты двух Vector2.

        3. Вычитание векторов.
           Перегрузи оператор -.

        Основные задачи

        4. Умножение на число.
           Перегрузи оператор *,
           чтобы Vector2 можно было умножить на int.

        5. Коробка чисел.
           Создай класс NumberBox,
           который внутри хранит массив int.

        6. Индексатор.
           Добавь индексатор this[int index],
           чтобы можно было читать и записывать элементы NumberBox через [].

        7. Длина.
           Добавь свойство Length только для чтения.

        8. Сумма.
           Добавь метод Sum(),
           который возвращает сумму всех чисел внутри NumberBox.

        Задачи на перенос

        9. Полка книг.
           Создай класс BookShelf,
           который хранит массив строк с названиями книг
           и дает доступ к ним через индексатор.

        10. Цена товара.
            Создай класс Money с полем Amount.
            Перегрузи + и - для сложения и вычитания денежных значений.
     */
    #endregion
}
