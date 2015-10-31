using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task2_TextEditor
{
    public class Text : ICollection<ITextItem>
    {
        private readonly ICollection<ITextItem> _sentences;

        public int Count => SentenceCount;
        public bool IsReadOnly => false;
        public int SentenceCount => _sentences.Count;
        public int CharsLength => ToString().Length;


        public Text()
        {
            _sentences = new List<ITextItem>();
        }
        public Text(IEnumerable<ITextItem> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            _sentences = source.ToList();
        }

        public IEnumerator<ITextItem> GetEnumerator()
        {
            return _sentences.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            return String.Join(" ", _sentences);
        }
        public static Text operator+(Text l, Text r)
        {
            if (l == null) throw new ArgumentNullException(nameof(l));
            if (r == null) throw new ArgumentNullException(nameof(r));

            return new Text(l._sentences.Concat(r._sentences));
        }
        public void Add(ITextItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _sentences.Add(item);
        }
        public void Clear()
        {
            _sentences.Clear();
        }
        public bool Contains(ITextItem item)
        {
            return _sentences.Contains(item);
        }
        public void CopyTo(ITextItem[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            _sentences.CopyTo(array, arrayIndex);
        }
        public bool Remove(ITextItem item)
        {
            return _sentences.Remove(item);
        }
    }
}
