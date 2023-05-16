using System;
using Halbot.Data.Records;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Halbot.Code.Charts;
using System.Text;
using System.Globalization;

namespace Halbot.Models
{
    public class PlanWeek
    {
        public int WeekMileage { get; set; }
        public IEnumerable<PlanRecord> Runs { get; set; }
    }

    public class PlanModel
    {
        // properties
        public Dictionary<int, PlanWeek> Weeks { get; }

        public ColumnChart Chart { get; }

        // constructor
        public PlanModel(List<PlanRecord> planRecords)
        {
            //initialize (our cache holds all plan records, here we select those we'd like to show 
            var currentWeekNumber = DateTime.UtcNow.Week();
            Weeks = Enumerable.Range(currentWeekNumber, 10)
                .Select(w => new
                {
                    WeekNumber = w,
                    Records = planRecords.Where(r => r.Date.Week() == w)
                })
                .ToDictionary(i => i.WeekNumber, i => new PlanWeek
                {
                    Runs = i.Records,
                    WeekMileage = WeeklyMileage(i.Records)
                })
                .Where(p => p.Value.Runs.Any())
                .ToDictionary(p => p.Key, p => p.Value);

            Chart = FillChart(Weeks);
        }

        private static int WeeklyMileage(IEnumerable<PlanRecord> runs)
        {
            return runs.Where(r => 
                    !string.IsNullOrWhiteSpace(r.Description) 
                    && r.Description.Split("km").Any() 
                    && int.TryParse(r.Description.Split("km")[0], out _))
                .Select(r => int.Parse(r.Description.Split("km")[0]))
                .Sum();
        }

        private static ColumnChart FillChart(Dictionary<int, PlanWeek> weeks)
        {
            var chart = new ColumnChart("planchart", 100);
            var volume = new ColumnChart.DataSet("plan");

            foreach (var week in weeks)
            {
                volume.Add(week.Key.ToString(), string.Empty, week.Value.WeekMileage);
            }

            chart.AddDataSet(volume);
            return chart;
        }
    }
}
