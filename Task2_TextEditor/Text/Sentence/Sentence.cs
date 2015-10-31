using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task2_TextEditor
{
    public class Sentence : ITextItem, ICollection<ISentenceItem>
    {
        private readonly ICollection<ISentenceItem> _sentenceItems;

        public int Count => SentenceItemsCount;
        public bool IsReadOnly => false;
        public int SentenceItemsCount => _sentenceItems.Count;
        public int CharsLength => ToString().Length;


        public Sentence()
        {
            _sentenceItems = new List<ISentenceItem>();
        }
        public Sentence(IEnumerable<ISentenceItem> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            _sentenceItems = source.ToList();
        }

        public IEnumerator<ISentenceItem> GetEnumerator()
        {
            return _sentenceItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            return String.Join("", _sentenceItems);
        }
        public void Add(ISentenceItem item)
        {
            _sentenceItems.Add(item);
        }
        public void Clear()
        {
            _sentenceItems.Clear();
        }
        public bool Contains(ISentenceItem item)
        {
            return _sentenceItems.Contains(item);
        }
        public void CopyTo(ISentenceItem[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            _sentenceItems.CopyTo(array, arrayIndex);
        }
        public bool Remove(ISentenceItem item)
        {
            return _sentenceItems.Remove(item);
        }
    }
}
