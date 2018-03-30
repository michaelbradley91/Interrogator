using System;
using System.Collections.Generic;
using System.Linq;

namespace Interrogator.Helpers
{
    public static class EnumerableHelpers
    {
        public static IEnumerable<IList<T>> Permutations<T>(this List<T> elements)
        {
            if (elements.Count == 1)
            {
                yield return elements;
                yield break;
            }

            for (var i = 0; i < elements.Count; i++)
            {
                foreach (var permuation in elements.Take(i).Concat(elements.Skip(i + 1)).ToList().Permutations())
                {
                    var list = new List<T> {elements[i]};
                    list.AddRange(permuation);
                    yield return list;
                }
            }
        }
    }
}