using System.Numerics;

namespace AbysmFeatureTesting.Scroller.Scrollers
{
    class LeftScroller : IDirectionalScroller
    {
        public Vector2 Scroll()
        {
            return new Vector2(-1, 0);
        }
    }
}