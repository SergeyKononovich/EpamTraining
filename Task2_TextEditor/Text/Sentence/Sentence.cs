using System;
using System.Collections.Generic;

namespace Task2_TextEditor
{
    public class Sentence : ISentence
    {
        public ICollection<ISentenceItem> Items { get; set; }


        public Sentence()
        {
            Items = new List<ISentenceItem>();
        }
        public Sentence(IEnumerable<ISentenceItem> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            Items = new List<ISentenceItem>(source);
        }
    }
}
