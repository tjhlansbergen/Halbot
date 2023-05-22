using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Halbot.Models
{
    public class MapModel
    {
        public List<HalbotActivity> Activities { get; private set; }
        public List<HalbotActivity> Geos { get; set; }

        private readonly int _filterDecimals = 2;
        // decimal places  degrees distance
        //1	0.1	11.1 km
        //2	0.01	1.11 km
        //3	0.001	111 m
 

        //constructor
        public MapModel(List<HalbotActivity> activities)
        {
            Activities = activities;
            
            Geos = new List<HalbotActivity>();
            foreach (var item in Activities.OrderByDescending(a => a.Date))     // order lets recent activities overwrite older ones on the map
            {
                //filter out activities without location
                if (item.Lat == 0 || item.Lng == 0) continue;

                Geos.Add(item);

                // add, while preventing items close together
                if (!Geos.Any(i => Math.Round(i.Lat, _filterDecimals) == Math.Round(item.Lat, _filterDecimals) && Math.Round(i.Lng, _filterDecimals) == Math.Round(item.Lng, _filterDecimals)))
                {
                    //
                }
            }
        }
    }
}
