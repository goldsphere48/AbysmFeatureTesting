using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbysmFeatureTesting
{
    public class LayerEnumerator : IEnumerable<string>, IEnumerator<string>
    {
        public event Action<Location> LocationEnd;
        public event Action<Location, Sublocation> SublocationChanged;

        private Location _location;
        private Sublocation _currentSublocation;
        private LayerName _layerName;
        private bool _initFirst = false;

        public BackgroundLayer Layer => _location.BackgroundLayers[_layerName];

        public LayerEnumerator(LayerName layerName)
        {
            _layerName = layerName;
        }

        public LayerEnumerator(Location location, LayerName layer)
        {
            _location = location;
            _layerName = layer;
        }

        public string Current => _currentSublocation.Current;

        object IEnumerator.Current => _currentSublocation.Current;

        public IEnumerator<string> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (!_initFirst) {
                _initFirst = true;
                //location entered
                return MoveNextSublocation();
			}
            if (!_currentSublocation.MoveNext()) {
                if (!MoveNextSublocation()) {
                    OnLocationEnd();
                    Reset();
                    return false;
				}
                OnSublocationChanged();
            }
            return true;
        }

        public bool MoveNextSublocation()
		{
            if (Layer.MoveNext()) {
                _currentSublocation = Layer.Current;
                return _currentSublocation.MoveNext();
            }
            return false;
        }

        public void Reset()
        {
            _initFirst = false;
            _location.BackgroundLayers[_layerName].Reset();
        }

        public void ResetCurrentSublocation()
        {
            Layer.ResetCurrentSublocation();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Reset();
        }

        public void EnableLooping()
		{
            _currentSublocation.EnableLooping();
		}

        public void DisableLooping()
        {
            _currentSublocation.DisableLooping();
        }

        private void OnSublocationChanged()
		{
            SublocationChanged.SafeInvoke(_location, _currentSublocation);
		}

        private void OnLocationEnd()
		{
            LocationEnd.SafeInvoke(_location);
		}
	}
}