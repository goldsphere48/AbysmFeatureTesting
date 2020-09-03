using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbysmFeatureTesting
{
    public class BackgroundsEnumerator : IEnumerable<string>, IEnumerator<string>,  IInfiniteEnumerator<string>
    {
        private readonly List<Location> _locations = new List<Location>();
        private Location _currentLocation;
        private Sublocation _currentSublocation;
        private int _current = -1;
        private LayerName _layerName;

        private BackgroundLayer CurrentLayer => _currentLocation.BackgroundLayers[_layerName];

        public BackgroundsEnumerator(LayerName layerName)
        {
            _layerName = layerName;
        }

        public BackgroundsEnumerator(IEnumerable<Location> collections, LayerName layer)
        {
            _locations = new List<Location>(collections);
            _layerName = layer;
        }

        public string Current => _currentSublocation.Current;

        object IEnumerator.Current => _currentSublocation.Current;

        public void Add(Location location)
        {
            _locations.Add(location);
        }

        public string Next()
        {
            if (!MoveNext()) {
                _current = _locations.Count - 1;
                ResetCurrentSublocation();
                MoveNext();
            }
            return Current;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (_current < 0)
            {
                _current++;
                NextLocation();
                return NextSublocation();
            }
            if (!_currentSublocation.MoveNext()) {
				if (!CurrentLayer.MoveNext()) {
                    _currentLocation.Exited.SafeInvoke();
                    _current++;
                    if (_current < _locations.Count) {
                        NextLocation();
                    } else {
                        return false;
					}
                }
                return NextSublocation();
            }
            return true;
        }

        public void Reset()
        {
            foreach (var collection in _locations)
            {
                collection.BackgroundLayers[_layerName].Reset();
            }
            _current = -1;
        }

        public void ResetCurrentSublocation()
        {
            CurrentLayer.ResetCurrentSublocation();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Reset();
        }

        private bool NextLocation()
		{
            _currentLocation = _locations[_current];
            _currentLocation.Entered.SafeInvoke();
            return CurrentLayer.MoveNext();
        }

        private bool NextSublocation()
		{
            _currentSublocation = CurrentLayer.Current;
            return _currentSublocation.MoveNext();
        }
    }
}