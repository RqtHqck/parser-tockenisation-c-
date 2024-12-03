using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace solution;

[XmlRoot("Text")]
public class Text
{
    public List<Sentence> Sentences { get; set; } = new List<Sentence>();

    
    public Text()
    {
        Sentences = new List<Sentence>();
    }

    public Text(IEnumerable<Sentence> sentences)
    {
        Sentences = new List<Sentence>(sentences);
    }
    
    // Метод для парсинга текста
    public static Text Parse(string input)
    {
        var sentenceStrings = Regex.Split(input, @"(?<=[.!?])\s+"); // Делит предложение посредством . ! ? и создаёт коллекцию предложений
        var sentences = sentenceStrings.Select(Sentence.Parse).ToList(); // Используем Parse для каждого предложения в коллекции sentenceStrings
        // .ToList() - преобразует IEnumerable<Sentence> в конкретный тип List<Sentence>

        return new Text(sentences);
    }
    
    // Метод для получения предложений в порядке возрастания количества слов
    public IEnumerable<Sentence> GetSentencesByWordCount()
    {
        return Sentences.OrderBy(s => s.WordCount);
    }
    
    // Метод для получения предложений в порядке возрастания количества слов
    public IEnumerable<Sentence> GetSentencesByLength()
    {
        return Sentences.OrderBy(s => s.GetLength());
    }
    
    public IEnumerable<string> GetWordsOfLengthInQuestions(int length)
    {
        var result = new List<string>();

        foreach (var sentence in Sentences)
        {
            if (sentence.ToString().Trim().EndsWith("?"))
            {
                // Перебираем только слова в текущем предложении
                foreach (var word in sentence.Words)
                {
                    // Добавляем слово в результат, если его длина равна length и оно еще не было добавлено
                    if (word.Content.Length == length && !result.Contains(word.Content))
                    {
                        result.Add(word.Content);
                    }
                }
            }
        }
    
        return result;
    }

    public Text RemoveWordsByConsonant(int length)
    {
    var updatedSentences = new List<Sentence>();

    foreach (var sentence in Sentences)
    {
        var filteredElements = new List<object>();

        foreach (var element in sentence.Elements)
        {
            if (element is Word word)
            {
                string constant = "BCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyzБВГДЖЗКЛМНПРСТФХЦЧШЩбвгджзклмнпрстфхцчшщ";
  
                // Проверяем, если слово НЕ начинается с согласной или длина слова не равна указанной
                if (word.Content.Length != length || !constant.Contains(word.Content[0]))
                {
                    filteredElements.Add(word);
                }
            }
            else
            {
                // Добавляем другие элементы (пунктуацию)
                filteredElements.Add(element);
            }
        }

        // Создаём новое предложение с отфильтрованными элементами
        updatedSentences.Add(new Sentence(filteredElements));
    }

        // Возвращаем новый текст с обновлёнными предложениями
        return new Text(updatedSentences);
    }
    
    // Метод для удаления стоп-слов из текста
    public Text RemoveStopWords(string stopWordsFilePath)
    {
        var stopWords = LoadStopWords(stopWordsFilePath);
        var updatedSentences = new List<Sentence>();

        foreach (var sentence in Sentences)
        {
            var filteredElements = new List<object>();
            foreach (var element in sentence.Elements)
            {
                if (element is Word word)
                {
                    if (!stopWords.Contains(word.Content.ToLower())) { filteredElements.Add(word); }
                }
                else { filteredElements.Add(element); }
            }
            updatedSentences.Add(new Sentence(filteredElements));
        }

        return new Text(updatedSentences); // Возвращаем новый текст без стоп-слов
    }

    // Метод для загрузки стоп-слов из файла
    public static IEnumerable<string> LoadStopWords(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Стоп-слова файл не найден", filePath);
        }

        return File.ReadAllLines(filePath).Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line));
    }


    public override string ToString() => string.Join(" ", Sentences.Select(s => s.ToString())); 
}
