using System;

namespace Task2_TextEditor
{
    public interface ITextItem : ICloneable
    {
        int CharsLength { get; }

        string ToString();
    }
}
