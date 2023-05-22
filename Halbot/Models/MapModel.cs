using System.Collections.Generic;
using System.Linq;

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
            Geos = Activities.OrderByDescending(a => a.Date)
                .Where(a => a.Lat != 0 && a.Lng != 0)
                .ToList();
        }
    }
}
