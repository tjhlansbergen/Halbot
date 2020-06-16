using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halbot.Charts;

namespace Halbot.Models
{
    public class ChartsWorkloadModel
    {
        public List<HalbotActivity> Activities { get; }
        public CircleChart CircleChart { get; set; }

        public ChartsWorkloadModel(List<HalbotActivity> activities)
        {
            //initialize data
            Activities = activities;
            var now = DateTime.Now;

            // create chart
            CircleChart = new CircleChart("TestCircle", 900, 600);

            // create dataset
            var data1 = new CircleChart.DataSet("dataset", 120, 160, 250, 450);     // heartrate bounds: 110 - 190, speed bounds 2(m/s) - 5(m/s)

            // previous weeks
            var previousWeeksSelection = Activities.Where(a => a.Date.Year == now.Year).Where(b => b.Week < HalbotActivity.WeekOfYear(now) - 1);
            foreach (var activity in previousWeeksSelection)
            {
                data1.Add(Convert.ToInt32(activity.Heartrate == 0 ? 140 : activity.Heartrate), Convert.ToInt32(activity.Speed * 100), Convert.ToInt32(activity.Distance * 0.002), "#cccccc");
            }

            // add last week
            var lastWeekSelection = Activities.Where(a => a.Date.Year == now.Year).Where(b => b.Week == HalbotActivity.WeekOfYear(now) - 1);
            foreach (var activity in lastWeekSelection)
            {
                data1.Add(Convert.ToInt32(activity.Heartrate == 0 ? 140 : activity.Heartrate), Convert.ToInt32(activity.Speed * 100), Convert.ToInt32(activity.Distance * 0.002), "#0099cc");
            }

            // add the current week
            var thisWeekSelection = Activities.Where(a => a.Date.Year == now.Year).Where(b => b.Week == HalbotActivity.WeekOfYear(now));
            foreach (var activity in thisWeekSelection)
            {
                data1.Add(Convert.ToInt32(activity.Heartrate == 0 ? 140 : activity.Heartrate), Convert.ToInt32(activity.Speed * 100), Convert.ToInt32(activity.Distance * 0.002), "gold");
            }

            // redraw the last activity in a highlighted color
            var lastActivity = Activities.OrderByDescending(a => a.Date).FirstOrDefault();
            if (lastActivity != null)
            {
                data1.Add(Convert.ToInt32(lastActivity.Heartrate == 0 ? 140 : lastActivity.Heartrate), Convert.ToInt32(lastActivity.Speed * 100), Convert.ToInt32(lastActivity.Distance * 0.002), "#2db92d");
            }
            
            CircleChart.SetDataSet(data1);
        }
    }
}
