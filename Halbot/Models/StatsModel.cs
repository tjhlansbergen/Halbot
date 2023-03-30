using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Halbot.Models
{
    public class StatsModel
    {
        //properties
        public List<HalbotActivity> Activities { get; private set; }

        //properties
        //stats
        public List<Tuple<String, String, String>> Week { get; private set; }
        public List<Tuple<String, String, String>> Month { get; private set; }
        public List<Tuple<String, String, String>> Year { get; private set; }
        public List<Tuple<String, String>> Alltime { get; private set; }

        //properties
        //bests
        public List<HalbotActivity> LongestRuns { get; private set; }
        public List<HalbotActivity> FastestRuns { get; private set; }
        public List<HalbotActivity> HighestRuns { get; private set; }
        public List<HalbotActivity> HighestElevation { get; private set; }
        public HalbotActivity LowestElevation { get; private set; }
        public List<HalbotActivity> BestEffort { get; private set; }

        public List<KeyValuePair<String, double>> BestWeeks { get; private set; }
        public List<KeyValuePair<String, double>> BestMonths { get; private set; }
        public List<KeyValuePair<String, double>> BestYears { get; private set; }


        //constructor
        public StatsModel(List<HalbotActivity> activities)
        {
            //initialize
            Activities = activities;

            _buildStats();
            _buildWeekStats();
            _buildMonthStats();
            _buildYearStats();

            //bests
            LongestRuns = Activities.OrderByDescending(run => run.Distance).Take(5).ToList<HalbotActivity>();
            FastestRuns = Activities.OrderByDescending(run => run.Speed).Take(5).ToList<HalbotActivity>();
            HighestRuns = Activities.OrderByDescending(run => run.Climb).Take(5).ToList<HalbotActivity>();
            BestEffort = Activities.OrderByDescending(run => run.Effort).Take(5).ToList<HalbotActivity>();
            HighestElevation = Activities.OrderByDescending(run => run.MaxElevation).Take(5).ToList<HalbotActivity>();
            LowestElevation = Activities.OrderBy(run => run.MinElevation).First();


            var weeks = Activities.GroupBy(run => new { run.Date.Year, run.Week });
            BestWeeks = new List<KeyValuePair<String, double>>();
            foreach (var items in weeks)
            {
                var first = items.First<HalbotActivity>();
                DateTime monday = HalbotActivity.FirstDateOfWeekIso8601(first.Date.Year, first.Week);
                BestWeeks.Add(new KeyValuePair<string, double>(
                    $"{first.Date.Year} week {first.Week} ({monday.ToString("dd MMMM")} - {monday.AddDays(6).ToString("dd MMMM")})", Math.Round(items.Sum(run => run.Distance) / 1000, 2)));
            }
            BestWeeks = BestWeeks.OrderByDescending(a => a.Value).Take(5).ToList();

            var months = Activities.GroupBy(run => new { run.Date.Year, run.Date.Month });
            BestMonths = new List<KeyValuePair<String, double>>();
            foreach (var items in months)
            {
                BestMonths.Add(new KeyValuePair<String, double>(items.First<HalbotActivity>().Date.ToString("yyyy MMMM "), items.Sum(run => run.Distance) / 1000));
            }
            BestMonths = BestMonths.OrderByDescending(a => a.Value).Take(5).ToList();

            var years = Activities.GroupBy(run => new { run.Date.Year });
            BestYears = new List<KeyValuePair<String, double>>();
            foreach (var items in years)
            {
                BestYears.Add(new KeyValuePair<String, double>(items.First<HalbotActivity>().Date.ToString("yyyy "), items.Sum(run => run.Distance) / 1000));
            }
            BestYears = BestYears.OrderByDescending(a => a.Value).Take(5).ToList();
        }


        /// <summary>
        /// helper for getting weekly stats
        /// </summary>
        private void _buildWeekStats()
        {
            DateTime now = DateTime.Now;

            //get selection of activities per week
            var this_week_selection = Activities.Where(a => a.Date.Year == now.Year).Where(b => b.Week == HalbotActivity.WeekOfYear(now));
            var last_week_selection = Activities.Where(a => a.Date.Year == now.Year).Where(b => b.Week == HalbotActivity.WeekOfYear(now) - 1);
            if (HalbotActivity.WeekOfYear(now) == 1) last_week_selection = Activities.Where(a => a.Date.Year == now.Year - 1).Where(b => b.Week == 52);

            //create list and populate
            Week = new List<Tuple<string, string, string>>();
            Week.Add(new Tuple<String, String, String>("Activities:", last_week_selection.Count().ToString(), this_week_selection.Count().ToString()));
            Week.Add(new Tuple<string, string, string>("Total effort:", $"{last_week_selection.Sum(e => e.Effort)}",
                $"{this_week_selection.Sum(e => e.Effort)}"));
            Week.Add(new Tuple<String, String, String>("Total distance:",
                $"{Math.Round(last_week_selection.Sum(c => c.Distance) / 1000, 2)}km",
                $"{Math.Round(this_week_selection.Sum(c => c.Distance) / 1000, 2)}km"));
            Week.Add(new Tuple<String, String, String>("Total climb:",
                $"{Math.Round(last_week_selection.Sum(c => c.Climb))}m",
                $"{Math.Round(this_week_selection.Sum(c => c.Climb))}m"));
            Week.Add(new Tuple<String, String, String>("Average effort:", _averageEffort(last_week_selection), _averageEffort(this_week_selection)));
            Week.Add(new Tuple<String, String, String>("Average pace:", _averagePace(last_week_selection), _averagePace(this_week_selection)));
            Week.Add(new Tuple<String, String, String>("Average heartrate:", _averageHeartrate(last_week_selection), _averageHeartrate(this_week_selection)));
            Week.Add(new Tuple<String, String, String>("Average distance:", _averageDistance(last_week_selection), _averageDistance(this_week_selection)));
        }


        /// <summary>
        /// Helper for creating monthly stats
        /// </summary>
        private void _buildMonthStats()
        {
            DateTime now = DateTime.Now;

            //get selection of activities per month
            var this_month_selection = Activities.Where(a => a.Date.Year == now.Year).Where(b => b.Date.Month == now.Month);
            var last_month_selection = Activities.Where(a => a.Date.Year == now.Year).Where(b => b.Date.Month == now.Month - 1);
            if (now.Month == 1) last_month_selection = Activities.Where(a => a.Date.Year == now.Year - 1).Where(b => b.Date.Month == 12);

            //create list and populate
            Month = new List<Tuple<string, string, string>>();
            Month.Add(new Tuple<String, String, String>("Activities:", last_month_selection.Count().ToString(), this_month_selection.Count().ToString()));
            Month.Add(new Tuple<String, String, String>("Total effort:", $"{last_month_selection.Sum(c => c.Effort)}",
                $"{this_month_selection.Sum(c => c.Effort)}"));
            Month.Add(new Tuple<String, String, String>("Total distance:",
                $"{Math.Round(last_month_selection.Sum(c => c.Distance) / 1000, 2)}km",
                $"{Math.Round(this_month_selection.Sum(c => c.Distance) / 1000, 2)}km"));
            Month.Add(new Tuple<String, String, String>("Total climb:", 
                $"{Math.Round(last_month_selection.Sum(c => c.Climb))}m",
                $"{Math.Round(this_month_selection.Sum(c => c.Climb))}m"));
            Month.Add(new Tuple<String, String, String>("Average effort:", _averageEffort(last_month_selection), _averageEffort(this_month_selection)));
            Month.Add(new Tuple<String, String, String>("Average pace:", _averagePace(last_month_selection), _averagePace(this_month_selection)));
            Month.Add(new Tuple<String, String, String>("Average heartrate:", _averageHeartrate(last_month_selection), _averageHeartrate(this_month_selection)));
            Month.Add(new Tuple<String, String, String>("Average distance:", _averageDistance(last_month_selection), _averageDistance(this_month_selection)));

        }

        /// <summary>
        /// Helper for creating yearly stats
        /// </summary>
        private void _buildYearStats()
        {
            DateTime now = DateTime.Now;

            //get selection of activities per year
            var this_year_selection = Activities.Where(a => a.Date.Year == now.Year);
            var last_year_selection = Activities.Where(a => a.Date.Year == now.Year - 1);

            //create list and populate
            Year = new List<Tuple<string, string, string>>();
            Year.Add(new Tuple<String, String, String>("Activities:", last_year_selection.Count().ToString(), this_year_selection.Count().ToString()));
            Year.Add(new Tuple<String, String, String>("Total effort:", $"{last_year_selection.Sum(c => c.Effort)}",
                $"{this_year_selection.Sum(c => c.Effort)}"));
            Year.Add(new Tuple<String, String, String>("Total distance:",
                $"{Math.Round(last_year_selection.Sum(c => c.Distance) / 1000, 2)}km", $"{Math.Round(this_year_selection.Sum(c => c.Distance) / 1000, 2)}km"));
            Year.Add(new Tuple<String, String, String>("Total climb:", $"{Math.Round(last_year_selection.Sum(c => c.Climb))}m", $"{Math.Round(this_year_selection.Sum(c => c.Climb))}m"));
            Year.Add(new Tuple<String, String, String>("Average effort:", _averageEffort(last_year_selection), _averageEffort(this_year_selection)));
            Year.Add(new Tuple<String, String, String>("Average pace:", _averagePace(last_year_selection), _averagePace(this_year_selection)));
            Year.Add(new Tuple<String, String, String>("Average heartrate:", _averageHeartrate(last_year_selection), _averageHeartrate(this_year_selection)));
            Year.Add(new Tuple<String, String, String>("Average distance:", _averageDistance(last_year_selection), _averageDistance(this_year_selection)));

        }

        /// <summary>
        /// helper for calculating average pace that is safe when input is null
        /// </summary>
        /// <param name="activities">collection of activities</param>
        /// <returns></returns>
        private string _averagePace(IEnumerable<HalbotActivity> activities)
        {
            try
            {
                return $"{HalbotActivity.PaceForSpeed(activities.Average(d => d.Speed))}m/km";
            }
            catch (Exception)
            {
                //unable to return an average, return hyphen
                return "-";
            }
        }

        /// <summary>
        /// helper for calculating average heartrate that is safe when input is null, omits invalid (less than 100 bpm) heartrates
        /// </summary>
        /// <param name="activities">collection of activities</param>
        /// <returns></returns>
        private string _averageHeartrate(IEnumerable<HalbotActivity> activities)
        {
            try
            {
                return $"{Math.Round(activities.Where(a => a.Heartrate > 100).Average(d => d.Heartrate))}bpm";
            }
            catch (Exception)
            {
                //unable to return an average, return hyphen
                return "-";
            }
        }

        /// <summary>
        /// helper for calculating average heartrate that is safe when input is null, omits invalid (less than 100 bpm) heartrates
        /// </summary>
        /// <param name="activities">collection of activities</param>
        /// <returns></returns>
        private string _averageEffort(IEnumerable<HalbotActivity> activities)
        {
            try
            {
                return $"{Math.Round(activities.Average(e => e.Effort))}";
            }
            catch (Exception)
            {
                //unable to return an average, return hyphen
                return "-";
            }
        }

        /// <summary>
        /// helper for calculating rounded average distance that is safe when input is null
        /// </summary>
        /// <param name="activities">collection of activities</param>
        /// <returns></returns>
        private string _averageDistance(IEnumerable<HalbotActivity> activities)
        {
            try
            {
                return $"{Math.Round(activities.Average(a => a.Distance) / 1000, 2)}km";
            }
            catch (Exception)
            {
                //unable to return an average, return hyphen
                return "-";
            }
        }

        //helper for constructor
        private void _buildStats()
        {

            Alltime = new List<Tuple<string, string>>();

            Alltime.Add(new Tuple<String, String>("Activities:", Activities.Count.ToString()));
            Alltime.Add(new Tuple<String, String>("Effort:", $"{Activities.Sum(c => c.Effort)}"));
            Alltime.Add(new Tuple<String, String>("Distance:", $"{Math.Round(Activities.Sum(c => c.Distance) / 1000, 2)}km"));
            Alltime.Add(new Tuple<String, String>("Climb:", $"{Math.Round(Activities.Sum(c => c.Climb))}m"));

            Alltime.Add(new Tuple<String, String>("Average pace:", _averagePace(Activities)));
            Alltime.Add(new Tuple<String, String>("Average heartrate:", _averageHeartrate(Activities)));
            Alltime.Add(new Tuple<String, String>("Average effort:", _averageEffort(Activities)));
            Alltime.Add(new Tuple<String, String>("Average distance:", _averageDistance(Activities)));

        }

    }
}
