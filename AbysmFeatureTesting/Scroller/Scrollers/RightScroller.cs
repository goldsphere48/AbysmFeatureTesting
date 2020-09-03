using System.Numerics;

namespace AbysmFeatureTesting.Scroller.Scrollers
{
    class RightScroller : IDirectionalScroller
    {
        public Vector2 Scroll()
        {
            return new Vector2(1, 0);
        }
    }
}