using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Helpers
{
	public static class ListExtensions
	{
		public static int[] FindAllIndexes<T>(this List<T> source, Predicate<T> predicate)
		{
			return source.Select((b, i) => predicate(b) ? i : -1).Where(c => c != -1).ToArray();
		}
	}
}
