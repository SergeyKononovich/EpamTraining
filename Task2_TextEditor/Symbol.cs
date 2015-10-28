using System;

namespace Task2_TextEditor
{
    public struct Symbol
    {
        public string Chars { get; }


        public Symbol(string chars)
        {
            if (chars == null) throw new ArgumentNullException(nameof(chars));
            if (chars == String.Empty) throw new FormatException("Сan not be an empty string.");

            Chars = chars;
        }
        public Symbol(char source)
        {
            Chars = source.ToString();
        }
    }
}
