using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_TextEditor
{
    public class Space : ISentenceItem
    {
        public Symbol Value { get; } = new Symbol(" ");
        public string Chars => Value.Chars;

        public override string ToString()
        {
            return Chars;
        }
    }
}
