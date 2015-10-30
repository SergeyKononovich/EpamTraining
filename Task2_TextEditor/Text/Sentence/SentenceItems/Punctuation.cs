using System.Linq.Expressions;

namespace Task2_TextEditor
{
    public class Punctuation : ISentenceItem
    {
        public Symbol Value { get; }
        public string Chars => Value.Chars;
        public bool IsSentencesSeparator { get; }
        public bool IsSyntacticConstructionsSeparator { get; }


        public Punctuation(string chars, bool isSyntacticConstructionsSeparator, bool isSentencesSeparator)
        {
            Value = new Symbol(chars);
            IsSyntacticConstructionsSeparator = isSyntacticConstructionsSeparator;
            IsSentencesSeparator = isSentencesSeparator;
        }

        public override string ToString()
        {
            return Chars;
        }
    }
}
