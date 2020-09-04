using System;
using System.Collections.Generic;
using System.Numerics;
using AbysmFeatureTesting.Scroller.Scrollers;

namespace AbysmFeatureTesting.Scroller
{
    class LocationScroller<TItem>
    {
        private static readonly Dictionary<ScrollDirection, IDirectionalScroller> _scrollers;
        
        static LocationScroller()
        {
            _scrollers = new Dictionary<ScrollDirection, IDirectionalScroller>
            {
                {ScrollDirection.Down, new DownScroller()},
                {ScrollDirection.Up, new UpScroller()},
                {ScrollDirection.Left, new LeftScroller()},
                {ScrollDirection.Right, new RightScroller()}
            };
        }
        
        private double _currentState = 0;
        private double _backgroundLength = 30;
        private DateTime _oldTime = DateTime.Now;
        private IDirectionalScroller _scroller;
        private LayerName _layerName;

        public float Speed { get; set; }
        public IInfiniteEnumerator<TItem> Enumerator { private get; set; }
        public ScrollDirection Direction
        {
            set => _scroller = _scrollers[value];
        }

        public LocationScroller(LayerName layerName)
        {
            Direction = ScrollDirection.Up;
            _layerName = layerName;
        }
        
        public void Update()
        {
            var seconds = (DateTime.Now - _oldTime).TotalSeconds;
            _currentState += seconds * Speed * _scroller.Scroll().Y;
            if (_currentState >= _backgroundLength)
            {
                Console.WriteLine("Up");
                _currentState = 0;
                Next();
            }
            else if (_currentState < -_backgroundLength)
            {
                Console.WriteLine("Down");
                _currentState = 0;
                Next();    
            }

            _oldTime = DateTime.Now;
        }

        private void Next()
        {
            Console.WriteLine(Enumerator.Next());
        }
    }
}