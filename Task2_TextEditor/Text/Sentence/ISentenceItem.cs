namespace Task2_TextEditor
{
    public interface ISentenceItem
    {
        string Chars { get; }
        int CharsLength { get; }

        string ToString();
    }
}
