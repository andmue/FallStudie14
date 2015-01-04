using System;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class RoutePlannerConsoleApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutePlanner (Version {0})", Assembly.GetExecutingAssembly().GetName().Version);

            var wayPointWindisch = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            var wayPointChicago = new WayPoint("Chicago", 41.850033, -87.6500523);
            Console.WriteLine("{0}: {1}/{2}", wayPointWindisch.Name, wayPointWindisch.Latitude, wayPointWindisch.Longitude);
            Console.WriteLine(wayPointWindisch.ToString());
            Console.WriteLine("{0}: {1}/{2}", wayPointChicago.Name, wayPointChicago.Latitude, wayPointChicago.Longitude);
            Console.WriteLine(wayPointChicago.ToString());
            double distance = wayPointWindisch.Distance(wayPointChicago);
            Console.WriteLine("Distance from {0} to {1} is {2}", wayPointWindisch.Name, wayPointChicago.Name, distance);
            Console.ReadKey();
        }
    }
}
