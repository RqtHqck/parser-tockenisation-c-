using System;
using System.IO;

namespace solution;

class Program
{
    static void Main()
    {
        // Путь к файлам
        string baseDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        string inputFilePath = Path.Combine(baseDirectory, "input", "input.txt");
        string stopWordsFilePath = Path.Combine(baseDirectory, "input", "stopWords.txt");
        string xmlFilePath = Path.Combine(baseDirectory, "output", "xmlFile.xml");

        // Инициализация ParserService
        ParserService parserService = new ParserService(inputFilePath, stopWordsFilePath, xmlFilePath);

        // Выполнение задач
        parserService.DisplayOriginalText();
        parserService.DisplaySentences();
        parserService.DisplaySortedSentencesByWordCount();
        parserService.DisplaySortedSentencesByLength();
        parserService.FindWordsInQuestionsByLength();
        parserService.RemoveWordsByConsonantLength();
        parserService.ReplaceWordsByLengthInSentence();
        parserService.RemoveStopWords();
        parserService.ExportToXml();
        parserService.Concordance();
    }
}