using System;

namespace Task2_TextEditor
{
    public struct Symbol
    {
        public string Chars { get; }


        public Symbol(string chars)
        {
            Chars = chars;
        }
        public Symbol(char source)
        {
            Chars = source.ToString();
        }
        public override string ToString()
        {
            return Chars;
        }
    }
}
