namespace CshBook.Ответы.Глава_0;

public class ForSolutions
{
    public static void Main_()
    {

    // Выведите на экран n раз фразу "Silence is golden". Число n вводит пользователь.
    int n1 = int.Parse(Console.ReadLine());
    
    for (int i = 0; i < n1; i++)
    {
        Console.WriteLine("Silence is golden");
    }



    // Выведите на экран прямоугольник из нулей. Количество строк вводит пользователь, количество столбцов равно 5.
    
    int rows = int.Parse(Console.ReadLine());
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            Console.Write("0");
        }
        Console.WriteLine();
    }
    
    
    
    // Вывести на экран фигуру из звездочек:
    // *******
    // *******
    // *******
    // *******
    // (квадрат из n строк, в каждой строке n звездочек)
    
    int n2 = int.Parse(Console.ReadLine());
    
    for (int i = 0; i < n2; i++)
    {
        for (int j = 0; j < n2; j++)
        {
            Console.Write("*");
        }
    
        Console.WriteLine();
    }
    
    
    // Вывести на экран числа от 1000 до 9999 такие, что среди цифр есть цифра 3.



    for (int i = 1000; i < 10000; i++)
    {
        int a1 = i / 1000;
        int a2 = i / 100 % 10;
        int a3 = i % 100 / 10;
        int a4 = i % 10;
    
    
        if (a1 == 3 ||
            a2 == 3 ||
            a3 == 3 ||
            a4 == 3)
        {
            Console.WriteLine(i);
        }
        
    }
    
    
    // Вывести на экран:
    // AAABBBAAABBBAAABBB
    // BBBAAABBBAAABBBAAA
    // AAABBBAAABBBAAABBB
    // (таких строк n, в каждой строке m троек AAA)
    
    int n3 = int.Parse(Console.ReadLine());
    int m3 = int.Parse(Console.ReadLine());
    
    for (int i = 0; i < n3; i++)
    {
        string pattern = i % 2 == 0 ? "AAABBB" : "BBBAAA";
    
        for (int j = 0; j < m3; j++)
        {
            Console.Write(pattern);
        }
    
        Console.WriteLine();
    }
    
    
    
    // Вывести на экран:
    // AAAAAAAAAAAAAAAA
    // ABBBBBBBBBBBBBBA
    // ABBBBBBBBBBBBBBA
    // ABBBBBBBBBBBBBBA
    // AAAAAAAAAAAAAAAA
    // (количество строк вводит пользователь, ширина прямоугольника в два раза больше высоты)

    int strCoutn = int.Parse(Console.ReadLine());
    int wh = strCoutn * 2;
    
    for (int i = 0; i < strCoutn; i++)
    {
        for (int j = 0; j < wh; j++)
        {
            string ch = "A";
    
            if (i != strCoutn - 1 &&
                i != 0 &&
                j != wh - 1 &&
                j != 0)
                ch = "B";
    
            Console.Write(ch);
        }
    
        Console.WriteLine();
    }


    //
    // Выведите на экран квадрат из нулей и единиц, причем нули находятся только на диагонали квадрата. Всего в квадрате сто цифр. Обе диагонали состоят из 0
    //

    int n4 = int.Parse(Console.ReadLine());
    
    for (int i = 0; i < n4; i++)
    {
        for (int j = 0; j < n4; j++)
        {
            string ch = "1";
    
            if (i == j ||
                j + i == n4 - 1)
                ch = "0";
            
            Console.Write(ch);
        }
    
        Console.WriteLine();
    }




    // Вывести на экран числа от 1 до 100, которые делятся на 3 или на 5.
    //
    
    for (int i = 1; i < 100; i++)
    {
        if (i % 3 == 0 && i % 5 == 0)
            Console.WriteLine(i);
    }
    
    

    // Вывести на экран все числа от 1 до n, которые являются степенью двойки.
    //  

  
    int n5 = int.Parse(Console.ReadLine());
        
    Console.WriteLine($"Степени двойки от 1 до {n5}:");
        
    for (int i = 1; i <= n5; i *= 2)
    {
        Console.WriteLine(i);
    }

    }
}