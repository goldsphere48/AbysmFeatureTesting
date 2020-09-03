using System.Numerics;

namespace AbysmFeatureTesting.Scroller.Scrollers
{
           
    class DownScroller : IDirectionalScroller
    {
        public Vector2 Scroll()
        {
            return new Vector2(0, -1);
        }
    }
}