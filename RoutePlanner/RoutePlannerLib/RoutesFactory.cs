using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            var algorithmName = Properties.Settings.Default.RouteAlgorithm;
            return Create(cities, algorithmName);
        }
        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            IRoutes routeFactory = null;
            Assembly assem = Assembly.GetExecutingAssembly();
            Type t = assem.GetType(algorithmClassName);

            if(t == null)
            {
                return null;
            }
            
            routeFactory = (IRoutes)Activator.CreateInstance(t, cities);
            return routeFactory as IRoutes;
            
        }
    }
}
