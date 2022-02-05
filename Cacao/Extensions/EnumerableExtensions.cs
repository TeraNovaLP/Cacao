/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Cacao.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<TSource>(this IEnumerable<TSource> entries, Action<TSource> action)
        {
            foreach (var entry in entries)
            {
                action(entry);
            }
        }

        public static IEnumerable<TSource> RemoveDuplicatesBy<TSource, TKey>(this IEnumerable<TSource> entries, Func<TSource, TKey> keySelector)
        {
            return entries
                .GroupBy(keySelector)
                .Select(x => x.First());
        }

        /// <summary>
        /// Batches the sequence into chunks of the given size.
        /// </summary>
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> entries, int batchSize)
        {
            var batchCount = Math.DivRem(entries.Count(), batchSize, out var remain);

            if (remain > 0)
            {
                batchCount++;
            }

            var batches = new List<IEnumerable<TSource>>();

            for (var i = 0; i < batchCount; i++)
            {
                batches.Add(entries.Skip(batchSize * i).Take(batchSize));
            }

            return batches;
        }

        /// <summary>
        /// Checks if all the given items are included.
        /// </summary>
        public static bool ContainsMany<T>(this IEnumerable<T> items1, IEnumerable<T> items2)
        {
            return items2.All(items1.Contains);
        }

        /// <summary>
        /// Checks if all the given items are included.
        /// </summary>
        public static bool ContainsMany(this IEnumerable<string> items1, IEnumerable<string> items2, StringComparison comparison)
        {
            return items2.All(x => items1.Any(y => y.Equals(x, comparison)));
        }


        /// <summary>
        /// Transforms the elements of a sequence and filters it afterwards.
        /// </summary>
        /// <param name="selector">Property selector</param>
        /// <param name="filter">Filter condition</param>
        public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> items, Func<TSource, TResult> selector, Func<TResult, bool> filter)
        {
            foreach (var item in items)
            {
                var value = selector(item);

                if (filter(value))
                {
                    yield return value;
                }
            }
        }
    }
}
