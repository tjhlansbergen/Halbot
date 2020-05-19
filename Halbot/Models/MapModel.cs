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

        //constructor
        public MapModel(List<HalbotActivity> activities)
        {
            Activities = activities;

            //filter out activities without location
            Geos = new List<HalbotActivity>();
            foreach (var item in Activities)
            {
                if (item.Lat != 0 && item.Lng != 0)
                {
                    Geos.Add(item);
                }
            }
        }
    }
}
