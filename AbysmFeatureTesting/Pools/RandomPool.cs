using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbysmFeatureTesting.Pools
{
	class RandomPool<T> : PoolContainer<T>
	{
		private readonly List<IPoolNode<T>> _data;
		private bool[] _used;
		private Random _random = new Random();
		private bool _finished = false;

		public RandomPool(List<IPoolNode<T>> data)
		{
			_data = data;
			_used = new bool[data.Count];
			Initialize();
		}

		protected override List<IPoolNode<T>> Select()
		{
			return _data;
		}

		protected override int GetIndex()
		{
			var freeIndexes = new List<int>();
			for (var i = 0; i < _used.Length; i++)
			{
				if (!_used[i])
				{
					freeIndexes.Add(i);
				}
			}
			var count = freeIndexes.Count;
			if (count > 0)
			{
				var index = freeIndexes[_random.Next(freeIndexes.Count)];
				_used[index] = true;
				return index;
			}
			else
			{
				_finished = true;
				return -1;
			}
		}

		protected override bool IsEmpty()
		{
			return _finished;
		}

		public override void Reset()
		{
			base.Reset();
			for (var i = 0; i < _used.Length; i++)
			{
				_used[i] = false;
			}

			_finished = false;
		}
	}
}
