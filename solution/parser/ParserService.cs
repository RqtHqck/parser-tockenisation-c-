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

    public ParserService(string inputFilePath, string stopWordsFilePath, string xmlFilePath)
    {
        _inputFilePath = inputFilePath;
        _stopWordsFilePath = stopWordsFilePath;
        _xmlFilePath = xmlFilePath;

        string inputText = File.ReadAllText(_inputFilePath);
        _parsedText = Text.Parse(inputText);
    }

    public void DisplayOriginalText()
    {
        Console.WriteLine("Исходный текст:");
        Console.WriteLine(_parsedText);
    }

    public void DisplaySentences()
    {
        Console.WriteLine("\nВсе предложения:");
        foreach (var sentence in _parsedText.Sentences)
        {
            Console.WriteLine(sentence);
        }
    }

    public void DisplaySortedSentencesByWordCount()
    {
        Console.WriteLine("\nВ порядке возрастания по количеству слов в предложении:");
        foreach (var sentence in _parsedText.GetSentencesByWordCount())
        {
            Console.WriteLine(sentence);
        }
    }

    public void DisplaySortedSentencesByLength()
    {
        Console.WriteLine("\nВ порядке возрастания по длине предложения:");
        foreach (var sentence in _parsedText.GetSentencesByLength())
        {
            Console.WriteLine(sentence);
        }
    }

    public void FindWordsInQuestionsByLength()
    {
        Console.WriteLine("Введите длину искомого слова:");
        if (int.TryParse(Console.ReadLine(), out int length))
        {
            var words = _parsedText.GetWordsOfLengthInQuestions(length);
            Console.WriteLine($"\nСлова с указанной длиной в вопросительных предложениях:");
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
        Console.WriteLine("Введите длину искомого слова:");
        if (int.TryParse(Console.ReadLine(), out int length))
        {
            _parsedText = _parsedText.RemoveWordsByConsonant(length);
            Console.WriteLine("\nТекст после удаления слов:");
            Console.WriteLine(_parsedText);
        }
        else
        {
            Console.WriteLine("Неверный ввод для длины слова.");
        }
    }

    public void ReplaceWordsByLengthInSentence()
    {
        Console.WriteLine("\nВсе предложения:");
        for (int i = 0; i < _parsedText.Sentences.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {_parsedText.Sentences[i]}");
        }

        Console.WriteLine("\nВыберите номер предложения из данных:");
        if (int.TryParse(Console.ReadLine(), out int sentenceIndex) && sentenceIndex > 0 && sentenceIndex <= _parsedText.Sentences.Count)
        {
            Sentence sentence = _parsedText.Sentences[sentenceIndex - 1];

            Console.WriteLine("Введите длину искомого слова:");
            if (int.TryParse(Console.ReadLine(), out int length))
            {
                Console.WriteLine("\nУкажите подстроку:");
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
        Console.WriteLine("Удаление стоп-слов:");
        _parsedText = _parsedText.RemoveStopWords(_stopWordsFilePath);
        Console.WriteLine("\nТекст после удаления стоп-слов:");
        Console.WriteLine(_parsedText);
    }

    public void ExportToXml()
    {
        XmlWorker.SerializeToXml(_parsedText, _xmlFilePath);
        Console.WriteLine($"\nТекст экспортирован в XML файл: {_xmlFilePath}");
    }
}
