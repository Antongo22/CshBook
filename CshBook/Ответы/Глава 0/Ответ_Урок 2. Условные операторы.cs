namespace CshBook.Answers.Chapter0.Lesson02Conditionals
{
    internal static class AnswerLesson02Conditionals
    {
        public static void Main_()
        {
            Console.WriteLine("Урок 2. Условные операторы");
            Console.WriteLine("==========================");
            Console.WriteLine();

            {
                int age = 19;

                Console.WriteLine("1. Проверка возраста");
                if (age >= 18)
                {
                    Console.WriteLine("Можно войти");
                }
                else
                {
                    Console.WriteLine("Пока нельзя");
                }
                Console.WriteLine();
            }

            {
                int number = 14;

                Console.WriteLine("2. Четное или нечетное");
                if (number % 2 == 0)
                {
                    Console.WriteLine("Обычный if: Четное");
                }
                else
                {
                    Console.WriteLine("Обычный if: Нечетное");
                }

                string result = number % 2 == 0 ? "Четное" : "Нечетное";
                Console.WriteLine($"Тернарный оператор: {result}");
                Console.WriteLine();
            }

            {
                int firstNumber = 12;
                int secondNumber = 9;

                Console.WriteLine("3. Кто больше");
                if (firstNumber > secondNumber)
                {
                    Console.WriteLine("Первое больше");
                }
                else if (secondNumber > firstNumber)
                {
                    Console.WriteLine("Второе больше");
                }
                else
                {
                    Console.WriteLine("Числа равны");
                }
                Console.WriteLine();
            }

            {
                int firstNumber = 8;
                int secondNumber = 15;
                int thirdNumber = 11;
                int max = firstNumber;

                if (secondNumber > max)
                {
                    max = secondNumber;
                }

                if (thirdNumber > max)
                {
                    max = thirdNumber;
                }

                Console.WriteLine("4. Самое большое из трех");
                Console.WriteLine($"Максимальное число: {max}");
                Console.WriteLine();
            }

            {
                double purchaseAmount = 2750;
                double discountRate;

                if (purchaseAmount < 1000)
                {
                    discountRate = 0;
                }
                else if (purchaseAmount < 3000)
                {
                    discountRate = 0.05;
                }
                else
                {
                    discountRate = 0.10;
                }

                double discountAmount = purchaseAmount * discountRate;
                double finalAmount = purchaseAmount - discountAmount;

                Console.WriteLine("5. Скидка в магазине");
                Console.WriteLine($"Скидка: {discountAmount:F2}");
                Console.WriteLine($"Итоговая сумма: {finalAmount:F2}");
                Console.WriteLine();
            }

            {
                string login = "admin";
                string password = "12345";
                string expectedLogin = "admin";
                string expectedPassword = "12345";

                Console.WriteLine("6. Проверка пароля");
                if (login == expectedLogin && password == expectedPassword)
                {
                    Console.WriteLine("Вход выполнен");
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль");
                }
                Console.WriteLine();
            }

            {
                int age = 15;

                Console.WriteLine("7. Категория билета");
                if (age < 14)
                {
                    Console.WriteLine("Детский билет");
                }
                else if (age <= 17)
                {
                    Console.WriteLine("Подростковый билет");
                }
                else
                {
                    Console.WriteLine("Взрослый билет");
                }
                Console.WriteLine();
            }

            {
                int monthNumber = 7;

                Console.WriteLine("8. Номер месяца");
                if (monthNumber == 12 || monthNumber == 1 || monthNumber == 2)
                {
                    Console.WriteLine("Зима");
                }
                else if (monthNumber >= 3 && monthNumber <= 5)
                {
                    Console.WriteLine("Весна");
                }
                else if (monthNumber >= 6 && monthNumber <= 8)
                {
                    Console.WriteLine("Лето");
                }
                else if (monthNumber >= 9 && monthNumber <= 11)
                {
                    Console.WriteLine("Осень");
                }
                else
                {
                    Console.WriteLine("Такого месяца нет");
                }
                Console.WriteLine();
            }

            {
                int number = 2048;
                int absoluteNumber = number < 0 ? -number : number;
                string result = absoluteNumber >= 1000 && absoluteNumber <= 9999 ? "Успешно" : "Неудача";

                Console.WriteLine("9. Четырехзначное число");
                Console.WriteLine(result);
                Console.WriteLine();
            }

            {
                double firstNumber = 12;
                double secondNumber = 0;
                string operation = "/";

                Console.WriteLine("10. Мини-калькулятор");
                if (operation == "+")
                {
                    Console.WriteLine($"Результат: {firstNumber + secondNumber}");
                }
                else if (operation == "-")
                {
                    Console.WriteLine($"Результат: {firstNumber - secondNumber}");
                }
                else if (operation == "*")
                {
                    Console.WriteLine($"Результат: {firstNumber * secondNumber}");
                }
                else if (operation == "/")
                {
                    if (secondNumber == 0)
                    {
                        Console.WriteLine("Ошибка: деление на 0 невозможно");
                    }
                    else
                    {
                        Console.WriteLine($"Результат: {firstNumber / secondNumber}");
                    }
                }
                else
                {
                    Console.WriteLine("Неизвестная операция");
                }
                Console.WriteLine();
            }
        }
    }
}
