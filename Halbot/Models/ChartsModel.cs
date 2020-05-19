using System.Collections.Generic;
using System.Linq;
using Halbot.Code.Charts;

namespace Halbot.Models
{
    public class ChartsModel
    {
        //properties
        public List<HalbotActivity> Activities { get;}
        public ColumnChart LastXActivities { get;}
        public ColumnChart LastXClimbPace { get;}

        //constructor
        public ChartsModel(List<HalbotActivity> activities)
        {
            //initialize data
            Activities = activities;

            // init charts
            LastXActivities = GetLastXActivities(14);
            LastXClimbPace = GetLastXClimbPace(14);

        }

        private ColumnChart GetLastXActivities(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("lastactivities", 200);
            var runs = Activities.OrderByDescending(a => a.Date).Reverse().Take(x).ToList();

            ColumnChart.DataSet volume = new ColumnChart.DataSet("volume");

            foreach (var run in runs)
            {
                volume.Add(run.Date.ToString("dd-MM"), $"{(run.Distance / 1000):0.00}", run.Distance / 1000);
            }

            chart.AddDataSet(volume);
            return chart;
        }

        private ColumnChart GetLastXClimbPace(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("lastclimbpace", 200);
            var runs = Activities.OrderByDescending(a => a.Date).Reverse().Take(x).ToList();

            ColumnChart.DataSet pace = new ColumnChart.DataSet("pace");

            foreach (var run in runs)
            {
                pace.Add(run.Date.ToString("dd-MM"), run.Pace, run.Speed * run.Speed * run.Speed * run.Speed);
            }

            chart.AddDataSet(pace);
            return chart;
        }
    }
}