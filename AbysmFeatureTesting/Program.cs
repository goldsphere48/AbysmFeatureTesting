using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AbysmFeatureTesting.Pools;
using AbysmFeatureTesting.Scroller;

namespace AbysmFeatureTesting
{
    internal static class Program
    {
	    private static void Main(string[] args)
	    {
		    var e = new LinearPool<int> (new List<IPoolNode<int>>
            {
				new RandomPool<int>(new List<IPoolNode<int>>
				{
					new Atom<int>(100),
					new Atom<int>(200),
					new Atom<int>(300),
				}),
				new Atom<int>(1),
				new Atom<int>(2),
				new Atom<int>(3),
				new RandomPool<int>(new List<IPoolNode<int>>
				{
					new Atom<int>(1),
					new LinearPool<int>(new List<IPoolNode<int>>
					{
						new Atom<int>(3),
						new Atom<int>(4),
						new Atom<int>(5),
					}),
					new Atom<int>(2),
					new Atom<int>(3),
					new LinearPool<int>(new List<IPoolNode<int>>
					{
						new Atom<int>(6),
						new Atom<int>(7),
						new LinearPool<int>(new List<IPoolNode<int>>
						{
							new Atom<int>(1),
							new Atom<int>(2),
							new Atom<int>(3),
						}),
						new Atom<int>(8),
					})
				})
			});

            var en = e.GetEnumerator();
            while (en.MoveNext())
            {
				Console.WriteLine(en.Current.Value);
            }
            

            Console.ReadKey();
        }
    }
}
