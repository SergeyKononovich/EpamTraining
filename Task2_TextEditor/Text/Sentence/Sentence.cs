using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2_TextEditor
{
    public class Sentence : ISentence
    {
        public ICollection<ISentenceItem> Items { get; }


        public Sentence()
        {
            Items = new List<ISentenceItem>();
        }
        public Sentence(IEnumerable<ISentenceItem> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            Items = source.ToList();
        }

        public override string ToString()
        {
            return String.Join("", Items);
        }
    }
}
