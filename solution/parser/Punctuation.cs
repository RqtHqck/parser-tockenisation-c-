using System.Xml.Serialization;

namespace solution;
public class Punctuation
{
    public char Content { get; set; }
    
    public Punctuation()
    {
        Content = '\0'; 
    }

    
    public Punctuation(char content)
    {
        Content = content;
    }
    
    public override string ToString() => Content.ToString();
}   