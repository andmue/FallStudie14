using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        private List<City> _cities = new List<City>();

        public City this[int index]
        {
            set
            {
                _cities.Add(value);
            }
            get
            {
                if(index > _cities.Count - 1 || index < 0)
                    return null;
                return _cities[index];
            }
        }

        public int Count
        {
            get { return _cities.Count; }
        }

        public int ReadCities(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var numberOfCities = 0;

            foreach (var line in lines)
            {
                var input = line.Split('\t');
                _cities.Add(new City(input[0], input[1], Convert.ToInt32(input[2]), Convert.ToDouble(input[3], CultureInfo.InvariantCulture), Convert.ToDouble(input[4], CultureInfo.InvariantCulture)));
                numberOfCities++;
            }

            return numberOfCities;
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            var neighbours = new List<City>();

            foreach (var city in _cities)
            {
                if(location.Distance(city.Location) <= distance)
                    neighbours.Add(city);
            }

            return neighbours;
        }
    }
}
