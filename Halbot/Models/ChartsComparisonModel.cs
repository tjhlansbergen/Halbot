using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Halbot.Models
{
    public class ChartsComparisonModel
    {
        public List<HalbotActivity> Activities { get; }

        public Dictionary<int, int> Kilometers { get; private set; }
        public Dictionary<int, int> Runs { get; private set; }
        public Dictionary<int, int> FastRuns { get; private set; }
        public Dictionary<int, int> Climb { get; private set; }
        public Dictionary<int, int> ClimbRuns { get; private set; }

        public ChartsComparisonModel(List<HalbotActivity> activities)
        {
            //initialize data
            Activities = activities;
            Fill(5);
        }

        private void Fill(int numberOfYears)
        {
            
            Kilometers = new Dictionary<int, int>();
            Runs = new Dictionary<int, int>();
            FastRuns = new Dictionary<int, int>();
            Climb = new Dictionary<int, int>();
            ClimbRuns = new Dictionary<int, int>();

            for (int i = 0; i < numberOfYears; i++)
            {
                int year = DateTime.Now.Year - i;

                int kms = Convert.ToInt32(Activities.Where(a => a.Date.Year == year && a.Date.DayOfYear <= DateTime.Now.DayOfYear).Sum(a => a.Distance) / 1000);
                Kilometers.Add(year, kms);

                int runs = Activities.Count(a => a.Date.Year == year && a.Date.DayOfYear <= DateTime.Now.DayOfYear);
                Runs.Add(year, runs);

                int fastruns = Activities.Count(a => a.Date.Year == year && a.Date.DayOfYear <= DateTime.Now.DayOfYear && a.Speed > 3.333);
                FastRuns.Add(year, fastruns);

                int climb = Convert.ToInt32(Activities.Where(a => a.Date.Year == year && a.Date.DayOfYear <= DateTime.Now.DayOfYear).Sum(a => a.Climb));
                Climb.Add(year, climb);

                int climbruns = Activities.Count(a => a.Date.Year == year && a.Date.DayOfYear <= DateTime.Now.DayOfYear && a.Climb > 99);
                ClimbRuns.Add(year, climbruns);
            }


        }
    }
}
