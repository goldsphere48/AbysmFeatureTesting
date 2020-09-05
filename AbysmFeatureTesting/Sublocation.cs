using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AbysmFeatureTesting
{
    public class Sublocation : IEnumerable<string>, IEnumerator<string>
    {
        public event Action CycleFinished;
        public event Action AllCyclesFinished;

        public string Current => _items[_current];
        object IEnumerator.Current => _items[_current];
        public bool Looping { get; set; }
        public int Cycles
        {
            get => _cycles;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Cycles can't be negative");
                }
                _cycles = value;
            }
        }

        public readonly int MaxCycles;

        private List<string> _items;
        private int _current = -1;
        private int _cycles;

        public Sublocation(IEnumerable<string> items, int cycles)
        {
            _items = new List<string>(items);
            Cycles = MaxCycles = cycles;
        }

        public Sublocation()
        {
            _items = new List<string>();
        }

        public Sublocation(int cycles)
            : this()
        {
            Cycles = MaxCycles = cycles;
        }

        public void Add(string item)
        {
            _items.Add(item);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            _current++;
            if (_current == _items.Count)
            {
                _current = 0;
                if (!Looping) {
                    _cycles--;
                }
                CycleFinished.SafeInvoke();
                if (_cycles == 0)
                {
                    _cycles = MaxCycles;
                    AllCyclesFinished.SafeInvoke();
                    return false;
                }
            }
            return true;
        }

        public void Reset()
        {
            _current = -1;
            _cycles = MaxCycles;
        }

        public void Dispose()
        {
            Reset();
        }
    }
}