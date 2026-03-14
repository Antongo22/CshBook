namespace CshBook.Answers.Chapter0.Lesson01DataTypes
{
    internal static class AnswerLesson01DataTypes
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 1. Типы данных");
            Console.WriteLine("===================");
            Console.WriteLine();

            {
                string name = "Антон";
                string city = "Москва";
                string hobby = "писать код";

                Console.WriteLine("1. Мини-визитка");
                Console.WriteLine($"Меня зовут {name}, я живу в {city} и люблю {hobby}.");
                Console.WriteLine();
            }

            {
                int price = 350;
                int quantity = 4;
                int total = price * quantity;

                Console.WriteLine("2. Стоимость покупки");
                Console.WriteLine($"Цена: {price}");
                Console.WriteLine($"Количество: {quantity}");
                Console.WriteLine($"Общая стоимость: {total}");
                Console.WriteLine();
            }

            {
                int totalMinutes = 135;
                int hours = totalMinutes / 60;
                int minutes = totalMinutes % 60;

                Console.WriteLine("3. Время в пути");
                Console.WriteLine($"{totalMinutes} минут = {hours} ч. {minutes} мин.");
                Console.WriteLine();
            }

            {
                int firstNumber = 17;
                int secondNumber = 5;

                Console.WriteLine("4. Мини-калькулятор");
                Console.WriteLine($"{firstNumber} + {secondNumber} = {firstNumber + secondNumber}");
                Console.WriteLine($"{firstNumber} - {secondNumber} = {firstNumber - secondNumber}");
                Console.WriteLine($"{firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
                Console.WriteLine($"{firstNumber} / {secondNumber} = {firstNumber / secondNumber}");
                Console.WriteLine($"{firstNumber} % {secondNumber} = {firstNumber % secondNumber}");
                Console.WriteLine();
            }

            {
                int pencils = 29;
                int boxCapacity = 6;
                int fullBoxes = pencils / boxCapacity;
                int remainder = pencils % boxCapacity;

                Console.WriteLine("5. Коробки и остаток");
                Console.WriteLine($"Полных коробок: {fullBoxes}");
                Console.WriteLine($"Останется карандашей: {remainder}");
                Console.WriteLine();
            }

            {
                int firstNumber = 12;
                int secondNumber = 18;

                Console.WriteLine("6. Проверка сравнений");
                Console.WriteLine(firstNumber > secondNumber);
                Console.WriteLine(firstNumber == secondNumber);
                Console.WriteLine(firstNumber != secondNumber);
                Console.WriteLine();
            }

            {
                string studentName = "Маша";
                int age = 16;
                double averageScore = 4.7;

                Console.WriteLine("7. Карточка ученика");
                Console.WriteLine($"Ученик: {studentName}, возраст: {age}, средний балл: {averageScore:F1}");
                Console.WriteLine();
            }

            {
                string productName = "Тетрадь";
                int price = 45;
                int quantity = 3;
                int total = price * quantity;

                Console.WriteLine("8. Чек в магазине");
                Console.WriteLine($"Товар: {productName}");
                Console.WriteLine($"Количество: {quantity} шт. по {price} руб.");
                Console.WriteLine($"Итоговая сумма: {total} руб.");
                Console.WriteLine();
            }

            {
                string studentName = "Лена";
                int firstMark = 5;
                int secondMark = 4;
                int thirdMark = 5;
                int total = firstMark + secondMark + thirdMark;
                double average = total / 3.0;

                Console.WriteLine("9. Учебная статистика");
                Console.WriteLine($"Сумма оценок: {total}");
                Console.WriteLine($"Средний балл: {average:F2}");
                Console.WriteLine($"Ученица {studentName}: сумма = {total}, средний балл = {average:F2}");
                Console.WriteLine();
            }
        }
    }
}
