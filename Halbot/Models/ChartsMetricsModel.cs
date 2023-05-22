using System.Collections.Generic;
using System.Linq;
using Halbot.Charts;

namespace Halbot.Models
{
    public class ChartsMetricsModel
    {
        public List<HalbotActivity> Activities { get; }
        public SpiderChart SpiderChart { get; set; }

        public ChartsMetricsModel(List<HalbotActivity> activities)
        {
            //initialize data
            Activities = activities;

            // create chart
            var data = new DataSet("activities")
            {
                Items = activities.Select(a => new DataItem { Value = a.Effort, Date = a.Date }).ToList()
            };

            SpiderChart = new SpiderChart("Metrics", 900,10, data);
        }



    }
}
