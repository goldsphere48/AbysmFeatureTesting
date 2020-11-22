using System;
using System.Collections.Generic;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.Pools
{
	public class RandomPool<T> : PoolContainer<T>
	{
		private readonly List<bool> _used;
		private readonly Random _random = new Random();
		private bool _finished = false;

		public RandomPool(List<IPoolNode<T>> data)
		{
			_used = new List<bool>(new bool[data.Count]);
			_poolData = data;
		}

		protected override int GetIndex()
		{
			var freeIndexes = _used.FindAllIndexes(x => !x);
			var count = freeIndexes.Length;
			if (count > 0)
			{
				var index = freeIndexes[_random.Next(freeIndexes.Length)];
				_used[index] = true;
				return index;
			}
			else
			{
				_finished = true;
				return -1;
			}
		}

		protected override bool CanMove()
		{
			return !_finished;
		}

		public override void Reset()
		{
			base.Reset();
			for (var i = 0; i < _used.Count; i++)
			{
				_used[i] = false;
			}

			_finished = false;
		}
	}
}
