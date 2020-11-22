namespace Assets.Scripts.Pools
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
