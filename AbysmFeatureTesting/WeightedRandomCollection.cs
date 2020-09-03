using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AbysmFeatureTesting
{
    class WeightedRandomCollection<TItem> : IInfiniteEnumerator<TItem>
    {
        private Dictionary<TItem, int> _source = new Dictionary<TItem, int>();
        private Random _generator = new Random();
        private int _sum = 0;

        public void Add(TItem item, int weight)
        {
            _source.Add(item, weight);
            _sum += weight;
        }

        public TItem Next(LayerName layerName)
        {
            var n = 0;
            var num = _generator.Next(_sum);
            foreach (var item in _source)
            {
                n += item.Value;
                if (n > num)
                {
                    return item.Key;
                }
            }

            return default;
        }
    }
}