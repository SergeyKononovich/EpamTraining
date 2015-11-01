using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task2_TextEditor
{
    public class Text : IList<ITextItem>, ICloneable
    {
        private readonly IList<ITextItem> _textItems;

        public int Count => SentenceCount;
        public bool IsReadOnly => false;
        public int SentenceCount => _textItems.Count;
        public int CharsLength => ToString().Length;
        public ITextItem this[int index]
        {
            get { return _textItems[index]; }
            set { _textItems[index] = value; }
        }


        protected Text(Text clone)
            : this()
        {
            foreach (var item in clone._textItems)
                _textItems.Add(item.Clone() as ITextItem);
        }
        public Text()
        {
            _textItems = new List<ITextItem>();
        }
        public Text(IEnumerable<ITextItem> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            _textItems = source.ToList();
        }

        public IEnumerator<ITextItem> GetEnumerator()
        {
            return _textItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            return String.Join(" ", _textItems);
        }
        public object Clone()
        {
            return new Text(this);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Text p = (Text)obj;
            return _textItems.Equals(p._textItems);
        }
        protected bool Equals(Text other)
        {
            return Equals(_textItems, other._textItems);
        }
        public override int GetHashCode()
        {
            return _textItems?.GetHashCode() ?? 0;
        }
        public void Add(ITextItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _textItems.Add(item);
        }
        public void Clear()
        {
            _textItems.Clear();
        }
        public bool Contains(ITextItem item)
        {
            return _textItems.Contains(item);
        }
        public void CopyTo(ITextItem[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            _textItems.CopyTo(array, arrayIndex);
        }
        public bool Remove(ITextItem item)
        {
            return _textItems.Remove(item);
        }
        public int IndexOf(ITextItem item)
        {
            return _textItems.IndexOf(item);
        }
        public void Insert(int index, ITextItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _textItems.Insert(index, item);
        }
        public void Insert(int index, IEnumerable<ITextItem> items)
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
            _textItems.RemoveAt(index);
        }

        public static Text operator +(Text l, Text r)
        {
            if (l == null) throw new ArgumentNullException(nameof(l));
            if (r == null) throw new ArgumentNullException(nameof(r));

            return new Text(l._textItems.Concat(r._textItems));
        }
    }
}
