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

        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> entries, int batchSize)
        {
            var batchCount = Math.DivRem(entries.Count(), batchSize, out var remain);

            if (remain > 0)
            {
                batchCount++;
            }

            var batches = new List<IEnumerable<TSource>>();

            int rounds = 0;

            for (int i = 0; i < batchCount; i++)
            {
                rounds = rounds + 1;
                batches.Add(entries.Skip(batchSize * i).Take(batchSize));
            }

            return batches;
        }
    }
}
