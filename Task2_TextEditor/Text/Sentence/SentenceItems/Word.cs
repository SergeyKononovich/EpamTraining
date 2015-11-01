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
        public object Clone()
        {
            return this;
        }
        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
                return false;

            Word p = (Word)obj;
            return Chars.Equals(p.Chars);
        }
        protected bool Equals(Word other)
        {
            return string.Equals(Chars, other.Chars);
        }
        public override int GetHashCode()
        {
            return Chars?.GetHashCode() ?? 0;
        }

        public static Word operator +(Word l, Word r)
        {
            if (l == null) throw new ArgumentNullException(nameof(l));
            if (r == null) throw new ArgumentNullException(nameof(r));

            return new Word(l.Chars + r.Chars);
        }
    }
}
