using System;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting
{
	static class DelegateExtension
	{
		public static void SafeInvoke(this Action action) 
		{
			action?.Invoke();
		}

		public static void SafeInvoke<T>(this Action<T> action, T param)
		{
			action?.Invoke(param);
		}

		public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
		{
			action?.Invoke(param1, param2);
		}
	}
}
