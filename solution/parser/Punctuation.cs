namespace solution;

public class Punctuation
{
    public char Content { get; }
    public Punctuation(char content)
    {
        Content = content;
    }
    
    public override string ToString() => Content.ToString();
}   