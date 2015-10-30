using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Task2_TextEditor
{
    public class Text
    {
        public ICollection<ISentence> Sentences { get; }

        public Text()
        {
            Sentences = new List<ISentence>();
        }
        public Text(IEnumerable<ISentence> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            Sentences = source.ToList();
        }

        public override string ToString()
        {
            return String.Join(" ", Sentences);
        }
        public static Text operator+(Text l, Text r)
        {
            var result = new Text();

            foreach (var s in l.Sentences)
                result.Sentences.Add(s);

            foreach (var s in r.Sentences)
                result.Sentences.Add(s);

            return result;
        }
    }
}
