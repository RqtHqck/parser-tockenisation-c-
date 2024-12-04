using System;
using System.Collections.Generic;
using System.IO;

namespace solution;
public class ParserService
{
    private readonly string _inputFilePath;
    private readonly string _stopWordsFilePath;
    private readonly string _xmlFilePath;
    private Text _parsedText;
    private Text _originalText;

    public Text ParsedText
    {
        get => _parsedText; 
        set => _parsedText = value; 
    }

    public ParserService(string inputFilePath, string stopWordsFilePath, string xmlFilePath)
    {
        _inputFilePath = inputFilePath;
        _stopWordsFilePath = stopWordsFilePath;
        _xmlFilePath = xmlFilePath;

        string inputText = File.ReadAllText(_inputFilePath);
        _originalText = Text.Parse(inputText);
        ParsedText = _originalText;
    }
    
    private void ResetToOriginalText()
    {
        ParsedText = _originalText;
    }

    public void DisplayOriginalText()
    {
        Console.WriteLine("Исходный токенизированный текст:");
        Console.WriteLine(ParsedText);
    }

    public void DisplaySentences()
    {
        Console.WriteLine("\nВсе предложения:");
        foreach (var sentence in ParsedText.Sentences)
        {
            Console.WriteLine(sentence);
        }
    }

    public void DisplaySortedSentencesByWordCount()
    {
        ResetToOriginalText();
        Console.WriteLine("\n\n1. Вывести все предложения заданного текста в порядке возрастания количества слов в предложениях");
        foreach (var sentence in ParsedText.GetSentencesByWordCount())
        {
            Console.WriteLine(sentence);
        }

    }

    public void DisplaySortedSentencesByLength()
    {
        ResetToOriginalText();
        Console.WriteLine("\n\n2. Вывести все предложения заданного текста в порядке возрастания длины предложения.");
        foreach (var sentence in ParsedText.GetSentencesByLength())
        {
            Console.WriteLine(sentence);
        }
    }

    public void FindWordsInQuestionsByLength()
    {
        ResetToOriginalText();
        Console.WriteLine("\n\n3. Во всех вопросительных предложениях текста найти слова заданной длины (не повторять одни и те же слова).");
        Console.WriteLine("Задать длину слова:");
        if (int.TryParse(Console.ReadLine(), out int length))
        {
            var words = ParsedText.GetWordsOfLengthInQuestions(length);
            Console.WriteLine($"Слова с указанной длиной в вопросительных предложениях:");
            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }
        else
        {
            Console.WriteLine("Неверный ввод для длины слова.");
        }
    }

    public void RemoveWordsByConsonantLength()
    {
        ResetToOriginalText();
        Console.WriteLine("\n\n4. Удалить из текста все слова заданной длины, начинающиеся с согласной буквы.");
        Console.WriteLine("Задать длину слова:");
        if (int.TryParse(Console.ReadLine(), out int length))
        {
            ParsedText = ParsedText.RemoveWordsByConsonant(length);
            Console.WriteLine("Текст после удаления слов:");
            Console.WriteLine(ParsedText);
        }
        else
        {
            Console.WriteLine("Неверный ввод для длины слова.");
        }
    }

    public void ReplaceWordsByLengthInSentence()
    {
        ResetToOriginalText();
        Console.WriteLine("\n\n5. В некотором предложении текста заменить слова заданной длины на указанную подстроку, длина которой может не совпадать с длиной слова.");
        Console.WriteLine("Все предложения:");
        for (int i = 0; i < ParsedText.Sentences.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {ParsedText.Sentences[i]}");
        }

        Console.WriteLine("Выберите номер предложения:");
        if (int.TryParse(Console.ReadLine(), out int sentenceIndex) && sentenceIndex > 0 && sentenceIndex <= ParsedText.Sentences.Count)
        {
            Sentence sentence = ParsedText.Sentences[sentenceIndex - 1];

            Console.WriteLine("Задать длину слова:");
            if (int.TryParse(Console.ReadLine(), out int length))
            {
                Console.WriteLine("Введите подстроку:");
                string userString = Console.ReadLine();
                if (userString != null && userString.Length != length)
                {
                    Sentence updatedSentence = sentence.replaceWordsByLength(length, sentence, userString);
                    Console.WriteLine("Обновленное предложение:");
                    Console.WriteLine(updatedSentence);
                }
                else
                {
                    Console.WriteLine("Некорректная длина подстроки.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод для длины слова.");
            }
        }
        else
        {
            Console.WriteLine("Ошибка: введите корректный номер предложения.");
        }
    }

    public void RemoveStopWords()
    {
        ResetToOriginalText();
        Console.WriteLine("\n\n6. В текстах часто встречаются стоп-слова. Удалить стоп-слова из текста.");
        ParsedText = ParsedText.RemoveStopWords(_stopWordsFilePath);
        Console.WriteLine(ParsedText);
    }

    public void ExportToXml()
    {
        // 7. Экспортировать текстовый объект в XML-документ (с помощью механизма сериализации, System.Xml.Serialization)
        ResetToOriginalText();
        XmlWorker.SerializeToXml(ParsedText, _xmlFilePath);
        Console.WriteLine($"\nТекст экспортирован в XML файл: {_xmlFilePath}");
    }

    public void Concordance()
    {
        ResetToOriginalText();
        Dictionary<string, List<int>> dict= ParsedText.Concordance();
        
        Console.WriteLine("{0,-25} {1,-15} {2}", "Слово", "Повторений", "Номера предложений");
        Console.WriteLine(new string('-', 50));
        
        foreach (var entry in dict)
        {
            string word = entry.Key;
            int occurrences = entry.Value[0];
            string sentenceNumbers = string.Join(", ", entry.Value.Skip(1));
            
            Console.WriteLine("{0,-25} {1,-15} {2}", word, occurrences, sentenceNumbers);

        }
    }
}
