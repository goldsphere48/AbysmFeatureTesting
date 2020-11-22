using System.Collections.Generic;

namespace Assets.Scripts.Pools
{
	public class LinearPool<T> : PoolContainer<T>
	{
		private int _currentIndex = -1;

		public LinearPool(List<IPoolNode<T>> data)
		{
			_poolData = data;
		}

		protected override int GetIndex()
		{
			return ++_currentIndex;
		}

		protected override bool CanMove()
		{
			return _currentIndex < Count;
		}

		public override void Reset()
		{
			base.Reset();
			_currentIndex = -1;
		}
	}
}
