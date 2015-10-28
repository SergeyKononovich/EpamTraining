using System.Linq.Expressions;

namespace Task2_TextEditor
{
    public class Punctuation : ISentenceItem
    {
        public Symbol Value { get; }
        public string Chars => Value.Chars;


        public Punctuation(string chars)
        {
            Value = new Symbol(chars);
        }
    }
}
