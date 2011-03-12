using System;
using System.Collections.Generic;

namespace Lazybones.Utils
{
	public static class IEnumerableExtensions
	{
		public static void Apply<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null) throw new ArgumentNullException("source");

			foreach (var item in source)
			{
				action(item);
			}
		}
	}
}