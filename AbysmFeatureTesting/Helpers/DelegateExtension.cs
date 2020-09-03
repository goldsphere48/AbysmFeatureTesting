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
	}
}
