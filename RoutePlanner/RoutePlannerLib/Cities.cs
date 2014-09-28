using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    class Cities
    {
        public int numberOfCities;

        public int ReadCities(string filename)
        {
            numberOfCities = 0;
            //read cities from file and safe in list
            numberOfCities++;

            return numberOfCities;
        }
    }
}
