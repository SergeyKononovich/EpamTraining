using System;

namespace Task2_TextEditor
{
    public class Word : ISentenceItem
    {
        public string Chars { get; }
        public int CharsLength => Chars.Length;


        public Word(string chars)
        {
            if (chars == null) throw new ArgumentNullException(nameof(chars));
            if (chars == String.Empty || chars.Contains(" "))
                throw new FormatException("Chars must not contain spaces or be empty");

            Chars = chars;
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
