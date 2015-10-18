using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public class ExtendedList<T> : List<T>, IExtendedCollection<T>
    {
        public ExtendedList() { }
        public ExtendedList(int capacity) 
            : base(capacity) { }
        public ExtendedList(IEnumerable<T> collection) 
            : base(collection) { }
        private ExtendedList(ExtendedList<T> other)
            : this(other as IEnumerable<T>) { }


        public object Clone()
        {
            return new ExtendedList<T>(this);
        }
    }
}
