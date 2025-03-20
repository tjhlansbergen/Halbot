using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Halbot.Code.Charts;
using Halbot.Data.Records;

namespace Halbot.Models
{
    public class ChartsProgressionModel
    {
        //properties
        public List<HalbotActivity> Activities { get; }
        public List<WorkoutRecord> Workouts { get; }

        public ColumnChart LastXVolume { get; }
        public ColumnChart LastXPace { get; }
        public ColumnChart LastXClimb { get; }

        public ColumnChart LastWeekVolume { get; }
        public ColumnChart LastWeekPace { get; }
        public ColumnChart LastWeekClimb { get; }

        public ColumnChart LastWeekWorkout { get; }

        public ColumnChart LastMonthVolume { get; }
        public ColumnChart LastMonthPace { get; }
        public ColumnChart LastMonthClimb { get; }

        //constructor
        public ChartsProgressionModel(List<HalbotActivity> activities, List<WorkoutRecord> workouts)
        {
            //initialize data
            Activities = activities;
            Workouts = workouts;

            // init charts
            LastXVolume = GetLastXVolume(14);
            LastXPace = GetLastXPace(14);
            LastXClimb = GetLastXClimb(14);

            LastWeekVolume = GetLastWeekVolume(14);
            LastWeekPace = GetLastWeekPace(14);
            LastWeekClimb = GetLastWeekClimb(14);
            LastWeekWorkout = GetLastWeekWorkout(14);

            LastMonthVolume = GetLastMonthVolume(14);
            LastMonthPace = GetLastMonthPace(14);
            LastMonthClimb = GetLastMonthClimb(14);
        }

        private ColumnChart GetLastXVolume(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("lastvolume", 200);
            var runs = Activities.OrderByDescending(a => a.Date).Take(x).Reverse().ToList();

            ColumnChart.DataSet volume = new ColumnChart.DataSet("volume");

            foreach (var run in runs)
            {
                volume.Add(run.Date.ToString("dd-MM"), $"{(run.Distance / 1000):0.00}", run.Distance / 1000);
            }

            chart.AddDataSet(volume);
            return chart;
        }

        private ColumnChart GetLastXPace(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("lastpace", 200);
            var runs = Activities.OrderByDescending(a => a.Date).Take(x).Reverse().ToList();

            ColumnChart.DataSet pace = new ColumnChart.DataSet("pace");

            foreach (var run in runs)
            {
                pace.Add(run.Date.ToString("dd-MM"), run.Pace, run.Speed * run.Speed * run.Speed * run.Speed);
            }

            chart.AddDataSet(pace);
            return chart;
        }

        private ColumnChart GetLastXClimb(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("lastclimb", 200);
            var runs = Activities.OrderByDescending(a => a.Date).Take(x).Reverse().ToList();

            ColumnChart.DataSet climb = new ColumnChart.DataSet("climb");

            foreach (var run in runs)
            {
                climb.Add(run.Date.ToString("dd-MM"), Math.Round(run.Climb).ToString(CultureInfo.InvariantCulture), run.Climb);
            }

            chart.AddDataSet(climb);
            return chart;
        }

        private ColumnChart GetLastWeekVolume(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("weekvolume", 200);
            var weeks = Activities.OrderByDescending(run => run.Date).GroupBy(run => new { run.Date.Year, run.Week }).Take(x).Reverse().ToList();

            ColumnChart.DataSet volume = new ColumnChart.DataSet("volume");

            foreach (var week in weeks)
            {
                double sum = week.Sum<HalbotActivity>(run => run.Distance / 1000);
                volume.Add(week.First<HalbotActivity>().Week.ToString(), $"{sum:0.00}", sum);
            }

            chart.AddDataSet(volume);
            return chart;
        }

        private ColumnChart GetLastWeekPace(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("weekpace", 200);
            var weeks = Activities.OrderByDescending(run => run.Date).GroupBy(run => new { run.Date.Year, run.Week }).Take(x).Reverse().ToList();

            ColumnChart.DataSet pace = new ColumnChart.DataSet("pace");

            foreach (var week in weeks)
            {
                double avg_speed = week.Average<HalbotActivity>(run => run.Speed);
                pace.Add(week.First<HalbotActivity>().Week.ToString(), $"{HalbotActivity.PaceForSpeed(avg_speed)}", avg_speed * avg_speed * avg_speed * avg_speed);
            }

            chart.AddDataSet(pace);
            return chart;
        }

        private ColumnChart GetLastWeekClimb(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("weekclimb", 200);
            var weeks = Activities.OrderByDescending(run => run.Date).GroupBy(run => new { run.Date.Year, run.Week }).Take(x).Reverse().ToList();

            ColumnChart.DataSet climb = new ColumnChart.DataSet("climb");

            foreach (var week in weeks)
            {
                double sum = week.Sum<HalbotActivity>(run => run.Climb);
                climb.Add(week.First<HalbotActivity>().Week.ToString(), $"{sum:0}", sum);
            }

            chart.AddDataSet(climb);
            return chart;
        }

        private ColumnChart GetLastWeekWorkout(int x)
        {
            ColumnChart chart = new ColumnChart("weekworkout", 200);
            var weeks = Workouts.OrderByDescending(w => w.Date).GroupBy(w => new { w.Date.Year, w.Week }).Take(x).Reverse().ToList();

            ColumnChart.DataSet workout = new ColumnChart.DataSet("workout");

            foreach (var week in weeks)
            {
                double sum = week.Sum(w => w.Minutes);
                workout.Add(week.First().Week.ToString(), $"{sum:0}", sum);
            }

            chart.AddDataSet(workout);
            return chart;
        }

        private ColumnChart GetLastMonthVolume(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("monthvolume", 200);
            var months = Activities.OrderByDescending(run => run.Date).GroupBy(run => new { run.Date.Year, run.Date.Month }).Take(x).Reverse().ToList();

            ColumnChart.DataSet volume = new ColumnChart.DataSet("volume");

            foreach (var month in months)
            {
                double sum = month.Sum<HalbotActivity>(run => run.Distance / 1000);
                volume.Add(month.First<HalbotActivity>().Date.ToString("MMM"), $"{sum:0.00}", sum);
            }

            chart.AddDataSet(volume);
            return chart;
        }

        private ColumnChart GetLastMonthPace(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("monthpace", 200);
            var months = Activities.OrderByDescending(run => run.Date).GroupBy(run => new { run.Date.Year, run.Date.Month }).Take(x).Reverse().ToList();

            ColumnChart.DataSet pace = new ColumnChart.DataSet("pace");

            foreach (var month in months)
            {
                double avg_speed = month.Average<HalbotActivity>(run => run.Speed);
                pace.Add(month.First<HalbotActivity>().Date.ToString("MMM"), $"{HalbotActivity.PaceForSpeed(avg_speed)}", avg_speed * avg_speed * avg_speed * avg_speed);
            }

            chart.AddDataSet(pace);
            return chart;
        }

        private ColumnChart GetLastMonthClimb(int x)
        {
            if (x > Activities.Count) x = Activities.Count;

            ColumnChart chart = new ColumnChart("monthclimb", 200);
            var months = Activities.OrderByDescending(run => run.Date).GroupBy(run => new { run.Date.Year, run.Date.Month }).Take(x).Reverse().ToList();

            ColumnChart.DataSet climb = new ColumnChart.DataSet("climb");

            foreach (var month in months)
            {
                double sum = month.Sum<HalbotActivity>(run => run.Climb);
                climb.Add(month.First<HalbotActivity>().Date.ToString("MMM"), $"{sum:0}", sum);
            }

            chart.AddDataSet(climb);
            return chart;
        }
    }
}
