using System;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting
{
	public class Location
	{
		public Action Entered;
		public Action Exited;
		public IReadOnlyDictionary<LayerName, BackgroundLayer> BackgroundLayers => _backgroundLayers;

		private Dictionary<LayerName, BackgroundLayer> _backgroundLayers = new Dictionary<LayerName, BackgroundLayer>();

		public void SetLayer(LayerName layerName, BackgroundLayer layer)
		{
			_backgroundLayers[layerName] = layer;
		}
	}
}
