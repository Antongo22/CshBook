using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshBook.Lessons
{

    /* Общая информация
     
    Работа с файлами - очень важная часть в программировании.
    
    Пока что ты хранил все данные как переменные, они хранятся в оперативной памяти. 
    Когда мы работаем с файлами, мы храним их в памяти компьютера, что может привести к ухудшению скорости доступа к данным. 
    Однако, это даёт нам больше места, и освобождает ОЗУ, что хорошо для программы.


    Чтение и запись файлов
    Чтение - это процесс чтения данных из файлов. 
    Запись - это процесс записи данных в файл.   
     */

    /* Stream (FileStream )
     
    Stream — это абстрактный базовый класс, который представляет поток байтов. 
    Он является основой для всех классов, работающих с потоками данных, таких как файловые потоки, сетевые потоки, потоки памяти и т.д. 
    Класс Stream предоставляет базовые методы для чтения, записи и поиска в потоке.
     
    Read(byte[] buffer, int offset, int count): Считывает последовательность байтов из текущего потока и перемещает позицию в потоке на количество считанных байтов.

    Write(byte[] buffer, int offset, int count): Записывает последовательность байтов в текущий поток.

    Seek(long offset, SeekOrigin origin): Устанавливает позицию в текущем потоке.

    Flush(): Очищает все буферы для текущего потока и вызывает запись всех буферизованных данных на базовое устройство.

    Close(): Закрывает текущий поток и освобождает все ресурсы, связанные с потоком.

    Length: Возвращает длину потока в байтах.

    Position: Возвращает или задает текущую позицию в потоке.



    Read(byte[] buffer, int offset, int count)
    Описание: Считывает последовательность байтов из текущего потока и перемещает позицию в потоке на количество считанных байтов.

    Параметры:

    byte[] buffer: Массив байтов, в который будут считаны данные из потока.

    int offset: Смещение в массиве buffer, с которого начинается запись считанных данных.

    int count: Максимальное количество байтов, которое нужно считать из потока.

    Возвращаемое значение: Количество байтов, фактически считанных из потока.

    Пример:
    byte[] buffer = new byte[100];
    int bytesRead = stream.Read(buffer, 0, buffer.Length);


    Write(byte[] buffer, int offset, int count)
    Описание: Записывает последовательность байтов в текущий поток.

    Параметры:

    byte[] buffer: Массив байтов, из которого будут записаны данные в поток.

    int offset: Смещение в массиве buffer, с которого начинается чтение данных для записи.

    int count: Количество байтов, которое нужно записать в поток.

    Пример:
    byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
    stream.Write(data, 0, data.Length);


    Seek(long offset, SeekOrigin origin)
    Описание: Устанавливает позицию в текущем потоке.

    Параметры:

    long offset: Смещение байтов относительно начальной позиции, заданной параметром origin.

    SeekOrigin origin: Поле типа SeekOrigin, которое указывает начальную позицию для поиска. Возможные значения: Begin, Current, End.

    Возвращаемое значение: Новая позиция в потоке.

    Пример:
    stream.Seek(0, SeekOrigin.Begin); // Перемещает позицию в начало потока

     */

    /* File
     
     Класс File предоставляет статические методы для создания, копирования, удаления, перемещения и открытия файлов. Он является высокоуровневым классом, который упрощает работу с файлами, предоставляя методы для выполнения общих операций.

    Основные методы:
    Create(string path): Создает или перезаписывает файл по указанному пути.

    Open(string path, FileMode mode): Открывает файл в указанном режиме.

    Delete(string path): Удаляет файл по указанному пути.

    Copy(string sourceFileName, string destFileName): Копирует существующий файл в новый файл.

    Move(string sourceFileName, string destFileName): Перемещает указанный файл в новое местоположение.

    ReadAllText(string path): Открывает текстовый файл, считывает весь текст файла и затем закрывает файл.

    WriteAllText(string path, string contents): Создает новый файл, записывает в него указанную строку и затем закрывает файл.
     
     */

    internal class SeventhLesson
    {
        public static void Main_()
        {
            string filePath = "example.txt";

            #region FileStream

            string text;
            // Запись в файл
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                text = "Hello, World!";
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush(); // Очистка буферов
            }

            // Чтение из файла
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                int bytesRead = fs.Read(buffer, 0, buffer.Length);
                text = System.Text.Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);

                // Перемещение позиции в начало потока
                fs.Seek(0, SeekOrigin.Begin);

                // Получение текущей позиции
                long currentPosition = fs.Position;
                Console.WriteLine($"Current position: {currentPosition}");

                // Получение длины потока
                long length = fs.Length;
                Console.WriteLine($"Stream length: {length}");
            }

            // если мы используем using, то нам не нужно писать fs.Close() и проводить другие махинации для очистки потока;
            #endregion

            #region File
            // Запись в файл
            text = "Hello, World!";
            File.WriteAllText(filePath, text);

            // Чтение из файла
            string readText = File.ReadAllText(filePath);
            Console.WriteLine(readText);

            // Копирование файла
            string copyFilePath = "example_copy.txt";
            File.Copy(filePath, copyFilePath);

            // Удаление файла
            File.Delete(copyFilePath);
            #endregion
        }
    }

    /* Задачи
     
     1. Чтение и вывод содержимого файла:
        Напишите программу, которая открывает текстовый файл, читает его содержимое и выводит его на экран.

     2.Запись строки в файл:
       Напишите программу, которая запрашивает у пользователя строку и записывает ее в текстовый файл. Если файл уже существует, его содержимое должно быть удалено.

    3. Добавление текста в файл:
       Напишите программу, которая запрашивает у пользователя строку и добавляет эту строку в конец текстового файла, 
       не удаляя его предыдущего содержимого.
    
    4. Подсчет строк в файле:
       Напишите программу, которая открывает текстовый файл и подсчитывает количество строк в нем.

    5. Копирование содержимого одного файла в другой:
       Напишите программу, которая читает содержимое одного текстового файла и записывает его в другой файл.
     */
}
