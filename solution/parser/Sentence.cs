using System.Text.RegularExpressions;

namespace solution;

public class Sentence
{
    private List<object> _elements;
    // objects - любой тип объектов
    
    public List<object> Elements // Свойство для доступа к элементам
    {
        get => _elements;
        set => _elements = value; // Теперь можно изменять элементы списка
    }
    
    public Sentence(IEnumerable<object> elements)
    {
        _elements = new List<object>(elements);
    }

    public static Sentence Parse(string sentence)
    {
        var elements = new List<object>();
        var matches = Regex.Matches(sentence, @"\w+|[^\w\s]"); 
        // Регулярное выражение @"\w+|[^\w\s]" находит последовательности букв, цифр и подчеркиваний (слова),
        // а также символы, которые не являются буквами, цифрами или пробелами (например, знаки препинания).

        foreach (Match match in matches)
        {
            string token = match.Value;

            if (char.IsPunctuation(token[0]))
                elements.Add(new Punctuation(token[0]));
            else
                elements.Add(new Word(token));
        }

        return new Sentence(elements);
    }
    
    public override string ToString() => string.Join(" ", _elements.Select(e => e.ToString()));
}
