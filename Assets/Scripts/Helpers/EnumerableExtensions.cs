using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class EnumerableExtensions
    {
        /// Returns a random element in the sequence. Will enumerate the sequence.
        public static T Random<T>(this IEnumerable<T> sequence)
        {
            T[] array = sequence.ToArray();

            if (array.Length == 0)
            {
                throw new InvalidOperationException("Sequence was empty.");
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }
    }
}
