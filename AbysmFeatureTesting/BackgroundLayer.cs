using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AbysmFeatureTesting
{
    public class BackgroundLayer : IEnumerable<string>, IEnumerator<string>
    {
        public Sublocation CurrentSublocation => _sublocations[_current];
        public string Current => CurrentSublocation.Current;
        public bool Looping { get; set; }
        object IEnumerator.Current => CurrentSublocation.Current;

        private List<Sublocation> _sublocations;
        private int _current = 0;

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
            if (!CurrentSublocation.MoveNext()) {
                if (_current + 1 == _sublocations.Count) {
                    if (!Looping) {
                        return false;
					}
                    Reset();
                    _current = -1;
                }
                _current++;
                CurrentSublocation.MoveNext();
            }
            return true;
        }

        public void Reset()
        {
            _current = 0;
            foreach (var sublocation in _sublocations) {
                sublocation.Reset();
			}
        }

        public void ResetCurrentSublocation()
        {
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