using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(this IEnumerable<TValue> values, Int32 chunkSize)
        {
            using (var enumerator = values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return GetChunk(enumerator, chunkSize).ToList();
                }
            }
        }
        private static IEnumerable<T> GetChunk<T>(IEnumerator<T> enumerator, int chunkSize)
        {
            do
            {
                yield return enumerator.Current;
            } while (--chunkSize > 0 && enumerator.MoveNext());
        }
    }
}
