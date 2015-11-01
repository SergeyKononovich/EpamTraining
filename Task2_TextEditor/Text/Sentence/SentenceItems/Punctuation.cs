using System;

namespace Task2_TextEditor
{
    public class Punctuation : ISentenceItem
    {
        public string Chars { get; }
        public bool IsSentencesSeparator { get; }
        public bool IsSyntacticConstructionsSeparator { get; }
        public int CharsLength => Chars.Length;


        protected Punctuation(Punctuation clone)
        {
            Chars = clone.Chars;
            IsSentencesSeparator = clone.IsSentencesSeparator;
            IsSyntacticConstructionsSeparator = clone.IsSyntacticConstructionsSeparator;
        }
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
        public object Clone()
        {
            return new Punctuation(this);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Punctuation p = (Punctuation)obj;
            return Chars.Equals(p.Chars) && 
                IsSentencesSeparator == p.IsSentencesSeparator &&
                IsSyntacticConstructionsSeparator == p.IsSyntacticConstructionsSeparator;
        }
        protected bool Equals(Punctuation other)
        {
            return string.Equals(Chars, other.Chars) && 
                IsSentencesSeparator == other.IsSentencesSeparator && 
                IsSyntacticConstructionsSeparator == other.IsSyntacticConstructionsSeparator;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Chars != null ? Chars.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ IsSentencesSeparator.GetHashCode();
                hashCode = (hashCode*397) ^ IsSyntacticConstructionsSeparator.GetHashCode();
                return hashCode;
            }
        }
    }
}
