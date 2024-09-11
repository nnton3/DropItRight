using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Extenstions
{
	public static class CollectionExtensions
    {
        private static Random _random = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            var list = collection.ToList();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = _random.Next(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            return list;
        }
    }
}
