using System;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting.Pools
{
	public class LinearPool<T> : PoolContainer<T>
	{
		private readonly List<IPoolNode<T>> _data;
		private int _currentIndex = -1;

		public LinearPool(List<IPoolNode<T>> data)
		{
			_data = data;
			Initialize();
		}

		protected override List<IPoolNode<T>> Select()
		{
			return _data;
		}

		protected override int GetIndex()
		{
			return ++_currentIndex;
		}

		protected override bool IsEmpty()
		{
			return _currentIndex >= Count;
		}

		public override void Reset()
		{
			base.Reset();
			_currentIndex = -1;
		}
	}
}
