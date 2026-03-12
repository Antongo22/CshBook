namespace CshBook.Ответы.Глава_0;

public class WhileSolutions
{
    public static void Main_()
    {
        // 1. Напишите цикл while, который выведет числа от 1 до 10.

        int i = 1;
        while (i <= 10)
        {
            Console.WriteLine(i);
            i++;
        }
        
        // 2. Создайте цикл while, который будет находить сумму чисел от 1 до 100.

        int sum = 0;
        int i1 = 0;
        while (i1 !=100)
        {
            i1++;
            sum += i1;
        }
        
        Console.WriteLine(sum);

        //
        // 3. Используя цикл while, выведите все нечетные числа от 1 до 20.

        int i2 = 1;
        while (i2 <= 20)
        {
            if (i2 % 2 != 0)
                Console.WriteLine(i2);
            i2++;
        }

        // 4. Реализуйте бесконечный цикл while, который выводит числа от 1 до бесконечности, но прерывается при достижении числа 15.
        int i3 = 1;
        while (true)
        {
            if (i3 == 15)
                break;
            Console.WriteLine(i3);
            i3++;
        }
        
        // 5. Напишите цикл while, который будет запрашивать у пользователя ввод чисел и завершится, когда пользователь введет число 0.

        while (true)
        {
            int res = int.Parse(Console.ReadLine());
            
            if (res == 0)
                break;
        }
        
        Console.WriteLine("Конец");
        
        //     
        // 1. Напишите цикл do while, который выведет "Hello, World!" 5 раз.

        int i4 = 0;
        do
        {
            Console.WriteLine("Hello, World!");
            i4++;
        } while (i4 <= 4);
        

        // 2. Напишите программу, которая запрашивает ввод числа у пользователя и проверяет, положительное оно или отрицательное, до тех пор, пока пользователь не введет число 0.
        do
        {
            int ans = int.Parse(Console.ReadLine());
            
            if (ans == 0)
                break;
            else if (ans > 0)
                Console.WriteLine("+");
            else
                Console.WriteLine("-");
            
        } while (true);


        // 3. Напишите программу, которая запрашивает у пользователя пароль через цикл do while до тех пор, пока пароль не будет равен "12345".
        string pass;
        do
        {
            pass = Console.ReadLine();
        
        } while (pass != "12345");


        // 4. Используя do while, создайте цикл, который на каждой итерации уменьшает число на 3, начиная с 30, и завершится, когда число станет меньше или равно 0.

        int num = 30;
        do
        {
            Console.WriteLine(num);
            num -= 3;
        } while (num > 0);


    }
}