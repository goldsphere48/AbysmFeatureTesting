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
        private bool _infiniteLoop;

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
                CycleFinished?.Invoke();
                if (!_infiniteLoop)
                {
                    _cycles--;
                }
                if (_cycles == 0)
                {
                    _cycles = MaxCycles;
                    AllCyclesFinished?.Invoke();
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

        public void EnableLooping()
        {
            _infiniteLoop = true;
        }
        
        public void DisableLooping()
        {
            _infiniteLoop = false;
        }
    }
}