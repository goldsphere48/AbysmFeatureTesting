using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting.Pools
{
	public abstract class PoolContainer<T> : IPoolNode<T>, IEnumerable<IPoolNode<T>>, IEnumerator<IPoolNode<T>>
	{
		private List<IPoolNode<T>> _poolData;
		private int _currentIndex = -1;

		public int Count => _poolData.Count;

		public T Value => _poolData[_currentIndex].Value;

		object? IEnumerator.Current => Current;

		public IPoolNode<T> Current => _poolData[_currentIndex];
		

		protected void Initialize()
		{
			_poolData = Select();
		}

		public void Add(IPoolNode<T> item)
		{
			_poolData.Add(item);
		}

		public bool MoveNext()
		{
			_currentIndex = GetIndex();
			return !IsEmpty();
		}

		public IEnumerator<IPoolNode<T>> GetEnumerator()
		{
			while (MoveNext())
			{
				var item = Current;
				if (item is PoolContainer<T> pool) {
					using var enumerator = pool.GetEnumerator();
					while (enumerator.MoveNext()) {
						yield return enumerator.Current;
					}
				} else {
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
			_currentIndex = -1;
		}

		public void Dispose()
		{
			
		}

		protected abstract List<IPoolNode<T>> Select();
		protected abstract int GetIndex();
		protected abstract bool IsEmpty();
	}
}
