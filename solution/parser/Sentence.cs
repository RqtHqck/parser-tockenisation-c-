using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace solution;
[XmlInclude(typeof(Word))]
[XmlInclude(typeof(Punctuation))]
public class Sentence
{
    [XmlElement("Word", typeof(Word))]
    [XmlElement("Punctuation", typeof(Punctuation))]
    public List<object> Elements { get; set; }

    public Sentence()
    {
        Elements = new List<object>(); // Инициализируем пустой список
    }
    
    public Sentence(IEnumerable<object> elements)
    {
        Elements = new List<object>(elements);
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
    
    public IEnumerable<Word> Words => Elements.OfType<Word>();
    
    public int WordCount => Elements.Count(e => e is Word);
    
    public int GetLength() => Elements.Sum(e => e.ToString().Length);

    public Sentence replaceWordsByLength(int length, Sentence sentence, string userString)
    {
        var updatedElements = new List<object>();

        foreach (var element in sentence.Elements)
        {
            if (element is Word word && word.Content.Length == length)
            {
                // Заменяем слово на подстроку
                updatedElements.Add(new Word(userString));
            }
            else
            {
                // Сохраняем остальные элементы без изменений
                updatedElements.Add(element);
            }
        }

        return new Sentence(updatedElements);
    }


    public override string ToString() => string.Join(" ", Elements.Select(e => e.ToString()));
}
