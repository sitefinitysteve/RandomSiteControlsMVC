using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Telerik.Sitefinity
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Shuffle the Enumerable around in a random order
        /// 🔥 From SitefinitySteve, from StackOverflow 
        /// </summary>
        /// <returns>IEnumerable<T></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                int swapIndex = rng.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }

        /// <summary>
        /// Shuffle the Enumerable around in a random order, Better implimentation
        /// 🔥 From SitefinitySteve, from StackOverflow 
        /// </summary>
        /// <returns>void</returns>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeShuffuleRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static class ThreadSafeShuffuleRandom
        {
            [ThreadStatic]
            private static Random Local;

            public static Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }
    }
}