using System;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting.Pools
{
	public interface IPoolNode<T>
	{
		T Value { get; }
	}
}
