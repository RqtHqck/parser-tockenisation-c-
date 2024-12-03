namespace solution;

public class Word
{
    public string Content { get; }
    public Word(string content)
    {
        Content = content;
    }
    
    public override string ToString() => Content;
}