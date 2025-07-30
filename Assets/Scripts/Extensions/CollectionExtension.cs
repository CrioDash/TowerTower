using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class CollectionExtensions
    {
        public static T PickRandom<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                throw new System.InvalidOperationException("Cannot pick random from an empty list");
            return list[Random.Range(0, list.Count)];
        }
    }
}