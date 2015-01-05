using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;
using System.Diagnostics;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        private List<City> _cities = new List<City>();

        private static TraceSource logger = new TraceSource("Cities");

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
            logger.TraceEvent(TraceEventType.Information, 1, "ReadCities started");

            var numberOfCitiesOld = _cities.Count;
            var numberOfCities = 0;

            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    var citiesAsStrings = reader.GetSplittedLines('\t');
                    // Old Version Lab 4
                    foreach (var cs in citiesAsStrings)
                    {
                        _cities.Add(new City(cs[0].Trim(), cs[1].Trim(), int.Parse(cs[2], CultureInfo.InvariantCulture), double.Parse(cs[3], CultureInfo.InvariantCulture), double.Parse(cs[4], CultureInfo.InvariantCulture)));
                        numberOfCities++;
                    }
                    return numberOfCities;

                    _cities.AddRange(from cs in citiesAsStrings
                                     select new City(cs[0].Trim(),
                                         cs[1].Trim(),
                                         int.Parse(cs[2], CultureInfo.InvariantCulture),
                                         double.Parse(cs[3], CultureInfo.InvariantCulture),
                                         double.Parse(cs[4], CultureInfo.InvariantCulture)));

                    numberOfCities = (citiesAsStrings.Count() - numberOfCitiesOld);
                }
                return numberOfCities;
            }
            catch (Exception e)
            {
                if (isCritical(e))
                {
                    throw;
                }
                Console.WriteLine("File: \"" + filename + "\" could not be found");
                Console.WriteLine(e.Message);
                logger.TraceEvent(TraceEventType.Critical, 1, e.StackTrace);
                return -1;
            }
            finally
            {
                logger.TraceEvent(TraceEventType.Information, 1, "ReadCities ended");
            }
        }

        private bool isCritical(Exception e)
        {
            if (e is FileNotFoundException)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            // Old Version Lab 4
            //var neighbours = new List<City>();

            //foreach (var city in _cities)
            //{
            //    if(location.Distance(city.Location) <= distance)
            //        neighbours.Add(city);
            //}

            return _cities.Where(c => location.Distance(c.Location) <= distance).ToList();
        }


        public City FindCity(string cityName)
        {
            Predicate<City> predicate = delegate(City city)
            {
                return city.Name.Trim().ToLower().Equals(cityName.Trim().ToLower(), StringComparison.InvariantCultureIgnoreCase);
                };
            
            // Same in Lambda
            Predicate<City> p = c => c.Name.Trim().ToLower().Equals(cityName.Trim().ToLower());
            
            // Lambda inline
            //return FindCity(c => c.Name.Trim().ToLower().Equals(cityName.Trim().ToLower()));

            return FindCity(p);
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

        private List<City> InitIndexForAlgorithm(List<City> foundCities)
        {
            //set index for FloydWarshall
            for (int index = 0; index < foundCities.Count; index++)
            {
                foundCities[index].Index = index;
            }
            return foundCities;
        }

    }
}
