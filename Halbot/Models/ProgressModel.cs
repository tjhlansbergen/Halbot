using System.Collections.Generic;
using System.Linq;
using Halbot.Code;
using Halbot.Code.Charts;

namespace Halbot.Models
{
    public class ProgressModel
    {
        //properties
        public List<HalbotActivity> Activities { get; private set; }
        public ColumnChart LastXActivities { get; private set; }

        //constructor
        public ProgressModel(List<HalbotActivity> dbactivities)
        {
            //initialize data
            Activities = dbactivities;

            // init charts
            LastXActivities = _lastXActivities(15);
        }

        // last 15
        private ColumnChart _lastXActivities(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("lastactivities", 200);
            var runs = Activities.OrderByDescending(a => a.Date).Reverse().Take(x).ToList();

            ColumnChart.DataSet volume = new ColumnChart.DataSet("volume");
            ColumnChart.DataSet pace = new ColumnChart.DataSet("pace");
            ColumnChart.DataSet climb = new ColumnChart.DataSet("climb");

            foreach (var run in runs)
            {
                volume.Add(run.Date.ToString("dd-MM"), string.Format("{0:0.00}km", (run.Distance / 1000)),
                    run.Distance / 1000);
                pace.Add(string.Empty, run.Pace, run.Speed);
                climb.Add(string.Empty, string.Format("{0}m", run.Climb), run.Climb);
            }

            chart.AddDataSet(volume);
            chart.AddDataSet(pace);
            chart.AddDataSet(climb);

            return chart;
        }
    }
}