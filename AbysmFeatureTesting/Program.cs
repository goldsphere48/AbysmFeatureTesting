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

            //location.SublocationChanged += OnSubLocationChanged;

            var location1 = new BackgroundLayer
            {
                new Sublocation(2) { "2_SubLoc1_1", "2_SubLoc1_2" },
                new Sublocation(2) { "2_SubLoc2_1", "2_SubLoc2_2" },
            };

            //location1.SublocationChanged += OnSubLocationChanged;

            var location2 = new BackgroundLayer
            {
                new Sublocation(2) { "3_SubLoc1_1", "3_SubLoc1_2" },
                new Sublocation(2) { "3_SubLoc2_1", "3_SubLoc2_2" },
            };

            

            

            while (location1.MoveNextBackground()) {
                Console.WriteLine(location1.CurrentBackground);
			}
        }
    }
}