using System;
using System.IO;
using System.Text.RegularExpressions;

namespace solution;

class Program
{
    static void Main()
    {
        // Путь к файлу
        string inputFilePath = Path.Combine(
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName,
            "input",
            "input.txt"
        );
        
        // Чтение содержимого файла
        string inputText = File.ReadAllText(inputFilePath);
        
        // Распарсить текст
        Text parsedText = Text.Parse(inputText);

        Console.WriteLine("Исходный текст:");
        Console.WriteLine(parsedText);

        Console.WriteLine("\nВсе предложения:");
        foreach (var sentence in parsedText.Sentences)
        {
            Console.WriteLine(sentence);
        }
    }
}


