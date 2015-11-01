namespace Task2_TextEditor
{
    public class Space : ISentenceItem
    {
        private const string SpaceChar = " ";

        public string Chars => SpaceChar;
        public int CharsLength => Chars.Length;


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
            if (obj == null || GetType() != obj.GetType())
                return false;

            Space p = (Space)obj;
            return Chars.Equals(p.Chars);
        }
        protected bool Equals(Word other)
        {
            return string.Equals(Chars, other.Chars);
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
