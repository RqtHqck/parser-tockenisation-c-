using System.Text.RegularExpressions;

namespace solution;

public class Text
{
    private List<Sentence> _sentences; // Теперь это обычное поле, без модификатора readonly

    public List<Sentence> Sentences // Свойство для доступа к предложениям
    {
        get => _sentences;
        set => _sentences = value; // Теперь можно изменять список предложений
    }
    
    public Text(IEnumerable<Sentence> sentences)
    {
        _sentences = new List<Sentence>(sentences); // Инициализация списка предложений
    }
    
    // Статический метод для парсинга текста
    public static Text Parse(string input)
    {
        var sentenceStrings = Regex.Split(input, @"(?<=[.!?])\s+"); // Делит предложение посредством . ! ? и создаёт коллекцию предложений
        var sentences = sentenceStrings.Select(Sentence.Parse).ToList(); // Используем Parse для каждого предложения в коллекции sentenceStrings
        // .ToList() - преобразует IEnumerable<Sentence> в конкретный тип List<Sentence>

        return new Text(sentences);
    }
    
    public override string ToString() => string.Join(" ", _sentences.Select(s => s.ToString())); 
}
