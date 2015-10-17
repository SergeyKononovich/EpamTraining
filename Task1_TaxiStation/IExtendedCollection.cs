using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    public interface IExtendedCollection<T> : ICollection<T>, ICloneable { }
}
