using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    class RouteRequestWatcher
    {
        public Dictionary <string, int> CityRequests = new Dictionary <string, int>();

        /// <summary>
        /// observer: increases number of requests for city
        /// if city hasn't been requested before, new entry in CityRequests is made
        /// </summary>
        public void LogRouteRequests(object sender, RouteRequestEventArgs args)
        {
            string toCity = args.ToCity.Name;
            if (CityRequests.ContainsKey(toCity))
            {
                CityRequests[toCity] += 1;
            }
            else
            {
                CityRequests.Add(args.toCity.Name, 1);
            }
            Console.WriteLine();
            Console.WriteLine("Current Request State");
            Console.WriteLine("---------------------");
            foreach (string city in CityRequests.Keys)
            {
                Console.WriteLine("ToCity: {0} has been requested {1} times.");
            }
        }

        /// <summary>
        /// returns number of requests for selected city 
        /// </summary>
        /// <param name="city">requested city</param>
        /// <returns>number of requests for selected city</returns>
        public int GetCityRequest(string city)
        {
            try
            {
                return CityRequests[city];
            }
            catch
            {
                return 0;
            }
        }
    }
}
