using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2_TextEditor
{
    public class Word : ISentenceItem, IEnumerable<Symbol>
    {
        //private string chars;

        private readonly Symbol[] _symbols;
        
        public int Length => _symbols.Length;
        public Symbol this[int index] => _symbols[index];
        public string Chars
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var s in _symbols)
                {
                    sb.Append(s.Chars);
                }
                return sb.ToString();
            }
        }


        public Word(string chars)
        {
            _symbols = chars.Select(x => new Symbol(x)).ToArray();
        }

        public IEnumerator<Symbol> GetEnumerator()
        {
            return _symbols.AsEnumerable().GetEnumerator();
        }
        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _symbols.GetEnumerator();
        }
        public override string ToString()
        {
            return Chars;
        }
    }
}
