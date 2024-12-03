using System.Xml.Serialization;

namespace solution;
public class Word
{
    public string Content { get; set; }

    public Word()
    {
        Content = string.Empty; 
    }

    
    public Word(string content)
    {
        Content = content;
    }
    
    public override string ToString() => Content;
}