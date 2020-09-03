using System;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting
{
	public class LocationManager
	{
		public static LocationManager Instance =>
			_instance ?? (_instance = new LocationManager());
		private static LocationManager _instance;

		private List<Location> _locations = new List<Location>();

		public LocationManager()
		{

		}

		public LocationManager(IEnumerable<Location> locations)
		{
			_locations = new List<Location>(locations);
		}

		public void Add(Location location)
		{
			_locations.Add(location);
		}

		public void SetNextLocatrion(Location location)
		{

		}
	}
}
