using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        private const int Radius = 6371;
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
            return "WayPoint: " + (Name ?? string.Empty) + " " + Math.Round(Longitude,2) + " " + Math.Round(Latitude,2);
        }

        /// <summary>
        /// calculates the distance to targeted WayPoint
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <returns>distance</returns>
        public double Distance(WayPoint target)
        {
            double distance = Radius * Math.Acos(Math.Sin(Latitude)*Math.Sin(target.Latitude) +
                   Math.Cos(Latitude)*Math.Cos(target.Latitude)*Math.Cos(Longitude - target.Longitude));

            return distance;
        }
    }
}
