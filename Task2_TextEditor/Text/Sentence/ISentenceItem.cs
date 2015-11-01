using System;

namespace Task2_TextEditor
{
    public interface ISentenceItem : ICloneable
    {
        string Chars { get; }
        int CharsLength { get; }

        string ToString();
    }
}
