using System.Numerics;

namespace AbysmFeatureTesting.Scroller.Scrollers
{
    class UpScroller : IDirectionalScroller
    {
        public Vector2 Scroll()
        {
            return new Vector2(0, 1);
        }
    }
}