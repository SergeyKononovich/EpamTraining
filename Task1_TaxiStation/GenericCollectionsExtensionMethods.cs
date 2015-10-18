using System.Collections.Generic;

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
