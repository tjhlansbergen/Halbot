using Halbot.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using Halbot.Code.Charts;

namespace Halbot.Models
{
    public class ChartsVolumeModel
    {
        public List<HalbotActivity> Activities { get; }
        public LineChart DistanceChart { get; set; }
        public LineChart ClimbChart { get; set; }

        public ChartsVolumeModel(List<HalbotActivity> activities)
        {
            //initialize data
            Activities = activities;

            // create charts
            DistanceChart = new LineChart("Distance", 860, 400);
            ClimbChart = new LineChart("Climb", 860, 400);

            FillChart(DistanceChart, "Distance",25, "goldenrod", "#c8981e"); // 1000, for meters to km / 40 to fit in graph = 25
            FillChart(ClimbChart, "Climb", 0.2, "darkseagreen", "#70a970"); // * 5 to fit in graph (2000m in 400px)
        }

        private void FillChart(LineChart volume, string property, double correctionFactor, string color1, string color2)
        {
            // create values, we want to end up with nine years of data, where the first year starts with the current date nine years back

            // we determine 52 datapoints per year (every seventh day), find out how many there are in the first and current year
            // thats the number of datapoints we'll use for the first year
            var thisYearPoints = DateTime.Now.DayOfYear / 7;
            var firstYearPoints = 52 - thisYearPoints;

            int closingValue = 0;

            // create datasets for last 9 years (including the current year)
            for (int i = DateTime.Now.Year - 8; i <= DateTime.Now.Year; i++)
            {
                List<int> values = new List<int>
                {
                    closingValue
                };

                // get values
                for (int j = 0; j < 53; j++)
                {
                    var pointDate = new DateTime(i, 1, 1).AddDays(j * 7);
                    double total = 0;


                    total = property switch
                    {
                        "Distance" => Activities.Where(a => a.Date > pointDate.AddDays(-28) && a.Date <= pointDate).Sum(a => a.Distance),
                        "Climb" => Activities.Where(a => a.Date > pointDate.AddDays(-28) && a.Date <= pointDate).Sum(a => a.Climb),
                        _ => 0,
                    };
                    values.Add((int) Math.Round(total / 28 / correctionFactor)); // 28 for average   
                }

                // if this is the first year, slice that year up to the current date
                if (i == DateTime.Now.Year - 8)
                {
                    values = values.TakeLast(firstYearPoints).ToList();
                }

                //if this is the current year, slice of the remainder of the year
                if (i == DateTime.Now.Year)
                {
                    values = values.Take(thisYearPoints).ToList();
                }

                // create dataset
                var dataSet = new LineChart.DataSet
                {
                    Name = i.ToString(),
                    Color = AlternatingColor(i, color1, color2),
                    Values = values
                };

                volume.AddDataSet(dataSet);

                closingValue = values.Last();
            }
        }


        /// <summary>
        /// Pass in any integer and get a color returned
        /// </summary>
        /// <returns>A HTML colorcode as string</returns>
        private string AlternatingColor(int value, string color1, string color2)
        {
            string[] colors = { color1, color2 };

            return colors[value % 2];
        }
    }
}
