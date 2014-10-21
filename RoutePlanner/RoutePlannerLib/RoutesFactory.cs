using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            return Create(cities, Properties.Settings.Default.RouteAlgorithm);
        }
        
        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            //TODO
            return null;
        }
    }
}
