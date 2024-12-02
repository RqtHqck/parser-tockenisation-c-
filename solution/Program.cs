using System;
using System.IO;
using System.Text.RegularExpressions;

namespace solution;

class Program
{
    static void Main()
    {
        // Построение пути с использованием Path.Combine
        string baseDirectory = "/home/rqthqck/Coding/C#/lab3/solution";
        string relativePath = "input/input.txt";
        string filePath = Path.Combine(baseDirectory, relativePath);
        // Регулярное выражение для разделения на слова и знаки пунктуации
        string pattern = @"\w+|[^\w\s]+";

        try
        {
            string fileContent = File.ReadAllText(filePath);
            Console.WriteLine("Содержимое файла:");
            Console.WriteLine(fileContent);

            // Разделение текста на слова и знаки пунктуации
            var matches = Regex.Matches(fileContent, pattern);

            Console.WriteLine("Разбито на слова и знаки пунктуации:");
            foreach (var match in matches)
            {
                Console.WriteLine(match);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
