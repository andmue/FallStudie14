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
            var numberOfCities = 0;
            using (TextReader reader = new StreamReader(filename))
            {
                IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');
                foreach (var cs in citiesAsStrings)
                {
                    _cities.Add(new City(cs[0].Trim(), cs[1].Trim(), int.Parse(cs[2]), double.Parse(cs[3]), double.Parse(cs[4])));
                    numberOfCities++;
                }
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


        public City FindCity(string cityName)
        {
            Predicate<City> predicate = delegate(City city)
            {
                return city.Name.Trim().ToLower().Equals(cityName.Trim().ToLower());
            };
            return FindCity(predicate);
        }

        public City FindCity(Predicate<City> predicate)
        {
            return _cities.Find(predicate);
        }

        #region Lab04: FindShortestPath helper function
        /// <summary>
        /// Find all cities between 2 cities 
        /// </summary>
        /// <param name="from">source city</param>
        /// <param name="to">target city</param>
        /// <returns>list of cities</returns>
        public List<City> FindCitiesBetween(City from, City to)
        {
            var foundCities = new List<City>();
            if (from == null || to == null)
                return foundCities;

            foundCities.Add(from);

            var minLat = Math.Min(from.Location.Latitude, to.Location.Latitude);
            var maxLat = Math.Max(from.Location.Latitude, to.Location.Latitude);
            var minLon = Math.Min(from.Location.Longitude, to.Location.Longitude);
            var maxLon = Math.Max(from.Location.Longitude, to.Location.Longitude);

            // renames the name of the "cities" variable to your name of the internal City-List
            foundCities.AddRange(_cities.FindAll(c =>
                c.Location.Latitude > minLat && c.Location.Latitude < maxLat
                        && c.Location.Longitude > minLon && c.Location.Longitude < maxLon));

            foundCities.Add(to);
            return foundCities;
        }
        #endregion
    }
}
