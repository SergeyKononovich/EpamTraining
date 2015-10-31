using System;

namespace Task2_TextEditor
{
    public class Punctuation : ISentenceItem
    {
        public string Chars { get; }
        public bool IsSentencesSeparator { get; }
        public bool IsSyntacticConstructionsSeparator { get; }
        public int CharsLength => Chars.Length;


        public Punctuation(string chars, bool isSyntacticConstructionsSeparator = false, 
                           bool isSentencesSeparator = false)
        {
            if (chars == null) throw new ArgumentNullException(nameof(chars));
            if (chars == String.Empty || chars.Contains(" "))
                throw new FormatException("Chars must not contain spaces or be empty");

            Chars = chars;
            IsSyntacticConstructionsSeparator = isSyntacticConstructionsSeparator;
            IsSentencesSeparator = isSentencesSeparator;
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
