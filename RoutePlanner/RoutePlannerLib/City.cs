﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class City
    {
        public string Name { get; set; }
        public string Country { get; set;}
        public int Population { get; set; }
        public WayPoint Location { get; set; }

        [XmlIgnore]
        public int Index { get; set; }

        public City(){ }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_country"></param>
        /// <param name="_population"></param>
        /// <param name="_latitude"></param>
        /// <param name="_longitude"></param>
        public City(string _name, string _country, int _population, double _latitude, double _longitude)
        {
            Name = _name;
            Country = _country;
            Population = _population;
            Location = new WayPoint(_name,_latitude, _longitude);
        }

        public override bool Equals(object obj)
        {
            City cityObj = obj as City;
            if (cityObj == null)
            {
                return false;
            }
            else if(this.Name.Equals(((City)obj).Name) && this.Country.Equals(((City)obj).Country))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
