using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halbot.Charts;
using Halbot.Data.Models;

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
            SpiderChart = new SpiderChart("Metrics", 900, 1500, 75, 10, 6);

            
            var data = new DataSet("activities");

            for (int i = 0; i < 10; i++)
            {
                var week = HalbotActivity.WeekOfYear(DateTime.Now) - i;
                if (week < 1) { week += 52; }
                var year = (week <= HalbotActivity.WeekOfYear(DateTime.Now)) ? DateTime.Now.Year : DateTime.Now.Year - 1;

                var row = new List<DataItem>();

                // get the activities for the week
                var rowActivities = Activities.Where(a => a.Date.Year == year && a.Week == week).OrderByDescending(a => a.Date);

                foreach (var a in rowActivities)
                {
                    row.Add(new DataItem { A = a.Distance, B = a.Speed, C = a.Heartrate, D = a.Climb, FillColor = GetColors(a.Date.DayOfWeek).Item1, StrokeColor = GetColors(a.Date.DayOfWeek).Item2 });
                }

                data.AddRow(row);
            }



            SpiderChart.SetDataSet(data);

        }

        private static Tuple<string, string> GetColors(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return new Tuple<string, string>("#FC600A", "#9D3802");
                case DayOfWeek.Tuesday:
                    return new Tuple<string, string>("#FCCB1A", "#B08A03");
                case DayOfWeek.Wednesday:
                    return new Tuple<string, string>("#98CA32", "#59761E");
                case DayOfWeek.Thursday:
                    return new Tuple<string, string>("#559E54", "#305A30");
                case DayOfWeek.Friday:
                    return new Tuple<string, string>("#1258DC", "#0A337F");
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return new Tuple<string, string>("#AE0D7A", "#510639");
                default:
                    return new Tuple<string, string>("grey", "black");
            }
        }

    }
}
