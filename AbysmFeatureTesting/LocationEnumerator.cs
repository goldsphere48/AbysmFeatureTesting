using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting
{
	class LocationEnumerator
	{
		public IReadOnlyDictionary<LayerName, BackgroundsEnumerator> BackgroundEnumerators => _backgroundEnumerators;
		public Location Current => _locations[_current];

		private List<Location> _locations;
		private Dictionary<LayerName, BackgroundsEnumerator> _backgroundEnumerators;
		private int _current  = 0;



		public LocationEnumerator(IEnumerable<Location> locations)
		{
			_locations = new List<Location>(locations);
			_backgroundEnumerators = new Dictionary<LayerName, BackgroundsEnumerator>();
			foreach (var layerName in Helper.GetEnumValues<LayerName>()) {
				_backgroundEnumerators[layerName] = new BackgroundsEnumerator(_locations, layerName);
			}
		}
	}
}
