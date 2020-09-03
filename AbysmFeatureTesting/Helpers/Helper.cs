using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbysmFeatureTesting
{
	static class Helper
	{
		private static class EnumHelper<T>
		{
			public static readonly T[] Values = (T[])Enum.GetValues(typeof(T));
		}

		public static IEnumerable<T> GetEnumValues<T>() => EnumHelper<T>.Values;
	}
}
