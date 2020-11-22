using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Pools
{
	public abstract class PoolContainer<T> : IPoolNode<T>, IEnumerable<IPoolNode<T>>, IEnumerator<IPoolNode<T>>
	{
		protected List<IPoolNode<T>> _poolData;
		private int _index;
		private IPoolNode<T> _current;

		public int Count => _poolData.Count;
		public T Value => _poolData[_index].Value;
		public IPoolNode<T> Current => _current;
		object? IEnumerator.Current => Current;

		protected PoolContainer()
		{
			_current = default;
			_index = 0;
		}

		public void Add(IPoolNode<T> item)
		{
			_poolData.Add(item);
		}

		public bool MoveNext()
		{
			_index = GetIndex();
			var move = CanMove();
			if (move)
			{
				_current = _poolData[_index];
			}
			else
			{
				Reset();
			}

			return move;
		}

		public IEnumerator<IPoolNode<T>> GetEnumerator()
		{
			while (MoveNext())
			{
				var item = Current;
				if (item is PoolContainer<T> pool) 
				{
					using var enumerator = pool.GetEnumerator();
					while (enumerator.MoveNext()) 
					{
						yield return enumerator.Current;
					}
				} 
				else 
				{
					yield return item;
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public virtual void Reset()
		{
			_index = 0;
		}

		public void Dispose()
		{
			
		}

		protected abstract int GetIndex();
		protected abstract bool CanMove();
	}
}
