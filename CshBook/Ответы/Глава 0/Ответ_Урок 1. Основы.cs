using System;

namespace CshBook.Answers.Answ0
{
    public class BaseSolutions
    {
        public static void Main_()
        {
            // 1 ������������ ������ ��� �����, ����� ����� �� �����
            Console.Write("������� ������ �����: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("������� ������ �����: ");
            int num2 = int.Parse(Console.ReadLine());
            int sum1 = num1 + num2;
            Console.WriteLine("����� = " + sum1);

            Console.WriteLine("\n\n");


            // 2 ������������ ������ �����, �������� ��� ����� �� 5 ��� �������
            Console.Write("������� �����: ");
            int num3 = int.Parse(Console.ReadLine());
            int res1 = num3 / 5;
            Console.WriteLine("������� ��� �������: " + res1);

            Console.WriteLine("\n\n");
            Console.WriteLine("\n\n");


            // 3 ������������ ������ �����, ����� ������� �� ������� 100 �� ��� �����
            Console.Write("������� �����: ");
            int num4 = int.Parse(Console.ReadLine());
            int res2 = num4 % 100;
            Console.WriteLine("������� � �������� �� 100: " + res1);

            Console.WriteLine("\n\n");

            // 4 �������� ������ �� ����� 1 - 3 � ������� ������� �� ����� f-������ � �������

            Console.WriteLine($"{num1} + {num2} = {sum1}");
            Console.WriteLine($"{num3} / 5 = {res1}");
            Console.WriteLine($"{num4} % 100 = {res2}");

            Console.WriteLine("\n\n");

            // 5 ������������ ������ �������. ������� True, ���� ���� ������ 18, ����� else
            Console.Write("������� �������: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine(age > 18);

        }

    }
}
