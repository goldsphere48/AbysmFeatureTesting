namespace Assets.Scripts.Pools
{
	public interface IPoolNode<out T>
	{
		T Value { get; }
	}
}
