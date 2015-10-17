using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_TaxiStation
{
    static class GenericCollectionsExtensionMethods
    {
        public static void InitWith<TItem>(this ICollection<TItem> collection,
            IEnumerable<TItem> collectionForClone)
        {
            collection.Clear();

            foreach (var item in collectionForClone)
            {
                collection.Add(item);
            }
        }
    }
}
