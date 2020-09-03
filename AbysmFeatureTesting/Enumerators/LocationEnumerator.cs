using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting
{
	class LocationEnumerator : IInfiniteEnumerator<string>
	{
		public event Action<Location> LocationEnd;

		public Dictionary<LayerName, LayerEnumerator> Backgrounds;
		private Location _location;
		
		public LocationEnumerator(Location location)
		{
			_location = location;

			Backgrounds = new Dictionary<LayerName, LayerEnumerator>();
			foreach (var layerName in Helper.GetEnumValues<LayerName>()) {
				Backgrounds[layerName] = _location.CreateBackgroundEnumerator(layerName);
			}

			Backgrounds[LayerName.Backgrounds].EnableLooping();
			Backgrounds[LayerName.Base].LocationEnd += OnLocationEnd;
		}

		private void OnLocationEnd(Location location)
		{
			LocationEnd.SafeInvoke(location);
		}

		public string Next(LayerName layerName)
		{
			if (!Backgrounds[layerName].MoveNext()) {
				Backgrounds[layerName].ResetCurrentSublocation();
				Backgrounds[layerName].MoveNext();
			}
			return Backgrounds[layerName].Current;
		}
	}
}
