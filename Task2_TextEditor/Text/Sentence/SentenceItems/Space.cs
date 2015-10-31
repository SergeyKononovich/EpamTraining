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
    }
}
