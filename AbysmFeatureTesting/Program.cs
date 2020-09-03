using System;
using System.Collections.Generic;
using System.Linq;
using AbysmFeatureTesting.Scroller;

namespace AbysmFeatureTesting
{
    internal static class Program
    {
        private static void OnSubLocationChanged(Sublocation subloc)
        {
            Console.WriteLine("Sub location changed");
        }
        
        private static void OnLocationChanged()
        {
            Console.WriteLine("Location changed");
        }
        
        private static void Main(string[] args)
        {
            
            var location = new BackgroundLayer
            {
                new Sublocation(2) { "1_SubLoc1_1", "1_SubLoc1_2" },
                new Sublocation(2) { "1_SubLoc2_1", "1_SubLoc2_2" },
            };

            location.SublocationChanged += OnSubLocationChanged;

            var location1 = new BackgroundLayer
            {
                new Sublocation(2) { "2_SubLoc1_1", "2_SubLoc1_2" },
                new Sublocation(2) { "2_SubLoc2_1", "2_SubLoc2_2" },
            };

            location1.SublocationChanged += OnSubLocationChanged;

            var location2 = new BackgroundLayer
            {
                new Sublocation(2) { "3_SubLoc1_1", "3_SubLoc1_2" },
                new Sublocation(2) { "3_SubLoc2_1", "3_SubLoc2_2" },
            };

            location2.SublocationChanged += OnSubLocationChanged;

            var loc = new Location();
            loc.SetLayer(LayerName.Base, location);
            loc.Entered += () => Console.WriteLine("Entered 1");
            loc.Exited += () => Console.WriteLine("Exited 1");

            var loc1 = new Location();
            loc1.SetLayer(LayerName.Base, location1);

            var loc2 = new Location();
            loc2.SetLayer(LayerName.Base, location2);

            LocationManager.Instance.Add(loc);
            LocationManager.Instance.Add(loc1);
            LocationManager.Instance.Add(loc2);

            /*var i = 0;
            foreach (var item in container) {
                Console.WriteLine(item);
                i++;
                if (i == 1) {
                    location.Add(new Sublocation(2) { "Additional_1_1", "Additional_1_2" });
				}
			}*/

            var scroller = new LocationScroller<string>
            {
                Speed = 300f,
                Enumerator = LocationManager.Instance.CreateBackgroundEnumerator(LayerName.Base),
                Direction = ScrollDirection.Up
            };

            while (true)
            {
                scroller.Update();
            }
        }
    }
}