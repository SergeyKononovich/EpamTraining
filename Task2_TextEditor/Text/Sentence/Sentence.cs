using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Task2_TextEditor
{
    public class Sentence : ITextItem, IList<ISentenceItem>
    {
        private readonly IList<ISentenceItem> _sentenceItems;

        public int Count => SentenceItemsCount;
        public bool IsReadOnly => false;
        public int SentenceItemsCount => _sentenceItems.Count;
        public int CharsLength => ToString().Length;
        public ISentenceItem this[int index]
        {
            get { return _sentenceItems[index]; }
            set { _sentenceItems[index] = value; }
        }
        public bool IsInterrogative
        {
            get { return _sentenceItems[_sentenceItems.Count - 1].Chars.Contains('?'); }
        }
        public bool IsExclamatory
        {
            get { return _sentenceItems[_sentenceItems.Count - 1].Chars.Contains('!'); }
        }


        protected Sentence(Sentence clone)
            : this()
        {
            foreach (var item in clone._sentenceItems)
                _sentenceItems.Add(item.Clone() as ISentenceItem);
        }
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
        public object Clone()
        {
            return new Sentence(this);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Sentence p = (Sentence)obj;
            return _sentenceItems.Equals(p._sentenceItems);
        }
        protected bool Equals(Sentence other)
        {
            return Equals(_sentenceItems, other._sentenceItems);
        }
        public override int GetHashCode()
        {
            return _sentenceItems?.GetHashCode() ?? 0;
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
        public int IndexOf(ISentenceItem item)
        {
            return _sentenceItems.IndexOf(item);
        }
        public void Insert(int index, ISentenceItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _sentenceItems.Insert(index, item);
        }
        public void Insert(int index, IEnumerable<ISentenceItem> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                Insert(index, item);
                ++index;
            }
        }
        public void RemoveAt(int index)
        {
            _sentenceItems.RemoveAt(index);
        }

        public static Sentence operator +(Sentence l, Sentence r)
        {
            if (l == null) throw new ArgumentNullException(nameof(l));
            if (r == null) throw new ArgumentNullException(nameof(r));

            return new Sentence(l._sentenceItems.Concat(r._sentenceItems));
        }
    }
}
