using System;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting.Pools
{
	public class Atom<T> : IPoolNode<T>
	{
		public T Value { get; }

		public Atom(T value)
		{
			Value = value;
		}
	}
}
