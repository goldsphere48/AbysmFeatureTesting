using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AbysmFeatureTesting
{
    public class BackgroundLayer : IEnumerable<string>, IEnumerator<string>
    {
        public string Current => _sublocations[_current].Current;
        public Sublocation CurrentSublocation => _sublocations[_current];
        object IEnumerator.Current => _sublocations[_current].Current;

        private List<Sublocation> _sublocations;
        private int _current = -1;

        public BackgroundLayer(IEnumerable<Sublocation> sublocations)
        {
            _sublocations = new List<Sublocation>(sublocations);
        }

        public BackgroundLayer()
        {
            _sublocations = new List<Sublocation>();
        }

        public void Add(Sublocation location)
        {
            _sublocations.Add(location);
        }

        public bool MoveNext()
        {
            if (_current < 0) {
                ++_current;
                return _sublocations[_current].MoveNext();
            }
            if (!_sublocations[_current].MoveNext()) {
                ++_current;
                if (_current < _sublocations.Count) {
                    _sublocations[_current].MoveNext();
                    return true;
                }
                Reset();
                return false;
            }
            return true;
        }

        public void Reset()
        {
            _current = -1;
        }

        public void ResetCurrentSublocation()
        {
            if (_current < 0 || _current >= _sublocations.Count) {
                MoveNext();
			}
            _sublocations[_current].Reset();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
            Reset();
        }
    }
}