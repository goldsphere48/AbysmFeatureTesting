using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

            var layer = new BackgroundLayer
            {
                new Sublocation(2) { "1_SubLoc1_1", "1_SubLoc1_2" },
                new Sublocation(2) { "1_SubLoc2_1", "1_SubLoc2_2" },
            };

            var layer1 = new BackgroundLayer
            {
                new Sublocation(2) { "2_SubLoc1_1", "2_SubLoc1_2" },
                new Sublocation(2) { "2_SubLoc2_1", "2_SubLoc2_2" },
            };

            var layer2 = new BackgroundLayer
            {
                new Sublocation(2) { "3_SubLoc1_1", "3_SubLoc1_2" },
                new Sublocation(2) { "3_SubLoc2_1", "3_SubLoc2_2" },
            };

            var sub = new Sublocation(2) { "3_SubLoc1_1", "3_SubLoc1_2" };
            var i = 0;
            layer.Looping = true;
            foreach(var item in layer) {
                i++;
                if (i == 40) {
                    layer.Looping = false;
				}
                Console.WriteLine(item);
            }
        }
    }
}
