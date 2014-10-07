using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        private const int RADIUS = 6371;
        public string Name { get; set; } 
        public double Longitude { get; set; } 
        public double Latitude { get; set; }

        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name; 
            Latitude = _latitude; 
            Longitude = _longitude;
        }

        public override string ToString()
        {
            return "WayPoint:" + (Name == null ? "" : " " + Name) + " " + Math.Round(Latitude,2) + "/" + Math.Round(Longitude,2);
        }

        /// <summary>
        /// calculates the distance to targeted WayPoint
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <returns>distance</returns>
        public double Distance(WayPoint target)
        {
            var radLatitude = Math.PI * Latitude / 180;
            var radLongitude = Math.PI * Longitude / 180;
            var tarRadLatitude = Math.PI * target.Latitude / 180;
            var tarRadLongitude = Math.PI * target.Longitude / 180;

            return RADIUS * Math.Acos(Math.Sin(radLatitude) * Math.Sin(tarRadLatitude) + 
                          Math.Cos(radLatitude) * Math.Cos(tarRadLatitude) * Math.Cos(radLongitude - tarRadLongitude));
        }


        /// <summary>
        /// adds two WayPoints
        /// </summary>
        /// <param name="first">first WayPoint</param>
        /// <param name="second">second WayPoint</param>
        /// <returns>new WayPoint sum with same name as first Waypoint</returns>
        public static WayPoint operator+ (WayPoint first, WayPoint second)
        {
            WayPoint sum = new WayPoint(first.Name, first.Latitude + second.Latitude, first.Longitude + second.Longitude);
            return sum;
        }

        /// <summary>
        /// subtracts two WayPoints
        /// </summary>
        /// <param name="first">first WayPoint</param>
        /// <param name="second">second WayPoint</param>
        /// <returns>new WayPoint difference with same name as first Waypoint</returns>
        public static WayPoint operator- (WayPoint first, WayPoint second)
        {
            WayPoint difference = new WayPoint(first.Name, first.Latitude - second.Latitude, first.Longitude - second.Longitude);
            return difference;
        }
    }
}
